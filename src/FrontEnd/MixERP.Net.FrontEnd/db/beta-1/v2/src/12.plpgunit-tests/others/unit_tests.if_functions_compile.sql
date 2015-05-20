DROP FUNCTION IF EXISTS unit_tests.if_functions_compile();

CREATE FUNCTION unit_tests.if_functions_compile()
RETURNS public.test_result
AS
$$
    DECLARE schemas text[];
    DECLARE message public.test_result;
    DECLARE result  boolean;
BEGIN

    schemas := ARRAY(
                SELECT nspname::text
                FROM pg_namespace
                WHERE nspname NOT LIKE 'pg%'
                AND nspname NOT IN('assert', 'unit_tests', 'information_schema')
                ORDER BY nspname
                );


    SELECT * FROM assert.if_functions_compile(VARIADIC schemas) INTO message, result;
    
    IF(result=false) THEN
        RETURN message;
    END IF;

    SELECT assert.ok('End of test.') INTO message;  
    RETURN message; 
END
$$
LANGUAGE plpgsql;
