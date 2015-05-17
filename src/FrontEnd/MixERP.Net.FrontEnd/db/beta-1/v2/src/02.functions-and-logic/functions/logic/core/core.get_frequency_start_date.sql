DROP FUNCTION IF EXISTS core.get_frequency_start_date(_frequency_id integer, _value_date date);

CREATE FUNCTION core.get_frequency_start_date(_frequency_id integer, _value_date date)
RETURNS date
STABLE
AS
$$
    DECLARE _start_date date;
BEGIN
    SELECT MAX(value_date) 
    INTO _start_date
    FROM core.frequency_setups
    WHERE value_date < $2
    AND frequency_id = ANY(core.get_frequencies($1));

    IF(_start_date IS NULL AND $1 = 'EOY'::text::integer) THEN
        SELECT MAX(starts_from)
        INTO _start_date
        FROM core.fiscal_year
        WHERE starts_from < $2;
    END IF;

    RETURN _start_date;
END
$$
LANGUAGE plpgsql;

--SELECT * FROM core.get_frequency_start_date('eoy'::text::integer, '2015-05-14');
