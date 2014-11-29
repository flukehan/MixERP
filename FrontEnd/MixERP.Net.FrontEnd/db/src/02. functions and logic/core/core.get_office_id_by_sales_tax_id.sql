DROP FUNCTION IF EXISTS core.get_office_id_by_sales_tax_id(_sales_tax_id integer);

CREATE FUNCTION core.get_office_id_by_sales_tax_id(_sales_tax_id integer)
RETURNS integer
AS
$$
BEGIN
    RETURN office_id
    FROM core.sales_taxes
    WHERE core.sales_taxes.sales_tax_id=$1;
END
$$
LANGUAGE plpgsql;

ALTER TABLE office.stores
ADD CONSTRAINT stores_sales_tax_id_chk
CHECK(core.get_office_id_by_sales_tax_id(sales_tax_id) = office_id);