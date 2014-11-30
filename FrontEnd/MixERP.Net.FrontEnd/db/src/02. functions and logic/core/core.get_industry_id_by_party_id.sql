DROP FUNCTION IF EXISTS core.get_industry_id_by_party_id(_party_id bigint);

CREATE FUNCTION core.get_industry_id_by_party_id(_party_id bigint)
RETURNS integer
AS
$$
BEGIN
    RETURN industry_id
    FROM core.parties
    WHERE party_id=$1;
END
$$
LANGUAGE plpgsql;

