DROP FUNCTION IF EXISTS audit.is_valid_login_id(bigint);

CREATE FUNCTION audit.is_valid_login_id(bigint)
RETURNS boolean
STABLE
AS
$$
BEGIN
    IF EXISTS(SELECT 1 FROM audit.logins WHERE login_id=$1) THEN
            RETURN true;
    END IF;

    RETURN false;
END
$$
LANGUAGE plpgsql;
