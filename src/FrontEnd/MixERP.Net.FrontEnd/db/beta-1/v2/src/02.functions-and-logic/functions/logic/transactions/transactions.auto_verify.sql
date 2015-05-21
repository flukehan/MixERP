DROP FUNCTION IF EXISTS transactions.auto_verify
(
    _tran_id        bigint,
    _office_id      integer
) CASCADE;

CREATE FUNCTION transactions.auto_verify
(
    _tran_id        bigint,
    _office_id      integer
)
RETURNS VOID
VOLATILE
AS
$$
    DECLARE _transaction_master_id          bigint;
    DECLARE _transaction_posted_by          integer;
    DECLARE _verifier                       integer;
    DECLARE _status                         integer;
    DECLARE _reason                         national character varying(128);
    DECLARE _rejected                       smallint=-3;
    DECLARE _closed                         smallint=-2;
    DECLARE _withdrawn                      smallint=-1;
    DECLARE _unapproved                     smallint = 0;
    DECLARE _auto_approved                  smallint = 1;
    DECLARE _approved                       smallint=2;
    DECLARE _book                           text;
    DECLARE _auto_verify_sales              boolean;
    DECLARE _sales_verification_limit       public.money_strict2;
    DECLARE _auto_verify_purchase           boolean;
    DECLARE _purchase_verification_limit    public.money_strict2;
    DECLARE _auto_verify_gl                 boolean;
    DECLARE _gl_verification_limit          public.money_strict2;
    DECLARE _posted_amount                  public.money_strict2;
    DECLARE _auto_verification              boolean=true;
    DECLARE _has_policy                     boolean=false;
    DECLARE _voucher_date                   date;
    DECLARE _value_date                     date=transactions.get_value_date(_office_id);
BEGIN
    _transaction_master_id := $1;

    SELECT
        transactions.transaction_master.book,
        transactions.transaction_master.value_date,
        transactions.transaction_master.user_id
    INTO
        _book,
        _voucher_date,
        _transaction_posted_by  
    FROM
    transactions.transaction_master
    WHERE transactions.transaction_master.transaction_master_id=_transaction_master_id;

    IF(_voucher_date <> _value_date) THEN
        RETURN;
    END IF;

    _verifier := office.get_sys_user_id();
    _status := 2;
    _reason := 'Automatically verified by workflow.';

    IF EXISTS
    (
        SELECT 1 FROM policy.voucher_verification_policy    
        WHERE user_id=_verifier
        AND is_active=true
        AND now() >= effective_from
        AND now() <= ends_on
    ) THEN
        RAISE INFO 'A sys cannot have a verification policy defined.';
        RETURN;
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
    AND office_id = _office_id
    AND is_active=true
    AND now() >= effective_from
    AND now() <= ends_on;



    IF(lower(_book) LIKE 'sales%') THEN
        IF(_auto_verify_sales = false) THEN
            _auto_verification := false;
        END IF;
        IF(_auto_verify_sales = true) THEN
            IF(_posted_amount > _sales_verification_limit AND _sales_verification_limit > 0::public.money_strict2) THEN
                _auto_verification := false;
            END IF;
        END IF;         
    END IF;


    IF(lower(_book) LIKE 'purchase%') THEN
        IF(_auto_verify_purchase = false) THEN
            _auto_verification := false;
        END IF;
        IF(_auto_verify_purchase = true) THEN
            IF(_posted_amount > _purchase_verification_limit AND _purchase_verification_limit > 0::public.money_strict2) THEN
                _auto_verification := false;
            END IF;
        END IF;         
    END IF;


    IF(lower(_book) LIKE 'journal%') THEN
        IF(_auto_verify_gl = false) THEN
            _auto_verification := false;
        END IF;
        IF(_auto_verify_gl = true) THEN
            IF(_posted_amount > _gl_verification_limit AND _gl_verification_limit > 0::public.money_strict2) THEN
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
                transactions.transaction_master.transaction_master_id=_transaction_master_id
            OR
                transactions.transaction_master.cascading_tran_id=_transaction_master_id;

            PERFORM transactions.create_recurring_invoices(_transaction_master_id);
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
    DECLARE _value_date                             date;
    DECLARE _tran_id                                bigint;
    DECLARE _verification_status_id                 smallint;
    DECLARE _book_name                              national character varying(48)='Sales.Direct';
    DECLARE _office_id                              integer;
    DECLARE _user_id                                integer;
    DECLARE _login_id                               bigint;
    DECLARE _cost_center_id                         integer;
    DECLARE _reference_number                       national character varying(24)='Plpgunit.fixture';
    DECLARE _statement_reference                    text='Plpgunit test was here.';
    DECLARE _is_credit                              boolean=false;
    DECLARE _payment_term_id                        integer;
    DECLARE _party_code                             national character varying(12);
    DECLARE _price_type_id                          integer;
    DECLARE _salesperson_id                         integer;
    DECLARE _shipper_id                             integer;
    DECLARE _shipping_address_code                  national character varying(12)='';
    DECLARE _store_id                               integer;
    DECLARE _is_non_taxable_sales                   boolean=true;
    DECLARE _details                                transactions.stock_detail_type[];
    DECLARE _attachments                            core.attachment_type[];
    DECLARE message                                 test_result;
BEGIN
    PERFORM unit_tests.create_mock();
    PERFORM unit_tests.sign_in_test();

    _office_id          := office.get_office_id_by_office_code('dummy-off01');
    _user_id            := office.get_user_id_by_user_name('plpgunit-test-user-000001');
    _login_id           := office.get_login_id(_user_id);
    _value_date         := transactions.get_value_date(_office_id);
    _cost_center_id     := office.get_cost_center_id_by_cost_center_code('dummy-cs01');
    _payment_term_id    := core.get_payment_term_id_by_payment_term_code('dummy-pt01');
    _party_code         := 'dummy-pr01';
    _price_type_id      := core.get_price_type_id_by_price_type_code('dummy-pt01');
    _salesperson_id     := core.get_salesperson_id_by_salesperson_code('dummy-sp01');
    _shipper_id         := core.get_shipper_id_by_shipper_code('dummy-sh01');
    _store_id           := office.get_store_id_by_store_code('dummy-st01');

    
    _details            := ARRAY[
                             ROW(_store_id, 'dummy-it01', 1, 'Test Mock Unit',1800000, 0, 0, '', 0)::transactions.stock_detail_type,
                             ROW(_store_id, 'dummy-it02', 2, 'Test Mock Unit',1300000, 300, 0, '', 0)::transactions.stock_detail_type];
             
    
    PERFORM unit_tests.create_dummy_auto_verification_policy(office.get_user_id_by_user_name('plpgunit-test-user-000001'), _office_id, true, 0, true, 0, true, 0, '1-1-2000', '1-1-2020', true);


    SELECT * FROM transactions.post_sales
    (
        _book_name,_office_id, _user_id, _login_id, _value_date, _cost_center_id, _reference_number, _statement_reference,
        _is_credit, _payment_term_id, _party_code, _price_type_id, _salesperson_id, _shipper_id,
        _shipping_address_code,
        _store_id,
        _is_non_taxable_sales,
        _details,
        _attachments,
        NULL
    ) INTO _tran_id;

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
    DECLARE _value_date                             date;
    DECLARE _tran_id                                bigint;
    DECLARE _verification_status_id                 smallint;
    DECLARE _book_name                              national character varying(48)='Sales.Direct';
    DECLARE _office_id                              integer;
    DECLARE _user_id                                integer;
    DECLARE _login_id                               bigint;
    DECLARE _cost_center_id                         integer;
    DECLARE _reference_number                       national character varying(24)='Plpgunit.fixture';
    DECLARE _statement_reference                    text='Plpgunit test was here.';
    DECLARE _is_credit                              boolean=false;
    DECLARE _payment_term_id                        integer;
    DECLARE _party_code                             national character varying(12);
    DECLARE _price_type_id                          integer;
    DECLARE _salesperson_id                         integer;
    DECLARE _shipper_id                             integer;
    DECLARE _shipping_address_code                  national character varying(12)='';
    DECLARE _store_id                               integer;
    DECLARE _is_non_taxable_sales                   boolean=true;
    DECLARE _details                                transactions.stock_detail_type[];
    DECLARE _attachments                            core.attachment_type[];
    DECLARE message                                 test_result;
BEGIN
    PERFORM unit_tests.create_mock();
    PERFORM unit_tests.sign_in_test();

    _office_id          := office.get_office_id_by_office_code('dummy-off01');
    _user_id            := office.get_user_id_by_user_name('plpgunit-test-user-000001');
    _login_id           := office.get_login_id(_user_id);
    _value_date         := transactions.get_value_date(_office_id);
    _cost_center_id     := office.get_cost_center_id_by_cost_center_code('dummy-cs01');
    _payment_term_id    := core.get_payment_term_id_by_payment_term_code('dummy-pt01');
    _party_code         := 'dummy-pr01';
    _price_type_id      := core.get_price_type_id_by_price_type_code('dummy-pt01');
    _salesperson_id     := core.get_salesperson_id_by_salesperson_code('dummy-sp01');
    _shipper_id         := core.get_shipper_id_by_shipper_code('dummy-sh01');
    _store_id           := office.get_store_id_by_store_code('dummy-st01');

    
    _details            := ARRAY[
                             ROW(_store_id, 'dummy-it01', 1, 'Test Mock Unit',180000, 0, 0, '', 0)::transactions.stock_detail_type,
                             ROW(_store_id, 'dummy-it02', 2, 'Test Mock Unit',130000, 300, 0, '', 0)::transactions.stock_detail_type];

    PERFORM unit_tests.create_dummy_auto_verification_policy(office.get_user_id_by_user_name('plpgunit-test-user-000001'), _office_id, true, 100, true, 0, true, 0, '1-1-2000', '1-1-2020', true);

    SELECT * FROM transactions.post_sales
    (
        _book_name,_office_id, _user_id, _login_id, _value_date, _cost_center_id, _reference_number, _statement_reference,
        _is_credit, _payment_term_id, _party_code, _price_type_id, _salesperson_id, _shipper_id,
        _shipping_address_code,
        _store_id,
        _is_non_taxable_sales,
        _details,
        _attachments,
        NULL
    ) INTO _tran_id;


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
    DECLARE _value_date                             date;
    DECLARE _tran_id                                bigint;
    DECLARE _verification_status_id                 smallint;
    DECLARE _book_name                              national character varying(48)='Purchase.Direct';
    DECLARE _office_id                              integer;
    DECLARE _user_id                                integer;
    DECLARE _login_id                               bigint;
    DECLARE _cost_center_id                         integer;
    DECLARE _reference_number                       national character varying(24)='Plpgunit.fixture';
    DECLARE _statement_reference                    text='Plpgunit test was here.';
    DECLARE _is_credit                              boolean=false;
    DECLARE _payment_term_id                        integer;
    DECLARE _party_code                             national character varying(12);
    DECLARE _price_type_id                          integer;
    DECLARE _shipper_id                             integer;
    DECLARE _store_id                               integer;
    DECLARE _is_non_taxable_sales                   boolean=true;
    DECLARE _tran_ids                               bigint[];
    DECLARE _details                                transactions.stock_detail_type[];
    DECLARE _attachments                            core.attachment_type[];
    DECLARE message                                 test_result;
BEGIN
    PERFORM unit_tests.create_mock();
    PERFORM unit_tests.sign_in_test();

    _office_id          := office.get_office_id_by_office_code('dummy-off01');
    _user_id            := office.get_user_id_by_user_name('plpgunit-test-user-000001');
    _login_id           := office.get_login_id(_user_id);
    _value_date         := transactions.get_value_date(_office_id);
    _cost_center_id     := office.get_cost_center_id_by_cost_center_code('dummy-cs01');
    _party_code         := 'dummy-pr01';
    _price_type_id      := core.get_price_type_id_by_price_type_code('dummy-pt01');
    _shipper_id         := core.get_shipper_id_by_shipper_code('dummy-sh01');
    _store_id           := office.get_store_id_by_store_code('dummy-st01');

    
    _details            := ARRAY[
                             ROW(_store_id, 'dummy-it01', 1, 'Test Mock Unit',180000, 0, 0, '', 0)::transactions.stock_detail_type,
                             ROW(_store_id, 'dummy-it02', 2, 'Test Mock Unit',130000, 300, 0, '', 0)::transactions.stock_detail_type];

    PERFORM unit_tests.create_dummy_auto_verification_policy(office.get_user_id_by_user_name('plpgunit-test-user-000001'), _office_id, true, 0, true, 0, true, 0, '1-1-2000', '1-1-2020', true);

    SELECT * FROM transactions.post_purchase
    (
        _book_name,_office_id, _user_id, _login_id, _value_date, _cost_center_id, _reference_number, _statement_reference,
        _is_credit, _party_code, _price_type_id, _shipper_id,
        _store_id, _tran_ids, _details, _attachments
    ) INTO _tran_id;


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
    DECLARE _value_date                             date;
    DECLARE _tran_id                                bigint;
    DECLARE _verification_status_id                 smallint;
    DECLARE _book_name                              national character varying(48)='Purchase.Direct';
    DECLARE _office_id                              integer;
    DECLARE _user_id                                integer;
    DECLARE _login_id                               bigint;
    DECLARE _cost_center_id                         integer;
    DECLARE _reference_number                       national character varying(24)='Plpgunit.fixture';
    DECLARE _statement_reference                    text='Plpgunit test was here.';
    DECLARE _is_credit                              boolean=false;
    DECLARE _payment_term_id                        integer;
    DECLARE _party_code                             national character varying(12);
    DECLARE _price_type_id                          integer;
    DECLARE _shipper_id                             integer;
    DECLARE _store_id                               integer;
    DECLARE _is_non_taxable_sales                   boolean=true;
    DECLARE _tran_ids                               bigint[];
    DECLARE _details                                transactions.stock_detail_type[];
    DECLARE _attachments                            core.attachment_type[];
    DECLARE message                                 test_result;
BEGIN
    PERFORM unit_tests.create_mock();
    PERFORM unit_tests.sign_in_test();

    _office_id          := office.get_office_id_by_office_code('dummy-off01');
    _user_id            := office.get_user_id_by_user_name('plpgunit-test-user-000001');
    _login_id           := office.get_login_id(_user_id);
    _value_date         := transactions.get_value_date(_office_id);
    _cost_center_id     := office.get_cost_center_id_by_cost_center_code('dummy-cs01');
    _party_code         := 'dummy-pr01';
    _price_type_id      := core.get_price_type_id_by_price_type_code('dummy-pt01');
    _shipper_id         := core.get_shipper_id_by_shipper_code('dummy-sh01');
    _store_id           := office.get_store_id_by_store_code('dummy-st01');

    
    _details            := ARRAY[
                             ROW(_store_id, 'dummy-it01', 1, 'Test Mock Unit',180000, 0, 0, '', 0)::transactions.stock_detail_type,
                             ROW(_store_id, 'dummy-it02', 2, 'Test Mock Unit',130000, 300, 0, '', 0)::transactions.stock_detail_type];

    PERFORM unit_tests.create_dummy_auto_verification_policy(office.get_user_id_by_user_name('plpgunit-test-user-000001'), _office_id, true, 0, true, 100, true, 0, '1-1-2000', '1-1-2000', true);


    SELECT * FROM transactions.post_purchase
    (
        _book_name,_office_id, _user_id, _login_id, _value_date, _cost_center_id, _reference_number, _statement_reference,
        _is_credit, _party_code, _price_type_id, _shipper_id,
        _store_id, _tran_ids, _details, _attachments
    ) INTO _tran_id;


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
    DECLARE _value_date                             date;
    DECLARE _office_id                              integer;
    DECLARE _user_id                                integer;
    DECLARE _login_id                               bigint;
    DECLARE _tran_id                                bigint;
    DECLARE _verification_status_id                 smallint;
    DECLARE message                                 test_result;
BEGIN
    PERFORM unit_tests.create_mock();
    PERFORM unit_tests.sign_in_test();

    _office_id          := office.get_office_id_by_office_code('dummy-off01');
    _value_date         := transactions.get_value_date(_office_id);
    _user_id            := office.get_user_id_by_user_name('plpgunit-test-user-000001');
    _login_id           := office.get_login_id(_user_id);

    PERFORM unit_tests.create_dummy_auto_verification_policy(office.get_user_id_by_user_name('plpgunit-test-user-000001'), _office_id, true, 0, true, 0, true, 0, '1-1-2000', '1-1-2020', true);

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
        transactions.get_transaction_code(_value_date, _office_id, _user_id, 1),
        'Journal',
        _value_date,
        _user_id,
        _login_id,
        _office_id,
        'REF# TEST',
        'Thou art not able to see this.';



    INSERT INTO transactions.transaction_details
    (
        transaction_master_id, 
        value_date,
        tran_type, 
        account_id, 
        statement_reference, 
        currency_code, 
        amount_in_currency, 
        local_currency_code,    
        er, 
        amount_in_local_currency
    )

    SELECT _tran_id, _value_date, 'Cr', core.get_account_id_by_account_number('dummy-acc01'), '', 'NPR', 12000, 'NPR', 1, 12000 UNION ALL
    SELECT _tran_id, _value_date, 'Dr', core.get_account_id_by_account_number('dummy-acc02'), '', 'NPR', 3000, 'NPR', 1, 3000 UNION ALL
    SELECT _tran_id, _value_date, 'Dr', core.get_account_id_by_account_number('dummy-acc03'), '', 'NPR', 9000, 'NPR', 1, 9000;


    PERFORM transactions.auto_verify(currval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id')), office.get_office_id_by_office_code('dummy-off01'));

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
    DECLARE _value_date                             date;
    DECLARE _office_id                              integer;
    DECLARE _user_id                                integer;
    DECLARE _login_id                               bigint;
    DECLARE _tran_id                                bigint;
    DECLARE _verification_status_id                 smallint;
    DECLARE message                                 test_result;
BEGIN
    PERFORM unit_tests.create_mock();
    PERFORM unit_tests.sign_in_test();

    _office_id          := office.get_office_id_by_office_code('dummy-off01');
    _value_date         := transactions.get_value_date(_office_id);
    _user_id            := office.get_user_id_by_user_name('plpgunit-test-user-000001');
    _login_id           := office.get_login_id(_user_id);

     PERFORM unit_tests.create_dummy_auto_verification_policy(office.get_user_id_by_user_name('plpgunit-test-user-000001'), _office_id, true, 0, true, 0, true, 100, '1-1-2000', '1-1-2020', true);
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
        transactions.get_transaction_code(_value_date, _office_id, _user_id, 1),
        'Journal',
        _value_date,
        _user_id,
        _login_id,
        _office_id,
        'REF# TEST',
        'Thou art not able to see this.';



    INSERT INTO transactions.transaction_details
    (
        transaction_master_id,
        value_date,
        tran_type, 
        account_id, 
        statement_reference, 
        currency_code, 
        amount_in_currency, 
        local_currency_code,    
        er, 
        amount_in_local_currency
    )
    SELECT _tran_id, _value_date, 'Cr', core.get_account_id_by_account_number('dummy-acc01'), '', 'NPR', 12000, 'NPR', 1, 12000 UNION ALL
    SELECT _tran_id, _value_date, 'Dr', core.get_account_id_by_account_number('dummy-acc02'), '', 'NPR', 3000, 'NPR', 1, 3000 UNION ALL
    SELECT _tran_id, _value_date, 'Dr', core.get_account_id_by_account_number('dummy-acc03'), '', 'NPR', 9000, 'NPR', 1, 9000;


    PERFORM transactions.auto_verify(currval(pg_get_serial_sequence('transactions.transaction_master', 'transaction_master_id')), office.get_office_id_by_office_code('dummy-off01'));

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