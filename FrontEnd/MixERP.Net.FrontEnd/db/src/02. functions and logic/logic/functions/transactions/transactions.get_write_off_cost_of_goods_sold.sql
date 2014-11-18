DROP FUNCTION IF EXISTS transactions.get_write_off_cost_of_goods_sold(_stock_master_id bigint, _item_id integer, _unit_id integer, _quantity integer);

CREATE FUNCTION transactions.get_write_off_cost_of_goods_sold(_stock_master_id bigint, _item_id integer, _unit_id integer, _quantity integer)
RETURNS money_strict2
AS
$$
    DECLARE _base_unit_id integer;
    DECLARE _factor decimal;
BEGIN
    _base_unit_id    = core.get_root_unit_id(_unit_id);
    _factor          = core.convert_unit(_unit_id, _base_unit_id);

    RAISE NOTICE 'Item Id ->%, Unit ID->%, Quantity->%', _item_id, _unit_id, _quantity;

    RETURN
        SUM((cost_of_goods_sold / base_quantity) * _factor * _quantity)     
         FROM transactions.stock_details        
    WHERE stock_master_id = _stock_master_id
    AND item_id = _item_id;    
END
$$
LANGUAGE plpgsql;


--SELECT * FROM transactions.get_write_off_cost_of_goods_sold(7, 3, 1, 1);

