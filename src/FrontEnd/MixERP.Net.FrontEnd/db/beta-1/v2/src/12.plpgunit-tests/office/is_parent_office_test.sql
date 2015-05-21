/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
DROP FUNCTION IF EXISTS unit_tests.is_parent_office_test();

CREATE FUNCTION unit_tests.is_parent_office_test()
RETURNS public.test_result
AS
$$
	DECLARE have boolean;
	DECLARE want boolean;
	DECLARE message text;
	DECLARE result boolean;
	DECLARE grand_parent_id integer;
	DECLARE parent_id integer;
	DECLARE id integer;
	DECLARE sibling_id integer;
	DECLARE child_id integer;
BEGIN
	grand_parent_id := nextval('office.offices_office_id_seq');
	INSERT INTO office.offices(office_id, office_code,office_name,nick_name,registration_date, street,city,state,country,zip_code,phone,fax,email,url,registration_number,pan_number, currency_code)
	SELECT grand_parent_id, 'Grand Parent','Grand Parent', 'Grand Parent', '1-1-2000', '','','','','','','','','','0','0', 'NPR';
	
	parent_id := nextval('office.offices_office_id_seq');
	INSERT INTO office.offices(office_id, parent_office_id, office_code,office_name,nick_name,registration_date, street,city,state,country,zip_code,phone,fax,email,url,registration_number,pan_number, currency_code)
	SELECT parent_id, grand_parent_id, 'Parent','Parent', 'Parent', '1-1-2000', '','','','','','','','','','0','0', 'NPR';
	
	id := nextval('office.offices_office_id_seq');
	INSERT INTO office.offices(office_id, parent_office_id, office_code,office_name,nick_name,registration_date, street,city,state,country,zip_code,phone,fax,email,url,registration_number,pan_number, currency_code)
	SELECT id, parent_id, 'Office','Office', 'Office', '1-1-2000', '','','','','','','','','','0','0', 'NPR';

	sibling_id := nextval('office.offices_office_id_seq');
	INSERT INTO office.offices(office_id, parent_office_id, office_code,office_name,nick_name,registration_date, street,city,state,country,zip_code,phone,fax,email,url,registration_number,pan_number, currency_code)
	SELECT sibling_id, parent_id, 'Sibling','Sibling', 'Sibling', '1-1-2000', '','','','','','','','','','0','0', 'NPR';
	
	child_id := nextval('office.offices_office_id_seq');
	INSERT INTO office.offices(office_id, parent_office_id, office_code,office_name,nick_name,registration_date, street,city,state,country,zip_code,phone,fax,email,url,registration_number,pan_number, currency_code)
	SELECT child_id, id, 'Child','Child', 'Child', '1-1-2000', '','','','','','','','','','0','0', 'NPR';
	
	have := office.is_parent_office(id, sibling_id);
	want := false;
	SELECT * FROM assert.is_equal(have, want) INTO message, result;
	IF NOT result THEN
		RETURN message;
	END IF;

	have := office.is_parent_office(sibling_id, id);
	want := false;
	SELECT * FROM assert.is_equal(have, want) INTO message, result;
	IF NOT result THEN
		RETURN message;
	END IF;

	have := office.is_parent_office(id, child_id);
	want := true;
	SELECT * FROM assert.is_equal(have, want) INTO message, result;
	IF NOT result THEN
		RETURN message;
	END IF;

	have := office.is_parent_office(parent_id, id);
	want := true;
	SELECT * FROM assert.is_equal(have, want) INTO message, result;
	IF NOT result THEN
		RETURN message;
	END IF;
	
	have := office.is_parent_office(parent_id, sibling_id);
	want := true;
	SELECT * FROM assert.is_equal(have, want) INTO message, result;
	IF NOT result THEN
		RETURN message;
	END IF;
	
	have := office.is_parent_office(parent_id, child_id);
	want := true;
	SELECT * FROM assert.is_equal(have, want) INTO message, result;
	IF NOT result THEN
		RETURN message;
	END IF;
	
	have := office.is_parent_office(grand_parent_id, parent_id);
	want := true;
	SELECT * FROM assert.is_equal(have, want) INTO message, result;
	IF NOT result THEN
		RETURN message;
	END IF;
	
	have := office.is_parent_office(grand_parent_id, id);
	want := true;
	SELECT * FROM assert.is_equal(have, want) INTO message, result;
	IF NOT result THEN
		RETURN message;
	END IF;
	
	have := office.is_parent_office(grand_parent_id, sibling_id);
	want := true;
	SELECT * FROM assert.is_equal(have, want) INTO message, result;
	IF NOT result THEN
		RETURN message;
	END IF;
		
	have := office.is_parent_office(grand_parent_id, child_id);
	want := true;
	SELECT * FROM assert.is_equal(have, want) INTO message, result;
	IF NOT result THEN
		RETURN message;
	END IF;

	SELECT assert.ok('End of test.') INTO message;
	RETURN message;
END
$$
LANGUAGE plpgsql;

-- BEGIN;
-- SELECT unit_tests.begin();
-- ROLLBACK;