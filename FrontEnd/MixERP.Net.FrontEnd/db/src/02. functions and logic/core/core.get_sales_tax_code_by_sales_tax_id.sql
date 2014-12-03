DROP FUNCTION IF EXISTS core.get_sales_tax_code_by_sales_tax_id(_sales_tax_id integer);


CREATE FUNCTION core.get_sales_tax_code_by_sales_tax_id(_sales_tax_id integer)
RETURNS national character varying(24)
AS
$$
BEGIN
    RETURN
        sales_tax_code
    FROM
        core.sales_taxes
    WHERE
        sales_tax_id=$1;
END
$$
LANGUAGE plpgsql;
