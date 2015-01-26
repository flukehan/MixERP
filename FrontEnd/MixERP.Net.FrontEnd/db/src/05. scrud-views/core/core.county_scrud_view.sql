CREATE VIEW core.county_scrud_view
AS
SELECT 
  core.counties.county_id, 
  core.counties.county_code, 
  core.counties.county_name, 
  core.states.state_code || '('|| core.states.state_name||')' AS state
FROM 
  core.counties
INNER JOIN  core.states
ON core.counties.state_id = core.states.state_id;


