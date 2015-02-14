DROP FUNCTION IF EXISTS core.get_locale();

CREATE FUNCTION core.get_locale()
RETURNS text
AS
$$
BEGIN
    RETURN 'en-US';
END
$$
LANGUAGE plpgsql;