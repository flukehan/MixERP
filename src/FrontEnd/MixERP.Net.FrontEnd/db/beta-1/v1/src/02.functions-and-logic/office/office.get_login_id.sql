DROP FUNCTION IF EXISTS office.get_login_id(_user_id integer);

CREATE FUNCTION office.get_login_id(_user_id integer)
RETURNS bigint
AS
$$
BEGIN
    RETURN
    (
        SELECT login_id
        FROM audit.logins
        WHERE user_id=$1
        AND login_date_time = 
        (
            SELECT MAX(login_date_time)
            FROM audit.logins
            WHERE user_id=$1
        )
        LIMIT 1
    );
END
$$
LANGUAGE plpgsql;
