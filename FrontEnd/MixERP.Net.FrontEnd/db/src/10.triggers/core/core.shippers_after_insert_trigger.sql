CREATE FUNCTION core.shippers_after_insert_trigger()
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

CREATE TRIGGER shippers_after_insert_trigger
AFTER INSERT
ON core.shippers
FOR EACH ROW EXECUTE PROCEDURE core.shippers_after_insert_trigger();
