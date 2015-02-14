DROP FUNCTION IF EXISTS unit_tests.create_dummy_item_groups();

CREATE FUNCTION unit_tests.create_dummy_item_groups()
RETURNS void
AS
$$
    DECLARE _dummy_account_id bigint;
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.item_groups WHERE item_group_code='dummy-ig01') THEN

        _dummy_account_id := core.get_account_id_by_account_number('dummy-acc01');
        
        INSERT INTO core.item_groups(item_group_code, item_group_name, sales_tax_id, sales_account_id, sales_discount_account_id, sales_return_account_id, purchase_account_id, purchase_discount_account_id, inventory_account_id, cost_of_goods_sold_account_id)
        SELECT 'dummy-ig01', 'Test Mock Item Group', core.get_sales_tax_id_by_sales_tax_code('dummy-stx01'), _dummy_account_id, _dummy_account_id, _dummy_account_id, _dummy_account_id, _dummy_account_id, _dummy_account_id, _dummy_account_id;
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

