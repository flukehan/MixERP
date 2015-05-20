DROP FUNCTION IF EXISTS transactions.validate_item_for_return
(
    _transaction_master_id                  bigint, 
    _store_id                               integer, 
    _item_code                              national character varying(12), 
    _unit_name                              national character varying(50), 
    _quantity                               integer, 
    _price                                  public.money_strict
);

DROP FUNCTION IF EXISTS transactions.validate_items_for_return
(
    _transaction_master_id                  bigint, 
    _details                                transactions.stock_detail_type[]
);

CREATE FUNCTION transactions.validate_items_for_return
(
    _transaction_master_id                  bigint, 
    _details                                transactions.stock_detail_type[]
)
RETURNS boolean
AS
$$
    DECLARE _stock_master_id                bigint = 0;
    DECLARE _is_purchase                    boolean = false;
    DECLARE _item_id                        integer = 0;
    DECLARE _item_code                      text;
    DECLARE _unit_name                      text;
    DECLARE _unit_id                        integer = 0;
    DECLARE _factor_to_base_unit            numeric(24, 4);
    DECLARE _returned_in_previous_batch     public.decimal_strict2 = 0;
    DECLARE _in_verification_queue          public.decimal_strict2 = 0;
    DECLARE _actual_price_in_root_unit      public.money_strict2 = 0;
    DECLARE _price_in_root_unit             public.money_strict2 = 0;
    DECLARE _item_in_stock                  public.decimal_strict2 = 0;
    DECLARE this                            RECORD; 
BEGIN        
    _stock_master_id                := transactions.get_stock_master_id_by_transaction_master_id(_transaction_master_id);
    _is_purchase                    := transactions.is_purchase(_transaction_master_id);

    DROP TABLE IF EXISTS details_temp;
    CREATE TEMPORARY TABLE details_temp
    (
        store_id            integer,
        item_id             integer,
        item_code           national character varying(12),
        item_in_stock       numeric(24, 4),
        quantity            integer_strict,        
        unit_id             integer,
        unit_name           national character varying(50),
        price               money_strict,
        discount            money_strict2,
        shipping_charge     money_strict2,
        tax_form            national character varying(24),
        tax                 money_strict2,
        root_unit_id        integer,
        base_quantity       numeric(24, 4)
    ) ON COMMIT DROP;

    INSERT INTO details_temp(store_id, item_code, quantity, unit_name, price, discount, shipping_charge, tax_form, tax)
    SELECT store_id, item_code, quantity, unit_name, price, discount, shipping_charge, tax_form, tax
    FROM explode_array(_details);

    UPDATE details_temp
    SET 
        unit_id = core.get_unit_id_by_unit_name(unit_name),
        item_id = core.get_item_id_by_item_code(item_code),
        item_in_stock = core.count_item_in_stock(item_id, unit_id, store_id);
       
    UPDATE details_temp
    SET root_unit_id = core.get_root_unit_id(unit_id);

    UPDATE details_temp
    SET base_quantity = core.convert_unit(unit_id, root_unit_id) * quantity;


    --Determine whether the quantity of the returned item(s) is less than or equal to the same on the actual transaction
    DROP TABLE IF EXISTS item_summary_temp;
    CREATE TEMPORARY TABLE item_summary_temp
    (
        store_id                    integer,
        item_id                     integer,
        root_unit_id                integer,
        returned_quantity           numeric(24, 4),
        actual_quantity             numeric(24, 4),
        returned_in_previous_batch  numeric(24, 4),
        in_verification_queue       numeric(24, 4)
    ) ON COMMIT DROP;
    
    INSERT INTO item_summary_temp(store_id, item_id, root_unit_id, returned_quantity)
    SELECT
        store_id,
        item_id,
        root_unit_id, 
        SUM(base_quantity)
    FROM details_temp
    GROUP BY 
        store_id, 
        item_id,
        root_unit_id;

    UPDATE item_summary_temp
    SET actual_quantity = 
    (
        SELECT SUM(base_quantity)
        FROM transactions.stock_details
        WHERE transactions.stock_details.stock_master_id = _stock_master_id
        AND transactions.stock_details.item_id = item_summary_temp.item_id
    );

    UPDATE item_summary_temp
    SET returned_in_previous_batch = 
    (
        SELECT 
            COALESCE(SUM(base_quantity), 0)
        FROM transactions.stock_details
        WHERE stock_master_id IN
        (
            SELECT stock_master_id
            FROM transactions.stock_master
            INNER JOIN transactions.transaction_master
            ON transactions.transaction_master.transaction_master_id = transactions.stock_master.transaction_master_id
            WHERE transactions.transaction_master.verification_status_id > 0
            AND transactions.stock_master.transaction_master_id IN 
            (
                SELECT 
                return_transaction_master_id 
                FROM transactions.stock_return
                WHERE transaction_master_id = _transaction_master_id
            )
        )
        AND item_id = item_summary_temp.item_id
    );

    UPDATE item_summary_temp
    SET in_verification_queue =
    (
        SELECT 
            COALESCE(SUM(base_quantity), 0)
        FROM transactions.stock_details
        WHERE stock_master_id IN
        (
            SELECT stock_master_id
            FROM transactions.stock_master
            INNER JOIN transactions.transaction_master
            ON transactions.transaction_master.transaction_master_id = transactions.stock_master.transaction_master_id
            WHERE transactions.transaction_master.verification_status_id = 0
            AND transactions.stock_master.transaction_master_id IN 
            (
                SELECT 
                return_transaction_master_id 
                FROM transactions.stock_return
                WHERE transaction_master_id = _transaction_master_id
            )
        )
        AND item_id = item_summary_temp.item_id
    );
    
    --Determine whether the price of the returned item(s) is less than or equal to the same on the actual transaction
    DROP TABLE IF EXISTS cumulative_pricing_temp;
    CREATE TEMPORARY TABLE cumulative_pricing_temp
    (
        item_id                     integer,
        base_price                  numeric(24, 4),
        allowed_returns             numeric(24, 4)
    ) ON COMMIT DROP;

    INSERT INTO cumulative_pricing_temp
    SELECT 
        item_id,
        MIN(price  / base_quantity * quantity) as base_price,
        SUM(base_quantity) OVER(ORDER BY item_id, base_quantity) as allowed_returns
    FROM transactions.stock_details 
    WHERE stock_master_id = _stock_master_id
    GROUP BY item_id, base_quantity;

    IF EXISTS(SELECT 0 FROM details_temp WHERE store_id IS NULL OR store_id <= 0) THEN
        RAISE EXCEPTION 'Invalid store.'
        USING ERRCODE='P3012';
    END IF;    

    IF EXISTS(SELECT 0 FROM details_temp WHERE item_code IS NULL OR trim(item_code) = '' OR item_id IS NULL OR item_id <= 0) THEN
        RAISE EXCEPTION 'Invalid item.'
        USING ERRCODE='P3051';
    END IF;

    IF EXISTS(SELECT 0 FROM details_temp WHERE unit_name IS NULL OR trim(unit_name) = '' OR unit_id IS NULL OR unit_id <= 0) THEN
        RAISE EXCEPTION 'Invalid unit.'
        USING ERRCODE='P3052';
    END IF;

    IF EXISTS(SELECT 0 FROM details_temp WHERE quantity IS NULL OR quantity <= 0) THEN
        RAISE EXCEPTION 'Invalid quantity.'
        USING ERRCODE='P3301';
    END IF;

    IF(_stock_master_id  IS NULL OR _stock_master_id  <= 0) THEN
        RAISE EXCEPTION 'Invalid transaction id.'
        USING ERRCODE='P3302';
    END IF;

    IF NOT EXISTS
    (
        SELECT * FROM transactions.transaction_master
        WHERE transaction_master_id = _transaction_master_id
        AND verification_status_id > 0
    ) THEN
        RAISE EXCEPTION 'Invalid or rejected transaction.'
        USING ERRCODE='P5301';
    END IF;
        
    SELECT item_code INTO _item_code
    FROM details_temp
    WHERE item_id NOT IN
    (
        SELECT item_id FROM transactions.stock_details
        WHERE stock_master_id = _stock_master_id
    )
    LIMIT 1;

    IF(COALESCE(_item_code, '') != '') THEN
        RAISE EXCEPTION '%', format('The item %1$s is not associated with this transaction.', _item_code)
        USING ERRCODE='P4020';
    END IF;


    IF NOT EXISTS
    (
        SELECT * FROM transactions.stock_details
        INNER JOIN details_temp
        ON transactions.stock_details.item_id = details_temp.item_id
        WHERE stock_master_id = _stock_master_id
        AND core.get_root_unit_id(details_temp.unit_id) = core.get_root_unit_id(transactions.stock_details.unit_id)
        LIMIT 1
    ) THEN
        RAISE EXCEPTION 'Invalid or incompatible unit specified'
        USING ERRCODE='P3053';
    END IF;

    IF(_is_purchase = true) THEN    
        SELECT
            item_in_stock,
            unit_name,
            item_code
        INTO
            _item_in_stock,
            _unit_name,
            _item_code        
        FROM details_temp 
        WHERE item_in_stock < quantity 
        LIMIT 1;

        IF(COALESCE(_item_in_stock, 0) > 0) THEN
            RAISE EXCEPTION '%', format('Only %1$s %2$s of %3$s left in stock.',_item_in_stock, _unit_name, _item_code)
            USING ERRCODE='P5500';
        END IF;
    END IF;

    IF EXISTS
    (
        SELECT 0
        FROM item_summary_temp
        WHERE returned_quantity + returned_in_previous_batch + in_verification_queue > actual_quantity
        LIMIT 1
    ) THEN    
        RAISE EXCEPTION 'The returned quantity cannot be greater than actual quantity.'
        USING ERRCODE='P5203';
    END IF;

    FOR this IN
    SELECT item_id, base_quantity, (price / base_quantity * quantity)::numeric(24, 4) as price
    FROM details_temp
    LOOP
        IF NOT EXISTS
        (
            SELECT 0
            FROM cumulative_pricing_temp
            WHERE item_id = this.item_id
            AND base_price >=  this.price
            AND allowed_returns >= this.base_quantity
        ) THEN
            RAISE EXCEPTION 'The returned amount cannot be greater than actual amount.'
            USING ERRCODE='P5204';

            RETURN FALSE;
        END IF;
    END LOOP;
    
    RETURN TRUE;
END
$$
LANGUAGE plpgsql;

-- SELECT * FROM transactions.validate_items_for_return
-- (
--     127,
--     ARRAY[
--         ROW(1, 'RMBP', 2, 'Dozen', 1000, 0, 200, 'MoF-NY-BK-STX', 0)::transactions.stock_detail_type
--        
--     ]
-- );
