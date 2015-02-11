CREATE FUNCTION office.get_office_id_by_user_id(user_id integer_strict)
RETURNS integer
AS
$$
BEGIN
    RETURN
    (
        SELECT office.users.office_id FROM office.users
        WHERE office.users.user_id=$1
    );
END
$$
LANGUAGE plpgsql;
