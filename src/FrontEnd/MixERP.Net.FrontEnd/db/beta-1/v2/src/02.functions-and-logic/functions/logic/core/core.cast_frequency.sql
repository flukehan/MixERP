DROP FUNCTION IF EXISTS core.cast_frequency(_frequency_code text) CASCADE;

CREATE FUNCTION core.cast_frequency(_frequency_code text)
RETURNS integer
IMMUTABLE
AS
$$
BEGIN
    IF(UPPER($1) = 'EOM') THEN
        RETURN 2;
    END IF;
    IF(UPPER($1) = 'EOQ') THEN
        RETURN 3;
    END IF;
    IF(UPPER($1) = 'EOH') THEN
        RETURN 4;
    END IF;
    IF(UPPER($1) = 'EOY') THEN
        RETURN 5;
    END IF;

    RETURN NULL;
END
$$
LANGUAGE plpgsql;

CREATE CAST (text as integer)
WITH FUNCTION core.cast_frequency(text) AS ASSIGNMENT;


