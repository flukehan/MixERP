DROP FUNCTION IF EXISTS core.get_account_id_by_party_type_id(_party_type_id integer);

CREATE FUNCTION core.get_account_id_by_party_type_id(_party_type_id integer)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN account_id
    FROM core.party_types
    WHERE party_type_id=$1;
END;
$$
LANGUAGE plpgsql;