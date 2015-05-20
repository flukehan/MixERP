DROP VIEW IF EXISTS core.salesperson_account_selector_view;

CREATE VIEW core.salesperson_account_selector_view
AS
SELECT * FROM core.account_scrud_view
--Current Assets, Accounts Receivable, Current Liabilities, Accounts Payable
WHERE account_master_id = ANY(ARRAY[10100, 10110, 15000, 15010])
ORDER BY account_id;