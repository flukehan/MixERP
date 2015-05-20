DROP FUNCTION IF EXISTS unit_tests.create_dummy_accounts();

CREATE FUNCTION unit_tests.create_dummy_accounts()
RETURNS void 
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.accounts WHERE account_number = 'dummy-acc01') THEN
        INSERT INTO core.accounts(account_master_id, account_number, currency_code, account_name)
        SELECT core.get_account_master_id_by_account_master_code('BSA'), 'dummy-acc01', 'NPR', 'Test Mock Account 1';
    END IF;

    IF NOT EXISTS(SELECT 1 FROM core.accounts WHERE account_number = 'dummy-acc02') THEN
        INSERT INTO core.accounts(account_master_id, account_number, currency_code, account_name)
        SELECT core.get_account_master_id_by_account_master_code('BSA'), 'dummy-acc02', 'NPR', 'Test Mock Account 2';
    END IF;

    IF NOT EXISTS(SELECT 1 FROM core.accounts WHERE account_number = 'dummy-acc03') THEN
        INSERT INTO core.accounts(account_master_id, account_number, currency_code, account_name)
        SELECT core.get_account_master_id_by_account_master_code('BSA'), 'dummy-acc03', 'NPR', 'Test Mock Account 3';
    END IF;

    IF NOT EXISTS(SELECT 1 FROM core.accounts WHERE account_number = 'dummy-acc04') THEN
        INSERT INTO core.accounts(account_master_id, account_number, currency_code, account_name)
        SELECT core.get_account_master_id_by_account_master_code('BSA'), 'dummy-acc04', 'NPR', 'Test Mock Account 4';
    END IF;

    IF NOT EXISTS(SELECT 1 FROM core.accounts WHERE account_number = 'dummy-acc05') THEN
        INSERT INTO core.accounts(account_master_id, account_number, currency_code, account_name)
        SELECT core.get_account_master_id_by_account_master_code('BSA'), 'dummy-acc05', 'NPR', 'Test Mock Account 5';
    END IF;

    IF NOT EXISTS(SELECT 1 FROM core.accounts WHERE account_number = 'dummy-acc06') THEN
        INSERT INTO core.accounts(account_master_id, account_number, currency_code, account_name)
        SELECT core.get_account_master_id_by_account_master_code('CAS'), 'dummy-acc06', 'NPR', 'Test Mock Account 6';
    END IF;
END
$$
LANGUAGE plpgsql;

