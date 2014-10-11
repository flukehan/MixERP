CREATE FUNCTION core.get_account_id_by_party_code(party_code text)
RETURNS bigint
AS
$$
BEGIN
    RETURN
    (
        SELECT account_id
        FROM core.parties
        WHERE core.parties.party_code=$1
    );
END
$$
LANGUAGE plpgsql;
