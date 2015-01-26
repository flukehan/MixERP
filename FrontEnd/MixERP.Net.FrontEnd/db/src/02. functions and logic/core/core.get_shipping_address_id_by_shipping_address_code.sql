
CREATE FUNCTION core.get_shipping_address_id_by_shipping_address_code(text, bigint)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN
    (
        SELECT
            shipping_address_id
        FROM
            core.shipping_addresses
        WHERE 
            core.shipping_addresses.shipping_address_code=$1
        AND
            core.shipping_addresses.party_id=$2
    );
END
$$
LANGUAGE plpgsql;
