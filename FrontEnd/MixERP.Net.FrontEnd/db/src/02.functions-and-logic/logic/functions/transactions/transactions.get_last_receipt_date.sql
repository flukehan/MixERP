DROP FUNCTION IF EXISTS transactions.get_last_receipt_date(office_id integer, party_id bigint);
CREATE FUNCTION transactions.get_last_receipt_date(office_id integer, party_id bigint)
RETURNS date
AS
$$
BEGIN
    RETURN
    (
        SELECT MAX(transactions.verified_transaction_view.value_date)
        FROM transactions.verified_transaction_view
        INNER JOIN transactions.customer_receipts
        ON transactions.verified_transaction_view.transaction_master_id = transactions.customer_receipts.transaction_master_id
        WHERE transactions.verified_transaction_view.office_id=$1
        AND transactions.customer_receipts.party_id = $2
    );
END
$$
LANGUAGE plpgsql;


