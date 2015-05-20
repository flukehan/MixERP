CREATE VIEW core.state_scrud_view
AS
SELECT 
  core.states.state_id, 
  core.countries.country_code || '('|| core.countries.country_name||')' AS country_name, 
  core.states.state_code, 
  core.states.state_name
FROM 
  core.states
INNER JOIN core.countries
ON core.states.country_id = core.countries.country_id; 




