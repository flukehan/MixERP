CREATE FUNCTION office.get_logged_in_culture(_user_id integer)
RETURNS text
AS
$$
BEGIN
    RETURN
    (
        SELECT culture
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
