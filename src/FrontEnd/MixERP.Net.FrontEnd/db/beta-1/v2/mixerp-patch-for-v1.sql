-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/01.types-domains-tables-and-constraints/tables-and-constraints.sql --<--<--
DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1 
        FROM   pg_catalog.pg_class c
        JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
        WHERE  n.nspname = 'policy'
        AND    c.relname = 'http_actions'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE policy.http_actions
        (
            http_action_code                text NOT NULL PRIMARY KEY
        );

        CREATE UNIQUE INDEX policy_http_action_code_uix
        ON policy.http_actions(UPPER(http_action_code));    
    END IF;    
END
$$
LANGUAGE plpgsql;

DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1 
        FROM   pg_catalog.pg_class c
        JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
        WHERE  n.nspname = 'policy'
        AND    c.relname = 'api_access_policy'
        AND    c.relkind = 'r'
    ) THEN
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
    
    END IF;    
END
$$
LANGUAGE plpgsql;

DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1 
        FROM   pg_catalog.pg_class c
        JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
        WHERE  n.nspname = 'core'
        AND    c.relname = 'recurrence_types'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE core.recurrence_types
        (
            recurrence_type_id              SERIAL NOT NULL PRIMARY KEY,
            recurrence_type_code            national character varying(12) NOT NULL,
            recurrence_type_name            national character varying(50) NOT NULL,
            is_frequency                    boolean NOT NULL,
            audit_user_id                   integer NULL REFERENCES office.users(user_id),
            audit_ts                        TIMESTAMP WITH TIME ZONE NULL 
                                            DEFAULT(NOW())            
        );
    END IF;    
END
$$
LANGUAGE plpgsql;




DROP TABLE IF EXISTS core.recurring_invoices_temp;
DROP TABLE IF EXISTS core.recurring_invoice_setup_temp;

CREATE TABLE core.recurring_invoices_temp
AS
SELECT * FROM core.recurring_invoices;

CREATE TABLE core.recurring_invoice_setup_temp
AS
SELECT * FROM core.recurring_invoice_setup;


DROP TABLE IF EXISTS core.recurring_invoices CASCADE;

CREATE TABLE core.recurring_invoices
(
    recurring_invoice_id                        SERIAL NOT NULL PRIMARY KEY,
    recurring_invoice_code                      national character varying(12) NOT NULL,
    recurring_invoice_name                      national character varying(50) NOT NULL,
    item_id                                     integer NULL REFERENCES core.items(item_id),
    compound_item_id                            integer NULL REFERENCES core.compound_items(compound_item_id)
                                                CONSTRAINT recurring_invoices_item_chk
                                                CHECK
                                                (
                                                    (item_id IS NULL)::integer + (compound_item_id IS NULL)::integer = 1 --Only one of these two can be NOT NULL.
                                                ),
    recurrence_type_id                          integer REFERENCES core.recurrence_types(recurrence_type_id),
    recurring_frequency_id                      integer NOT NULL REFERENCES core.frequencies(frequency_id),
    recurring_duration                          public.integer_strict2 NOT NULL DEFAULT(0),
    recurs_on_same_calendar_date                boolean NOT NULL DEFAULT(true),
    recurring_amount                            money_strict NOT NULL 
                                                CONSTRAINT recurring_invoices_recurring_amount_chk 
                                                CHECK(recurring_amount > 0),
    auto_trigger_on_sales                       boolean NOT NULL,
    audit_user_id                               integer NULL REFERENCES office.users(user_id),
    audit_ts                                    TIMESTAMP WITH TIME ZONE NULL 
                                                DEFAULT(NOW())    
);

CREATE UNIQUE INDEX recurring_invoices_item_id_auto_trigger_on_sales_uix
ON core.recurring_invoices(item_id, auto_trigger_on_sales)
WHERE auto_trigger_on_sales = true;

CREATE UNIQUE INDEX recurring_invoices_compound_item_id_auto_trigger_on_sales_uix
ON core.recurring_invoices(compound_item_id, auto_trigger_on_sales)
WHERE auto_trigger_on_sales = true;


DROP TABLE IF EXISTS core.recurring_invoice_setup CASCADE;
CREATE TABLE core.recurring_invoice_setup
(
    recurring_invoice_setup_id                  SERIAL NOT NULL PRIMARY KEY,
    recurring_invoice_id                        integer NOT NULL REFERENCES core.recurring_invoices(recurring_invoice_id),
    party_id                                    bigint NOT NULL REFERENCES core.parties(party_id),
    starts_from                                 date NOT NULL,
    ends_on                                     date NOT NULL
                                                CONSTRAINT recurring_invoice_setup_date_chk
                                                CHECK
                                                (
                                                    ends_on >= starts_from
                                                ),
    recurrence_type_id                          integer NOT NULL REFERENCES core.recurrence_types(recurrence_type_id),
    recurring_frequency_id                      integer NULL REFERENCES core.frequencies(frequency_id),
    recurring_duration                          public.integer_strict2 NOT NULL DEFAULT(0),
    recurs_on_same_calendar_date                boolean NOT NULL DEFAULT(true),
    recurring_amount                            money_strict NOT NULL 
                                                CONSTRAINT recurring_invoice_setup_recurring_amount_chk
                                                CHECK(recurring_amount > 0),
    payment_term_id                             integer NOT NULL REFERENCES core.payment_terms(payment_term_id),
    is_active                                   boolean NOT NULL DEFAULT(true),
    audit_user_id                               integer NULL REFERENCES office.users(user_id),
    audit_ts                                    TIMESTAMP WITH TIME ZONE NULL 
                                                DEFAULT(NOW())    
    
);


INSERT INTO core.recurring_invoices
(
    recurring_invoice_id, 
    recurring_invoice_code, 
    recurring_invoice_name, 
    item_id, 
    compound_item_id, 
    recurring_frequency_id, 
    recurring_amount,
    auto_trigger_on_sales,
    audit_user_id,
    audit_ts
)
SELECT
    recurring_invoice_id, 
    recurring_invoice_code, 
    recurring_invoice_name, 
    item_id, 
    compound_item_id, 
    recurring_frequency_id, 
    recurring_amount,
    auto_trigger_on_sales,
    audit_user_id,
    audit_ts
FROM core.recurring_invoices_temp;

SELECT setval
(
    pg_get_serial_sequence('core.recurring_invoices', 'recurring_invoice_id'), 
    (SELECT MAX(recurring_invoice_id) FROM core.recurring_invoices) + 1
); 


INSERT INTO core.recurring_invoice_setup
(
    recurring_invoice_setup_id,
    recurring_invoice_id,
    party_id,
    starts_from,
    ends_on,
    recurring_amount,
    payment_term_id,
    audit_user_id,
    audit_ts
)
SELECT
    recurring_invoice_setup_id,
    recurring_invoice_id,
    party_id,
    starts_from,
    ends_on,
    recurring_amount,
    payment_term_id,
    audit_user_id,
    audit_ts
FROM core.recurring_invoice_setup_temp;


SELECT setval
(
    pg_get_serial_sequence('core.recurring_invoice_setup', 'recurring_invoice_setup_id'), 
    (SELECT MAX(recurring_invoice_setup_id) FROM core.recurring_invoice_setup) + 1
); 

DROP TABLE IF EXISTS core.recurring_invoices_temp;
DROP TABLE IF EXISTS core.recurring_invoice_setup_temp;


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

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/core/core.create_menu.sql --<--<--
DROP FUNCTION IF EXISTS core.create_menu
(
    _menu_text          text,
    _url                text,
    _menu_code          text,
    _level              integer,
    _parent_menu_id     integer
);

CREATE FUNCTION core.create_menu
(
    _menu_text          text,
    _url                text,
    _menu_code          text,
    _level              integer,
    _parent_menu_id     integer
)
RETURNS void
VOLATILE
AS
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT * FROM core.menus
        WHERE menu_code = _menu_code
    ) THEN
        INSERT INTO core.menus(menu_text, url, menu_code, level, parent_menu_id)
        SELECT _menu_text, _url, _menu_code, _level, _parent_menu_id;
    END IF;

    UPDATE core.menus
    SET 
        menu_text       = _menu_text, 
        url             = _url, 
        level           = _level,
        parent_menu_id  = _parent_menu_id
    WHERE menu_code=_menu_code;    
END
$$
LANGUAGE plpgsql;


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/core/core.create_menu_locale.sql --<--<--
DROP FUNCTION IF EXISTS core.create_menu_locale
(
    _menu_id            integer,
    _culture            text,
    _menu_text          text
);

CREATE FUNCTION core.create_menu_locale
(
    _menu_id            integer,
    _culture            text,
    _menu_text          text
)
RETURNS void
VOLATILE
AS
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT * FROM core.menu_locale
        WHERE menu_id = _menu_id
        AND culture = _culture
    ) THEN
        INSERT INTO core.menu_locale(menu_id, culture, menu_text)
        SELECT _menu_id, _culture, _menu_text;
    END IF;

    UPDATE core.menu_locale
    SET
        menu_text       = _menu_text
    WHERE menu_id = _menu_id
    AND culture = _culture;
END
$$
LANGUAGE plpgsql;


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/core/core.get_menu_id_by_menu_code.sql --<--<--
DROP FUNCTION IF EXISTS core.get_menu_id_by_menu_code(national character varying(250));

CREATE FUNCTION core.get_menu_id_by_menu_code(national character varying(250))
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN menu_id
    FROM core.menus
    WHERE menu_code=$1;
END
$$
LANGUAGE plpgsql;


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/core/core.get_recurrence_type_id_by_recurrence_type_code.sql --<--<--
DROP FUNCTION IF EXISTS core.get_recurrence_type_id_by_recurrence_type_code(text);

CREATE FUNCTION core.get_recurrence_type_id_by_recurrence_type_code(text)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN recurrence_type_id
    FROM core.recurrence_types
    WHERE recurrence_type_code = $1;
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/public/public.add_column.sql --<--<--
DROP FUNCTION IF EXISTS public.add_column(regclass, text, regtype, text, text);

CREATE FUNCTION public.add_column
(
    _table_name     regclass, 
    _column_name    text, 
    _data_type      regtype,
    _default        text = '',
    _comment        text = ''
)
RETURNS void
VOLATILE
AS
$$
BEGIN
    IF NOT EXISTS (
       SELECT 1 FROM pg_attribute
       WHERE  attrelid = _table_name
       AND    attname = _column_name
       AND    NOT attisdropped) THEN
        EXECUTE '
        ALTER TABLE ' || _table_name || ' ADD COLUMN ' || quote_ident(_column_name) || ' ' || _data_type;
    END IF;

    IF(COALESCE(_default, '') != '') THEN
        EXECUTE '
        ALTER TABLE ' || _table_name || ' ALTER COLUMN ' || _column_name || ' SET DEFAULT ' || _default;
    END IF;

    IF(COALESCE(_comment, '') != '') THEN
        EXECUTE '
        COMMENT ON COLUMN ' || _table_name || '.' || _column_name || ' IS ''' || _comment || ''''; 
    END IF;    
END
$$
LANGUAGE plpgsql;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/02.functions-and-logic/logic/transactions/transactions.auto_verify.sql --<--<--
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
    DECLARE _voucher_date date;
    DECLARE _value_date date=transactions.get_value_date(_office_id);
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
    DECLARE _value_date                             date;
    DECLARE _tran_id                                bigint;
    DECLARE _verification_status_id                 smallint;
    DECLARE _book_name                              national character varying(12)='Sales.Direct';
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
             
    
    PERFORM unit_tests.create_dummy_auto_verification_policy(office.get_user_id_by_user_name('plpgunit-test-user-000001'), true, 0, true, 0, true, 0, '1-1-2000', '1-1-2020', true);


    SELECT * FROM transactions.post_sales
    (
        _book_name,_office_id, _user_id, _login_id, _value_date, _cost_center_id, _reference_number, _statement_reference,
        _is_credit, _payment_term_id, _party_code, _price_type_id, _salesperson_id, _shipper_id,
        _shipping_address_code,
        _store_id,
        _is_non_taxable_sales,
        _details,
        _attachments
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
    DECLARE _book_name                              national character varying(12)='Sales.Direct';
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

    PERFORM unit_tests.create_dummy_auto_verification_policy(office.get_user_id_by_user_name('plpgunit-test-user-000001'), true, 100, true, 0, true, 0, '1-1-2000', '1-1-2020', true);

    SELECT * FROM transactions.post_sales
    (
        _book_name,_office_id, _user_id, _login_id, _value_date, _cost_center_id, _reference_number, _statement_reference,
        _is_credit, _payment_term_id, _party_code, _price_type_id, _salesperson_id, _shipper_id,
        _shipping_address_code,
        _store_id,
        _is_non_taxable_sales,
        _details,
        _attachments
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
    DECLARE _book_name                              national character varying(12)='Sales.Direct';
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

    PERFORM unit_tests.create_dummy_auto_verification_policy(office.get_user_id_by_user_name('plpgunit-test-user-000001'), true, 0, true, 0, true, 0, '1-1-2000', '1-1-2020', true);

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
    DECLARE _book_name                              national character varying(12)='Sales.Direct';
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

    PERFORM unit_tests.create_dummy_auto_verification_policy(office.get_user_id_by_user_name('plpgunit-test-user-000001'), true, 0, true, 100, true, 0, '1-1-2000', '1-1-2000', true);


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
WITH http_actions
AS
(
SELECT 'GET' as code UNION ALL
SELECT 'PUT' UNION ALL
SELECT 'POST' UNION ALL
SELECT 'DELETE'
)

INSERT INTO policy.http_actions
SELECT * FROM http_actions
WHERE code NOT IN
(
    SELECT http_action_code FROM policy.http_actions
    WHERE http_action_code IN('GET','PUT','POST','DELETE')
);


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/04.default-values/recurrence-types.sql --<--<--
WITH recurrence_types
AS
(
SELECT 'FRE' AS code,   'Frequency' AS name,    true AS is_freq UNION ALL
SELECT 'DUR',           'Duration',             false
)

INSERT INTO core.recurrence_types(recurrence_type_code, recurrence_type_name, is_frequency)
SELECT * FROM recurrence_types
WHERE code NOT IN
(
    SELECT recurrence_type_code FROM core.recurrence_types
    WHERE recurrence_type_code IN('FRE','DUR')
);

UPDATE core.recurring_invoices 
SET recurrence_type_id = core.get_recurrence_type_id_by_recurrence_type_code('FRE')
WHERE recurrence_type_id IS NULL;

ALTER TABLE core.recurring_invoices
ALTER COLUMN recurrence_type_id SET NOT NULL;

ALTER TABLE core.recurring_invoices
ALTER COLUMN recurring_frequency_id DROP NOT NULL;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/05.scrud-views/core/core.recurring_invoice_scrud_view.sql --<--<--
CREATE VIEW core.recurring_invoice_scrud_view
AS
SELECT 
  core.recurring_invoices.recurring_invoice_id, 
  core.recurring_invoices.recurring_invoice_code, 
  core.recurring_invoices.recurring_invoice_name,
  core.items.item_code || '('|| core.items.item_name||')' AS item,
  core.compound_items.compound_item_code || ' (' || core.compound_items.compound_item_name || ')' AS compound_item,
  core.frequencies.frequency_code || '('|| core.frequencies.frequency_name||')' AS recurring_frequency,
  core.recurring_invoices.recurring_amount, 
  core.recurring_invoices.auto_trigger_on_sales
FROM 
  core.recurring_invoices
LEFT JOIN core.items 
ON core.recurring_invoices.item_id = core.items.item_id
INNER JOIN core.frequencies
ON core.recurring_invoices.recurring_frequency_id = core.frequencies.frequency_id
LEFT JOIN core.compound_items
ON core.recurring_invoices.compound_item_id=core.compound_items.compound_item_id;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/05.scrud-views/core/core.recurring_invoice_setup_scrud_view.sql --<--<--
DROP VIEW IF EXISTS core.recurring_invoice_setup_scrud_view;
CREATE VIEW core.recurring_invoice_setup_scrud_view
AS
SELECT 
  core.recurring_invoice_setup.recurring_invoice_setup_id, 
  core.recurring_invoices.recurring_invoice_code || ' (' || core.recurring_invoices.recurring_invoice_name || ')' AS recurring_invoice,
  core.parties.party_code || ' (' || core.parties.party_name || ')' AS party,
  core.recurring_invoice_setup.starts_from, 
  core.recurring_invoice_setup.ends_on, 
  core.recurring_invoice_setup.recurring_amount, 
  core.payment_terms.payment_term_code || ' (' || core.payment_terms.payment_term_name || ')' AS payment_term
FROM 
  core.recurring_invoice_setup
INNER JOIN core.recurring_invoices
ON core.recurring_invoice_setup.recurring_invoice_id = core.recurring_invoices.recurring_invoice_id
INNER JOIN core.parties ON 
core.recurring_invoice_setup.party_id = core.parties.party_id
INNER JOIN core.payment_terms ON 
core.recurring_invoice_setup.payment_term_id = core.payment_terms.payment_term_id;

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/06.sample-data/0.menus.sql --<--<--
SELECT * FROM core.create_menu('API Access Policy', '~/Modules/BackOffice/Policy/ApiAccess.mix', 'SAA', 2, core.get_menu_id('SPM'));

--FRENCH
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'fr', 'API Stratégie d''accès');


--GERMAN
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'de', 'API-Richtlinien');

--RUSSIAN
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'ru', 'Политика доступа API');

--JAPANESE
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'ja', 'APIのアクセスポリシー');

--SPANISH
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'es', 'API Política de Acceso');

--DUTCH
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'nl', 'API Access Policy');

--SIMPLIFIED CHINESE
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'zh', 'API访问策略');

--PORTUGUESE
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'pt', 'Política de Acesso API');

--SWEDISH
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'sv', 'API Access Policy');

--MALAY
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'ms', 'Dasar Akses API');

--INDONESIAN
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'id', 'API Kebijakan Access');

--FILIPINO
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'fil', 'API Patakaran sa Pag-access');

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/src/06.sample-data/1.remove-obsolete-menus.sql --<--<--
DELETE FROM core.menu_locale
WHERE menu_id = core.get_menu_id_by_menu_code('TRA');

DELETE FROM policy.menu_access
WHERE menu_id = core.get_menu_id_by_menu_code('TRA');

DELETE FROM core.menus
WHERE menu_code = 'TRA';


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
