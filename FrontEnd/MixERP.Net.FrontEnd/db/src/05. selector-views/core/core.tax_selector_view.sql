DROP VIEW IF EXISTS core.tax_selector_view;

CREATE VIEW core.tax_selector_view
AS
SELECT * FROM core.sales_taxes;
