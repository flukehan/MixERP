DROP FUNCTION IF EXISTS transactions.post_sales
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
    _payment_term_id                        integer,
    _party_code                             national character varying(12),
    _price_type_id                          integer,
    _salesperson_id                         integer,
    _shipper_id                             integer,
    _shipping_address_code                  national character varying(12),
    _store_id                               integer,
    _is_non_taxable_sales                   boolean,
    _details                                transactions.stock_detail_type[],
    _attachments                            core.attachment_type[]
);

CREATE FUNCTION transactions.post_sales
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
    _payment_term_id                        integer,
    _party_code                             national character varying(12),
    _price_type_id                          integer,
    _salesperson_id                         integer,
    _shipper_id                             integer,
    _shipping_address_code                  national character varying(12),
    _store_id                               integer,
    _is_non_taxable_sales                   boolean,
    _details                                transactions.stock_detail_type[],
    _attachments                            core.attachment_type[]
)
RETURNS bigint
AS
$$
    DECLARE _party_id                       bigint;
    DECLARE _transaction_master_id          bigint;
    DECLARE _stock_master_id                bigint;
    DECLARE _stock_detail_id                bigint;
    DECLARE _shipping_address_id            integer;
    DECLARE _grand_total                    money_strict;
    DECLARE _discount_total                 money_strict2;
    DECLARE _tax_total                      money_strict2;
    DECLARE _receivable                     money_strict2;
    DECLARE _default_currency_code          national character varying(12);
    DECLARE _is_periodic                    boolean = office.is_periodic_inventory(_office_id);
    DECLARE _cost_of_goods                  money_strict;
    DECLARE _tran_counter                   integer;
    DECLARE _transaction_code               text;
    DECLARE _shipping_charge                money_strict2;
    DECLARE this                            RECORD;
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
        cost_of_goods_sold              money_strict2 DEFAULT(0),
        discount                        money_strict2,
        shipping_charge                 money_strict2,
        tax_form                        text,
        sales_tax_id                    integer,
        tax                             money_strict2,
        sales_account_id                integer,
        sales_discount_account_id       integer,
        inventory_account_id            integer,
        cost_of_goods_sold_account_id   integer
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
        tran_type                       = 'Cr',
        sales_tax_id                    = core.get_sales_tax_id_by_sales_tax_code(tax_form),
        item_id                         = core.get_item_id_by_item_code(item_code),
        unit_id                         = core.get_unit_id_by_unit_name(unit_name),
        base_quantity                   = core.get_base_quantity_by_unit_name(unit_name, quantity),
        base_unit_id                    = core.get_base_unit_id_by_unit_name(unit_name);

    UPDATE temp_stock_details
    SET
        sales_account_id                = core.get_sales_account_id(item_id),
        sales_discount_account_id       = core.get_sales_discount_account_id(item_id),
        inventory_account_id            = core.get_inventory_account_id(item_id),
        cost_of_goods_sold_account_id   = core.get_cost_of_goods_sold_account_id(item_id);
            
    IF EXISTS
    (
            SELECT 1 FROM temp_stock_details AS details
            WHERE core.is_valid_unit_id(details.unit_id, details.item_id) = false
            LIMIT 1
    ) THEN
        RAISE EXCEPTION 'Item/unit mismatch.'
        USING ERRCODE='P3201';
    END IF;

    IF(_is_non_taxable_sales) THEN
        IF EXISTS(SELECT * FROM temp_stock_details WHERE sales_tax_id IS NOT NULL LIMIT 1) THEN
            RAISE EXCEPTION 'You cannot provide sales tax information for non taxable sales.'
            USING ERRCODE='P5110';
        END IF;
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
        FROM transactions.get_sales_tax('Sales', _store_id, _party_code, _shipping_address_code, _price_type_id, this.item_code, this.price, this.quantity, this.discount, this.shipping_charge, this.sales_tax_id);
    END LOOP;

    UPDATE temp_stock_details
    SET tax =
    (SELECT SUM(COALESCE(temp_stock_tax_details.tax, 0)) FROM temp_stock_tax_details
    WHERE temp_stock_tax_details.temp_stock_detail_id = temp_stock_details.id);


    SELECT SUM(COALESCE(tax, 0))                                INTO _tax_total FROM temp_stock_tax_details;
    SELECT SUM(COALESCE(discount, 0))                           INTO _discount_total FROM temp_stock_details;
    SELECT SUM(COALESCE(price, 0) * COALESCE(quantity, 0))      INTO _grand_total FROM temp_stock_details;
    SELECT SUM(COALESCE(shipping_charge, 0))                    INTO _shipping_charge FROM temp_stock_details;
    
     _receivable                    := _grand_total - COALESCE(_discount_total, 0) + COALESCE(_tax_total, 0) + COALESCE(_shipping_charge, 0);
    
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


    INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
    SELECT 'Cr', sales_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0)), 1, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0))
    FROM temp_stock_details
    GROUP BY sales_account_id;

    IF(_is_periodic = false) THEN
        --Perpetutal Inventory Accounting System

        UPDATE temp_stock_details SET cost_of_goods_sold = transactions.get_cost_of_goods_sold(item_id, unit_id, store_id, quantity);
        
        SELECT SUM(cost_of_goods_sold) INTO _cost_of_goods
        FROM temp_stock_details;

        IF(_cost_of_goods > 0) THEN
            INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
            SELECT 'Dr', cost_of_goods_sold_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0)), 1, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0))
            FROM temp_stock_details
            GROUP BY cost_of_goods_sold_account_id;

            INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
            SELECT 'Cr', inventory_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0)), 1, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0))
            FROM temp_stock_details
            GROUP BY inventory_account_id;
        END IF;
    END IF;

    IF(_tax_total > 0) THEN
        FOR this IN 
        SELECT 
            format('P: %s x R: %s %% = %s (%s)/', SUM(principal)::text, rate::text, SUM(tax)::text, sales_tax_detail_code) as statement_reference,
            account_id,
            SUM(tax) AS tax 
        FROM temp_stock_tax_details
        GROUP BY account_id, rate, sales_tax_detail_code
        LOOP
            INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
            SELECT 'Cr', this.account_id, this.statement_reference || _statement_reference, _default_currency_code, this.tax, 1, _default_currency_code, this.tax;
        END LOOP;    
    END IF;

    IF(COALESCE(_shipping_charge, 0) > 0) THEN
        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Cr', core.get_account_id_by_shipper_id(_shipper_id), _statement_reference, _default_currency_code, _shipping_charge, 1, _default_currency_code, _shipping_charge;                
    END IF;


    IF(_discount_total > 0) THEN
        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Dr', sales_discount_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(discount, 0)), 1, _default_currency_code, SUM(COALESCE(discount, 0))
        FROM temp_stock_details
        GROUP BY sales_discount_account_id;
    END IF;

    IF(_is_credit = true) THEN
        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Dr', core.get_account_id_by_party_id(_party_id), _statement_reference, _default_currency_code, _receivable, 1, _default_currency_code, _receivable;
    ELSE
        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Dr', _cash_account_id, _statement_reference, _cash_repository_id, _default_currency_code, _receivable, 1, _default_currency_code, _receivable;
    END IF;

    _transaction_master_id  := nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));
    _stock_master_id        := nextval(pg_get_serial_sequence('transactions.stock_master', 'stock_master_id'));    
    _tran_counter           := transactions.get_new_transaction_counter(_value_date);
    _transaction_code       := transactions.get_transaction_code(_value_date, _office_id, _user_id, _login_id);
    _shipping_address_id    := core.get_shipping_address_id_by_shipping_address_code(_shipping_address_code, _party_id);

    UPDATE temp_transaction_details     SET transaction_master_id   = _transaction_master_id;
    UPDATE temp_stock_details           SET stock_master_id         = _stock_master_id;
    
    INSERT INTO transactions.transaction_master(transaction_master_id, transaction_counter, transaction_code, book, value_date, user_id, login_id, office_id, cost_center_id, reference_number, statement_reference) 
    SELECT _transaction_master_id, _tran_counter, _transaction_code, _book_name, _value_date, _user_id, _login_id, _office_id, _cost_center_id, _reference_number, _statement_reference;


    INSERT INTO transactions.transaction_details(value_date, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency)
    SELECT _value_date, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency
    FROM temp_transaction_details
    ORDER BY tran_type DESC;


    INSERT INTO transactions.stock_master(value_date, stock_master_id, transaction_master_id, party_id, salesperson_id, price_type_id, is_credit, payment_term_id, shipper_id, shipping_address_id, shipping_charge, store_id, cash_repository_id, non_taxable)
    SELECT _value_date, _stock_master_id, _transaction_master_id, _party_id, _salesperson_id, _price_type_id, _is_credit, _payment_term_id, _shipper_id, _shipping_address_id, _shipping_charge, _store_id, _cash_repository_id, _is_non_taxable_sales;
            

    FOR this IN SELECT * FROM temp_stock_details ORDER BY id
    LOOP
        _stock_detail_id        := nextval(pg_get_serial_sequence('transactions.stock_details', 'stock_detail_id'));

        INSERT INTO transactions.stock_details(stock_detail_id, value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, shipping_charge, sales_tax_id, tax)
        SELECT _stock_detail_id, _value_date, this.stock_master_id, this.tran_type, this.store_id, this.item_id, this.quantity, this.unit_id, this.base_quantity, this.base_unit_id, this.price, COALESCE(this.cost_of_goods_sold, 0), this.discount, this.shipping_charge, this.sales_tax_id, COALESCE(this.tax, 0) 
        FROM temp_stock_details
        WHERE id = this.id;


        INSERT INTO transactions.stock_tax_details(stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax)
        SELECT _stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax
        FROM temp_stock_tax_details
        WHERE temp_stock_detail_id = this.id;
        
    END LOOP;



    IF(array_length(_attachments, 1) > 0 AND _attachments != ARRAY[NULL::core.attachment_type]) THEN
        INSERT INTO core.attachments(user_id, resource, resource_key, resource_id, original_file_name, file_extension, file_path, comment)
        SELECT _user_id, 'transactions.transaction_master', 'transaction_master_id', _transaction_master_id, original_file_name, file_extension, file_path, comment 
        FROM explode_array(_attachments);
    END IF;
    
    PERFORM transactions.auto_verify(_transaction_master_id, _office_id);
    RETURN _transaction_master_id;
END
$$
LANGUAGE plpgsql;



--       SELECT * FROM transactions.post_sales('Sales.Direct', 2, 2, 5, '1-1-2020', 1, 'asdf', 'Test', false, NULL, 'JASMI-0002', 1, 1, 1, NULL, 1, false,
--       ARRAY[
--                  ROW(1, 'RMBP', 1, 'Piece',180000, 0, 200, 'MoF-NY-BK-STX', 0)::transactions.stock_detail_type,
--                  ROW(1, '13MBA', 1, 'Dozen',130000, 300, 30, 'MoF-NY-BK-STX', 0)::transactions.stock_detail_type,
--                  ROW(1, '11MBA', 1, 'Piece',110000, 5000, 50, 'MoF-NY-BK-STX', 0)::transactions.stock_detail_type], 
--       ARRAY[NULL::core.attachment_type]);



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


