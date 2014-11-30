DROP FUNCTION IF EXISTS core.get_party_type_id_by_party_id(_party_id bigint);

CREATE FUNCTION core.get_party_type_id_by_party_id(_party_id bigint)
RETURNS integer
AS
$$
BEGIN
    RETURN party_type_id
    FROM core.parties
    WHERE party_id=$1;
END
$$
LANGUAGE plpgsql;

