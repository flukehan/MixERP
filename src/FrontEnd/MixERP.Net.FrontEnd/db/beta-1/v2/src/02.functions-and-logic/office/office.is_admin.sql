CREATE OR REPLACE FUNCTION office.is_admin(_user_id integer)
RETURNS boolean
AS
$$
BEGIN
    RETURN
    (
        SELECT office.roles.is_admin FROM office.users
        INNER JOIN office.roles
        ON office.users.role_id = office.roles.role_id
        WHERE office.users.user_id=$1
    );
END
$$
LANGUAGE PLPGSQL;