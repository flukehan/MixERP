CREATE OR REPLACE VIEW office.role_view
AS
SELECT 
  roles.role_id, 
  roles.role_code, 
  roles.role_name
FROM 
  office.roles;
   