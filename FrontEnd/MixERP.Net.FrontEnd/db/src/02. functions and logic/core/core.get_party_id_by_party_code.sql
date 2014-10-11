CREATE FUNCTION core.get_party_id_by_party_code(text)
RETURNS bigint
AS
$$
BEGIN
    RETURN
    (
        SELECT
            party_id
        FROM
            core.parties
        WHERE 
            core.parties.party_code=$1
    );
END
$$
LANGUAGE plpgsql;
