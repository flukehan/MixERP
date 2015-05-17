DROP FUNCTION IF EXISTS core.get_party_code_by_party_id(bigint);

CREATE FUNCTION core.get_party_code_by_party_id(bigint)
RETURNS text
AS
$$
BEGIN
    RETURN
    (
        SELECT
            party_code
        FROM
            core.parties
        WHERE 
            core.parties.party_id=$1
    );
END
$$
LANGUAGE plpgsql;
