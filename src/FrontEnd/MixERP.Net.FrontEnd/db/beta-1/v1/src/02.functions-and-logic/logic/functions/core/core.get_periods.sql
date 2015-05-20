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
AS
$$
BEGIN
    RETURN 
        array_agg
        (
            (
                core.get_frequency_setup_code_by_frequency_setup_id(frequency_setup_id),
                core.get_frequency_setup_start_date_frequency_setup_id(frequency_setup_id),
                core.get_frequency_setup_end_date_frequency_setup_id(frequency_setup_id)
            )::core.period
        )::core.period[]
    FROM core.frequency_setups
    WHERE value_date BETWEEN _date_from AND _date_to;
END
$$
LANGUAGE plpgsql;

