CREATE VIEW core.bank_account_view
AS
SELECT
    core.accounts.account_id,
    core.accounts.account_number,
    core.accounts.account_name,
    office.users.user_name AS maintained_by,
    core.bank_accounts.bank_name,
    core.bank_accounts.bank_branch,
    core.bank_accounts.bank_contact_number,
    core.bank_accounts.bank_address,
    core.bank_accounts.bank_account_number,
    core.bank_accounts.bank_account_type,
    core.bank_accounts.relationship_officer_name AS relation_officer
FROM
    core.bank_accounts
INNER JOIN core.accounts ON core.accounts.account_id = core.bank_accounts.account_id
INNER JOIN office.users ON core.bank_accounts.maintained_by_user_id = office.users.user_id;

