CREATE FUNCTION core.update_shipping_address_code_trigger()
RETURNS TRIGGER
AS
$$
DECLARE _counter integer;
BEGIN
    IF TG_OP='INSERT' THEN

        SELECT COALESCE(MAX(shipping_address_code::integer), 0) + 1
        INTO _counter
        FROM core.shipping_addresses
        WHERE party_id=NEW.party_id;

        NEW.shipping_address_code := trim(to_char(_counter, '000'));
        
        RETURN NEW;
    END IF;
END
$$
LANGUAGE plpgsql;


CREATE TRIGGER update_shipping_address_code_trigger
BEFORE INSERT
ON core.shipping_addresses
FOR EACH ROW EXECUTE PROCEDURE core.update_shipping_address_code_trigger();

