CREATE VIEW core.country_scrud_view
AS
SELECT 
  core.countries.country_id, 
  core.countries.country_code, 
  core.countries.country_name
FROM 
  core.countries;
