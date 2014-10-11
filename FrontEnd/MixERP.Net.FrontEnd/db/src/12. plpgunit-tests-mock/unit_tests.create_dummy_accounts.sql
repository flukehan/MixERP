DROP FUNCTION IF EXISTS unit_tests.create_dummy_accounts();

CREATE FUNCTION unit_tests.create_dummy_accounts()
RETURNS void 
AS
$$
BEGIN
        IF NOT EXISTS(SELECT 1 FROM core.accounts WHERE account_code = 'TEST-ACC-001') THEN
                INSERT INTO core.accounts(account_master_id, account_code, currency_code, account_name)
                SELECT core.get_account_master_id_by_account_master_code('BSA'), 'TEST-ACC-001', 'NPR', 'Test Mock Account 1';
        END IF;

        IF NOT EXISTS(SELECT 1 FROM core.accounts WHERE account_code = 'TEST-ACC-002') THEN
                INSERT INTO core.accounts(account_master_id, account_code, currency_code, account_name)
                SELECT core.get_account_master_id_by_account_master_code('BSA'), 'TEST-ACC-002', 'NPR', 'Test Mock Account 2';
        END IF;

        IF NOT EXISTS(SELECT 1 FROM core.accounts WHERE account_code = 'TEST-ACC-003') THEN
                INSERT INTO core.accounts(account_master_id, account_code, currency_code, account_name)
                SELECT core.get_account_master_id_by_account_master_code('BSA'), 'TEST-ACC-003', 'NPR', 'Test Mock Account 3';
        END IF;

        IF NOT EXISTS(SELECT 1 FROM core.accounts WHERE account_code = 'TEST-ACC-004') THEN
                INSERT INTO core.accounts(account_master_id, account_code, currency_code, account_name)
                SELECT core.get_account_master_id_by_account_master_code('BSA'), 'TEST-ACC-004', 'NPR', 'Test Mock Account 4';
        END IF;

        IF NOT EXISTS(SELECT 1 FROM core.accounts WHERE account_code = 'TEST-ACC-005') THEN
                INSERT INTO core.accounts(account_master_id, account_code, currency_code, account_name)
                SELECT core.get_account_master_id_by_account_master_code('BSA'), 'TEST-ACC-005', 'NPR', 'Test Mock Account 5';
        END IF;

END
$$
LANGUAGE plpgsql;
