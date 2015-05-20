DROP FUNCTION IF EXISTS core.get_item_group_id_by_item_id(integer);

CREATE FUNCTION core.get_item_group_id_by_item_id(integer)
RETURNS integer
AS
$$
BEGIN
        RETURN item_group_id
        FROM core.items
        WHERE item_id=$1;
END
$$
LANGUAGE plpgsql;

