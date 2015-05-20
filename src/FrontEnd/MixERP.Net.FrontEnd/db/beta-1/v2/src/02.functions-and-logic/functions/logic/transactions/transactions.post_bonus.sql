DROP FUNCTION IF EXISTS transactions.post_bonus(_user_id integer, _login_id bigint, _office_id integer, _value_date date);

CREATE FUNCTION transactions.post_bonus(_user_id integer, _login_id bigint, _office_id integer, _value_date date)
RETURNS void
VOLATILE
AS
$$
    DECLARE _frequency_id           integer;
    DECLARE _transaction_master_id  bigint;
    DECLARE _tran_counter           integer;
    DECLARE _transaction_code       text;
    DECLARE _default_currency_code  national character varying(12);
    DECLARE _sys                    integer = office.get_sys_user_id();
    DECLARE this                    RECORD;
BEGIN
    IF(_value_date != transactions.get_value_date(_office_id)) THEN
        RAISE EXCEPTION 'Invalid value date.'
        USING ERRCODE='P3007';
    END IF;

    IF NOT EXISTS(SELECT 1 FROM core.bonus_slabs WHERE ends_on >= _value_date) THEN
        RETURN;
    END IF;

    IF NOT EXISTS(SELECT 1 FROM core.salesperson_bonus_setups) THEN
        RETURN;
    END IF;

    SELECT frequency_id INTO _frequency_id
    FROM core.frequency_setups
    WHERE value_date = _value_date;

    IF(COALESCE(_frequency_id, 0) = 0) THEN
        --Today does not fall on a frequency
        RETURN;
    END IF;


    DROP TABLE IF EXISTS bonus_slab_temp;
    CREATE TEMPORARY TABLE bonus_slab_temp
    (
        bonus_slab_id               integer,
        bonus_slab_code             text,
        bonus_slab_name             text,
        checking_frequency_id       integer,
        amount_from                 public.money_strict,
        amount_to                   public.money_strict,
        bonus_rate                  numeric,
        salesperson_id              integer,
        period_from                 date,
        period_to                   date,
        salesperson_account_id      bigint,
        bonus_account_id            bigint,
        statement_reference         national character varying(100)
    ) ON COMMIT DROP;

    INSERT INTO bonus_slab_temp(bonus_slab_id, bonus_slab_code, bonus_slab_name, checking_frequency_id, amount_from, amount_to, bonus_rate, salesperson_id, salesperson_account_id, bonus_account_id, statement_reference)
    SELECT
        core.bonus_slabs.bonus_slab_id,
        core.bonus_slabs.bonus_slab_code,
        core.bonus_slabs.bonus_slab_name,
        core.bonus_slabs.checking_frequency_id,
        core.bonus_slab_details.amount_from,
        core.bonus_slab_details.amount_to,
        core.bonus_slab_details.bonus_rate,
        core.salesperson_bonus_setups.salesperson_id,
        core.salespersons.account_id,
        core.bonus_slabs.account_id,
        core.bonus_slabs.statement_reference
    FROM core.bonus_slab_details
    INNER JOIN core.bonus_slabs
    ON core.bonus_slabs.bonus_slab_id = core.bonus_slab_details.bonus_slab_id
    INNER JOIN core.salesperson_bonus_setups
    ON core.salesperson_bonus_setups.bonus_slab_id = core.bonus_slabs.bonus_slab_id
    INNER JOIN core.salespersons
    ON core.salespersons.salesperson_id = core.salesperson_bonus_setups.salesperson_id
    WHERE ends_on >= _value_date::date
    AND _frequency_id = ANY(core.get_frequencies(core.bonus_slabs.checking_frequency_id));

    IF(SELECT COUNT(*) FROM bonus_slab_temp) = 0 THEN
        --Nothing found to post today
        RETURN;
    END IF;

    UPDATE bonus_slab_temp
    SET period_to = _value_date,
    period_from = core.get_frequency_start_date(_frequency_id, _value_date);

    DROP TABLE IF EXISTS bonus_temp;
    CREATE TEMPORARY TABLE bonus_temp
    (
        id                      SERIAL,
        salesperson_id          integer,
        period_from             date,
        period_to               date,
        sales                   public.money_strict2,
        bonus_rate              numeric,
        bonus                   numeric,
        salesperson_account_id  bigint,
        bonus_account_id        bigint,
        statement_reference     national character varying(100)
    ) ON COMMIT DROP;

    INSERT INTO bonus_temp(salesperson_id, period_from, period_to, salesperson_account_id, bonus_account_id, statement_reference)
    SELECT 
    DISTINCT 
        bonus_slab_temp.salesperson_id, 
        bonus_slab_temp.period_from, 
        bonus_slab_temp.period_to,
        bonus_slab_temp.salesperson_account_id,
        bonus_slab_temp.bonus_account_id,        
        bonus_slab_temp.statement_reference
    FROM bonus_slab_temp;
    
    UPDATE bonus_temp
    SET sales = 
    (
        SELECT
            SUM
            (
                (
                    COALESCE(quantity, 0)
                    * 
                    COALESCE(price, 0)
                ) - COALESCE(discount, 0)
            ) AS total
        FROM transactions.transaction_master
        INNER JOIN transactions.stock_master
        ON transactions.transaction_master.transaction_master_id = transactions.stock_master.transaction_master_id
        INNER JOIN transactions.stock_details
        ON transactions.stock_details.stock_master_id = transactions.stock_master.stock_master_id
        WHERE transactions.transaction_master.verification_status_id > 0
        AND transactions.stock_master.salesperson_id = bonus_temp.salesperson_id
        AND transactions.transaction_master.book = ANY(ARRAY['Sales.Direct', 'Sales.Delivery'])
        AND transactions.transaction_master.value_date
        BETWEEN bonus_temp.period_from AND bonus_temp.period_to
   );

   
    UPDATE bonus_temp
    SET bonus_rate = 
    (
        SELECT bonus_slab_temp.bonus_rate
        FROM bonus_slab_temp
        WHERE bonus_slab_temp.salesperson_id = bonus_temp.salesperson_id
        AND bonus_temp.sales > bonus_slab_temp.amount_from
        AND bonus_temp.sales <= bonus_slab_temp.amount_to
    );


    UPDATE bonus_temp
    SET bonus = ROUND(bonus_temp.sales * bonus_temp.bonus_rate / 100, 2);

    UPDATE bonus_temp
    SET statement_reference = REPLACE(bonus_temp.statement_reference, '{From}', bonus_temp.period_from::text);

    UPDATE bonus_temp
    SET statement_reference = REPLACE(bonus_temp.statement_reference, '{To}', bonus_temp.period_to::text);


    _default_currency_code                  := transactions.get_default_currency_code_by_office_id(_office_id);

    FOR this IN
    SELECT bonus_temp.id 
    FROM bonus_temp 
    WHERE COALESCE(bonus_temp.bonus, 0) > 0
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
            'Bonus.Slab', 
            _value_date, 
            _user_id, 
            _office_id,             
            bonus_temp.statement_reference,
            1,
            _sys,
            _sys,
            'Automatically verified by workflow.'
        FROM bonus_temp
        WHERE bonus_temp.id  = this.id
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
            'Cr',
            bonus_temp.salesperson_account_id,
            bonus_temp.statement_reference,
            _default_currency_code, 
            bonus_temp.bonus, 
            1 AS exchange_rate,
            _default_currency_code,
            bonus_temp.bonus
        FROM bonus_temp
        WHERE bonus_temp.id = this.id

        UNION ALL
        SELECT
            _transaction_master_id,
            _value_date,
            'Dr',
            bonus_temp.bonus_account_id,
            bonus_temp.statement_reference,
            _default_currency_code, 
            bonus_temp.bonus, 
            1 AS exchange_rate,
            _default_currency_code,
            bonus_temp.bonus
        FROM bonus_temp
        WHERE bonus_temp.id = this.id;
    END LOOP;    
END
$$
LANGUAGE plpgsql;

DELETE FROM transactions.routines where routine_code='REF-POBNS';
SELECT transactions.create_routine('POST-BNS', 'transactions.post_bonus', 201);

--select * from transactions.post_bonus(2, 5, 2, '2015-04-13');



