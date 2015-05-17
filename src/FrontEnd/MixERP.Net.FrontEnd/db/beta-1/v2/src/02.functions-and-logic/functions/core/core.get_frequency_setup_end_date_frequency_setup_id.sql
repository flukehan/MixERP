DROP FUNCTION IF EXISTS core.get_frequency_setup_end_date_frequency_setup_id(_frequency_setup_id integer);
DROP FUNCTION IF EXISTS core.get_frequency_setup_end_date_by_frequency_setup_id(_frequency_setup_id integer);
CREATE FUNCTION core.get_frequency_setup_end_date_by_frequency_setup_id(_frequency_setup_id integer)
RETURNS date
AS
$$
BEGIN
    RETURN
        value_date
    FROM
        core.frequency_setups
    WHERE
        frequency_setup_id = $1;
END
$$
LANGUAGE plpgsql;