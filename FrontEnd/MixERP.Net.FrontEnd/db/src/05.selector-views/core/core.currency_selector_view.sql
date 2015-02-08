DROP VIEW IF EXISTS core.currency_selector_view;

CREATE VIEW core.currency_selector_view
AS
SELECT * FROM core.currencies;