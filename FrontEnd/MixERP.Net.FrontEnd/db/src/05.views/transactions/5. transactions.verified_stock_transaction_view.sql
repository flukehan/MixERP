DROP MATERIALIZED VIEW IF EXISTS transactions.verified_stock_transaction_view;

CREATE MATERIALIZED VIEW transactions.verified_stock_transaction_view
AS
SELECT * FROM transactions.stock_transaction_view
WHERE verification_status_id > 0;
