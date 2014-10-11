DROP FUNCTION IF EXISTS core.get_item_group_id_by_item_group_code(text);

CREATE FUNCTION core.get_item_group_id_by_item_group_code(text)
RETURNS integer
AS
$$
BEGIN
        RETURN item_group_id
        FROM core.item_groups
        WHERE item_group_code=$1;
END
$$
LANGUAGE plpgsql;

