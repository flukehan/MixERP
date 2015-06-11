/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

DROP FUNCTION IF EXISTS unit_tests.check_frequency();

CREATE FUNCTION unit_tests.check_frequency()
RETURNS public.test_result
AS
$$
	DECLARE message text;
BEGIN
	IF NOT EXISTS(SELECT 1 FROM core.frequencies WHERE frequency_id=2) THEN
		SELECT assert.fail('EOM frequency not present in the catalog.') INTO message;
		RETURN message;		
	END IF;

	IF NOT EXISTS(SELECT 1 FROM core.frequencies WHERE frequency_id=3) THEN
		SELECT assert.fail('EOQ frequency not present in the catalog.') INTO message;
		RETURN message;		
	END IF;

	IF NOT EXISTS(SELECT 1 FROM core.frequencies WHERE frequency_id=4) THEN
		SELECT assert.fail('EOH frequency not present in the catalog.') INTO message;
		RETURN message;		
	END IF;

	IF NOT EXISTS(SELECT 1 FROM core.frequencies WHERE frequency_id=5) THEN
		SELECT assert.fail('EOY frequency not present in the catalog.') INTO message;
		RETURN message;		
	END IF;
	
	SELECT assert.ok('End of test.') INTO message;
	RETURN message;	
END
$$
LANGUAGE plpgsql;
