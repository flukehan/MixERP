DROP FUNCTION IF EXISTS unit_tests.create_dummy_store_types();

CREATE FUNCTION unit_tests.create_dummy_store_types()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM office.store_types WHERE store_type_code='dummy-st01') THEN
        INSERT INTO office.store_types(store_type_code, store_type_name)
        SELECT 'dummy-st01', 'Test Mock Store Type';
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

