DROP VIEW IF EXISTS transactions.verified_transaction_view CASCADE;

CREATE VIEW transactions.verified_transaction_view
AS
SELECT * FROM transactions.transaction_view
WHERE verification_status_id > 0;
