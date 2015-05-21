/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

DROP FUNCTION IF EXISTS unit_tests.check_frequency_setups();

CREATE FUNCTION unit_tests.check_frequency_setups()
RETURNS public.test_result
AS
$$
	DECLARE message text;
	DECLARE fy_code text;
	DECLARE frequency_count integer;
BEGIN
	SELECT fiscal_year_code INTO fy_code
	FROM core.fiscal_year
	WHERE ends_on >= NOW()
	ORDER BY ends_on DESC
	LIMIT 1;

	IF(TRIM(COALESCE(fy_code, '')) = '') THEN
		SELECT assert.fail('Fiscal year not present in the catalog.') INTO message;
		RETURN message;
	END IF;

	SELECT COUNT(*) INTO frequency_count
	FROM core.frequency_setups
	WHERE fiscal_year_code = fy_code;
	
	IF frequency_count <> 12 THEN
		SELECT assert.fail('Invalid frequency setup encountered.') INTO message;
		RETURN message;		
	END IF;
	
	SELECT assert.ok('End of test.') INTO message;
	RETURN message;	
END
$$
LANGUAGE plpgsql;


