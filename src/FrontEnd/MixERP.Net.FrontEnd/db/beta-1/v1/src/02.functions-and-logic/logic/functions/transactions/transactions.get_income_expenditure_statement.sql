DROP FUNCTION IF EXISTS transactions.get_income_expenditure_statement
(
    _date_from              date,
    _date_to                date,
    _user_id                integer,
    _office_id              integer,
    _compact                boolean
);

CREATE FUNCTION transactions.get_income_expenditure_statement
(
    _date_from              date,
    _date_to                date,
    _user_id                integer,
    _office_id              integer,
    _compact                boolean
)
RETURNS TABLE
(
    id                      integer,
    account_id              integer,
    account_number          text,
    account                 text,
    previous_debit          decimal(24, 4),
    previous_credit         decimal(24, 4),
    previous_balance        decimal(24, 4),
    debit                   decimal(24, 4),
    credit                  decimal(24, 4),
    balance                 decimal(24, 4),
    closing_debit           decimal(24, 4),
    closing_credit          decimal(24, 4),
    closing_balance         decimal(24, 4)
)
AS
$$
    DECLARE _account_master_id  integer;
BEGIN
    IF(_date_from = 'infinity') THEN
        RAISE EXCEPTION '%', 'Invalid date.'
        USING ERRCODE='P3008';
    END IF;

    IF NOT EXISTS
    (
        SELECT 0 FROM office.offices
        WHERE office_id IN 
        (
            SELECT * FROM office.get_office_ids(1)
        )
        HAVING count(DISTINCT currency_code) = 1
   ) THEN
        RAISE EXCEPTION 'Cannot produce P/L statement of office(s) having different base currencies.'
        USING ERRCODE='P8001';
   END IF;

   SELECT 
    account_master_id 
   INTO 
    _account_master_id
   FROM core.account_masters
   WHERE core.account_masters.account_master_code = 'PLA';


    DROP TABLE IF EXISTS temp_income_expenditure_statement;
    CREATE TEMPORARY TABLE temp_income_expenditure_statement
    (
        id                      integer,
        account_id              integer,
        account_number          text,
        account                 text,
        previous_debit          decimal(24, 4) DEFAULT(0),
        previous_credit         decimal(24, 4) DEFAULT(0),
        previous_balance        decimal(24, 4) DEFAULT(0),
        debit                   decimal(24, 4) DEFAULT(0),
        credit                  decimal(24, 4) DEFAULT(0),
        balance                 decimal(24, 4) DEFAULT(0),
        closing_debit           decimal(24, 4) DEFAULT(0),
        closing_credit          decimal(24, 4) DEFAULT(0),
        closing_balance         decimal(24, 4) DEFAULT(0),
        root_account_id         integer,
        normally_debit          boolean
    ) ON COMMIT DROP;

    INSERT INTO temp_income_expenditure_statement(account_id, previous_debit, previous_credit)    
    SELECT 
        verified_transaction_mat_view.account_id, 
        SUM(CASE tran_type WHEN 'Dr' THEN amount_in_local_currency ELSE 0 END),
        SUM(CASE tran_type WHEN 'Cr' THEN amount_in_local_currency ELSE 0 END)        
    FROM transactions.verified_transaction_mat_view
    WHERE value_date < _date_from
    AND office_id IN (SELECT * FROM office.get_office_ids(_office_id))
    AND account_master_id = _account_master_id
    GROUP BY verified_transaction_mat_view.account_id;



    IF(_date_to = 'infinity') THEN
        INSERT INTO temp_income_expenditure_statement(account_id, debit, credit)    
        SELECT 
            verified_transaction_mat_view.account_id, 
            SUM(CASE tran_type WHEN 'Dr' THEN amount_in_local_currency ELSE 0 END),
            SUM(CASE tran_type WHEN 'Cr' THEN amount_in_local_currency ELSE 0 END)        
        FROM transactions.verified_transaction_mat_view
        WHERE value_date > _date_from
        AND office_id IN (SELECT * FROM office.get_office_ids(_office_id))
        AND account_master_id = _account_master_id
        GROUP BY verified_transaction_mat_view.account_id;
    ELSE
        INSERT INTO temp_income_expenditure_statement(account_id, debit, credit)    
        SELECT 
            verified_transaction_mat_view.account_id, 
            SUM(CASE tran_type WHEN 'Dr' THEN amount_in_local_currency ELSE 0 END),
            SUM(CASE tran_type WHEN 'Cr' THEN amount_in_local_currency ELSE 0 END)        
        FROM transactions.verified_transaction_mat_view
        WHERE value_date >= _date_from AND value_date <= _date_to
        AND office_id IN (SELECT * FROM office.get_office_ids(_office_id))
        AND account_master_id = _account_master_id
        GROUP BY verified_transaction_mat_view.account_id;    
    END IF;

    UPDATE temp_income_expenditure_statement SET root_account_id = core.get_second_root_account_id(temp_income_expenditure_statement.account_id);


    DROP TABLE IF EXISTS temp_income_expenditure_statement2;
    
    IF(_compact) THEN
        CREATE TEMPORARY TABLE temp_income_expenditure_statement2
        ON COMMIT DROP
        AS
        SELECT
            temp_income_expenditure_statement.root_account_id AS account_id,
            ''::text as account_number,
            ''::text as account,
            SUM(temp_income_expenditure_statement.previous_debit) AS previous_debit,
            SUM(temp_income_expenditure_statement.previous_credit) AS previous_credit,
            0::decimal(24, 4) AS previous_balance,
            SUM(temp_income_expenditure_statement.debit) AS debit,
            SUM(temp_income_expenditure_statement.credit) as credit,
            0::decimal(24, 4) AS balance,
            SUM(temp_income_expenditure_statement.closing_debit) AS closing_debit,
            SUM(temp_income_expenditure_statement.closing_credit) AS closing_credit,
            0::decimal(24, 4) AS closing_balance,
            temp_income_expenditure_statement.normally_debit
        FROM temp_income_expenditure_statement
        GROUP BY 
            temp_income_expenditure_statement.root_account_id,
            temp_income_expenditure_statement.normally_debit;
    ELSE
        CREATE TEMPORARY TABLE temp_income_expenditure_statement2
        ON COMMIT DROP
        AS
        SELECT
            temp_income_expenditure_statement.account_id,
            ''::text as account_number,
            ''::text as account,
            SUM(temp_income_expenditure_statement.previous_debit) AS previous_debit,
            SUM(temp_income_expenditure_statement.previous_credit) AS previous_credit,
            0::decimal(24, 4) AS previous_balance,
            SUM(temp_income_expenditure_statement.debit) AS debit,
            SUM(temp_income_expenditure_statement.credit) as credit,
            0::decimal(24, 4) AS balance,
            SUM(temp_income_expenditure_statement.closing_debit) AS closing_debit,
            SUM(temp_income_expenditure_statement.closing_credit) AS closing_credit,
            0::decimal(24, 4) AS closing_balance,
            temp_income_expenditure_statement.normally_debit
        FROM temp_income_expenditure_statement
        GROUP BY 
            temp_income_expenditure_statement.account_id,
            temp_income_expenditure_statement.normally_debit;
    END IF;
    
    UPDATE temp_income_expenditure_statement2 SET
        account_number = core.accounts.account_number,
        account = core.accounts.account_name,
        normally_debit = core.account_masters.normally_debit
    FROM core.accounts
    INNER JOIN core.account_masters
    ON core.accounts.account_master_id = core.account_masters.account_master_id
    WHERE temp_income_expenditure_statement2.account_id = core.accounts.account_id;

    UPDATE temp_income_expenditure_statement2 SET 
        previous_balance = temp_income_expenditure_statement2.previous_credit - temp_income_expenditure_statement2.previous_debit,
        balance = temp_income_expenditure_statement2.credit - temp_income_expenditure_statement2.debit,
        closing_debit = temp_income_expenditure_statement2.previous_debit + temp_income_expenditure_statement2.debit,
        closing_credit = temp_income_expenditure_statement2.previous_credit + temp_income_expenditure_statement2.credit,
        closing_balance = temp_income_expenditure_statement2.previous_credit + temp_income_expenditure_statement2.credit - (temp_income_expenditure_statement2.previous_debit + temp_income_expenditure_statement2.debit);


    UPDATE temp_income_expenditure_statement2 SET 
        previous_balance = temp_income_expenditure_statement2.previous_balance * -1,
        balance = temp_income_expenditure_statement2.balance * -1,
        closing_balance = temp_income_expenditure_statement2.closing_balance * -1
    WHERE temp_income_expenditure_statement2.normally_debit;

    UPDATE temp_income_expenditure_statement2 SET previous_debit   = NULL WHERE temp_income_expenditure_statement2.previous_debit     = 0;
    UPDATE temp_income_expenditure_statement2 SET previous_credit  = NULL WHERE temp_income_expenditure_statement2.previous_credit    = 0;
    UPDATE temp_income_expenditure_statement2 SET previous_balance = NULL WHERE temp_income_expenditure_statement2.previous_balance   = 0;
    UPDATE temp_income_expenditure_statement2 SET debit            = NULL WHERE temp_income_expenditure_statement2.debit              = 0;
    UPDATE temp_income_expenditure_statement2 SET credit           = NULL WHERE temp_income_expenditure_statement2.credit             = 0;
    UPDATE temp_income_expenditure_statement2 SET balance          = NULL WHERE temp_income_expenditure_statement2.balance            = 0;
    UPDATE temp_income_expenditure_statement2 SET closing_debit    = NULL WHERE temp_income_expenditure_statement2.closing_debit      = 0;
    UPDATE temp_income_expenditure_statement2 SET closing_credit   = NULL WHERE temp_income_expenditure_statement2.closing_credit     = 0;
    UPDATE temp_income_expenditure_statement2 SET closing_balance  = NULL WHERE temp_income_expenditure_statement2.closing_balance    = 0;


    DELETE FROM temp_income_expenditure_statement2 WHERE temp_income_expenditure_statement2.closing_balance = 0;
   
    RETURN QUERY
    SELECT
        row_number() OVER(ORDER BY temp_income_expenditure_statement2.account_id)::integer AS id,
        temp_income_expenditure_statement2.account_id,
        temp_income_expenditure_statement2.account_number,
        temp_income_expenditure_statement2.account,
        temp_income_expenditure_statement2.previous_debit,
        temp_income_expenditure_statement2.previous_credit,
        temp_income_expenditure_statement2.previous_balance,
        temp_income_expenditure_statement2.debit,
        temp_income_expenditure_statement2.credit,
        temp_income_expenditure_statement2.balance,
        temp_income_expenditure_statement2.closing_debit,
        temp_income_expenditure_statement2.closing_credit,
        temp_income_expenditure_statement2.closing_balance
    FROM temp_income_expenditure_statement2;
END
$$
LANGUAGE plpgsql;

--SELECT * FROM transactions.get_income_expenditure_statement('1-1-2010','1-1-2020',1,1, true);
