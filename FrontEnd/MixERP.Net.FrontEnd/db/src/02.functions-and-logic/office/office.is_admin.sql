CREATE FUNCTION office.is_admin(integer)
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

ALTER TABLE office.users
ADD CONSTRAINT users_elevated_chk
CHECK
(
    (NOT office.is_admin(user_id) AND NOT elevated)
    OR
    (office.is_admin(user_id))
);