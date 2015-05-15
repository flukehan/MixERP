DROP VIEW IF EXISTS core.bank_account_scrud_view CASCADE;

CREATE OR REPLACE VIEW core.bank_account_scrud_view
AS
SELECT
    account_id,
    office.users.user_name AS maintained_by,
    office.offices.office_code || ' (' || office.offices.office_name || ')' AS office,
    bank_name,
    bank_branch,
    bank_contact_number,
    bank_address,
    bank_account_number,
    bank_account_type,
    relationship_officer_name,
    is_merchant_account
FROM core.bank_accounts
INNER JOIN office.users
ON core.bank_accounts.maintained_by_user_id = office.users.user_id
INNER JOIN office.offices
ON core.bank_accounts.office_id = office.offices.office_id;
