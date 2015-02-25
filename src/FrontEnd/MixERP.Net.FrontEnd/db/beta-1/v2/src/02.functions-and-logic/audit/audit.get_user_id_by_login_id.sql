DROP FUNCTION IF EXISTS audit.get_user_id_by_login_id(bigint);

CREATE FUNCTION audit.get_user_id_by_login_id(bigint)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN user_id
    FROM audit.logins
    WHERE login_id=$1;
END
$$
LANGUAGE plpgsql;