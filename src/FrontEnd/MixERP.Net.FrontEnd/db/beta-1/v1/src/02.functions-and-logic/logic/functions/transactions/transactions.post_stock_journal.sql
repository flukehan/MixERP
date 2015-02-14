DROP FUNCTION IF EXISTS transactions.post_stock_journal
(
    _office_id                              integer,
    _user_id                                integer,
    _login_id                               bigint,
    _value_date                             date,
    _reference_number                       national character varying(24),
    _statement_reference                    text,
    _details                                transactions.stock_adjustment_type[]
);


CREATE FUNCTION transactions.post_stock_journal
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
    DECLARE _transaction_master_id                  bigint;
    DECLARE _stock_master_id                        bigint;
    DECLARE _book_name                              text='Stock.Transfer';
BEGIN
    IF(policy.can_post_transaction(_login_id, _user_id, _office_id, _book_name, _value_date) = false) THEN
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
        GROUP BY item_code, store_name
        HAVING COUNT(item_code) <> 1
    ) THEN
        RAISE EXCEPTION 'An item can appear only once in a store.'
        USING ERRCODE='P5202';
    END IF;

    IF EXISTS
    (
        SELECT item_code FROM temp_stock_details
        GROUP BY item_code
        HAVING SUM(CASE WHEN tran_type='Dr' THEN quantity ELSE quantity *-1 END) <> 0
    ) THEN
        RAISE EXCEPTION 'Referencing sides are not equal.'
        USING ERRCODE='P5000';        
    END IF;


    UPDATE temp_stock_details SET 
    item_id         = core.get_item_id_by_item_code(item_code),
    unit_id         = core.get_unit_id_by_unit_name(unit_name),
    store_id        = office.get_store_id_by_store_name(store_name);

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



    IF EXISTS
    (
            SELECT 1
            FROM 
            office.stores
            WHERE office.stores.store_id
            IN
            (
                SELECT temp_stock_details.store_id
                FROM temp_stock_details
            )
            HAVING COUNT(DISTINCT office.stores.office_id) > 1

    ) THEN
        RAISE EXCEPTION E'Access is denied!\nA stock journal transaction cannot references multiple branches.'
        USING ERRCODE='P9013';
    END IF;

    IF EXISTS
    (
            SELECT 1
            FROM 
            temp_stock_details
            WHERE tran_type = 'Cr'
            AND quantity > core.count_item_in_stock(item_id, unit_id, store_id)
    ) THEN
        RAISE EXCEPTION 'Negative stock is not allowed.'
        USING ERRCODE='P5001';
    END IF;

    INSERT INTO transactions.transaction_master
    (
            transaction_master_id,
            transaction_counter,
            transaction_code,
            book,
            value_date,
            login_id,
            user_id,
            office_id,
            reference_number,
            statement_reference
    )
    SELECT
            nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id')), 
            transactions.get_new_transaction_counter(_value_date), 
            transactions.get_transaction_code(_value_date, _office_id, _user_id, _login_id),
            _book_name,
            _value_date,
            _login_id,
            _user_id,
            _office_id,
            _reference_number,
            _statement_reference;


    _transaction_master_id                          := currval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));


    INSERT INTO transactions.stock_master(stock_master_id, transaction_master_id, value_date)
    SELECT nextval(pg_get_serial_sequence('transactions.stock_master', 'stock_master_id')), _transaction_master_id, _value_date;

    _stock_master_id                                := currval(pg_get_serial_sequence('transactions.stock_master', 'stock_master_id'));

    INSERT INTO transactions.stock_details(stock_master_id, value_date, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price)
    SELECT _stock_master_id, _value_date, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price
    FROM temp_stock_details;
    
    
    PERFORM transactions.auto_verify(_transaction_master_id, _office_id);
    RETURN _transaction_master_id;
END
$$
LANGUAGE plpgsql;


-- SELECT * FROM transactions.post_stock_journal(2, 2, 5, '1-1-2020', '22', 'Test', 
-- ARRAY[
-- ROW('Cr', 'Store 1', 'RMBP', 'Piece', 1)::transactions.stock_adjustment_type,
-- ROW('Dr', 'Godown 1', 'RMBP', 'Piece', 1)::transactions.stock_adjustment_type,
-- ROW('Cr', 'Store 1', '11MBA', 'Piece', 1)::transactions.stock_adjustment_type,
-- ROW('Dr', 'Godown 1', '11MBA', 'Piece', 1)::transactions.stock_adjustment_type
-- ]
-- );


