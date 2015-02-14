CREATE VIEW office.department_scrud_view
AS
SELECT 
	departments.department_id,
	departments.department_code,
	departments.department_name
FROM office.departments;
