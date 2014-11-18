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
    _tran_ids                               bigint[],
    _details                                transactions.stock_detail_type[],
    _attachments                            core.attachment_type[]

);

CREATE FUNCTION transactions.post_non_gl_transaction
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
    _tran_ids                               bigint[],
    _details                                transactions.stock_detail_type[],
    _attachments                            core.attachment_type[]

)
RETURNS bigint
AS
$$
    DECLARE _party_id                       bigint;
    DECLARE _non_glstock_master_id          bigint;
BEGIN
    _party_id                               := core.get_party_id_by_party_code(_party_code);

    CREATE TEMPORARY TABLE temp_stock_details
    (
        non_gl_stock_master_id          bigint, 
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
        tran_type                   = 'Cr',
        item_id                     = core.get_item_id_by_item_code(item_code),
        unit_id                     = core.get_unit_id_by_unit_name(unit_name),
        base_quantity               = core.get_base_quantity_by_unit_name(unit_name, quantity),
        base_unit_id                = core.get_base_unit_id_by_unit_name(unit_name);
            

    _non_glstock_master_id          := nextval(pg_get_serial_sequence('transactions.non_gl_stock_master', 'non_gl_stock_master_id'));

    UPDATE temp_stock_details SET non_gl_stock_master_id = _non_glstock_master_id;
    
    INSERT INTO transactions.non_gl_stock_master(non_gl_stock_master_id, value_date, book, party_id, price_type_id, login_id, user_id, office_id, reference_number, statement_reference) 
    SELECT _non_glstock_master_id, _value_date, _book_name, _party_id, _price_type_id, _login_id, _user_id, _office_id, _reference_number, _statement_reference;


    INSERT INTO transactions.non_gl_stock_details(non_gl_stock_master_id, value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax)    
    SELECT non_gl_stock_master_id, _value_date, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax FROM temp_stock_details;


    IF(_tran_ids != NULL::bigint[]) THEN
        INSERT INTO transactions.non_gl_stock_master_relations(order_non_gl_stock_master_id, quotation_non_gl_stock_master_id)
        SELECT _non_glstock_master_id, explode_array(_tran_ids);

    END IF;


    IF(_attachments != ARRAY[NULL::core.attachment_type]) THEN
        INSERT INTO core.attachments(user_id, resource, resource_key, resource_id, original_file_name, file_extension, file_path, comment)
        SELECT _user_id, 'transactions.non_gl_stock_master', 'non_gl_stock_master_id', _non_glstock_master_id, original_file_name, file_extension, file_path, comment 
        FROM explode_array(_attachments);
    END IF;

    
    RETURN _non_glstock_master_id;
END;
$$
LANGUAGE plpgsql;