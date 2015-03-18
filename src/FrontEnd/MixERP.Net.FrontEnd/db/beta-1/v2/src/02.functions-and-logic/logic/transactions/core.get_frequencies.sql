DROP FUNCTION IF EXISTS core.get_frequencies(_frequency_id integer);

CREATE FUNCTION core.get_frequencies(_frequency_id integer)
RETURNS integer[]
IMMUTABLE
AS
$$
    DECLARE _frequencies integer[];
BEGIN
    IF(_frequency_id = 2) THEN--End of month
        --End of month
        --End of quarter is also end of third/ninth month
        --End of half is also end of sixth month
        --End of year is also end of twelfth month
        _frequencies = ARRAY[2, 3, 4, 5];
    ELSIF(_frequency_id = 3) THEN--End of quarter
        --End of quarter
        --End of half is the second end of quarter
        --End of year is the fourth/last end of quarter
        _frequencies = ARRAY[3, 4, 5];
    ELSIF(_frequency_id = 4) THEN--End of half
        --End of half
        --End of year is the second end of half
        _frequencies = ARRAY[4, 5];
    ELSIF(_frequency_id = 5) THEN--End of year
        --End of year
        _frequencies = ARRAY[5];
    END IF;

    RETURN _frequencies;
END
$$
LANGUAGE plpgsql;

--SELECT * FROM core.get_frequencies(3);