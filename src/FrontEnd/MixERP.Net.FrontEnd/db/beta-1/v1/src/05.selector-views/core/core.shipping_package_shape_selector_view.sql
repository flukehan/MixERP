DROP VIEW IF EXISTS core.shipping_package_shape_selector_view;

CREATE VIEW core.shipping_package_shape_selector_view
AS
SELECT 
  shipping_package_shapes.shipping_package_shape_id, 
  shipping_package_shapes.shipping_package_shape_code, 
  shipping_package_shapes.shipping_package_shape_name, 
  shipping_package_shapes.is_rectangular
FROM 
  core.shipping_package_shapes;
