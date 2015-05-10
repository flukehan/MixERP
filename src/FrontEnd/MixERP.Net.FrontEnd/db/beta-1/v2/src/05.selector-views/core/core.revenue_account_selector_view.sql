DROP VIEW IF EXISTS core.revenue_account_selector_view;

CREATE VIEW core.revenue_account_selector_view
AS
SELECT * FROM core.account_scrud_view
WHERE account_master_id = 20100
ORDER BY account_id;