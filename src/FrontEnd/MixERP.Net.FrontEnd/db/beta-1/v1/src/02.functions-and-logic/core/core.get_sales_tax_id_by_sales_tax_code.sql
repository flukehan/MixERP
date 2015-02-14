DROP FUNCTION IF EXISTS core.get_sales_tax_id_by_sales_tax_code(_sales_tax_code national character varying(24));


CREATE FUNCTION core.get_sales_tax_id_by_sales_tax_code(_sales_tax_code national character varying(24))
RETURNS integer
AS
$$
BEGIN
    RETURN
        sales_tax_id
    FROM
        core.sales_taxes
    WHERE
        sales_tax_code=$1;
END
$$
LANGUAGE plpgsql;