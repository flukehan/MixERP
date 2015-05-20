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
    DECLARE _default_currency_code  national character varying(12);
    DECLARE _cost_of_goods_sold     money_strict2;
    DECLARE _sm_id                  bigint;
    DECLARE _is_non_taxable_sales   boolean;
    DECLARE this                    RECORD;
    DECLARE _shipping_address_code  national character varying(12);
BEGIN
    IF(policy.can_post_transaction(_login_id, _user_id, _office_id, 'Sales.Return', _value_date) = false) THEN
        RETURN 0;
    END IF;
    
    _party_id                       := core.get_party_id_by_party_code(_party_code);
    _default_currency_code          := transactions.get_default_currency_code_by_office_id(_office_id);
    
    SELECT cost_center_id   INTO _cost_center_id    FROM transactions.transaction_master WHERE transactions.transaction_master.transaction_master_id = _transaction_master_id;

    SELECT 
        is_credit,
        non_taxable,
        core.get_shipping_address_code_by_shipping_address_id(shipping_address_id),
        stock_master_id
    INTO 
        _is_credit,
        _is_non_taxable_sales,
        _shipping_address_code,
        _sm_id
    FROM transactions.stock_master 
    WHERE transaction_master_id = _transaction_master_id;

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
        sales_return_account_id         integer,
        inventory_account_id            integer,
        cost_of_goods_sold_account_id   integer        
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

    INSERT INTO temp_stock_details(store_id, item_code, quantity, unit_name, price, discount, shipping_charge, tax_form, tax)
    SELECT store_id, item_code, quantity, unit_name, price, discount, shipping_charge, tax_form, tax
    FROM explode_array(_details);

    UPDATE temp_stock_details 
    SET
        tran_type                   = 'Dr',
        sales_tax_id                = core.get_sales_tax_id_by_sales_tax_code(tax_form),
        item_id                     = core.get_item_id_by_item_code(item_code),
        unit_id                     = core.get_unit_id_by_unit_name(unit_name),
        base_quantity               = core.get_base_quantity_by_unit_name(unit_name, quantity),
        base_unit_id                = core.get_base_unit_id_by_unit_name(unit_name);

    UPDATE temp_stock_details
    SET
        sales_account_id                = core.get_sales_account_id(item_id),
        sales_discount_account_id       = core.get_sales_discount_account_id(item_id),
        sales_return_account_id         = core.get_sales_return_account_id(item_id),        
        inventory_account_id            = core.get_inventory_account_id(item_id),
        cost_of_goods_sold_account_id   = core.get_cost_of_goods_sold_account_id(item_id);
    
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

    _tran_master_id             := nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));
    _stock_master_id            := nextval(pg_get_serial_sequence('transactions.stock_master', 'stock_master_id'));
    _tran_counter               := transactions.get_new_transaction_counter(_value_date);
    _tran_code                  := transactions.get_transaction_code(_value_date, _office_id, _user_id, _login_id);

    INSERT INTO transactions.transaction_master(transaction_master_id, transaction_counter, transaction_code, book, value_date, user_id, login_id, office_id, cost_center_id, reference_number, statement_reference)
    SELECT _tran_master_id, _tran_counter, _tran_code, 'Sales.Return', _value_date, _user_id, _login_id, _office_id, _cost_center_id, _reference_number, _statement_reference;
        
    SELECT SUM(COALESCE(tax, 0))                                INTO _tax_total FROM temp_stock_tax_details;
    SELECT SUM(COALESCE(discount, 0))                           INTO _discount_total FROM temp_stock_details;
    SELECT SUM(COALESCE(price, 0) * COALESCE(quantity, 0))      INTO _grand_total FROM temp_stock_details;



    UPDATE temp_stock_details
    SET cost_of_goods_sold = transactions.get_write_off_cost_of_goods_sold(_sm_id, item_id, unit_id, quantity);


    SELECT SUM(cost_of_goods_sold) INTO _cost_of_goods_sold FROM temp_stock_details;


    IF(_cost_of_goods_sold > 0) THEN
        INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT _tran_master_id, _value_date, 'Dr', inventory_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0)), 1, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0))
        FROM temp_stock_details
        GROUP BY inventory_account_id;


        INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT _tran_master_id, _value_date, 'Cr', cost_of_goods_sold_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0)), 1, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0))
        FROM temp_stock_details
        GROUP BY cost_of_goods_sold_account_id;
    END IF;


    INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, local_currency_code, er,amount_in_local_currency) 
    SELECT _tran_master_id, _value_date, 'Dr', sales_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0)), _default_currency_code, 1, SUM(COALESCE(price, 0) * COALESCE(quantity, 0))
    FROM temp_stock_details
    GROUP BY sales_account_id;


    IF(_tax_total IS NOT NULL AND _tax_total > 0) THEN
        INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency)
        SELECT _tran_master_id, _value_date, 'Dr', temp_stock_tax_details.account_id, _statement_reference, _default_currency_code, SUM(COALESCE(tax, 0)), _default_currency_code, 1, SUM(COALESCE(tax, 0))
        FROM temp_stock_tax_details
        GROUP BY temp_stock_tax_details.account_id;
    END IF;

    IF(_discount_total IS NOT NULL AND _discount_total > 0) THEN
        INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency) 
        SELECT _tran_master_id, _value_date, 'Cr', sales_discount_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(discount, 0)), _default_currency_code, 1, SUM(COALESCE(discount, 0))
        FROM temp_stock_details
        GROUP BY sales_discount_account_id;
    END IF;

    IF(_is_credit) THEN
        INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency) 
        SELECT _tran_master_id, _value_date, 'Cr',  core.get_account_id_by_party_code(_party_code), _statement_reference, _default_currency_code, _grand_total + _tax_total - _discount_total, _default_currency_code, 1, _grand_total + _tax_total - _discount_total;
    ELSE
        INSERT INTO transactions.transaction_details(transaction_master_id, value_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency) 
        SELECT _tran_master_id, _value_date, 'Cr',  sales_return_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0)) + SUM(COALESCE(tax, 0)) - SUM(COALESCE(discount, 0)), _default_currency_code, 1, SUM(COALESCE(price, 0) * COALESCE(quantity, 0)) + SUM(COALESCE(tax, 0)) - SUM(COALESCE(discount, 0))
        FROM temp_stock_details
        GROUP BY sales_return_account_id;
    END IF;



    INSERT INTO transactions.stock_master(stock_master_id, value_date, transaction_master_id, party_id, price_type_id, is_credit, store_id) 
    SELECT _stock_master_id, _value_date, _tran_master_id, _party_id, _price_type_id, false, _store_id;


    INSERT INTO transactions.stock_details(value_date, stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, sales_tax_id, tax)
    SELECT _value_date, _stock_master_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, sales_tax_id, tax FROM temp_stock_details;

    INSERT INTO transactions.stock_return(transaction_master_id, return_transaction_master_id)
    SELECT _transaction_master_id, _tran_master_id;

    PERFORM transactions.auto_verify(_transaction_master_id, _office_id);
    RETURN _tran_master_id;
END
$$
LANGUAGE plpgsql;




-- CREATE TEMPORARY TABLE temp_sales_return
-- ON COMMIT DROP
-- AS
-- 
-- SELECT * FROM transactions.post_sales_return(5, 2, 2, 1, '1-1-2000', 1, 'MAJON-0002', 1, '1234-AD', 'Test', 
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
-- WHERE transaction_master_id  = (SELECT * FROM temp_sales_return);


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


