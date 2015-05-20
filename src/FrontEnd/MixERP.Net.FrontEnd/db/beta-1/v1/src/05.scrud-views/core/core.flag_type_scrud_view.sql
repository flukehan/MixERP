CREATE VIEW core.flag_type_scrud_view
AS
SELECT 
  flag_types.flag_type_id, 
  flag_types.flag_type_name, 
  flag_types.background_color, 
  flag_types.foreground_color
FROM 
  core.flag_types;
