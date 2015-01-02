CREATE VIEW office.role_scrud_view
AS
SELECT 
  roles.role_id, 
  roles.role_code, 
  roles.role_name, 
  roles.is_admin, 
  roles.is_system
FROM 
  office.roles;
