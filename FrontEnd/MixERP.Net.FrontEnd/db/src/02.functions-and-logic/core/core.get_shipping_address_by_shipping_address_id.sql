DROP FUNCTION IF EXISTS core.get_shipping_address_by_shipping_address_id(bigint);

CREATE FUNCTION core.get_shipping_address_by_shipping_address_id(bigint)
RETURNS text
AS
$$
BEGIN
        IF($1 IS NULL OR $1 <=0) THEN
                RETURN '';
        END IF;


        RETURN
                core.append_if_not_null(po_box, '&lt;br /&gt;') || 
                core.append_if_not_null(address_line_1, '&lt;br /&gt;') || 
                core.append_if_not_null(address_line_2, '&lt;br /&gt;') || 
                core.append_if_not_null(street, '&lt;br /&gt;') ||
                city  || '&lt;br /&gt;' ||
                state  || '&lt;br /&gt;' ||
                country 
        FROM core.shipping_addresses
        WHERE shipping_address_id=$1;
        
END
$$
LANGUAGE plpgsql;

--SELECT core.get_shipping_address_by_shipping_address_id(1);

