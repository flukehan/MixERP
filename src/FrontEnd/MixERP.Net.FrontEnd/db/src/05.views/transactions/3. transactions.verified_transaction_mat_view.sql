DROP MATERIALIZED VIEW IF EXISTS transactions.verified_transaction_mat_view CASCADE;

CREATE MATERIALIZED VIEW transactions.verified_transaction_mat_view
AS
SELECT * FROM transactions.verified_transaction_view;

ALTER MATERIALIZED VIEW transactions.verified_transaction_mat_view
OWNER TO mix_erp;