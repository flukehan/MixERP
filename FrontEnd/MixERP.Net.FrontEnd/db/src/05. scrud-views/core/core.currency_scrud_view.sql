CREATE VIEW core.currency_scrud_view
AS
SELECT 
  currencies.currency_code, 
  currencies.currency_symbol, 
  currencies.currency_name, 
  currencies.hundredth_name
FROM 
  core.currencies;
