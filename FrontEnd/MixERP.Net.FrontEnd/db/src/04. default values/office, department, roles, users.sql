

/*******************************************************************
    SAMPLE DATA FEED
    TODO: REMOVE THE BELOW BEFORE RELEASE
*******************************************************************/

INSERT INTO office.offices(office_code,office_name,nick_name,registration_date, street,city,state,country,zip_code,phone,fax,email,url,registration_number,pan_number,currency_code)
SELECT 'MoF','Mix Open Foundation', 'MoF', '06/06/1989', 'Brooklyn','NY','','US','','','','info@mixof.org','http://mixof.org','0','0','NPR';


INSERT INTO office.offices(office_code,office_name,nick_name, registration_date, street,city,state,country,zip_code,phone,fax,email,url,registration_number,pan_number,currency_code,parent_office_id)
SELECT 'MoF-NY-BK','Brooklyn Branch', 'MoF Brooklyn', '06/06/1989', 'Brooklyn','NY','12345555','US','','','','info@mixof.org','http://mixof.org','0','0','NPR',(SELECT office_id FROM office.offices WHERE office_code='MoF');

INSERT INTO office.offices(office_code,office_name,nick_name, registration_date, street,city,state,country,zip_code,phone,fax,email,url,registration_number,pan_number,currency_code,parent_office_id)
SELECT 'MoF-NY-RV','Rio Vista Branch', 'MoF Rio Vista', '06/06/1989', 'Rio Vista', 'CA','','US','','64464554','','info@mixof.org','http://mixof.org','0','0','NPR',(SELECT office_id FROM office.offices WHERE office_code='MoF');

INSERT INTO office.offices(office_code,office_name,nick_name, registration_date, street,city,state,country,zip_code,phone,fax,email,url,registration_number,pan_number,currency_code,parent_office_id)
SELECT 'MoF-NP-KTM','Kathmandu Branch', 'MoF Kathmandu', '06/06/1989', 'Baneshwor', 'Kathmandu','Bagmati','NP','','64464554','','info@mixof.org','http://mixof.org','0','0','NPR',(SELECT office_id FROM office.offices WHERE office_code='MoF');


INSERT INTO office.departments(department_code, department_name)
SELECT 'SAL', 'Sales & Billing'         UNION ALL
SELECT 'MKT', 'Marketing & Promotion'   UNION ALL
SELECT 'SUP', 'Support'                 UNION ALL
SELECT 'CC', 'Customer Care';

INSERT INTO office.roles(role_code,role_name, is_system)
SELECT 'SYST', 'System', true;

INSERT INTO office.roles(role_code,role_name, is_admin)
SELECT 'ADMN', 'Administrators', true;

INSERT INTO office.roles(role_code,role_name)
SELECT 'USER', 'Users'                  UNION ALL
SELECT 'EXEC', 'Executive'              UNION ALL
SELECT 'MNGR', 'Manager'                UNION ALL
SELECT 'SALE', 'Sales'                  UNION ALL
SELECT 'MARK', 'Marketing'              UNION ALL
SELECT 'LEGL', 'Legal & Compliance'     UNION ALL
SELECT 'FINC', 'Finance'                UNION ALL
SELECT 'HUMR', 'Human Resources'        UNION ALL
SELECT 'INFO', 'Information Technology' UNION ALL
SELECT 'CUST', 'Customer Service';


SELECT office.create_user((SELECT role_id FROM office.roles WHERE role_code='SYST'),(SELECT office_id FROM office.offices WHERE office_code='MoF'),'sys','','System');

/*******************************************************************
    TODO: REMOVE THESE USERS ON DEPLOYMENT
*******************************************************************/
SELECT office.create_user((SELECT role_id FROM office.roles WHERE role_code='ADMN'),(SELECT office_id FROM office.offices WHERE office_code='MoF'),'binod','37c6ca5a5570ce76affa5e779036c4955d764520980d17b597ea2908e9dcc515607f12eb25c3ce26e6b5dcaa812fe2acefbb20663ac220b02da82ec2f7e1d0e9','Binod', false);
SELECT office.create_user((SELECT role_id FROM office.roles WHERE role_code='USER'),(SELECT office_id FROM office.offices WHERE office_code='MoF'),'demo','339d611a358910b5b0fa62a9c7c7bf625a8e993a41539a59f9eedeb443e474584da6a545f518283b155c7bc0d42dd747f06606eb75aca8adf623bf0e1fe4a9f0','Demo User', false);
SELECT office.create_user((SELECT role_id FROM office.roles WHERE role_code='ADMN'),(SELECT office_id FROM office.offices WHERE office_code='MoF'),'nirvan','c75c521057da3ff26f6732c8b4b8710ed9aede9d7eb5a64b2a1bf9f42deef89f1e666ca21927ce1ccef5860764cf3690164432fde2c4a0db69260aaa20b47bcf','Nirvan', true);


UPDATE office.users SET can_change_password=false
WHERE user_name='binod';

