DROP FUNCTION IF EXISTS core.get_county_id_by_county_name(text);

CREATE FUNCTION core.get_county_id_by_county_name(text)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN county_id
    FROM core.counties
    WHERE county_name = $1;
END
$$
LANGUAGE plpgsql;
