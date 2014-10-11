CREATE FUNCTION office.get_sys_user_id()
RETURNS integer
AS
$$
BEGIN
    RETURN
    (
        SELECT office.users.user_id 
        FROM office.roles, office.users
        WHERE office.roles.role_id = office.users.role_id
        AND office.roles.is_system=true LIMIT 1
    );
END
$$
LANGUAGE plpgsql;
