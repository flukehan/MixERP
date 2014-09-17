DROP FUNCTION IF EXISTS transactions.is_purchase(_transaction_master_id bigint);

CREATE FUNCTION transactions.is_purchase(_transaction_master_id bigint)
RETURNS boolean
AS
$$
BEGIN
        IF EXISTS
        (
                SELECT * FROM transactions.transaction_master
                WHERE transactions.transaction_master.transaction_master_id = $1
                AND book IN ('Purchase.Direct', 'Purchase.Receipt')
        ) THEN
                RETURN true;
        END IF;

        RETURN false;
END
$$
LANGUAGE plpgsql;

