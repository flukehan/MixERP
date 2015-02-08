CREATE FUNCTION core.get_state_id_by_state_code(national character varying(12))
RETURNS integer
AS
$$
BEGIN
    RETURN
        state_id
    FROM
        core.states
    WHERE
        state_code = $1;
END
$$
LANGUAGE plpgsql;