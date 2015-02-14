DROP FUNCTION IF EXISTS unit_tests.create_dummy_items();

CREATE FUNCTION unit_tests.create_dummy_items()
RETURNS void
AS
$$
    DECLARE _dummy_unit_id integer;
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.items WHERE item_code='dummy-it01') THEN

        _dummy_unit_id := core.get_unit_id_by_unit_code('dummy-uni01');
    
        INSERT INTO core.items
        (
            item_code, 
            item_name, 
            item_group_id,
            item_type_id,
            brand_id,
            preferred_supplier_id,
            lead_time_in_days,
            unit_id,
            hot_item,
            cost_price,
            selling_price,
            sales_tax_id,
            reorder_unit_id,
            reorder_level,
            reorder_quantity,
            maintain_stock
        )
        SELECT 
            'dummy-it01'                                            AS item_code, 
            'Test Mock Item'                                        AS item_name,
            core.get_item_group_id_by_item_group_code('dummy-ig01') AS item_group_id,
            core.get_item_type_id_by_item_type_code('dummy-it01')   AS item_type_id,
            core.get_brand_id_by_brand_code('dummy-br01')           AS brand_id, 
            core.get_party_id_by_party_code('dummy-pr01')           AS preferred_supplier_id,
            10                                                      AS lead_time,
            _dummy_unit_id                                          AS unit_id,
            false                                                   AS hot_item,
            3000                                                    AS cost_price,
            4000                                                    AS selling_price,
            core.get_sales_tax_id_by_sales_tax_code('dummy-stx01')  AS sales_tax_id,
            _dummy_unit_id                                          AS reorder_unit_id,
            10                                                      AS reorder_level,
            100                                                     AS reorder_quantity,
            false                                                   AS maintain_stock
        UNION ALL
        SELECT 
            'dummy-it02'                                            AS item_code, 
            'Test Mock Item2'                                       AS item_name,
            core.get_item_group_id_by_item_group_code('dummy-ig01') AS item_group_id,
            core.get_item_type_id_by_item_type_code('dummy-it01')   AS item_type_id,
            core.get_brand_id_by_brand_code('dummy-br01')           AS brand_id, 
            core.get_party_id_by_party_code('dummy-pr01')           AS preferred_supplier_id,
            17                                                      AS lead_time,
            _dummy_unit_id                                          AS unit_id,
            false                                                   AS hot_item,
            1400                                                    AS cost_price,
            1800                                                    AS selling_price,
            core.get_sales_tax_id_by_sales_tax_code('dummy-stx01')  AS sales_tax_id,
            _dummy_unit_id                                          AS reorder_unit_id,
            10                                                      AS reorder_level,
            50                                                      AS reorder_quantity,
            false                                                   AS maintain_stock;        
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

