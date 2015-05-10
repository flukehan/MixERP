DROP VIEW IF EXISTS core.cost_of_sales_account_selector_view;

CREATE VIEW core.cost_of_sales_account_selector_view
AS
SELECT * FROM core.account_scrud_view
--Cost of Sales
WHERE account_master_id = 20400
ORDER BY account_id;