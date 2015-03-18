-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/00.db core/plpgunit/install/0.uninstall-unit-test.sql --<--<--

/********************************************************************************
The PostgreSQL License

Copyright (c) 2014, Binod Nepal, Mix Open Foundation (http://mixof.org).

Permission to use, copy, modify, and distribute this software and its documentation 
for any purpose, without fee, and without a written agreement is hereby granted, 
provided that the above copyright notice and this paragraph and 
the following two paragraphs appear in all copies.

IN NO EVENT SHALL MIX OPEN FOUNDATION BE LIABLE TO ANY PARTY FOR DIRECT, INDIRECT, 
SPECIAL, INCIDENTAL, OR CONSEQUENTIAL DAMAGES, INCLUDING LOST PROFITS, 
ARISING OUT OF THE USE OF THIS SOFTWARE AND ITS DOCUMENTATION, EVEN IF 
MIX OPEN FOUNDATION HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

MIX OPEN FOUNDATION SPECIFICALLY DISCLAIMS ANY WARRANTIES, INCLUDING, 
BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS 
FOR A PARTICULAR PURPOSE. THE SOFTWARE PROVIDED HEREUNDER IS ON AN "AS IS" BASIS, 
AND MIX OPEN FOUNDATION HAS NO OBLIGATIONS TO PROVIDE MAINTENANCE, SUPPORT, 
UPDATES, ENHANCEMENTS, OR MODIFICATIONS.
***********************************************************************************/

DROP SCHEMA IF EXISTS assert CASCADE;
DROP SCHEMA IF EXISTS unit_tests CASCADE;
DROP DOMAIN IF EXISTS public.test_result CASCADE;


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/00.db core/plpgunit/install/1.install-unit-test.sql --<--<--
/********************************************************************************
The PostgreSQL License

Copyright (c) 2014, Binod Nepal, Mix Open Foundation (http://mixof.org).

Permission to use, copy, modify, and distribute this software and its documentation 
for any purpose, without fee, and without a written agreement is hereby granted, 
provided that the above copyright notice and this paragraph and 
the following two paragraphs appear in all copies.

IN NO EVENT SHALL MIX OPEN FOUNDATION BE LIABLE TO ANY PARTY FOR DIRECT, INDIRECT, 
SPECIAL, INCIDENTAL, OR CONSEQUENTIAL DAMAGES, INCLUDING LOST PROFITS, 
ARISING OUT OF THE USE OF THIS SOFTWARE AND ITS DOCUMENTATION, EVEN IF 
MIX OPEN FOUNDATION HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

MIX OPEN FOUNDATION SPECIFICALLY DISCLAIMS ANY WARRANTIES, INCLUDING, 
BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS 
FOR A PARTICULAR PURPOSE. THE SOFTWARE PROVIDED HEREUNDER IS ON AN "AS IS" BASIS, 
AND MIX OPEN FOUNDATION HAS NO OBLIGATIONS TO PROVIDE MAINTENANCE, SUPPORT, 
UPDATES, ENHANCEMENTS, OR MODIFICATIONS.
***********************************************************************************/

CREATE SCHEMA IF NOT EXISTS assert;
CREATE SCHEMA IF NOT EXISTS unit_tests;

DO 
$$
BEGIN
    IF NOT EXISTS 
    (
        SELECT * FROM pg_type
        WHERE 
            typname ='test_result'
        AND 
            typnamespace = 
            (
                SELECT oid FROM pg_namespace 
                WHERE nspname ='public'
            )
    ) THEN
        CREATE DOMAIN public.test_result AS text;
    END IF;
END
$$
LANGUAGE plpgsql;


DROP TABLE IF EXISTS unit_tests.test_details CASCADE;
DROP TABLE IF EXISTS unit_tests.tests CASCADE;
CREATE TABLE unit_tests.tests
(
    test_id                                 SERIAL NOT NULL PRIMARY KEY,
    started_on                              TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT(CURRENT_TIMESTAMP AT TIME ZONE 'UTC'),
    completed_on                            TIMESTAMP WITHOUT TIME ZONE NULL,
    total_tests                             integer NULL DEFAULT(0),
    failed_tests                            integer NULL DEFAULT(0)
);

CREATE INDEX unit_tests_tests_started_on_inx
ON unit_tests.tests(started_on);

CREATE INDEX unit_tests_tests_completed_on_inx
ON unit_tests.tests(completed_on);

CREATE INDEX unit_tests_tests_failed_tests_inx
ON unit_tests.tests(failed_tests);

CREATE TABLE unit_tests.test_details
(
    id                                      BIGSERIAL NOT NULL PRIMARY KEY,
    test_id                                 integer NOT NULL REFERENCES unit_tests.tests(test_id),
    function_name                           text NOT NULL,
    message                                 text NOT NULL,
    ts                                      TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT(CURRENT_TIMESTAMP AT TIME ZONE 'UTC'),
    status                                  boolean NOT NULL
);

CREATE INDEX unit_tests_test_details_test_id_inx
ON unit_tests.test_details(test_id);

CREATE INDEX unit_tests_test_details_status_inx
ON unit_tests.test_details(status);


DROP FUNCTION IF EXISTS assert.fail(message text);
CREATE FUNCTION assert.fail(message text)
RETURNS text
AS
$$
BEGIN
    IF $1 IS NULL OR trim($1) = '' THEN
        message := 'NO REASON SPECIFIED';
    END IF;
    
    RAISE WARNING 'ASSERT FAILED : %', message;
    RETURN message;
END
$$
LANGUAGE plpgsql
IMMUTABLE STRICT;

DROP FUNCTION IF EXISTS assert.pass(message text);
CREATE FUNCTION assert.pass(message text)
RETURNS text
AS
$$
BEGIN
    RAISE NOTICE 'ASSERT PASSED : %', message;
    RETURN '';
END
$$
LANGUAGE plpgsql
IMMUTABLE STRICT;

DROP FUNCTION IF EXISTS assert.ok(message text);
CREATE FUNCTION assert.ok(message text)
RETURNS text
AS
$$
BEGIN
    RAISE NOTICE 'OK : %', message;
    RETURN '';
END
$$
LANGUAGE plpgsql
IMMUTABLE STRICT;

DROP FUNCTION IF EXISTS assert.is_equal(IN have anyelement, IN want anyelement, OUT message text, OUT result boolean);
CREATE FUNCTION assert.is_equal(IN have anyelement, IN want anyelement, OUT message text, OUT result boolean)
AS
$$
BEGIN
    IF($1 = $2) THEN
        message := 'Assert is equal.';
        PERFORM assert.ok(message);
        result := true;
        RETURN;
    END IF;

    message := E'ASSERT IS_EQUAL FAILED.\n\nHave -> ' || $1::text || E'\nWant -> ' || $2::text || E'\n';    
    PERFORM assert.fail(message);
    result := false;
    RETURN;
END
$$
LANGUAGE plpgsql
IMMUTABLE STRICT;


DROP FUNCTION IF EXISTS assert.are_equal(VARIADIC anyarray, OUT message text, OUT result boolean);
CREATE FUNCTION assert.are_equal(VARIADIC anyarray, OUT message text, OUT result boolean)
AS
$$
    DECLARE count integer=0;
BEGIN
    SELECT COUNT(DISTINCT $1[s.i]) INTO count
    FROM generate_series(array_lower($1,1), array_upper($1,1)) AS s(i)
    ORDER BY 1;

    IF count <> 1 THEN
        MESSAGE := 'ASSERT ARE_EQUAL FAILED.';  
        PERFORM assert.fail(MESSAGE);
        RESULT := FALSE;
        RETURN;
    END IF;

    message := 'Asserts are equal.';
    PERFORM assert.ok(message);
    result := true;
    RETURN;
END
$$
LANGUAGE plpgsql
IMMUTABLE STRICT;

DROP FUNCTION IF EXISTS assert.is_not_equal(IN already_have anyelement, IN dont_want anyelement, OUT message text, OUT result boolean);
CREATE FUNCTION assert.is_not_equal(IN already_have anyelement, IN dont_want anyelement, OUT message text, OUT result boolean)
AS
$$
BEGIN
    IF($1 != $2) THEN
        message := 'Assert is not equal.';
        PERFORM assert.ok(message);
        result := true;
        RETURN;
    END IF;
    
    message := E'ASSERT IS_NOT_EQUAL FAILED.\n\nAlready Have -> ' || $1::text || E'\nDon''t Want   -> ' || $2::text || E'\n';   
    PERFORM assert.fail(message);
    result := false;
    RETURN;
END
$$
LANGUAGE plpgsql
IMMUTABLE STRICT;

DROP FUNCTION IF EXISTS assert.are_not_equal(VARIADIC anyarray, OUT message text, OUT result boolean);
CREATE FUNCTION assert.are_not_equal(VARIADIC anyarray, OUT message text, OUT result boolean)
AS
$$
    DECLARE count integer=0;
BEGIN
    SELECT COUNT(DISTINCT $1[s.i]) INTO count
    FROM generate_series(array_lower($1,1), array_upper($1,1)) AS s(i)
    ORDER BY 1;

    IF count <> array_upper($1,1) THEN
        MESSAGE := 'ASSERT ARE_NOT_EQUAL FAILED.';  
        PERFORM assert.fail(MESSAGE);
        RESULT := FALSE;
        RETURN;
    END IF;

    message := 'Asserts are not equal.';
    PERFORM assert.ok(message);
    result := true;
    RETURN;
END
$$
LANGUAGE plpgsql
IMMUTABLE STRICT;

DROP FUNCTION IF EXISTS assert.is_null(IN anyelement, OUT message text, OUT result boolean);
CREATE FUNCTION assert.is_null(IN anyelement, OUT message text, OUT result boolean)
AS
$$
BEGIN
    IF($1 IS NULL) THEN
        message := 'Assert is NULL.';
        PERFORM assert.ok(message);
        result := true;
        RETURN;
    END IF;
    
    message := E'ASSERT IS_NULL FAILED. NULL value was expected.\n\n\n';    
    PERFORM assert.fail(message);
    result := false;
    RETURN;
END
$$
LANGUAGE plpgsql
IMMUTABLE STRICT;

DROP FUNCTION IF EXISTS assert.is_not_null(IN anyelement, OUT message text, OUT result boolean);
CREATE FUNCTION assert.is_not_null(IN anyelement, OUT message text, OUT result boolean)
AS
$$
BEGIN
    IF($1 IS NOT NULL) THEN
        message := 'Assert is not NULL.';
        PERFORM assert.ok(message);
        result := true;
        RETURN;
    END IF;
    
    message := E'ASSERT IS_NOT_NULL FAILED. The value is NULL.\n\n\n';  
    PERFORM assert.fail(message);
    result := false;
    RETURN;
END
$$
LANGUAGE plpgsql
IMMUTABLE STRICT;

DROP FUNCTION IF EXISTS assert.is_true(IN boolean, OUT message text, OUT result boolean);
CREATE FUNCTION assert.is_true(IN boolean, OUT message text, OUT result boolean)
AS
$$
BEGIN
    IF($1 = true) THEN
        message := 'Assert is true.';
        PERFORM assert.ok(message);
        result := true;
        RETURN;
    END IF;
    
    message := E'ASSERT IS_TRUE FAILED. A true condition was expected.\n\n\n';  
    PERFORM assert.fail(message);
    result := false;
    RETURN;
END
$$
LANGUAGE plpgsql
IMMUTABLE STRICT;

DROP FUNCTION IF EXISTS assert.is_false(IN boolean, OUT message text, OUT result boolean);
CREATE FUNCTION assert.is_false(IN boolean, OUT message text, OUT result boolean)
AS
$$
BEGIN
    IF($1 = false) THEN
        message := 'Assert is false.';
        PERFORM assert.ok(message);
        result := true;
        RETURN;
    END IF;
    
    message := E'ASSERT IS_FALSE FAILED. A false condition was expected.\n\n\n';    
    PERFORM assert.fail(message);
    result := false;
    RETURN;
END
$$
LANGUAGE plpgsql
IMMUTABLE STRICT;

DROP FUNCTION IF EXISTS assert.is_greater_than(IN x anyelement, IN y anyelement, OUT message text, OUT result boolean);
CREATE FUNCTION assert.is_greater_than(IN x anyelement, IN y anyelement, OUT message text, OUT result boolean)
AS
$$
BEGIN
    IF($1 > $2) THEN
        message := 'Assert greater than condition is satisfied.';
        PERFORM assert.ok(message);
        result := true;
        RETURN;
    END IF;
    
    message := E'ASSERT IS_GREATER_THAN FAILED.\n\n X : -> ' || $1::text || E'\n is not greater than Y:   -> ' || $2::text || E'\n';    
    PERFORM assert.fail(message);
    result := false;
    RETURN;
END
$$
LANGUAGE plpgsql
IMMUTABLE STRICT;

DROP FUNCTION IF EXISTS assert.is_less_than(IN x anyelement, IN y anyelement, OUT message text, OUT result boolean);
CREATE FUNCTION assert.is_less_than(IN x anyelement, IN y anyelement, OUT message text, OUT result boolean)
AS
$$
BEGIN
    IF($1 < $2) THEN
        message := 'Assert less than condition is satisfied.';
        PERFORM assert.ok(message);
        result := true;
        RETURN;
    END IF;
    
    message := E'ASSERT IS_LESS_THAN FAILED.\n\n X : -> ' || $1::text || E'\n is not  than Y:   -> ' || $2::text || E'\n';  
    PERFORM assert.fail(message);
    result := false;
    RETURN;
END
$$
LANGUAGE plpgsql
IMMUTABLE STRICT;

DROP FUNCTION IF EXISTS assert.function_exists(function_name text, OUT message text, OUT result boolean);
CREATE FUNCTION assert.function_exists(function_name text, OUT message text, OUT result boolean)
AS
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT  1
        FROM    pg_catalog.pg_namespace n
        JOIN    pg_catalog.pg_proc p
        ON      pronamespace = n.oid
        WHERE replace(nspname || '.' || proname || '(' || oidvectortypes(proargtypes) || ')', ' ' , '')::text=$1
    ) THEN
        message := 'The function % does not exist.', $1;
        PERFORM assert.fail(message);

        result := false;
        RETURN;
    END IF;

    message := 'OK. The function ' || $1 || ' exists.';
    PERFORM assert.ok(message);
    result := true;
    RETURN;
END
$$
LANGUAGE plpgsql;

DROP FUNCTION IF EXISTS assert.if_functions_compile(VARIADIC _schema_name text[], OUT message text, OUT result boolean);
CREATE OR REPLACE FUNCTION assert.if_functions_compile
(
    VARIADIC _schema_name text[],
    OUT message text, 
    OUT result boolean
)
AS
$$
    DECLARE all_parameters              text;
    DECLARE current_function            RECORD;
    DECLARE current_function_name       text;
    DECLARE current_type                text;
    DECLARE current_type_schema         text;
    DECLARE current_parameter           text;
    DECLARE functions_count             smallint := 0;
    DECLARE current_parameters_count    int;
    DECLARE i                           int;
    DECLARE command_text                text;
    DECLARE failed_functions            text;
BEGIN
    FOR current_function IN 
        SELECT proname, proargtypes, nspname 
        FROM pg_proc 
        INNER JOIN pg_namespace 
        ON pg_proc.pronamespace = pg_namespace.oid 
        WHERE pronamespace IN 
        (
            SELECT oid FROM pg_namespace 
            WHERE nspname = ANY($1) 
            AND nspname NOT IN
            (
                'assert', 'unit_tests', 'information_schema'
            ) 
            AND proname NOT IN('if_functions_compile')
        ) 
    LOOP
        current_parameters_count := array_upper(current_function.proargtypes, 1) + 1;

        i := 0;
        all_parameters := '';

        LOOP
        IF i < current_parameters_count THEN
            IF i > 0 THEN
                all_parameters := all_parameters || ', ';
            END IF;

            SELECT 
                nspname, typname 
            INTO 
                current_type_schema, current_type 
            FROM pg_type 
            INNER JOIN pg_namespace 
            ON pg_type.typnamespace = pg_namespace.oid
            WHERE pg_type.oid = current_function.proargtypes[i];

            IF(current_type IN('int4', 'int8', 'numeric', 'integer_strict', 'money_strict','decimal_strict', 'integer_strict2', 'money_strict2','decimal_strict2', 'money','decimal', 'numeric', 'bigint')) THEN
                current_parameter := '1::' || current_type_schema || '.' || current_type;
            ELSIF(substring(current_type, 1, 1) = '_') THEN
                current_parameter := 'NULL::' || current_type_schema || '.' || substring(current_type, 2, length(current_type)) || '[]';
            ELSIF(current_type in ('date')) THEN            
                current_parameter := '''1-1-2000''::' || current_type;
            ELSIF(current_type = 'bool') THEN
                current_parameter := 'false';            
            ELSE
                current_parameter := '''''::' || quote_ident(current_type_schema) || '.' || quote_ident(current_type);
            END IF;
            
            all_parameters = all_parameters || current_parameter;

            i := i + 1;
        ELSE
            EXIT;
        END IF;
    END LOOP;

    BEGIN
        current_function_name := quote_ident(current_function.nspname)  || '.' || quote_ident(current_function.proname);
        command_text := 'SELECT * FROM ' || current_function_name || '(' || all_parameters || ');';

        EXECUTE command_text;
        functions_count := functions_count + 1;

        EXCEPTION WHEN OTHERS THEN
            IF(failed_functions IS NULL) THEN 
                failed_functions := '';
            END IF;
            
            IF(SQLSTATE IN('42702', '42704')) THEN
                failed_functions := failed_functions || E'\n' || command_text || E'\n' || SQLERRM || E'\n';                
            END IF;
    END;


    END LOOP;

    IF(failed_functions != '') THEN
        message := E'The test if_functions_compile failed. The following functions failed to compile : \n\n' || failed_functions;
        result := false;
        PERFORM assert.fail(message);
        RETURN;
    END IF;
END;
$$
LANGUAGE plpgsql 
VOLATILE;

DROP FUNCTION IF EXISTS assert.if_views_compile(VARIADIC _schema_name text[], OUT message text, OUT result boolean);
CREATE FUNCTION assert.if_views_compile
(
    VARIADIC _schema_name text[],
    OUT message text, 
    OUT result boolean    
)
AS
$$

    DECLARE message                     test_result;
    DECLARE current_view                RECORD;
    DECLARE current_view_name           text;
    DECLARE command_text                text;
    DECLARE failed_views                text;
BEGIN
    FOR current_view IN 
        SELECT table_name, table_schema 
        FROM information_schema.views
        WHERE table_schema = ANY($1) 
    LOOP

    BEGIN
        current_view_name := quote_ident(current_view.table_schema)  || '.' || quote_ident(current_view.table_name);
        command_text := 'SELECT * FROM ' || current_view_name || ' LIMIT 1;';

        RAISE NOTICE '%', command_text;
        
        EXECUTE command_text;

        EXCEPTION WHEN OTHERS THEN
            IF(failed_views IS NULL) THEN 
                failed_views := '';
            END IF;

            failed_views := failed_views || E'\n' || command_text || E'\n' || SQLERRM || E'\n';                
    END;


    END LOOP;

    IF(failed_views != '') THEN
        message := E'The test if_views_compile failed. The following views failed to compile : \n\n' || failed_views;
        result := false;
        PERFORM assert.fail(message);
        RETURN;
    END IF;

    RETURN;
END;
$$
LANGUAGE plpgsql 
VOLATILE;


DROP FUNCTION IF EXISTS unit_tests.begin(verbosity integer, format text);
CREATE FUNCTION unit_tests.begin(verbosity integer DEFAULT 9, format text DEFAULT '')
RETURNS TABLE(message text, result character(1))
AS
$$
    DECLARE this                    record;
    DECLARE _function_name          text;
    DECLARE _sql                    text;
    DECLARE _message                text;
    DECLARE _result                 character(1);
    DECLARE _test_id                integer;
    DECLARE _status                 boolean;
    DECLARE _total_tests            integer                         = 0;
    DECLARE _failed_tests           integer                         = 0;
    DECLARE _list_of_failed_tests   text;
    DECLARE _started_from           TIMESTAMP WITHOUT TIME ZONE;
    DECLARE _completed_on           TIMESTAMP WITHOUT TIME ZONE;
    DECLARE _delta                  integer;
    DECLARE _ret_val                text                            = '';
    DECLARE _verbosity              text[]                          = 
                                    ARRAY['debug5', 'debug4', 'debug3', 'debug2', 'debug1', 'log', 'notice', 'warning', 'error', 'fatal', 'panic'];
BEGIN
    _started_from := clock_timestamp() AT TIME ZONE 'UTC';

    IF(format='teamcity') THEN
        RAISE INFO '##teamcity[testSuiteStarted name=''Plpgunit'' message=''Test started from : %'']', _started_from; 
    ELSE
        RAISE INFO 'Test started from : %', _started_from; 
    END IF;
    
    IF($1 > 11) THEN
        $1 := 9;
    END IF;
    
    EXECUTE 'SET CLIENT_MIN_MESSAGES TO ' || _verbosity[$1];
    RAISE WARNING 'CLIENT_MIN_MESSAGES set to : %' , _verbosity[$1];
    
    SELECT nextval('unit_tests.tests_test_id_seq') INTO _test_id;

    INSERT INTO unit_tests.tests(test_id)
    SELECT _test_id;

    FOR this IN
        SELECT 
            nspname AS ns_name,
            proname AS function_name
        FROM    pg_catalog.pg_namespace n
        JOIN    pg_catalog.pg_proc p
        ON      pronamespace = n.oid
        WHERE
            prorettype='test_result'::regtype::oid
        ORDER BY p.oid ASC
    LOOP
        BEGIN
            _status := false;
            _total_tests := _total_tests + 1;
            
            _function_name = this.ns_name|| '.' || this.function_name || '()';
            _sql := 'SELECT ' || _function_name || ';';
            
            RAISE NOTICE 'RUNNING TEST : %.', _function_name;

            IF(format='teamcity') THEN
                RAISE INFO '##teamcity[testStarted name=''%'' message=''%'']', _function_name, _started_from; 
            ELSE
                RAISE INFO 'Running test % : %', _function_name, _started_from; 
            END IF;
            
            EXECUTE _sql INTO _message;

            IF _message = '' THEN
                _status := true;

                IF(format='teamcity') THEN
                    RAISE INFO '##teamcity[testFinished name=''%'' message=''%'']', _function_name, clock_timestamp() AT TIME ZONE 'UTC'; 
                ELSE
                    RAISE INFO 'Passed % : %', _function_name, clock_timestamp() AT TIME ZONE 'UTC'; 
                END IF;
            ELSE
                IF(format='teamcity') THEN
                    RAISE INFO '##teamcity[testFailed name=''%'' message=''%'']', _function_name, _message; 
                    RAISE INFO '##teamcity[testFinished name=''%'' message=''%'']', _function_name, clock_timestamp() AT TIME ZONE 'UTC'; 
                ELSE
                    RAISE INFO 'Test failed % : %', _function_name, _message; 
                END IF;
            END IF;
            
            INSERT INTO unit_tests.test_details(test_id, function_name, message, status, ts)
            SELECT _test_id, _function_name, _message, _status, clock_timestamp();

            IF NOT _status THEN
                _failed_tests := _failed_tests + 1;         
                RAISE WARNING 'TEST % FAILED.', _function_name;
                RAISE WARNING 'REASON: %', _message;
            ELSE
                RAISE NOTICE 'TEST % COMPLETED WITHOUT ERRORS.', _function_name;
            END IF;

        EXCEPTION WHEN OTHERS THEN
            _message := 'ERR' || SQLSTATE || ': ' || SQLERRM;
            INSERT INTO unit_tests.test_details(test_id, function_name, message, status)
            SELECT _test_id, _function_name, _message, false;

            _failed_tests := _failed_tests + 1;         

            RAISE WARNING 'TEST % FAILED.', _function_name;
            RAISE WARNING 'REASON: %', _message;

            IF(format='teamcity') THEN
                RAISE INFO '##teamcity[testFailed name=''%'' message=''%'']', _function_name, _message; 
                RAISE INFO '##teamcity[testFinished name=''%'' message=''%'']', _function_name, clock_timestamp() AT TIME ZONE 'UTC'; 
            ELSE
                RAISE INFO 'Test failed % : %', _function_name, _message; 
            END IF;
        END;
    END LOOP;

    _completed_on := clock_timestamp() AT TIME ZONE 'UTC';
    _delta := extract(millisecond from _completed_on - _started_from)::integer;
    
    UPDATE unit_tests.tests
    SET total_tests = _total_tests, failed_tests = _failed_tests, completed_on = _completed_on
    WHERE test_id = _test_id;

    IF format='junit' THEN
        SELECT 
            '<?xml version="1.0" encoding="UTF-8"?>'||
            xmlelement
            (
                name testsuites,
                xmlelement
                (
                    name                    testsuite,
                    xmlattributes
                    (
                        'plpgunit'          AS name, 
                        t.total_tests       AS tests, 
                        t.failed_tests      AS failures, 
                        0                   AS errors, 
                        EXTRACT
                        (
                            EPOCH FROM t.completed_on - t.started_on
                        )                   AS time
                    ),
                    xmlagg
                    (
                        xmlelement
                        (
                            name testcase, 
                            xmlattributes
                            (
                                td.function_name
                                            AS name, 
                                EXTRACT
                                (
                                    EPOCH FROM td.ts - t.started_on
                                )           AS time
                            ),
                            CASE 
                                WHEN td.status=false 
                                THEN 
                                    xmlelement
                                    (
                                        name failure, 
                                        td.message
                                    ) 
                                END
                        )
                    )
                )
            ) INTO _ret_val
        FROM unit_tests.test_details td, unit_tests.tests t
        WHERE
            t.test_id=_test_id
        AND 
            td.test_id=t.test_id
        GROUP BY t.test_id;
    ELSE
        WITH failed_tests AS
        (
            SELECT row_number() OVER (ORDER BY id) AS id, 
                unit_tests.test_details.function_name,
                unit_tests.test_details.message
            FROM unit_tests.test_details 
            WHERE test_id = _test_id
            AND status= false
        )
        SELECT array_to_string(array_agg(f.id::text || '. ' || f.function_name || ' --> ' || f.message), E'\n') INTO _list_of_failed_tests 
        FROM failed_tests f;

        _ret_val := _ret_val ||  'Test completed on : ' || _completed_on::text || E' UTC. \nTotal test runtime: ' || _delta::text || E' ms.\n';
        _ret_val := _ret_val || E'\nTotal tests run : ' || COALESCE(_total_tests, '0')::text;
        _ret_val := _ret_val || E'.\nPassed tests    : ' || (COALESCE(_total_tests, '0') - COALESCE(_failed_tests, '0'))::text;
        _ret_val := _ret_val || E'.\nFailed tests    : ' || COALESCE(_failed_tests, '0')::text;
        _ret_val := _ret_val || E'.\n\nList of failed tests:\n' || '----------------------';
        _ret_val := _ret_val || E'\n' || COALESCE(_list_of_failed_tests, '<NULL>')::text;
        _ret_val := _ret_val || E'\n' || E'End of plpgunit test.\n\n';
    END IF;
    
    IF _failed_tests > 0 THEN
        _result := 'N';

        IF(format='teamcity') THEN
            RAISE INFO '##teamcity[testStarted name=''Result'']'; 
            RAISE INFO '##teamcity[testFailed name=''Result'' message=''%'']', REPLACE(_ret_val, E'\n', ' |n'); 
            RAISE INFO '##teamcity[testFinished name=''Result'']'; 
            RAISE INFO '##teamcity[testSuiteFinished name=''Plpgunit'' message=''%'']', REPLACE(_ret_val, E'\n', '|n'); 
        ELSE
            RAISE INFO '%', _ret_val;
        END IF;
    ELSE
        _result := 'Y';

        IF(format='teamcity') THEN
            RAISE INFO '##teamcity[testSuiteFinished name=''Plpgunit'' message=''%'']', REPLACE(_ret_val, E'\n', '|n'); 
        ELSE
            RAISE INFO '%', _ret_val;
        END IF;
    END IF;

    SET CLIENT_MIN_MESSAGES TO notice;
    
    RETURN QUERY SELECT _ret_val, _result;
END
$$
LANGUAGE plpgsql;

DROP FUNCTION IF EXISTS unit_tests.begin_junit(verbosity integer);
CREATE FUNCTION unit_tests.begin_junit(verbosity integer DEFAULT 9)
RETURNS TABLE(message text, result character(1))
AS
$$
BEGIN
    RETURN QUERY 
    SELECT * FROM unit_tests.begin($1, 'junit');
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/01.types-domains-tables-and-constraints/tables-and-constraints.sql --<--<--
DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1 
        FROM   pg_catalog.pg_class c
        JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
        WHERE  n.nspname = 'policy'
        AND    c.relname = 'http_actions'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE policy.http_actions
        (
            http_action_code                text NOT NULL PRIMARY KEY
        );

        CREATE UNIQUE INDEX policy_http_action_code_uix
        ON policy.http_actions(UPPER(http_action_code));    
    END IF;    
END
$$
LANGUAGE plpgsql;

DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1 
        FROM   pg_catalog.pg_class c
        JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
        WHERE  n.nspname = 'policy'
        AND    c.relname = 'api_access_policy'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE policy.api_access_policy
        (
            api_access_policy_id            BIGSERIAL NOT NULL PRIMARY KEY,
            user_id                         integer NOT NULL REFERENCES office.users(user_id),
            office_id                       integer NOT NULL REFERENCES office.offices(office_id),
            poco_type_name                  text NOT NULL,
            http_action_code                text NOT NULL REFERENCES policy.http_actions(http_action_code),
            valid_till                      date NOT NULL,
            audit_user_id                   integer NULL REFERENCES office.users(user_id),
            audit_ts                        TIMESTAMP WITH TIME ZONE NULL 
                                            DEFAULT(NOW())    
        );

        CREATE UNIQUE INDEX api_access_policy_uix
        ON policy.api_access_policy(user_id, poco_type_name, http_action_code, valid_till);
    
    END IF;    
END
$$
LANGUAGE plpgsql;

DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1 
        FROM   pg_catalog.pg_class c
        JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
        WHERE  n.nspname = 'core'
        AND    c.relname = 'recurrence_types'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE core.recurrence_types
        (
            recurrence_type_id              SERIAL NOT NULL PRIMARY KEY,
            recurrence_type_code            national character varying(12) NOT NULL,
            recurrence_type_name            national character varying(50) NOT NULL,
            is_frequency                    boolean NOT NULL,
            audit_user_id                   integer NULL REFERENCES office.users(user_id),
            audit_ts                        TIMESTAMP WITH TIME ZONE NULL 
                                            DEFAULT(NOW())            
        );
    END IF;    
END
$$
LANGUAGE plpgsql;




DROP TABLE IF EXISTS core.recurring_invoices_temp;
DROP TABLE IF EXISTS core.recurring_invoice_setup_temp;

CREATE TABLE core.recurring_invoices_temp
AS
SELECT * FROM core.recurring_invoices;

CREATE TABLE core.recurring_invoice_setup_temp
AS
SELECT * FROM core.recurring_invoice_setup;

DROP TABLE IF EXISTS core.recurring_invoices CASCADE;
DROP TABLE IF EXISTS core.recurring_invoice_setup CASCADE;

CREATE TABLE core.recurring_invoices
(
    recurring_invoice_id                        SERIAL NOT NULL PRIMARY KEY,
    recurring_invoice_code                      national character varying(12) NOT NULL,
    recurring_invoice_name                      national character varying(50) NOT NULL,
    item_id                                     integer NULL REFERENCES core.items(item_id),
    total_duration                              integer NOT NULL DEFAULT(365),
    recurrence_type_id                          integer REFERENCES core.recurrence_types(recurrence_type_id),
    recurring_frequency_id                      integer NOT NULL REFERENCES core.frequencies(frequency_id),
    recurring_duration                          public.integer_strict2 NOT NULL DEFAULT(30),
    recurs_on_same_calendar_date                boolean NOT NULL DEFAULT(true),
    recurring_amount                            public.money_strict NOT NULL,
    account_id                                  bigint REFERENCES core.accounts(account_id),
    payment_term_id                             integer NOT NULL REFERENCES core.payment_terms(payment_term_id),
    auto_trigger_on_sales                       boolean NOT NULL,
    is_active                                   boolean NOT NULL DEFAULT(true),
    statement_reference                         national character varying(100) NOT NULL DEFAULT(''),
    audit_user_id                               integer NULL REFERENCES office.users(user_id),
    audit_ts                                    TIMESTAMP WITH TIME ZONE NULL 
                                                DEFAULT(NOW())
);

CREATE UNIQUE INDEX recurring_invoices_item_id_auto_trigger_on_sales_uix
ON core.recurring_invoices(item_id, auto_trigger_on_sales)
WHERE auto_trigger_on_sales = true;

CREATE TABLE core.recurring_invoice_setup
(
    recurring_invoice_setup_id                  SERIAL NOT NULL PRIMARY KEY,
    recurring_invoice_id                        integer NOT NULL REFERENCES core.recurring_invoices(recurring_invoice_id),
    party_id                                    bigint NOT NULL REFERENCES core.parties(party_id),
    starts_from                                 date NOT NULL,
    ends_on                                     date NOT NULL
                                                CONSTRAINT recurring_invoice_setup_date_chk
                                                CHECK
                                                (
                                                    ends_on >= starts_from
                                                ),
    recurrence_type_id                          integer NOT NULL REFERENCES core.recurrence_types(recurrence_type_id),
    recurring_frequency_id                      integer NULL REFERENCES core.frequencies(frequency_id),
    recurring_duration                          public.integer_strict2 NOT NULL DEFAULT(0),
    recurs_on_same_calendar_date                boolean NOT NULL DEFAULT(true),
    recurring_amount                            public.money_strict NOT NULL,
    account_id                                  bigint REFERENCES core.accounts(account_id),
    payment_term_id                             integer NOT NULL REFERENCES core.payment_terms(payment_term_id),
    is_active                                   boolean NOT NULL DEFAULT(true),
    statement_reference                         national character varying(100) NOT NULL DEFAULT(''),
    audit_user_id                               integer NULL REFERENCES office.users(user_id),
    audit_ts                                    TIMESTAMP WITH TIME ZONE NULL 
                                                DEFAULT(NOW())    
    
);


INSERT INTO core.recurring_invoices
(
    recurring_invoice_id, 
    recurring_invoice_code, 
    recurring_invoice_name, 
    item_id, 
    recurring_frequency_id, 
    recurring_amount,
    auto_trigger_on_sales,
    payment_term_id,
    audit_user_id,
    audit_ts
)
SELECT
    recurring_invoice_id, 
    recurring_invoice_code, 
    recurring_invoice_name, 
    item_id, 
    recurring_frequency_id, 
    recurring_amount,
    auto_trigger_on_sales,
    core.get_payment_term_id_by_payment_term_code('07-D'),
    audit_user_id,
    audit_ts
FROM core.recurring_invoices_temp;

SELECT setval
(
    pg_get_serial_sequence('core.recurring_invoices', 'recurring_invoice_id'), 
    (SELECT MAX(recurring_invoice_id) FROM core.recurring_invoices) + 1
); 


INSERT INTO core.recurring_invoice_setup
(
    recurring_invoice_setup_id,
    recurring_invoice_id,
    party_id,
    starts_from,
    ends_on,
    recurring_amount,
    payment_term_id,
    audit_user_id,
    audit_ts
)
SELECT
    recurring_invoice_setup_id,
    recurring_invoice_id,
    party_id,
    starts_from,
    ends_on,
    recurring_amount,
    payment_term_id,
    audit_user_id,
    audit_ts
FROM core.recurring_invoice_setup_temp;

UPDATE core.recurring_invoices
SET account_id = core.get_account_id_by_account_number('30100');

UPDATE core.recurring_invoice_setup
SET account_id = core.get_account_id_by_account_number('30100');

ALTER TABLE core.recurring_invoices
ALTER COLUMN account_id SET NOT NULL;

ALTER TABLE core.recurring_invoice_setup
ALTER COLUMN account_id SET NOT NULL;

SELECT setval
(
    pg_get_serial_sequence('core.recurring_invoice_setup', 'recurring_invoice_setup_id'), 
    (SELECT MAX(recurring_invoice_setup_id) FROM core.recurring_invoice_setup) + 1
); 

DROP TABLE IF EXISTS core.recurring_invoices_temp;
DROP TABLE IF EXISTS core.recurring_invoice_setup_temp;

DO
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT 1
        FROM   pg_attribute 
        WHERE  attrelid = 'core.bonus_slabs'::regclass
        AND    attname = 'account_id'
        AND    NOT attisdropped
    ) THEN
        ALTER TABLE core.bonus_slabs
        ADD COLUMN account_id bigint NOT NULL 
        REFERENCES core.accounts(account_id)
        CONSTRAINT bonus_slab_account_id_df
        DEFAULT(core.get_account_id_by_account_number('40230'));
    END IF;

    IF NOT EXISTS
    (
        SELECT 1
        FROM   pg_attribute 
        WHERE  attrelid = 'core.bonus_slabs'::regclass
        AND    attname = 'statement_reference'
        AND    NOT attisdropped
    ) THEN
        ALTER TABLE core.bonus_slabs
        ADD COLUMN statement_reference national character varying(100) NOT NULL DEFAULT('');
    END IF;

    ALTER TABLE transactions.transaction_master
    ALTER COLUMN login_id DROP NOT NULL;
END
$$
LANGUAGE plpgsql;


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/audit/audit.get_office_id_by_login_id.sql --<--<--
DROP FUNCTION IF EXISTS audit.get_office_id_by_login_id(bigint);

CREATE FUNCTION audit.get_office_id_by_login_id(bigint)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN office_id
    FROM audit.logins
    WHERE login_id=$1;
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/audit/audit.get_user_id_by_login_id.sql --<--<--
DROP FUNCTION IF EXISTS audit.get_user_id_by_login_id(bigint);

CREATE FUNCTION audit.get_user_id_by_login_id(bigint)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN user_id
    FROM audit.logins
    WHERE login_id=$1;
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/core/core.create_menu.sql --<--<--
DROP FUNCTION IF EXISTS core.create_menu
(
    _menu_text          text,
    _url                text,
    _menu_code          text,
    _level              integer,
    _parent_menu_id     integer
);

CREATE FUNCTION core.create_menu
(
    _menu_text          text,
    _url                text,
    _menu_code          text,
    _level              integer,
    _parent_menu_id     integer
)
RETURNS void
VOLATILE
AS
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT * FROM core.menus
        WHERE menu_code = _menu_code
    ) THEN
        INSERT INTO core.menus(menu_text, url, menu_code, level, parent_menu_id)
        SELECT _menu_text, _url, _menu_code, _level, _parent_menu_id;
    END IF;

    UPDATE core.menus
    SET 
        menu_text       = _menu_text, 
        url             = _url, 
        level           = _level,
        parent_menu_id  = _parent_menu_id
    WHERE menu_code=_menu_code;    
END
$$
LANGUAGE plpgsql;


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/core/core.create_menu_locale.sql --<--<--
DROP FUNCTION IF EXISTS core.create_menu_locale
(
    _menu_id            integer,
    _culture            text,
    _menu_text          text
);

CREATE FUNCTION core.create_menu_locale
(
    _menu_id            integer,
    _culture            text,
    _menu_text          text
)
RETURNS void
VOLATILE
AS
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT * FROM core.menu_locale
        WHERE menu_id = _menu_id
        AND culture = _culture
    ) THEN
        INSERT INTO core.menu_locale(menu_id, culture, menu_text)
        SELECT _menu_id, _culture, _menu_text;
    END IF;

    UPDATE core.menu_locale
    SET
        menu_text       = _menu_text
    WHERE menu_id = _menu_id
    AND culture = _culture;
END
$$
LANGUAGE plpgsql;


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/core/core.get_frequency_setup_end_date_frequency_setup_id.sql --<--<--
DROP FUNCTION IF EXISTS core.get_frequency_setup_end_date_frequency_setup_id(_frequency_setup_id integer);
DROP FUNCTION IF EXISTS core.get_frequency_setup_end_date_by_frequency_setup_id(_frequency_setup_id integer);
CREATE FUNCTION core.get_frequency_setup_end_date_by_frequency_setup_id(_frequency_setup_id integer)
RETURNS date
AS
$$
BEGIN
    RETURN
        value_date
    FROM
        core.frequency_setups
    WHERE
        frequency_setup_id = $1;
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/core/core.get_frequency_setup_start_date_frequency_setup_id.sql --<--<--
DROP FUNCTION IF EXISTS core.get_frequency_setup_start_date_frequency_setup_id(_frequency_setup_id integer);
DROP FUNCTION IF EXISTS core.get_frequency_setup_start_date_by_frequency_setup_id(_frequency_setup_id integer);
CREATE FUNCTION core.get_frequency_setup_start_date_by_frequency_setup_id(_frequency_setup_id integer)
RETURNS date
AS
$$
    DECLARE _start_date date;
BEGIN
    SELECT MAX(value_date) + 1 
    INTO _start_date
    FROM core.frequency_setups
    WHERE value_date < 
    (
        SELECT value_date
        FROM core.frequency_setups
        WHERE frequency_setup_id = $1
    );

    IF(_start_date IS NULL) THEN
        SELECT starts_from 
        INTO _start_date
        FROM core.fiscal_year;
    END IF;

    RETURN _start_date;
END
$$
LANGUAGE plpgsql;


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/core/core.get_menu_id_by_menu_code.sql --<--<--
DROP FUNCTION IF EXISTS core.get_menu_id_by_menu_code(national character varying(250));

CREATE FUNCTION core.get_menu_id_by_menu_code(national character varying(250))
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN menu_id
    FROM core.menus
    WHERE menu_code=$1;
END
$$
LANGUAGE plpgsql;


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/core/core.get_recurrence_type_id_by_recurrence_type_code.sql --<--<--
DROP FUNCTION IF EXISTS core.get_recurrence_type_id_by_recurrence_type_code(text);

CREATE FUNCTION core.get_recurrence_type_id_by_recurrence_type_code(text)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN recurrence_type_id
    FROM core.recurrence_types
    WHERE recurrence_type_code = $1;
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/core/core.cast_frequency.sql --<--<--
DROP FUNCTION IF EXISTS core.cast_frequency(text) CASCADE;

CREATE FUNCTION core.cast_frequency(text)
RETURNS integer
IMMUTABLE
AS
$$
BEGIN
    IF(UPPER($1) = 'EOM') THEN
        RETURN 2;
    END IF;
    IF(UPPER($1) = 'EOQ') THEN
        RETURN 3;
    END IF;
    IF(UPPER($1) = 'EOH') THEN
        RETURN 4;
    END IF;
    IF(UPPER($1) = 'EOY') THEN
        RETURN 5;
    END IF;

    RETURN NULL;
END
$$
LANGUAGE plpgsql;

CREATE CAST (text as integer)
WITH FUNCTION core.cast_frequency(text) AS ASSIGNMENT;




-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/core/core.get_frequency_start_date.sql --<--<--
DROP FUNCTION IF EXISTS core.get_frequency_start_date(_frequency_id integer, _value_date date);

CREATE FUNCTION core.get_frequency_start_date(_frequency_id integer, _value_date date)
RETURNS date
STABLE
AS
$$
    DECLARE _start_date date;
BEGIN
    SELECT MAX(value_date) 
    INTO _start_date
    FROM core.frequency_setups
    WHERE value_date < $2
    AND frequency_id = ANY(core.get_frequencies($1));

    IF(_start_date IS NULL AND $1 = 'EOY'::text::integer) THEN
        SELECT MAX(starts_from)
        INTO _start_date
        FROM core.fiscal_year
        WHERE starts_from < $2;
    END IF;

    RETURN _start_date;
END
$$
LANGUAGE plpgsql;

--SELECT * FROM core.get_frequency_start_date('eoy'::text::integer, '2015-05-14');


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/core/core.get_periods.sql --<--<--
DROP FUNCTION IF EXISTS core.get_periods
(
    _date_from                      date,
    _date_to                        date
);

CREATE FUNCTION core.get_periods
(
    _date_from                      date,
    _date_to                        date
)
RETURNS core.period[]
VOLATILE
AS
$$
BEGIN
    DROP TABLE IF EXISTS frequency_setups_temp;
    CREATE TEMPORARY TABLE frequency_setups_temp
    (
        frequency_setup_id      int,
        value_date              date
    ) ON COMMIT DROP;

    INSERT INTO frequency_setups_temp
    SELECT frequency_setup_id, value_date
    FROM core.frequency_setups
    WHERE value_date BETWEEN _date_from AND _date_to
    ORDER BY value_date;

    RETURN
        array_agg
        (
            (
                core.get_frequency_setup_code_by_frequency_setup_id(frequency_setup_id),
                core.get_frequency_setup_start_date_by_frequency_setup_id(frequency_setup_id),
                core.get_frequency_setup_end_date_by_frequency_setup_id(frequency_setup_id)
            )::core.period
        )::core.period[]
    FROM frequency_setups_temp;
END
$$
LANGUAGE plpgsql;

--SELECT * FROM core.get_periods('1-1-2000', '1-1-2020');

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/public/public.add_column.sql --<--<--
DROP FUNCTION IF EXISTS public.add_column(regclass, text, regtype, text, text);

CREATE FUNCTION public.add_column
(
    _table_name     regclass, 
    _column_name    text, 
    _data_type      regtype,
    _default        text = '',
    _comment        text = ''
)
RETURNS void
VOLATILE
AS
$$
BEGIN
    IF NOT EXISTS (
       SELECT 1 FROM pg_attribute
       WHERE  attrelid = _table_name
       AND    attname = _column_name
       AND    NOT attisdropped) THEN
        EXECUTE '
        ALTER TABLE ' || _table_name || ' ADD COLUMN ' || quote_ident(_column_name) || ' ' || _data_type;
    END IF;

    IF(COALESCE(_default, '') != '') THEN
        EXECUTE '
        ALTER TABLE ' || _table_name || ' ALTER COLUMN ' || _column_name || ' SET DEFAULT ' || _default;
    END IF;

    IF(COALESCE(_comment, '') != '') THEN
        EXECUTE '
        COMMENT ON COLUMN ' || _table_name || '.' || _column_name || ' IS ''' || _comment || ''''; 
    END IF;    
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/transactions/core.get_frequencies.sql --<--<--
DROP FUNCTION IF EXISTS core.get_frequencies(_frequency_id integer);

CREATE FUNCTION core.get_frequencies(_frequency_id integer)
RETURNS integer[]
IMMUTABLE
AS
$$
    DECLARE _frequencies integer[];
BEGIN
    IF(_frequency_id = 2) THEN--End of month
        --End of month
        --End of quarter is also end of third/ninth month
        --End of half is also end of sixth month
        --End of year is also end of twelfth month
        _frequencies = ARRAY[2, 3, 4, 5];
    ELSIF(_frequency_id = 3) THEN--End of quarter
        --End of quarter
        --End of half is the second end of quarter
        --End of year is the fourth/last end of quarter
        _frequencies = ARRAY[3, 4, 5];
    ELSIF(_frequency_id = 4) THEN--End of half
        --End of half
        --End of year is the second end of half
        _frequencies = ARRAY[4, 5];
    ELSIF(_frequency_id = 5) THEN--End of year
        --End of year
        _frequencies = ARRAY[5];
    END IF;

    RETURN _frequencies;
END
$$
LANGUAGE plpgsql;

--SELECT * FROM core.get_frequencies(3);

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/transactions/transactions.auto_verify.sql --<--<--
DROP FUNCTION IF EXISTS transactions.auto_verify
(
    _tran_id        bigint,
    _office_id      integer
) CASCADE;

CREATE FUNCTION transactions.auto_verify
(
    _tran_id        bigint,
    _office_id      integer
)
RETURNS VOID
VOLATILE
AS
$$
    DECLARE _transaction_master_id          bigint;
    DECLARE _transaction_posted_by          integer;
    DECLARE _verifier                       integer;
    DECLARE _status                         integer;
    DECLARE _reason                         national character varying(128);
    DECLARE _rejected                       smallint=-3;
    DECLARE _closed                         smallint=-2;
    DECLARE _withdrawn                      smallint=-1;
    DECLARE _unapproved                     smallint = 0;
    DECLARE _auto_approved                  smallint = 1;
    DECLARE _approved                       smallint=2;
    DECLARE _book                           text;
    DECLARE _auto_verify_sales              boolean;
    DECLARE _sales_verification_limit       public.money_strict2;
    DECLARE _auto_verify_purchase           boolean;
    DECLARE _purchase_verification_limit    public.money_strict2;
    DECLARE _auto_verify_gl                 boolean;
    DECLARE _gl_verification_limit          public.money_strict2;
    DECLARE _posted_amount                  public.money_strict2;
    DECLARE _auto_verification              boolean=true;
    DECLARE _has_policy                     boolean=false;
    DECLARE _voucher_date                   date;
    DECLARE _value_date                     date=transactions.get_value_date(_office_id);
BEGIN
    _transaction_master_id := $1;

    SELECT
        transactions.transaction_master.book,
        transactions.transaction_master.value_date,
        transactions.transaction_master.user_id
    INTO
        _book,
        _voucher_date,
        _transaction_posted_by  
    FROM
    transactions.transaction_master
    WHERE transactions.transaction_master.transaction_master_id=_transaction_master_id;

    IF(_voucher_date <> _value_date) THEN
        RETURN;
    END IF;

    _verifier := office.get_sys_user_id();
    _status := 2;
    _reason := 'Automatically verified by workflow.';

    IF EXISTS
    (
        SELECT 1 FROM policy.voucher_verification_policy    
        WHERE user_id=_verifier
        AND is_active=true
        AND now() >= effective_from
        AND now() <= ends_on
    ) THEN
        RAISE INFO 'A sys cannot have a verification policy defined.';
        RETURN;
    END IF;
    
    SELECT
        SUM(amount_in_local_currency)
    INTO
        _posted_amount
    FROM
        transactions.transaction_details
    WHERE transactions.transaction_details.transaction_master_id = _transaction_master_id
    AND transactions.transaction_details.tran_type='Cr';


    SELECT
        true,
        verify_sales_transactions,
        sales_verification_limit,
        verify_purchase_transactions,
        purchase_verification_limit,
        verify_gl_transactions,
        gl_verification_limit
    INTO
        _has_policy,
        _auto_verify_sales,
        _sales_verification_limit,
        _auto_verify_purchase,
        _purchase_verification_limit,
        _auto_verify_gl,
        _gl_verification_limit
    FROM
    policy.auto_verification_policy
    WHERE user_id=_transaction_posted_by
    AND is_active=true
    AND now() >= effective_from
    AND now() <= ends_on;



    IF(lower(_book) LIKE 'sales%') THEN
        IF(_auto_verify_sales = false) THEN
            _auto_verification := false;
        END IF;
        IF(_auto_verify_sales = true) THEN
            IF(_posted_amount > _sales_verification_limit AND _sales_verification_limit > 0::public.money_strict2) THEN
                _auto_verification := false;
            END IF;
        END IF;         
    END IF;


    IF(lower(_book) LIKE 'purchase%') THEN
        IF(_auto_verify_purchase = false) THEN
            _auto_verification := false;
        END IF;
        IF(_auto_verify_purchase = true) THEN
            IF(_posted_amount > _purchase_verification_limit AND _purchase_verification_limit > 0::public.money_strict2) THEN
                _auto_verification := false;
            END IF;
        END IF;         
    END IF;


    IF(lower(_book) LIKE 'journal%') THEN
        IF(_auto_verify_gl = false) THEN
            _auto_verification := false;
        END IF;
        IF(_auto_verify_gl = true) THEN
            IF(_posted_amount > _gl_verification_limit AND _gl_verification_limit > 0::public.money_strict2) THEN
                _auto_verification := false;
            END IF;
        END IF;         
    END IF;

    IF(_has_policy=true) THEN
        IF(_auto_verification = true) THEN
            UPDATE transactions.transaction_master
            SET 
                last_verified_on = now(),
                verified_by_user_id=_verifier,
                verification_status_id=_status,
                verification_reason=_reason
            WHERE
                transactions.transaction_master.transaction_master_id=_transaction_master_id;

            PERFORM transactions.create_recurring_invoices(_transaction_master_id);
        END IF;
    ELSE
        RAISE NOTICE 'No auto verification policy found for this user.';
    END IF;
    RETURN;
END
$$
LANGUAGE plpgsql;



/**************************************************************************************************************************
--------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------
'########::'##:::::::'########:::'######:::'##::::'##:'##::: ##:'####:'########::::'########:'########::'######::'########:
 ##.... ##: ##::::::: ##.... ##:'##... ##:: ##:::: ##: ###:: ##:. ##::... ##..:::::... ##..:: ##.....::'##... ##:... ##..::
 ##:::: ##: ##::::::: ##:::: ##: ##:::..::: ##:::: ##: ####: ##:: ##::::: ##:::::::::: ##:::: ##::::::: ##:::..::::: ##::::
 ########:: ##::::::: ########:: ##::'####: ##:::: ##: ## ## ##:: ##::::: ##:::::::::: ##:::: ######:::. ######::::: ##::::
 ##.....::: ##::::::: ##.....::: ##::: ##:: ##:::: ##: ##. ####:: ##::::: ##:::::::::: ##:::: ##...:::::..... ##:::: ##::::
 ##:::::::: ##::::::: ##:::::::: ##::: ##:: ##:::: ##: ##:. ###:: ##::::: ##:::::::::: ##:::: ##:::::::'##::: ##:::: ##::::
 ##:::::::: ########: ##::::::::. ######:::. #######:: ##::. ##:'####:::: ##:::::::::: ##:::: ########:. ######::::: ##::::
..:::::::::........::..::::::::::......:::::.......:::..::::..::....:::::..:::::::::::..:::::........:::......::::::..:::::
--------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------
**************************************************************************************************************************/


DROP FUNCTION IF EXISTS unit_tests.auto_verify_sales_test1();

CREATE FUNCTION unit_tests.auto_verify_sales_test1()
RETURNS public.test_result
AS
$$
    DECLARE _value_date                             date;
    DECLARE _tran_id                                bigint;
    DECLARE _verification_status_id                 smallint;
    DECLARE _book_name                              national character varying(12)='Sales.Direct';
    DECLARE _office_id                              integer;
    DECLARE _user_id                                integer;
    DECLARE _login_id                               bigint;
    DECLARE _cost_center_id                         integer;
    DECLARE _reference_number                       national character varying(24)='Plpgunit.fixture';
    DECLARE _statement_reference                    text='Plpgunit test was here.';
    DECLARE _is_credit                              boolean=false;
    DECLARE _payment_term_id                        integer;
    DECLARE _party_code                             national character varying(12);
    DECLARE _price_type_id                          integer;
    DECLARE _salesperson_id                         integer;
    DECLARE _shipper_id                             integer;
    DECLARE _shipping_address_code                  national character varying(12)='';
    DECLARE _store_id                               integer;
    DECLARE _is_non_taxable_sales                   boolean=true;
    DECLARE _details                                transactions.stock_detail_type[];
    DECLARE _attachments                            core.attachment_type[];
    DECLARE message                                 test_result;
BEGIN
    PERFORM unit_tests.create_mock();
    PERFORM unit_tests.sign_in_test();

    _office_id          := office.get_office_id_by_office_code('dummy-off01');
    _user_id            := office.get_user_id_by_user_name('plpgunit-test-user-000001');
    _login_id           := office.get_login_id(_user_id);
    _value_date         := transactions.get_value_date(_office_id);
    _cost_center_id     := office.get_cost_center_id_by_cost_center_code('dummy-cs01');
    _payment_term_id    := core.get_payment_term_id_by_payment_term_code('dummy-pt01');
    _party_code         := 'dummy-pr01';
    _price_type_id      := core.get_price_type_id_by_price_type_code('dummy-pt01');
    _salesperson_id     := core.get_salesperson_id_by_salesperson_code('dummy-sp01');
    _shipper_id         := core.get_shipper_id_by_shipper_code('dummy-sh01');
    _store_id           := office.get_store_id_by_store_code('dummy-st01');

    
    _details            := ARRAY[
                             ROW(_store_id, 'dummy-it01', 1, 'Test Mock Unit',1800000, 0, 0, '', 0)::transactions.stock_detail_type,
                             ROW(_store_id, 'dummy-it02', 2, 'Test Mock Unit',1300000, 300, 0, '', 0)::transactions.stock_detail_type];
             
    
    PERFORM unit_tests.create_dummy_auto_verification_policy(office.get_user_id_by_user_name('plpgunit-test-user-000001'), true, 0, true, 0, true, 0, '1-1-2000', '1-1-2020', true);


    SELECT * FROM transactions.post_sales
    (
        _book_name,_office_id, _user_id, _login_id, _value_date, _cost_center_id, _reference_number, _statement_reference,
        _is_credit, _payment_term_id, _party_code, _price_type_id, _salesperson_id, _shipper_id,
        _shipping_address_code,
        _store_id,
        _is_non_taxable_sales,
        _details,
        _attachments
    ) INTO _tran_id;

    SELECT verification_status_id
    INTO _verification_status_id
    FROM transactions.transaction_master
    WHERE transaction_master_id = _tran_id;

    IF(_verification_status_id < 1) THEN
        SELECT assert.fail('This transaction should have been verified.') INTO message;
        RETURN message;
    END IF;

    SELECT assert.ok('End of test.') INTO message;  
    RETURN message;
END
$$
LANGUAGE plpgsql;


DROP FUNCTION IF EXISTS unit_tests.auto_verify_sales_test2();

CREATE FUNCTION unit_tests.auto_verify_sales_test2()
RETURNS public.test_result
AS
$$
    DECLARE _value_date                             date;
    DECLARE _tran_id                                bigint;
    DECLARE _verification_status_id                 smallint;
    DECLARE _book_name                              national character varying(12)='Sales.Direct';
    DECLARE _office_id                              integer;
    DECLARE _user_id                                integer;
    DECLARE _login_id                               bigint;
    DECLARE _cost_center_id                         integer;
    DECLARE _reference_number                       national character varying(24)='Plpgunit.fixture';
    DECLARE _statement_reference                    text='Plpgunit test was here.';
    DECLARE _is_credit                              boolean=false;
    DECLARE _payment_term_id                        integer;
    DECLARE _party_code                             national character varying(12);
    DECLARE _price_type_id                          integer;
    DECLARE _salesperson_id                         integer;
    DECLARE _shipper_id                             integer;
    DECLARE _shipping_address_code                  national character varying(12)='';
    DECLARE _store_id                               integer;
    DECLARE _is_non_taxable_sales                   boolean=true;
    DECLARE _details                                transactions.stock_detail_type[];
    DECLARE _attachments                            core.attachment_type[];
    DECLARE message                                 test_result;
BEGIN
    PERFORM unit_tests.create_mock();
    PERFORM unit_tests.sign_in_test();

    _office_id          := office.get_office_id_by_office_code('dummy-off01');
    _user_id            := office.get_user_id_by_user_name('plpgunit-test-user-000001');
    _login_id           := office.get_login_id(_user_id);
    _value_date         := transactions.get_value_date(_office_id);
    _cost_center_id     := office.get_cost_center_id_by_cost_center_code('dummy-cs01');
    _payment_term_id    := core.get_payment_term_id_by_payment_term_code('dummy-pt01');
    _party_code         := 'dummy-pr01';
    _price_type_id      := core.get_price_type_id_by_price_type_code('dummy-pt01');
    _salesperson_id     := core.get_salesperson_id_by_salesperson_code('dummy-sp01');
    _shipper_id         := core.get_shipper_id_by_shipper_code('dummy-sh01');
    _store_id           := office.get_store_id_by_store_code('dummy-st01');

    
    _details            := ARRAY[
                             ROW(_store_id, 'dummy-it01', 1, 'Test Mock Unit',180000, 0, 0, '', 0)::transactions.stock_detail_type,
                             ROW(_store_id, 'dummy-it02', 2, 'Test Mock Unit',130000, 300, 0, '', 0)::transactions.stock_detail_type];

    PERFORM unit_tests.create_dummy_auto_verification_policy(office.get_user_id_by_user_name('plpgunit-test-user-000001'), true, 100, true, 0, true, 0, '1-1-2000', '1-1-2020', true);

    SELECT * FROM transactions.post_sales
    (
        _book_name,_office_id, _user_id, _login_id, _value_date, _cost_center_id, _reference_number, _statement_reference,
        _is_credit, _payment_term_id, _party_code, _price_type_id, _salesperson_id, _shipper_id,
        _shipping_address_code,
        _store_id,
        _is_non_taxable_sales,
        _details,
        _attachments
    ) INTO _tran_id;


    SELECT verification_status_id
    INTO _verification_status_id
    FROM transactions.transaction_master
    WHERE transaction_master_id = _tran_id;

    IF(_verification_status_id > 0) THEN
        SELECT assert.fail('This transaction should not have been verified.') INTO message;
        RETURN message;
    END IF;

    SELECT assert.ok('End of test.') INTO message;  
    RETURN message;
END
$$
LANGUAGE plpgsql;






DROP FUNCTION IF EXISTS unit_tests.auto_verify_purchase_test1();

CREATE FUNCTION unit_tests.auto_verify_purchase_test1()
RETURNS public.test_result
AS
$$
    DECLARE _value_date                             date;
    DECLARE _tran_id                                bigint;
    DECLARE _verification_status_id                 smallint;
    DECLARE _book_name                              national character varying(12)='Sales.Direct';
    DECLARE _office_id                              integer;
    DECLARE _user_id                                integer;
    DECLARE _login_id                               bigint;
    DECLARE _cost_center_id                         integer;
    DECLARE _reference_number                       national character varying(24)='Plpgunit.fixture';
    DECLARE _statement_reference                    text='Plpgunit test was here.';
    DECLARE _is_credit                              boolean=false;
    DECLARE _payment_term_id                        integer;
    DECLARE _party_code                             national character varying(12);
    DECLARE _price_type_id                          integer;
    DECLARE _shipper_id                             integer;
    DECLARE _store_id                               integer;
    DECLARE _is_non_taxable_sales                   boolean=true;
    DECLARE _tran_ids                               bigint[];
    DECLARE _details                                transactions.stock_detail_type[];
    DECLARE _attachments                            core.attachment_type[];
    DECLARE message                                 test_result;
BEGIN
    PERFORM unit_tests.create_mock();
    PERFORM unit_tests.sign_in_test();

    _office_id          := office.get_office_id_by_office_code('dummy-off01');
    _user_id            := office.get_user_id_by_user_name('plpgunit-test-user-000001');
    _login_id           := office.get_login_id(_user_id);
    _value_date         := transactions.get_value_date(_office_id);
    _cost_center_id     := office.get_cost_center_id_by_cost_center_code('dummy-cs01');
    _party_code         := 'dummy-pr01';
    _price_type_id      := core.get_price_type_id_by_price_type_code('dummy-pt01');
    _shipper_id         := core.get_shipper_id_by_shipper_code('dummy-sh01');
    _store_id           := office.get_store_id_by_store_code('dummy-st01');

    
    _details            := ARRAY[
                             ROW(_store_id, 'dummy-it01', 1, 'Test Mock Unit',180000, 0, 0, '', 0)::transactions.stock_detail_type,
                             ROW(_store_id, 'dummy-it02', 2, 'Test Mock Unit',130000, 300, 0, '', 0)::transactions.stock_detail_type];

    PERFORM unit_tests.create_dummy_auto_verification_policy(office.get_user_id_by_user_name('plpgunit-test-user-000001'), true, 0, true, 0, true, 0, '1-1-2000', '1-1-2020', true);

    SELECT * FROM transactions.post_purchase
    (
        _book_name,_office_id, _user_id, _login_id, _value_date, _cost_center_id, _reference_number, _statement_reference,
        _is_credit, _party_code, _price_type_id, _shipper_id,
        _store_id, _tran_ids, _details, _attachments
    ) INTO _tran_id;


    SELECT verification_status_id
    INTO _verification_status_id
    FROM transactions.transaction_master
    WHERE transaction_master_id = _tran_id;

    IF(_verification_status_id < 1) THEN
            SELECT assert.fail('This transaction should have been verified.') INTO message;
            RETURN message;
    END IF;

    SELECT assert.ok('End of test.') INTO message;  
    RETURN message;
END
$$
LANGUAGE plpgsql;


DROP FUNCTION IF EXISTS unit_tests.auto_verify_purchase_test2();

CREATE FUNCTION unit_tests.auto_verify_purchase_test2()
RETURNS public.test_result
AS
$$
    DECLARE _value_date                             date;
    DECLARE _tran_id                                bigint;
    DECLARE _verification_status_id                 smallint;
    DECLARE _book_name                              national character varying(12)='Sales.Direct';
    DECLARE _office_id                              integer;
    DECLARE _user_id                                integer;
    DECLARE _login_id                               bigint;
    DECLARE _cost_center_id                         integer;
    DECLARE _reference_number                       national character varying(24)='Plpgunit.fixture';
    DECLARE _statement_reference                    text='Plpgunit test was here.';
    DECLARE _is_credit                              boolean=false;
    DECLARE _payment_term_id                        integer;
    DECLARE _party_code                             national character varying(12);
    DECLARE _price_type_id                          integer;
    DECLARE _shipper_id                             integer;
    DECLARE _store_id                               integer;
    DECLARE _is_non_taxable_sales                   boolean=true;
    DECLARE _tran_ids                               bigint[];
    DECLARE _details                                transactions.stock_detail_type[];
    DECLARE _attachments                            core.attachment_type[];
    DECLARE message                                 test_result;
BEGIN
    PERFORM unit_tests.create_mock();
    PERFORM unit_tests.sign_in_test();

    _office_id          := office.get_office_id_by_office_code('dummy-off01');
    _user_id            := office.get_user_id_by_user_name('plpgunit-test-user-000001');
    _login_id           := office.get_login_id(_user_id);
    _value_date         := transactions.get_value_date(_office_id);
    _cost_center_id     := office.get_cost_center_id_by_cost_center_code('dummy-cs01');
    _party_code         := 'dummy-pr01';
    _price_type_id      := core.get_price_type_id_by_price_type_code('dummy-pt01');
    _shipper_id         := core.get_shipper_id_by_shipper_code('dummy-sh01');
    _store_id           := office.get_store_id_by_store_code('dummy-st01');

    
    _details            := ARRAY[
                             ROW(_store_id, 'dummy-it01', 1, 'Test Mock Unit',180000, 0, 0, '', 0)::transactions.stock_detail_type,
                             ROW(_store_id, 'dummy-it02', 2, 'Test Mock Unit',130000, 300, 0, '', 0)::transactions.stock_detail_type];

    PERFORM unit_tests.create_dummy_auto_verification_policy(office.get_user_id_by_user_name('plpgunit-test-user-000001'), true, 0, true, 100, true, 0, '1-1-2000', '1-1-2000', true);


    SELECT * FROM transactions.post_purchase
    (
        _book_name,_office_id, _user_id, _login_id, _value_date, _cost_center_id, _reference_number, _statement_reference,
        _is_credit, _party_code, _price_type_id, _shipper_id,
        _store_id, _tran_ids, _details, _attachments
    ) INTO _tran_id;


    SELECT verification_status_id
    INTO _verification_status_id
    FROM transactions.transaction_master
    WHERE transaction_master_id = _tran_id;

    IF(_verification_status_id > 0) THEN
        SELECT assert.fail('This transaction should not have been verified.') INTO message;
        RETURN message;
    END IF;

    SELECT assert.ok('End of test.') INTO message;  
    RETURN message;
END
$$
LANGUAGE plpgsql;

DROP FUNCTION IF EXISTS unit_tests.auto_verify_journal_test1();

CREATE FUNCTION unit_tests.auto_verify_journal_test1()
RETURNS public.test_result
AS
$$
    DECLARE _value_date                             date;
    DECLARE _office_id                              integer;
    DECLARE _user_id                                integer;
    DECLARE _login_id                               bigint;
    DECLARE _tran_id                                bigint;
    DECLARE _verification_status_id                 smallint;
    DECLARE message                                 test_result;
BEGIN
    PERFORM unit_tests.create_mock();
    PERFORM unit_tests.sign_in_test();

    _office_id          := office.get_office_id_by_office_code('dummy-off01');
    _value_date         := transactions.get_value_date(_office_id);
    _user_id            := office.get_user_id_by_user_name('plpgunit-test-user-000001');
    _login_id           := office.get_login_id(_user_id);

    PERFORM unit_tests.create_dummy_auto_verification_policy(office.get_user_id_by_user_name('plpgunit-test-user-000001'), true, 0, true, 0, true, 0, '1-1-2000', '1-1-2020', true);

    _tran_id := nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));

    INSERT INTO transactions.transaction_master
    (
        transaction_master_id, 
        transaction_counter, 
        transaction_code, 
        book, 
        value_date, 
        user_id, 
        login_id, 
        office_id, 
        reference_number, 
        statement_reference
    )
    SELECT 
        _tran_id, 
        transactions.get_new_transaction_counter(_value_date), 
        transactions.get_transaction_code(_value_date, _office_id, _user_id, 1),
        'Journal',
        _value_date,
        _user_id,
        _login_id,
        _office_id,
        'REF# TEST',
        'Thou art not able to see this.';



    INSERT INTO transactions.transaction_details
    (
        transaction_master_id, 
        value_date,
        tran_type, 
        account_id, 
        statement_reference, 
        currency_code, 
        amount_in_currency, 
        local_currency_code,    
        er, 
        amount_in_local_currency
    )

    SELECT _tran_id, _value_date, 'Cr', core.get_account_id_by_account_number('dummy-acc01'), '', 'NPR', 12000, 'NPR', 1, 12000 UNION ALL
    SELECT _tran_id, _value_date, 'Dr', core.get_account_id_by_account_number('dummy-acc02'), '', 'NPR', 3000, 'NPR', 1, 3000 UNION ALL
    SELECT _tran_id, _value_date, 'Dr', core.get_account_id_by_account_number('dummy-acc03'), '', 'NPR', 9000, 'NPR', 1, 9000;


    PERFORM transactions.auto_verify(currval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id')), office.get_office_id_by_office_code('dummy-off01'));

    SELECT verification_status_id
    INTO _verification_status_id
    FROM transactions.transaction_master
    WHERE transaction_master_id = _tran_id;

    IF(_verification_status_id < 1) THEN
            SELECT assert.fail('This transaction should have been verified.') INTO message;
            RETURN message;
    END IF;

    SELECT assert.ok('End of test.') INTO message;  
    RETURN message;
END
$$
LANGUAGE plpgsql;



DROP FUNCTION IF EXISTS unit_tests.auto_verify_journal_test2();

CREATE FUNCTION unit_tests.auto_verify_journal_test2()
RETURNS public.test_result
AS
$$
    DECLARE _value_date                             date;
    DECLARE _office_id                              integer;
    DECLARE _user_id                                integer;
    DECLARE _login_id                               bigint;
    DECLARE _tran_id                                bigint;
    DECLARE _verification_status_id                 smallint;
    DECLARE message                                 test_result;
BEGIN
    PERFORM unit_tests.create_mock();
    PERFORM unit_tests.sign_in_test();

    _office_id          := office.get_office_id_by_office_code('dummy-off01');
    _value_date         := transactions.get_value_date(_office_id);
    _user_id            := office.get_user_id_by_user_name('plpgunit-test-user-000001');
    _login_id           := office.get_login_id(_user_id);

     PERFORM unit_tests.create_dummy_auto_verification_policy(office.get_user_id_by_user_name('plpgunit-test-user-000001'), true, 0, true, 0, true, 100, '1-1-2000', '1-1-2020', true);
    _tran_id := nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));

    INSERT INTO transactions.transaction_master
    (
        transaction_master_id, 
        transaction_counter, 
        transaction_code, 
        book, 
        value_date, 
        user_id, 
        login_id, 
        office_id, 
        reference_number, 
        statement_reference
    )
    SELECT 
        _tran_id, 
        transactions.get_new_transaction_counter(_value_date), 
        transactions.get_transaction_code(_value_date, _office_id, _user_id, 1),
        'Journal',
        _value_date,
        _user_id,
        _login_id,
        _office_id,
        'REF# TEST',
        'Thou art not able to see this.';



    INSERT INTO transactions.transaction_details
    (
        transaction_master_id,
        value_date,
        tran_type, 
        account_id, 
        statement_reference, 
        currency_code, 
        amount_in_currency, 
        local_currency_code,    
        er, 
        amount_in_local_currency
    )
    SELECT _tran_id, _value_date, 'Cr', core.get_account_id_by_account_number('dummy-acc01'), '', 'NPR', 12000, 'NPR', 1, 12000 UNION ALL
    SELECT _tran_id, _value_date, 'Dr', core.get_account_id_by_account_number('dummy-acc02'), '', 'NPR', 3000, 'NPR', 1, 3000 UNION ALL
    SELECT _tran_id, _value_date, 'Dr', core.get_account_id_by_account_number('dummy-acc03'), '', 'NPR', 9000, 'NPR', 1, 9000;


    PERFORM transactions.auto_verify(currval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id')), office.get_office_id_by_office_code('dummy-off01'));

    SELECT verification_status_id
    INTO _verification_status_id
    FROM transactions.transaction_master
    WHERE transaction_master_id = _tran_id;

    IF(_verification_status_id > 0) THEN
            SELECT assert.fail('This transaction should not have been verified.') INTO message;
            RETURN message;
    END IF;

    SELECT assert.ok('End of test.') INTO message;  
    RETURN message;
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/transactions/transactions.create_recurring_invoices.sql --<--<--
DROP FUNCTION IF EXISTS transactions.create_recurring_invoices(bigint);

CREATE FUNCTION transactions.create_recurring_invoices(bigint)
RETURNS void
VOLATILE
AS
$$
    DECLARE _party_id       bigint;
BEGIN
    IF NOT EXISTS
    (
        SELECT * FROM transactions.transaction_master WHERE book IN('Sales.Direct', 'Sales.Delivery')
        AND transaction_master_id=$1
        AND verification_status_id > 0
    ) THEN
        RETURN;
    END IF;

    SELECT party_id INTO _party_id 
    FROM transactions.stock_master
    WHERE transaction_master_id = $1;

    IF(COALESCE(_party_id, 0) = 0) THEN
        RETURN;
    END IF;
    
    DROP TABLE IF EXISTS recurring_invoices_temp;

    CREATE TEMPORARY TABLE recurring_invoices_temp
    (
        recurring_invoice_id            integer,
        party_id                        bigint,
        total_duration                  integer,
        starts_from                     date,
        ends_on                         date,
        recurrence_type_id              integer,
        recurring_frequency_id          integer,
        recurring_duration              integer,
        recurs_on_same_calendar_date    boolean,
        recurring_amount                public.money_strict,
        account_id                      bigint,
        payment_term_id                 integer,
        is_active                       boolean DEFAULT(true),
        statement_reference             national character varying(100)
    ) ON COMMIT DROP;

    INSERT INTO recurring_invoices_temp
    (
        recurring_invoice_id,
        total_duration,
        recurrence_type_id,
        recurring_frequency_id,
        recurring_duration,
        recurs_on_same_calendar_date,
        recurring_amount,
        account_id,
        payment_term_id,
        is_active,
        statement_reference
    )
    SELECT
        recurring_invoice_id,
        total_duration,
        recurrence_type_id,
        recurring_frequency_id,
        recurring_duration,
        recurs_on_same_calendar_date,
        recurring_amount,
        account_id,
        payment_term_id,
        is_active,
        statement_reference
    FROM core.recurring_invoices
    WHERE is_active
    AND auto_trigger_on_sales
    AND item_id
    IN
    (
        SELECT item_id FROM transactions.stock_details
        INNER JOIN transactions.stock_master
        ON transactions.stock_master.stock_master_id = transactions.stock_details.stock_master_id
        WHERE 1 = 1
        AND transactions.stock_master.transaction_master_id = $1
        AND tran_type='Cr'
    );

    UPDATE recurring_invoices_temp
    SET 
        party_id        = _party_id, 
        starts_from     = now()::date,
        ends_on         = now()::date + total_duration;

    INSERT INTO core.recurring_invoice_setup
    (
        recurring_invoice_id,
        party_id,
        starts_from,
        ends_on,
        recurrence_type_id,
        recurring_frequency_id,
        recurring_duration,
        recurs_on_same_calendar_date,
        recurring_amount,
        account_id,
        payment_term_id,
        is_active,
        statement_reference
    )
    SELECT
        recurring_invoice_id,
        party_id,
        starts_from,
        ends_on,
        recurrence_type_id,
        recurring_frequency_id,
        recurring_duration,
        recurs_on_same_calendar_date,
        recurring_amount,
        account_id,
        payment_term_id,
        is_active,
        statement_reference
    FROM recurring_invoices_temp;
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/transactions/transactions.create_routine.sql --<--<--
DROP FUNCTION IF EXISTS transactions.create_routine(_routine_code national character varying(12), _routine regproc, _order integer);

CREATE FUNCTION transactions.create_routine(_routine_code national character varying(12), _routine regproc, _order integer)
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT * FROM transactions.routines WHERE routine_code=_routine_code) THEN
        INSERT INTO transactions.routines(routine_code, routine_name, "order")
        SELECT $1, $2, $3;
        RETURN;
    END IF;

    UPDATE transactions.routines
    SET
        routine_name = _routine,
        "order" = _order
    WHERE routine_code=_routine_code;
    RETURN;
END
$$
LANGUAGE plpgsql;


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/transactions/transactions.get_cash_repository_balance.sql --<--<--
DROP FUNCTION IF EXISTS transactions.get_cash_repository_balance(_cash_repository_id integer, _currency_code national character varying(12));
CREATE FUNCTION transactions.get_cash_repository_balance(_cash_repository_id integer, _currency_code national character varying(12))
RETURNS public.money_strict2
AS
$$
    DECLARE _debit public.money_strict2;
    DECLARE _credit public.money_strict2;
BEGIN
    SELECT COALESCE(SUM(amount_in_currency), 0::public.money_strict2) INTO _debit
    FROM transactions.verified_transaction_view
    WHERE cash_repository_id=$1
    AND currency_code=$2
    AND tran_type='Dr';

    SELECT COALESCE(SUM(amount_in_currency), 0::public.money_strict2) INTO _credit
    FROM transactions.verified_transaction_view
    WHERE cash_repository_id=$1
    AND currency_code=$2
    AND tran_type='Cr';

    RETURN _debit - _credit;
END
$$
LANGUAGE plpgsql;


DROP FUNCTION IF EXISTS transactions.get_cash_repository_balance(_cash_repository_id integer);
CREATE FUNCTION transactions.get_cash_repository_balance(_cash_repository_id integer)
RETURNS public.money_strict2
AS
$$
    DECLARE _local_currency_code national character varying(12) = transactions.get_default_currency_code($1);
    DECLARE _debit public.money_strict2;
    DECLARE _credit public.money_strict2;
BEGIN
    SELECT COALESCE(SUM(amount_in_currency), 0::public.money_strict2) INTO _debit
    FROM transactions.verified_transaction_view
    WHERE cash_repository_id=$1
    AND currency_code=_local_currency_code
    AND tran_type='Dr';

    SELECT COALESCE(SUM(amount_in_currency), 0::public.money_strict2) INTO _credit
    FROM transactions.verified_transaction_view
    WHERE cash_repository_id=$1
    AND currency_code=_local_currency_code
    AND tran_type='Cr';

    RETURN _debit - _credit;
END
$$
LANGUAGE plpgsql;




-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/transactions/transactions.get_net_profit.sql --<--<--
DROP FUNCTION IF EXISTS transactions.get_net_profit
(
    _date_from                      date,
    _date_to                        date,
    _office_id                      integer,
    _factor                         integer,
    _no_provison                    boolean
);

CREATE FUNCTION transactions.get_net_profit
(
    _date_from                      date,
    _date_to                        date,
    _office_id                      integer,
    _factor                         integer,
    _no_provison                    boolean DEFAULT false
)
RETURNS decimal(24, 4)
AS
$$
    DECLARE _incomes                decimal(24, 4) = 0;
    DECLARE _expenses               decimal(24, 4) = 0;
    DECLARE _profit_before_tax      decimal(24, 4) = 0;
    DECLARE _tax_paid               decimal(24, 4) = 0;
    DECLARE _tax_provison           decimal(24, 4) = 0;
BEGIN
    SELECT SUM(CASE tran_type WHEN 'Cr' THEN amount_in_local_currency ELSE amount_in_local_currency * -1 END)
    INTO _incomes
    FROM transactions.verified_transaction_mat_view
    WHERE value_date >= _date_from AND value_date <= _date_to
    AND office_id IN (SELECT * FROM office.get_office_ids(_office_id))
    AND account_master_id >=20100
    AND account_master_id <= 20300;
    
    SELECT SUM(CASE tran_type WHEN 'Dr' THEN amount_in_local_currency ELSE amount_in_local_currency * -1 END)
    INTO _expenses
    FROM transactions.verified_transaction_mat_view
    WHERE value_date >= _date_from AND value_date <= _date_to
    AND office_id IN (SELECT * FROM office.get_office_ids(_office_id))
    AND account_master_id >=20400
    AND account_master_id <= 20701;
    
    SELECT SUM(CASE tran_type WHEN 'Dr' THEN amount_in_local_currency ELSE amount_in_local_currency * -1 END)
    INTO _tax_paid
    FROM transactions.verified_transaction_mat_view
    WHERE value_date >= _date_from AND value_date <= _date_to
    AND office_id IN (SELECT * FROM office.get_office_ids(_office_id))
    AND account_master_id =20800;
    
    _profit_before_tax := COALESCE(_incomes, 0) - COALESCE(_expenses, 0);

    IF(_no_provison) THEN
        RETURN (_profit_before_tax - COALESCE(_tax_paid, 0)) / _factor;
    END IF;
    
    _tax_provison      := core.get_income_tax_provison_amount(_office_id, _profit_before_tax, COALESCE(_tax_paid, 0));
    
    RETURN (_profit_before_tax - (COALESCE(_tax_provison, 0) + COALESCE(_tax_paid, 0))) / _factor;
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/transactions/transactions.perform_eod_operation.sql --<--<--
DROP FUNCTION IF EXISTS transactions.perform_eod_operation(_user_id integer, _office_id integer, _value_date date);
DROP FUNCTION IF EXISTS transactions.perform_eod_operation(_user_id integer, _login_id bigint, _office_id integer, _value_date date);

CREATE FUNCTION transactions.perform_eod_operation(_user_id integer, _login_id bigint, _office_id integer, _value_date date)
RETURNS boolean
AS
$$
    DECLARE _routine            regproc;
    DECLARE _routine_id         integer;
    DECLARE this                RECORD;
    DECLARE _sql                text;
    DECLARE _is_error           boolean=false;
    DECLARE _notice             text;
    DECLARE _office_code        text;
BEGIN
    IF(_value_date IS NULL) THEN
        RAISE EXCEPTION 'Invalid date.'
        USING ERRCODE='P3008';
    END IF;

    IF(NOT policy.is_elevated_user(_user_id)) THEN
        RAISE EXCEPTION 'Access is denied.'
        USING ERRCODE='P9001';
    END IF;

    IF(_value_date != transactions.get_value_date(_office_id)) THEN
        RAISE EXCEPTION 'Invalid value date.'
        USING ERRCODE='P3007';
    END IF;

    SELECT * FROM transactions.day_operation
    WHERE value_date=_value_date 
    AND office_id = _office_id INTO this;

    IF(this IS NULL) THEN
        RAISE EXCEPTION 'Invalid value date.'
        USING ERRCODE='P3007';
    ELSE    
        IF(this.completed OR this.completed_on IS NOT NULL) THEN
            RAISE WARNING 'EOD operation was already performed.';
            _is_error        := true;
        END IF;
    END IF;
    
    IF(NOT _is_error) THEN
        _office_code        := office.get_office_code_by_id(_office_id);
        _notice             := 'EOD started.'::text;
        RAISE INFO  '%', _notice;

        FOR this IN
        SELECT routine_id, routine_name 
        FROM transactions.routines 
        WHERE status 
        ORDER BY "order" ASC
        LOOP
            _routine_id             := this.routine_id;
            _routine                := this.routine_name;
            _sql                    := format('SELECT * FROM %1$s($1, $2, $3, $4);', _routine);

            RAISE NOTICE '%', _sql;

            _notice             := 'Performing ' || _routine::text || '.';
            RAISE INFO '%', _notice;

            PERFORM pg_sleep(5);
            EXECUTE _sql USING _user_id, _login_id, _office_id, _value_date;

            _notice             := 'Completed  ' || _routine::text || '.';
            RAISE INFO '%', _notice;
            
            PERFORM pg_sleep(5);            
        END LOOP;


        UPDATE transactions.day_operation SET 
            completed_on = NOW(), 
            completed_by = _user_id,
            completed = true
        WHERE value_date=_value_date
        AND office_id = _office_id;

        _notice             := 'EOD of ' || _office_code || ' for ' || _value_date::text || ' completed without errors.'::text;
        RAISE INFO '%', _notice;

        _notice             := 'OK'::text;
        RAISE INFO '%', _notice;

        RETURN true;
    END IF;

    RETURN false;    
END;
$$
LANGUAGE plpgsql;

DROP FUNCTION IF EXISTS transactions.perform_eod_operation(_login_id bigint);

CREATE FUNCTION transactions.perform_eod_operation(_login_id bigint)
RETURNS boolean
AS
$$
    DECLARE _user_id    integer;
    DECLARE _office_id integer;
    DECLARE _value_date date;
BEGIN
    SELECT 
        user_id,
        office_id,
        transactions.get_value_date(office_id)
    INTO
        _user_id,
        _office_id,
        _value_date
    FROM audit.logins
    WHERE login_id=$1;

    RETURN transactions.perform_eod_operation(_user_id,_login_id, _office_id, _value_date);
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/transactions/transactions.post_bonus.sql --<--<--
DROP FUNCTION IF EXISTS transactions.post_bonus(_user_id integer, _login_id bigint, _office_id integer, _value_date date);

CREATE FUNCTION transactions.post_bonus(_user_id integer, _login_id bigint, _office_id integer, _value_date date)
RETURNS void
VOLATILE
AS
$$
    DECLARE _frequency_id           integer;
    DECLARE _transaction_master_id  bigint;
    DECLARE _tran_counter           integer;
    DECLARE _transaction_code       text;
    DECLARE _default_currency_code  national character varying(12);
    DECLARE _sys                    integer = office.get_sys_user_id();
    DECLARE this                    RECORD;
BEGIN
    IF(_value_date != transactions.get_value_date(_office_id)) THEN
        RAISE EXCEPTION 'Invalid value date.'
        USING ERRCODE='P3007';
    END IF;

    IF NOT EXISTS(SELECT 1 FROM core.bonus_slabs WHERE ends_on >= _value_date) THEN
        RETURN;
    END IF;

    IF NOT EXISTS(SELECT 1 FROM core.salesperson_bonus_setups) THEN
        RETURN;
    END IF;

    SELECT frequency_id INTO _frequency_id
    FROM core.frequency_setups
    WHERE value_date = _value_date;

    IF(COALESCE(_frequency_id, 0) = 0) THEN
        --Today does not fall on a frequency
        RETURN;
    END IF;


    DROP TABLE IF EXISTS bonus_slab_temp;
    CREATE TEMPORARY TABLE bonus_slab_temp
    (
        bonus_slab_id               integer,
        bonus_slab_code             text,
        bonus_slab_name             text,
        checking_frequency_id       integer,
        amount_from                 public.money_strict,
        amount_to                   public.money_strict,
        bonus_rate                  numeric,
        salesperson_id              integer,
        period_from                 date,
        period_to                   date,
        salesperson_account_id      bigint,
        bonus_account_id            bigint,
        statement_reference         national character varying(100)
    ) ON COMMIT DROP;

    INSERT INTO bonus_slab_temp(bonus_slab_id, bonus_slab_code, bonus_slab_name, checking_frequency_id, amount_from, amount_to, bonus_rate, salesperson_id, salesperson_account_id, bonus_account_id, statement_reference)
    SELECT
        core.bonus_slabs.bonus_slab_id,
        core.bonus_slabs.bonus_slab_code,
        core.bonus_slabs.bonus_slab_name,
        core.bonus_slabs.checking_frequency_id,
        core.bonus_slab_details.amount_from,
        core.bonus_slab_details.amount_to,
        core.bonus_slab_details.bonus_rate,
        core.salesperson_bonus_setups.salesperson_id,
        core.salespersons.account_id,
        core.bonus_slabs.account_id,
        core.bonus_slabs.statement_reference
    FROM core.bonus_slab_details
    INNER JOIN core.bonus_slabs
    ON core.bonus_slabs.bonus_slab_id = core.bonus_slab_details.bonus_slab_id
    INNER JOIN core.salesperson_bonus_setups
    ON core.salesperson_bonus_setups.bonus_slab_id = core.bonus_slabs.bonus_slab_id
    INNER JOIN core.salespersons
    ON core.salespersons.salesperson_id = core.salesperson_bonus_setups.salesperson_id
    WHERE ends_on >= _value_date::date
    AND _frequency_id = ANY(core.get_frequencies(core.bonus_slabs.checking_frequency_id));

    IF(SELECT COUNT(*) FROM bonus_slab_temp) = 0 THEN
        --Nothing found to post today
        RETURN;
    END IF;

    UPDATE bonus_slab_temp
    SET period_to = _value_date,
    period_from = core.get_frequency_start_date(_frequency_id, _value_date);

    DROP TABLE IF EXISTS bonus_temp;
    CREATE TEMPORARY TABLE bonus_temp
    (
        id                      SERIAL,
        salesperson_id          integer,
        period_from             date,
        period_to               date,
        sales                   public.money_strict2,
        bonus_rate              numeric,
        bonus                   numeric,
        salesperson_account_id  bigint,
        bonus_account_id        bigint,
        statement_reference     national character varying(100)
    ) ON COMMIT DROP;

    INSERT INTO bonus_temp(salesperson_id, period_from, period_to, salesperson_account_id, bonus_account_id, statement_reference)
    SELECT 
    DISTINCT 
        bonus_slab_temp.salesperson_id, 
        bonus_slab_temp.period_from, 
        bonus_slab_temp.period_to,
        bonus_slab_temp.salesperson_account_id,
        bonus_slab_temp.bonus_account_id,        
        bonus_slab_temp.statement_reference
    FROM bonus_slab_temp;
    
    UPDATE bonus_temp
    SET sales = 
    (
        SELECT
            SUM
            (
                (
                    COALESCE(quantity, 0)
                    * 
                    COALESCE(price, 0)
                ) - COALESCE(discount, 0)
            ) AS total
        FROM transactions.transaction_master
        INNER JOIN transactions.stock_master
        ON transactions.transaction_master.transaction_master_id = transactions.stock_master.transaction_master_id
        INNER JOIN transactions.stock_details
        ON transactions.stock_details.stock_master_id = transactions.stock_master.stock_master_id
        WHERE transactions.transaction_master.verification_status_id > 0
        AND transactions.stock_master.salesperson_id = bonus_temp.salesperson_id
        AND transactions.transaction_master.book = ANY(ARRAY['Sales.Direct', 'Sales.Delivery'])
        AND transactions.transaction_master.value_date
        BETWEEN bonus_temp.period_from AND bonus_temp.period_to
   );

   
    UPDATE bonus_temp
    SET bonus_rate = 
    (
        SELECT bonus_slab_temp.bonus_rate
        FROM bonus_slab_temp
        WHERE bonus_slab_temp.salesperson_id = bonus_temp.salesperson_id
        AND bonus_temp.sales > bonus_slab_temp.amount_from
        AND bonus_temp.sales <= bonus_slab_temp.amount_to
    );


    UPDATE bonus_temp
    SET bonus = ROUND(bonus_temp.sales * bonus_temp.bonus_rate / 100, 2);

    UPDATE bonus_temp
    SET statement_reference = REPLACE(bonus_temp.statement_reference, '{From}', bonus_temp.period_from::text);

    UPDATE bonus_temp
    SET statement_reference = REPLACE(bonus_temp.statement_reference, '{To}', bonus_temp.period_to::text);


    FOR this IN
    SELECT bonus_temp.id 
    FROM bonus_temp 
    WHERE COALESCE(bonus_temp.bonus, 0) > 0
    LOOP
        _transaction_master_id  := nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));
        _tran_counter           := transactions.get_new_transaction_counter(_value_date);
        _transaction_code       := transactions.get_transaction_code(_value_date, _office_id, _user_id, _login_id);

        INSERT INTO transactions.transaction_master
        (
            transaction_master_id, 
            transaction_counter, 
            transaction_code, 
            book, 
            value_date, 
            user_id, 
            office_id, 
            statement_reference,
            verification_status_id,
            sys_user_id,
            verified_by_user_id,
            verification_reason
        ) 
        SELECT            
            _transaction_master_id, 
            _tran_counter, 
            _transaction_code, 
            'Bonus.Slab', 
            _value_date, 
            _user_id, 
            _office_id,             
            bonus_temp.statement_reference,
            1,
            _sys,
            _sys,
            'Automatically verified by workflow.'
        FROM bonus_temp
        WHERE bonus_temp.id  = this.id
        LIMIT 1;

        INSERT INTO transactions.transaction_details
        (
            transaction_master_id,
            value_date,
            tran_type, 
            account_id, 
            statement_reference, 
            currency_code, 
            amount_in_currency, 
            er, 
            local_currency_code, 
            amount_in_local_currency
        )
        SELECT
            _transaction_master_id,
            _value_date,
            'Cr',
            bonus_temp.salesperson_account_id,
            bonus_temp.statement_reference,
            _default_currency_code, 
            bonus_temp.bonus, 
            1 AS exchange_rate,
            _default_currency_code,
            bonus_temp.bonus
        FROM bonus_temp
        WHERE bonus_temp.id = this.id

        UNION ALL
        SELECT
            _transaction_master_id,
            _value_date,
            'Dr',
            bonus_temp.bonus_account_id,
            bonus_temp.statement_reference,
            _default_currency_code, 
            bonus_temp.bonus, 
            1 AS exchange_rate,
            _default_currency_code,
            bonus_temp.bonus
        FROM bonus_temp
        WHERE bonus_temp.id = this.id;
    END LOOP;    
END
$$
LANGUAGE plpgsql;

DELETE FROM transactions.routines where routine_code='REF-POBNS';
SELECT transactions.create_routine('POST-BNS', 'transactions.post_bonus', 201);

--select * from transactions.post_bonus(2, 5, 2, '2015-04-13');





-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/transactions/transactions.post_er_fluctuation.sql --<--<--
DROP FUNCTION IF EXISTS transactions.post_er_fluctuation(_user_id integer, _login_id bigint, _office_id integer, _value_date date);

CREATE FUNCTION transactions.post_er_fluctuation(_user_id integer, _login_id bigint, _office_id integer, _value_date date)
RETURNS void
AS
$$
BEGIN

END
$$
LANGUAGE plpgsql;


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/transactions/transactions.post_late_fee.sql --<--<--
DROP FUNCTION IF EXISTS transactions.post_late_fee(_user_id integer, _login_id bigint, _office_id integer, _value_date date);

CREATE FUNCTION transactions.post_late_fee(_user_id integer, _login_id bigint, _office_id integer, _value_date date)
RETURNS void
AS
$$
BEGIN

END
$$
LANGUAGE plpgsql;


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/transactions/transactions.post_purchase_return.sql --<--<--
DROP FUNCTION IF EXISTS transactions.post_purchase_return
(
    _transaction_master_id          bigint,
    _office_id                      integer,
    _user_id                        integer,
    _login_id                       bigint,
    _value_date                     date,
    _store_id                       integer,
    _party_code                     national character varying(12),
    _price_type_id                  integer,
    _reference_number               national character varying(24),
    _statement_reference            text,
    _details                        transactions.stock_detail_type[],
    _attachments                    core.attachment_type[]
);

CREATE FUNCTION transactions.post_purchase_return
(
    _transaction_master_id          bigint,
    _office_id                      integer,
    _user_id                        integer,
    _login_id                       bigint,
    _value_date                     date,
    _store_id                       integer,
    _party_code                     national character varying(12),
    _price_type_id                  integer,
    _reference_number               national character varying(24),
    _statement_reference            text,
    _details                        transactions.stock_detail_type[],
    _attachments                    core.attachment_type[]
)
RETURNS bigint
AS
$$
    DECLARE _party_id                       bigint;
    DECLARE _cost_center_id                 bigint;
    DECLARE _tran_master_id                 bigint;
    DECLARE _stock_detail_id                bigint;
    DECLARE _tran_counter                   integer;
    DECLARE _transaction_code               text;
    DECLARE _stock_master_id                bigint;
    DECLARE _grand_total                    money_strict;
    DECLARE _discount_total                 money_strict2;
    DECLARE _tax_total                      money_strict2;
    DECLARE _is_credit                      boolean;
    DECLARE _credit_account_id              bigint;
    DECLARE _default_currency_code          national character varying(12);
    DECLARE _sm_id                          bigint;
    DECLARE this                            RECORD;
    DECLARE _shipping_address_code          national character varying(12);
    DECLARE _is_periodic                    boolean = office.is_periodic_inventory(_office_id);
    DECLARE _book_name                      text='Purchase.Return';
    DECLARE _receivable                     money_strict;
BEGIN
    IF(policy.can_post_transaction(_login_id, _user_id, _office_id, _book_name, _value_date) = false) THEN
        RETURN 0;
    END IF;
    
    CREATE TEMPORARY TABLE temp_stock_details
    (
        id                              SERIAL PRIMARY KEY,
        stock_master_id                 bigint, 
        tran_type                       transaction_type, 
        store_id                        integer,
        item_code                       text,
        item_id                         integer, 
        quantity                        integer_strict,
        unit_name                       text,
        unit_id                         integer,
        base_quantity                   decimal,
        base_unit_id                    integer,                
        price                           money_strict,
        discount                        money_strict2,
        shipping_charge                 money_strict2,
        tax_form                        text,
        sales_tax_id                    integer,
        tax                             money_strict2,
        purchase_account_id             integer, 
        purchase_discount_account_id    integer, 
        inventory_account_id            integer
    ) ON COMMIT DROP;

    CREATE TEMPORARY TABLE temp_stock_tax_details
    (
        id                                      SERIAL,
        temp_stock_detail_id                    integer REFERENCES temp_stock_details(id),
        sales_tax_detail_code                   text,
        stock_detail_id                         bigint,
        sales_tax_detail_id                     integer,
        state_sales_tax_id                      integer,
        county_sales_tax_id                     integer,
        account_id                              integer,
        principal                               money_strict,
        rate                                    decimal_strict,
        tax                                     money_strict
    ) ON COMMIT DROP;

    CREATE TEMPORARY TABLE temp_transaction_details
    (
        transaction_master_id       BIGINT, 
        tran_type                   transaction_type, 
        account_id                  integer, 
        statement_reference         text, 
        cash_repository_id          integer, 
        currency_code               national character varying(12), 
        amount_in_currency          money_strict, 
        local_currency_code         national character varying(12), 
        er                          decimal_strict, 
        amount_in_local_currency    money_strict
    ) ON COMMIT DROP;

    _party_id                       := core.get_party_id_by_party_code(_party_code);
    _default_currency_code          := transactions.get_default_currency_code_by_office_id(_office_id);
    
    SELECT 
        cost_center_id   
    INTO 
        _cost_center_id    
    FROM transactions.transaction_master 
    WHERE transactions.transaction_master.transaction_master_id = _transaction_master_id;

    SELECT 
        is_credit,
        core.get_shipping_address_code_by_shipping_address_id(shipping_address_id),
        stock_master_id
    INTO 
        _is_credit,
        _shipping_address_code,
        _sm_id
    FROM transactions.stock_master 
    WHERE transaction_master_id = _transaction_master_id;

    INSERT INTO temp_stock_details(store_id, item_code, quantity, unit_name, price, discount, shipping_charge, tax_form, tax)
    SELECT store_id, item_code, quantity, unit_name, price, discount, shipping_charge, tax_form, tax
    FROM explode_array(_details);

    UPDATE temp_stock_details 
    SET
        tran_type                   = 'Cr',
        sales_tax_id                = core.get_sales_tax_id_by_sales_tax_code(tax_form),
        item_id                     = core.get_item_id_by_item_code(item_code),
        unit_id                     = core.get_unit_id_by_unit_name(unit_name),
        base_quantity               = core.get_base_quantity_by_unit_name(unit_name, quantity),
        base_unit_id                = core.get_base_unit_id_by_unit_name(unit_name);

    UPDATE temp_stock_details
    SET
        purchase_account_id             = core.get_purchase_account_id(item_id),
        purchase_discount_account_id    = core.get_purchase_discount_account_id(item_id),
        inventory_account_id            = core.get_inventory_account_id(item_id);

    IF EXISTS
    (

        SELECT * 
        FROM transactions.stock_details
        INNER JOIN temp_stock_details
        ON temp_stock_details.item_id = transactions.stock_details.item_id
        WHERE transactions.stock_details.stock_master_id = _sm_id
        AND COALESCE(temp_stock_details.sales_tax_id, 0) != COALESCE(transactions.stock_details.sales_tax_id, 0)
        LIMIT 1
    ) THEN
        RAISE EXCEPTION 'Tax form mismatch.'
        USING ERRCODE='P3202';
    END IF;
    
    IF EXISTS
    (
            SELECT 1 FROM temp_stock_details AS details
            WHERE core.is_valid_unit_id(details.unit_id, details.item_id) = false
            LIMIT 1
    ) THEN
        RAISE EXCEPTION 'Item/unit mismatch.'
        USING ERRCODE='P3201';
    END IF;

    FOR this IN SELECT * FROM temp_stock_details
    LOOP
        PERFORM FROM transactions.validate_item_for_return(_transaction_master_id, this.store_id, this.item_code, this.unit_name, this.quantity, this.price);
    END LOOP;

    FOR this IN SELECT * FROM temp_stock_details ORDER BY id
    LOOP
        INSERT INTO temp_stock_tax_details
        (
            temp_stock_detail_id,
            sales_tax_detail_code,
            account_id, 
            sales_tax_detail_id, 
            state_sales_tax_id, 
            county_sales_tax_id, 
            principal, 
            rate, 
            tax
        )
        SELECT 
            this.id, 
            sales_tax_detail_code,
            account_id, 
            sales_tax_detail_id, 
            state_sales_tax_id, 
            county_sales_tax_id, 
            taxable_amount, 
            rate, 
            tax
        FROM transactions.get_sales_tax('Purchase', _store_id, _party_code, _shipping_address_code, _price_type_id, this.item_code, this.price, this.quantity, this.discount, this.shipping_charge, this.sales_tax_id);
    END LOOP;
    
    UPDATE temp_stock_details
    SET tax =
    (SELECT SUM(COALESCE(temp_stock_tax_details.tax, 0)) FROM temp_stock_tax_details
    WHERE temp_stock_tax_details.temp_stock_detail_id = temp_stock_details.id);

    _credit_account_id = core.get_account_id_by_party_code(_party_code); 

        
    SELECT SUM(COALESCE(tax, 0))                                INTO _tax_total FROM temp_stock_tax_details;
    SELECT SUM(COALESCE(discount, 0))                           INTO _discount_total FROM temp_stock_details;
    SELECT SUM(COALESCE(price, 0) * COALESCE(quantity, 0))      INTO _grand_total FROM temp_stock_details;

    _receivable := _grand_total - COALESCE(_discount_total, 0) + COALESCE(_tax_total, 0);


    IF(_is_periodic = true) THEN
        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Cr', purchase_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0)), 1, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0))
        FROM temp_stock_details
        GROUP BY purchase_account_id;
    ELSE
        --Perpetutal Inventory Accounting System
        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Cr', inventory_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0)), 1, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0))
        FROM temp_stock_details
        GROUP BY inventory_account_id;
    END IF;


    IF(COALESCE(_tax_total, 0) > 0) THEN
        FOR this IN 
        SELECT 
            format('P: %s x R: %s %% = %s (%s)', principal::text, rate::text, tax::text, sales_tax_detail_code) as statement_reference,
            account_id,
            tax
        FROM temp_stock_tax_details ORDER BY id
        LOOP    
            INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
            SELECT 'Cr', this.account_id, this.statement_reference || _statement_reference, _default_currency_code, this.tax, 1, _default_currency_code, this.tax;
        END LOOP;
    END IF;

    IF(COALESCE(_discount_total, 0) > 0) THEN
        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Dr', purchase_discount_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(discount, 0)), 1, _default_currency_code, SUM(COALESCE(discount, 0))
        FROM temp_stock_details
        GROUP BY purchase_discount_account_id;
    END IF;

    INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
    SELECT 'Dr', core.get_account_id_by_party_id(_party_id), _statement_reference, _default_currency_code, _receivable, 1, _default_currency_code, _receivable;


    _tran_master_id         := nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));
    _stock_master_id        := nextval(pg_get_serial_sequence('transactions.stock_master', 'stock_master_id'));
    _tran_counter           := transactions.get_new_transaction_counter(_value_date);
    _transaction_code       := transactions.get_transaction_code(_value_date, _office_id, _user_id, _login_id);

    UPDATE temp_transaction_details     SET transaction_master_id   = _tran_master_id;
    UPDATE temp_stock_details           SET stock_master_id         = _stock_master_id;

    INSERT INTO transactions.transaction_master(transaction_master_id, transaction_counter, transaction_code, book, value_date, user_id, login_id, office_id, cost_center_id, reference_number, statement_reference) 
    SELECT _tran_master_id, _tran_counter, _transaction_code, _book_name, _value_date, _user_id, _login_id, _office_id, _cost_center_id, _reference_number, _statement_reference;


    INSERT INTO transactions.transaction_details(value_date, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency)
    SELECT _value_date, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency
    FROM temp_transaction_details
    ORDER BY tran_type DESC;


    INSERT INTO transactions.stock_master(value_date, stock_master_id, transaction_master_id, party_id, price_type_id, is_credit, shipper_id, shipping_charge, store_id, cash_repository_id)
    SELECT _value_date, _stock_master_id, _tran_master_id, _party_id, _price_type_id, _is_credit, NULL, 0, _store_id, NULL;
            
    FOR this IN SELECT * FROM temp_stock_details ORDER BY id
    LOOP
        _stock_detail_id        := nextval(pg_get_serial_sequence('transactions.stock_details', 'stock_detail_id'));

        INSERT INTO transactions.stock_details(stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, sales_tax_id, tax)
        SELECT _stock_detail_id, _value_date, this.stock_master_id, this.tran_type, this.store_id, this.item_id, this.quantity, this.unit_id, this.base_quantity, this.base_unit_id, this.price, this.discount, this.sales_tax_id, COALESCE(this.tax, 0)
        FROM temp_stock_details
        WHERE id = this.id;


        INSERT INTO transactions.stock_tax_details(stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax)
        SELECT _stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax
        FROM temp_stock_tax_details
        WHERE temp_stock_detail_id = this.id;
        
    END LOOP;

    INSERT INTO transactions.stock_return(transaction_master_id, return_transaction_master_id)
    SELECT _transaction_master_id, _tran_master_id;

    IF(array_length(_attachments, 1) > 0 AND _attachments != ARRAY[NULL::core.attachment_type]) THEN
        INSERT INTO core.attachments(user_id, resource, resource_key, resource_id, original_file_name, file_extension, file_path, comment)
        SELECT _user_id, 'transactions.transaction_master', 'transaction_master_id', _tran_master_id, original_file_name, file_extension, file_path, comment 
        FROM explode_array(_attachments);
    END IF;
    
    PERFORM transactions.auto_verify(_tran_master_id, _office_id);
    RETURN _tran_master_id;
END
$$
LANGUAGE plpgsql;




-- CREATE TEMPORARY TABLE temp_purchase_return
-- ON COMMIT DROP
-- AS
-- 
-- SELECT * FROM transactions.post_purchase_return(5, 2, 2, 1, '1-1-2000', 1, 'MAJON-0002', 1, '1234-AD', 'Test', 
-- ARRAY[
--  ROW(1, 'RMBP', 1, 'Piece', 180000, 0, 200, 'MoF-NY-BK-STX', 0)::transactions.stock_detail_type,
--  ROW(1, '13MBA', 1, 'Piece', 110000, 5000, 50, 'MoF-NY-BK-STX', 0)::transactions.stock_detail_type
-- ],
-- ARRAY[
-- NULL::core.attachment_type
-- ]);
-- 
-- SELECT  tran_type, core.get_account_name_by_account_id(account_id), amount_in_local_currency 
-- FROM transactions.transaction_details
-- WHERE transaction_master_id  = (SELECT * FROM temp_purchase_return);


/**************************************************************************************************************************
--------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------
'########::'##:::::::'########:::'######:::'##::::'##:'##::: ##:'####:'########::::'########:'########::'######::'########:
 ##.... ##: ##::::::: ##.... ##:'##... ##:: ##:::: ##: ###:: ##:. ##::... ##..:::::... ##..:: ##.....::'##... ##:... ##..::
 ##:::: ##: ##::::::: ##:::: ##: ##:::..::: ##:::: ##: ####: ##:: ##::::: ##:::::::::: ##:::: ##::::::: ##:::..::::: ##::::
 ########:: ##::::::: ########:: ##::'####: ##:::: ##: ## ## ##:: ##::::: ##:::::::::: ##:::: ######:::. ######::::: ##::::
 ##.....::: ##::::::: ##.....::: ##::: ##:: ##:::: ##: ##. ####:: ##::::: ##:::::::::: ##:::: ##...:::::..... ##:::: ##::::
 ##:::::::: ##::::::: ##:::::::: ##::: ##:: ##:::: ##: ##:. ###:: ##::::: ##:::::::::: ##:::: ##:::::::'##::: ##:::: ##::::
 ##:::::::: ########: ##::::::::. ######:::. #######:: ##::. ##:'####:::: ##:::::::::: ##:::: ########:. ######::::: ##::::
..:::::::::........::..::::::::::......:::::.......:::..::::..::....:::::..:::::::::::..:::::........:::......::::::..:::::
--------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------
**************************************************************************************************************************/




-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/transactions/transactions.post_recurring_invoices.sql --<--<--
DROP FUNCTION IF EXISTS transactions.post_recurring_invoices(_user_id integer, _login_id bigint, _office_id integer, _value_date date);

CREATE FUNCTION transactions.post_recurring_invoices(_user_id integer, _login_id bigint, _office_id integer, _value_date date)
RETURNS void
AS
$$
    DECLARE _frequency_id           integer;
    DECLARE _frequencies            integer[];
    DECLARE _day                    double precision;
    DECLARE _transaction_master_id  bigint;
    DECLARE _tran_counter           integer;
    DECLARE _transaction_code       text;
    DECLARE _sys                    integer = office.get_sys_user_id();
    DECLARE _default_currency_code  national character varying(12);
    DECLARE this                    RECORD;
BEGIN
    IF(_value_date != transactions.get_value_date(_office_id)) THEN
        RAISE EXCEPTION 'Invalid value date.'
        USING ERRCODE='P3007';
    END IF;

    _default_currency_code          := transactions.get_default_currency_code_by_office_id(_office_id);

    DROP TABLE IF EXISTS recurring_invoices_temp;
    CREATE TEMPORARY TABLE recurring_invoices_temp
    (
        id                          SERIAL,
        recurring_invoice_setup_id  integer,
        tran_type                   public.transaction_type,
        party_id                    bigint,
        recurring_amount            public.money_strict2,
        account_id                  bigint NOT NULL,
        statement_reference         national character varying(100),
        transaction_master_id       bigint
    ) ON COMMIT DROP;

    SELECT frequency_id INTO _frequency_id
    FROM core.frequency_setups
    WHERE value_date = _value_date;

    _frequency_id   := COALESCE(_frequency_id, 0);
    _day            := EXTRACT(DAY FROM _value_date);
    _frequencies    := core.get_frequencies(_frequency_id);

    --INSERT RECURRING INVOICES THAT :
    -->RECUR BASED ON SAME CALENDAR DATE 
    -->AND OCCUR TODAY 
    -->AND HAVE DURATION RECURRENCE TYPE
    INSERT INTO recurring_invoices_temp(recurring_invoice_setup_id, tran_type, party_id, recurring_amount, account_id, statement_reference)
    SELECT 
        core.recurring_invoice_setup.recurring_invoice_setup_id,
        'Cr' AS tran_type,
        core.recurring_invoice_setup.party_id, 
        core.recurring_invoice_setup.recurring_amount, 
        core.recurring_invoice_setup.account_id,
        core.recurring_invoice_setup.statement_reference
    FROM core.recurring_invoice_setup
    WHERE 1 = 1
    AND is_active                                   --IS ACTIVE
    AND _value_date > starts_from                   --HAS NOT STARTED YET
    AND _value_date <= ends_on                      --HAS NOT ENDED YET
    AND recurs_on_same_calendar_date                --RECURS ON THE SAME CALENDAR DATE
    AND _day = EXTRACT(DAY FROM starts_from) - 1    --OCCURS TODAY
    AND recurrence_type_id IN                       --HAS DURATION RECURRENCE TYPE
    (
        SELECT recurrence_type_id FROM core.recurrence_types
        WHERE NOT is_frequency
    );
   
    --INSERT RECURRING INVOICES THAT :
    -->DO NOT RECUR BASED ON SAME CALENDAR DATE, BUT RECURRING DAYS
    -->AND OCCUR TODAY
    -->AND HAVE DURATION RECURRENCE TYPE
    INSERT INTO recurring_invoices_temp(recurring_invoice_setup_id, tran_type, party_id, recurring_amount, account_id, statement_reference)
    SELECT 
        core.recurring_invoice_setup.recurring_invoice_setup_id, 
        'Cr' AS tran_type,
        core.recurring_invoice_setup.party_id, 
        core.recurring_invoice_setup.recurring_amount, 
        core.recurring_invoice_setup.account_id,
        core.recurring_invoice_setup.statement_reference
    FROM core.recurring_invoice_setup
    WHERE 1 = 1
    AND is_active                                   --IS ACTIVE
    AND _value_date > starts_from                   --HAS NOT STARTED YET
    AND _value_date <= ends_on                      --HAS NOT ENDED YET
    AND NOT recurs_on_same_calendar_date            --DOES NOT RECUR ON THE SAME CALENDAR DATE, BUT RECURRING DAYS
    --OCCURS TODAY
    AND _value_date
    IN
    (
        SELECT 
        GENERATE_SERIES
        (
            starts_from::timestamp, 
            ends_on::timestamp, 
            (
                recurring_duration::text || 'days'
            )::interval
        )::date - INTERVAL '1 DAY'
    )
    AND recurrence_type_id IN                       --HAS DURATION RECURRENCE TYPE
    (
        SELECT recurrence_type_id FROM core.recurrence_types
        WHERE NOT is_frequency
    );
   
    --INSERT RECURRING INVOICES THAT :
    -->OCCUR TODAY 
    -->AND RECUR BASED ON FREQUENCIES
    INSERT INTO recurring_invoices_temp(recurring_invoice_setup_id, tran_type, party_id, recurring_amount, account_id, statement_reference)
    SELECT
        core.recurring_invoice_setup.recurring_invoice_setup_id, 
        'Cr' AS tran_type,
        core.recurring_invoice_setup.party_id, 
        core.recurring_invoice_setup.recurring_amount, 
        core.recurring_invoice_setup.account_id,
        core.recurring_invoice_setup.statement_reference    
    FROM core.recurring_invoice_setup
    WHERE 1 = 1
    AND is_active                                   --IS ACTIVE
    AND _value_date > starts_from                   --HAS NOT STARTED YET
    AND _value_date <= ends_on                      --HAS NOT ENDED YET
    AND recurring_frequency_id = ANY(_frequencies)  --OCCURS TODAY
    AND recurrence_type_id IN                       --RECURS BASED ON FREQUENCIES
    (
        SELECT recurrence_type_id FROM core.recurrence_types
        WHERE is_frequency
    );

    UPDATE recurring_invoices_temp
    SET statement_reference = REPLACE(recurring_invoices_temp.statement_reference, '{RIMonth}', to_char(date_trunc('month', _value_date), 'MON'));

    UPDATE recurring_invoices_temp
    SET statement_reference = REPLACE(recurring_invoices_temp.statement_reference, '{RIYear}', to_char(date_trunc('year', _value_date), 'YYYY'));

    INSERT INTO recurring_invoices_temp(recurring_invoice_setup_id, tran_type, party_id, recurring_amount, account_id, statement_reference)
    SELECT 
        recurring_invoices_temp.recurring_invoice_setup_id, 
        'Dr' AS tran_type,
        recurring_invoices_temp.party_id, 
        recurring_invoices_temp.recurring_amount, 
        core.get_account_id_by_party_id(recurring_invoices_temp.party_id), 
        recurring_invoices_temp.statement_reference
    FROM recurring_invoices_temp;


    FOR this IN
    SELECT DISTINCT recurring_invoices_temp.recurring_invoice_setup_id
    FROM recurring_invoices_temp
    WHERE COALESCE(recurring_invoices_temp.recurring_amount, 0) > 0
    LOOP
        _transaction_master_id  := nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));
        _tran_counter           := transactions.get_new_transaction_counter(_value_date);
        _transaction_code       := transactions.get_transaction_code(_value_date, _office_id, _user_id, _login_id);

        INSERT INTO transactions.transaction_master
        (
            transaction_master_id, 
            transaction_counter, 
            transaction_code, 
            book, 
            value_date, 
            user_id, 
            office_id, 
            statement_reference,
            verification_status_id,
            sys_user_id,
            verified_by_user_id,
            verification_reason
        ) 
        SELECT            
            _transaction_master_id, 
            _tran_counter, 
            _transaction_code, 
            'Recurring.Invoice', 
            _value_date, 
            _user_id, 
            _office_id,             
            recurring_invoices_temp.statement_reference,
            1,
            _sys,
            _sys,
            'Automatically verified by workflow.'
        FROM recurring_invoices_temp
        WHERE recurring_invoices_temp.recurring_invoice_setup_id  = this.recurring_invoice_setup_id
        LIMIT 1;

        INSERT INTO transactions.transaction_details
        (
            transaction_master_id,
            value_date,
            tran_type, 
            account_id, 
            statement_reference, 
            currency_code, 
            amount_in_currency, 
            er, 
            local_currency_code, 
            amount_in_local_currency
        )
        SELECT
            _transaction_master_id,
            _value_date,
            recurring_invoices_temp.tran_type,
            recurring_invoices_temp.account_id,
            recurring_invoices_temp.statement_reference,
            _default_currency_code, 
            recurring_invoices_temp.recurring_amount, 
            1 AS exchange_rate,
            _default_currency_code,
            recurring_invoices_temp.recurring_amount
        FROM recurring_invoices_temp
        WHERE recurring_invoices_temp.recurring_invoice_setup_id  = this.recurring_invoice_setup_id;
    END LOOP;    
END
$$
LANGUAGE plpgsql;


DELETE FROM transactions.routines where routine_code='REF-PORCIV';
SELECT transactions.create_routine('POST-RCIV', 'transactions.post_recurring_invoices', 200);


--SELECT  * FROM transactions.post_recurring_invoices(2, 5, 2, '2015-04-17');


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/transactions/transactions.post_sales_return.sql --<--<--
DROP FUNCTION IF EXISTS transactions.post_sales_return
(
    _transaction_master_id          bigint,
    _office_id                      integer,
    _user_id                        integer,
    _login_id                       bigint,
    _value_date                     date,
    _store_id                       integer,
    _party_code                     national character varying(12),
    _price_type_id                  integer,
    _reference_number               national character varying(24),
    _statement_reference            text,
    _details                        transactions.stock_detail_type[],
    _attachments                    core.attachment_type[]
);

CREATE FUNCTION transactions.post_sales_return
(
    _transaction_master_id          bigint,
    _office_id                      integer,
    _user_id                        integer,
    _login_id                       bigint,
    _value_date                     date,
    _store_id                       integer,
    _party_code                     national character varying(12),
    _price_type_id                  integer,
    _reference_number               national character varying(24),
    _statement_reference            text,
    _details                        transactions.stock_detail_type[],
    _attachments                    core.attachment_type[]
)
RETURNS bigint
AS
$$
    DECLARE _party_id               bigint;
    DECLARE _cost_center_id         bigint;
    DECLARE _tran_master_id         bigint;
    DECLARE _tran_counter           integer;
    DECLARE _tran_code              text;
    DECLARE _stock_master_id        bigint;
    DECLARE _grand_total            money_strict;
    DECLARE _discount_total         money_strict2;
    DECLARE _tax_total              money_strict2;
    DECLARE _is_credit              boolean;
    DECLARE _default_currency_code  national character varying(12);
    DECLARE _cost_of_goods_sold     money_strict2;
    DECLARE _sm_id                  bigint;
    DECLARE _is_non_taxable_sales   boolean;
    DECLARE this                    RECORD;
    DECLARE _shipping_address_code  national character varying(12);
BEGIN
    IF(policy.can_post_transaction(_login_id, _user_id, _office_id, 'Sales.Return', _value_date) = false) THEN
        RETURN 0;
    END IF;
    
    _party_id                       := core.get_party_id_by_party_code(_party_code);
    _default_currency_code          := transactions.get_default_currency_code_by_office_id(_office_id);
    
    SELECT cost_center_id   INTO _cost_center_id    FROM transactions.transaction_master WHERE transactions.transaction_master.transaction_master_id = _transaction_master_id;

    SELECT 
        is_credit,
        non_taxable,
        core.get_shipping_address_code_by_shipping_address_id(shipping_address_id),
        stock_master_id
    INTO 
        _is_credit,
        _is_non_taxable_sales,
        _shipping_address_code,
        _sm_id
    FROM transactions.stock_master 
    WHERE transaction_master_id = _transaction_master_id;

    CREATE TEMPORARY TABLE temp_stock_details
    (
        id                              SERIAL PRIMARY KEY,
        stock_master_id                 bigint, 
        tran_type                       transaction_type, 
        store_id                        integer,
        item_code                       text,
        item_id                         integer, 
        quantity                        integer_strict,
        unit_name                       text,
        unit_id                         integer,
        base_quantity                   decimal,
        base_unit_id                    integer,                
        price                           money_strict,
        cost_of_goods_sold              money_strict2 DEFAULT(0),
        discount                        money_strict2,
        shipping_charge                 money_strict2,
        tax_form                        text,
        sales_tax_id                    integer,
        tax                             money_strict2,
        sales_account_id                integer,
        sales_discount_account_id       integer,
        sales_return_account_id         integer,
        inventory_account_id            integer,
        cost_of_goods_sold_account_id   integer        
    ) ON COMMIT DROP;

    CREATE TEMPORARY TABLE temp_stock_tax_details
    (
        id                                      SERIAL,
        temp_stock_detail_id                    integer REFERENCES temp_stock_details(id),
        sales_tax_detail_code                   text,
        stock_detail_id                         bigint,
        sales_tax_detail_id                     integer,
        state_sales_tax_id                      integer,
        county_sales_tax_id                     integer,
        account_id                              integer,
        principal                               money_strict,
        rate                                    decimal_strict,
        tax                                     money_strict
    ) ON COMMIT DROP;

    INSERT INTO temp_stock_details(store_id, item_code, quantity, unit_name, price, discount, shipping_charge, tax_form, tax)
    SELECT store_id, item_code, quantity, unit_name, price, discount, shipping_charge, tax_form, tax
    FROM explode_array(_details);

    UPDATE temp_stock_details 
    SET
        tran_type                   = 'Dr',
        sales_tax_id                = core.get_sales_tax_id_by_sales_tax_code(tax_form),
        item_id                     = core.get_item_id_by_item_code(item_code),
        unit_id                     = core.get_unit_id_by_unit_name(unit_name),
        base_quantity               = core.get_base_quantity_by_unit_name(unit_name, quantity),
        base_unit_id                = core.get_base_unit_id_by_unit_name(unit_name);

    UPDATE temp_stock_details
    SET
        sales_account_id                = core.get_sales_account_id(item_id),
        sales_discount_account_id       = core.get_sales_discount_account_id(item_id),
        sales_return_account_id         = core.get_sales_return_account_id(item_id),        
        inventory_account_id            = core.get_inventory_account_id(item_id),
        cost_of_goods_sold_account_id   = core.get_cost_of_goods_sold_account_id(item_id);
    
    IF EXISTS
    (

        SELECT * 
        FROM transactions.stock_details
        INNER JOIN temp_stock_details
        ON temp_stock_details.item_id = transactions.stock_details.item_id
        WHERE transactions.stock_details.stock_master_id = _sm_id
        AND COALESCE(temp_stock_details.sales_tax_id, 0) != COALESCE(transactions.stock_details.sales_tax_id, 0)
        LIMIT 1
    ) THEN
        RAISE EXCEPTION 'Tax form mismatch.'
        USING ERRCODE='P3202';
    END IF;

    IF EXISTS
    (
            SELECT 1 FROM temp_stock_details AS details
            WHERE core.is_valid_unit_id(details.unit_id, details.item_id) = false
            LIMIT 1
    ) THEN
        RAISE EXCEPTION 'Item/unit mismatch.'
        USING ERRCODE='P3201';
    END IF;

    IF(_is_non_taxable_sales) THEN
        IF EXISTS(SELECT * FROM temp_stock_details WHERE sales_tax_id IS NOT NULL LIMIT 1) THEN
            RAISE EXCEPTION 'You cannot provide sales tax information for non taxable sales.'
            USING ERRCODE='P5110';
        END IF;
    END IF;

    FOR this IN SELECT * FROM temp_stock_details
    LOOP
        PERFORM FROM transactions.validate_item_for_return(_transaction_master_id, this.store_id, this.item_code, this.unit_name, this.quantity, this.price);
    END LOOP;

    FOR this IN SELECT * FROM temp_stock_details ORDER BY id
    LOOP
        INSERT INTO temp_stock_tax_details
        (
            temp_stock_detail_id,
            sales_tax_detail_code,
            account_id, 
            sales_tax_detail_id, 
            state_sales_tax_id, 
            county_sales_tax_id, 
            principal, 
            rate, 
            tax
        )
        SELECT 
            this.id, 
            sales_tax_detail_code,
            account_id, 
            sales_tax_detail_id, 
            state_sales_tax_id, 
            county_sales_tax_id, 
            taxable_amount, 
            rate, 
            tax
        FROM transactions.get_sales_tax('Sales', _store_id, _party_code, _shipping_address_code, _price_type_id, this.item_code, this.price, this.quantity, this.discount, this.shipping_charge, this.sales_tax_id);
    END LOOP;
    
    UPDATE temp_stock_details
    SET tax =
    (SELECT SUM(COALESCE(temp_stock_tax_details.tax, 0)) FROM temp_stock_tax_details
    WHERE temp_stock_tax_details.temp_stock_detail_id = temp_stock_details.id);

    _tran_master_id             := nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));
    _stock_master_id            := nextval(pg_get_serial_sequence('transactions.stock_master', 'stock_master_id'));
    _tran_counter               := transactions.get_new_transaction_counter(_value_date);
    _tran_code                  := transactions.get_transaction_code(_value_date, _office_id, _user_id, _login_id);

    INSERT INTO transactions.transaction_master(transaction_master_id, transaction_counter, transaction_code, book, value_date, user_id, login_id, office_id, cost_center_id, reference_number, statement_reference)
    SELECT _tran_master_id, _tran_counter, _tran_code, 'Sales.Return', _value_date, _user_id, _login_id, _office_id, _cost_center_id, _reference_number, _statement_reference;
        
    SELECT SUM(COALESCE(tax, 0))                                INTO _tax_total FROM temp_stock_tax_details;
    SELECT SUM(COALESCE(discount, 0))                           INTO _discount_total FROM temp_stock_details;
    SELECT SUM(COALESCE(price, 0) * COALESCE(quantity, 0))      INTO _grand_total FROM temp_stock_details;



    UPDATE temp_stock_details
    SET cost_of_goods_sold = transactions.get_write_off_cost_of_goods_sold(_sm_id, item_id, unit_id, quantity);


    SELECT SUM(cost_of_goods_sold) INTO _cost_of_goods_sold FROM temp_stock_details;


    IF(_cost_of_goods_sold > 0) THEN
        INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT _tran_master_id, _value_date, 'Dr', inventory_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0)), 1, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0))
        FROM temp_stock_details
        GROUP BY inventory_account_id;


        INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT _tran_master_id, _value_date, 'Cr', cost_of_goods_sold_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0)), 1, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0))
        FROM temp_stock_details
        GROUP BY cost_of_goods_sold_account_id;
    END IF;


    INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, local_currency_code, er,amount_in_local_currency) 
    SELECT _tran_master_id, _value_date, 'Dr', sales_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0)), _default_currency_code, 1, SUM(COALESCE(price, 0) * COALESCE(quantity, 0))
    FROM temp_stock_details
    GROUP BY sales_account_id;


    IF(_tax_total IS NOT NULL AND _tax_total > 0) THEN
        INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency)
        SELECT _tran_master_id, _value_date, 'Dr', temp_stock_tax_details.account_id, _statement_reference, _default_currency_code, SUM(COALESCE(tax, 0)), _default_currency_code, 1, SUM(COALESCE(tax, 0))
        FROM temp_stock_tax_details
        GROUP BY temp_stock_tax_details.account_id;
    END IF;

    IF(_discount_total IS NOT NULL AND _discount_total > 0) THEN
        INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency) 
        SELECT _tran_master_id, _value_date, 'Cr', sales_discount_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(discount, 0)), _default_currency_code, 1, SUM(COALESCE(discount, 0))
        FROM temp_stock_details
        GROUP BY sales_discount_account_id;
    END IF;

    IF(_is_credit) THEN
        INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency) 
        SELECT _tran_master_id, _value_date, 'Cr',  core.get_account_id_by_party_code(_party_code), _statement_reference, _default_currency_code, _grand_total + _tax_total - _discount_total, _default_currency_code, 1, _grand_total + _tax_total - _discount_total;
    ELSE
        INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency) 
        SELECT _tran_master_id, _value_date, 'Cr',  sales_return_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0)) + SUM(COALESCE(tax, 0)) - SUM(COALESCE(discount, 0)), _default_currency_code, 1, SUM(COALESCE(price, 0) * COALESCE(quantity, 0)) + SUM(COALESCE(tax, 0)) - SUM(COALESCE(discount, 0))
        FROM temp_stock_details
        GROUP BY sales_return_account_id;
    END IF;



    INSERT INTO transactions.stock_master(stock_master_id, value_date, transaction_master_id, party_id, price_type_id, is_credit, store_id) 
    SELECT _stock_master_id, _value_date, _tran_master_id, _party_id, _price_type_id, false, _store_id;


    INSERT INTO transactions.stock_details(value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, sales_tax_id, tax)
    SELECT _value_date, _stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, sales_tax_id, tax FROM temp_stock_details;

    INSERT INTO transactions.stock_return(transaction_master_id, return_transaction_master_id)
    SELECT _transaction_master_id, _tran_master_id;

    PERFORM transactions.auto_verify(_transaction_master_id, _office_id);
    RETURN _tran_master_id;
END
$$
LANGUAGE plpgsql;




-- CREATE TEMPORARY TABLE temp_sales_return
-- ON COMMIT DROP
-- AS
-- 
-- SELECT * FROM transactions.post_sales_return(5, 2, 2, 1, '1-1-2000', 1, 'MAJON-0002', 1, '1234-AD', 'Test', 
-- ARRAY[
--  ROW(1, 'RMBP', 1, 'Piece', 180000, 0, 200, 'MoF-NY-BK-STX', 0)::transactions.stock_detail_type,
--  ROW(1, '13MBA', 1, 'Piece', 110000, 5000, 50, 'MoF-NY-BK-STX', 0)::transactions.stock_detail_type
-- ],
-- ARRAY[
-- NULL::core.attachment_type
-- ]);
-- 
-- SELECT  tran_type, core.get_account_name_by_account_id(account_id), amount_in_local_currency 
-- FROM transactions.transaction_details
-- WHERE transaction_master_id  = (SELECT * FROM temp_sales_return);


/**************************************************************************************************************************
--------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------
'########::'##:::::::'########:::'######:::'##::::'##:'##::: ##:'####:'########::::'########:'########::'######::'########:
 ##.... ##: ##::::::: ##.... ##:'##... ##:: ##:::: ##: ###:: ##:. ##::... ##..:::::... ##..:: ##.....::'##... ##:... ##..::
 ##:::: ##: ##::::::: ##:::: ##: ##:::..::: ##:::: ##: ####: ##:: ##::::: ##:::::::::: ##:::: ##::::::: ##:::..::::: ##::::
 ########:: ##::::::: ########:: ##::'####: ##:::: ##: ## ## ##:: ##::::: ##:::::::::: ##:::: ######:::. ######::::: ##::::
 ##.....::: ##::::::: ##.....::: ##::: ##:: ##:::: ##: ##. ####:: ##::::: ##:::::::::: ##:::: ##...:::::..... ##:::: ##::::
 ##:::::::: ##::::::: ##:::::::: ##::: ##:: ##:::: ##: ##:. ###:: ##::::: ##:::::::::: ##:::: ##:::::::'##::: ##:::: ##::::
 ##:::::::: ########: ##::::::::. ######:::. #######:: ##::. ##:'####:::: ##:::::::::: ##:::: ########:. ######::::: ##::::
..:::::::::........::..::::::::::......:::::.......:::..::::..::....:::::..:::::::::::..:::::........:::......::::::..:::::
--------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------
**************************************************************************************************************************/




-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/transactions/transactions.refresh_materialized_views.sql --<--<--
DROP FUNCTION IF EXISTS transactions.refresh_materialized_views(_office_id integer);
DROP FUNCTION IF EXISTS transactions.refresh_materialized_views(_user_id integer, _login_id bigint, _office_id integer, _value_date date);

CREATE FUNCTION transactions.refresh_materialized_views(_user_id integer, _login_id bigint, _office_id integer, _value_date date)
RETURNS void
AS
$$
BEGIN
    REFRESH MATERIALIZED VIEW transactions.trial_balance_view;
    REFRESH MATERIALIZED VIEW transactions.verified_stock_transaction_view;
    REFRESH MATERIALIZED VIEW transactions.verified_transaction_mat_view;
    REFRESH MATERIALIZED VIEW transactions.verified_cash_transaction_mat_view;
END
$$
LANGUAGE plpgsql;


SELECT transactions.create_routine('REF-MV', 'transactions.refresh_materialized_views', 1000);


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/transactions/transactions.validate_item_for_return.sql --<--<--
DROP FUNCTION IF EXISTS transactions.validate_item_for_return
(
    _transaction_master_id  bigint, 
    _store_id               integer, 
    _item_code              national character varying(12), 
    _unit_name              national character varying(50), 
    _quantity               integer, 
    _price                  public.money_strict
);

CREATE FUNCTION transactions.validate_item_for_return
(
    _transaction_master_id  bigint, 
    _store_id               integer, 
    _item_code              national character varying(12), 
    _unit_name              national character varying(50), 
    _quantity               integer, 
    _price                  public.money_strict
)
RETURNS boolean
AS
$$
    DECLARE _stock_master_id                bigint = 0;
    DECLARE _is_purchase                    boolean = false;
    DECLARE _item_id                        integer = 0;
    DECLARE _unit_id                        integer = 0;
    DECLARE _actual_quantity                public.decimal_strict2 = 0;
    DECLARE _returned_in_previous_batch     public.decimal_strict2 = 0;
    DECLARE _in_verification_queue          public.decimal_strict2 = 0;
    DECLARE _actual_price_in_root_unit      public.money_strict2 = 0;
    DECLARE _price_in_root_unit             public.money_strict2 = 0;
    DECLARE _item_in_stock                  public.decimal_strict2 = 0;        
BEGIN        
    IF(_store_id IS NULL OR _store_id <= 0) THEN
        RAISE EXCEPTION 'Invalid store.'
        USING ERRCODE='P3012';
    END IF;


    IF(_item_code IS NULL OR trim(_item_code) = '') THEN
        RAISE EXCEPTION 'Invalid item.'
        USING ERRCODE='P3051';
    END IF;

    IF(_unit_name IS NULL OR trim(_unit_name) = '') THEN
        RAISE EXCEPTION 'Invalid unit.'
        USING ERRCODE='P3052';
    END IF;

    IF(_quantity IS NULL OR _quantity <= 0) THEN
        RAISE EXCEPTION 'Invalid quantity.'
        USING ERRCODE='P3301';
    END IF;


    IF NOT EXISTS
    (
        SELECT * FROM transactions.transaction_master
        WHERE transaction_master_id = _transaction_master_id
        AND verification_status_id > 0
    ) THEN
        RAISE EXCEPTION 'Invalid or rejected transaction.'
        USING ERRCODE='P5301';
    END IF;
    
    
    _stock_master_id                := transactions.get_stock_master_id_by_transaction_master_id(_transaction_master_id);

    IF(_stock_master_id  IS NULL OR _stock_master_id  <= 0) THEN
        RAISE EXCEPTION 'Invalid transaction id.'
        USING ERRCODE='P3302';
    END IF;

    _item_id                        := core.get_item_id_by_item_code(_item_code);
    IF(_item_id IS NULL OR _item_id <= 0) THEN
        RAISE EXCEPTION 'Invalid item.'
        USING ERRCODE='P3051';
    END IF;

    IF NOT EXISTS
    (
        SELECT * FROM transactions.stock_details
        WHERE stock_master_id = _stock_master_id
        AND item_id = _item_id
        LIMIT 1
    ) THEN
            RAISE EXCEPTION '%', format('The item %1$s is not associated with this transaction.', _item_code)
            USING ERRCODE='P4020';
    END IF;

    _unit_id                        := core.get_unit_id_by_unit_name(_unit_name);
    IF(_unit_id IS NULL OR _unit_id <= 0) THEN
        RAISE EXCEPTION 'Invalid unit.'
        USING ERRCODE='P3052';
    END IF;


    _is_purchase                    := transactions.is_purchase(_transaction_master_id);

    IF NOT EXISTS
    (
        SELECT * FROM transactions.stock_details
        WHERE stock_master_id = _stock_master_id
        AND item_id = _item_id
        AND core.get_root_unit_id(_unit_id) = core.get_root_unit_id(unit_id)
        LIMIT 1
    ) THEN
        RAISE EXCEPTION 'Invalid or incompatible unit specified'
        USING ERRCODE='P3053';
    END IF;

    IF(_is_purchase = true) THEN
        _item_in_stock = core.count_item_in_stock(_item_id, _unit_id, _store_id);

        IF(_item_in_stock < _quantity) THEN
                RAISE EXCEPTION '%', format('Only %1$s %2$s of %3$s left in stock.',_item_in_stock, _unit_name, _item_code)
                USING ERRCODE='P5500';
        END IF;
    END IF;

    SELECT 
            COALESCE(core.convert_unit(base_unit_id, _unit_id) * base_quantity, 0)
            INTO _actual_quantity
    FROM transactions.stock_details
    WHERE stock_master_id = _stock_master_id
    AND item_id = _item_id;


    SELECT 
        COALESCE(SUM(core.convert_unit(base_unit_id, 1) * base_quantity), 0)
        INTO _returned_in_previous_batch
    FROM transactions.stock_details
    WHERE stock_master_id IN
    (
        SELECT stock_master_id
        FROM transactions.stock_master
        INNER JOIN transactions.transaction_master
        ON transactions.transaction_master.transaction_master_id = transactions.stock_master.transaction_master_id
        WHERE transactions.transaction_master.verification_status_id > 0
        AND transactions.stock_master.transaction_master_id IN 
        (
            SELECT 
            return_transaction_master_id 
            FROM transactions.stock_return
            WHERE transaction_master_id = _transaction_master_id
        )
    )
    AND item_id = _item_id;

    SELECT 
        COALESCE(SUM(core.convert_unit(base_unit_id, 1) * base_quantity), 0)
        INTO _in_verification_queue
    FROM transactions.stock_details
    WHERE stock_master_id IN
    (
        SELECT stock_master_id
        FROM transactions.stock_master
        INNER JOIN transactions.transaction_master
        ON transactions.transaction_master.transaction_master_id = transactions.stock_master.transaction_master_id
        WHERE transactions.transaction_master.verification_status_id = 0
        AND transactions.stock_master.transaction_master_id IN 
        (
            SELECT 
            return_transaction_master_id 
            FROM transactions.stock_return
            WHERE transaction_master_id = _transaction_master_id
        )
    )
    AND item_id = _item_id;

    IF(_quantity + _returned_in_previous_batch + _in_verification_queue > _actual_quantity) THEN
        RAISE EXCEPTION 'The returned quantity cannot be greater than actual quantity.'
        USING ERRCODE='P5203';
    END IF;

    _price_in_root_unit := core.convert_unit(core.get_root_unit_id(_unit_id), _unit_id) * _price;

    SELECT 
            (core.convert_unit(core.get_root_unit_id(transactions.stock_details.unit_id), transactions.stock_details.base_unit_id) * price) / (base_quantity/quantity)
            INTO _actual_price_in_root_unit
    FROM transactions.stock_details
    WHERE stock_master_id = _stock_master_id
    AND item_id = _item_id;


    IF(_price_in_root_unit > _actual_price_in_root_unit) THEN
        RAISE EXCEPTION 'The returned amount cannot be greater than actual amount.'
        USING ERRCODE='P5204';

        RETURN FALSE;
    END IF;

    RETURN TRUE;
END
$$
LANGUAGE plpgsql;

--SELECT * FROM transactions.validate_item_for_return(121, 5, 'RMBP', 'Piece', 4, 180000);

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/transactions/transactions.verify_transaction.sql --<--<--
-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/FrontEnd/MixERP.Net.FrontEnd/db/src/02. functions and logic/logic/functions/transactions/transactions.verify_transaction.sql --<--<--
DROP FUNCTION IF EXISTS transactions.verify_transaction
(
    _transaction_master_id                  bigint,
    _office_id                              integer,
    _user_id                                integer,
    _login_id                               bigint,
    _verification_status_id                 smallint,
    _reason                                 national character varying
) 
CASCADE;

CREATE FUNCTION transactions.verify_transaction
(
    _transaction_master_id                  bigint,
    _office_id                              integer,
    _user_id                                integer,
    _login_id                               bigint,
    _verification_status_id                 smallint,
    _reason                                 national character varying
)
RETURNS VOID
VOLATILE
AS
$$
    DECLARE _transaction_posted_by          integer;
    DECLARE _can_approve                    boolean=true;
    DECLARE _book                           text;
    DECLARE _verify_sales                   boolean;
    DECLARE _sales_verification_limit       public.money_strict2;
    DECLARE _verify_purchase                boolean;
    DECLARE _purchase_verification_limit    public.money_strict2;
    DECLARE _verify_gl                      boolean;
    DECLARE _gl_verification_limit          public.money_strict2;
    DECLARE _posted_amount                  public.money_strict2;
    DECLARE _has_policy                     boolean=false;
    DECLARE _voucher_date                   date;
    DECLARE _voucher_office_id              integer;
    DECLARE _value_date                     date=transactions.get_value_date(_office_id);
BEGIN

    SELECT
        transactions.transaction_master.book,
        transactions.transaction_master.value_date,
        transactions.transaction_master.office_id,
        transactions.transaction_master.user_id
    INTO
        _book,
        _voucher_date,
        _voucher_office_id,
        _transaction_posted_by  
    FROM
    transactions.transaction_master
    WHERE transactions.transaction_master.transaction_master_id=_transaction_master_id;


    IF(_voucher_office_id <> _office_id) THEN
        RAISE EXCEPTION 'Access is denied. You cannot verify a transaction of another office.'
        USING ERRCODE='P9014';
    END IF;
    
    IF(_voucher_date <> _value_date) THEN
        RAISE EXCEPTION 'Access is denied. You cannot verify past or futuer dated transaction.'
        USING ERRCODE='P9015';
    END IF;
    
    SELECT
        SUM(amount_in_local_currency)
    INTO
        _posted_amount
    FROM
        transactions.transaction_details
    WHERE transactions.transaction_details.transaction_master_id = _transaction_master_id
    AND transactions.transaction_details.tran_type='Cr';


    SELECT
        true,
        can_verify_sales_transactions,
        sales_verification_limit,
        can_verify_purchase_transactions,
        purchase_verification_limit,
        can_verify_gl_transactions,
        gl_verification_limit
    INTO
        _has_policy,
        _verify_sales,
        _sales_verification_limit,
        _verify_purchase,
        _purchase_verification_limit,
        _verify_gl,
        _gl_verification_limit
    FROM
    policy.voucher_verification_policy
    WHERE user_id=_user_id
    AND is_active=true
    AND now() >= effective_from
    AND now() <= ends_on;


    IF(lower(_book) LIKE 'sales%') THEN
        IF(_verify_sales = false) THEN
            _can_approve := false;
        END IF;
        IF(_verify_sales = true) THEN
            IF(_posted_amount > _sales_verification_limit AND _sales_verification_limit > 0::public.money_strict2) THEN
                _can_approve := false;
            END IF;
        END IF;         
    END IF;


    IF(lower(_book) LIKE 'purchase%') THEN
        IF(_verify_purchase = false) THEN
            _can_approve := false;
        END IF;
        IF(_verify_purchase = true) THEN
            IF(_posted_amount > _purchase_verification_limit AND _purchase_verification_limit > 0::public.money_strict2) THEN
                _can_approve := false;
            END IF;
        END IF;         
    END IF;


    IF(lower(_book) LIKE 'journal%') THEN
        IF(_verify_gl = false) THEN
            _can_approve := false;
        END IF;
        IF(_verify_gl = true) THEN
            IF(_posted_amount > _gl_verification_limit AND _gl_verification_limit > 0::public.money_strict2) THEN
                _can_approve := false;
            END IF;
        END IF;         
    END IF;

    IF(_has_policy=true) THEN
        IF(_can_approve = true) THEN
            UPDATE transactions.transaction_master
            SET 
                last_verified_on = now(),
                verified_by_user_id=_user_id,
                verification_status_id=_verification_status_id,
                verification_reason=_reason
            WHERE
                transactions.transaction_master.transaction_master_id=_transaction_master_id;

            PERFORM transactions.create_recurring_invoices(_transaction_master_id);

            RAISE NOTICE 'Done.';
        END IF;
    ELSE
        RAISE EXCEPTION 'No verification policy found for this user.'
        USING ERRCODE='P4030';
    END IF;
    RETURN;
END
$$
LANGUAGE plpgsql;


--SELECT * FROM transactions.verify_transaction(65::bigint, 2, 2, 51::bigint, -3::smallint, '');

/**************************************************************************************************************************
--------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------
'########::'##:::::::'########:::'######:::'##::::'##:'##::: ##:'####:'########::::'########:'########::'######::'########:
 ##.... ##: ##::::::: ##.... ##:'##... ##:: ##:::: ##: ###:: ##:. ##::... ##..:::::... ##..:: ##.....::'##... ##:... ##..::
 ##:::: ##: ##::::::: ##:::: ##: ##:::..::: ##:::: ##: ####: ##:: ##::::: ##:::::::::: ##:::: ##::::::: ##:::..::::: ##::::
 ########:: ##::::::: ########:: ##::'####: ##:::: ##: ## ## ##:: ##::::: ##:::::::::: ##:::: ######:::. ######::::: ##::::
 ##.....::: ##::::::: ##.....::: ##::: ##:: ##:::: ##: ##. ####:: ##::::: ##:::::::::: ##:::: ##...:::::..... ##:::: ##::::
 ##:::::::: ##::::::: ##:::::::: ##::: ##:: ##:::: ##: ##:. ###:: ##::::: ##:::::::::: ##:::: ##:::::::'##::: ##:::: ##::::
 ##:::::::: ########: ##::::::::. ######:::. #######:: ##::. ##:'####:::: ##:::::::::: ##:::: ########:. ######::::: ##::::
..:::::::::........::..::::::::::......:::::.......:::..::::..::....:::::..:::::::::::..:::::........:::......::::::..:::::
--------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------
**************************************************************************************************************************/

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/triggers/transactions.verification_trigger.sql --<--<--
DROP FUNCTION IF EXISTS transactions.verification_trigger() CASCADE;
CREATE FUNCTION transactions.verification_trigger()
RETURNS TRIGGER
AS
$$
    DECLARE _transaction_master_id              bigint;
    DECLARE _transaction_posted_by              integer;
    DECLARE _old_verifier                       integer;
    DECLARE _old_status                         integer;
    DECLARE _old_reason                         national character varying(128);
    DECLARE _verifier                           integer;
    DECLARE _status                             integer;
    DECLARE _reason                             national character varying(128);
    DECLARE _has_policy                         boolean;
    DECLARE _is_sys                             boolean;
    DECLARE _rejected                           smallint=-3;
    DECLARE _closed                             smallint=-2;
    DECLARE _withdrawn                          smallint=-1;
    DECLARE _unapproved                         smallint = 0;
    DECLARE _auto_approved                      smallint = 1;
    DECLARE _approved                           smallint=2;
    DECLARE _book                               text;
    DECLARE _can_verify_sales_transactions      boolean;
    DECLARE _sales_verification_limit           public.money_strict2;
    DECLARE _can_verify_purchase_transactions   boolean;
    DECLARE _purchase_verification_limit        public.money_strict2;
    DECLARE _can_verify_gl_transactions         boolean;
    DECLARE _gl_verification_limit              public.money_strict2;
    DECLARE _can_verify_self                    boolean;
    DECLARE _self_verification_limit            public.money_strict2;
    DECLARE _posted_amount                      public.money_strict2;
    DECLARE _voucher_date                       date;
    DECLARE _value_date                         date;
BEGIN
    IF TG_OP='DELETE' THEN
        RAISE EXCEPTION 'Deleting a transaction is not allowed. Mark the transaction as rejected instead.'
        USING ERRCODE='P5800';
    END IF;

    IF TG_OP='UPDATE' THEN
        RAISE NOTICE 'Columns except the following will be ignored for this update: %', 'verified_by_user_id, verification_status_id, verification_reason.';

        IF(OLD.transaction_master_id IS DISTINCT FROM NEW.transaction_master_id) THEN
            RAISE EXCEPTION 'Cannot update the column %', '"transaction_master_id".'
            USING ERRCODE='P8502';
        END IF;

        IF(OLD.transaction_counter IS DISTINCT FROM NEW.transaction_counter) THEN
            RAISE EXCEPTION 'Cannot update the column %', '"transaction_counter".'
            USING ERRCODE='P8502';            
        END IF;

        IF(OLD.transaction_code IS DISTINCT FROM NEW.transaction_code) THEN
            RAISE EXCEPTION 'Cannot update the column %', '"transaction_code".'
            USING ERRCODE='P8502';
        END IF;

        IF(OLD.book IS DISTINCT FROM NEW.book) THEN
            RAISE EXCEPTION 'Cannot update the column %', '"book".'
            USING ERRCODE='P8502';
        END IF;

        IF(OLD.value_date IS DISTINCT FROM NEW.value_date) THEN
            RAISE EXCEPTION 'Cannot update the column %', '"value_date".'
            USING ERRCODE='P8502';
        END IF;

        IF(OLD.transaction_ts IS DISTINCT FROM NEW.transaction_ts) THEN
            RAISE EXCEPTION 'Cannot update the column %', '"transaction_ts".'
            USING ERRCODE='P8502';
        END IF;

        IF(OLD.login_id IS DISTINCT FROM NEW.login_id) THEN
            RAISE EXCEPTION 'Cannot update the column %', '"login_id".'
            USING ERRCODE='P8502';
        END IF;

        IF(OLD.user_id IS DISTINCT FROM NEW.user_id) THEN
            RAISE EXCEPTION 'Cannot update the column %', '"user_id".'
            USING ERRCODE='P8502';
        END IF;

        IF(OLD.sys_user_id IS DISTINCT FROM NEW.sys_user_id) THEN
            RAISE EXCEPTION 'Cannot update the column %', '"sys_user_id".'
            USING ERRCODE='P8502';
        END IF;

        IF(OLD.office_id IS DISTINCT FROM NEW.office_id) THEN
            RAISE EXCEPTION 'Cannot update the column %', '"office_id".'
            USING ERRCODE='P8502';
        END IF;

        IF(OLD.cost_center_id IS DISTINCT FROM NEW.cost_center_id) THEN
            RAISE EXCEPTION 'Cannot update the column %', '"cost_center_id".'
            USING ERRCODE='P8502';
        END IF;

        _transaction_master_id  := OLD.transaction_master_id;
        _book                   := OLD.book;
        _old_verifier           := OLD.verified_by_user_id;
        _old_status             := OLD.verification_status_id;
        _old_reason             := OLD.verification_reason;
        _transaction_posted_by  := OLD.user_id;      
        _verifier               := NEW.verified_by_user_id;
        _status                 := NEW.verification_status_id;
        _reason                 := NEW.verification_reason;
        _is_sys                 := office.is_sys(_verifier);
        _voucher_date           := NEW.value_date;
        _value_date             := transactions.get_value_date(NEW.office_id);
        
        SELECT
            SUM(amount_in_local_currency)
        INTO
            _posted_amount
        FROM
            transactions.transaction_details
        WHERE transactions.transaction_details.transaction_master_id = _transaction_master_id
        AND transactions.transaction_details.tran_type='Cr';


        SELECT
            true,
            can_verify_sales_transactions,
            sales_verification_limit,
            can_verify_purchase_transactions,
            purchase_verification_limit,
            can_verify_gl_transactions,
            gl_verification_limit,
            can_self_verify,
            self_verification_limit
        INTO
            _has_policy,
            _can_verify_sales_transactions,
            _sales_verification_limit,
            _can_verify_purchase_transactions,
            _purchase_verification_limit,
            _can_verify_gl_transactions,
            _gl_verification_limit,
            _can_verify_self,
            _self_verification_limit
        FROM
        policy.voucher_verification_policy
        WHERE user_id=_verifier
        AND is_active=true
        AND now() >= effective_from
        AND now() <= ends_on;

        IF(_verifier IS NULL) THEN
            RAISE EXCEPTION 'Access is denied.'
            USING ERRCODE='P9001';
        END IF;     
        
        IF(_status != _withdrawn AND _has_policy = false) THEN
            RAISE EXCEPTION 'Access is denied. You don''t have the right to verify the transaction.'
            USING ERRCODE='P9016';
        END IF;

        IF(_voucher_date < _value_date) THEN
            RAISE EXCEPTION 'Access is denied. You don''t have the right to withdraw the transaction.'
            USING ERRCODE='P9017';
        END IF;

        IF(_status = _withdrawn AND _has_policy = false) THEN
            IF(_transaction_posted_by != _verifier) THEN
                RAISE EXCEPTION 'Access is denied. You don''t have the right to withdraw the transaction.'
                USING ERRCODE='P9017';
            END IF;
        END IF;

        IF(_status = _auto_approved AND _is_sys = false) THEN
            RAISE EXCEPTION 'Access is denied.'
            USING ERRCODE='P9001';
        END IF;


        IF(_has_policy = false) THEN
            RAISE EXCEPTION 'Access is denied.'
            USING ERRCODE='P9001';
        END IF;


        --Is trying verify self transaction.
        IF(NEW.verified_by_user_id = NEW.user_id) THEN
            IF(_can_verify_self = false) THEN
                RAISE EXCEPTION 'Please ask someone else to verify the transaction you posted.'
                USING ERRCODE='P5901';                
            END IF;
            IF(_can_verify_self = true) THEN
                IF(_posted_amount > _self_verification_limit AND _self_verification_limit > 0::public.money_strict2) THEN
                    RAISE EXCEPTION 'Self verification limit exceeded. The transaction was not verified.'
                    USING ERRCODE='P5910';
                END IF;
            END IF;
        END IF;

        IF(lower(_book) LIKE '%sales%') THEN
            IF(_can_verify_sales_transactions = false) THEN
                RAISE EXCEPTION 'Access is denied.'
                USING ERRCODE='P9001';
            END IF;
            IF(_can_verify_sales_transactions = true) THEN
                IF(_posted_amount > _sales_verification_limit AND _sales_verification_limit > 0::public.money_strict2) THEN
                    RAISE EXCEPTION 'Sales verification limit exceeded. The transaction was not verified.'
                    USING ERRCODE='P5911';
                END IF;
            END IF;         
        END IF;


        IF(lower(_book) LIKE '%purchase%') THEN
            IF(_can_verify_purchase_transactions = false) THEN
                RAISE EXCEPTION 'Access is denied.'
                USING ERRCODE='P9001';
            END IF;
            IF(_can_verify_purchase_transactions = true) THEN
                IF(_posted_amount > _purchase_verification_limit AND _purchase_verification_limit > 0::public.money_strict2) THEN
                    RAISE EXCEPTION 'Purchase verification limit exceeded. The transaction was not verified.'
                    USING ERRCODE='P5912';
                END IF;
            END IF;         
        END IF;


        IF(lower(_book) LIKE 'journal%') THEN
            IF(_can_verify_gl_transactions = false) THEN
                RAISE EXCEPTION 'Access is denied.'
                USING ERRCODE='P9001';
            END IF;
            IF(_can_verify_gl_transactions = true) THEN
                IF(_posted_amount > _gl_verification_limit AND _gl_verification_limit > 0::public.money_strict2) THEN
                    RAISE EXCEPTION 'GL verification limit exceeded. The transaction was not verified.'
                    USING ERRCODE='P5913';
                END IF;
            END IF;         
        END IF;

        NEW.last_verified_on := now();

    END IF; 
    RETURN NEW;
END
$$
LANGUAGE plpgsql;


CREATE TRIGGER verification_update_trigger
AFTER UPDATE
ON transactions.transaction_master
FOR EACH ROW 
EXECUTE PROCEDURE transactions.verification_trigger();

CREATE TRIGGER verification_delete_trigger
BEFORE DELETE
ON transactions.transaction_master
FOR EACH ROW 
EXECUTE PROCEDURE transactions.verification_trigger();


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/office/office.create_user.sql --<--<--
DROP FUNCTION IF EXISTS office.create_user
(
    _role_id		integer,
    _department_id	integer,
    _office_id		integer,
    _user_name 		text,
    _password 		text,
    _full_name 		text,
    _elevated 		boolean
);

CREATE FUNCTION office.create_user
(
    _role_id		integer,
    _department_id	integer,
    _office_id		integer,
    _user_name 		text,
    _password 		text,
    _full_name 		text,
    _elevated 		boolean = false
)
RETURNS VOID
AS
$$
BEGIN
    IF(COALESCE(_user_name, '')) THEN
        RETURN;
    END IF;

    INSERT INTO office.users(role_id, department_id, office_id, user_name, password, full_name, elevated)
    SELECT _role_id, _department_id, _office_id, _user_name, _password, _full_name, _elevated;
    RETURN;
END
$$
LANGUAGE plpgsql;


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/office/office.get_department_id_by_department_code.sql --<--<--
DROP FUNCTION IF EXISTS office.get_department_id_by_code(text);

DROP FUNCTION IF EXISTS office.get_department_id_by_department_code(text);

CREATE FUNCTION office.get_department_id_by_department_code(text)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN department_id
    FROM office.departments
    WHERE department_code=$1;
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/04.default-values/01.frequencies-payment-terms-late-fee.sql.sql --<--<--
UPDATE core.frequency_setups
SET frequency_setup_code= 'Sep-Oct'
WHERE frequency_setup_code= 'Sep-Oc';

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/04.default-values/http-actions.sql --<--<--
--This table should not be localized.
WITH http_actions
AS
(
SELECT 'GET' as code UNION ALL
SELECT 'PUT' UNION ALL
SELECT 'POST' UNION ALL
SELECT 'DELETE'
)

INSERT INTO policy.http_actions
SELECT * FROM http_actions
WHERE code NOT IN
(
    SELECT http_action_code FROM policy.http_actions
    WHERE http_action_code IN('GET','PUT','POST','DELETE')
);


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/04.default-values/recurrence-types.sql --<--<--
WITH recurrence_types
AS
(
SELECT 'FRE' AS code,   'Frequency' AS name,    true AS is_freq UNION ALL
SELECT 'DUR',           'Duration',             false
)

INSERT INTO core.recurrence_types(recurrence_type_code, recurrence_type_name, is_frequency)
SELECT * FROM recurrence_types
WHERE code NOT IN
(
    SELECT recurrence_type_code FROM core.recurrence_types
    WHERE recurrence_type_code IN('FRE','DUR')
);

UPDATE core.recurring_invoices 
SET recurrence_type_id = core.get_recurrence_type_id_by_recurrence_type_code('FRE')
WHERE recurrence_type_id IS NULL;

ALTER TABLE core.recurring_invoices
ALTER COLUMN recurrence_type_id SET NOT NULL;

ALTER TABLE core.recurring_invoices
ALTER COLUMN recurring_frequency_id DROP NOT NULL;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/05.scrud-views/core/core.recurring_invoice_scrud_view.sql --<--<--
DROP VIEW IF EXISTS core.recurring_invoice_scrud_view;

CREATE VIEW core.recurring_invoice_scrud_view
AS
SELECT 
  core.recurring_invoices.recurring_invoice_id, 
  core.recurring_invoices.recurring_invoice_code, 
  core.recurring_invoices.recurring_invoice_name,
  core.items.item_code || '('|| core.items.item_name||')' AS item,
  core.frequencies.frequency_code || '('|| core.frequencies.frequency_name||')' AS recurring_frequency,
  core.recurring_invoices.recurring_amount, 
  core.recurring_invoices.auto_trigger_on_sales
FROM 
  core.recurring_invoices
INNER JOIN core.items 
ON core.recurring_invoices.item_id = core.items.item_id
LEFT JOIN core.frequencies
ON core.recurring_invoices.recurring_frequency_id = core.frequencies.frequency_id;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/05.scrud-views/core/core.recurring_invoice_setup_scrud_view.sql --<--<--
DROP VIEW IF EXISTS core.recurring_invoice_setup_scrud_view;
CREATE VIEW core.recurring_invoice_setup_scrud_view
AS
SELECT 
  core.recurring_invoice_setup.recurring_invoice_setup_id, 
  core.recurring_invoices.recurring_invoice_code || ' (' || core.recurring_invoices.recurring_invoice_name || ')' AS recurring_invoice,
  core.parties.party_code || ' (' || core.parties.party_name || ')' AS party,
  core.recurring_invoice_setup.starts_from, 
  core.recurring_invoice_setup.ends_on, 
  core.recurring_invoice_setup.recurring_amount, 
  core.payment_terms.payment_term_code || ' (' || core.payment_terms.payment_term_name || ')' AS payment_term
FROM 
  core.recurring_invoice_setup
INNER JOIN core.recurring_invoices
ON core.recurring_invoice_setup.recurring_invoice_id = core.recurring_invoices.recurring_invoice_id
INNER JOIN core.parties ON 
core.recurring_invoice_setup.party_id = core.parties.party_id
INNER JOIN core.payment_terms ON 
core.recurring_invoice_setup.payment_term_id = core.payment_terms.payment_term_id;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/06.sample-data/0.menus.sql --<--<--
SELECT * FROM core.create_menu('API Access Policy', '~/Modules/BackOffice/Policy/ApiAccess.mix', 'SAA', 2, core.get_menu_id('SPM'));

--FRENCH
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'fr', 'API Stratégie d''accès');


--GERMAN
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'de', 'API-Richtlinien');

--RUSSIAN
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'ru', 'Политика доступа API');

--JAPANESE
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'ja', 'APIのアクセスポリシー');

--SPANISH
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'es', 'API Política de Acceso');

--DUTCH
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'nl', 'API Access Policy');

--SIMPLIFIED CHINESE
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'zh', 'API访问策略');

--PORTUGUESE
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'pt', 'Política de Acesso API');

--SWEDISH
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'sv', 'API Access Policy');

--MALAY
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'ms', 'Dasar Akses API');

--INDONESIAN
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'id', 'API Kebijakan Access');

--FILIPINO
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'fil', 'API Patakaran sa Pag-access');

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/06.sample-data/1.remove-obsolete-menus.sql --<--<--
DELETE FROM core.menu_locale
WHERE menu_id = core.get_menu_id_by_menu_code('TRA');

DELETE FROM policy.menu_access
WHERE menu_id = core.get_menu_id_by_menu_code('TRA');

DELETE FROM core.menus
WHERE menu_code = 'TRA';


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests/core/parties/unit_tests.check_party_currency_code_mismatch.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.check_party_currency_code_mismatch();

CREATE FUNCTION unit_tests.check_party_currency_code_mismatch()
RETURNS public.test_result
AS
$$
    DECLARE message public.test_result;
BEGIN
    IF EXISTS
    (
        SELECT party_code FROM core.parties
        INNER JOIN core.accounts
        ON core.parties.account_id = core.accounts.account_id
        WHERE core.parties.currency_code != core.accounts.currency_code
        LIMIT 1
    ) THEN
        SELECT assert.fail('Some party accounts have different currency setup on their mapped GL heads.') INTO message;
        RETURN message;
    END IF;

    SELECT assert.ok('End of test.') INTO message;  
    RETURN message;
END
$$
LANGUAGE plpgsql;



-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests/core/parties/unit_tests.check_party_null_account_id.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.check_party_null_account_id();

CREATE FUNCTION unit_tests.check_party_null_account_id()
RETURNS public.test_result
AS
$$
    DECLARE message public.test_result;
BEGIN
    IF EXISTS
    (
        SELECT party_code FROM core.parties
        WHERE core.parties.account_id IS NULL
        LIMIT 1
    ) THEN
        SELECT assert.fail('Some party accounts don''t have mapped GL heads.') INTO message;
        RETURN message;
    END IF;

    SELECT assert.ok('End of test.') INTO message;  
    RETURN message;
END
$$
LANGUAGE plpgsql;



-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests/core/parties/unit_tests.test_transactions_post_receipt_function.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.test_transactions_post_receipt_function();

CREATE FUNCTION unit_tests.test_transactions_post_receipt_function()
RETURNS public.test_result
AS
$$
    DECLARE message                 public.test_result;
    DECLARE _user_id                integer;
    DECLARE _office_id              integer; 
    DECLARE _login_id               bigint;
    DECLARE _party_code             national character varying(12); 
    DECLARE _currency_code          national character varying(12); 
    DECLARE _amount                 public.money_strict; 
    DECLARE _exchange_rate_debit    public.decimal_strict; 
    DECLARE _exchange_rate_credit   public.decimal_strict;
    DECLARE _reference_number       national character varying(24); 
    DECLARE _statement_reference    national character varying(128); 
    DECLARE _cost_center_id         integer;
    DECLARE _cash_repository_id     integer;
    DECLARE _posted_date            date;
    DECLARE _bank_account_id        integer;
    DECLARE _bank_instrument_code   national character varying(128);
    DECLARE _bank_tran_code         national character varying(128);
    DECLARE _result                 bigint;
BEGIN
    PERFORM unit_tests.create_mock();
    PERFORM unit_tests.sign_in_test();

    _office_id                      := office.get_office_id_by_office_code('dummy-off01');
    _user_id                        := office.get_user_id_by_user_name('plpgunit-test-user-000001');
    _login_id                       := office.get_login_id(_user_id);
    _party_code                     := 'dummy-pr01';
    _currency_code                  := 'USD';
    _amount                         := 1000.00;
    _exchange_rate_debit            := 100.00;
    _exchange_rate_credit           := 100.00;
    _reference_number               := 'PL-PG-UNIT-TEST';
    _statement_reference            := 'This transaction should have been rollbacked already.';
    _cost_center_id                 := office.get_cost_center_id_by_cost_center_code('dummy-cs01');
    _cash_repository_id             := office.get_cash_repository_id_by_cash_repository_code('dummy-cr01');
    _posted_date                    := NULL;
    _bank_account_id                := NULL;
    _bank_instrument_code           := NULL;
    _bank_tran_code                 := NULL;
                                                        
    _result                         := transactions.post_receipt_function
                                    (
                                        _user_id, 
                                        _office_id, 
                                        _login_id,
                                        _party_code, 
                                        _currency_code, 
                                        _amount, 
                                        _exchange_rate_debit, 
                                        _exchange_rate_credit,
                                        _reference_number, 
                                        _statement_reference, 
                                        _cost_center_id,
                                        _cash_repository_id,
                                        _posted_date,
                                        _bank_account_id,
                                        _bank_instrument_code,
                                        _bank_tran_code 
                                    );

    IF(_result <= 0) THEN
        SELECT assert.fail('Cannot compile transactions.post_receipt_function.') INTO message;
        RETURN message;
    END IF;

    SELECT assert.ok('End of test.') INTO message;  
    RETURN message;
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests/office/unit_tests.sign_in_test.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.sign_in_test();

CREATE FUNCTION unit_tests.sign_in_test()
RETURNS public.test_result
AS
$$
    DECLARE _office_id          integer;
    DECLARE _user_name          text='plpgunit-test-user-000001';
    DECLARE _password           text = encode(digest(encode(digest('plpgunit-test-user-000001thoushaltnotlogin', 'sha512'), 'hex') || 'common', 'sha512'), 'hex');
    DECLARE _culture            text='en-US';
    DECLARE _login_id           bigint;
    DECLARE _sing_in_message    text;
    DECLARE message             public.test_result;
BEGIN
    PERFORM unit_tests.create_dummy_offices();
    PERFORM unit_tests.create_dummy_users();

    _office_id := office.get_office_id_by_office_code('dummy-off01');
    
    SELECT * FROM office.sign_in(_office_id, _user_name, _password, 'Plpgunit', '127.0.0.1', 'Plpgunit/plpgunit-test-user-000001', _culture, 'common')    
    INTO _login_id, _sing_in_message;
    
    IF(COALESCE(_login_id, 0) = 0) THEN
        SELECT assert.fail(_sing_in_message) INTO message;
        RETURN message;
    END IF;

    SELECT assert.ok('End of test.') INTO message;  
    RETURN message;
END
$$
LANGUAGE plpgsql;



-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests/others/unit_tests.if_functions_compile.sql --<--<--
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


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests/others/unit_tests.if_views_compile.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.if_views_compile();

CREATE FUNCTION unit_tests.if_views_compile()
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


    SELECT * FROM assert.if_views_compile(VARIADIC schemas) INTO message, result;
    
    IF(result=false) THEN
        RETURN message;
    END IF;

    SELECT assert.ok('End of test.') INTO message;  
    RETURN message; 
END
$$
LANGUAGE plpgsql;



-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests/transactions/unit_tests.balance_sheet_test.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.balance_sheet_test();

--Todo--check balance sheet by office
CREATE FUNCTION unit_tests.balance_sheet_test()
RETURNS public.test_result
AS
$$
    DECLARE _period_from    date;
    DECLARE _period_to      date;
    DECLARE _counter        date;
    DECLARE _bs_unequal     bool=false;
    DECLARE _message        public.test_result;
    DECLARE _difference     numeric;
    DECLARE _amount         numeric;
    DECLARE _office_id      integer;
    DECLARE _user_id        integer;
BEGIN
    SELECT office_id INTO _office_id
    FROM office.offices
    ORDER BY office_id
    LIMIT 1;

    SELECT user_id INTO _user_id
    FROM office.users
    LIMIT 1;
    
    SELECT 
        min(value_date), 
        max(value_date)
    INTO
        _period_from,
        _period_to
    FROM transactions.transaction_master;


    SELECT 
    SUM
    (
        CASE WHEN item='Assets' 
        THEN current_period * -1 
        ELSE current_period END
    )
    INTO
        _difference            
    FROM transactions.get_balance_sheet(_period_from, _period_to, _user_id, _office_id, 1)
    WHERE item IN ('Assets', 'Liabilities & Shareholders'' Equity');

    IF(_difference) <> 0 THEN
        _bs_unequal := true;
        _counter    := _period_to;
    END IF;


    WHILE _bs_unequal
    LOOP
        SELECT 
        SUM
        (
            CASE WHEN item='Assets' 
            THEN current_period * -1 
            ELSE current_period END
        )
        INTO
            _amount            
        FROM transactions.get_balance_sheet('7/17/2014', _counter, _user_id, _office_id, 1)
        WHERE item IN ('Assets', 'Liabilities & Shareholders'' Equity');
                
        IF(COALESCE(_amount, 0) = 0) THEN
            _bs_unequal := false;
            _message    := 'Balance sheet unequal on date: ' || (_counter + 1)::text || '. Difference in base currency : ' || _difference::text || '.';
            
            PERFORM assert.fail(_message);
            RETURN _message;
         END IF;

        _counter := _counter - 1;
     END LOOP;

    SELECT assert.ok('End of test.') INTO _message;  
    RETURN _message;
END
$$
LANGUAGE plpgsql;


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests/unit_tests.create_mock.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_mock();

CREATE FUNCTION unit_tests.create_mock()
RETURNS void
AS
$$
BEGIN
    PERFORM unit_tests.create_dummy_offices();
    PERFORM unit_tests.create_dummy_departments();
    PERFORM unit_tests.create_dummy_users();
    PERFORM unit_tests.create_dummy_accounts();
    PERFORM unit_tests.create_dummy_sales_taxes();
    PERFORM unit_tests.create_dummy_countries();
    PERFORM unit_tests.create_dummy_states();
    PERFORM unit_tests.create_dummy_party_types();
    PERFORM unit_tests.create_dummy_item_groups();
    PERFORM unit_tests.create_dummy_item_types();
    PERFORM unit_tests.create_dummy_units();
    PERFORM unit_tests.create_dummy_brands();
    PERFORM unit_tests.create_dummy_parties();
    PERFORM unit_tests.create_dummy_items();
    PERFORM unit_tests.create_dummy_cost_centers();
    PERFORM unit_tests.create_dummy_late_fees();
    PERFORM unit_tests.create_dummy_sales_teams();
    PERFORM unit_tests.create_dummy_salespersons();
    PERFORM unit_tests.create_dummy_shippers();
    PERFORM unit_tests.create_dummy_cash_repositories();
    PERFORM unit_tests.create_dummy_store_types();
    PERFORM unit_tests.create_dummy_stores();
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_accounts.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_accounts();

CREATE FUNCTION unit_tests.create_dummy_accounts()
RETURNS void 
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.accounts WHERE account_number = 'dummy-acc01') THEN
        INSERT INTO core.accounts(account_master_id, account_number, currency_code, account_name)
        SELECT core.get_account_master_id_by_account_master_code('BSA'), 'dummy-acc01', 'NPR', 'Test Mock Account 1';
    END IF;

    IF NOT EXISTS(SELECT 1 FROM core.accounts WHERE account_number = 'dummy-acc02') THEN
        INSERT INTO core.accounts(account_master_id, account_number, currency_code, account_name)
        SELECT core.get_account_master_id_by_account_master_code('BSA'), 'dummy-acc02', 'NPR', 'Test Mock Account 2';
    END IF;

    IF NOT EXISTS(SELECT 1 FROM core.accounts WHERE account_number = 'dummy-acc03') THEN
        INSERT INTO core.accounts(account_master_id, account_number, currency_code, account_name)
        SELECT core.get_account_master_id_by_account_master_code('BSA'), 'dummy-acc03', 'NPR', 'Test Mock Account 3';
    END IF;

    IF NOT EXISTS(SELECT 1 FROM core.accounts WHERE account_number = 'dummy-acc04') THEN
        INSERT INTO core.accounts(account_master_id, account_number, currency_code, account_name)
        SELECT core.get_account_master_id_by_account_master_code('BSA'), 'dummy-acc04', 'NPR', 'Test Mock Account 4';
    END IF;

    IF NOT EXISTS(SELECT 1 FROM core.accounts WHERE account_number = 'dummy-acc05') THEN
        INSERT INTO core.accounts(account_master_id, account_number, currency_code, account_name)
        SELECT core.get_account_master_id_by_account_master_code('BSA'), 'dummy-acc05', 'NPR', 'Test Mock Account 5';
    END IF;

    IF NOT EXISTS(SELECT 1 FROM core.accounts WHERE account_number = 'dummy-acc06') THEN
        INSERT INTO core.accounts(account_master_id, account_number, currency_code, account_name)
        SELECT core.get_account_master_id_by_account_master_code('CAS'), 'dummy-acc06', 'NPR', 'Test Mock Account 6';
    END IF;
END
$$
LANGUAGE plpgsql;



-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_auto_verification_policy.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_auto_verification_policy
(
        _user_id integer, 
        _verify_sales_transactions      boolean, 
        _sales_verification_limit       public.money_strict2, 
        _verify_purchase_transactions   boolean, 
        _purchase_verification_limit    public.money_strict2, 
        _verify_gl_transactions         boolean,
        _gl_verification_limit          public.money_strict2,
        _effective_from                 date,
        _ends_on                        date,
        _is_active                      boolean
);

CREATE FUNCTION unit_tests.create_dummy_auto_verification_policy
(
        _user_id                        integer, 
        _verify_sales_transactions      boolean, 
        _sales_verification_limit       public.money_strict2, 
        _verify_purchase_transactions   boolean, 
        _purchase_verification_limit    public.money_strict2, 
        _verify_gl_transactions         boolean,
        _gl_verification_limit          public.money_strict2,
        _effective_from                 date,
        _ends_on                        date,
        _is_active                      boolean
)
RETURNS void 
AS
$$
BEGIN
        IF NOT EXISTS(SELECT 1 FROM policy.auto_verification_policy WHERE user_id=_user_id) THEN
                INSERT INTO policy.auto_verification_policy(user_id, verify_sales_transactions, sales_verification_limit, verify_purchase_transactions, purchase_verification_limit, verify_gl_transactions, gl_verification_limit, effective_from, ends_on, is_active)
                SELECT _user_id, _verify_sales_transactions, _sales_verification_limit, _verify_purchase_transactions, _purchase_verification_limit, _verify_gl_transactions, _gl_verification_limit, _effective_from, _ends_on, _is_active;
                RETURN;
        END IF;

        UPDATE policy.auto_verification_policy
        SET 
                verify_sales_transactions = _verify_sales_transactions,
                sales_verification_limit = _sales_verification_limit,
                verify_purchase_transactions = _verify_purchase_transactions,
                purchase_verification_limit = _purchase_verification_limit,
                verify_gl_transactions = _verify_gl_transactions, 
                gl_verification_limit = _gl_verification_limit, 
                effective_from = _effective_from, 
                ends_on = _ends_on, 
                is_active = _is_active                
        WHERE user_id=_user_id;
        
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_brands.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_brands();

CREATE FUNCTION unit_tests.create_dummy_brands()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.brands WHERE brand_code='dummy-br01') THEN        
        INSERT INTO core.brands(brand_code, brand_name)
        SELECT 'dummy-br01', 'Test Mock Brand';
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;



-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_cash_repositories.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_cash_repositories();

CREATE FUNCTION unit_tests.create_dummy_cash_repositories()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM office.cash_repositories WHERE cash_repository_code='dummy-cr01') THEN        
        INSERT INTO office.cash_repositories(cash_repository_code, cash_repository_name, office_id)
        SELECT 'dummy-cr01', 'Test Mock Cash Repository', office.get_office_id_by_office_code('dummy-off01');
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;



-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_cost_centers.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_cost_centers();

CREATE FUNCTION unit_tests.create_dummy_cost_centers()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM office.cost_centers WHERE cost_center_code='dummy-cs01') THEN        
        INSERT INTO office.cost_centers(cost_center_code, cost_center_name)
        SELECT 'dummy-cs01', 'Test Mock Cost Center';
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;



-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_countries.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_countries();

CREATE FUNCTION unit_tests.create_dummy_countries()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.countries WHERE country_code='dummy-co01') THEN        
        INSERT INTO core.countries(country_code, country_name)
        SELECT 'dummy-co01', 'Test Mock Country';
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;



-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_departments.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_departments();

CREATE FUNCTION unit_tests.create_dummy_departments()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM office.departments WHERE department_code='dummy-dp01') THEN        
        INSERT INTO office.departments(department_code, department_name)
        SELECT 'dummy-dp01', 'Test Mock Department';
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_item_groups.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_item_groups();

CREATE FUNCTION unit_tests.create_dummy_item_groups()
RETURNS void
AS
$$
    DECLARE _dummy_account_id bigint;
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.item_groups WHERE item_group_code='dummy-ig01') THEN

        _dummy_account_id := core.get_account_id_by_account_number('dummy-acc01');
        
        INSERT INTO core.item_groups(item_group_code, item_group_name, sales_tax_id, sales_account_id, sales_discount_account_id, sales_return_account_id, purchase_account_id, purchase_discount_account_id, inventory_account_id, cost_of_goods_sold_account_id)
        SELECT 'dummy-ig01', 'Test Mock Item Group', core.get_sales_tax_id_by_sales_tax_code('dummy-stx01'), _dummy_account_id, _dummy_account_id, _dummy_account_id, _dummy_account_id, _dummy_account_id, _dummy_account_id, _dummy_account_id;
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;



-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_item_types.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_item_types();

CREATE FUNCTION unit_tests.create_dummy_item_types()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.item_types WHERE item_type_code='dummy-it01') THEN
        INSERT INTO core.item_types(item_type_code, item_type_name)
        SELECT 'dummy-it01', 'Test Mock Item Type';
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;



-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_items.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_items();

CREATE FUNCTION unit_tests.create_dummy_items()
RETURNS void
AS
$$
    DECLARE _dummy_unit_id integer;
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.items WHERE item_code='dummy-it01') THEN

        _dummy_unit_id := core.get_unit_id_by_unit_code('dummy-uni01');
    
        INSERT INTO core.items
        (
            item_code, 
            item_name, 
            item_group_id,
            item_type_id,
            brand_id,
            preferred_supplier_id,
            lead_time_in_days,
            unit_id,
            hot_item,
            cost_price,
            selling_price,
            sales_tax_id,
            reorder_unit_id,
            reorder_level,
            reorder_quantity,
            maintain_stock
        )
        SELECT 
            'dummy-it01'                                            AS item_code, 
            'Test Mock Item'                                        AS item_name,
            core.get_item_group_id_by_item_group_code('dummy-ig01') AS item_group_id,
            core.get_item_type_id_by_item_type_code('dummy-it01')   AS item_type_id,
            core.get_brand_id_by_brand_code('dummy-br01')           AS brand_id, 
            core.get_party_id_by_party_code('dummy-pr01')           AS preferred_supplier_id,
            10                                                      AS lead_time,
            _dummy_unit_id                                          AS unit_id,
            false                                                   AS hot_item,
            3000                                                    AS cost_price,
            4000                                                    AS selling_price,
            core.get_sales_tax_id_by_sales_tax_code('dummy-stx01')  AS sales_tax_id,
            _dummy_unit_id                                          AS reorder_unit_id,
            10                                                      AS reorder_level,
            100                                                     AS reorder_quantity,
            false                                                   AS maintain_stock
        UNION ALL
        SELECT 
            'dummy-it02'                                            AS item_code, 
            'Test Mock Item2'                                       AS item_name,
            core.get_item_group_id_by_item_group_code('dummy-ig01') AS item_group_id,
            core.get_item_type_id_by_item_type_code('dummy-it01')   AS item_type_id,
            core.get_brand_id_by_brand_code('dummy-br01')           AS brand_id, 
            core.get_party_id_by_party_code('dummy-pr01')           AS preferred_supplier_id,
            17                                                      AS lead_time,
            _dummy_unit_id                                          AS unit_id,
            false                                                   AS hot_item,
            1400                                                    AS cost_price,
            1800                                                    AS selling_price,
            core.get_sales_tax_id_by_sales_tax_code('dummy-stx01')  AS sales_tax_id,
            _dummy_unit_id                                          AS reorder_unit_id,
            10                                                      AS reorder_level,
            50                                                      AS reorder_quantity,
            false                                                   AS maintain_stock;        
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;



-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_late_fees.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_late_fees();

CREATE FUNCTION unit_tests.create_dummy_late_fees()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.late_fee WHERE late_fee_code='dummy-lf01') THEN        
        INSERT INTO core.late_fee(late_fee_code, late_fee_name, is_flat_amount, rate)
        SELECT 'dummy-lf01', 'Test Mock Late Fee', false, 22;
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_offices.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_offices();

CREATE FUNCTION unit_tests.create_dummy_offices()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM office.offices WHERE office_code='dummy-off01') THEN
        INSERT INTO office.offices(office_code, office_name, nick_name, registration_date, currency_code, allow_transaction_posting)
        SELECT 'dummy-off01', 'PLPGUnit Test Office', 'PTO-DUMMY-0001', NOW()::date, 'NPR', true;
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_parties.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_parties();

CREATE FUNCTION unit_tests.create_dummy_parties()
RETURNS void
AS
$$
    DECLARE _dummy_account_id   bigint;
    DECLARE _party_id           bigint;
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.parties WHERE party_code='dummy-pr01') THEN        
        _dummy_account_id := core.get_account_id_by_account_number('dummy-acc01');

        INSERT INTO core.parties(party_type_id, first_name, last_name, country_id, state_id, currency_code, account_id)
        SELECT            
            core.get_party_type_id_by_party_type_code('dummy-pt01'), 
            'Test Mock party', 
            'Test', 
            core.get_country_id_by_country_code('dummy-co01'),
            core.get_state_id_by_state_code('dummy-st01'),
            'NPR',
            _dummy_account_id
       RETURNING party_id INTO _party_id;

    UPDATE core.parties
    SET party_code = 'dummy-pr01'
    WHERE party_id = _party_id;
       
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;



-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_party_types.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_party_types();

CREATE FUNCTION unit_tests.create_dummy_party_types()
RETURNS void
AS
$$
    DECLARE _dummy_account_id bigint;
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.party_types WHERE party_type_code='dummy-pt01') THEN        
        _dummy_account_id := core.get_account_id_by_account_number('dummy-acc01');

        INSERT INTO core.party_types(party_type_code, party_type_name, is_supplier, account_id)
        SELECT 'dummy-pt01', 'Test Mock Party Type', false, _dummy_account_id;
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;



-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_payment_terms.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_payment_terms();

CREATE FUNCTION unit_tests.create_dummy_payment_terms()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.payment_terms WHERE payment_term_code='dummy-pt01') THEN        
        INSERT INTO core.payment_terms(payment_term_code, payment_term_name, due_on_date, due_days, grace_peiod, late_fee_id, late_fee_posting_frequency_id)
        SELECT 'dummy-pt01', 'Test Mock Payment Term', false, 10, 5, core.get_late_fee_id_by_late_fee_code('dummy-lf01'), core.get_frequency_id_by_frequency_code('EOM');
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_sales_taxes.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_sales_taxes();

CREATE FUNCTION unit_tests.create_dummy_sales_taxes()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.tax_master WHERE tax_master_code='dummy-tm01') THEN
        INSERT INTO core.tax_master(tax_master_code, tax_master_name)
        SELECT 'dummy-tm01', 'Dummy Tax Master';
    END IF;
    
    IF NOT EXISTS(SELECT 1 FROM core.sales_taxes WHERE sales_tax_code='dummy-stx01') THEN
        INSERT INTO core.sales_taxes(tax_master_id, office_id, sales_tax_code, sales_tax_name, is_exemption, tax_base_amount_type_code, rate)
        SELECT 
            core.get_tax_master_id_by_tax_master_code('dummy-tm01'), 
            office.get_office_id_by_office_code('dummy-off01'),
            'dummy-stx01',
            'Dummy Sales Tax',
            false,
            'P',
            12.4;
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

--SELECT * FROM unit_tests.create_dummy_sales_tax()

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_sales_teams.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_sales_teams();

CREATE FUNCTION unit_tests.create_dummy_sales_teams()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.sales_teams WHERE sales_team_code='dummy-st01') THEN        
        INSERT INTO core.sales_teams(sales_team_code, sales_team_name)
        SELECT 'dummy-st01', 'Test Mock Sales Team';
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;



-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_salespersons.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_salespersons();

CREATE FUNCTION unit_tests.create_dummy_salespersons()
RETURNS void
AS
$$
    DECLARE _dummy_account_id bigint;
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.salespersons WHERE salesperson_code='dummy-sp01') THEN        
        _dummy_account_id := core.get_account_id_by_account_number('dummy-acc01');

        INSERT INTO core.salespersons(salesperson_code, salesperson_name, sales_team_id, address, contact_number, account_id)
        SELECT 'dummy-sp01', 'Test Mock Salesperson', core.get_sales_team_id_by_sales_team_code('dummy-st01'), '', '', _dummy_account_id;
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;



-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_shippers.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_shippers();

CREATE FUNCTION unit_tests.create_dummy_shippers()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.shippers WHERE shipper_code='dummy-sh01') THEN        
        INSERT INTO core.shippers(shipper_code, shipper_name, company_name, account_id)
        SELECT 'dummy-sh01', 'Test Mock Shipper', 'Test Mock Shipper', core.get_account_id_by_account_number('dummy-acc01');
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;



-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_states.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_states();

CREATE FUNCTION unit_tests.create_dummy_states()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.states WHERE state_code='dummy-st01') THEN        
        INSERT INTO core.states(state_code, state_name, country_id)
        SELECT 'dummy-st01', 'Test Mock State', core.get_country_id_by_country_code('dummy-co01');
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;



-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_store_types.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_store_types();

CREATE FUNCTION unit_tests.create_dummy_store_types()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM office.store_types WHERE store_type_code='dummy-st01') THEN
        INSERT INTO office.store_types(store_type_code, store_type_name)
        SELECT 'dummy-st01', 'Test Mock Store Type';
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;



-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_stores.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_stores();

CREATE FUNCTION unit_tests.create_dummy_stores()
RETURNS void
AS
$$
    DECLARE _cash_account_id bigint;
BEGIN
    IF NOT EXISTS(SELECT 1 FROM office.stores WHERE store_code='dummy-st01') THEN
        INSERT INTO office.stores(store_code, store_name, office_id, store_type_id, allow_sales, sales_tax_id, default_cash_account_id, default_cash_repository_id)
        SELECT 
            'dummy-st01', 
            'Test Mock Store',
            office.get_office_id_by_office_code('dummy-off01'),
            office.get_store_type_id_by_store_type_code('dummy-st01'),
            true,
            core.get_sales_tax_id_by_sales_tax_code('dummy-stx01'),
            core.get_account_id_by_account_number('dummy-acc06'),
            office.get_cash_repository_id_by_cash_repository_code('dummy-cr01');
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;



-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_units.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_units();

CREATE FUNCTION unit_tests.create_dummy_units()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.units WHERE unit_code='dummy-uni01') THEN        
        INSERT INTO core.units(unit_code, unit_name)
        SELECT 'dummy-uni01', 'Test Mock Unit';
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;



-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_users.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_users();

CREATE FUNCTION unit_tests.create_dummy_users()
RETURNS void 
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM office.users WHERE user_name='plpgunit-test-user-000001') THEN
        INSERT INTO office.users(role_id, department_id, user_name, full_name, password, office_id)
        SELECT office.get_role_id_by_role_code('USER'), office.get_department_id_by_department_code('dummy-dp01'), 'plpgunit-test-user-000001', 'PLPGUnit Test User', 'thoushaltnotlogin', office.get_office_id_by_office_code('dummy-off01');
    END IF;
END
$$
LANGUAGE plpgsql;


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/refresh-materialized-views.sql --<--<--
SELECT * FROM transactions.refresh_materialized_views(2, 2, 5, '1/1/2015');
