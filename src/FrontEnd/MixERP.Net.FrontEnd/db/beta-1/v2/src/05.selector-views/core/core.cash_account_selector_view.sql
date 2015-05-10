DROP VIEW IF EXISTS core.cash_account_selector_view;

CREATE VIEW core.cash_account_selector_view
AS
SELECT * FROM core.account_scrud_view
WHERE account_master_id = 10101
ORDER BY account_id;