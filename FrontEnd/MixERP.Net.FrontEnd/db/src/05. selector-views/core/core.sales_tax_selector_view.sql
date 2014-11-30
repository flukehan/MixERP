DROP VIEW IF EXISTS core.sales_tax_selector_view;

CREATE VIEW core.sales_tax_selector_view
AS
SELECT * FROM core.sales_taxes;
