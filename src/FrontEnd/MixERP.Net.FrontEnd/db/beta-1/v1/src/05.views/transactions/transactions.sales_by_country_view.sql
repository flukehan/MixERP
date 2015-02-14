CREATE VIEW transactions.sales_by_country_view
AS
WITH country_data
AS
(
SELECT country_id, SUM((price * quantity) - discount + tax + shipping_charge) AS sales
FROM transactions.verified_stock_transaction_view
WHERE book = ANY(ARRAY['Sales.Delivery', 'Sales.Direct'])
GROUP BY country_id
)

SELECT country_code, sales 
FROM country_data
INNER JOIN core.countries
ON country_data.country_id = core.countries.country_id;