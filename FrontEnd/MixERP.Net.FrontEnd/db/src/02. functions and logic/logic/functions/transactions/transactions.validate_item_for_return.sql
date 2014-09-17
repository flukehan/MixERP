DROP FUNCTION IF EXISTS transactions.validate_item_for_return(_transaction_master_id bigint, _store_id integer, _item_code national character varying(12), _unit_name national character varying(50), _quantity integer, _price money_strict);

CREATE FUNCTION transactions.validate_item_for_return(_transaction_master_id bigint, _store_id integer, _item_code national character varying(12), _unit_name national character varying(50), _quantity integer, _price money_strict)
RETURNS boolean
AS
$$
        DECLARE _stock_master_id bigint = 0;
        DECLARE _is_purchase boolean = false;
        DECLARE _item_id integer = 0;
        DECLARE _unit_id integer = 0;
        DECLARE _actual_quantity decimal_strict2 = 0;
        DECLARE _actual_price_in_root_unit money_strict2 = 0;
        DECLARE _price_in_root_unit money_strict2 = 0;
        DECLARE _item_in_stock decimal_strict2 = 0;
        
BEGIN        
        IF(_store_id IS NULL OR _store_id <= 0) THEN
                RAISE EXCEPTION 'Invalid store.';
        END IF;


        IF(_item_code IS NULL OR trim(_item_code) = '') THEN
                RAISE EXCEPTION 'Invalid item.';
        END IF;

        IF(_unit_name IS NULL OR trim(_unit_name) = '') THEN
                RAISE EXCEPTION 'Invalid unit.';
        END IF;

        IF(_quantity IS NULL OR _quantity <= 0) THEN
                RAISE EXCEPTION 'Invalid quantity.';
        END IF;
        
        _stock_master_id                := transactions.get_stock_master_id_by_transaction_master_id(_transaction_master_id);
        IF(_stock_master_id  IS NULL OR _stock_master_id  <= 0) THEN
                RAISE EXCEPTION 'Invalid transaction id.';
        END IF;

        _item_id                        := core.get_item_id_by_item_code(_item_code);
        IF(_item_id IS NULL OR _item_id <= 0) THEN
                RAISE EXCEPTION 'Invalid item.';
        END IF;

        IF NOT EXISTS
        (
                SELECT * FROM transactions.stock_details
                WHERE stock_master_id = _stock_master_id
                AND item_id = _item_id
                LIMIT 1
        ) THEN
                RAISE EXCEPTION '%', format('The item %1$s is not associated with this transaction.', _item_code);
        END IF;

        _unit_id                        := core.get_unit_id_by_unit_name(_unit_name);
        IF(_unit_id IS NULL OR _unit_id <= 0) THEN
                RAISE EXCEPTION 'Invalid unit.';
        END IF;


        _is_purchase                    := transactions.is_purchase(_transaction_master_id);

        IF NOT EXISTS
        (
                SELECT * FROM transactions.stock_details
                WHERE stock_master_id = _stock_master_id
                AND item_id = _item_id
                AND core.get_root_unit_id(_unit_id) = core.get_root_unit_id(unit_id)
                LIMIT 1
        ) THEN
                RAISE EXCEPTION 'Invalid or incompatible unit specified';
        END IF;

        IF(_is_purchase = true) THEN
                _item_in_stock = core.count_item_in_stock(_item_id, _unit_id, _store_id);

                IF(_item_in_stock < _quantity) THEN
                        RAISE EXCEPTION '%', format('Only %1$s %2$s of %3$s left in stock.',_item_in_stock, _unit_name, _item_code);
                END IF;
        END IF;

        SELECT 
                core.convert_unit(base_unit_id, _unit_id) * base_quantity
                INTO _actual_quantity
        FROM transactions.stock_details
        WHERE stock_master_id = _stock_master_id
        AND item_id = _item_id;

        IF(_quantity > _actual_quantity) THEN
                RAISE EXCEPTION 'The returned quantity cannot be greater than actual quantity.';
        END IF;

        _price_in_root_unit := core.convert_unit(core.get_root_unit_id(_unit_id), _unit_id) * _price;



        SELECT 
                (core.convert_unit(core.get_root_unit_id(transactions.stock_details.unit_id), transactions.stock_details.base_unit_id) * price) / (base_quantity/quantity)
                INTO _actual_price_in_root_unit
        FROM transactions.stock_details
        WHERE stock_master_id = _stock_master_id
        AND item_id = _item_id;


        IF(_price_in_root_unit > _actual_price_in_root_unit) THEN
                RAISE EXCEPTION 'The returned amount cannot be greater than actual amount.';
                RETURN FALSE;
        END IF;

        RETURN TRUE;
END
$$
LANGUAGE plpgsql;

--SELECT * FROM transactions.validate_item_for_return(25, 1, 'CAS', 'Piece', 1, 40000);
