DROP FUNCTION IF EXISTS transactions.verify_stock_master_integrity_trigger() CASCADE;

CREATE FUNCTION transactions.verify_stock_master_integrity_trigger()
RETURNS TRIGGER
AS
$$
        DECLARE _office_id integer=0;
BEGIN
        SELECT office_id INTO _office_id
        FROM transactions.transaction_master
        WHERE transactions.transaction_master.transaction_master_id = NEW.transaction_master_id;
        
        IF(office.get_office_id_by_store_id(NEW.store_id) != _office_id) THEN
                RAISE EXCEPTION 'Invalid store.'
                USING ERRCODE='P3012';
        END IF;

        IF(office.get_office_id_by_cash_repository_id(NEW.cash_repository_id)  != _office_id) THEN
                RAISE EXCEPTION 'Invalid cash repository.'
                USING ERRCODE='P3013';
        END IF;
                
        RETURN NEW;
END
$$
LANGUAGE plpgsql;


CREATE TRIGGER verify_stock_master_integrity_trigger_after_insert
AFTER INSERT
ON transactions.stock_master
FOR EACH ROW 
EXECUTE PROCEDURE transactions.verify_stock_master_integrity_trigger();


CREATE TRIGGER verify_stock_master_integrity_trigger_after_update
AFTER UPDATE
ON transactions.stock_master
FOR EACH ROW 
EXECUTE PROCEDURE transactions.verify_stock_master_integrity_trigger();




