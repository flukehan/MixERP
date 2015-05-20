DROP FUNCTION IF EXISTS unit_tests.check_party_currency_code_mismatch();

CREATE FUNCTION unit_tests.check_party_currency_code_mismatch()
RETURNS public.test_result
AS
$$
    DECLARE message public.test_result;
BEGIN
    IF EXISTS
    (
        SELECT party_code FROM core.parties
        INNER JOIN core.accounts
        ON core.parties.account_id = core.accounts.account_id
        WHERE core.parties.currency_code != core.accounts.currency_code
        LIMIT 1
    ) THEN
        SELECT assert.fail('Some party accounts have different currency setup on their mapped GL heads.') INTO message;
        RETURN message;
    END IF;

    SELECT assert.ok('End of test.') INTO message;  
    RETURN message;
END
$$
LANGUAGE plpgsql;

