CREATE FUNCTION core.get_item_cost_price(item_id_ integer, unit_id_ integer, party_id_ bigint)
RETURNS money_strict2
AS
$$
    DECLARE _price money_strict2;
    DECLARE _unit_id integer;
    DECLARE _factor decimal;
    DECLARE _tax_rate decimal;
    DECLARE _includes_tax boolean;
    DECLARE _tax money_strict2;
BEGIN
    --Fist pick the catalog price which matches all these fields:
    --Item, Unit, and Supplier.
    --This is the most effective price.
    SELECT 
        item_cost_prices.price, 
        item_cost_prices.unit_id,
        item_cost_prices.includes_tax
    INTO 
        _price, 
        _unit_id,
        _includes_tax       
    FROM core.item_cost_prices
    WHERE item_cost_prices.item_id = $1
    AND item_cost_prices.unit_id = $2
    AND item_cost_prices.party_id =$3;

    IF(_unit_id IS NULL) THEN
        --We do not have a cost price of this item for the unit supplied.
        --Let's see if this item has a price for other units.
        SELECT 
            item_cost_prices.price, 
            item_cost_prices.unit_id,
            item_cost_prices.includes_tax
        INTO 
            _price, 
            _unit_id,
            _includes_tax
        FROM core.item_cost_prices
        WHERE item_cost_prices.item_id=$1
        AND item_cost_prices.party_id =$3;
    END IF;

    
    IF(_price IS NULL) THEN
        --This item does not have cost price defined in the catalog.
        --Therefore, getting the default cost price from the item definition.
        SELECT 
            cost_price, 
            unit_id,
            cost_price_includes_tax
        INTO 
            _price, 
            _unit_id,
            _includes_tax
        FROM core.items
        WHERE core.items.item_id = $1;
    END IF;

    IF(_includes_tax) THEN
        _tax_rate := core.get_item_tax_rate($1);
        _price := _price / ((100 + _tax_rate)/ 100);
    END IF;

    --Get the unitary conversion factor if the requested unit does not match with the price defition.
    _factor := core.convert_unit($2, _unit_id);

    RETURN _price * _factor;
END
$$
LANGUAGE plpgsql;
