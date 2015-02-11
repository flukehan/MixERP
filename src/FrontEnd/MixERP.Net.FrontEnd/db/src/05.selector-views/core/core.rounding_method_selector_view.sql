DROP VIEW IF EXISTS core.rounding_method_selector_view;

CREATE VIEW core.rounding_method_selector_view
AS
SELECT * FROM core.rounding_methods;
