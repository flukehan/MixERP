CREATE VIEW core.sales_tax_scrud_view
AS
SELECT 
  core.sales_taxes.sales_tax_id, 
  core.tax_master.tax_master_code || ' (' || core.tax_master.tax_master_name||')' AS tax_master, 
  office.offices.office_code || ' (' || offices.office_name||')' AS office, 
  core.sales_taxes.sales_tax_code, 
  core.sales_taxes.sales_tax_name, 
  core.sales_taxes.is_exemption, 
  core.tax_base_amount_types.tax_base_amount_type_code || '('|| core.tax_base_amount_types.tax_base_amount_type_name||')' AS tax_base_amount,
  core.sales_taxes.rate
FROM 
  core.sales_taxes 
INNER JOIN core.tax_master
ON sales_taxes.tax_master_id = tax_master.tax_master_id
INNER JOIN office.offices
ON  sales_taxes.office_id = offices.office_id 
INNER JOIN core.tax_base_amount_types 
ON sales_taxes.tax_base_amount_type_code = tax_base_amount_types.tax_base_amount_type_code;



