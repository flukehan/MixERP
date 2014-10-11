CREATE FUNCTION office.get_user_name_by_user_id(user_id integer)
RETURNS text
AS
$$
BEGIN
    RETURN
    (
        SELECT office.users.user_name FROM office.users
        WHERE office.users.user_id=$1
    );
END
$$
LANGUAGE plpgsql;
