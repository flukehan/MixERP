DROP FUNCTION IF EXISTS core.get_email_address_by_party_id(bigint);

CREATE FUNCTION core.get_email_address_by_party_id(bigint)
RETURNS TEXT
STABLE
AS
$$
BEGIN
    RETURN email 
    FROM core.parties
    WHERE party_id=$1;
END
$$
LANGUAGE plpgsql;