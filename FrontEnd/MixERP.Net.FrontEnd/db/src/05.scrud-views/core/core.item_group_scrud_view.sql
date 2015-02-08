CREATE VIEW core.item_group_scrud_view
AS
SELECT 
        core.item_groups.item_group_id,
        core.item_groups.item_group_code,
        core.item_groups.item_group_name,
        core.item_groups.exclude_from_purchase,
        core.item_groups.exclude_from_sales,
        sales_tax_code || ' (' || sales_tax_name || ')' AS sales_tax,
        parent_item_group.item_group_code || ' (' || parent_item_group.item_group_name || ')' AS parent        
FROM core.item_groups
INNER JOIN core.sales_taxes
ON core.item_groups.sales_tax_id = sales_taxes.sales_tax_id
LEFT JOIN core.item_groups AS parent_item_group
ON core.item_groups.parent_item_group_id = parent_item_group.item_group_id;
