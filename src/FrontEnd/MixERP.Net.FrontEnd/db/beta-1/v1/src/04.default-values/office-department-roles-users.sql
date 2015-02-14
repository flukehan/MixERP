DO
$$
BEGIN
    IF(core.get_locale() = 'en-US') THEN
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
    END IF;
END
$$
LANGUAGE plpgsql;        