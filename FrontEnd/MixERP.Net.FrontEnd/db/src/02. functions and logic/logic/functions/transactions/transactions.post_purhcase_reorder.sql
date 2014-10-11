DROP FUNCTION IF EXISTS transactions.post_purhcase_reorder
(
        _value_date                             date,
        _login_id                               bigint,
        _user_id                                integer,
        _office_id                              integer,
        _details                                transactions.purchase_reorder_type[]
);

CREATE FUNCTION transactions.post_purhcase_reorder
(
        _value_date                             date,
        _login_id                               bigint,
        _user_id                                integer,
        _office_id                              integer,
        _details                                transactions.purchase_reorder_type[]
)
RETURNS bool
AS
$$
BEGIN
        IF(audit.is_valid_login_id(_login_id) = false) THEN
                RAISE EXCEPTION 'Invalid LoginId.';
        END IF; 

        IF(office.is_valid_office_id(_office_id) = false) THEN
                RAISE EXCEPTION 'Invalid OfficeId.';
        END IF; 

        IF(policy.can_post_transaction(_user_id, _office_id, 'Purchase.Order') = false) THEN
                RAISE EXCEPTION 'Access is denied. You are not authorized to post this transaction.';
        END IF; 

        
        IF EXISTS
        (
                SELECT 1 FROM explode_array(_details) AS details
                WHERE core.is_valid_item_id(details.item_id) = false
                LIMIT 1
        ) THEN
                RAISE EXCEPTION 'Invalid item.';
        END IF;

        IF EXISTS
        (
                SELECT 1 FROM explode_array(_details) AS details
                WHERE core.is_valid_unit_id(details.unit_id) = false
                LIMIT 1
        ) THEN
                RAISE EXCEPTION 'Invalid unit.';
        END IF;
        
        IF EXISTS
        (
                SELECT 1 FROM explode_array(_details) AS details
                WHERE core.is_valid_unit_id(details.unit_id, details.item_id) = false
                LIMIT 1
        ) THEN
                RAISE EXCEPTION 'Item/unit mismatch.';
        END IF;

        

        

        CREATE TEMPORARY TABLE _temp_transaction(supplier_code national character varying(12), supplier_id bigint UNIQUE, non_gl_stock_master_id bigint UNIQUE)  ON COMMIT DROP;

        INSERT INTO _temp_transaction(supplier_code)
        SELECT DISTINCT supplier_code FROM explode_array(_details);

        UPDATE _temp_transaction
        SET supplier_id = core.get_party_id_by_party_code(supplier_code); 
        
        WITH returned AS
        (
                INSERT INTO transactions.non_gl_stock_master(value_date, book, party_id, login_id, user_id, office_id, statement_reference)
                SELECT _value_date, 'Purchase.Order', supplier_id, _login_id, _user_id, _office_id, 'Automatically generated order.'
                FROM _temp_transaction
                RETURNING party_id, non_gl_stock_master_id 
        )

        UPDATE _temp_transaction
        SET 
                non_gl_stock_master_id = returned.non_gl_stock_master_id
        FROM returned
        WHERE returned.party_id = _temp_transaction.supplier_id;


        INSERT INTO transactions.non_gl_stock_details(non_gl_stock_master_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, discount, tax_rate, tax, audit_user_id)
        SELECT 
                _temp_transaction.non_gl_stock_master_id, 
                details.item_id, 
                details.order_quantity, 
                details.unit_id, 
                core.get_base_quantity_by_unit_id(details.unit_id, details.order_quantity),
                core.get_root_unit_id(details.unit_id), 
                details.price, 
                0 AS discount,
                details.tax_rate,
                details.price * details.order_quantity * details.tax_rate/100,
                _user_id
        FROM explode_array(_details) as details
        INNER JOIN _temp_transaction ON
        _temp_transaction.supplier_code = details.supplier_code;
        
        RETURN FALSE;
END
$$
LANGUAGE plpgsql;


-- SELECT * FROM transactions.post_purhcase_reorder('1-1-2000', 1, 2, 2,
-- ARRAY[
-- ROW(1, 'ETBRO-0002', 1, 40000, 13, 10)::transactions.purchase_reorder_type,
-- ROW(1, 'ETBRO-0002', 1, 40000, 13, 10)::transactions.purchase_reorder_type
-- ]);
-- 



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


