DROP VIEW IF EXISTS core.account_selector_view;

CREATE VIEW core.account_selector_view
AS
SELECT
    core.accounts.account_id,
    core.accounts.account_number,
    core.accounts.account_name,
    core.accounts.description,
    core.accounts.sys_type,
    core.accounts.parent_account_id,
    parent_accounts.account_number AS parent_account_number,
    parent_accounts.account_name AS parent_account_name,
    core.account_masters.account_master_id,
    core.account_masters.account_master_code,
    core.account_masters.account_master_name
FROM
    core.account_masters
    INNER JOIN core.accounts 
    ON core.account_masters.account_master_id = core.accounts.account_master_id
    LEFT OUTER JOIN core.accounts AS parent_accounts 
    ON core.accounts.parent_account_id = parent_accounts.account_id;
