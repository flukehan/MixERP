DROP FUNCTION IF EXISTS core.is_valid_unit_id(integer);

CREATE FUNCTION core.is_valid_unit_id(integer)
RETURNS boolean
AS
$$
BEGIN
        IF EXISTS(SELECT 1 FROM core.units WHERE unit_id=$1) THEN
                RETURN true;
        END IF;

        RETURN false;
END
$$
LANGUAGE plpgsql;

DROP FUNCTION IF EXISTS core.is_valid_unit_id(_unit_id integer, _item_id integer);

CREATE FUNCTION core.is_valid_unit_id(_unit_id integer, _item_id integer)
RETURNS boolean
AS
$$
BEGIN
        IF EXISTS
        (
                SELECT 1
                FROM core.items
                WHERE item_id = $2
                AND core.get_root_unit_id($1) = core.get_root_unit_id(unit_id)
        ) THEN
                RETURN true;
        END IF;

        RETURN false;
END
$$
LANGUAGE plpgsql;