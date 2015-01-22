DROP FUNCTION IF EXISTS core.get_brand_id_by_brand_name(text);

CREATE FUNCTION core.get_brand_id_by_brand_name(text)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN brand_id
    FROM core.brands
    WHERE brand_name=$1;
END
$$
LANGUAGE plpgsql;

--SELECT * FROM core.get_brand_id_by_brand_name('DEF');