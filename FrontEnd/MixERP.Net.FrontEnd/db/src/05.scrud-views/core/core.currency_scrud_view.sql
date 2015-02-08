DROP VIEW IF EXISTS core.currency_scrud_view;
CREATE VIEW core.currency_scrud_view
AS
SELECT 
  core.currencies.currency_code, 
  core.currencies.currency_symbol, 
  core.currencies.currency_name, 
  core.currencies.hundredth_name
FROM 
  core.currencies;
