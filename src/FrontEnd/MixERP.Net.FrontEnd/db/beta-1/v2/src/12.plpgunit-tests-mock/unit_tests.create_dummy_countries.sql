DROP FUNCTION IF EXISTS unit_tests.create_dummy_countries();

CREATE FUNCTION unit_tests.create_dummy_countries()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.countries WHERE country_code='dummy-co01') THEN        
        INSERT INTO core.countries(country_code, country_name)
        SELECT 'dummy-co01', 'Test Mock Country';
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

