DROP FUNCTION IF EXISTS unit_tests.create_dummy_item_types();

CREATE FUNCTION unit_tests.create_dummy_item_types()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.item_types WHERE item_type_code='dummy-it01') THEN
        INSERT INTO core.item_types(item_type_code, item_type_name)
        SELECT 'dummy-it01', 'Test Mock Item Type';
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

