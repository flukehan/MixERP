DROP FUNCTION IF EXISTS unit_tests.create_dummy_salespersons();

CREATE FUNCTION unit_tests.create_dummy_salespersons()
RETURNS void
AS
$$
    DECLARE _dummy_account_id bigint;
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.salespersons WHERE salesperson_code='dummy-sp01') THEN        
        _dummy_account_id := core.get_account_id_by_account_number('dummy-acc01');

        INSERT INTO core.salespersons(salesperson_code, salesperson_name, sales_team_id, address, contact_number, account_id)
        SELECT 'dummy-sp01', 'Test Mock Salesperson', core.get_sales_team_id_by_sales_team_code('dummy-st01'), '', '', _dummy_account_id;
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

