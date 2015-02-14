DROP FUNCTION IF EXISTS unit_tests.create_dummy_shippers();

CREATE FUNCTION unit_tests.create_dummy_shippers()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.shippers WHERE shipper_code='dummy-sh01') THEN        
        INSERT INTO core.shippers(shipper_code, shipper_name, company_name, account_id)
        SELECT 'dummy-sh01', 'Test Mock Shipper', 'Test Mock Shipper', core.get_account_id_by_account_number('dummy-acc01');
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

