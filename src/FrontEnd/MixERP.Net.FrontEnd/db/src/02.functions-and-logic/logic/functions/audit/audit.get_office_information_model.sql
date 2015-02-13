DROP FUNCTION IF EXISTS audit.get_office_information_model(integer);

CREATE FUNCTION audit.get_office_information_model(integer)
RETURNS TABLE
(
    office              text,
    logged_in_to        text,
    last_login_ip       text,
    last_login_on       TIMESTAMP WITH TIME ZONE,
    current_ip          text,
    current_login_on    TIMESTAMP WITH TIME ZONE,
    role                text,
    department          text
)
VOLATILE
AS
$$
BEGIN
    CREATE TEMPORARY TABLE temp_model
    (
        office              text,
        logged_in_to        text,
        last_login_ip       text,
        last_login_on       TIMESTAMP WITH TIME ZONE,
        current_ip          text,
        current_login_on    TIMESTAMP WITH TIME ZONE,
        role                text,
        department          text
    ) ON COMMIT DROP;


    INSERT INTO temp_model(office, role, department)
    SELECT 
        office.offices.office_code || ' (' || office.offices.office_name || ')',
        office.roles.role_code || ' (' || office.roles.role_name || ')',
        office.departments.department_code || ' (' || office.departments.department_name
    FROM office.users
    INNER JOIN office.offices
    ON office.users.office_id = office.users.office_id
    INNER JOIN office.roles
    ON office.users.role_id = office.roles.role_id
    INNER JOIN office.departments
    ON office.users.department_id = office.departments.department_id
    WHERE office.users.user_id = $1;

    WITH login_info
    AS
    (
        SELECT 
            office.offices.office_code || ' (' || office.offices.office_name || ')' AS logged_in_to,
            ip_address AS current_ip,
            login_date_time AS current_login_on
        FROM audit.logins
        INNER JOIN office.offices
        ON audit.logins.office_id = office.offices.office_id
        WHERE user_id = $1
        AND login_date_time = 
        (
            SELECT max(login_date_time)
            FROM audit.logins
            WHERE user_id = $1
        )
    )

    UPDATE temp_model
    SET 
        logged_in_to        = login_info.logged_in_to,
        current_ip          = login_info.current_ip,
        current_login_on    = login_info.current_login_on
    FROM login_info;


    WITH last_login_info
    AS
    (
        SELECT 
            ip_address          AS last_login_ip,
            login_date_time     AS last_login_on
        FROM audit.logins
        WHERE user_id = $1
        AND login_date_time < 
        (
            SELECT max(login_date_time)
            FROM audit.logins
            WHERE user_id = $1
        )
        ORDER BY login_date_time DESC
        LIMIT 1
    )
    UPDATE temp_model
    SET 
        last_login_ip       = last_login_info.last_login_ip,
        last_login_on       = last_login_info.last_login_on
    FROM last_login_info;
    
    
    RETURN QUERY
    SELECT * FROM temp_model;
END
$$
LANGUAGE plpgsql;


