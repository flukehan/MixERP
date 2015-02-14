DROP FUNCTION IF EXISTS office.validate_login
(
    user_name       text,
    password        text,
    challenge       text
);

CREATE FUNCTION office.validate_login
(
    user_name       text,
    password        text,
    challenge       text
)
RETURNS boolean
AS
$$
BEGIN

    IF EXISTS
    (
        SELECT 1 FROM office.users 
        WHERE office.users.user_name=$1 
        AND encode(digest(office.users.password || challenge, 'sha512'), 'hex')=$2
        --The system user must never login.
        AND office.users.role_id != 
        (
            SELECT office.roles.role_id 
            FROM office.roles 
            WHERE office.roles.role_code='SYST'
        )
    ) THEN
        RETURN true;
    END IF;
    RETURN false;
END
$$
LANGUAGE plpgsql;
