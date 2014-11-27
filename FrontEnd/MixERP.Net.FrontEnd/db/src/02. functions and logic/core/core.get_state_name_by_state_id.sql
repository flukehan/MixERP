CREATE FUNCTION core.get_state_name_by_state_id(integer)
RETURNS text
AS
$$
BEGIN
    RETURN
        state_name
    FROM
        core.states
    WHERE
        state_id = $1;
END
$$
LANGUAGE plpgsql;