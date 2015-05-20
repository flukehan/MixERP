DROP FUNCTION IF EXISTS transactions.restrict_delete_trigger() CASCADE;
CREATE FUNCTION transactions.restrict_delete_trigger()
RETURNS TRIGGER
AS
$$
BEGIN
    IF TG_OP='DELETE' THEN
        RAISE EXCEPTION 'Deleting a transaction is not allowed. Mark the transaction as rejected instead.'
        USING ERRCODE='P5800';
    END IF;
END
$$
LANGUAGE 'plpgsql';


CREATE TRIGGER restrict_delete_trigger
BEFORE DELETE
ON transactions.transaction_details
FOR EACH ROW 
EXECUTE PROCEDURE transactions.restrict_delete_trigger();


CREATE TRIGGER restrict_delete_trigger
BEFORE DELETE
ON transactions.stock_master
FOR EACH ROW 
EXECUTE PROCEDURE transactions.restrict_delete_trigger();


CREATE TRIGGER restrict_delete_trigger
BEFORE DELETE
ON transactions.stock_details
FOR EACH ROW 
EXECUTE PROCEDURE transactions.restrict_delete_trigger();

