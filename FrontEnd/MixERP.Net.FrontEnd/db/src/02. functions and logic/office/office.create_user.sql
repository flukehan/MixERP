CREATE FUNCTION office.create_user
(
    role_id integer_strict,
    office_id integer_strict,
    user_name text,
    password text,
    full_name text,
    elevated boolean = false
)
RETURNS VOID
AS
$$
BEGIN
    INSERT INTO office.users(role_id,office_id,user_name,password, full_name, elevated)
    SELECT $1, $2, $3, $4,$5, $6;
    RETURN;
END
$$
LANGUAGE plpgsql;
