DROP FUNCTION IF EXISTS transactions.create_recurring_invoices(bigint);

CREATE FUNCTION transactions.create_recurring_invoices(bigint)
RETURNS void
VOLATILE
AS
$$
    DECLARE _party_id       bigint;
BEGIN
    IF NOT EXISTS
    (
        SELECT * FROM transactions.transaction_master WHERE book IN('Sales.Direct', 'Sales.Delivery')
        AND transaction_master_id=$1
        AND verification_status_id > 0
    ) THEN
        RETURN;
    END IF;

    SELECT party_id INTO _party_id 
    FROM transactions.stock_master
    WHERE transaction_master_id = $1;

    IF(COALESCE(_party_id, 0) = 0) THEN
        RETURN;
    END IF;
    
    DROP TABLE IF EXISTS recurring_invoices_temp;

    CREATE TEMPORARY TABLE recurring_invoices_temp
    (
        recurring_invoice_id            integer,
        party_id                        bigint,
        total_duration                  integer,
        starts_from                     date,
        ends_on                         date,
        recurrence_type_id              integer,
        recurring_frequency_id          integer,
        recurring_duration              integer,
        recurs_on_same_calendar_date    boolean,
        recurring_amount                public.money_strict,
        account_id                      bigint,
        payment_term_id                 integer,
        is_active                       boolean DEFAULT(true),
        statement_reference             national character varying(100)
    ) ON COMMIT DROP;

    INSERT INTO recurring_invoices_temp
    (
        recurring_invoice_id,
        total_duration,
        recurrence_type_id,
        recurring_frequency_id,
        recurring_duration,
        recurs_on_same_calendar_date,
        recurring_amount,
        account_id,
        payment_term_id,
        is_active,
        statement_reference
    )
    SELECT
        recurring_invoice_id,
        total_duration,
        recurrence_type_id,
        recurring_frequency_id,
        recurring_duration,
        recurs_on_same_calendar_date,
        recurring_amount,
        account_id,
        payment_term_id,
        is_active,
        statement_reference
    FROM core.recurring_invoices
    WHERE is_active
    AND auto_trigger_on_sales
    AND item_id
    IN
    (
        SELECT item_id FROM transactions.stock_details
        INNER JOIN transactions.stock_master
        ON transactions.stock_master.stock_master_id = transactions.stock_details.stock_master_id
        WHERE 1 = 1
        AND transactions.stock_master.transaction_master_id = $1
        AND tran_type='Cr'
    );

    UPDATE recurring_invoices_temp
    SET 
        party_id        = _party_id, 
        starts_from     = now()::date,
        ends_on         = now()::date + total_duration;

    INSERT INTO core.recurring_invoice_setup
    (
        recurring_invoice_id,
        party_id,
        starts_from,
        ends_on,
        recurrence_type_id,
        recurring_frequency_id,
        recurring_duration,
        recurs_on_same_calendar_date,
        recurring_amount,
        account_id,
        payment_term_id,
        is_active,
        statement_reference
    )
    SELECT
        recurring_invoice_id,
        party_id,
        starts_from,
        ends_on,
        recurrence_type_id,
        recurring_frequency_id,
        recurring_duration,
        recurs_on_same_calendar_date,
        recurring_amount,
        account_id,
        payment_term_id,
        is_active,
        statement_reference
    FROM recurring_invoices_temp;
END
$$
LANGUAGE plpgsql;