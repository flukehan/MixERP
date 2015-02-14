DROP FUNCTION IF EXISTS unit_tests.create_dummy_units();

CREATE FUNCTION unit_tests.create_dummy_units()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.units WHERE unit_code='dummy-uni01') THEN        
        INSERT INTO core.units(unit_code, unit_name)
        SELECT 'dummy-uni01', 'Test Mock Unit';
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

