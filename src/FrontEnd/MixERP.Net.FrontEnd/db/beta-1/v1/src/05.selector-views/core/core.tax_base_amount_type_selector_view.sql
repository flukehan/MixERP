DROP VIEW IF EXISTS core.tax_base_amount_type_selector_view;

CREATE VIEW core.tax_base_amount_type_selector_view
AS
SELECT * FROM core.tax_base_amount_types;
