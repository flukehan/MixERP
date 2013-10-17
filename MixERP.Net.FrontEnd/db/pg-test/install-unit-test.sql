/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

DROP SCHEMA IF EXISTS assert CASCADE;
DROP SCHEMA IF EXISTS unit_tests CASCADE;
DROP DOMAIN IF EXISTS test_message CASCADE;

CREATE SCHEMA assert AUTHORIZATION postgres;
CREATE SCHEMA unit_tests AUTHORIZATION postgres;
CREATE DOMAIN test_message AS text;

CREATE TABLE unit_tests.tests
(
	test_id			SERIAL NOT NULL PRIMARY KEY,
	started_on		TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(NOW()),
	completed_on		TIMESTAMP WITH TIME ZONE NULL,
	total_tests		integer NULL DEFAULT(0),
	failed_tests		integer NULL DEFAULT(0)
);


CREATE TABLE unit_tests.test_details
(
	id			BIGSERIAL NOT NULL PRIMARY KEY,
	test_id			integer NOT NULL REFERENCES unit_tests.tests(test_id),
	function_name		text NOT NULL,
	message			text NOT NULL,
	ts			TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(NOW()),
	status			boolean NOT NULL
);

CREATE FUNCTION assert.fail(message text)
RETURNS text
AS
$$
BEGIN
	IF $1 IS NULL OR trim($1) = '' THEN
		message := 'NO REASON SPECIFIED';
	ELSE
		message := message;
	END IF;
	
	RAISE WARNING 'ASSERT FAILED : %', message;
	RETURN message;
END
$$
LANGUAGE plpgsql;

CREATE FUNCTION assert.ok(message text)
RETURNS text
AS
$$
BEGIN
	RAISE NOTICE '%', message;
	RETURN '';
END
$$
LANGUAGE plpgsql;

CREATE FUNCTION unit_tests.run_tests()
RETURNS text
AS
$$
DECLARE this record;
DECLARE function_name text;
DECLARE sql text;
DECLARE _message text;
DECLARE _test_id integer;
DECLARE _status boolean;
DECLARE _total_tests integer = 0;
DECLARE _failed_tests integer = 0;
DECLARE failed_tests text;
DECLARE ret_val text;
BEGIN

	SELECT nextval('unit_tests.tests_test_id_seq') INTO _test_id;

	INSERT INTO unit_tests.tests(test_id)
	SELECT _test_id;

	FOR this IN
		SELECT proname as function_name
		FROM    pg_catalog.pg_namespace n
		JOIN    pg_catalog.pg_proc p
		ON      pronamespace = n.oid
		WHERE   nspname = 'unit_tests'
		AND prorettype='test_message'::regtype::oid
	LOOP
		_status := false;
		_total_tests := _total_tests + 1;
		
		function_name = 'unit_tests.' || this.function_name || '()';
		sql := 'SELECT ' || function_name || ';';
		
		RAISE NOTICE 'RUNNING TEST : %.', function_name;

		EXECUTE sql INTO _message;

		IF _message = '' THEN
			_status := true;
		END IF;

		
		INSERT INTO unit_tests.test_details(test_id, function_name, message, status)
		SELECT _test_id, function_name, _message, _status;

		IF NOT _status THEN
			_failed_tests := _failed_tests + 1;			
			RAISE WARNING 'TEST % FAILED.', function_name;
			RAISE WARNING 'REASON: %', _message;
		ELSE
			RAISE NOTICE 'TEST % COMPLETED WITHOUT ERRORS.', function_name;
		END IF;
	END LOOP;

	UPDATE unit_tests.tests
	SET total_tests = _total_tests, failed_tests = _failed_tests, completed_on = NOW()
	WHERE test_id = _test_id;

	IF _failed_tests > 0 THEN
		SELECT array_to_string(array_agg(unit_tests.test_details.function_name || ' --> ' || unit_tests.test_details.message), E'\n') INTO failed_tests 
		FROM unit_tests.test_details 
		WHERE test_id = _test_id
		AND status= false;
		
		ret_val := E'\nTotal tests run:' || _total_tests;
		ret_val := ret_val || E'\nPassed tests : ' || _total_tests - _failed_tests;
		ret_val := ret_val || E'.\nFailed tests: ' || _failed_tests;
		ret_val := ret_val || E'.\n\nList of failed tests:\n' || '-----------------------------';
		ret_val := ret_val || E'\n' || failed_tests;
		ret_val := ret_val || E'\n\n';

		RAISE WARNING '%', ret_val;
	END IF;
	
	RETURN ret_val;
END
$$
LANGUAGE plpgsql;

--select unit_tests.run_tests();