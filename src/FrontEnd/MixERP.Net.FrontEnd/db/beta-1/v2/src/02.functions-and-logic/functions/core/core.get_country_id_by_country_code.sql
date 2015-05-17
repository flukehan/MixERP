DROP FUNCTION IF EXISTS core.get_country_id_by_country_code(national character varying(12));

CREATE FUNCTION core.get_country_id_by_country_code(_country_code national character varying(12))
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN country_id
    FROM core.countries
    WHERE country_code = $1;
END
$$
LANGUAGE plpgsql;