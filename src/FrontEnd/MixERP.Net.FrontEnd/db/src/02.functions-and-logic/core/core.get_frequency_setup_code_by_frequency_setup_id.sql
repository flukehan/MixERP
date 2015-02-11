DROP FUNCTION IF EXISTS core.get_frequency_setup_code_by_frequency_setup_id(_frequency_setup_id integer);

CREATE FUNCTION core.get_frequency_setup_code_by_frequency_setup_id(_frequency_setup_id integer)
RETURNS text
STABLE
AS
$$
BEGIN
    RETURN frequency_setup_code
    FROM core.frequency_setups
    WHERE frequency_setup_id = $1;
END
$$
LANGUAGE plpgsql;

