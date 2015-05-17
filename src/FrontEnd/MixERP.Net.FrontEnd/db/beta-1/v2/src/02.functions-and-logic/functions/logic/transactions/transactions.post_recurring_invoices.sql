DROP FUNCTION IF EXISTS transactions.post_recurring_invoices(_user_id integer, _login_id bigint, _office_id integer, _value_date date);

CREATE FUNCTION transactions.post_recurring_invoices(_user_id integer, _login_id bigint, _office_id integer, _value_date date)
RETURNS void
AS
$$
    DECLARE _frequency_id           integer;
    DECLARE _frequencies            integer[];
    DECLARE _day                    double precision;
    DECLARE _transaction_master_id  bigint;
    DECLARE _tran_counter           integer;
    DECLARE _transaction_code       text;
    DECLARE _sys                    integer = office.get_sys_user_id();
    DECLARE _default_currency_code  national character varying(12);
    DECLARE this                    RECORD;
BEGIN
    IF(_value_date != transactions.get_value_date(_office_id)) THEN
        RAISE EXCEPTION 'Invalid value date.'
        USING ERRCODE='P3007';
    END IF;

    _default_currency_code          := transactions.get_default_currency_code_by_office_id(_office_id);

    DROP TABLE IF EXISTS recurring_invoices_temp;
    CREATE TEMPORARY TABLE recurring_invoices_temp
    (
        id                          SERIAL,
        recurring_invoice_setup_id  integer,
        tran_type                   public.transaction_type,
        party_id                    bigint,
        recurring_amount            public.money_strict2,
        account_id                  bigint NOT NULL,
        statement_reference         national character varying(100),
        transaction_master_id       bigint
    ) ON COMMIT DROP;

    SELECT frequency_id INTO _frequency_id
    FROM core.frequency_setups
    WHERE value_date = _value_date;

    _frequency_id   := COALESCE(_frequency_id, 0);
    _day            := EXTRACT(DAY FROM _value_date);
    _frequencies    := core.get_frequencies(_frequency_id);

    --INSERT RECURRING INVOICES THAT :
    -->RECUR BASED ON SAME CALENDAR DATE 
    -->AND OCCUR TODAY 
    -->AND HAVE DURATION RECURRENCE TYPE
    INSERT INTO recurring_invoices_temp(recurring_invoice_setup_id, tran_type, party_id, recurring_amount, account_id, statement_reference)
    SELECT 
        core.recurring_invoice_setup.recurring_invoice_setup_id,
        'Cr' AS tran_type,
        core.recurring_invoice_setup.party_id, 
        core.recurring_invoice_setup.recurring_amount, 
        core.recurring_invoice_setup.account_id,
        core.recurring_invoice_setup.statement_reference
    FROM core.recurring_invoice_setup
    WHERE 1 = 1
    AND is_active                                   --IS ACTIVE
    AND _value_date > starts_from                   --HAS NOT STARTED YET
    AND _value_date <= ends_on                      --HAS NOT ENDED YET
    AND recurs_on_same_calendar_date                --RECURS ON THE SAME CALENDAR DATE
    AND _day = EXTRACT(DAY FROM starts_from) - 1    --OCCURS TODAY
    AND recurrence_type_id IN                       --HAS DURATION RECURRENCE TYPE
    (
        SELECT recurrence_type_id FROM core.recurrence_types
        WHERE NOT is_frequency
    );
   
    --INSERT RECURRING INVOICES THAT :
    -->DO NOT RECUR BASED ON SAME CALENDAR DATE, BUT RECURRING DAYS
    -->AND OCCUR TODAY
    -->AND HAVE DURATION RECURRENCE TYPE
    INSERT INTO recurring_invoices_temp(recurring_invoice_setup_id, tran_type, party_id, recurring_amount, account_id, statement_reference)
    SELECT 
        core.recurring_invoice_setup.recurring_invoice_setup_id, 
        'Cr' AS tran_type,
        core.recurring_invoice_setup.party_id, 
        core.recurring_invoice_setup.recurring_amount, 
        core.recurring_invoice_setup.account_id,
        core.recurring_invoice_setup.statement_reference
    FROM core.recurring_invoice_setup
    WHERE 1 = 1
    AND is_active                                   --IS ACTIVE
    AND _value_date > starts_from                   --HAS NOT STARTED YET
    AND _value_date <= ends_on                      --HAS NOT ENDED YET
    AND NOT recurs_on_same_calendar_date            --DOES NOT RECUR ON THE SAME CALENDAR DATE, BUT RECURRING DAYS
    --OCCURS TODAY
    AND _value_date
    IN
    (
        SELECT 
        GENERATE_SERIES
        (
            starts_from::timestamp, 
            ends_on::timestamp, 
            (
                recurring_duration::text || 'days'
            )::interval
        )::date - INTERVAL '1 DAY'
    )
    AND recurrence_type_id IN                       --HAS DURATION RECURRENCE TYPE
    (
        SELECT recurrence_type_id FROM core.recurrence_types
        WHERE NOT is_frequency
    );
   
    --INSERT RECURRING INVOICES THAT :
    -->OCCUR TODAY 
    -->AND RECUR BASED ON FREQUENCIES
    INSERT INTO recurring_invoices_temp(recurring_invoice_setup_id, tran_type, party_id, recurring_amount, account_id, statement_reference)
    SELECT
        core.recurring_invoice_setup.recurring_invoice_setup_id, 
        'Cr' AS tran_type,
        core.recurring_invoice_setup.party_id, 
        core.recurring_invoice_setup.recurring_amount, 
        core.recurring_invoice_setup.account_id,
        core.recurring_invoice_setup.statement_reference    
    FROM core.recurring_invoice_setup
    WHERE 1 = 1
    AND is_active                                   --IS ACTIVE
    AND _value_date > starts_from                   --HAS NOT STARTED YET
    AND _value_date <= ends_on                      --HAS NOT ENDED YET
    AND recurring_frequency_id = ANY(_frequencies)  --OCCURS TODAY
    AND recurrence_type_id IN                       --RECURS BASED ON FREQUENCIES
    (
        SELECT recurrence_type_id FROM core.recurrence_types
        WHERE is_frequency
    );

    UPDATE recurring_invoices_temp
    SET statement_reference = REPLACE(recurring_invoices_temp.statement_reference, '{RIMonth}', to_char(date_trunc('month', _value_date), 'MON'));

    UPDATE recurring_invoices_temp
    SET statement_reference = REPLACE(recurring_invoices_temp.statement_reference, '{RIYear}', to_char(date_trunc('year', _value_date), 'YYYY'));

    INSERT INTO recurring_invoices_temp(recurring_invoice_setup_id, tran_type, party_id, recurring_amount, account_id, statement_reference)
    SELECT 
        recurring_invoices_temp.recurring_invoice_setup_id, 
        'Dr' AS tran_type,
        recurring_invoices_temp.party_id, 
        recurring_invoices_temp.recurring_amount, 
        core.get_account_id_by_party_id(recurring_invoices_temp.party_id), 
        recurring_invoices_temp.statement_reference
    FROM recurring_invoices_temp;


    FOR this IN
    SELECT DISTINCT recurring_invoices_temp.recurring_invoice_setup_id
    FROM recurring_invoices_temp
    WHERE COALESCE(recurring_invoices_temp.recurring_amount, 0) > 0
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
            'Recurring.Invoice', 
            _value_date, 
            _user_id, 
            _office_id,             
            recurring_invoices_temp.statement_reference,
            1,
            _sys,
            _sys,
            'Automatically verified by workflow.'
        FROM recurring_invoices_temp
        WHERE recurring_invoices_temp.recurring_invoice_setup_id  = this.recurring_invoice_setup_id
        LIMIT 1;

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
            recurring_invoices_temp.tran_type,
            recurring_invoices_temp.account_id,
            recurring_invoices_temp.statement_reference,
            _default_currency_code, 
            recurring_invoices_temp.recurring_amount, 
            1 AS exchange_rate,
            _default_currency_code,
            recurring_invoices_temp.recurring_amount
        FROM recurring_invoices_temp
        WHERE recurring_invoices_temp.recurring_invoice_setup_id  = this.recurring_invoice_setup_id;
    END LOOP;    
END
$$
LANGUAGE plpgsql;


DELETE FROM transactions.routines where routine_code='REF-PORCIV';
SELECT transactions.create_routine('POST-RCIV', 'transactions.post_recurring_invoices', 200);


--SELECT  * FROM transactions.post_recurring_invoices(2, 5, 2, '2015-04-17');
