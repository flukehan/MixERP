DROP FUNCTION IF EXISTS transactions.post_direct_sales
(
        _office_id                              integer,
        _user_id                                integer,
        _login_id                               bigint,
        _value_date                             date,
        _cost_center_id                         integer,
        _reference_number                       national character varying(24),
        _statement_reference                    text,
        _cash_repository_id                     integer,
        _is_credit                              boolean,
        _party_code                             national character varying(12),
        _price_type_id                          integer,
        _salesperson_id                        integer,
        _shipper_id                             integer,
        _shipping_address_code                  national character varying(12),
        _shipping_charge                        money_strict2,        
        _store_id                               integer,
        _details                                transactions.stock_detail_type[],
        _attachments                            core.attachment_type[]
);

CREATE FUNCTION transactions.post_direct_sales
(
        _office_id                              integer,
        _user_id                                integer,
        _login_id                               bigint,
        _value_date                             date,
        _cost_center_id                         integer,
        _reference_number                       national character varying(24),
        _statement_reference                    text,
        _cash_repository_id                     integer,
        _is_credit                              boolean,
        _party_code                             national character varying(12),
        _price_type_id                          integer,
        _salesperson_id                        integer,
        _shipper_id                             integer,
        _shipping_address_code                  national character varying(12),
        _shipping_charge                        money_strict2,        
        _store_id                               integer,
        _details                                transactions.stock_detail_type[],
        _attachments                            core.attachment_type[]
)
RETURNS bigint
AS
$$
        DECLARE _party_id                       bigint;
        DECLARE _transaction_master_id          bigint;
        DECLARE _stock_master_id                bigint;
        DECLARE _grand_total                    money_strict;
        DECLARE _discount_total                 money_strict2;
        DECLARE _tax_total                      money_strict2;
BEGIN
        
        _party_id                               := core.get_party_id_by_party_code(_party_code);


        CREATE TEMPORARY TABLE temp_transaction_details
        (
                transaction_master_id           BIGINT, 
                tran_type                       transaction_type, 
                account_id                      integer, 
                statement_reference             text, 
                cash_repository_id              integer, 
                currency_code                   national character varying(12), 
                amount_in_currency              money_strict, 
                local_currency_code             national character varying(12), 
                er                              decimal_strict, 
                amount_in_local_currency        money_strict
        ) ON COMMIT DROP;

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
                discount                        money_strict2,
                tax_rate                        decimal_strict2,
                tax                             money_strict2
        ) ON COMMIT DROP;
        

        
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
                'Sales.Direct', 
                _value_date, 
                _user_id, 
                _login_id, 
                _office_id, 
                _cost_center_id, 
                _reference_number, 
                _statement_reference;

        _transaction_master_id := currval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));

        UPDATE temp_transaction_details SET transaction_master_id = _transaction_master_id;

        INSERT INTO transactions.transaction_details
        (
                transaction_master_id, 
                tran_type, 
                account_id, 
                statement_reference, 
                cash_repository_id, 
                currency_code, 
                amount_in_currency, 
                local_currency_code, 
                er, 
                amount_in_local_currency
        )
        SELECT 
                transaction_master_id, 
                tran_type, 
                account_id, 
                statement_reference, 
                cash_repository_id, 
                currency_code, 
                amount_in_currency, 
                local_currency_code, 
                er, 
                amount_in_local_currency
        FROM temp_transaction_details;


        INSERT INTO transactions.stock_master
        (
                stock_master_id, 
                transaction_master_id, 
                party_id, 
                salesperson_id, 
                price_type_id, 
                is_credit, 
                shipper_id, 
                shipping_address_id, 
                shipping_charge, 
                store_id, 
                cash_repository_id
        )
        SELECT
                nextval(pg_get_serial_sequence('transactions.stock_master', 'stock_master_id')),
                _transaction_master_id,
                _party_id,
                _salesperson_id,
                _price_type_id,
                _is_credit,
                _shipper_id,
                core.get_shipping_address_id_by_shipping_address_code(_shipping_address_code, _party_id),
                _shipping_charge,
                _store_id,
                _cash_repository_id;
                
        _stock_master_id := currval(pg_get_serial_sequence('transactions.stock_master', 'stock_master_id'));

        UPDATE temp_stock_details SET stock_master_id = _stock_master_id;

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
        FROM temp_stock_details;


        IF(_attachments != ARRAY[NULL::core.attachment_type]) THEN        
                INSERT INTO core.attachments
                (
                        user_id, 
                        resource, 
                        resource_key, 
                        resource_id, 
                        original_file_name, 
                        file_extension, 
                        file_path, 
                        comment
                )
                SELECT 
                        _user_id, 
                        'transactions.transaction_master', 
                        'transaction_master_id', 
                        _transaction_master_id, 
                        original_file_name, 
                        file_extension, 
                        file_path, 
                        comment
                FROM explode_array(_attachments);
        END IF;
        
END
$$
LANGUAGE plpgsql;

-- 
-- 
-- SELECT * FROM transactions.post_direct_sales(1, 1, 1, 1, '1-1-2000', 1, 'JASMI-0001', 1, '1234-AD', 'Test', 
-- ARRAY[
-- ROW(1, 'ITP', 1, 'Piece', 1000, 0, 13, 130)::transactions.stock_detail_type,
-- ROW(1, 'ITP', 1, 'Piece', 1000, 0, 13, 130)::transactions.stock_detail_type
-- ],
-- ARRAY[
-- NULL::core.attachment_type
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


