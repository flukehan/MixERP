CREATE VIEW core.county_sales_tax_scrud_view
AS
SELECT 
    core.county_sales_taxes.county_sales_tax_id,   
    core.county_sales_taxes.county_sales_tax_code,
    core.county_sales_taxes.county_sales_tax_name,
    core.counties.county_code ||'('||  core.counties.county_name || ')' AS county,
    core.entities.entity_name,
    core.industries.industry_name,
    core.item_groups.item_group_code ||'(' ||  core.item_groups.item_group_name  || ')' AS item_group,
    core.county_sales_taxes.rate
FROM
    core.county_sales_taxes
LEFT JOIN core.counties
ON core.county_sales_taxes.county_id=core.counties.county_id
LEFT JOIN core.entities
ON core.county_sales_taxes.entity_id=core.entities.entity_id
LEFT JOIN core.industries
ON core.county_sales_taxes.industry_id=core.industries.industry_id
LEFT JOIN core.item_groups
ON core.county_sales_taxes.item_group_id=core.item_groups.item_group_id;

