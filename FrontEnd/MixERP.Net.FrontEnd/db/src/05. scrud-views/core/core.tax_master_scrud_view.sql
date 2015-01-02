CREATE VIEW core.tax_master_scrud_view
AS
SELECT 
  tax_master.tax_master_id, 
  tax_master.tax_master_code, 
  tax_master.tax_master_name
FROM 
  core.tax_master;
