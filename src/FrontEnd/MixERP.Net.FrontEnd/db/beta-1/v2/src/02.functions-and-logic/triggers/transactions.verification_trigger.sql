DROP FUNCTION IF EXISTS transactions.verification_trigger() CASCADE;
CREATE FUNCTION transactions.verification_trigger()
RETURNS TRIGGER
AS
$$
    DECLARE _transaction_master_id bigint;
    DECLARE _transaction_posted_by integer;
    DECLARE _old_verifier integer;
    DECLARE _old_status integer;
    DECLARE _old_reason national character varying(128);
    DECLARE _verifier integer;
    DECLARE _status integer;
    DECLARE _reason national character varying(128);
    DECLARE _has_policy boolean;
    DECLARE _is_sys boolean;
    DECLARE _rejected smallint=-3;
    DECLARE _closed smallint=-2;
    DECLARE _withdrawn smallint=-1;
    DECLARE _unapproved smallint = 0;
    DECLARE _auto_approved smallint = 1;
    DECLARE _approved smallint=2;
    DECLARE _book text;
    DECLARE _can_verify_sales_transactions boolean;
    DECLARE _sales_verification_limit money_strict2;
    DECLARE _can_verify_purchase_transactions boolean;
    DECLARE _purchase_verification_limit money_strict2;
    DECLARE _can_verify_gl_transactions boolean;
    DECLARE _gl_verification_limit money_strict2;
    DECLARE _can_verify_self boolean;
    DECLARE _self_verification_limit money_strict2;
    DECLARE _posted_amount money_strict2;
BEGIN
    IF TG_OP='DELETE' THEN
        RAISE EXCEPTION 'Deleting a transaction is not allowed. Mark the transaction as rejected instead.'
        USING ERRCODE='P5800';
    END IF;

    IF TG_OP='UPDATE' THEN
        RAISE NOTICE 'Columns except the following will be ignored for this update: %', 'verified_by_user_id, verification_status_id, verification_reason.';

        IF(OLD.transaction_master_id IS DISTINCT FROM NEW.transaction_master_id) THEN
            RAISE EXCEPTION 'Cannot update the column %', '"transaction_master_id".'
            USING ERRCODE='P8502';
        END IF;

        IF(OLD.transaction_counter IS DISTINCT FROM NEW.transaction_counter) THEN
            RAISE EXCEPTION 'Cannot update the column %', '"transaction_counter".'
            USING ERRCODE='P8502';            
        END IF;

        IF(OLD.transaction_code IS DISTINCT FROM NEW.transaction_code) THEN
            RAISE EXCEPTION 'Cannot update the column %', '"transaction_code".'
            USING ERRCODE='P8502';
        END IF;

        IF(OLD.book IS DISTINCT FROM NEW.book) THEN
            RAISE EXCEPTION 'Cannot update the column %', '"book".'
            USING ERRCODE='P8502';
        END IF;

        IF(OLD.value_date IS DISTINCT FROM NEW.value_date) THEN
            RAISE EXCEPTION 'Cannot update the column %', '"value_date".'
            USING ERRCODE='P8502';
        END IF;

        IF(OLD.transaction_ts IS DISTINCT FROM NEW.transaction_ts) THEN
            RAISE EXCEPTION 'Cannot update the column %', '"transaction_ts".'
            USING ERRCODE='P8502';
        END IF;

        IF(OLD.login_id IS DISTINCT FROM NEW.login_id) THEN
            RAISE EXCEPTION 'Cannot update the column %', '"login_id".'
            USING ERRCODE='P8502';
        END IF;

        IF(OLD.user_id IS DISTINCT FROM NEW.user_id) THEN
            RAISE EXCEPTION 'Cannot update the column %', '"user_id".'
            USING ERRCODE='P8502';
        END IF;

        IF(OLD.sys_user_id IS DISTINCT FROM NEW.sys_user_id) THEN
            RAISE EXCEPTION 'Cannot update the column %', '"sys_user_id".'
            USING ERRCODE='P8502';
        END IF;

        IF(OLD.office_id IS DISTINCT FROM NEW.office_id) THEN
            RAISE EXCEPTION 'Cannot update the column %', '"office_id".'
            USING ERRCODE='P8502';
        END IF;

        IF(OLD.cost_center_id IS DISTINCT FROM NEW.cost_center_id) THEN
            RAISE EXCEPTION 'Cannot update the column %', '"cost_center_id".'
            USING ERRCODE='P8502';
        END IF;

        _transaction_master_id := OLD.transaction_master_id;
        _book := OLD.book;
        _old_verifier := OLD.verified_by_user_id;
        _old_status := OLD.verification_status_id;
        _old_reason := OLD.verification_reason;
        _transaction_posted_by := OLD.user_id;      
        _verifier := NEW.verified_by_user_id;
        _status := NEW.verification_status_id;
        _reason := NEW.verification_reason;
        _is_sys := office.is_sys(_verifier);

        
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
            gl_verification_limit,
            can_self_verify,
            self_verification_limit
        INTO
            _has_policy,
            _can_verify_sales_transactions,
            _sales_verification_limit,
            _can_verify_purchase_transactions,
            _purchase_verification_limit,
            _can_verify_gl_transactions,
            _gl_verification_limit,
            _can_verify_self,
            _self_verification_limit
        FROM
        policy.voucher_verification_policy
        WHERE user_id=_verifier
        AND is_active=true
        AND now() >= effective_from
        AND now() <= ends_on;

        IF(_verifier IS NULL) THEN
            RAISE EXCEPTION 'Access is denied.'
            USING ERRCODE='P9001';
        END IF;     
        
        IF(_status != _withdrawn AND _has_policy = false) THEN
            RAISE EXCEPTION 'Access is denied. You don''t have the right to verify the transaction.'
            USING ERRCODE='P9016';
        END IF;

        IF(_status = _withdrawn AND _has_policy = false) THEN
            IF(_transaction_posted_by != _verifier) THEN
                RAISE EXCEPTION 'Access is denied. You don''t have the right to withdraw the transaction.'
                USING ERRCODE='P9017';
            END IF;
        END IF;

        IF(_status = _auto_approved AND _is_sys = false) THEN
            RAISE EXCEPTION 'Access is denied.'
            USING ERRCODE='P9001';
        END IF;


        IF(_has_policy = false) THEN
            RAISE EXCEPTION 'Access is denied.'
            USING ERRCODE='P9001';
        END IF;


        --Is trying verify self transaction.
        IF(NEW.verified_by_user_id = NEW.user_id) THEN
            IF(_can_verify_self = false) THEN
                RAISE EXCEPTION 'Please ask someone else to verify the transaction you posted.'
                USING ERRCODE='P5901';                
            END IF;
            IF(_can_verify_self = true) THEN
                IF(_posted_amount > _self_verification_limit AND _self_verification_limit > 0::money_strict2) THEN
                    RAISE EXCEPTION 'Self verification limit exceeded. The transaction was not verified.'
                    USING ERRCODE='P5910';
                END IF;
            END IF;
        END IF;

        IF(lower(_book) LIKE '%sales%') THEN
            IF(_can_verify_sales_transactions = false) THEN
                RAISE EXCEPTION 'Access is denied.'
                USING ERRCODE='P9001';
            END IF;
            IF(_can_verify_sales_transactions = true) THEN
                IF(_posted_amount > _sales_verification_limit AND _sales_verification_limit > 0::money_strict2) THEN
                    RAISE EXCEPTION 'Sales verification limit exceeded. The transaction was not verified.'
                    USING ERRCODE='P5911';
                END IF;
            END IF;         
        END IF;


        IF(lower(_book) LIKE '%purchase%') THEN
            IF(_can_verify_purchase_transactions = false) THEN
                RAISE EXCEPTION 'Access is denied.'
                USING ERRCODE='P9001';
            END IF;
            IF(_can_verify_purchase_transactions = true) THEN
                IF(_posted_amount > _purchase_verification_limit AND _purchase_verification_limit > 0::money_strict2) THEN
                    RAISE EXCEPTION 'Purchase verification limit exceeded. The transaction was not verified.'
                    USING ERRCODE='P5912';
                END IF;
            END IF;         
        END IF;


        IF(lower(_book) LIKE 'journal%') THEN
            IF(_can_verify_gl_transactions = false) THEN
                RAISE EXCEPTION 'Access is denied.'
                USING ERRCODE='P9001';
            END IF;
            IF(_can_verify_gl_transactions = true) THEN
                IF(_posted_amount > _gl_verification_limit AND _gl_verification_limit > 0::money_strict2) THEN
                    RAISE EXCEPTION 'GL verification limit exceeded. The transaction was not verified.'
                    USING ERRCODE='P5913';
                END IF;
            END IF;         
        END IF;

        NEW.last_verified_on := now();

    END IF; 
    RETURN NEW;
END
$$
LANGUAGE plpgsql;


CREATE TRIGGER verification_update_trigger
AFTER UPDATE
ON transactions.transaction_master
FOR EACH ROW 
EXECUTE PROCEDURE transactions.verification_trigger();

CREATE TRIGGER verification_delete_trigger
BEFORE DELETE
ON transactions.transaction_master
FOR EACH ROW 
EXECUTE PROCEDURE transactions.verification_trigger();
