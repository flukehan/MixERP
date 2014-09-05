CREATE FUNCTION core.update_shipper_code()
RETURNS trigger
AS
$$
BEGIN
	UPDATE core.shippers
	SET 
		shipper_code=core.get_shipper_code(NEW.company_name)
	WHERE core.shippers.shipper_id=NEW.shipper_id;
	
	RETURN NEW;
END
$$
LANGUAGE plpgsql;

CREATE TRIGGER update_shipper_code
AFTER INSERT
ON core.shippers
FOR EACH ROW EXECUTE PROCEDURE core.update_shipper_code();
