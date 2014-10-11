CREATE FUNCTION office.get_role_id_by_use_id(user_id integer_strict)
RETURNS integer
AS
$$
BEGIN
    RETURN
    (
        SELECT office.users.role_id FROM office.users
        WHERE office.users.user_id=$1
    );
END
$$
LANGUAGE plpgsql;

