DROP VIEW IF EXISTS transactions.verified_stock_details_view;

CREATE VIEW transactions.verified_stock_details_view
AS
SELECT transactions.stock_details.* 
FROM transactions.stock_details
INNER JOIN transactions.stock_master
ON transactions.stock_master.stock_master_id = transactions.stock_details.stock_master_id
INNER JOIN transactions.transaction_master
ON transactions.transaction_master.transaction_master_id = transactions.stock_master.transaction_master_id
AND transactions.transaction_master.verification_status_id > 0;

