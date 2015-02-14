DROP FUNCTION IF EXISTS office.get_role_id_by_role_name(text);

CREATE FUNCTION office.get_role_id_by_role_name(text)
RETURNS integer
AS
$$
BEGIN
        RETURN
        role_id
        FROM office.roles
        WHERE role_name=$1;
END
$$
LANGUAGE plpgsql;
