DROP FUNCTION IF EXISTS core.get_recurrence_type_id_by_recurrence_type_code(text);

CREATE FUNCTION core.get_recurrence_type_id_by_recurrence_type_code(text)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN recurrence_type_id
    FROM core.recurrence_types
    WHERE recurrence_type_code = $1;
END
$$
LANGUAGE plpgsql;