
CREATE FUNCTION transactions.get_invoice_amount(transaction_master_id_ bigint)
RETURNS money_strict2
AS
$$
DECLARE _shipping_charge money_strict2;
DECLARE _stock_total money_strict2;
BEGIN
    SELECT SUM((quantity * price) + tax - discount) INTO _stock_total
    FROM transactions.stock_details
    WHERE transactions.stock_details.stock_master_id =
    (
        SELECT transactions.stock_master.stock_master_id
        FROM transactions.stock_master WHERE transactions.stock_master.transaction_master_id= $1
    );

    SELECT shipping_charge INTO _shipping_charge
    FROM transactions.stock_master
    WHERE transactions.stock_master.transaction_master_id=$1;

    RETURN COALESCE(_stock_total + _shipping_charge, 0::money_strict2); 
END
$$
LANGUAGE plpgsql;


