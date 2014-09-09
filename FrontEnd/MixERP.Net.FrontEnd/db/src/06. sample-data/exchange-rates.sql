INSERT INTO core.exchange_rates(office_id)
SELECT 2;

INSERT INTO core.exchange_rate_details(exchange_rate_id, local_currency_code, foreign_currency_code, unit, exchange_rate)
SELECT 1, 'NPR', 'USD', 1, 100.00; 

INSERT INTO core.exchange_rates(office_id)
SELECT 3;

INSERT INTO core.exchange_rate_details(exchange_rate_id, local_currency_code, foreign_currency_code, unit, exchange_rate)
SELECT 2, 'NPR', 'USD', 1, 100.00; 


