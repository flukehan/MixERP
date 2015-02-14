DROP FUNCTION IF EXISTS core.get_party_type_id_by_party_type_code(text);

CREATE FUNCTION core.get_party_type_id_by_party_type_code(text)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN party_type_id
    FROM core.party_types
    WHERE party_type_code=$1;
END
$$
LANGUAGE plpgsql;