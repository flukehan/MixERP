-- DO
-- $$
        -- DECLARE sql text;
-- BEGIN
        -- SELECT array_to_string(
        -- ARRAY(
        -- SELECT 
        -- 'SELECT audit.audit_table(''' || table_schema || '.' || table_name || '''::regclass, true, true, null);'
        -- FROM information_schema.tables
        -- WHERE table_schema NOT IN('pg_catalog', 'information_schema', 'unit_tests')
        -- AND table_name NOT IN('logged_actions')
        -- AND table_type='BASE TABLE'
        -- ORDER BY table_schema), '')
        -- INTO sql;

        -- EXECUTE sql;
        
-- END
-- $$
-- LANGUAGE plpgsql;