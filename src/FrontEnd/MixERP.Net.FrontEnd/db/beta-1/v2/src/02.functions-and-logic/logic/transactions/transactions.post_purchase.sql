DROP FUNCTION IF EXISTS transactions.post_purchase
(
    _book_name                              national character varying(12),
    _office_id                              integer,
    _user_id                                integer,
    _login_id                               bigint,
    _value_date                             date,
    _cost_center_id                         integer,
    _reference_number                       national character varying(24),
    _statement_reference                    text,
    _is_credit                              boolean,
    _party_code                             national character varying(12),
    _price_type_id                          integer,
    _shipper_id                             integer,
    _store_id                               integer,
    _tran_ids                               bigint[],
    _details                                transactions.stock_detail_type[],
    _attachments                            core.attachment_type[]
);


CREATE FUNCTION transactions.post_purchase
(
    _book_name                              national character varying(48),
    _office_id                              integer,
    _user_id                                integer,
    _login_id                               bigint,
    _value_date                             date,
    _cost_center_id                         integer,
    _reference_number                       national character varying(24),
    _statement_reference                    text,
    _is_credit                              boolean,
    _party_code                             national character varying(12),
    _price_type_id                          integer,
    _shipper_id                             integer,
    _store_id                               integer,
    _tran_ids                               bigint[],
    _details                                transactions.stock_detail_type[],
    _attachments                            core.attachment_type[]
)
RETURNS bigint
AS
$$
    DECLARE _party_id                       bigint;
    DECLARE _transaction_master_id          bigint;
    DECLARE _cash_tran_id                   bigint;
    DECLARE _stock_master_id                bigint;
    DECLARE _stock_detail_id                bigint;
    DECLARE _shipping_address_id            integer;
    DECLARE _grand_total                    money_strict;
    DECLARE _discount_total                 money_strict2;
    DECLARE _tax_total                      money_strict2;
    DECLARE _payable                        money_strict2;
    DECLARE _default_currency_code          national character varying(12);
    DECLARE _is_periodic                    boolean = office.is_periodic_inventory(_office_id);
    DECLARE _tran_counter                   integer;
    DECLARE _transaction_code               text;
    DECLARE _shipping_charge                money_strict2;
    DECLARE _tax                            RECORD;
    DECLARE _cash_repository_id             integer;
    DECLARE _cash_account_id                bigint;
    DECLARE _is_cash                        boolean;
BEGIN
    IF(policy.can_post_transaction(_login_id, _user_id, _office_id, _book_name, _value_date) = false) THEN
        RETURN 0;
    END IF;

    _party_id                               := core.get_party_id_by_party_code(_party_code);
    _default_currency_code                  := transactions.get_default_currency_code_by_office_id(_office_id);
    _cash_account_id                        := core.get_cash_account_id_by_store_id(_store_id);
    _cash_repository_id                     := core.get_cash_repository_id_by_store_id(_store_id);
    _is_cash                                := core.is_cash_account_id(_cash_account_id);

    IF(NOT _is_cash) THEN
        _cash_repository_id                 := NULL;
    END IF;

    DROP TABLE IF EXISTS temp_stock_details CASCADE;
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
        cost_of_goods_sold              money_strict2,
        discount                        money_strict2,
        shipping_charge                 money_strict2,
        tax_form                        text,
        sales_tax_id                    integer,
        tax                             money_strict2,
        purchase_account_id             integer, 
        purchase_discount_account_id    integer, 
        inventory_account_id            integer
    ) ON COMMIT DROP;


    DROP TABLE IF EXISTS temp_stock_tax_details;
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
    

    INSERT INTO temp_stock_details(store_id, item_code, quantity, unit_name, price, discount, shipping_charge, tax_form, tax)
    SELECT store_id, item_code, quantity, unit_name, price, discount, shipping_charge, tax_form, tax
    FROM explode_array(_details);

    UPDATE temp_stock_details 
    SET
        tran_type                       = 'Dr',
        sales_tax_id                    = core.get_sales_tax_id_by_sales_tax_code(tax_form),
        item_id                         = core.get_item_id_by_item_code(item_code),
        unit_id                         = core.get_unit_id_by_unit_name(unit_name),
        base_quantity                   = core.get_base_quantity_by_unit_name(unit_name, quantity),
        base_unit_id                    = core.get_base_unit_id_by_unit_name(unit_name);

    UPDATE temp_stock_details
    SET
        purchase_account_id             = core.get_purchase_account_id(item_id),
        purchase_discount_account_id    = core.get_purchase_discount_account_id(item_id),
        inventory_account_id            = core.get_inventory_account_id(item_id);

    IF EXISTS
    (
            SELECT 1 FROM temp_stock_details AS details
            WHERE core.is_valid_unit_id(details.unit_id, details.item_id) = false
            LIMIT 1
    ) THEN
        RAISE EXCEPTION 'Item/unit mismatch.'
        USING ERRCODE='P3201';
    END IF;

    FOR _tax IN SELECT * FROM temp_stock_details ORDER BY id
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
            _tax.id, 
            sales_tax_detail_code,
            account_id, 
            sales_tax_detail_id, 
            state_sales_tax_id, 
            county_sales_tax_id, 
            taxable_amount, 
            rate, 
            tax
        FROM transactions.get_sales_tax('Sales', _store_id, _party_code, NULL, _price_type_id, _tax.item_code, _tax.price, _tax.quantity, _tax.discount, _tax.shipping_charge, _tax.sales_tax_id);
    END LOOP;

    UPDATE temp_stock_details
    SET tax =
    (SELECT SUM(COALESCE(temp_stock_tax_details.tax, 0)) FROM temp_stock_tax_details
    WHERE temp_stock_tax_details.temp_stock_detail_id = temp_stock_tax_details.id);

    SELECT SUM(COALESCE(tax,0))                                     INTO _tax_total FROM temp_stock_details;
    SELECT SUM(COALESCE(discount, 0))                               INTO _discount_total FROM temp_stock_details;
    SELECT SUM(COALESCE(price, 0) * COALESCE(quantity, 0))          INTO _grand_total FROM temp_stock_details;
    SELECT SUM(COALESCE(shipping_charge, 0))                        INTO _shipping_charge FROM temp_stock_details;

    _payable                                := _grand_total - COALESCE(_discount_total, 0) + COALESCE(_tax_total, 0) + COALESCE(_shipping_charge, 0);

    DROP TABLE IF EXISTS temp_transaction_details;
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


    IF(_is_periodic = true) THEN
        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Dr', purchase_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0)), 1, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0))
        FROM temp_stock_details
        GROUP BY purchase_account_id;
    ELSE
        --Perpetutal Inventory Accounting System
        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Dr', inventory_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0)), 1, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0))
        FROM temp_stock_details
        GROUP BY inventory_account_id;
    END IF;

    IF(_tax_total > 0) THEN
        FOR _tax IN 
        SELECT 
            format('P: %s x R: %s %% = %s (%s)', principal::text, rate::text, tax::text, sales_tax_detail_code) as statement_reference,
            account_id,
            tax
        FROM temp_stock_tax_details ORDER BY id
        LOOP    
            INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
            SELECT 'Dr', _tax.account_id, _tax.statement_reference || _statement_reference, _default_currency_code, _tax.tax, 1, _default_currency_code, _tax.tax;
        END LOOP;
    END IF;


    IF(_discount_total > 0) THEN
        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Cr', purchase_discount_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(discount, 0)), 1, _default_currency_code, SUM(COALESCE(discount, 0))
        FROM temp_stock_details
        GROUP BY purchase_discount_account_id;
    END IF;

    INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
    SELECT 'Cr', core.get_account_id_by_party_id(_party_id), _statement_reference, _default_currency_code, _payable, 1, _default_currency_code, _payable;

    _transaction_master_id  := nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));
    _cash_tran_id           := nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));
    _stock_master_id        := nextval(pg_get_serial_sequence('transactions.stock_master', 'stock_master_id'));
    _tran_counter           := transactions.get_new_transaction_counter(_value_date);
    _transaction_code       := transactions.get_transaction_code(_value_date, _office_id, _user_id, _login_id);

    UPDATE temp_transaction_details     SET transaction_master_id   = _transaction_master_id;
    UPDATE temp_stock_details           SET stock_master_id         = _stock_master_id;
    
    INSERT INTO transactions.transaction_master(transaction_master_id, transaction_counter, transaction_code, book, value_date, user_id, login_id, office_id, cost_center_id, reference_number, statement_reference) 
    SELECT _transaction_master_id, _tran_counter, _transaction_code, _book_name, _value_date, _user_id, _login_id, _office_id, _cost_center_id, _reference_number, _statement_reference;


    INSERT INTO transactions.transaction_details(value_date, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency)
    SELECT _value_date, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency
    FROM temp_transaction_details
    ORDER BY tran_type DESC;


    INSERT INTO transactions.stock_master(value_date, stock_master_id, transaction_master_id, party_id, price_type_id, is_credit, shipper_id, shipping_charge, store_id, cash_repository_id)
    SELECT _value_date, _stock_master_id, _transaction_master_id, _party_id, _price_type_id, _is_credit, _shipper_id, _shipping_charge, _store_id, _cash_repository_id;
            
    FOR _tax IN SELECT * FROM temp_stock_details ORDER BY id
    LOOP
        _stock_detail_id        := nextval(pg_get_serial_sequence('transactions.stock_details', 'stock_detail_id'));

        INSERT INTO transactions.stock_details(stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, sales_tax_id, tax)
        SELECT _stock_detail_id, _value_date, _tax.stock_master_id, _tax.tran_type, _tax.store_id, _tax.item_id, _tax.quantity, _tax.unit_id, _tax.base_quantity, _tax.base_unit_id, _tax.price, COALESCE(_tax.cost_of_goods_sold, 0), _tax.discount, _tax.sales_tax_id, COALESCE(_tax.tax, 0)
        FROM temp_stock_details
        WHERE id = _tax.id;


        INSERT INTO transactions.stock_tax_details(stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax)
        SELECT _stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax
        FROM temp_stock_tax_details
        WHERE temp_stock_detail_id = _tax.id;
        
    END LOOP;


    IF(array_length(_tran_ids, 1) > 0 AND _tran_ids != ARRAY[NULL::bigint]) THEN
        INSERT INTO transactions.stock_master_non_gl_relations(stock_master_id, non_gl_stock_master_id)
        SELECT _stock_master_id, explode_array(_tran_ids);
    END IF;

    IF(array_length(_attachments, 1) > 0 AND _attachments != ARRAY[NULL::core.attachment_type]) THEN
        INSERT INTO core.attachments(user_id, resource, resource_key, resource_id, original_file_name, file_extension, file_path, comment)
        SELECT _user_id, 'transactions.transaction_master', 'transaction_master_id', _transaction_master_id, original_file_name, file_extension, file_path, comment 
        FROM explode_array(_attachments);
    END IF;


    IF(_is_credit = false) THEN
        _tran_counter           := transactions.get_new_transaction_counter(_value_date);
        _transaction_code       := transactions.get_transaction_code(_value_date, _office_id, _user_id, _login_id);

        INSERT INTO transactions.transaction_master(transaction_master_id, transaction_counter, transaction_code, book, value_date, user_id, login_id, office_id, cost_center_id, reference_number, statement_reference, cascading_tran_id) 
        SELECT _cash_tran_id, _tran_counter, _transaction_code, 'Purchase.Payment', _value_date, _user_id, _login_id, _office_id, _cost_center_id, _reference_number, _statement_reference, _transaction_master_id;


        INSERT INTO transactions.transaction_details(value_date, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency)
        SELECT _value_date, _cash_tran_id, 'Cr', _cash_account_id, _statement_reference, _cash_repository_id, _default_currency_code, _payable, _default_currency_code, 1, _payable UNION ALL
        SELECT _value_date, _cash_tran_id, 'Dr', core.get_account_id_by_party_id(_party_id), _statement_reference, NULL, _default_currency_code, _payable, _default_currency_code, 1, _payable;
    END IF;

    
    PERFORM transactions.auto_verify(_transaction_master_id, _office_id);
    PERFORM transactions.settle_party_due(_party_id, _office_id);
    RETURN _transaction_master_id;
END
$$
LANGUAGE plpgsql;


-- 
-- SELECT * FROM transactions.post_purchase('Purchase.Direct', 2, 2, 56, '2/2/2015', 1, '', '', false, 'JASMI-0002', NULL, NULL, 1, ARRAY[]::bigint[], 
--       ARRAY[
--                  ROW(1, 'RMBP', 1, 'Piece',180000, 0, 200, 'MoF-NY-BK-STX', 0)::transactions.stock_detail_type,
--                  ROW(1, '13MBA', 1, 'Dozen',130000, 300, 30, 'MoF-NY-BK-STX', 0)::transactions.stock_detail_type,
--                  ROW(1, '11MBA', 1, 'Piece',110000, 5000, 50, 'MoF-NY-BK-STX', 0)::transactions.stock_detail_type], 
--       ARRAY[NULL::core.attachment_type]);
