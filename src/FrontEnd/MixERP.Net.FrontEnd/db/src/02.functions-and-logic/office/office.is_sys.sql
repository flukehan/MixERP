CREATE FUNCTION office.is_sys(integer)
RETURNS boolean
AS
$$
BEGIN
    RETURN
    (
        SELECT office.roles.is_system FROM office.users
        INNER JOIN office.roles
        ON office.users.role_id = office.roles.role_id
        WHERE office.users.user_id=$1
    );
END
$$
LANGUAGE PLPGSQL;



