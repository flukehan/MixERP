DROP FUNCTION IF EXISTS transactions.is_valid_stock_transaction_by_transaction_master_id(_transaction_master_id bigint);

CREATE FUNCTION transactions.is_valid_stock_transaction_by_transaction_master_id(_transaction_master_id bigint)
RETURNS boolean
AS
$$
BEGIN
        IF EXISTS(SELECT * FROM transactions.stock_master WHERE transaction_master_id=$1) THEN
                RETURN true;
        END IF;

        RETURN false;
END
$$
LANGUAGE plpgsql;
