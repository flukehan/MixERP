DROP VIEW IF EXISTS core.purcahse_account_selector_view;

CREATE VIEW core.purcahse_account_selector_view
AS
SELECT * FROM core.account_scrud_view
WHERE account_master_id = 2 --Profit and Loss Account
AND COALESCE(parent, '') <> ''
ORDER BY account_id;
