CREATE FUNCTION core.get_country_name_by_country_id(integer)
RETURNS text
STABLE
AS
$$
BEGIN
    RETURN
    country_name
    FROM core.countries
    WHERE country_id=$1;
END
$$
LANGUAGE plpgsql;

