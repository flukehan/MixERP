DROP FUNCTION IF EXISTS office.get_role_id_by_role_code(text);

CREATE FUNCTION office.get_role_id_by_role_code(text)
RETURNS integer
AS
$$
BEGIN
        RETURN
        role_id
        FROM office.roles
        WHERE role_code=$1;
END
$$
LANGUAGE plpgsql;
