DROP FUNCTION IF EXISTS core.get_state_id_by_state_name(text);

CREATE FUNCTION core.get_state_id_by_state_name(text)
RETURNS integer
AS
$$
BEGIN
    RETURN state_id
    FROM core.states
    WHERE state_name = $1;
END
$$
LANGUAGE plpgsql;
