DROP VIEW IF EXISTS core.inventory_account_selector_view;

CREATE VIEW core.inventory_account_selector_view
AS
SELECT * FROM core.account_scrud_view
--Current Assets
WHERE account_master_id = 10100
ORDER BY account_id;