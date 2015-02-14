DROP FUNCTION IF EXISTS core.items_unit_check_trigger() CASCADE;

CREATE FUNCTION core.items_unit_check_trigger()
RETURNS TRIGGER
AS
$$        
BEGIN
        IF(core.get_root_unit_id(NEW.unit_id) != core.get_root_unit_id(NEW.reorder_unit_id)) THEN
            RAISE EXCEPTION 'The reorder unit is incompatible with the base unit.'
            USING ERRCODE='P3054';
        END IF;
        RETURN NEW;
END
$$
LANGUAGE plpgsql;

CREATE TRIGGER items_unit_check_trigger
AFTER INSERT OR UPDATE
ON core.items
FOR EACH ROW EXECUTE PROCEDURE core.items_unit_check_trigger();