CREATE OR REPLACE FUNCTION office.is_sys_user(_user_id integer)
RETURNS boolean
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT * FROM office.users
        WHERE user_id=$1
        AND role_id IN
        (
            SELECT office.roles.role_id FROM office.roles WHERE office.roles.role_code='SYST'
        )
    ) THEN
        RETURN true;
    END IF;

    RETURN false;
END
$$
LANGUAGE plpgsql;

