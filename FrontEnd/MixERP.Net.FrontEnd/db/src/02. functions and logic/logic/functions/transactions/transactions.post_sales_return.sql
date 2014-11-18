DROP FUNCTION IF EXISTS transactions.post_sales_return
(
    _transaction_master_id          bigint,
    _office_id                      integer,
    _user_id                        integer,
    _login_id                       bigint,
    _value_date                     date,
    _store_id                       integer,
    _party_code                     national character varying(12),
    _price_type_id                  integer,
    _reference_number               national character varying(24),
    _statement_reference            text,
    _details                        transactions.stock_detail_type[],
    _attachments                    core.attachment_type[]
);

CREATE FUNCTION transactions.post_sales_return
(
    _transaction_master_id          bigint,
    _office_id                      integer,
    _user_id                        integer,
    _login_id                       bigint,
    _value_date                     date,
    _store_id                       integer,
    _party_code                     national character varying(12),
    _price_type_id                  integer,
    _reference_number               national character varying(24),
    _statement_reference            text,
    _details                        transactions.stock_detail_type[],
    _attachments                    core.attachment_type[]
)
RETURNS bigint
AS
$$
    DECLARE _party_id               bigint;
    DECLARE _cost_center_id         bigint;
    DECLARE _tran_master_id         bigint;
    DECLARE _tran_counter           integer;
    DECLARE _tran_code              text;
    DECLARE _stock_master_id        bigint;
    DECLARE _grand_total            money_strict;
    DECLARE _discount_total         money_strict2;
    DECLARE _tax_total              money_strict2;
    DECLARE _is_credit              boolean;
    DECLARE _credit_account_id      bigint;
    DECLARE _default_currency_code  national character varying(12);
    DECLARE _cost_of_goods_sold     money_strict2;
    DECLARE _sm_id                  bigint;
BEGIN
    
    _party_id                       := core.get_party_id_by_party_code(_party_code);
    _default_currency_code          := transactions.get_default_currency_code_by_office_id(_office_id);
    
    SELECT cost_center_id   INTO _cost_center_id    FROM transactions.transaction_master WHERE transactions.transaction_master.transaction_master_id = _transaction_master_id;
    SELECT is_credit        INTO _is_credit         FROM transactions.stock_master WHERE transaction_master_id = _transaction_master_id;

    CREATE TEMPORARY TABLE temp_stock_details
    (
        stock_master_id                 bigint, 
        tran_type                       transaction_type, 
        store_id                        integer,
        item_code                       national character varying(12),
        item_id                         integer, 
        quantity                        integer_strict,
        unit_name                       national character varying(50),
        unit_id                         integer,
        base_quantity                   decimal,
        base_unit_id                    integer,                
        price                           money_strict,
        cost_of_goods_sold              money_strict2 DEFAULT(0),
        discount                        money_strict2,
        tax_rate                        decimal_strict2,
        tax                             money_strict2
    ) ON COMMIT DROP;

    INSERT INTO temp_stock_details(store_id, item_code, quantity, unit_name, price, discount, tax_rate, tax)
    SELECT store_id, item_code, quantity, unit_name, price, discount, tax_rate, tax
    FROM explode_array(_details);

    UPDATE temp_stock_details 
    SET
        tran_type                   = 'Dr',
        item_id                     = core.get_item_id_by_item_code(item_code),
        unit_id                     = core.get_unit_id_by_unit_name(unit_name),
        base_quantity               = core.get_base_quantity_by_unit_name(unit_name, quantity),
        base_unit_id                = core.get_base_unit_id_by_unit_name(unit_name);


    IF(_is_credit) THEN
        _credit_account_id = core.get_account_id_by_party_code(_party_code); 
    ELSE
        _credit_account_id = core.get_account_id_by_parameter('Sales.Return');
    END IF;


    _tran_master_id             := nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));
    _stock_master_id            := nextval(pg_get_serial_sequence('transactions.stock_master', 'stock_master_id'));
    _tran_counter               := transactions.get_new_transaction_counter(_value_date);
    _tran_code                  := transactions.get_transaction_code(_value_date, _office_id, _user_id, _login_id);

    INSERT INTO transactions.transaction_master(transaction_master_id, transaction_counter, transaction_code, book, value_date, user_id, login_id, office_id, cost_center_id, reference_number, statement_reference)
    SELECT _tran_master_id, _tran_counter, _tran_code, 'Sales.Return', _value_date, _user_id, _login_id, _office_id, _cost_center_id, _reference_number, _statement_reference;
        
    SELECT      SUM(price * quantity),  SUM(discount),      SUM(tax)
    INTO        _grand_total,           _discount_total,    _tax_total        
    FROM        explode_array(_details);


    SELECT stock_master_id INTO _sm_id FROM transactions.stock_master WHERE transaction_master_id = _transaction_master_id;

    UPDATE temp_stock_details
    SET cost_of_goods_sold = transactions.get_write_off_cost_of_goods_sold(_sm_id, item_id, unit_id, quantity);


    SELECT SUM(cost_of_goods_sold) INTO _cost_of_goods_sold FROM temp_stock_details;


    IF(_cost_of_goods_sold > 0) THEN
        INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT _tran_master_id, _value_date, 'Dr', core.get_account_id_by_parameter('Inventory'), _statement_reference, _default_currency_code, _cost_of_goods_sold, 1, _default_currency_code, _cost_of_goods_sold UNION ALL
        SELECT _tran_master_id, _value_date, 'Cr', core.get_account_id_by_parameter('COGS'), _statement_reference, _default_currency_code, _cost_of_goods_sold, 1, _default_currency_code, _cost_of_goods_sold;                         
    END IF;


    INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, local_currency_code, er,amount_in_local_currency) 
    SELECT _tran_master_id, _value_date, 'Dr', core.get_account_id_by_parameter('Sales'), _statement_reference, _default_currency_code, _grand_total, _default_currency_code, 1, _grand_total;

    IF(_tax_total IS NOT NULL AND _tax_total > 0) THEN
        INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency)
        SELECT _tran_master_id, _value_date, 'Dr', core.get_account_id_by_parameter('Sales.Tax'), _statement_reference, _default_currency_code, _tax_total, _default_currency_code, 1, _tax_total;
    END IF;

    IF(_discount_total IS NOT NULL AND _discount_total > 0) THEN
        INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency) 
        SELECT _tran_master_id, _value_date, 'Cr', core.get_account_id_by_parameter('Sales.Discount'), _statement_reference, _default_currency_code, _discount_total, _default_currency_code, 1, _discount_total;
    END IF;

    INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency) 
    SELECT _tran_master_id, _value_date, 'Cr', _credit_account_id, _statement_reference, _default_currency_code, _grand_total + _tax_total - _discount_total, _default_currency_code, 1, _grand_total + _tax_total - _discount_total;


    INSERT INTO transactions.stock_master(stock_master_id, value_date, transaction_master_id, party_id, price_type_id, is_credit, store_id) 
    SELECT _stock_master_id, _value_date, _tran_master_id, _party_id, _price_type_id, false, _store_id;


    INSERT INTO transactions.stock_details(value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, tax_rate, tax)
    SELECT _value_date, _stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, tax_rate, tax FROM temp_stock_details;

    INSERT INTO transactions.stock_return(transaction_master_id, return_transaction_master_id)
    SELECT _transaction_master_id, _tran_master_id;

    RETURN _tran_master_id;
END
$$
LANGUAGE plpgsql;

 
 
--  SELECT * FROM transactions.post_sales_return(5, 2, 1, 1, '1-1-2000', 1, 'MAJON-0002', 1, '1234-AD', 'Test', 
--  ARRAY[
--  ROW(1, 'RMBP', 1, 'Piece', 225000, 0, 13, 29250)::transactions.stock_detail_type--,
--  --ROW(1, 'ITP', 1, 'Piece', 1000, 0, 13, 130)::transactions.stock_detail_type
--  ],
--  ARRAY[
--  NULL::core.attachment_type
--  ]);


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


