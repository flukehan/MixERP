DROP FUNCTION IF EXISTS core.get_inventory_account_id(_item_id integer);

CREATE FUNCTION core.get_inventory_account_id(_item_id integer)
RETURNS integer
AS
$$
BEGIN
    RETURN
        inventory_account_id
    FROM core.item_groups
    INNER JOIN core.items
    ON core.item_groups.item_group_id = core.items.item_group_id
    WHERE core.items.item_id = $1;    
END
$$
LANGUAGE plpgsql;