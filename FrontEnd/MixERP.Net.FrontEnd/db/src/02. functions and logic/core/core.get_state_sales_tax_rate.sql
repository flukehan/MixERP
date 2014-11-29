DROP FUNCTION IF EXISTS core.get_state_sales_tax_rate(_state_sales_tax_id integer);

CREATE FUNCTION core.get_state_sales_tax_rate(_state_sales_tax_id integer)
RETURNS decimal_strict2
AS
$$
BEGIN
    RETURN
        rate
    FROM core.state_sales_taxes
    WHERE state_sales_tax_id=$1;
END
$$
LANGUAGE plpgsql;
