/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

DROP FUNCTION IF EXISTS unit_tests.ensure_verification_statuses();

CREATE FUNCTION unit_tests.ensure_verification_statuses()
RETURNS public.test_result
AS
$$
DECLARE message public.test_result = '';
BEGIN
	IF NOT EXISTS(SELECT * FROM core.verification_statuses WHERE verification_status_id = -3) THEN
		SELECT assert.fail('The rejected flag (-3) does not exist on table core.verification_statuses.') INTO message;			
		RETURN message;
	END IF;

	IF NOT EXISTS(SELECT * FROM core.verification_statuses WHERE verification_status_id = -2) THEN
		SELECT assert.fail('The closed flag (-2) does not exist on table core.verification_statuses.') INTO message;			
		RETURN message;
	END IF;

	IF NOT EXISTS(SELECT * FROM core.verification_statuses WHERE verification_status_id = -1) THEN
		SELECT assert.fail('The withdrawn flag (-1) does not exist on table core.verification_statuses.') INTO message;			
		RETURN message;
	END IF;

	IF NOT EXISTS(SELECT * FROM core.verification_statuses WHERE verification_status_id = 0) THEN
		SELECT assert.fail('The unverified flag (0) does not exist on table core.verification_statuses.') INTO message;			
		RETURN message;
	END IF;

	IF NOT EXISTS(SELECT * FROM core.verification_statuses WHERE verification_status_id = 1) THEN
		SELECT assert.fail('The auto-workflow-approved flag (1) does not exist on table core.verification_statuses.') INTO message;			
		RETURN message;
	END IF;

	IF NOT EXISTS(SELECT * FROM core.verification_statuses WHERE verification_status_id = 2) THEN
		SELECT assert.fail('The approved flag (2) does not exist on table core.verification_statuses.') INTO message;			
		RETURN message;
	END IF;

	SELECT assert.ok('End of test.') INTO message;	
	RETURN message;
END
$$
LANGUAGE plpgsql;
