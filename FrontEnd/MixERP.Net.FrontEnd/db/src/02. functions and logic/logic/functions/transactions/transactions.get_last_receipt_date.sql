DROP FUNCTION IF EXISTS transactions.get_last_receipt_date(office_id integer, party_id integer);
CREATE FUNCTION transactions.get_last_receipt_date(office_id integer, party_id integer)
RETURNS date
AS
$$
BEGIN
	RETURN
	(
		SELECT MAX(transactions.verified_transactions_view.value_date)
		FROM transactions.verified_transactions_view
		INNER JOIN transactions.customer_receipts
		ON transactions.verified_transactions_view.transaction_master_id = transactions.customer_receipts.transaction_master_id
		WHERE transactions.verified_transactions_view.office_id=$1
		AND transactions.customer_receipts.party_id = $2
	);
END
$$
LANGUAGE plpgsql;
