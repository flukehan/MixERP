DROP FUNCTION IF EXISTS core.get_county_sales_tax_rate(_county_sales_tax_id integer);

CREATE FUNCTION core.get_county_sales_tax_rate(_county_sales_tax_id integer)
RETURNS decimal_strict2
STABLE
AS
$$
BEGIN
    RETURN rate
    FROM core.county_sales_taxes
    WHERE county_sales_tax_id=$1;
END
$$
LANGUAGE plpgsql;



