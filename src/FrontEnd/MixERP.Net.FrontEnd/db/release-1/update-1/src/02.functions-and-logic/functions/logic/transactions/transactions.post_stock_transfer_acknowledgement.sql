DROP FUNCTION IF EXISTS transactions.post_stock_transfer_acknowledgement
(
    _office_id                              integer,
    _user_id                                integer,
    _login_id                               bigint,
    _request_id                             bigint
);

CREATE FUNCTION transactions.post_stock_transfer_acknowledgement
(
    _office_id                              integer,
    _user_id                                integer,
    _login_id                               bigint,
    _request_id                             bigint
)
RETURNS bigint
AS
$$
    DECLARE _value_date                             date=transactions.get_value_date(_office_id);
    DECLARE _book_name                              text='Inventory.Transfer';
    DECLARE _reference_number                       national character varying(24);
    DECLARE _statement_reference                    text;
    DECLARE _source_store_id                        integer;
    DECLARE _destination_store_id                   integer;
    DECLARE _details                                transactions.stock_adjustment_type[];
    DECLARE _transaction_master_id                  bigint;
BEGIN
    IF(policy.can_post_transaction(_login_id, _user_id, _office_id, _book_name, _value_date) = false) THEN
        RETURN 0;
    END IF;

    IF NOT EXISTS
    (
        SELECT * FROM transactions.inventory_transfer_requests
        WHERE inventory_transfer_request_id = _request_id
        AND office_id = _office_id
    ) THEN
        RAISE EXCEPTION 'Invalid office.'
        USING ERRCODE='P3011';
    END IF;

    IF NOT EXISTS
    (
        SELECT * FROM transactions.inventory_transfer_requests
        WHERE inventory_transfer_request_id = _request_id
        AND authorization_status_id > 0
    ) THEN
        RAISE EXCEPTION 'Acess is denied. This transaction was rejected by administrator.'
        USING ERRCODE='P9250';
    END IF;

    IF NOT EXISTS
    (
        SELECT * FROM transactions.inventory_transfer_delivery_details
        WHERE inventory_transfer_delivery_id = 
        (
            SELECT inventory_transfer_delivery_id
            FROM transactions.inventory_transfer_deliveries
            WHERE inventory_transfer_request_id = _request_id
        )
    ) THEN
        RAISE EXCEPTION 'Cannot receive a stock transfer because the delivery contains no item.'
        USING ERRCODE='P5005';
    END IF;

    SELECT 
        reference_number, 
        statement_reference,
        source_store_id,
        destination_store_id
    INTO
        _reference_number, 
        _statement_reference,
        _source_store_id,
        _destination_store_id
    FROM transactions.inventory_transfer_deliveries
    WHERE inventory_transfer_request_id = _request_id;

    DROP TABLE IF EXISTS temp_details;

    CREATE TEMPORARY TABLE temp_details
    (
        tran_type       transaction_type,
        store_name      national character varying(50),
        item_code       national character varying(12),
        unit_name       national character varying(50),
        quantity        integer_strict
    ) ON COMMIT DROP;

    INSERT INTO temp_details(tran_type, store_name, item_code, unit_name, quantity)
    SELECT 
        'Dr',
        office.get_store_name_by_store_id(_destination_store_id),
        core.get_item_code_by_item_id(item_id),
        core.get_unit_name_by_unit_id(unit_id),
        quantity
    FROM transactions.inventory_transfer_delivery_details
    WHERE inventory_transfer_delivery_id = 
    (
        SELECT inventory_transfer_delivery_id
        FROM transactions.inventory_transfer_deliveries
        WHERE inventory_transfer_request_id = _request_id
    );

    INSERT INTO temp_details(tran_type, store_name, item_code, unit_name, quantity)
    SELECT 
        'Cr', 
        office.get_store_name_by_store_id(_source_store_id),
        item_code, 
        unit_name, 
        quantity
    FROM temp_details;

    SELECT 
        array_agg
        (
            ROW(tran_type, store_name, item_code, unit_name, quantity)::transactions.stock_adjustment_type
        )
    INTO
        _details
    FROM temp_details;

    _transaction_master_id := transactions.post_stock_journal(_office_id, _user_id, _login_id, _value_date, _reference_number, _statement_reference, _details);
    
    UPDATE transactions.inventory_transfer_requests
    SET received = true
    WHERE inventory_transfer_request_id = _request_id;

    RETURN _transaction_master_id;
END
$$
LANGUAGE plpgsql;

-- 
-- SELECT * FROM transactions.post_stock_transfer_acknowledgement
-- (
--     2,
--     3,
--     5,
--     1
-- );
-- 
-- 
-- ROLLBACK;