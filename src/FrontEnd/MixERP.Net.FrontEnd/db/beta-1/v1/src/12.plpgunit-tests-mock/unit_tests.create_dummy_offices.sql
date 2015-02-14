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