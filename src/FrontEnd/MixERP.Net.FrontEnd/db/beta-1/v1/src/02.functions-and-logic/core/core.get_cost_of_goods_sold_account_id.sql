DROP FUNCTION IF EXISTS core.get_cost_of_goods_sold_account_id(_item_id integer);

CREATE FUNCTION core.get_cost_of_goods_sold_account_id(_item_id integer)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN
        cost_of_goods_sold_account_id
    FROM core.item_groups
    INNER JOIN core.items
    ON core.item_groups.item_group_id = core.items.item_group_id
    WHERE core.items.item_id = $1;    
END
$$
LANGUAGE plpgsql;