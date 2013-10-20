/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

DROP FUNCTION IF EXISTS unit_tests.check_interest_calulation();

CREATE FUNCTION unit_tests.check_interest_calulation()
RETURNS test_result
AS
$$
DECLARE message test_result = '';
DECLARE have numeric;
DECLARE want numeric;
DECLARE result boolean;
BEGIN

	SELECT core.calculate_interest(46790.59, 9.45, 37, 5, 365) INTO have;
	want := 408.22821;
	SELECT * FROM assert.is_equal(have, want) INTO message, result;

	IF result = false THEN
		RETURN message;
	END IF;

	SELECT assert.ok('End of test.') INTO message;
	RETURN message;
END
$$
LANGUAGE plpgsql;

