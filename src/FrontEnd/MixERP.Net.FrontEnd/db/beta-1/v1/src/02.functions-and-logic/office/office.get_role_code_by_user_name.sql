CREATE FUNCTION office.get_role_code_by_user_name(user_name text)
RETURNS text
AS
$$
BEGIN
    RETURN
    (
        SELECT office.roles.role_code FROM office.roles, office.users
        WHERE office.roles.role_id=office.users.role_id
        AND office.users.user_name=$1
    );
END
$$
LANGUAGE plpgsql;
