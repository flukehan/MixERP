CREATE VIEW core.state_sales_tax_scrud_view
AS
SELECT 
    core.state_sales_taxes.state_sales_tax_id,   
    core.state_sales_taxes.state_sales_tax_code,
    core.state_sales_taxes.state_sales_tax_name,
    core.states.state_code || ' (' ||  core.states.state_name || ')' AS state,
    core.entities.entity_name,
    core.industries.industry_name,
    core.item_groups.item_group_code || ' (' ||  core.item_groups.item_group_name || ')' AS item_group,
    core.state_sales_taxes.rate
FROM
    core.state_sales_taxes
INNER JOIN core.states
ON core.state_sales_taxes.state_id=core.states.state_id
LEFT JOIN core.entities
ON core.state_sales_taxes.entity_id=core.entities.entity_id
LEFT JOIN core.industries
ON core.state_sales_taxes.industry_id=core.industries.industry_id
LEFT JOIN core.item_groups
ON core.state_sales_taxes.item_group_id=core.item_groups.item_group_id;

