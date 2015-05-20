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
    DECLARE this                                RECORD;
    DECLARE detail                              transactions.stock_detail_type[];
BEGIN
        
        CREATE TEMPORARY TABLE _temp_transaction(supplier_code national character varying(12))  ON COMMIT DROP;

        INSERT INTO _temp_transaction(supplier_code)
        SELECT DISTINCT supplier_code FROM explode_array(_details);

        FOR this IN SELECT supplier_code FROM _temp_transaction
        LOOP
            detail := 
                    (
                        SELECT 
                            array_agg
                            (
                                (
                                    NULL,--store_id
                                    core.get_item_code_by_item_id(details.item_id),
                                    details.order_quantity,
                                    core.get_unit_name_by_unit_id(details.unit_id),
                                    details.price,
                                    0, --discount
                                    0, --shipping_charge
                                    details.tax_code,
                                    NULL --tax (will be automatically caculated)
                               )::transactions.stock_detail_type
                           )
                        FROM explode_array(_details) as details
                        WHERE details.supplier_code = this.supplier_code
                    )::transactions.stock_detail_type[];

            
            PERFORM transactions.post_non_gl_transaction(
                'Purchase.Order', 
                _office_id,
                _user_id,
                _login_id,
                _value_date,
                '',
                'Automatically generated order.',
                this.supplier_code,
                NULL,
                false,
                NULL,
                NULL,
                NULL,
                NULL,
                NULL,
                detail,
                NULL

            );

        END LOOP;

        RETURN true;
END
$$
LANGUAGE plpgsql;


-- SELECT * FROM transactions.post_purhcase_reorder('1-1-2000', 1, 2, 2,
-- ARRAY[
-- ROW(1, 'ETBRO-0002', 1, 40000, 'MoF-NP-KTM-VAT', 10)::transactions.purchase_reorder_type,
-- ROW(1, 'ETBRO-0002', 1, 40000, '', 10)::transactions.purchase_reorder_type
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


