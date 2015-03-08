DROP FUNCTION IF EXISTS unit_tests.create_dummy_parties();

CREATE FUNCTION unit_tests.create_dummy_parties()
RETURNS void
AS
$$
    DECLARE _dummy_account_id   bigint;
    DECLARE _party_id           bigint;
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.parties WHERE party_code='dummy-pr01') THEN        
        _dummy_account_id := core.get_account_id_by_account_number('dummy-acc01');

        INSERT INTO core.parties(party_type_id, first_name, last_name, country_id, state_id, currency_code, account_id)
        SELECT            
            core.get_party_type_id_by_party_type_code('dummy-pt01'), 
            'Test Mock party', 
            'Test', 
            core.get_country_id_by_country_code('dummy-co01'),
            core.get_state_id_by_state_code('dummy-st01'),
            'NPR',
            _dummy_account_id
       RETURNING party_id INTO _party_id;

    UPDATE core.parties
    SET party_code = 'dummy-pr01'
    WHERE party_id = _party_id;
       
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

