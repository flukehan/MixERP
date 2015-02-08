DROP VIEW IF EXISTS core.account_master_selector_view;

CREATE VIEW core.account_master_selector_view
AS
SELECT * FROM core.account_masters;