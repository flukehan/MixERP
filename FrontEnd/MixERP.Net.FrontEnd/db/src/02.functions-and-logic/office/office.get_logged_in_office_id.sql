CREATE FUNCTION office.get_logged_in_office_id(_user_id integer)
RETURNS integer
AS
$$
BEGIN
    RETURN
    (
        SELECT office_id
        FROM audit.logins
        WHERE user_id=$1
        AND login_date_time = 
        (
            SELECT MAX(login_date_time)
            FROM audit.logins
            WHERE user_id=$1
        )
    );
END
$$
LANGUAGE plpgsql;
