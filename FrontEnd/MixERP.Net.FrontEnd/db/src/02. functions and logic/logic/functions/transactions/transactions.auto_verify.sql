DROP FUNCTION IF EXISTS transactions.auto_verify(bigint) CASCADE;

CREATE FUNCTION transactions.auto_verify(bigint)
RETURNS VOID
AS
$$
    DECLARE _transaction_master_id bigint;
    DECLARE _transaction_posted_by integer;
    DECLARE _verifier integer;
    DECLARE _status integer;
    DECLARE _reason national character varying(128);
    DECLARE _rejected smallint=-3;
    DECLARE _closed smallint=-2;
    DECLARE _withdrawn smallint=-1;
    DECLARE _unapproved smallint = 0;
    DECLARE _auto_approved smallint = 1;
    DECLARE _approved smallint=2;
    DECLARE _book text;
    DECLARE _auto_verify_sales boolean;
    DECLARE _sales_verification_limit money_strict2;
    DECLARE _auto_verify_purchase boolean;
    DECLARE _purchase_verification_limit money_strict2;
    DECLARE _auto_verify_gl boolean;
    DECLARE _gl_verification_limit money_strict2;
    DECLARE _posted_amount money_strict2;
    DECLARE _auto_verification boolean=true;
    DECLARE _has_policy boolean=false;
BEGIN
    _transaction_master_id := $1;

    SELECT
        transactions.transaction_master.book,
        transactions.transaction_master.user_id
    INTO
        _book,
        _transaction_posted_by  
    FROM
    transactions.transaction_master
    WHERE transactions.transaction_master.transaction_master_id=_transaction_master_id;
    

    _verifier := office.get_sys_user_id();
    _status := 2;
    _reason := 'Automatically verified by workflow.';

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
        verify_sales_transactions,
        sales_verification_limit,
        verify_purchase_transactions,
        purchase_verification_limit,
        verify_gl_transactions,
        gl_verification_limit
    INTO
        _has_policy,
        _auto_verify_sales,
        _sales_verification_limit,
        _auto_verify_purchase,
        _purchase_verification_limit,
        _auto_verify_gl,
        _gl_verification_limit
    FROM
    policy.auto_verification_policy
    WHERE user_id=_transaction_posted_by
    AND is_active=true
    AND now() >= effective_from
    AND now() <= ends_on;



    IF(lower(_book) LIKE 'sales%') THEN
        IF(_auto_verify_sales = false) THEN
            _auto_verification := false;
        END IF;
        IF(_auto_verify_sales = true) THEN
            IF(_posted_amount > _sales_verification_limit AND _sales_verification_limit > 0::money_strict2) THEN
                _auto_verification := false;
            END IF;
        END IF;         
    END IF;


    IF(lower(_book) LIKE 'purchase%') THEN
        IF(_auto_verify_purchase = false) THEN
            _auto_verification := false;
        END IF;
        IF(_auto_verify_purchase = true) THEN
            IF(_posted_amount > _purchase_verification_limit AND _purchase_verification_limit > 0::money_strict2) THEN
                _auto_verification := false;
            END IF;
        END IF;         
    END IF;


    IF(lower(_book) LIKE 'journal%') THEN
        IF(_auto_verify_gl = false) THEN
            _auto_verification := false;
        END IF;
        IF(_auto_verify_gl = true) THEN
            IF(_posted_amount > _gl_verification_limit AND _gl_verification_limit > 0::money_strict2) THEN
                _auto_verification := false;
            END IF;
        END IF;         
    END IF;

    IF(_has_policy=true) THEN
        IF(_auto_verification = true) THEN
            UPDATE transactions.transaction_master
            SET 
                last_verified_on = now(),
                verified_by_user_id=_verifier,
                verification_status_id=_status,
                verification_reason=_reason
            WHERE
                transactions.transaction_master.transaction_master_id=_transaction_master_id;
        END IF;
    ELSE
        RAISE NOTICE 'No auto verification policy found for this user.';
    END IF;
    RETURN;
END
$$
LANGUAGE plpgsql;



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


DROP FUNCTION IF EXISTS unit_tests.auto_verify_sales_test1();

CREATE FUNCTION unit_tests.auto_verify_sales_test1()
RETURNS public.test_result
AS
$$
        DECLARE _value_date date;
        DECLARE _tran_id bigint;
        DECLARE _verification_status_id smallint;
        DECLARE message test_result;
BEGIN
        _value_date := NOW()::date;

        PERFORM unit_tests.create_dummy_office();
        PERFORM unit_tests.create_dummy_users();
        PERFORM unit_tests.create_dummy_accounts();
        PERFORM unit_tests.create_dummy_auto_verification_policy(office.get_user_id_by_user_name('plpgunit-test-user-000001'), true, 0, true, 0, true, 0, '1-1-2000', '1-1-2020', true);

        _tran_id := nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));

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
                reference_number, 
                statement_reference
        )
        SELECT 
        _tran_id, 
        transactions.get_new_transaction_counter(_value_date), 
        transactions.get_transaction_code(_value_date, office.get_office_id_by_office_code('dummy-off01'), office.get_user_id_by_user_name('plpgunit-test-user-000001'), 1),
        'Sales.Direct',
        _value_date,
        office.get_user_id_by_user_name('plpgunit-test-user-000001'),
        1,
        office.get_office_id_by_office_code('dummy-off01'),
        'REF# TEST',
        'Thou art not able to see this.';



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

        SELECT _tran_id, 'Cr', core.get_account_id_by_account_number('TEST-ACC-001'), '', 'NPR', 12000, 'NPR', 1, 12000 UNION ALL
        SELECT _tran_id, 'Dr', core.get_account_id_by_account_number('TEST-ACC-002'), '', 'NPR', 3000, 'NPR', 1, 3000 UNION ALL
        SELECT _tran_id, 'Dr', core.get_account_id_by_account_number('TEST-ACC-003'), '', 'NPR', 9000, 'NPR', 1, 9000;


        PERFORM transactions.auto_verify(currval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id')));

        SELECT verification_status_id
        INTO _verification_status_id
        FROM transactions.transaction_master
        WHERE transaction_master_id = _tran_id;

        IF(_verification_status_id < 1) THEN
                SELECT assert.fail('This transaction should have been verified.') INTO message;
                RETURN message;
        END IF;

    SELECT assert.ok('End of test.') INTO message;  
    RETURN message;
END
$$
LANGUAGE plpgsql;


DROP FUNCTION IF EXISTS unit_tests.auto_verify_sales_test2();

CREATE FUNCTION unit_tests.auto_verify_sales_test2()
RETURNS public.test_result
AS
$$
        DECLARE _value_date date;
        DECLARE _tran_id bigint;
        DECLARE _verification_status_id smallint;
        DECLARE message test_result;
BEGIN
        _value_date := NOW()::date;
        
        PERFORM unit_tests.create_dummy_office();
        PERFORM unit_tests.create_dummy_users();
        PERFORM unit_tests.create_dummy_accounts();
        PERFORM unit_tests.create_dummy_auto_verification_policy(office.get_user_id_by_user_name('plpgunit-test-user-000001'), true, 100, true, 0, true, 0, '1-1-2000', '1-1-2020', true);

        _tran_id := nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));

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
                reference_number, 
                statement_reference
        )
        SELECT 
        _tran_id, 
        transactions.get_new_transaction_counter(_value_date), 
        transactions.get_transaction_code(_value_date, office.get_office_id_by_office_code('dummy-off01'), office.get_user_id_by_user_name('plpgunit-test-user-000001'), 1),
        'Sales.Direct',
        _value_date,
        office.get_user_id_by_user_name('plpgunit-test-user-000001'),
        1,
        office.get_office_id_by_office_code('dummy-off01'),
        'REF# TEST',
        'Thou art not able to see this.';



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

        SELECT _tran_id, 'Cr', core.get_account_id_by_account_number('TEST-ACC-001'), '', 'NPR', 12000, 'NPR', 1, 12000 UNION ALL
        SELECT _tran_id, 'Dr', core.get_account_id_by_account_number('TEST-ACC-002'), '', 'NPR', 3000, 'NPR', 1, 3000 UNION ALL
        SELECT _tran_id, 'Dr', core.get_account_id_by_account_number('TEST-ACC-003'), '', 'NPR', 9000, 'NPR', 1, 9000;


        PERFORM transactions.auto_verify(currval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id')));

        SELECT verification_status_id
        INTO _verification_status_id
        FROM transactions.transaction_master
        WHERE transaction_master_id = _tran_id;

        IF(_verification_status_id > 0) THEN
                SELECT assert.fail('This transaction should not have been verified.') INTO message;
                RETURN message;
        END IF;

    SELECT assert.ok('End of test.') INTO message;  
    RETURN message;
END
$$
LANGUAGE plpgsql;






DROP FUNCTION IF EXISTS unit_tests.auto_verify_purchase_test1();

CREATE FUNCTION unit_tests.auto_verify_purchase_test1()
RETURNS public.test_result
AS
$$
        DECLARE _value_date date;
        DECLARE _tran_id bigint;
        DECLARE _verification_status_id smallint;
        DECLARE message test_result;
BEGIN
        _value_date := NOW()::date;

        PERFORM unit_tests.create_dummy_office();
        PERFORM unit_tests.create_dummy_users();
        PERFORM unit_tests.create_dummy_accounts();
        PERFORM unit_tests.create_dummy_auto_verification_policy(office.get_user_id_by_user_name('plpgunit-test-user-000001'), true, 0, true, 0, true, 0, '1-1-2000', '1-1-2020', true);

        _tran_id := nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));

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
                reference_number, 
                statement_reference
        )
        SELECT 
        _tran_id, 
        transactions.get_new_transaction_counter(_value_date), 
        transactions.get_transaction_code(_value_date, office.get_office_id_by_office_code('dummy-off01'), office.get_user_id_by_user_name('plpgunit-test-user-000001'), 1),
        'Purchase.Direct',
        _value_date,
        office.get_user_id_by_user_name('plpgunit-test-user-000001'),
        1,
        office.get_office_id_by_office_code('dummy-off01'),
        'REF# TEST',
        'Thou art not able to see this.';



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

        SELECT _tran_id, 'Cr', core.get_account_id_by_account_number('TEST-ACC-001'), '', 'NPR', 12000, 'NPR', 1, 12000 UNION ALL
        SELECT _tran_id, 'Dr', core.get_account_id_by_account_number('TEST-ACC-002'), '', 'NPR', 3000, 'NPR', 1, 3000 UNION ALL
        SELECT _tran_id, 'Dr', core.get_account_id_by_account_number('TEST-ACC-003'), '', 'NPR', 9000, 'NPR', 1, 9000;


        PERFORM transactions.auto_verify(currval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id')));

        SELECT verification_status_id
        INTO _verification_status_id
        FROM transactions.transaction_master
        WHERE transaction_master_id = _tran_id;

        IF(_verification_status_id < 1) THEN
                SELECT assert.fail('This transaction should have been verified.') INTO message;
                RETURN message;
        END IF;

    SELECT assert.ok('End of test.') INTO message;  
    RETURN message;
END
$$
LANGUAGE plpgsql;


DROP FUNCTION IF EXISTS unit_tests.auto_verify_purchase_test2();

CREATE FUNCTION unit_tests.auto_verify_purchase_test2()
RETURNS public.test_result
AS
$$
        DECLARE _value_date date;
        DECLARE _tran_id bigint;
        DECLARE _verification_status_id smallint;
        DECLARE message test_result;
BEGIN
        _value_date := NOW()::date;
        
        PERFORM unit_tests.create_dummy_office();
        PERFORM unit_tests.create_dummy_users();
        PERFORM unit_tests.create_dummy_accounts();
        PERFORM unit_tests.create_dummy_auto_verification_policy(office.get_user_id_by_user_name('plpgunit-test-user-000001'), true, 0, true, 100, true, 0, '1-1-2000', '1-1-2020', true);

        _tran_id := nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));

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
                reference_number, 
                statement_reference
        )
        SELECT 
        _tran_id, 
        transactions.get_new_transaction_counter(_value_date), 
        transactions.get_transaction_code(_value_date, office.get_office_id_by_office_code('dummy-off01'), office.get_user_id_by_user_name('plpgunit-test-user-000001'), 1),
        'Purchase.Direct',
        _value_date,
        office.get_user_id_by_user_name('plpgunit-test-user-000001'),
        1,
        office.get_office_id_by_office_code('dummy-off01'),
        'REF# TEST',
        'Thou art not able to see this.';



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

        SELECT _tran_id, 'Cr', core.get_account_id_by_account_number('TEST-ACC-001'), '', 'NPR', 12000, 'NPR', 1, 12000 UNION ALL
        SELECT _tran_id, 'Dr', core.get_account_id_by_account_number('TEST-ACC-002'), '', 'NPR', 3000, 'NPR', 1, 3000 UNION ALL
        SELECT _tran_id, 'Dr', core.get_account_id_by_account_number('TEST-ACC-003'), '', 'NPR', 9000, 'NPR', 1, 9000;


        PERFORM transactions.auto_verify(currval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id')));

        SELECT verification_status_id
        INTO _verification_status_id
        FROM transactions.transaction_master
        WHERE transaction_master_id = _tran_id;

        IF(_verification_status_id > 0) THEN
                SELECT assert.fail('This transaction should not have been verified.') INTO message;
                RETURN message;
        END IF;

    SELECT assert.ok('End of test.') INTO message;  
    RETURN message;
END
$$
LANGUAGE plpgsql;






DROP FUNCTION IF EXISTS unit_tests.auto_verify_journal_test1();

CREATE FUNCTION unit_tests.auto_verify_journal_test1()
RETURNS public.test_result
AS
$$
        DECLARE _value_date date;
        DECLARE _tran_id bigint;
        DECLARE _verification_status_id smallint;
        DECLARE message test_result;
BEGIN
        _value_date := NOW()::date;

        PERFORM unit_tests.create_dummy_office();
        PERFORM unit_tests.create_dummy_users();
        PERFORM unit_tests.create_dummy_accounts();
        PERFORM unit_tests.create_dummy_auto_verification_policy(office.get_user_id_by_user_name('plpgunit-test-user-000001'), true, 0, true, 0, true, 0, '1-1-2000', '1-1-2020', true);

        _tran_id := nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));

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
                reference_number, 
                statement_reference
        )
        SELECT 
        _tran_id, 
        transactions.get_new_transaction_counter(_value_date), 
        transactions.get_transaction_code(_value_date, office.get_office_id_by_office_code('dummy-off01'), office.get_user_id_by_user_name('plpgunit-test-user-000001'), 1),
        'Journal',
        _value_date,
        office.get_user_id_by_user_name('plpgunit-test-user-000001'),
        1,
        office.get_office_id_by_office_code('dummy-off01'),
        'REF# TEST',
        'Thou art not able to see this.';



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

        SELECT _tran_id, 'Cr', core.get_account_id_by_account_number('TEST-ACC-001'), '', 'NPR', 12000, 'NPR', 1, 12000 UNION ALL
        SELECT _tran_id, 'Dr', core.get_account_id_by_account_number('TEST-ACC-002'), '', 'NPR', 3000, 'NPR', 1, 3000 UNION ALL
        SELECT _tran_id, 'Dr', core.get_account_id_by_account_number('TEST-ACC-003'), '', 'NPR', 9000, 'NPR', 1, 9000;


        PERFORM transactions.auto_verify(currval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id')));

        SELECT verification_status_id
        INTO _verification_status_id
        FROM transactions.transaction_master
        WHERE transaction_master_id = _tran_id;

        IF(_verification_status_id < 1) THEN
                SELECT assert.fail('This transaction should have been verified.') INTO message;
                RETURN message;
        END IF;

    SELECT assert.ok('End of test.') INTO message;  
    RETURN message;
END
$$
LANGUAGE plpgsql;


DROP FUNCTION IF EXISTS unit_tests.auto_verify_journal_test2();

CREATE FUNCTION unit_tests.auto_verify_journal_test2()
RETURNS public.test_result
AS
$$
        DECLARE _value_date date;
        DECLARE _tran_id bigint;
        DECLARE _verification_status_id smallint;
        DECLARE message test_result;
BEGIN
        _value_date := NOW()::date;
        
        PERFORM unit_tests.create_dummy_office();
        PERFORM unit_tests.create_dummy_users();
        PERFORM unit_tests.create_dummy_accounts();
        PERFORM unit_tests.create_dummy_auto_verification_policy(office.get_user_id_by_user_name('plpgunit-test-user-000001'), true, 0, true, 0, true, 100, '1-1-2000', '1-1-2020', true);

        _tran_id := nextval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id'));

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
                reference_number, 
                statement_reference
        )
        SELECT 
        _tran_id, 
        transactions.get_new_transaction_counter(_value_date), 
        transactions.get_transaction_code(_value_date, office.get_office_id_by_office_code('dummy-off01'), office.get_user_id_by_user_name('plpgunit-test-user-000001'), 1),
        'Journal',
        _value_date,
        office.get_user_id_by_user_name('plpgunit-test-user-000001'),
        1,
        office.get_office_id_by_office_code('dummy-off01'),
        'REF# TEST',
        'Thou art not able to see this.';



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

        SELECT _tran_id, 'Cr', core.get_account_id_by_account_number('TEST-ACC-001'), '', 'NPR', 12000, 'NPR', 1, 12000 UNION ALL
        SELECT _tran_id, 'Dr', core.get_account_id_by_account_number('TEST-ACC-002'), '', 'NPR', 3000, 'NPR', 1, 3000 UNION ALL
        SELECT _tran_id, 'Dr', core.get_account_id_by_account_number('TEST-ACC-003'), '', 'NPR', 9000, 'NPR', 1, 9000;


        PERFORM transactions.auto_verify(currval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id')));

        SELECT verification_status_id
        INTO _verification_status_id
        FROM transactions.transaction_master
        WHERE transaction_master_id = _tran_id;

        IF(_verification_status_id > 0) THEN
                SELECT assert.fail('This transaction should not have been verified.') INTO message;
                RETURN message;
        END IF;

    SELECT assert.ok('End of test.') INTO message;  
    RETURN message;
END
$$
LANGUAGE plpgsql;

