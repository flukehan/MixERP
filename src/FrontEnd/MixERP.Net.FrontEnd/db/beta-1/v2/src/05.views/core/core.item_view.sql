DROP VIEW IF EXISTS core.item_view;

CREATE VIEW core.item_view
AS
SELECT 
        item_id,
        item_code,
        item_name,
        item_group_code || ' (' || item_group_name || ')' AS item_group,
        item_type_code || ' (' || item_type_name || ')' AS item_type,
        maintain_stock,
        brand_code || ' (' || brand_name || ')' AS brand,
        party_code || ' (' || party_name || ')' AS preferred_supplier,
        lead_time_in_days,
        weight_in_grams,
        width_in_centimeters,
        height_in_centimeters,
        length_in_centimeters,
        machinable,
        shipping_mail_type_code || ' (' || shipping_mail_type_name || ')' AS preferred_shipping_mail_type,
        shipping_package_shape_code || ' (' || shipping_package_shape_name || ')' AS preferred_shipping_package_shape,
        core.units.unit_code || ' (' || core.units.unit_name || ')' AS unit,
        base_unit.unit_code || ' (' || base_unit.unit_name || ')' AS base_unit,
        hot_item,
        cost_price,
        selling_price,
        selling_price_includes_tax,
        sales_tax_code || ' (' || sales_tax_name || ')' AS sales_tax,
        reorder_unit.unit_code || ' (' || reorder_unit.unit_name || ')' AS reorder_unit,
        reorder_level,
        reorder_quantity
FROM core.items
INNER JOIN core.item_groups
ON core.items.item_group_id = core.item_groups.item_group_id
INNER JOIN core.item_types
ON core.items.item_type_id = core.item_types.item_type_id
INNER JOIN core.brands
ON core.items.brand_id = core.brands.brand_id
INNER JOIN core.parties
ON core.items.preferred_supplier_id = core.parties.party_id
INNER JOIN core.units
ON core.items.unit_id = core.units.unit_id
INNER JOIN core.units AS base_unit
ON core.get_root_unit_id(core.items.unit_id) = core.units.unit_id
INNER JOIN core.units AS reorder_unit
ON core.items.reorder_unit_id = reorder_unit.unit_id
INNER JOIN core.sales_taxes
ON core.items.sales_tax_id = core.sales_taxes.sales_tax_id
LEFT JOIN core.shipping_mail_types
ON core.items.preferred_shipping_mail_type_id = core.shipping_mail_types.shipping_mail_type_id
LEFT JOIN core.shipping_package_shapes
ON core.items.shipping_package_shape_id = core.shipping_package_shapes.shipping_package_shape_id;