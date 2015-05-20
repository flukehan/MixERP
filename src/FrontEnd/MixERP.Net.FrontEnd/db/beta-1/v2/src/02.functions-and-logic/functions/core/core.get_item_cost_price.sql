DROP FUNCTION IF EXISTS core.get_item_cost_price(integer, integer, bigint);
CREATE FUNCTION core.get_item_cost_price(item_id_ integer, unit_id_ integer, party_id_ bigint)
RETURNS public.money_strict2
AS
$$
    DECLARE _price public.money_strict2;
    DECLARE _unit_id integer;
    DECLARE _factor decimal;
   
BEGIN
    --Fist pick the catalog price which matches all these fields:
    --Item, Unit, and Supplier.
    --This is the most effective price.
    SELECT 
        item_cost_prices.price, 
        item_cost_prices.unit_id
       INTO 
        _price, 
        _unit_id
           FROM core.item_cost_prices
    WHERE item_cost_prices.item_id = $1
    AND item_cost_prices.unit_id = $2
    AND item_cost_prices.party_id =$3;

    IF(_unit_id IS NULL) THEN
        --We do not have a cost price of this item for the unit supplied.
        --Let's see if this item has a price for other units.
        SELECT 
            item_cost_prices.price, 
            item_cost_prices.unit_id
        INTO 
            _price, 
            _unit_id
                   FROM core.item_cost_prices
        WHERE item_cost_prices.item_id=$1
        AND item_cost_prices.party_id =$3;
    END IF;

    
    IF(_price IS NULL) THEN
        --This item does not have cost price defined in the catalog.
        --Therefore, getting the default cost price from the item definition.
        SELECT 
            cost_price, 
            unit_id
        INTO 
            _price, 
            _unit_id
        FROM core.items
        WHERE core.items.item_id = $1;
    END IF;

       --Get the unitary conversion factor if the requested unit does not match with the price defition.
    _factor := core.convert_unit($2, _unit_id);

    RETURN _price * _factor;
END
$$
LANGUAGE plpgsql;
