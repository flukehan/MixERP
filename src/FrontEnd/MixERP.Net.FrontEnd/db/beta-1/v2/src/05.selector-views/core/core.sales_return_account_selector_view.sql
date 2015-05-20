DROP VIEW IF EXISTS core.sales_return_account_selector_view;

CREATE VIEW core.sales_return_account_selector_view
AS
SELECT * FROM core.account_scrud_view
WHERE account_master_id >= 15000
AND account_master_id <= 15100
ORDER BY account_id; --Liabilities
