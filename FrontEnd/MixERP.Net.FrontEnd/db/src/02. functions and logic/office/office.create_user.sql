CREATE FUNCTION office.create_user
(
    role_id integer_strict,
    office_id integer_strict,
    user_name text,
    password text,
    full_name text
)
RETURNS VOID
AS
$$
BEGIN
    INSERT INTO office.users(role_id,office_id,user_name,password, full_name)
    SELECT $1, $2, $3, $4,$5;
    RETURN;
END
$$
LANGUAGE plpgsql;
