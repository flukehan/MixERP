DROP FUNCTION IF EXISTS unit_tests.create_dummy_cost_centers();

CREATE FUNCTION unit_tests.create_dummy_cost_centers()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM office.cost_centers WHERE cost_center_code='dummy-cs01') THEN        
        INSERT INTO office.cost_centers(cost_center_code, cost_center_name)
        SELECT 'dummy-cs01', 'Test Mock Cost Center';
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

