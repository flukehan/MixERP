DROP FUNCTION IF EXISTS core.get_state_id_by_shipping_address_code(text, bigint);

CREATE FUNCTION core.get_state_id_by_shipping_address_code(text, bigint)
RETURNS integer
AS
$$
BEGIN
    RETURN
    (
        SELECT
            state_id
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
