DROP FUNCTION IF EXISTS core.get_frequency_end_date(_frequency_id integer, _value_date date);

CREATE FUNCTION core.get_frequency_end_date(_frequency_id integer, _value_date date)
RETURNS date
STABLE
AS
$$
    DECLARE _end_date date;
BEGIN
    SELECT MIN(value_date)
    INTO _end_date
    FROM core.frequency_setups
    WHERE value_date > $2
    AND frequency_id = ANY(core.get_frequencies($1));

    RETURN _end_date;
END
$$
LANGUAGE plpgsql;


--SELECT * FROM core.get_frequency_end_date(2, transactions.get_value_date(2));