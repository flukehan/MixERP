CREATE VIEW core.fiscal_year_scrud_view
AS
SELECT 
  core.fiscal_year.fiscal_year_code, 
  core.fiscal_year.fiscal_year_name, 
  core.fiscal_year.starts_from, 
  core.fiscal_year.ends_on
FROM 
  core.fiscal_year;
