DROP FUNCTION IF EXISTS transactions.post_late_fee(_user_id integer, _login_id bigint, _office_id integer, _value_date date);

CREATE FUNCTION transactions.post_late_fee(_user_id integer, _login_id bigint, _office_id integer, _value_date date)
RETURNS void
VOLATILE
AS
$$
    DECLARE this                        RECORD;
    DECLARE _transaction_master_id      bigint;
    DECLARE _tran_counter               integer;
    DECLARE _transaction_code           text;
    DECLARE _sys                        integer = office.get_sys_user_id();
    DECLARE _default_currency_code      national character varying(12);
BEGIN
    IF(_value_date != transactions.get_value_date(_office_id)) THEN
        RAISE EXCEPTION 'Invalid value date.'
        USING ERRCODE='P3007';
    END IF;

    DROP TABLE IF EXISTS temp_late_fee;

    CREATE TEMPORARY TABLE temp_late_fee
    (
        transaction_master_id           bigint,
        value_date                      date,
        payment_term_id                 integer,
        payment_term_code               text,
        payment_term_name               text,        
        due_on_date                     boolean,
        due_days                        integer,
        due_frequency_id                integer,
        grace_period                    integer,
        late_fee_id                     integer,
        late_fee_posting_frequency_id   integer,
        late_fee_code                   text,
        late_fee_name                   text,
        is_flat_amount                  boolean,
        rate                            numeric(24, 4),
        due_amount                      public.money_strict2,
        late_fee                        public.money_strict2,
        party_id                        bigint,
        party_account_id                bigint,
        late_fee_account_id             bigint,
        due_date                        date
    ) ON COMMIT DROP;

    WITH unpaid_invoices
    AS
    (
        SELECT 
            transactions.transaction_master.transaction_master_id, 
            transactions.transaction_master.value_date,
            transactions.stock_master.payment_term_id,
            core.payment_terms.payment_term_code,
            core.payment_terms.payment_term_name,
            core.payment_terms.due_on_date,
            core.payment_terms.due_days,
            core.payment_terms.due_frequency_id,
            core.payment_terms.grace_period,
            core.payment_terms.late_fee_id,
            core.payment_terms.late_fee_posting_frequency_id,
            core.late_fee.late_fee_code,
            core.late_fee.late_fee_name,
            core.late_fee.is_flat_amount,
            core.late_fee.rate,
            0.00 as due_amount,
            0.00 as late_fee,
            transactions.stock_master.party_id,
            core.get_account_id_by_party_id(transactions.stock_master.party_id) AS party_account_id,
            core.late_fee.account_id AS late_fee_account_id
        FROM transactions.stock_master
        INNER JOIN transactions.transaction_master
        ON transactions.transaction_master.transaction_master_id = transactions.stock_master.transaction_master_id
        INNER JOIN core.payment_terms
        ON core.payment_terms.payment_term_id = transactions.stock_master.payment_term_id
        INNER JOIN core.late_fee
        ON core.payment_terms.late_fee_id = core.late_fee.late_fee_id
        WHERE transactions.transaction_master.verification_status_id > 0
        AND transactions.transaction_master.book = ANY(ARRAY['Sales.Delivery', 'Sales.Direct'])
        AND transactions.stock_master.is_credit AND NOT transactions.stock_master.credit_settled
        AND transactions.stock_master.payment_term_id IS NOT NULL
        AND core.payment_terms.late_fee_id IS NOT NULL
        AND transactions.transaction_master.transaction_master_id NOT IN
        (
            SELECT transactions.late_fee.transaction_master_id        --We have already posted the late fee before.
            FROM transactions.late_fee
        )
    ), 
    unpaid_invoices_details
    AS
    (
        SELECT *, 
        CASE WHEN unpaid_invoices.due_on_date
        THEN unpaid_invoices.value_date + unpaid_invoices.due_days + unpaid_invoices.grace_period
        ELSE core.get_frequency_end_date(unpaid_invoices.due_frequency_id, unpaid_invoices.value_date) +  unpaid_invoices.grace_period END as due_date
        FROM unpaid_invoices
    )


    INSERT INTO temp_late_fee
    SELECT * FROM unpaid_invoices_details
    WHERE unpaid_invoices_details.due_date <= _value_date;


    UPDATE temp_late_fee
    SET due_amount = 
    (
        SELECT
            SUM
            (
                (transactions.stock_details.quantity * transactions.stock_details.price) 
                - 
                transactions.stock_details.discount 
                + 
                transactions.stock_details.tax + 
                transactions.stock_details.shipping_charge
            )
        FROM transactions.stock_details
        INNER JOIN transactions.stock_master
        ON transactions.stock_master.stock_master_id = transactions.stock_details.stock_master_id
        WHERE transactions.stock_master.transaction_master_id = temp_late_fee.transaction_master_id
    ) WHERE NOT temp_late_fee.is_flat_amount;

    UPDATE temp_late_fee
    SET late_fee = temp_late_fee.rate
    WHERE temp_late_fee.is_flat_amount;

    UPDATE temp_late_fee
    SET late_fee = temp_late_fee.due_amount * temp_late_fee.rate / 100
    WHERE NOT temp_late_fee.is_flat_amount;

    _default_currency_code                  := transactions.get_default_currency_code_by_office_id(_office_id);

    FOR this IN
    SELECT * FROM temp_late_fee
    WHERE temp_late_fee.late_fee > 0
    AND temp_late_fee.party_account_id IS NOT NULL
    AND temp_late_fee.late_fee_account_id IS NOT NULL
    LOOP
        _transaction_master_id  := nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));
        _tran_counter           := transactions.get_new_transaction_counter(_value_date);
        _transaction_code       := transactions.get_transaction_code(_value_date, _office_id, _user_id, _login_id);

        INSERT INTO transactions.transaction_master
        (
            transaction_master_id, 
            transaction_counter, 
            transaction_code, 
            book, 
            value_date, 
            user_id, 
            office_id, 
            reference_number,
            statement_reference,
            verification_status_id,
            sys_user_id,
            verified_by_user_id,
            verification_reason
        ) 
        SELECT            
            _transaction_master_id, 
            _tran_counter, 
            _transaction_code, 
            'Late.Fee', 
            _value_date, 
            _user_id, 
            _office_id,             
            this.transaction_master_id::text AS reference_number,
            this.late_fee_name AS statement_reference,
            1,
            _sys,
            _sys,
            'Automatically verified by workflow.';

        INSERT INTO transactions.transaction_details
        (
            transaction_master_id,
            value_date,
            tran_type, 
            account_id, 
            statement_reference, 
            currency_code, 
            amount_in_currency, 
            er, 
            local_currency_code, 
            amount_in_local_currency
        )
        SELECT
            _transaction_master_id,
            _value_date,
            'Cr',
            this.late_fee_account_id,
            this.late_fee_name || ' (' || core.get_party_code_by_party_id(this.party_id) || ')',
            _default_currency_code, 
            this.late_fee, 
            1 AS exchange_rate,
            _default_currency_code,
            this.late_fee
        UNION ALL
        SELECT
            _transaction_master_id,
            _value_date,
            'Dr',
            this.party_account_id,
            this.late_fee_name,
            _default_currency_code, 
            this.late_fee, 
            1 AS exchange_rate,
            _default_currency_code,
            this.late_fee;

        INSERT INTO transactions.late_fee(transaction_master_id, party_id, value_date, late_fee_tran_id, amount)
        SELECT this.transaction_master_id, this.party_id, _value_date, _transaction_master_id, this.late_fee;
    END LOOP;
END
$$
LANGUAGE plpgsql;

SELECT transactions.create_routine('POST-LF', 'transactions.post_late_fee', 250);

--SELECT * FROM transactions.post_late_fee(2, 5, 2, transactions.get_value_date(2));
