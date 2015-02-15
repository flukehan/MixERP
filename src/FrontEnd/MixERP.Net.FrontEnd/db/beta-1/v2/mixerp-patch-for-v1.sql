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
