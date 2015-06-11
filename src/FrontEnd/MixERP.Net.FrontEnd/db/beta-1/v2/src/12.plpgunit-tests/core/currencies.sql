/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

DROP FUNCTION IF EXISTS unit_tests.check_currency();

CREATE FUNCTION unit_tests.check_currency()
RETURNS public.test_result
AS
$$
	DECLARE message text;
BEGIN
	IF NOT EXISTS(SELECT 1 FROM core.currencies LIMIT 1) THEN
		SELECT assert.fail('No currency found in the catalog.') INTO message;
		RETURN message;		
	END IF;

	SELECT assert.ok('End of test.') INTO message;
	RETURN message;	
END
$$
LANGUAGE plpgsql;
