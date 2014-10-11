CREATE FUNCTION office.count_item_in_stock(item_id_ integer, unit_id_ integer, office_id_ integer)
RETURNS decimal
AS
$$
    DECLARE _base_unit_id integer;
    DECLARE _debit decimal;
    DECLARE _credit decimal;
    DECLARE _balance decimal;
    DECLARE _factor decimal;
BEGIN

    --Get the base item unit
    SELECT 
        core.get_root_unit_id(core.items.unit_id) 
    INTO _base_unit_id
    FROM core.items
    WHERE core.items.item_id=$1;

    --Get the sum of debit stock quantity from approved transactions
    SELECT 
        COALESCE(SUM(base_quantity), 0)
    INTO _debit
    FROM transactions.stock_details
    INNER JOIN transactions.stock_master
    ON transactions.stock_master.stock_master_id = transactions.stock_details.stock_master_id
    INNER JOIN transactions.transaction_master
    ON transactions.stock_master.transaction_master_id = transactions.transaction_master.transaction_master_id
    WHERE transactions.transaction_master.verification_status_id > 0
    AND transactions.stock_details.item_id=$1
    AND transactions.stock_details.store_id IN (SELECT store_id FROM office.stores WHERE office.stores.office_id = $3)
    AND transactions.stock_details.tran_type='Dr';
    
    --Get the sum of credit stock quantity from approved transactions
    SELECT 
        COALESCE(SUM(base_quantity), 0)
    INTO _credit
    FROM transactions.stock_details
    INNER JOIN transactions.stock_master
    ON transactions.stock_master.stock_master_id = transactions.stock_details.stock_master_id
    INNER JOIN transactions.transaction_master
    ON transactions.stock_master.transaction_master_id = transactions.transaction_master.transaction_master_id
    WHERE transactions.transaction_master.verification_status_id > 0
    AND transactions.stock_details.item_id=$1
    AND transactions.stock_details.store_id IN (SELECT store_id FROM office.stores WHERE office.stores.office_id = $3)
    AND transactions.stock_details.tran_type='Cr';
    
    _balance:= _debit - _credit;

    
    _factor = core.convert_unit($2, _base_unit_id);

    return _balance / _factor;  
END
$$
LANGUAGE plpgsql;

