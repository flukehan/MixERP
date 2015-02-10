DROP VIEW IF EXISTS core.price_type_selector_view;
CREATE VIEW core.price_type_selector_view
AS
SELECT 
  price_types.price_type_id, 
  price_types.price_type_code, 
  price_types.price_type_name
FROM 
  core.price_types;
