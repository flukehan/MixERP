CREATE VIEW core.tax_exempt_type_scrud_view
AS
SELECT 
  tax_exempt_types.tax_exempt_type_id, 
  tax_exempt_types.tax_exempt_type_code, 
  tax_exempt_types.tax_exempt_type_name
FROM 
  core.tax_exempt_types;
