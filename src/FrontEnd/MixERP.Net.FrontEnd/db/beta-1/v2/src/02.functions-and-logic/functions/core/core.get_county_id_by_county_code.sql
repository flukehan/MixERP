DROP FUNCTION IF EXISTS core.get_county_id_by_county_code(national character varying(12));

CREATE FUNCTION core.get_county_id_by_county_code(_county_code national character varying(12))
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN county_id
    FROM core.counties
    WHERE county_code = $1;
END
$$
LANGUAGE plpgsql;