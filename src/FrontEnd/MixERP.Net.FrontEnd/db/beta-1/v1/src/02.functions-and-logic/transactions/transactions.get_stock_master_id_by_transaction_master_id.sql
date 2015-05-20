DROP FUNCTION IF EXISTS transactions.get_stock_master_id_by_transaction_master_id(_stock_master_id bigint);

CREATE FUNCTION transactions.get_stock_master_id_by_transaction_master_id(_stock_master_id bigint)
RETURNS bigint
AS
$$
BEGIN
        RETURN
        (
                SELECT transactions.stock_master.stock_master_id
                FROM transactions.stock_master
                WHERE transactions.stock_master.transaction_master_id=$1
        );
END
$$
LANGUAGE plpgsql;