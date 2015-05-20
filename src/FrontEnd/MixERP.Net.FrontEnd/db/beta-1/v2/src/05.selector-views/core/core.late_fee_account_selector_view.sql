DROP VIEW IF EXISTS core.late_fee_account_selector_view;

CREATE VIEW core.late_fee_account_selector_view
AS
SELECT * FROM core.account_scrud_view
--All income headings
WHERE account_master_id >= 20100
AND account_master_id < 20400
ORDER BY account_id;