DROP FUNCTION IF EXISTS transactions.post_purchase_return
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

CREATE FUNCTION transactions.post_purchase_return
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
    DECLARE _party_id                       bigint;
    DECLARE _cost_center_id                 bigint;
    DECLARE _tran_master_id                 bigint;
    DECLARE _stock_detail_id                bigint;
    DECLARE _tran_counter                   integer;
    DECLARE _transaction_code               text;
    DECLARE _stock_master_id                bigint;
    DECLARE _grand_total                    money_strict;
    DECLARE _discount_total                 money_strict2;
    DECLARE _tax_total                      money_strict2;
    DECLARE _is_credit                      boolean;
    DECLARE _credit_account_id              bigint;
    DECLARE _default_currency_code          national character varying(12);
    DECLARE _sm_id                          bigint;
    DECLARE this                            RECORD;
    DECLARE _shipping_address_code          national character varying(12);
    DECLARE _is_periodic                    boolean = office.is_periodic_inventory(_office_id);
    DECLARE _book_name                      text='Purchase.Return';
    DECLARE _receivable                     money_strict;
BEGIN
    IF(policy.can_post_transaction(_login_id, _user_id, _office_id, _book_name, _value_date) = false) THEN
        RETURN 0;
    END IF;

    IF(NOT transactions.validate_items_for_return(_transaction_master_id, _details)) THEN
        RETURN 0;
    END IF;
    
    CREATE TEMPORARY TABLE temp_stock_details
    (
        id                              SERIAL PRIMARY KEY,
        stock_master_id                 bigint, 
        tran_type                       transaction_type, 
        store_id                        integer,
        item_code                       text,
        item_id                         integer, 
        quantity                        integer_strict,
        unit_name                       text,
        unit_id                         integer,
        base_quantity                   decimal,
        base_unit_id                    integer,                
        price                           money_strict,
        discount                        money_strict2,
        shipping_charge                 money_strict2,
        tax_form                        text,
        sales_tax_id                    integer,
        tax                             money_strict2,
        purchase_account_id             integer, 
        purchase_discount_account_id    integer, 
        inventory_account_id            integer
    ) ON COMMIT DROP;

    CREATE TEMPORARY TABLE temp_stock_tax_details
    (
        id                                      SERIAL,
        temp_stock_detail_id                    integer REFERENCES temp_stock_details(id),
        sales_tax_detail_code                   text,
        stock_detail_id                         bigint,
        sales_tax_detail_id                     integer,
        state_sales_tax_id                      integer,
        county_sales_tax_id                     integer,
        account_id                              integer,
        principal                               money_strict,
        rate                                    decimal_strict,
        tax                                     money_strict
    ) ON COMMIT DROP;

    CREATE TEMPORARY TABLE temp_transaction_details
    (
        transaction_master_id       BIGINT, 
        tran_type                   transaction_type, 
        account_id                  integer, 
        statement_reference         text, 
        cash_repository_id          integer, 
        currency_code               national character varying(12), 
        amount_in_currency          money_strict, 
        local_currency_code         national character varying(12), 
        er                          decimal_strict, 
        amount_in_local_currency    money_strict
    ) ON COMMIT DROP;

    _party_id                       := core.get_party_id_by_party_code(_party_code);
    _default_currency_code          := transactions.get_default_currency_code_by_office_id(_office_id);
    
    SELECT 
        cost_center_id   
    INTO 
        _cost_center_id    
    FROM transactions.transaction_master 
    WHERE transactions.transaction_master.transaction_master_id = _transaction_master_id;

    SELECT 
        is_credit,
        core.get_shipping_address_code_by_shipping_address_id(shipping_address_id),
        stock_master_id
    INTO 
        _is_credit,
        _shipping_address_code,
        _sm_id
    FROM transactions.stock_master 
    WHERE transaction_master_id = _transaction_master_id;

    INSERT INTO temp_stock_details(store_id, item_code, quantity, unit_name, price, discount, shipping_charge, tax_form, tax)
    SELECT store_id, item_code, quantity, unit_name, price, discount, shipping_charge, tax_form, tax
    FROM explode_array(_details);

    UPDATE temp_stock_details 
    SET
        tran_type                   = 'Cr',
        sales_tax_id                = core.get_sales_tax_id_by_sales_tax_code(tax_form),
        item_id                     = core.get_item_id_by_item_code(item_code),
        unit_id                     = core.get_unit_id_by_unit_name(unit_name),
        base_quantity               = core.get_base_quantity_by_unit_name(unit_name, quantity),
        base_unit_id                = core.get_base_unit_id_by_unit_name(unit_name);

    UPDATE temp_stock_details
    SET
        purchase_account_id             = core.get_purchase_account_id(item_id),
        purchase_discount_account_id    = core.get_purchase_discount_account_id(item_id),
        inventory_account_id            = core.get_inventory_account_id(item_id);

    IF EXISTS
    (

        SELECT * 
        FROM transactions.stock_details
        INNER JOIN temp_stock_details
        ON temp_stock_details.item_id = transactions.stock_details.item_id
        WHERE transactions.stock_details.stock_master_id = _sm_id
        AND COALESCE(temp_stock_details.sales_tax_id, 0) != COALESCE(transactions.stock_details.sales_tax_id, 0)
        LIMIT 1
    ) THEN
        RAISE EXCEPTION 'Tax form mismatch.'
        USING ERRCODE='P3202';
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


    FOR this IN SELECT * FROM temp_stock_details ORDER BY id
    LOOP
        INSERT INTO temp_stock_tax_details
        (
            temp_stock_detail_id,
            sales_tax_detail_code,
            account_id, 
            sales_tax_detail_id, 
            state_sales_tax_id, 
            county_sales_tax_id, 
            principal, 
            rate, 
            tax
        )
        SELECT 
            this.id, 
            sales_tax_detail_code,
            account_id, 
            sales_tax_detail_id, 
            state_sales_tax_id, 
            county_sales_tax_id, 
            taxable_amount, 
            rate, 
            tax
        FROM transactions.get_sales_tax('Purchase', _store_id, _party_code, _shipping_address_code, _price_type_id, this.item_code, this.price, this.quantity, this.discount, this.shipping_charge, this.sales_tax_id);
    END LOOP;
    
    UPDATE temp_stock_details
    SET tax =
    (SELECT SUM(COALESCE(temp_stock_tax_details.tax, 0)) FROM temp_stock_tax_details
    WHERE temp_stock_tax_details.temp_stock_detail_id = temp_stock_details.id);

    _credit_account_id = core.get_account_id_by_party_code(_party_code); 

        
    SELECT SUM(COALESCE(tax, 0))                                INTO _tax_total FROM temp_stock_tax_details;
    SELECT SUM(COALESCE(discount, 0))                           INTO _discount_total FROM temp_stock_details;
    SELECT SUM(COALESCE(price, 0) * COALESCE(quantity, 0))      INTO _grand_total FROM temp_stock_details;

    _receivable := _grand_total - COALESCE(_discount_total, 0) + COALESCE(_tax_total, 0);


    IF(_is_periodic = true) THEN
        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Cr', purchase_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0)), 1, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0))
        FROM temp_stock_details
        GROUP BY purchase_account_id;
    ELSE
        --Perpetutal Inventory Accounting System
        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Cr', inventory_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0)), 1, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0))
        FROM temp_stock_details
        GROUP BY inventory_account_id;
    END IF;


    IF(COALESCE(_tax_total, 0) > 0) THEN
        FOR this IN 
        SELECT 
            format('P: %s x R: %s %% = %s (%s)', principal::text, rate::text, tax::text, sales_tax_detail_code) as statement_reference,
            account_id,
            tax
        FROM temp_stock_tax_details ORDER BY id
        LOOP    
            INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
            SELECT 'Cr', this.account_id, this.statement_reference || _statement_reference, _default_currency_code, this.tax, 1, _default_currency_code, this.tax;
        END LOOP;
    END IF;

    IF(COALESCE(_discount_total, 0) > 0) THEN
        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Dr', purchase_discount_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(discount, 0)), 1, _default_currency_code, SUM(COALESCE(discount, 0))
        FROM temp_stock_details
        GROUP BY purchase_discount_account_id;
    END IF;

    INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
    SELECT 'Dr', core.get_account_id_by_party_id(_party_id), _statement_reference, _default_currency_code, _receivable, 1, _default_currency_code, _receivable;


    _tran_master_id         := nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));
    _stock_master_id        := nextval(pg_get_serial_sequence('transactions.stock_master', 'stock_master_id'));
    _tran_counter           := transactions.get_new_transaction_counter(_value_date);
    _transaction_code       := transactions.get_transaction_code(_value_date, _office_id, _user_id, _login_id);

    UPDATE temp_transaction_details     SET transaction_master_id   = _tran_master_id;
    UPDATE temp_stock_details           SET stock_master_id         = _stock_master_id;

    INSERT INTO transactions.transaction_master(transaction_master_id, transaction_counter, transaction_code, book, value_date, user_id, login_id, office_id, cost_center_id, reference_number, statement_reference) 
    SELECT _tran_master_id, _tran_counter, _transaction_code, _book_name, _value_date, _user_id, _login_id, _office_id, _cost_center_id, _reference_number, _statement_reference;


    INSERT INTO transactions.transaction_details(value_date, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency)
    SELECT _value_date, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency
    FROM temp_transaction_details
    ORDER BY tran_type DESC;


    INSERT INTO transactions.stock_master(value_date, stock_master_id, transaction_master_id, party_id, price_type_id, is_credit, shipper_id, shipping_charge, store_id, cash_repository_id)
    SELECT _value_date, _stock_master_id, _tran_master_id, _party_id, _price_type_id, _is_credit, NULL, 0, _store_id, NULL;
            
    FOR this IN SELECT * FROM temp_stock_details ORDER BY id
    LOOP
        _stock_detail_id        := nextval(pg_get_serial_sequence('transactions.stock_details', 'stock_detail_id'));

        INSERT INTO transactions.stock_details(stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, sales_tax_id, tax)
        SELECT _stock_detail_id, _value_date, this.stock_master_id, this.tran_type, this.store_id, this.item_id, this.quantity, this.unit_id, this.base_quantity, this.base_unit_id, this.price, this.discount, this.sales_tax_id, COALESCE(this.tax, 0)
        FROM temp_stock_details
        WHERE id = this.id;


        INSERT INTO transactions.stock_tax_details(stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax)
        SELECT _stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax
        FROM temp_stock_tax_details
        WHERE temp_stock_detail_id = this.id;
        
    END LOOP;

    INSERT INTO transactions.stock_return(transaction_master_id, return_transaction_master_id)
    SELECT _transaction_master_id, _tran_master_id;

    IF(array_length(_attachments, 1) > 0 AND _attachments != ARRAY[NULL::core.attachment_type]) THEN
        INSERT INTO core.attachments(user_id, resource, resource_key, resource_id, original_file_name, file_extension, file_path, comment)
        SELECT _user_id, 'transactions.transaction_master', 'transaction_master_id', _tran_master_id, original_file_name, file_extension, file_path, comment 
        FROM explode_array(_attachments);
    END IF;
    
    PERFORM transactions.auto_verify(_tran_master_id, _office_id);
    RETURN _tran_master_id;
END
$$
LANGUAGE plpgsql;




-- CREATE TEMPORARY TABLE temp_purchase_return
-- ON COMMIT DROP
-- AS
-- 
-- SELECT * FROM transactions.post_purchase_return(5, 2, 2, 1, '1-1-2000', 1, 'MAJON-0002', 1, '1234-AD', 'Test', 
-- ARRAY[
--  ROW(1, 'RMBP', 1, 'Piece', 180000, 0, 200, 'MoF-NY-BK-STX', 0)::transactions.stock_detail_type,
--  ROW(1, '13MBA', 1, 'Piece', 110000, 5000, 50, 'MoF-NY-BK-STX', 0)::transactions.stock_detail_type
-- ],
-- ARRAY[
-- NULL::core.attachment_type
-- ]);
-- 
-- SELECT  tran_type, core.get_account_name_by_account_id(account_id), amount_in_local_currency 
-- FROM transactions.transaction_details
-- WHERE transaction_master_id  = (SELECT * FROM temp_purchase_return);


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


