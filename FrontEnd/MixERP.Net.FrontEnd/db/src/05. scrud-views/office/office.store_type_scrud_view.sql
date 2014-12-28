CREATE VIEW office.store_type_scrud_view
AS 

SELECT 
  store_types.store_type_id, 
  store_types.store_type_code, 
  store_types.store_type_name
FROM 
  office.store_types;
