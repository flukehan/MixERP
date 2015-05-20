DROP FUNCTION IF EXISTS core.count_purchases(_item_id integer, _unit_id integer, _store_id integer);

CREATE FUNCTION core.count_purchases(_item_id integer, _unit_id integer, _store_id integer)
RETURNS decimal
STABLE
AS
$$
        DECLARE _base_unit_id integer;
        DECLARE _debit decimal;
        DECLARE _factor decimal;
BEGIN
    --Get the base item unit
    SELECT 
        core.get_root_unit_id(core.items.unit_id) 
    INTO _base_unit_id
    FROM core.items
    WHERE core.items.item_id=$1;

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
    AND transactions.stock_details.store_id=$3
    AND transactions.stock_details.tran_type='Dr';

    _factor = core.convert_unit(_base_unit_id, $2);    
    RETURN _debit * _factor;
END
$$
LANGUAGE plpgsql;
