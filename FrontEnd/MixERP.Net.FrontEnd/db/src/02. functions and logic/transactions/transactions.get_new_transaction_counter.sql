
/*******************************************************************
    THIS FUNCTION RETURNS A NEW INCREMENTAL COUNTER SUBJECT 
    TO BE USED TO GENERATE TRANSACTION CODES
*******************************************************************/

CREATE FUNCTION transactions.get_new_transaction_counter(date)
RETURNS integer
AS
$$
    DECLARE _ret_val integer;
BEGIN
    SELECT INTO _ret_val
        COALESCE(MAX(transaction_counter),0)
    FROM transactions.transaction_master
    WHERE value_date=$1;

    IF _ret_val IS NULL THEN
        RETURN 1::integer;
    ELSE
        RETURN (_ret_val + 1)::integer;
    END IF;
END;
$$
LANGUAGE plpgsql;
