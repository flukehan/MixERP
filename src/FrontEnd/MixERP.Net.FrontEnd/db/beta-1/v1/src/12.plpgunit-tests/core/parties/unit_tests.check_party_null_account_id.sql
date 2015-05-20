DROP FUNCTION IF EXISTS unit_tests.check_party_null_account_id();

CREATE FUNCTION unit_tests.check_party_null_account_id()
RETURNS public.test_result
AS
$$
    DECLARE message test_result;
BEGIN
    IF EXISTS
    (
        SELECT party_code FROM core.parties
        WHERE core.parties.account_id IS NULL
        LIMIT 1
    ) THEN
        SELECT assert.fail('Some party accounts don''t have mapped GL heads.') INTO message;
        RETURN message;
    END IF;

    SELECT assert.ok('End of test.') INTO message;  
    RETURN message;
END
$$
LANGUAGE plpgsql;

