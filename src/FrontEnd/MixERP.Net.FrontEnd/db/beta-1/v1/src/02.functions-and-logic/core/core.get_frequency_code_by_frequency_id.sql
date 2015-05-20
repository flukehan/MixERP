CREATE FUNCTION core.get_frequency_code_by_frequency_id(integer)
RETURNS text
STABLE
AS
$$
BEGIN
    RETURN frequency_code
    FROM core.frequencies
    WHERE core.frequencies.frequency_id=$1;
END
$$
LANGUAGE plpgsql;
