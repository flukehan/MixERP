CREATE OR REPLACE VIEW office.role_view
AS
SELECT 
  roles.role_id, 
  roles.role_code, 
  roles.role_name
FROM 
  office.roles;
	
CREATE VIEW core.compound_unit_view
AS
SELECT
	compound_unit_id,
	base_unit.unit_name base_unit_name,
	value,
	compare_unit.unit_name compare_unit_name
FROM
	core.compound_units,
	core.units base_unit,
	core.units compare_unit
WHERE
	core.compound_units.base_unit_id = base_unit.unit_id
AND
	core.compound_units.compare_unit_id = compare_unit.unit_id;

