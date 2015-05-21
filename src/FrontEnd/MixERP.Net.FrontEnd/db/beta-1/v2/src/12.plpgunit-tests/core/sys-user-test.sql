/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

DROP FUNCTION IF EXISTS unit_tests.check_sys_group_count();

CREATE FUNCTION unit_tests.check_sys_group_count()
RETURNS public.test_result
AS
$$
DECLARE message public.test_result = '';
DECLARE sys_group_count integer;
BEGIN
	SELECT COUNT(*) INTO sys_group_count
	FROM office.roles
	WHERE office.roles.is_system = true;

	IF sys_group_count = 0 THEN
		SELECT assert.fail('No sys account group found in the database.') INTO message;
		RETURN message;
	END IF;

	IF sys_group_count > 1 THEN
		SELECT assert.fail('You can only have one sys account group.') INTO message;	
		RETURN message;
	END IF;

	SELECT assert.ok('End of test.') INTO message;
	RETURN message;
END
$$
LANGUAGE plpgsql;



DROP FUNCTION IF EXISTS unit_tests.check_sys_user_count();

CREATE FUNCTION unit_tests.check_sys_user_count()
RETURNS public.test_result
AS
$$
DECLARE message public.test_result = '';
DECLARE sys_user_count integer;
BEGIN
	SELECT COUNT(*) INTO sys_user_count
	FROM office.users
	INNER JOIN office.roles
	ON office.users.role_id = office.roles.role_id
	WHERE office.roles.is_system = true;

	IF sys_user_count = 0 THEN
		SELECT assert.fail('No sys user account found in the database.') INTO message;
		RETURN message;
	END IF;

	IF sys_user_count > 1 THEN
		SELECT assert.fail('You can only have one sys user account.') INTO message;	
		RETURN message;
	END IF;

	SELECT assert.ok('End of test.') INTO message;
	RETURN message;
END
$$
LANGUAGE plpgsql;

