CREATE VIEW core.sales_tax_type_scrud_view
AS
SELECT 
  core.sales_tax_types.sales_tax_type_id, 
  core.sales_tax_types.sales_tax_type_code, 
  core.sales_tax_types.sales_tax_type_name, 
  core.sales_tax_types.is_vat
FROM 
  core.sales_tax_types;
