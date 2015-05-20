DROP FUNCTION IF EXISTS unit_tests.create_dummy_auto_verification_policy
(
        _user_id integer, 
        _verify_sales_transactions boolean, 
        _sales_verification_limit money_strict2, 
        _verify_purchase_transactions boolean, 
        _purchase_verification_limit money_strict2, 
        _verify_gl_transactions boolean,
        _gl_verification_limit money_strict2,
        _effective_from date,
        _ends_on date,
        _is_active boolean
);

CREATE FUNCTION unit_tests.create_dummy_auto_verification_policy
(
        _user_id integer, 
        _verify_sales_transactions boolean, 
        _sales_verification_limit money_strict2, 
        _verify_purchase_transactions boolean, 
        _purchase_verification_limit money_strict2, 
        _verify_gl_transactions boolean,
        _gl_verification_limit money_strict2,
        _effective_from date,
        _ends_on date,
        _is_active boolean
)
RETURNS void 
AS
$$
BEGIN
        IF NOT EXISTS(SELECT 1 FROM policy.auto_verification_policy WHERE user_id=_user_id) THEN
                INSERT INTO policy.auto_verification_policy(user_id, verify_sales_transactions, sales_verification_limit, verify_purchase_transactions, purchase_verification_limit, verify_gl_transactions, gl_verification_limit, effective_from, ends_on, is_active)
                SELECT _user_id, _verify_sales_transactions, _sales_verification_limit, _verify_purchase_transactions, _purchase_verification_limit, _verify_gl_transactions, _gl_verification_limit, _effective_from, _ends_on, _is_active;
                RETURN;
        END IF;

        UPDATE policy.auto_verification_policy
        SET 
                verify_sales_transactions = _verify_sales_transactions,
                sales_verification_limit = _sales_verification_limit,
                verify_purchase_transactions = _verify_purchase_transactions,
                purchase_verification_limit = _purchase_verification_limit,
                verify_gl_transactions = _verify_gl_transactions, 
                gl_verification_limit = _gl_verification_limit, 
                effective_from = _effective_from, 
                ends_on = _ends_on, 
                is_active = _is_active                
        WHERE user_id=_user_id;
        
END
$$
LANGUAGE plpgsql;