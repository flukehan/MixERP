

/*******************************************************************
	SAMPLE DATA FEED
	TODO: REMOVE THE BELOW BEFORE RELEASE
*******************************************************************/

INSERT INTO office.offices(office_code,office_name,nick_name,registration_date, street,city,state,country,zip_code,phone,fax,email,url,registration_number,pan_number,currency_code)
SELECT 'PES','Planet Earth Solutions', 'PES Technologies', '06/06/1989', 'Brooklyn','NY','','US','','','','info@mixof.org','http://mixof.org','0','0','NPR';


INSERT INTO office.offices(office_code,office_name,nick_name, registration_date, street,city,state,country,zip_code,phone,fax,email,url,registration_number,pan_number,currency_code,parent_office_id)
SELECT 'PES-NY-BK','Brooklyn Branch', 'PES Brooklyn', '06/06/1989', 'Brooklyn','NY','12345555','','','','','info@mixof.org','http://mixof.org','0','0','NPR',(SELECT office_id FROM office.offices WHERE office_code='PES');

INSERT INTO office.offices(office_code,office_name,nick_name, registration_date, street,city,state,country,zip_code,phone,fax,email,url,registration_number,pan_number,currency_code,parent_office_id)
SELECT 'PES-NY-MEM','Memphis Branch', 'PES Memphis', '06/06/1989', 'Memphis', 'NY','','','','64464554','','info@mixof.org','http://mixof.org','0','0','NPR',(SELECT office_id FROM office.offices WHERE office_code='PES');


INSERT INTO office.departments(department_code, department_name)
SELECT 'SAL', 'Sales & Billing' UNION ALL
SELECT 'MKT', 'Marketing & Promotion' UNION ALL
SELECT 'SUP', 'Support' UNION ALL
SELECT 'CC', 'Customer Care';

INSERT INTO office.roles(role_code,role_name, is_system)
SELECT 'SYST', 'System', true;

INSERT INTO office.roles(role_code,role_name, is_admin)
SELECT 'ADMN', 'Administrators', true;

INSERT INTO office.roles(role_code,role_name)
SELECT 'USER', 'Users' UNION ALL
SELECT 'EXEC', 'Executive' UNION ALL
SELECT 'MNGR', 'Manager' UNION ALL
SELECT 'SALE', 'Sales' UNION ALL
SELECT 'MARK', 'Marketing' UNION ALL
SELECT 'LEGL', 'Legal & Compliance' UNION ALL
SELECT 'FINC', 'Finance' UNION ALL
SELECT 'HUMR', 'Human Resources' UNION ALL
SELECT 'INFO', 'Information Technology' UNION ALL
SELECT 'CUST', 'Customer Service';


SELECT office.create_user((SELECT role_id FROM office.roles WHERE role_code='SYST'),(SELECT office_id FROM office.offices WHERE office_code='PES'),'sys','','System');

/*******************************************************************
	TODO: REMOVE THIS USER ON DEPLOYMENT
*******************************************************************/
SELECT office.create_user((SELECT role_id FROM office.roles WHERE role_code='ADMN'),(SELECT office_id FROM office.offices WHERE office_code='PES'),'binod','+qJ9AMyGgrX/AOF4GmwmBa4SrA3+InlErVkJYmAopVZh+WFJD7k2ZO9dxox6XiqT38dSoM72jLoXNzwvY7JAQA==','Binod Nepal');


