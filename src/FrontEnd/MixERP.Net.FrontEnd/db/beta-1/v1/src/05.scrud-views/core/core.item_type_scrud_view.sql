DROP VIEW IF EXISTS core.item_type_scrud_view;
CREATE VIEW core.item_type_scrud_view
AS
SELECT 
  core.item_types.item_type_id, 
  core.item_types.item_type_code, 
  core.item_types.item_type_name
FROM 
  core.item_types;
