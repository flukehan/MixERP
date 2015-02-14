-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/FrontEnd/MixERP.Net.FrontEnd/db/src/02. functions and logic/logic/functions/transactions/transactions.verify_transaction.sql --<--<--
DROP FUNCTION IF EXISTS transactions.verify_transaction
(
    _transaction_master_id                  bigint,
    _office_id                              integer,
    _user_id                                integer,
    _login_id                               bigint,
    _verification_status_id                 smallint,
    _reason                                 national character varying
) 
CASCADE;

CREATE FUNCTION transactions.verify_transaction
(
    _transaction_master_id                  bigint,
    _office_id                              integer,
    _user_id                                integer,
    _login_id                               bigint,
    _verification_status_id                 smallint,
    _reason                                 national character varying
)
RETURNS VOID
VOLATILE
AS
$$
    DECLARE _transaction_posted_by integer;
    DECLARE _can_approve boolean=true;
    DECLARE _book text;
    DECLARE _verify_sales boolean;
    DECLARE _sales_verification_limit money_strict2;
    DECLARE _verify_purchase boolean;
    DECLARE _purchase_verification_limit money_strict2;
    DECLARE _verify_gl boolean;
    DECLARE _gl_verification_limit money_strict2;
    DECLARE _posted_amount money_strict2;
    DECLARE _has_policy boolean=false;
    DECLARE _voucher_date date;
    DECLARE _voucher_office_id integer;
    DECLARE _value_date date=transactions.get_value_date(_office_id);
BEGIN

    SELECT
        transactions.transaction_master.book,
        transactions.transaction_master.value_date,
        transactions.transaction_master.office_id,
        transactions.transaction_master.user_id
    INTO
        _book,
        _voucher_date,
        _voucher_office_id,
        _transaction_posted_by  
    FROM
    transactions.transaction_master
    WHERE transactions.transaction_master.transaction_master_id=_transaction_master_id;


    IF(_voucher_office_id <> _office_id) THEN
        RAISE EXCEPTION 'Access is denied. You cannot verify a transaction of another office.'
        USING ERRCODE='P9014';
    END IF;
    
    IF(_voucher_date <> _value_date) THEN
        RAISE EXCEPTION 'Access is denied. You cannot verify past or futuer dated transaction.'
        USING ERRCODE='P9015';
    END IF;
    
    SELECT
        SUM(amount_in_local_currency)
    INTO
        _posted_amount
    FROM
        transactions.transaction_details
    WHERE transactions.transaction_details.transaction_master_id = _transaction_master_id
    AND transactions.transaction_details.tran_type='Cr';


    SELECT
        true,
        can_verify_sales_transactions,
        sales_verification_limit,
        can_verify_purchase_transactions,
        purchase_verification_limit,
        can_verify_gl_transactions,
        gl_verification_limit
    INTO
        _has_policy,
        _verify_sales,
        _sales_verification_limit,
        _verify_purchase,
        _purchase_verification_limit,
        _verify_gl,
        _gl_verification_limit
    FROM
    policy.voucher_verification_policy
    WHERE user_id=_user_id
    AND is_active=true
    AND now() >= effective_from
    AND now() <= ends_on;


    IF(lower(_book) LIKE 'sales%') THEN
        IF(_verify_sales = false) THEN
            _can_approve := false;
        END IF;
        IF(_verify_sales = true) THEN
            IF(_posted_amount > _sales_verification_limit AND _sales_verification_limit > 0::money_strict2) THEN
                _can_approve := false;
            END IF;
        END IF;         
    END IF;


    IF(lower(_book) LIKE 'purchase%') THEN
        IF(_verify_purchase = false) THEN
            _can_approve := false;
        END IF;
        IF(_verify_purchase = true) THEN
            IF(_posted_amount > _purchase_verification_limit AND _purchase_verification_limit > 0::money_strict2) THEN
                _can_approve := false;
            END IF;
        END IF;         
    END IF;


    IF(lower(_book) LIKE 'journal%') THEN
        IF(_verify_gl = false) THEN
            _can_approve := false;
        END IF;
        IF(_verify_gl = true) THEN
            IF(_posted_amount > _gl_verification_limit AND _gl_verification_limit > 0::money_strict2) THEN
                _can_approve := false;
            END IF;
        END IF;         
    END IF;

    IF(_has_policy=true) THEN
        IF(_can_approve = true) THEN
            UPDATE transactions.transaction_master
            SET 
                last_verified_on = now(),
                verified_by_user_id=_user_id,
                verification_status_id=_verification_status_id,
                verification_reason=_reason
            WHERE
                transactions.transaction_master.transaction_master_id=_transaction_master_id;
            RAISE NOTICE 'Done.';
        END IF;
    ELSE
        RAISE EXCEPTION 'No verification policy found for this user.'
        USING ERRCODE='P4030';
    END IF;
    RETURN;
END
$$
LANGUAGE plpgsql;


--SELECT * FROM transactions.verify_transaction(65::bigint, 2, 2, 51::bigint, -3::smallint, '');

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