DROP FUNCTION IF EXISTS core.get_brand_name_by_brand_id(integer);

CREATE FUNCTION core.get_brand_name_by_brand_id(integer)
RETURNS text
STABLE
AS
$$
BEGIN
    RETURN brand_name
    FROM core.brands
    WHERE brand_id=$1;
END
$$
LANGUAGE plpgsql;

