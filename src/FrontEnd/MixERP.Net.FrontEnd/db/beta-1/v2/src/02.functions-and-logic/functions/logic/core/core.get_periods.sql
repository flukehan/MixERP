DROP FUNCTION IF EXISTS core.get_periods
(
    _date_from                      date,
    _date_to                        date
);

CREATE FUNCTION core.get_periods
(
    _date_from                      date,
    _date_to                        date
)
RETURNS core.period[]
VOLATILE
AS
$$
BEGIN
    DROP TABLE IF EXISTS frequency_setups_temp;
    CREATE TEMPORARY TABLE frequency_setups_temp
    (
        frequency_setup_id      int,
        value_date              date
    ) ON COMMIT DROP;

    INSERT INTO frequency_setups_temp
    SELECT frequency_setup_id, value_date
    FROM core.frequency_setups
    WHERE value_date BETWEEN _date_from AND _date_to
    ORDER BY value_date;

    RETURN
        array_agg
        (
            (
                core.get_frequency_setup_code_by_frequency_setup_id(frequency_setup_id),
                core.get_frequency_setup_start_date_by_frequency_setup_id(frequency_setup_id),
                core.get_frequency_setup_end_date_by_frequency_setup_id(frequency_setup_id)
            )::core.period
        )::core.period[]
    FROM frequency_setups_temp;
END
$$
LANGUAGE plpgsql;

--SELECT * FROM core.get_periods('1-1-2000', '1-1-2020');