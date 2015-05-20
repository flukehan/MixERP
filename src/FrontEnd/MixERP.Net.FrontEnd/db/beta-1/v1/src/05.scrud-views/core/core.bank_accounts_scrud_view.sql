DROP VIEW IF EXISTS core.bank_accounts_scrud_view;
CREATE VIEW core.bank_accounts_scrud_view
AS
SELECT 
    core.bank_accounts.account_id,
    office.users.user_name,
    office.offices.office_code || '(' || office.offices.office_name||')' AS office_name,
	core.bank_accounts.bank_name,
	core.bank_accounts.bank_branch,
	core.bank_accounts.bank_contact_number,
	core.bank_accounts.bank_address,
	core.bank_accounts.bank_account_number,
	core.bank_accounts.bank_account_type,
	core.bank_accounts.relationship_officer_name
FROM
    core.bank_accounts
INNER JOIN office.users
ON core.bank_accounts.maintained_by_user_id = office.users.user_id
INNER JOIN office.offices
ON core.bank_accounts.office_id = office.offices.office_id; 