DROP FUNCTION IF EXISTS core.count_item_in_stock(_item_id integer, _unit_id integer, _store_id integer);

CREATE FUNCTION core.count_item_in_stock(_item_id integer, _unit_id integer, _store_id integer)
RETURNS decimal
STABLE
AS
$$
    DECLARE _debit decimal;
    DECLARE _credit decimal;
    DECLARE _balance decimal;
BEGIN

    _debit := core.count_purchases($1, $2, $3);
    _credit := core.count_sales($1, $2, $3);

    _balance:= _debit - _credit;    
    return _balance;  
END
$$
LANGUAGE plpgsql;

