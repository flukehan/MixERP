DROP VIEW IF EXISTS core.bank_account_selector_view;

CREATE VIEW core.bank_account_selector_view
AS
SELECT * FROM core.account_scrud_view
WHERE account_master_id = 10102
ORDER BY account_id;