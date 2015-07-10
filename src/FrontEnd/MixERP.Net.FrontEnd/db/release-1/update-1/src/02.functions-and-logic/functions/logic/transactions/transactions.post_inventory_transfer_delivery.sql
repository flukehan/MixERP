DROP FUNCTION IF EXISTS transactions.post_inventory_transfer_delivery
(
    _office_id                              integer,
    _user_id                                integer,
    _login_id                               bigint,
    _inventory_transfer_request_id          bigint,
    _value_date                             date,
    _reference_number                       national character varying(24),
    _statement_reference                    text,
    _shipper_id                             integer,
    _source_store_id                        integer,
    _details                                transactions.stock_adjustment_type[]
);


CREATE FUNCTION transactions.post_inventory_transfer_delivery
(
    _office_id                              integer,
    _user_id                                integer,
    _login_id                               bigint,
    _inventory_transfer_request_id          bigint,
    _value_date                             date,
    _reference_number                       national character varying(24),
    _statement_reference                    text,
    _shipper_id                             integer,
    _source_store_id                        integer,
    _details                                transactions.stock_adjustment_type[]
)
RETURNS bigint
AS
$$
    DECLARE _inventory_transfer_delivery_id     bigint;
    DECLARE _stock_master_id                    bigint;
    DECLARE _destination_store_id               integer;
BEGIN
    IF(policy.can_post_transaction(_login_id, _user_id, _office_id, 'Inventory.Transfer.Delivery', _value_date) = false) THEN
        RETURN 0;
    END IF;


    CREATE TEMPORARY TABLE IF NOT EXISTS temp_stock_details
    (
        tran_type       transaction_type,
        store_id        integer,
        store_name      national character varying(50),
        item_id         integer,
        item_code       national character varying(12),
        unit_id         integer,
        base_unit_id    integer,
        unit_name       national character varying(50),
        quantity        integer_strict,
        base_quantity   integer,                
        price           money_strict                             
    ) 
    ON COMMIT DROP; 

    INSERT INTO temp_stock_details(tran_type, store_name, item_code, unit_name, quantity)
    SELECT tran_type, store_name, item_code, unit_name, quantity FROM explode_array(_details);

    IF EXISTS
    (
        SELECT 1 FROM temp_stock_details
        WHERE tran_type = 'Cr'
    ) THEN
        RAISE EXCEPTION 'Stock transfer delivery can only contain credit entries.'
        USING ERRCODE='P5004';
    END IF;

    IF EXISTS
    (
        SELECT 1 FROM temp_stock_details
        GROUP BY item_code
        HAVING COUNT(item_code) <> 1
    ) THEN
        RAISE EXCEPTION 'An item can appear only once in a store.'
        USING ERRCODE='P5202';
    END IF;

    IF EXISTS
    (
        SELECT 1 FROM temp_stock_details
        HAVING COUNT(DISTINCT store_name) <> 1
    ) THEN
        RAISE EXCEPTION 'You cannot provide more than one delivery destination store for this transaction.'
        USING ERRCODE='P5206';
    END IF;


    UPDATE temp_stock_details SET 
    item_id         = core.get_item_id_by_item_code(item_code),
    unit_id         = core.get_unit_id_by_unit_name(unit_name),
    store_id        = office.get_store_id_by_store_name(store_name);

    SELECT store_id INTO _destination_store_id
    FROM temp_stock_details
    LIMIT 1;

    IF(_destination_store_id = _source_store_id) THEN
        RAISE EXCEPTION 'The source and the destination stores can not be the same.'
        USING ERRCODE='P5207';
    END IF;
    
    IF EXISTS
    (
        SELECT * FROM temp_stock_details
        WHERE item_id IS NULL OR unit_id IS NULL OR store_id IS NULL
    ) THEN
        RAISE EXCEPTION 'Invalid data supplied.'
        USING ERRCODE='P3000';
    END IF;


    IF NOT EXISTS
    (
        SELECT * FROM transactions.inventory_transfer_requests
        WHERE inventory_transfer_request_id = _inventory_transfer_request_id
        AND store_id = _destination_store_id
    ) THEN
        RAISE EXCEPTION 'Invalid store.'
        USING ERRCODE='P3012';
    END IF;
    
    UPDATE temp_stock_details SET
    base_unit_id    = core.get_root_unit_id(unit_id),
    base_quantity   = core.get_base_quantity_by_unit_id(unit_id, quantity),
    price           = core.get_item_cost_price(item_id, unit_id, NULL);

    INSERT INTO transactions.inventory_transfer_deliveries
    (
            inventory_transfer_delivery_id,
            inventory_transfer_request_id,
            value_date,
            login_id,
            user_id,
            office_id,
            source_store_id,
            destination_store_id,
            reference_number,
            statement_reference
    )
    SELECT
            nextval(pg_get_serial_sequence('transactions.inventory_transfer_deliveries', 'inventory_transfer_delivery_id')),
            _inventory_transfer_request_id,
            _value_date,
            _login_id,
            _user_id,
            _office_id,
            _source_store_id,
            _destination_store_id,
            _reference_number,
            _statement_reference;


    _inventory_transfer_delivery_id                          := currval(pg_get_serial_sequence('transactions.inventory_transfer_deliveries', 'inventory_transfer_delivery_id'));

    INSERT INTO transactions.inventory_transfer_delivery_details
    (
        inventory_transfer_delivery_id,
        value_date,
        item_id,
        quantity,
        unit_id,
        base_quantity,
        base_unit_id
    )
    SELECT 
        _inventory_transfer_delivery_id, 
        _value_date, 
        item_id, 
        quantity, 
        unit_id, 
        base_quantity, 
        base_unit_id
    FROM temp_stock_details;


    UPDATE transactions.inventory_transfer_requests SET
        delivered = true,
        delivered_on = NOW(),
        delivered_by_user_id = _user_id
    WHERE inventory_transfer_request_id = _inventory_transfer_request_id;
    
    RETURN _inventory_transfer_delivery_id;
END
$$
LANGUAGE plpgsql;


-- SELECT * FROM transactions.post_inventory_transfer_delivery(2, 2, 5, 1, '1-1-2020', '22', 'Test', 1, 1,
-- ARRAY[
-- ROW('Dr', 'Store 1', 'RMBP', 'Dozen', 2)::transactions.stock_adjustment_type,
-- ROW('Dr', 'Store 1', 'SFIX', 'Piece', 24)::transactions.stock_adjustment_type
-- ]
-- );
-- 
-- 
