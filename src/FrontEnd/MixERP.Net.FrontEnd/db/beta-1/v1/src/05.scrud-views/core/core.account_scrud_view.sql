CREATE VIEW core.account_scrud_view
AS
SELECT
    core.accounts.account_id,
    core.account_masters.account_master_code || ' (' || core.account_masters.account_master_name || ')' AS account_master,
    core.accounts.account_number,
    core.accounts.external_code,
	core.currencies.currency_code || ' ('|| core.currencies.currency_name|| ')' currency,
    core.accounts.account_name,
    core.accounts.description,
	core.accounts.confidential,
	core.accounts.is_transaction_node,
    core.accounts.sys_type,
    parent_account.account_number || ' (' || parent_account.account_name || ')' AS parent
    
FROM core.accounts
INNER JOIN core.account_masters
ON core.account_masters.account_master_id=core.accounts.account_master_id
INNER JOIN core.currencies
ON core.accounts.currency_code = core.currencies.currency_code
LEFT JOIN core.accounts parent_account
ON parent_account.account_id=core.accounts.parent_account_id
WHERE NOT core.accounts.sys_type;