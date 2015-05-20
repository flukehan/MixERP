CREATE FUNCTION office.get_user_id_by_user_name(user_name text)
RETURNS integer
AS
$$
BEGIN
    RETURN
    (
        SELECT office.users.user_id FROM office.users
        WHERE office.users.user_name=$1
    );
END
$$
LANGUAGE plpgsql;
