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
    DECLARE _message        public.test_result;
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
