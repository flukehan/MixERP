DROP FUNCTION IF EXISTS core.get_brand_id_by_brand_code(text);

CREATE FUNCTION core.get_brand_id_by_brand_code(text)
RETURNS integer
STABLE
AS
$$
BEGIN
        RETURN brand_id
        FROM core.brands
        WHERE brand_code=$1;
END
$$
LANGUAGE plpgsql;

--SELECT * FROM core.get_brand_id_by_brand_code('DEF');