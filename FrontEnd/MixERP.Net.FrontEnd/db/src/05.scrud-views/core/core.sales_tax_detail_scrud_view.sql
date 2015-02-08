CREATE VIEW core.sales_tax_detail_scrud_view
AS
SELECT 
  core.sales_tax_details.sales_tax_detail_id, 
  core.sales_taxes.sales_tax_code || ' ('|| core.sales_taxes.sales_tax_name||')' AS sales_tax,
  core.sales_tax_types.sales_tax_type_code || ' ('|| core.sales_tax_types.sales_tax_type_name||')' AS sales_tax_type, 
  core.sales_tax_details.priority,
  core.sales_tax_details.sales_tax_detail_code, 
  core.sales_tax_details.sales_tax_detail_name, 
  core.sales_tax_details.based_on_shipping_address, 
  core.sales_tax_details.check_nexus, 
  core.sales_tax_details.applied_on_shipping_charge, 
  core.state_sales_taxes.state_sales_tax_code || ' ('|| core.state_sales_taxes.state_sales_tax_name||')' AS state_sales_tax, 
  core.county_sales_taxes.county_sales_tax_code || ' (' || core.county_sales_taxes.county_sales_tax_name||')' AS county_sales_tax, 
  core.tax_rate_types.tax_rate_type_code || '('|| core.tax_rate_types.tax_rate_type_name||')' AS tax_rate_type,  
  core.sales_tax_details.rate,
  reporting_tax_authority.tax_authority_code || ' (' || reporting_tax_authority.tax_authority_name||')' AS reporting_tax_authority, 
  collecting_tax_authority.tax_authority_code || ' (' || collecting_tax_authority.tax_authority_name||')' AS collecting_tax_authority,
  collecting_account.account_number || '  ('|| collecting_account.account_name||')' AS collecting_account,
  use_tax_collecting_account.account_number || '  ('|| use_tax_collecting_account.account_name||')' AS use_tax_collecting_account,
  core.rounding_methods.rounding_method_code || '('|| core.rounding_methods.rounding_method_name||')' AS rounding_method,
  core.sales_tax_details.rounding_decimal_places

FROM 
   core.sales_tax_details
INNER JOIN core.sales_taxes
ON core.sales_tax_details.sales_tax_id = core.sales_taxes.sales_tax_id 
INNER JOIN core.sales_tax_types
ON core.sales_tax_details.sales_tax_type_id = core.sales_tax_types.sales_tax_type_id
LEFT JOIN core.state_sales_taxes
ON core.sales_tax_details.state_sales_tax_id = core.state_sales_taxes.state_sales_tax_id
LEFT JOIN core.county_sales_taxes
ON core.sales_tax_details.county_sales_tax_id = core.county_sales_taxes.county_sales_tax_id
INNER JOIN core.tax_rate_types
ON  core.sales_tax_details.tax_rate_type_code  = core.tax_rate_types.tax_rate_type_code
INNER JOIN core.tax_authorities AS reporting_tax_authority
ON core.sales_tax_details.reporting_tax_authority_id = reporting_tax_authority.tax_authority_id
INNER JOIN core.tax_authorities AS collecting_tax_authority
ON core.sales_tax_details.collecting_tax_authority_id = collecting_tax_authority.tax_authority_id
INNER JOIN core.accounts AS collecting_account
ON core.sales_tax_details.collecting_account_id = collecting_account.account_id
LEFT JOIN core.accounts AS use_tax_collecting_account
ON core.sales_tax_details.use_tax_collecting_account_id = use_tax_collecting_account.account_id
LEFT JOIN core.rounding_methods
ON core.sales_tax_details.rounding_method_code = core.rounding_methods.rounding_method_code;

