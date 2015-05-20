DROP FUNCTION IF EXISTS core.get_currency_code_by_party_id(bigint);

CREATE FUNCTION core.get_currency_code_by_party_id(party_id bigint)
RETURNS text
STABLE
AS
$$
BEGIN
    RETURN core.accounts.currency_code
    FROM core.accounts
    INNER JOIN core.parties
    ON core.accounts.account_id = core.parties.account_id
    AND core.parties.party_id=$1;
END
$$
LANGUAGE plpgsql;
