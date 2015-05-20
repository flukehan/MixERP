DROP FUNCTION IF EXISTS core.get_shipping_address_code_by_shipping_address_id(bigint);

CREATE FUNCTION core.get_shipping_address_code_by_shipping_address_id(bigint)
RETURNS text
AS
$$
BEGIN
    RETURN
    (
        SELECT
            shipping_address_code
        FROM
            core.shipping_addresses
        WHERE 
            core.shipping_addresses.shipping_address_id=$1
    );
END
$$
LANGUAGE plpgsql;
