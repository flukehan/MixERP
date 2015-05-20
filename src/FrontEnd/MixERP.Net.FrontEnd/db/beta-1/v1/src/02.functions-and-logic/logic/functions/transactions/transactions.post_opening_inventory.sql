DROP FUNCTION IF EXISTS transactions.post_opening_inventory
(
    _office_id                              integer,
    _user_id                                integer,
    _login_id                               bigint,
    _value_date                             date,
    _reference_number                       national character varying(24),
    _statement_reference                    text,
    _details                                transactions.opening_stock_type[]    
);

CREATE FUNCTION transactions.post_opening_inventory
(
    _office_id                              integer,
    _user_id                                integer,
    _login_id                               bigint,
    _value_date                             date,
    _reference_number                       national character varying(24),
    _statement_reference                    text,
    _details                                transactions.opening_stock_type[]    
)
RETURNS bigint
VOLATILE
AS
$$
    DECLARE _book_name                      text = 'Opening.Inventory';
    DECLARE _transaction_master_id          bigint;
    DECLARE _stock_master_id                bigint;
    DECLARE _tran_counter                   integer;
    DECLARE _transaction_code               text;
BEGIN
    IF(policy.can_post_transaction(_login_id, _user_id, _office_id, _book_name, _value_date) = false) THEN
        RETURN 0;
    END IF;

    DROP TABLE IF EXISTS temp_stock_details;
    
    CREATE TEMPORARY TABLE temp_stock_details
    (
        id                              SERIAL PRIMARY KEY,
        tran_type                       transaction_type,
        store_name                      text, 
        store_id                        integer,
        item_code                       text,
        item_id                         integer, 
        quantity                        integer_strict,
        unit_name                       text,
        unit_id                         integer,
        base_quantity                   decimal,
        base_unit_id                    integer,                
        price                           money_strict
    ) ON COMMIT DROP;

    INSERT INTO temp_stock_details(store_name, item_code, quantity, unit_name, price)
    SELECT store_name, item_code, quantity, unit_name, amount
    FROM explode_array(_details);

    UPDATE temp_stock_details 
    SET
        tran_type                       = 'Dr',
        store_id                        = office.get_store_id_by_store_name(store_name),
        item_id                         = core.get_item_id_by_item_code(item_code),
        unit_id                         = core.get_unit_id_by_unit_name(unit_name),
        base_quantity                   = core.get_base_quantity_by_unit_name(unit_name, quantity),
        base_unit_id                    = core.get_base_unit_id_by_unit_name(unit_name);

    IF EXISTS
    (
        SELECT * FROM temp_stock_details
        WHERE store_id IS NULL
        OR item_id IS NULL
        OR unit_id IS NULL
    ) THEN
        RAISE EXCEPTION 'Access is denied. Invalid values supplied.'
        USING ERRCODE='P9011';
    END IF;

    IF EXISTS
    (
            SELECT 1 FROM temp_stock_details AS details
            WHERE core.is_valid_unit_id(details.unit_id, details.item_id) = false
            LIMIT 1
    ) THEN
        RAISE EXCEPTION 'Item/unit mismatch.'
        USING ERRCODE='P3201';
    END IF;

    
    _transaction_master_id  := nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));
    _stock_master_id        := nextval(pg_get_serial_sequence('transactions.stock_master', 'stock_master_id'));
    _tran_counter           := transactions.get_new_transaction_counter(_value_date);
    _transaction_code       := transactions.get_transaction_code(_value_date, _office_id, _user_id, _login_id);

    INSERT INTO transactions.transaction_master(transaction_master_id, transaction_counter, transaction_code, book, value_date, user_id, login_id, office_id, reference_number, statement_reference) 
    SELECT _transaction_master_id, _tran_counter, _transaction_code, _book_name, _value_date, _user_id, _login_id, _office_id, _reference_number, _statement_reference;

    INSERT INTO transactions.stock_master(value_date, stock_master_id, transaction_master_id)
    SELECT _value_date, _stock_master_id, _transaction_master_id;

    INSERT INTO transactions.stock_details(value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price)
    SELECT _value_date, _stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price
    FROM temp_stock_details;
    
    PERFORM transactions.auto_verify(_transaction_master_id, _office_id);    
    RETURN _transaction_master_id;
END;
$$
LANGUAGE plpgsql;


-- SELECT * FROM transactions.post_opening_inventory
-- (
--     2,
--     2,
--     5,
--     transactions.get_value_date(2),
--     '3424',
--     'ASDF',
--     ARRAY[
--          ROW('Store 1', 'RMBP', 1, 'Box',180000)::transactions.opening_stock_type,
--          ROW('Store 1', '13MBA', 1, 'Dozen',130000)::transactions.opening_stock_type,
--          ROW('Store 1', '11MBA', 1, 'Piece',110000)::transactions.opening_stock_type]);
