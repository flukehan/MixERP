CREATE MATERIALIZED VIEW transactions.verified_cash_transaction_mat_view
AS
SELECT * FROM transactions.verified_transaction_mat_view
WHERE transactions.verified_transaction_mat_view.transaction_master_id
IN
(
    SELECT transactions.verified_transaction_mat_view.transaction_master_id 
    FROM transactions.verified_transaction_mat_view
    WHERE account_master_id IN(10101, 10102) --Cash and Bank A/C
);

ALTER MATERIALIZED VIEW transactions.verified_cash_transaction_mat_view
OWNER TO mix_erp;