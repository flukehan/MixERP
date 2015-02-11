DROP FUNCTION IF EXISTS core.get_currency_code_by_party_code(national character varying(12));

CREATE FUNCTION core.get_currency_code_by_party_code(_party_code national character varying(12))
RETURNS text
STABLE
AS
$$
BEGIN
    RETURN core.accounts.currency_code
    FROM core.accounts
    INNER JOIN core.parties
    ON core.accounts.account_id = core.parties.account_id
    AND core.parties.party_code=$1;
END
$$
LANGUAGE plpgsql;

