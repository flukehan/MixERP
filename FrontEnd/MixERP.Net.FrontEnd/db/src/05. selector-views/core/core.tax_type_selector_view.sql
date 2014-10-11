DROP VIEW IF EXISTS core.tax_type_selector_view;

CREATE VIEW core.tax_type_selector_view
AS
SELECT * FROM core.tax_types;