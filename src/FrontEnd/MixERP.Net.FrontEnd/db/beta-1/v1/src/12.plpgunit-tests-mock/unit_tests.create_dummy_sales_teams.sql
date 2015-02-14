DROP FUNCTION IF EXISTS unit_tests.create_dummy_sales_teams();

CREATE FUNCTION unit_tests.create_dummy_sales_teams()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.sales_teams WHERE sales_team_code='dummy-st01') THEN        
        INSERT INTO core.sales_teams(sales_team_code, sales_team_name)
        SELECT 'dummy-st01', 'Test Mock Sales Team';
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

