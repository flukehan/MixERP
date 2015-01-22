DROP FUNCTION IF EXISTS core.get_entity_id_by_party_id(_party_id bigint);

CREATE FUNCTION core.get_entity_id_by_party_id(_party_id bigint)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN entity_id
    FROM core.parties
    WHERE party_id=$1;
END
$$
LANGUAGE plpgsql;

