CREATE VIEW office.sign_in_view
AS
SELECT 
  logins.login_id, 
  logins.user_id, 
  users.role_id, 
  roles.role_code || ' (' || roles.role_name || ')' AS role, 
  roles.role_code, 
  roles.role_name, 
  roles.is_admin, 
  roles.is_system, 
  logins.browser, 
  logins.ip_address, 
  logins.login_date_time, 
  logins.remote_user, 
  logins.culture, 
  users.user_name, 
  users.full_name, 
  users.elevated, 
  offices.office_code || ' (' || offices.office_name || ')' AS office,
  offices.office_id, 
  offices.office_code, 
  offices.office_name, 
  offices.nick_name, 
  offices.registration_date, 
  offices.currency_code, 
  offices.po_box, 
  offices.address_line_1, 
  offices.address_line_2, 
  offices.street, 
  offices.city, 
  offices.state, 
  offices.zip_code, 
  offices.country, 
  offices.phone, 
  offices.fax, 
  offices.email, 
  offices.url, 
  offices.registration_number, 
  offices.pan_number,
  offices.allow_transaction_posting
FROM 
  audit.logins, 
  office.users, 
  office.offices, 
  office.roles
WHERE 
  logins.user_id = users.user_id AND
  logins.office_id = offices.office_id AND
  users.role_id = roles.role_id;