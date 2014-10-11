CREATE VIEW office.sign_in_view
AS
SELECT
    users.user_id, 
    roles.role_code || ' (' || roles.role_name || ')' AS role, 
    roles.is_admin,
    roles.is_system,
    users.user_name, 
    users.full_name,
    office.get_login_id(office.users.user_id) AS login_id,
    office.get_logged_in_office_id(office.users.user_id) AS office_id,
    office.get_logged_in_culture(office.users.user_id) AS culture,
    logged_in_office.office_code || ' (' || logged_in_office.office_name || ')' AS office,
    logged_in_office.office_code,
    logged_in_office.office_name,
    logged_in_office.nick_name,
    logged_in_office.registration_date,
    logged_in_office.registration_number,
    logged_in_office.pan_number,
    logged_in_office.po_box,
    logged_in_office.address_line_1,
    logged_in_office.address_line_2,
    logged_in_office.street,
    logged_in_office.city,
    logged_in_office.state,
    logged_in_office.country,
    logged_in_office.zip_code,
    logged_in_office.phone,
    logged_in_office.fax,
    logged_in_office.email,
    logged_in_office.url
FROM 
    office.users
INNER JOIN
    office.roles
ON
    users.role_id = roles.role_id 
INNER JOIN
    office.offices
ON
    users.office_id = offices.office_id
LEFT JOIN
    office.offices AS logged_in_office
ON
    logged_in_office.office_id = office.get_logged_in_office_id(office.users.user_id);
