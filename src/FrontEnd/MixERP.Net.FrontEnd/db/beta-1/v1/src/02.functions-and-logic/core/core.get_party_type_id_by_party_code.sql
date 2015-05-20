CREATE FUNCTION core.get_party_type_id_by_party_code(text)
RETURNS integer
AS
$$
BEGIN
    RETURN
    (
        SELECT
            party_type_id
        FROM
            core.parties
        WHERE 
            core.parties.party_code=$1
    );
END
$$
LANGUAGE plpgsql;
