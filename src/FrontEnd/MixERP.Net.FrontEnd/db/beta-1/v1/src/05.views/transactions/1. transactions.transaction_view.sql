DROP VIEW IF EXISTS transactions.transaction_view;
CREATE VIEW transactions.transaction_view
AS
SELECT
    transactions.transaction_master.transaction_master_id,
    transactions.transaction_master.transaction_counter,
    transactions.transaction_master.transaction_code,
    transactions.transaction_master.book,
    transactions.transaction_master.value_date,
    transactions.transaction_master.transaction_ts,
    transactions.transaction_master.login_id,
    transactions.transaction_master.user_id,
    transactions.transaction_master.sys_user_id,
    transactions.transaction_master.office_id,
    transactions.transaction_master.cost_center_id,
    transactions.transaction_master.reference_number,
    transactions.transaction_master.statement_reference AS master_statement_reference,
    transactions.transaction_master.last_verified_on,
    transactions.transaction_master.verified_by_user_id,
    transactions.transaction_master.verification_status_id,
    transactions.transaction_master.verification_reason,
    transactions.transaction_details.transaction_detail_id,
    transactions.transaction_details.tran_type,
    transactions.transaction_details.account_id,
    core.accounts.account_number,
    core.accounts.account_name,
    core.account_masters.normally_debit,
    core.account_masters.account_master_code,
    core.account_masters.account_master_name,
    core.accounts.account_master_id,
    core.accounts.confidential,
    transactions.transaction_details.statement_reference,
    transactions.transaction_details.cash_repository_id,
    transactions.transaction_details.currency_code,
    transactions.transaction_details.amount_in_currency,
    transactions.transaction_details.local_currency_code,
    transactions.transaction_details.amount_in_local_currency
FROM
transactions.transaction_master
INNER JOIN transactions.transaction_details
ON transactions.transaction_master.transaction_master_id = transactions.transaction_details.transaction_master_id
INNER JOIN core.accounts
ON transactions.transaction_details.account_id = core.accounts.account_id
INNER JOIN core.account_masters
ON core.accounts.account_master_id = core.account_masters.account_master_id;