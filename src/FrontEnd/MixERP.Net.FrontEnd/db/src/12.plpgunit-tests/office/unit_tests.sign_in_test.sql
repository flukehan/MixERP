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
    DECLARE message             test_result;
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

