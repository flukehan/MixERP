DROP FUNCTION IF EXISTS transactions.post_non_gl_transaction
(
    _book_name                              national character varying(12),
    _office_id                              integer,
    _user_id                                integer,
    _login_id                               bigint,
    _value_date                             date,
    _reference_number                       national character varying(24),
    _statement_reference                    text,
    _party_code                             national character varying(12),
    _price_type_id                          integer,
    _is_non_taxable_sales                   boolean,
    _salesperson_id                         integer,
    _shipper_id                             integer,
    _shipping_address_code                  national character varying(12),
    _store_id                               integer,
    _tran_ids                               bigint[],
    _details                                transactions.stock_detail_type[],
    _attachments                            core.attachment_type[]

);


CREATE FUNCTION transactions.post_non_gl_transaction
(
    _book_name                              national character varying(48),
    _office_id                              integer,
    _user_id                                integer,
    _login_id                               bigint,
    _value_date                             date,
    _reference_number                       national character varying(24),
    _statement_reference                    text,
    _party_code                             national character varying(12),
    _price_type_id                          integer,
    _is_non_taxable_sales                   boolean,
    _salesperson_id                         integer,
    _shipper_id                             integer,
    _shipping_address_code                  national character varying(12),
    _store_id                               integer,
    _tran_ids                               bigint[],
    _details                                transactions.stock_detail_type[],
    _attachments                            core.attachment_type[]

)
RETURNS bigint
AS
$$
    DECLARE _party_id                       bigint;
    DECLARE _non_gl_stock_master_id         bigint;
    DECLARE _non_gl_stock_detail_id         bigint;
    DECLARE _shipping_address_id            bigint;
    DECLARE _shipping_charge                public.money_strict2;
    DECLARE _tran_type                      transaction_type;
    DECLARE this                            RECORD;
BEGIN
    IF(policy.can_post_transaction(_login_id, _user_id, _office_id, _book_name, _value_date) = false) THEN
        RETURN 0;
    END IF;

    _party_id                               := core.get_party_id_by_party_code(_party_code);
    _shipping_address_id                    := core.get_shipping_address_id_by_shipping_address_code(_shipping_address_code, _party_id);

    DROP TABLE IF EXISTS temp_stock_details CASCADE;

    CREATE TEMPORARY TABLE temp_stock_details
    (
        id                              SERIAL PRIMARY KEY,
        non_gl_stock_master_id          bigint, 
        tran_type                       transaction_type, 
        store_id                        integer,
        item_code                       text,
        item_id                         integer, 
        quantity                        public.integer_strict,
        unit_name                       text,
        unit_id                         integer,
        base_quantity                   decimal,
        base_unit_id                    integer,                
        price                           public.money_strict,
        cost_of_goods_sold              public.money_strict2 DEFAULT(0),
        discount                        public.money_strict2,
        shipping_charge                 public.money_strict2,
        tax_form                        text,
        sales_tax_id                    integer,
        tax                             public.money_strict2
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
        principal                               public.money_strict,
        rate                                    public.decimal_strict,
        tax                                     public.money_strict
    ) ON COMMIT DROP;

    INSERT INTO temp_stock_details(store_id, item_code, quantity, unit_name, price, discount, shipping_charge, tax_form, tax)
    SELECT store_id, item_code, quantity, unit_name, price, discount, shipping_charge, tax_form, tax
    FROM explode_array(_details);

    UPDATE temp_stock_details 
    SET
        tran_type                   = _tran_type,
        sales_tax_id                = core.get_sales_tax_id_by_sales_tax_code(tax_form),
        item_id                     = core.get_item_id_by_item_code(item_code),
        unit_id                     = core.get_unit_id_by_unit_name(unit_name),
        base_quantity               = core.get_base_quantity_by_unit_name(unit_name, quantity),
        base_unit_id                = core.get_base_unit_id_by_unit_name(unit_name);

    IF EXISTS
    (
            SELECT 1 FROM temp_stock_details AS details
            WHERE core.is_valid_unit_id(details.unit_id, details.item_id) = false
            LIMIT 1
    ) THEN
        RAISE EXCEPTION 'Item/unit mismatch.'
        USING ERRCODE='P3201';
    END IF;

    SELECT SUM(COALESCE(shipping_charge, 0))                    INTO _shipping_charge FROM temp_stock_details;

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

    _non_gl_stock_master_id          := nextval(pg_get_serial_sequence('transactions.non_gl_stock_master', 'non_gl_stock_master_id'));

    UPDATE temp_stock_details SET non_gl_stock_master_id = _non_gl_stock_master_id;
    
    INSERT INTO transactions.non_gl_stock_master(non_gl_stock_master_id, value_date, book, party_id, price_type_id, login_id, user_id, office_id, reference_number, statement_reference, non_taxable, salesperson_id, shipper_id, shipping_address_id, shipping_charge, store_id) 
    SELECT _non_gl_stock_master_id, _value_date, _book_name, _party_id, _price_type_id, _login_id, _user_id, _office_id, _reference_number, _statement_reference, _is_non_taxable_sales, _salesperson_id, _shipper_id, _shipping_address_id, _shipping_charge, _store_id;


    FOR this IN SELECT * FROM temp_stock_details ORDER BY id
    LOOP
        _non_gl_stock_detail_id        := nextval(pg_get_serial_sequence('transactions.non_gl_stock_details', 'non_gl_stock_detail_id'));

        INSERT INTO transactions.non_gl_stock_details(non_gl_stock_detail_id, non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, tax)    
        SELECT _non_gl_stock_detail_id, non_gl_stock_master_id, _value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, shipping_charge, sales_tax_id, COALESCE(this.tax, 0) 
        FROM temp_stock_details
        WHERE id = this.id;


        INSERT INTO transactions.non_gl_stock_tax_details(non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax)
        SELECT _non_gl_stock_detail_id, sales_tax_detail_id, state_sales_tax_id, county_sales_tax_id, principal, rate, tax
        FROM temp_stock_tax_details
        WHERE temp_stock_detail_id = this.id;
        
    END LOOP;

    IF(array_length(_tran_ids, 1) > 0 AND _tran_ids != ARRAY[NULL::bigint]) THEN
        INSERT INTO transactions.non_gl_stock_master_relations(order_non_gl_stock_master_id, quotation_non_gl_stock_master_id)
        SELECT _non_gl_stock_master_id, explode_array(_tran_ids);
    END IF;

    IF(array_length(_attachments, 1) > 0 AND _attachments != ARRAY[NULL::core.attachment_type]) THEN
        INSERT INTO core.attachments(user_id, resource, resource_key, resource_id, original_file_name, file_extension, file_path, comment)
        SELECT _user_id, 'transactions.non_gl_stock_master', 'non_gl_stock_master_id', _non_gl_stock_master_id, original_file_name, file_extension, file_path, comment 
        FROM explode_array(_attachments);
    END IF;

    
    RETURN _non_gl_stock_master_id;
END;
$$
LANGUAGE plpgsql;

-- SELECT * FROM transactions.post_non_gl_transaction('Sales.Order', 2, 2, 5, '1-1-2020', '1', 'asdf', 'JASMI-0002', 1, false, 1, 1, '',  1, null::bigint[],
-- ARRAY[
--            ROW(1, 'RMBP', 1, 'Piece',180000, 0, 200, 'MoF-NY-BK-STX', 0)::transactions.stock_detail_type,
--            ROW(1, '13MBA', 1, 'Dozen',130000, 300, 30, 'MoF-NY-BK-STX', 0)::transactions.stock_detail_type,
--            ROW(1, '11MBA', 1, 'Piece',110000, 5000, 50, 'MoF-NY-BK-STX', 0)::transactions.stock_detail_type], 
-- ARRAY[NULL::core.attachment_type]);
