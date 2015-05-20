DROP FUNCTION IF EXISTS core.get_recurring_amount_by_recurring_invoice_id(_recurring_invoice_id integer);

CREATE FUNCTION core.get_recurring_amount_by_recurring_invoice_id(_recurring_invoice_id integer)
RETURNS money_strict
AS
$$
BEGIN
    RETURN
        recurring_amount
    FROM
        core.recurring_invoices
    WHERE
        core.recurring_invoices.recurring_invoice_id=$1;
END
$$
LANGUAGE plpgsql;