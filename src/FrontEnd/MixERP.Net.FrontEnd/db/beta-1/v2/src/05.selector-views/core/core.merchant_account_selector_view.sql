DROP VIEW IF EXISTS core.merchant_account_selector_view;

CREATE VIEW core.merchant_account_selector_view
AS
SELECT * FROM core.bank_account_scrud_view
WHERE is_merchant_account = true
ORDER BY account_id;