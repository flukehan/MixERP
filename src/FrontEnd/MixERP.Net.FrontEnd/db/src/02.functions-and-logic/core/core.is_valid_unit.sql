DROP FUNCTION IF EXISTS core.is_valid_unit(_item_id integer, _unit_id integer);

CREATE FUNCTION core.is_valid_unit(_item_id integer, _unit_id integer)
RETURNS boolean
AS
$$
        DECLARE _item_unit_id integer;
BEGIN
        SELECT unit_id INTO _item_unit_id
        FROM core.items
        WHERE item_id=$1;

        IF(core.get_root_unit_id(_item_unit_id) = core.get_root_unit_id(_unit_id)) THEN
                RETURN true;
        END IF;

        RETURN false;        
END
$$
LANGUAGE plpgsql;