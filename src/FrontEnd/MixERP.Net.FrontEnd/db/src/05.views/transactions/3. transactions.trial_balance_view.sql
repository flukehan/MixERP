DROP MATERIALIZED VIEW IF EXISTS transactions.trial_balance_view;
CREATE MATERIALIZED VIEW transactions.trial_balance_view
AS
SELECT core.get_account_name(account_id), 
    SUM(CASE transactions.verified_transaction_view.tran_type WHEN 'Dr' THEN amount_in_local_currency ELSE NULL END) AS debit,
    SUM(CASE transactions.verified_transaction_view.tran_type WHEN 'Cr' THEN amount_in_local_currency ELSE NULL END) AS Credit
FROM transactions.verified_transaction_view
GROUP BY account_id;

ALTER MATERIALIZED VIEW transactions.trial_balance_view
OWNER TO mix_erp;