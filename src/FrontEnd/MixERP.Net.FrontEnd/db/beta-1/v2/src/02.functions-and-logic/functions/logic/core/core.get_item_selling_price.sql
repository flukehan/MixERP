DROP FUNCTION IF EXISTS core.get_item_selling_price(item_id_ integer, party_type_id_ integer, price_type_id_ integer, unit_id_ integer);
CREATE FUNCTION core.get_item_selling_price(item_id_ integer, party_type_id_ integer, price_type_id_ integer, unit_id_ integer)
RETURNS public.money_strict2
AS
$$
    DECLARE _price          public.money_strict2;
    DECLARE _unit_id        integer;
    DECLARE _factor         decimal;
    DECLARE _tax_rate       decimal;
    DECLARE _includes_tax   boolean;
    DECLARE _tax            public.money_strict2;
BEGIN

    --Fist pick the catalog price which matches all these fields:
    --Item, Customer Type, Price Type, and Unit.
    --This is the most effective price.
    SELECT 
        item_selling_prices.price, 
        item_selling_prices.unit_id,
        item_selling_prices.includes_tax
    INTO 
        _price, 
        _unit_id,
        _includes_tax       
    FROM core.item_selling_prices
    WHERE item_selling_prices.item_id=$1
    AND item_selling_prices.party_type_id=$2
    AND item_selling_prices.price_type_id =$3
    AND item_selling_prices.unit_id = $4;

    IF(_unit_id IS NULL) THEN
        --We do not have a selling price of this item for the unit supplied.
        --Let's see if this item has a price for other units.
        SELECT 
            item_selling_prices.price, 
            item_selling_prices.unit_id,
            item_selling_prices.includes_tax
        INTO 
            _price, 
            _unit_id,
            _includes_tax
        FROM core.item_selling_prices
        WHERE item_selling_prices.item_id=$1
        AND item_selling_prices.party_type_id=$2
        AND item_selling_prices.price_type_id =$3;
    END IF;

    IF(_price IS NULL) THEN
        SELECT 
            item_selling_prices.price, 
            item_selling_prices.unit_id,
            item_selling_prices.includes_tax
        INTO 
            _price, 
            _unit_id,
            _includes_tax
        FROM core.item_selling_prices
        WHERE item_selling_prices.item_id=$1
        AND item_selling_prices.price_type_id =$3;
    END IF;

    
    IF(_price IS NULL) THEN
        --This item does not have selling price defined in the catalog.
        --Therefore, getting the default selling price from the item definition.
        SELECT 
            selling_price, 
            unit_id,
            selling_price_includes_tax
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
    _factor := core.convert_unit($4, _unit_id);

    RETURN _price * _factor;
END
$$
LANGUAGE plpgsql;



/**************************************************************************************************************************
--------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------
'########::'##:::::::'########:::'######:::'##::::'##:'##::: ##:'####:'########::::'########:'########::'######::'########:
 ##.... ##: ##::::::: ##.... ##:'##... ##:: ##:::: ##: ###:: ##:. ##::... ##..:::::... ##..:: ##.....::'##... ##:... ##..::
 ##:::: ##: ##::::::: ##:::: ##: ##:::..::: ##:::: ##: ####: ##:: ##::::: ##:::::::::: ##:::: ##::::::: ##:::..::::: ##::::
 ########:: ##::::::: ########:: ##::'####: ##:::: ##: ## ## ##:: ##::::: ##:::::::::: ##:::: ######:::. ######::::: ##::::
 ##.....::: ##::::::: ##.....::: ##::: ##:: ##:::: ##: ##. ####:: ##::::: ##:::::::::: ##:::: ##...:::::..... ##:::: ##::::
 ##:::::::: ##::::::: ##:::::::: ##::: ##:: ##:::: ##: ##:. ###:: ##::::: ##:::::::::: ##:::: ##:::::::'##::: ##:::: ##::::
 ##:::::::: ########: ##::::::::. ######:::. #######:: ##::. ##:'####:::: ##:::::::::: ##:::: ########:. ######::::: ##::::
..:::::::::........::..::::::::::......:::::.......:::..::::..::....:::::..:::::::::::..:::::........:::......::::::..:::::
--------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------
**************************************************************************************************************************/


--SELECT * FROM core.get_item_selling_price(1, 1, 2, 1);