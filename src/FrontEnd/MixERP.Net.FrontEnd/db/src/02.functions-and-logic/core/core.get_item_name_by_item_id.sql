DROP FUNCTION IF EXISTS core.get_item_name_by_item_id(integer);

CREATE FUNCTION core.get_item_name_by_item_id(integer)
RETURNS text
AS
$$
BEGIN
        RETURN
                item_name
        FROM
                core.items
        WHERE item_id=$1;
END
$$
LANGUAGE plpgsql;

--SELECT core.get_item_name_by_item_id(1);