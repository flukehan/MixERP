DROP FUNCTION IF EXISTS transactions.post_sales_return
(
        _transaction_master_id                  bigint,
        _office_id                              integer,
        _user_id                                integer,
        _login_id                               bigint,
        _value_date                             date,
        _store_id                               integer,
        _party_code                             national character varying(12),
        _price_type_id                          integer,
        _reference_number                       national character varying(24),
        _statement_reference                    text,
        _details                                transactions.stock_detail_type[],
        _attachments                            attachment_type[]
);

CREATE FUNCTION transactions.post_sales_return
(
        _transaction_master_id                  bigint,
        _office_id                              integer,
        _user_id                                integer,
        _login_id                               bigint,
        _value_date                             date,
        _store_id                               integer,
        _party_code                             national character varying(12),
        _price_type_id                          integer,
        _reference_number                       national character varying(24),
        _statement_reference                    text,
        _details                                transactions.stock_detail_type[],
        _attachments                            attachment_type[]
)
RETURNS bigint
AS
$$
        DECLARE _party_id                       bigint;
        DECLARE _cost_center_id                 bigint;
        DECLARE _tran_master_id                 bigint;
        DECLARE _stock_master_id                bigint;
        DECLARE _grand_total                    money_strict;
        DECLARE _discount_total                 money_strict2;
        DECLARE _tax_total                      money_strict2;
        DECLARE _is_credit                      boolean;
        DECLARE _credit_account_id              bigint;
BEGIN
        
        _party_id                               := core.get_party_id_by_party_code(_party_code);
        
        SELECT 
                cost_center_id 
        INTO 
                _cost_center_id
        FROM 
                transactions.transaction_master
        WHERE 
                transactions.transaction_master.transaction_master_id = _transaction_master_id;


        SELECT
               is_credit
        INTO
                _is_credit
        FROM transactions.stock_master
        WHERE transaction_master_id = _transaction_master_id;

        IF(_is_credit) THEN
                _credit_account_id = core.get_account_id_by_party_code(_party_code); 
        ELSE
                _credit_account_id = core.get_account_id_by_parameter('Sales.Return');         
        END IF;



        INSERT INTO transactions.transaction_master
        (
                transaction_master_id, 
                transaction_counter, 
                transaction_code, 
                book, 
                value_date, 
                user_id, 
                login_id, 
                office_id, 
                cost_center_id, 
                reference_number, 
                statement_reference
        )
        SELECT 
                nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id')), 
                transactions.get_new_transaction_counter(_value_date), 
                transactions.get_transaction_code(_value_date, _office_id, _user_id, _login_id),
                'Sales.Return',
                _value_date,
                _user_id,
                _login_id,
                _office_id,
                _cost_center_id,
                _reference_number,
                _statement_reference;
                


        _tran_master_id                         := currval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));


        SELECT
                SUM(price * quantity),
                SUM(discount),
                SUM(tax)
        INTO
                _grand_total,
                _discount_total,
                _tax_total                
        FROM 
        explode_array(_details);



        INSERT INTO transactions.transaction_details
        (
                transaction_master_id, 
                tran_type, 
                account_id, 
                statement_reference, 
                currency_code, 
                amount_in_currency, 
                local_currency_code, 
                er, 
                amount_in_local_currency
        ) 
        SELECT
                _tran_master_id,
                'Dr',
                core.get_account_id_by_parameter('Sales'),
                _statement_reference,
                transactions.get_default_currency_code_by_office_id(_office_id),
                _grand_total,
                transactions.get_default_currency_code_by_office_id(_office_id),
                1,
                _grand_total;

        IF(_tax_total IS NOT NULL AND _tax_total > 0) THEN
                INSERT INTO transactions.transaction_details
                (
                        transaction_master_id, 
                        tran_type, 
                        account_id, 
                        statement_reference, 
                        currency_code, 
                        amount_in_currency, 
                        local_currency_code, 
                        er, 
                        amount_in_local_currency
                ) 
                SELECT
                        _tran_master_id,
                        'Dr',
                        core.get_account_id_by_parameter('Sales.Tax'),
                        _statement_reference,
                        transactions.get_default_currency_code_by_office_id(_office_id),
                        _tax_total,
                        transactions.get_default_currency_code_by_office_id(_office_id),
                        1,
                        _tax_total;
        END IF;

        IF(_discount_total IS NOT NULL AND _discount_total > 0) THEN
                INSERT INTO transactions.transaction_details
                (
                        transaction_master_id, 
                        tran_type, 
                        account_id, 
                        statement_reference, 
                        currency_code, 
                        amount_in_currency, 
                        local_currency_code, 
                        er, 
                        amount_in_local_currency
                ) 
                SELECT
                        _tran_master_id,
                        'Cr',
                        core.get_account_id_by_parameter('Sales.Discount'),
                        _statement_reference,
                        transactions.get_default_currency_code_by_office_id(_office_id),
                        _discount_total,
                        transactions.get_default_currency_code_by_office_id(_office_id),
                        1,
                        _discount_total;
        END IF;

        INSERT INTO transactions.transaction_details
        (
                transaction_master_id, 
                tran_type, 
                account_id, 
                statement_reference, 
                currency_code, 
                amount_in_currency, 
                local_currency_code, 
                er, 
                amount_in_local_currency
        ) 
        SELECT
                _tran_master_id,
                'Cr',
                _credit_account_id,
                _statement_reference,
                transactions.get_default_currency_code_by_office_id(_office_id),
                _grand_total + _tax_total - _discount_total,
                transactions.get_default_currency_code_by_office_id(_office_id),
                1,
                _grand_total + _tax_total - _discount_total;


        INSERT INTO transactions.stock_master
        (
                stock_master_id, 
                transaction_master_id, 
                party_id, 
                price_type_id, 
                is_credit, 
                store_id
        ) 
        SELECT 
                nextval(pg_get_serial_sequence('transactions.stock_master', 'stock_master_id')), 
                _tran_master_id,
                _party_id,
                _price_type_id,
                false,
                _store_id;
       

        _stock_master_id                        := currval(pg_get_serial_sequence('transactions.stock_master', 'stock_master_id'));


        INSERT INTO transactions.stock_details
        (
                stock_master_id, 
                tran_type, 
                store_id, 
                item_id, 
                quantity, 
                unit_id, 
                base_quantity, 
                base_unit_id, 
                price, 
                discount, 
                tax_rate, 
                tax
        )
        SELECT 
                _stock_master_id, 
                'Dr', 
                _store_id, 
                core.get_item_id_by_item_code(item_code), 
                quantity, 
                core.get_unit_id_by_unit_name(unit_name), 
                core.get_base_quantity_by_unit_name(unit_name, quantity), 
                core.get_base_unit_id_by_unit_name(unit_name),
                price,
                discount,
                tax_rate,
                tax
        FROM 
        explode_array(_details);

        INSERT INTO transactions.stock_return(transaction_master_id, return_transaction_master_id)
        SELECT _transaction_master_id, _tran_master_id;

        RETURN _tran_master_id;
END
$$
LANGUAGE plpgsql;

-- 
-- 
-- SELECT * FROM transactions.post_sales_return(1, 1, 1, 1, '1-1-2000', 1, 'JASMI-0001', 1, '1234-AD', 'Test', 
-- ARRAY[
-- ROW(1, 'ITP', 1, 'Piece', 1000, 0, 13, 130)::transactions.stock_detail_type,
-- ROW(1, 'ITP', 1, 'Piece', 1000, 0, 13, 130)::transactions.stock_detail_type
-- ],
-- ARRAY[
-- NULL::attachment_type
-- ]);


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


