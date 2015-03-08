DROP FUNCTION IF EXISTS transactions.post_recurring_invoices(_office_id integer);

CREATE FUNCTION transactions.post_recurring_invoices(_office_id integer)
RETURNS TABLE
(
        id                      integer,
        party_id                bigint,
        recurring_amount        public.money_strict2,
        account_id              bigint,
        statement_reference     national character varying(100)
)
AS
$$
    DECLARE _value_date         date='5/7/2015';
    DECLARE _frequency_id       integer; 
    DECLARE _day                double precision;
BEGIN
    DROP TABLE IF EXISTS recurring_invoices_temp;
    CREATE TEMPORARY TABLE recurring_invoices_temp
    (
        id                      SERIAL,
        party_id                bigint,
        recurring_amount        public.money_strict2,
        account_id              bigint,
        statement_reference     national character varying(100)
    ) ON COMMIT DROP;

    SELECT frequency_id INTO _frequency_id
    FROM core.frequency_setups
    WHERE value_date = _value_date;

    _frequency_id   := COALESCE(_frequency_id, 0);
    _day            := EXTRACT(DAY FROM _value_date);

    --INSERT RECURRING INVOICES THAT :
    -->RECUR BASED ON SAME CALENDAR DATE 
    -->AND OCCUR TODAY 
    -->AND HAVE DURATION RECURRENCE TYPE
    INSERT INTO recurring_invoices_temp(party_id, recurring_amount, account_id, statement_reference)
    SELECT 
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
    INSERT INTO recurring_invoices_temp(party_id, recurring_amount, account_id, statement_reference)
    SELECT 
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
    INSERT INTO recurring_invoices_temp(party_id, recurring_amount, account_id, statement_reference)
    SELECT
        core.recurring_invoice_setup.party_id, 
        core.recurring_invoice_setup.recurring_amount, 
        core.recurring_invoice_setup.account_id,
        core.recurring_invoice_setup.statement_reference    
    FROM core.recurring_invoice_setup
    WHERE 1 = 1
    AND is_active                                   --IS ACTIVE
    AND _value_date > starts_from                   --HAS NOT STARTED YET
    AND _value_date <= ends_on                      --HAS NOT ENDED YET
    AND recurring_frequency_id = _frequency_id      --OCCURS TODAY
    AND recurrence_type_id IN                       --RECURS BASED ON FREQUENCIES
    (
        SELECT recurrence_type_id FROM core.recurrence_types
        WHERE is_frequency
    );

    UPDATE recurring_invoices_temp
    SET statement_reference = REPLACE(recurring_invoices_temp.statement_reference, '{RIMonth}', to_char(date_trunc('month', _value_date), 'MON'));

    UPDATE recurring_invoices_temp
    SET statement_reference = REPLACE(recurring_invoices_temp.statement_reference, '{RIYear}', to_char(date_trunc('year', _value_date), 'YYYY'));



    RETURN QUERY
    SELECT *
    FROM recurring_invoices_temp;
END
$$
LANGUAGE plpgsql;


SELECT transactions.create_routine('REF-PORCIV', 'transactions.post_recurring_invoices', 200);

--SELECT  * FROM transactions.post_recurring_invoices(2);
--SELECT to_char(to_timestamp (now()::text, 'DD'), 'MON')
