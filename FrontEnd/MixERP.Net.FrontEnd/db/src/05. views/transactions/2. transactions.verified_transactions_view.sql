DROP VIEW IF EXISTS transactions.verified_transactions_view CASCADE;

CREATE VIEW transactions.verified_transactions_view
AS
SELECT * FROM transactions.transaction_view
WHERE verification_status_id > 0;
