CREATE FUNCTION core.is_supplier(bigint)
RETURNS boolean
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT 1 FROM core.parties 
        INNER JOIN core.party_types 
        ON core.parties.party_type_id=core.party_types.party_type_id
        WHERE core.parties.party_id=$1
        AND core.party_types.is_supplier=true
    ) THEN
        RETURN true;
    END IF;
    
    RETURN false;
END
$$
LANGUAGE plpgsql;

