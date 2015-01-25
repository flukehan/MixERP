CREATE VIEW core.sales_tax_exempt_detail_scrud_view
AS
SELECT 
  core.sales_tax_exempt_details.sales_tax_exempt_detail_id, 
  core.sales_tax_exempts.sales_tax_exempt_code || '('|| core.sales_tax_exempts.sales_tax_exempt_name ||')' AS sales_tax_exempt,  
  core.entities.entity_name, 
  core.industries.industry_name, 
  core.parties.party_code || '(' || core.parties.party_name ||')' AS party, 
  core.party_types.party_type_code || '('|| core.party_types.party_type_name ||')' AS party_type, 
  core.items.item_code || '('|| core.items.item_name ||')' AS item, 
  core.item_groups.item_group_code || '('|| core.item_groups.item_group_name ||')' AS item_group
FROM 
  core.sales_tax_exempt_details
LEFT JOIN core.sales_tax_exempts
ON core.sales_tax_exempt_details.sales_tax_exempt_id = core.sales_tax_exempts.sales_tax_exempt_id
LEFT JOIN core.entities
ON core.sales_tax_exempt_details.entity_id = core.entities.entity_id
LEFT JOIN core.industries
ON core.sales_tax_exempt_details.industry_id = core.industries.industry_id
LEFT JOIN  core.parties
ON core.sales_tax_exempt_details.party_id = core.parties.party_id
LEFT JOIN core.party_types
ON core.sales_tax_exempt_details.party_type_id = core.party_types.party_type_id
LEFT JOIN core.items
ON core.sales_tax_exempt_details.item_id = core.items.item_id
LEFT JOIN core.item_groups
ON core.sales_tax_exempt_details.item_group_id = core.item_groups.item_group_id;


