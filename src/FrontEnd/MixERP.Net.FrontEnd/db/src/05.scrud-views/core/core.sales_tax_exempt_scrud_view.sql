CREATE VIEW core.sales_tax_exempt_scrud_view
AS 
SELECT 
  core.sales_tax_exempts.sales_tax_exempt_id, 
  core.tax_master.tax_master_code || '('|| core.tax_master.tax_master_name||')' AS tax_master, 
  core.sales_tax_exempts.sales_tax_exempt_code, 
  core.sales_tax_exempts.sales_tax_exempt_name, 
  core.tax_exempt_types.tax_exempt_type_code || '('|| core.tax_exempt_types.tax_exempt_type_name||')' AS tax_exempt_type,
  office.stores.store_code || '('|| office.stores.store_name||')' AS store,
  core.sales_taxes.sales_tax_code || '('|| core.sales_taxes.sales_tax_name||')' AS sales_tax, 
  core.sales_tax_exempts.valid_from, 
  core.sales_tax_exempts.valid_till, 
  core.sales_tax_exempts.price_from, 
  core.sales_tax_exempts.price_to
FROM 
  core.sales_tax_exempts
INNER JOIN core.tax_master
ON core.sales_tax_exempts.tax_master_id = core.tax_master.tax_master_id
INNER JOIN core.tax_exempt_types
ON core.sales_tax_exempts.tax_exempt_type_id = core.tax_exempt_types.tax_exempt_type_id
INNER JOIN office.stores
ON core.sales_tax_exempts.store_id = office.stores.store_id
INNER JOIN core.sales_taxes
ON core.sales_tax_exempts.sales_tax_id = core.sales_taxes.sales_tax_id;
