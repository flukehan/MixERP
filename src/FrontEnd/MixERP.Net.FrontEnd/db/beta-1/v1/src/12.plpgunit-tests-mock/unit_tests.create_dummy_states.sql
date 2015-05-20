DROP FUNCTION IF EXISTS unit_tests.create_dummy_states();

CREATE FUNCTION unit_tests.create_dummy_states()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.states WHERE state_code='dummy-st01') THEN        
        INSERT INTO core.states(state_code, state_name, country_id)
        SELECT 'dummy-st01', 'Test Mock State', core.get_country_id_by_country_code('dummy-co01');
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

