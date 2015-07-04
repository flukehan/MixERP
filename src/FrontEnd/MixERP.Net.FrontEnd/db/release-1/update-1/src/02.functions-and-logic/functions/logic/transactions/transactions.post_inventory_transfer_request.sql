DROP FUNCTION IF EXISTS transactions.post_inventory_transfer_request
(
    _office_id                              integer,
    _user_id                                integer,
    _login_id                               bigint,
    _value_date                             date,
    _reference_number                       national character varying(24),
    _statement_reference                    text,
    _details                                transactions.stock_adjustment_type[]
);


CREATE FUNCTION transactions.post_inventory_transfer_request
(
    _office_id                              integer,
    _user_id                                integer,
    _login_id                               bigint,
    _value_date                             date,
    _reference_number                       national character varying(24),
    _statement_reference                    text,
    _details                                transactions.stock_adjustment_type[]
)
RETURNS bigint
AS
$$
    DECLARE _inventory_transfer_request_id          bigint;
    DECLARE _stock_master_id                        bigint;
    DECLARE _store_id                               integer;
BEGIN
    IF(policy.can_post_transaction(_login_id, _user_id, _office_id, 'Inventory.Transfer.Request', _value_date) = false) THEN
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
        RAISE EXCEPTION 'Stock transfer request can only contain debit entries.'
        USING ERRCODE='P5003';
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
        RAISE EXCEPTION 'You cannot provide more than one store for this transaction.'
        USING ERRCODE='P5205';
    END IF;

    UPDATE temp_stock_details SET 
    item_id         = core.get_item_id_by_item_code(item_code),
    unit_id         = core.get_unit_id_by_unit_name(unit_name),
    store_id        = office.get_store_id_by_store_name(store_name);

    SELECT store_id INTO _store_id
    FROM temp_stock_details
    LIMIT 1;

    IF EXISTS
    (
        SELECT * FROM temp_stock_details
        WHERE item_id IS NULL OR unit_id IS NULL OR store_id IS NULL
    ) THEN
        RAISE EXCEPTION 'Invalid data supplied.'
        USING ERRCODE='P3000';
    END IF;

    UPDATE temp_stock_details SET
    base_unit_id    = core.get_root_unit_id(unit_id),
    base_quantity   = core.get_base_quantity_by_unit_id(unit_id, quantity),
    price           = core.get_item_cost_price(item_id, unit_id, NULL);

    INSERT INTO transactions.inventory_transfer_requests
    (
            inventory_transfer_request_id,
            value_date,
            store_id,
            login_id,
            user_id,
            office_id,
            reference_number,
            statement_reference
    )
    SELECT
            nextval(pg_get_serial_sequence('transactions.inventory_transfer_requests', 'inventory_transfer_request_id')), 
            _value_date,
            _store_id,
            _login_id,
            _user_id,
            _office_id,
            _reference_number,
            _statement_reference;


    _inventory_transfer_request_id                          := currval(pg_get_serial_sequence('transactions.inventory_transfer_requests', 'inventory_transfer_request_id'));

    INSERT INTO transactions.inventory_transfer_request_details
    (
        inventory_transfer_request_id,
        value_date,
        item_id,
        quantity,
        unit_id,
        base_quantity,
        base_unit_id
    )
    SELECT 
        _inventory_transfer_request_id, 
        _value_date, 
        item_id, 
        quantity, 
        unit_id, 
        base_quantity, 
        base_unit_id
    FROM temp_stock_details;
    
    
    RETURN _inventory_transfer_request_id;
END
$$
LANGUAGE plpgsql;

-- 
-- SELECT * FROM transactions.post_inventory_transfer_request(2, 2, 5, '1-1-2020', '22', 'Test', 
-- ARRAY[
-- ROW('Dr', 'Store 1', 'RMBP', 'Dozen', 2)::transactions.stock_adjustment_type,
-- ROW('Dr', 'Store 1', 'SFIX', 'Piece', 24)::transactions.stock_adjustment_type
-- ]
-- );


