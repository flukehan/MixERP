DROP VIEW IF EXISTS core.shipping_package_shape_selector_view;

CREATE VIEW core.shipping_package_shape_selector_view
AS
SELECT * FROM core.shipping_package_shapes;