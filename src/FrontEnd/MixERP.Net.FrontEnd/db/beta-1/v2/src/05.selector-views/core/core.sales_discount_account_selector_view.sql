DROP VIEW IF EXISTS core.sales_discount_account_selector_view;

CREATE VIEW core.sales_discount_account_selector_view
AS
SELECT * FROM core.account_scrud_view
WHERE account_master_id >= 20400
ORDER BY account_id; --Expenses