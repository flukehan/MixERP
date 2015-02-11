DROP FUNCTION IF EXISTS unit_tests.create_dummy_brands();

CREATE FUNCTION unit_tests.create_dummy_brands()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.brands WHERE brand_code='dummy-br01') THEN        
        INSERT INTO core.brands(brand_code, brand_name)
        SELECT 'dummy-br01', 'Test Mock Brand';
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

