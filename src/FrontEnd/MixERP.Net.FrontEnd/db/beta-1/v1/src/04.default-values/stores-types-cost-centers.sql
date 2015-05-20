DO
$$
BEGIN
    IF(core.get_locale() = 'en-US') THEN
        INSERT INTO office.store_types(store_type_code,store_type_name)
        SELECT 'GOD', 'Godown'                              UNION ALL
        SELECT 'SAL', 'Sales Center'                        UNION ALL
        SELECT 'WAR', 'Warehouse'                           UNION ALL
        SELECT 'PRO', 'Production';

        INSERT INTO office.cost_centers(cost_center_code, cost_center_name)
        SELECT 'DEF', 'Default'                             UNION ALL
        SELECT 'GEN', 'General Administration'              UNION ALL
        SELECT 'HUM', 'Human Resources'                     UNION ALL
        SELECT 'SCC', 'Support & Customer Care'             UNION ALL
        SELECT 'GAE', 'Guest Accomodation & Entertainment'  UNION ALL
        SELECT 'MKT', 'Marketing & Promotion'               UNION ALL
        SELECT 'SAL', 'Sales & Billing'                     UNION ALL
        SELECT 'FIN', 'Finance & Accounting';
    END IF;
END
$$
LANGUAGE plpgsql;