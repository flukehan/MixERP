-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/01.types-domains-tables-and-constraints/tables-and-constraints.sql --<--<--
CREATE TABLE policy.http_actions
(
    http_action_code                text NOT NULL PRIMARY KEY
);

CREATE UNIQUE INDEX policy_http_action_code_uix
ON policy.http_actions(UPPER(http_action_code));

CREATE TABLE policy.api_access_policy
(
    api_access_policy_id            BIGSERIAL NOT NULL PRIMARY KEY,
    user_id                         integer NOT NULL REFERENCES office.users(user_id),
    office_id                       integer NOT NULL REFERENCES office.offices(office_id),
    poco_type_name                  text NOT NULL,
    http_action_code                text NOT NULL REFERENCES policy.http_actions(http_action_code),
    valid_till                      date NOT NULL,
    audit_user_id                   integer NULL REFERENCES office.users(user_id),
    audit_ts                        TIMESTAMP WITH TIME ZONE NULL 
                                    DEFAULT(NOW())    
);

CREATE UNIQUE INDEX api_access_policy_uix
ON policy.api_access_policy(user_id, poco_type_name, http_action_code, valid_till);

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/audit/audit.get_office_id_by_login_id.sql --<--<--
DROP FUNCTION IF EXISTS audit.get_office_id_by_login_id(bigint);

CREATE FUNCTION audit.get_office_id_by_login_id(bigint)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN office_id
    FROM audit.logins
    WHERE login_id=$1;
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/audit/audit.get_user_id_by_login_id.sql --<--<--
DROP FUNCTION IF EXISTS audit.get_user_id_by_login_id(bigint);

CREATE FUNCTION audit.get_user_id_by_login_id(bigint)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN user_id
    FROM audit.logins
    WHERE login_id=$1;
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/transactions/transactions.get_cash_repository_balance.sql --<--<--
DROP FUNCTION IF EXISTS transactions.get_cash_repository_balance(_cash_repository_id integer, _currency_code national character varying(12));
CREATE FUNCTION transactions.get_cash_repository_balance(_cash_repository_id integer, _currency_code national character varying(12))
RETURNS public.money_strict2
AS
$$
    DECLARE _debit public.money_strict2;
    DECLARE _credit public.money_strict2;
BEGIN
    SELECT COALESCE(SUM(amount_in_currency), 0::public.money_strict2) INTO _debit
    FROM transactions.verified_transaction_view
    WHERE cash_repository_id=$1
    AND currency_code=$2
    AND tran_type='Dr';

    SELECT COALESCE(SUM(amount_in_currency), 0::public.money_strict2) INTO _credit
    FROM transactions.verified_transaction_view
    WHERE cash_repository_id=$1
    AND currency_code=$2
    AND tran_type='Cr';

    RETURN _debit - _credit;
END
$$
LANGUAGE plpgsql;


DROP FUNCTION IF EXISTS transactions.get_cash_repository_balance(_cash_repository_id integer);
CREATE FUNCTION transactions.get_cash_repository_balance(_cash_repository_id integer)
RETURNS public.money_strict2
AS
$$
    DECLARE _local_currency_code national character varying(12) = transactions.get_default_currency_code($1);
    DECLARE _debit public.money_strict2;
    DECLARE _credit public.money_strict2;
BEGIN
    SELECT COALESCE(SUM(amount_in_currency), 0::public.money_strict2) INTO _debit
    FROM transactions.verified_transaction_view
    WHERE cash_repository_id=$1
    AND currency_code=_local_currency_code
    AND tran_type='Dr';

    SELECT COALESCE(SUM(amount_in_currency), 0::public.money_strict2) INTO _credit
    FROM transactions.verified_transaction_view
    WHERE cash_repository_id=$1
    AND currency_code=_local_currency_code
    AND tran_type='Cr';

    RETURN _debit - _credit;
END
$$
LANGUAGE plpgsql;




-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/transactions/transactions.get_net_profit.sql --<--<--
DROP FUNCTION IF EXISTS transactions.get_net_profit
(
    _date_from                      date,
    _date_to                        date,
    _office_id                      integer,
    _factor                         integer,
    _no_provison                    boolean
);

CREATE FUNCTION transactions.get_net_profit
(
    _date_from                      date,
    _date_to                        date,
    _office_id                      integer,
    _factor                         integer,
    _no_provison                    boolean DEFAULT false
)
RETURNS decimal(24, 4)
AS
$$
    DECLARE _incomes                decimal(24, 4) = 0;
    DECLARE _expenses               decimal(24, 4) = 0;
    DECLARE _profit_before_tax      decimal(24, 4) = 0;
    DECLARE _tax_paid               decimal(24, 4) = 0;
    DECLARE _tax_provison           decimal(24, 4) = 0;
BEGIN
    SELECT SUM(CASE tran_type WHEN 'Cr' THEN amount_in_local_currency ELSE amount_in_local_currency * -1 END)
    INTO _incomes
    FROM transactions.verified_transaction_mat_view
    WHERE value_date >= _date_from AND value_date <= _date_to
    AND office_id IN (SELECT * FROM office.get_office_ids(_office_id))
    AND account_master_id >=20100
    AND account_master_id <= 20300;
    
    SELECT SUM(CASE tran_type WHEN 'Dr' THEN amount_in_local_currency ELSE amount_in_local_currency * -1 END)
    INTO _expenses
    FROM transactions.verified_transaction_mat_view
    WHERE value_date >= _date_from AND value_date <= _date_to
    AND office_id IN (SELECT * FROM office.get_office_ids(_office_id))
    AND account_master_id >=20400
    AND account_master_id <= 20701;
    
    SELECT SUM(CASE tran_type WHEN 'Dr' THEN amount_in_local_currency ELSE amount_in_local_currency * -1 END)
    INTO _tax_paid
    FROM transactions.verified_transaction_mat_view
    WHERE value_date >= _date_from AND value_date <= _date_to
    AND office_id IN (SELECT * FROM office.get_office_ids(_office_id))
    AND account_master_id =20800;
    
    _profit_before_tax := COALESCE(_incomes, 0) - COALESCE(_expenses, 0);

    IF(_no_provison) THEN
        RETURN (_profit_before_tax - COALESCE(_tax_paid, 0)) / _factor;
    END IF;
    
    _tax_provison      := core.get_income_tax_provison_amount(_office_id, _profit_before_tax, COALESCE(_tax_paid, 0));
    
    RETURN (_profit_before_tax - (COALESCE(_tax_provison, 0) + COALESCE(_tax_paid, 0))) / _factor;
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/triggers/transactions.verification_trigger.sql --<--<--
DROP FUNCTION IF EXISTS transactions.verification_trigger() CASCADE;
CREATE FUNCTION transactions.verification_trigger()
RETURNS TRIGGER
AS
$$
    DECLARE _transaction_master_id              bigint;
    DECLARE _transaction_posted_by              integer;
    DECLARE _old_verifier                       integer;
    DECLARE _old_status                         integer;
    DECLARE _old_reason                         national character varying(128);
    DECLARE _verifier                           integer;
    DECLARE _status                             integer;
    DECLARE _reason                             national character varying(128);
    DECLARE _has_policy                         boolean;
    DECLARE _is_sys                             boolean;
    DECLARE _rejected                           smallint=-3;
    DECLARE _closed                             smallint=-2;
    DECLARE _withdrawn                          smallint=-1;
    DECLARE _unapproved                         smallint = 0;
    DECLARE _auto_approved                      smallint = 1;
    DECLARE _approved                           smallint=2;
    DECLARE _book                               text;
    DECLARE _can_verify_sales_transactions      boolean;
    DECLARE _sales_verification_limit           public.money_strict2;
    DECLARE _can_verify_purchase_transactions   boolean;
    DECLARE _purchase_verification_limit        money_strict2;
    DECLARE _can_verify_gl_transactions         boolean;
    DECLARE _gl_verification_limit              public.money_strict2;
    DECLARE _can_verify_self                    boolean;
    DECLARE _self_verification_limit            public.money_strict2;
    DECLARE _posted_amount                      public.money_strict2;
    DECLARE _voucher_date                       date;
    DECLARE _value_date                         date;
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

        _transaction_master_id  := OLD.transaction_master_id;
        _book                   := OLD.book;
        _old_verifier           := OLD.verified_by_user_id;
        _old_status             := OLD.verification_status_id;
        _old_reason             := OLD.verification_reason;
        _transaction_posted_by  := OLD.user_id;      
        _verifier               := NEW.verified_by_user_id;
        _status                 := NEW.verification_status_id;
        _reason                 := NEW.verification_reason;
        _is_sys                 := office.is_sys(_verifier);
        _voucher_date           := NEW.value_date;
        _value_date             := transactions.get_value_date(NEW.office_id);
        
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

        IF(_voucher_date < _value_date) THEN
            RAISE EXCEPTION 'Access is denied. You don''t have the right to withdraw the transaction.'
            USING ERRCODE='P9017';
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


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/office/office.get_department_id_by_department_code.sql --<--<--
DROP FUNCTION IF EXISTS office.get_department_id_by_code(text);

DROP FUNCTION IF EXISTS office.get_department_id_by_department_code(text);

CREATE FUNCTION office.get_department_id_by_department_code(text)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN department_id
    FROM office.departments
    WHERE department_code=$1;
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/04.default-values/http-actions.sql --<--<--
--This table should not be localized.
INSERT INTO policy.http_actions
SELECT 'GET' UNION ALL
SELECT 'PUT' UNION ALL
SELECT 'POST' UNION ALL
SELECT 'DELETE';


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/06.sample-data/0.menus.sql --<--<--
INSERT INTO core.menus(menu_text, url, menu_code, level, parent_menu_id)
SELECT 'API Access Policy', '~/Modules/BackOffice/Policy/ApiAccessPolicy.mix', 'SAA', 2, core.get_menu_id('SPM');

--FRENCH
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'fr', 'API Stratégie d''accès';


--GERMAN
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'de', 'API-Richtlinien';

--RUSSIAN
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'ru', 'Политика доступа API';

--JAPANESE
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'ja', 'APIのアクセスポリシー';

--SPANISH
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'es', 'API Política de Acceso';

--DUTCH
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'nl', 'API Access Policy';

--SIMPLIFIED CHINESE
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'zh', 'API访问策略';

--PORTUGUESE
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'pt', 'Política de Acesso API';

--SWEDISH
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'sv', 'API Access Policy';

--MALAY
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'ms', 'Dasar Akses API';

--INDONESIAN
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'id', 'API Kebijakan Access';

--FILIPINO
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'fil', 'API Patakaran sa Pag-access';

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests/transactions/unit_tests.balance_sheet_test.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.balance_sheet_test();

--Todo--check balance sheet by office
CREATE FUNCTION unit_tests.balance_sheet_test()
RETURNS public.test_result
AS
$$
    DECLARE _period_from    date;
    DECLARE _period_to      date;
    DECLARE _counter        date;
    DECLARE _bs_unequal     bool=false;
    DECLARE _message        test_result;
    DECLARE _difference     numeric;
    DECLARE _amount         numeric;
    DECLARE _office_id      integer;
    DECLARE _user_id        integer;
BEGIN
    SELECT office_id INTO _office_id
    FROM office.offices
    ORDER BY office_id
    LIMIT 1;

    SELECT user_id INTO _user_id
    FROM office.users
    LIMIT 1;
    
    SELECT 
        min(value_date), 
        max(value_date)
    INTO
        _period_from,
        _period_to
    FROM transactions.transaction_master;


    SELECT 
    SUM
    (
        CASE WHEN item='Assets' 
        THEN current_period * -1 
        ELSE current_period END
    )
    INTO
        _difference            
    FROM transactions.get_balance_sheet(_period_from, _period_to, _user_id, _office_id, 1)
    WHERE item IN ('Assets', 'Liabilities & Shareholders'' Equity');

    IF(_difference) <> 0 THEN
        _bs_unequal := true;
        _counter    := _period_to;
    END IF;


    WHILE _bs_unequal
    LOOP
        SELECT 
        SUM
        (
            CASE WHEN item='Assets' 
            THEN current_period * -1 
            ELSE current_period END
        )
        INTO
            _amount            
        FROM transactions.get_balance_sheet('7/17/2014', _counter, _user_id, _office_id, 1)
        WHERE item IN ('Assets', 'Liabilities & Shareholders'' Equity');
                
        IF(COALESCE(_amount, 0) = 0) THEN
            _bs_unequal := false;
            _message    := 'Balance sheet unequal on date: ' || (_counter + 1)::text || '. Difference in base currency : ' || _difference::text || '.';
            
            PERFORM assert.fail(_message);
            RETURN _message;
         END IF;

        _counter := _counter - 1;
     END LOOP;

    SELECT assert.ok('End of test.') INTO _message;  
    RETURN _message;
END
$$
LANGUAGE plpgsql;


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests/unit_tests.create_mock.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_mock();

CREATE FUNCTION unit_tests.create_mock()
RETURNS void
AS
$$
BEGIN
    PERFORM unit_tests.create_dummy_offices();
    PERFORM unit_tests.create_dummy_departments();
    PERFORM unit_tests.create_dummy_users();
    PERFORM unit_tests.create_dummy_accounts();
    PERFORM unit_tests.create_dummy_sales_taxes();
    PERFORM unit_tests.create_dummy_countries();
    PERFORM unit_tests.create_dummy_states();
    PERFORM unit_tests.create_dummy_party_types();
    PERFORM unit_tests.create_dummy_item_groups();
    PERFORM unit_tests.create_dummy_item_types();
    PERFORM unit_tests.create_dummy_units();
    PERFORM unit_tests.create_dummy_brands();
    PERFORM unit_tests.create_dummy_parties();
    PERFORM unit_tests.create_dummy_items();
    PERFORM unit_tests.create_dummy_cost_centers();
    PERFORM unit_tests.create_dummy_late_fees();
    PERFORM unit_tests.create_dummy_sales_teams();
    PERFORM unit_tests.create_dummy_salespersons();
    PERFORM unit_tests.create_dummy_shippers();
    PERFORM unit_tests.create_dummy_cash_repositories();
    PERFORM unit_tests.create_dummy_store_types();
    PERFORM unit_tests.create_dummy_stores();
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_departments.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_departments();

CREATE FUNCTION unit_tests.create_dummy_departments()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM office.departments WHERE department_code='dummy-dp01') THEN        
        INSERT INTO office.departments(department_code, department_name)
        SELECT 'dummy-dp01', 'Test Mock Department';
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/12.plpgunit-tests-mock/unit_tests.create_dummy_users.sql --<--<--
DROP FUNCTION IF EXISTS unit_tests.create_dummy_users();

CREATE FUNCTION unit_tests.create_dummy_users()
RETURNS void 
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM office.users WHERE user_name='plpgunit-test-user-000001') THEN
        INSERT INTO office.users(role_id, department_id, user_name, full_name, password, office_id)
        SELECT office.get_role_id_by_role_code('USER'), office.get_department_id_by_department_code('dummy-dp01'), 'plpgunit-test-user-000001', 'PLPGUnit Test User', 'thoushaltnotlogin', office.get_office_id_by_office_code('dummy-off01');
    END IF;
END
$$
LANGUAGE plpgsql;


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/refresh-materialized-views.sql --<--<--
SELECT * FROM transactions.refresh_materialized_views(1);
