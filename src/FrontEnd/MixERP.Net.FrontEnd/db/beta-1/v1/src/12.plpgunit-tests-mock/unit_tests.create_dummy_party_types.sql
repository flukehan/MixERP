DROP FUNCTION IF EXISTS unit_tests.create_dummy_party_types();

CREATE FUNCTION unit_tests.create_dummy_party_types()
RETURNS void
AS
$$
    DECLARE _dummy_account_id bigint;
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.party_types WHERE party_type_code='dummy-pt01') THEN        
        _dummy_account_id := core.get_account_id_by_account_number('dummy-acc01');

        INSERT INTO core.party_types(party_type_code, party_type_name, is_supplier, account_id)
        SELECT 'dummy-pt01', 'Test Mock Party Type', false, _dummy_account_id;
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

