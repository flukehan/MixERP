DROP FUNCTION IF EXISTS transactions.get_retained_earnings_statement
(
    _date_to                        date,
    _office_id                      integer,
    _factor                         integer    
);

CREATE FUNCTION transactions.get_retained_earnings_statement
(
    _date_to                        date,
    _office_id                      integer,
    _factor                         integer    
)
RETURNS TABLE
(
    id                              integer,
    value_date                      date,
    tran_code                       text,
    statement_reference             text,
    debit                           decimal(24, 4),
    credit                          decimal(24, 4),
    balance                         decimal(24, 4),
    office                          text,
    book                            text,
    account_id                      integer,
    account_number                  text,
    account                         text,
    posted_on                       TIMESTAMP WITH TIME ZONE,
    posted_by                       text,
    approved_by                     text,
    verification_status             integer
)
AS
$$
    DECLARE _accounts               integer[];
    DECLARE _date_from              date;
    DECLARE _net_profit             decimal(24, 4)  = 0;
    DECLARE _income_tax_rate        real            = 0;
    DECLARE _itp                    decimal(24, 4)  = 0;
BEGIN
    _date_from                      := core.get_fiscal_year_start_date(_office_id);
    _net_profit                     := transactions.get_net_profit(_date_from, _date_to, _office_id, _factor);
    _income_tax_rate                := office.get_income_tax_rate(_office_id);

    IF(COALESCE(_factor , 0) = 0) THEN
        _factor                         := 1;
    END IF; 

    IF(_income_tax_rate != 0) THEN
        _itp                            := (_net_profit * _income_tax_rate) / (100 - _income_tax_rate);
    END IF;

    DROP TABLE IF EXISTS temp_account_statement;
    CREATE TEMPORARY TABLE temp_account_statement
    (
        id                          SERIAL,
        value_date                  date,
        tran_code                   text,
        statement_reference         text,
        debit                       decimal(24, 4),
        credit                      decimal(24, 4),
        balance                     decimal(24, 4),
        office                      text,
        book                        text,
        account_id                  integer,
        account_number              text,
        account                     text,
        posted_on                   TIMESTAMP WITH TIME ZONE,
        posted_by                   text,
        approved_by                 text,
        verification_status         integer
    ) ON COMMIT DROP;

    SELECT array_agg(core.accounts.account_id) INTO _accounts
    FROM core.accounts
    WHERE core.accounts.account_master_id BETWEEN 15300 AND 15400;

    INSERT INTO temp_account_statement(value_date, tran_code, statement_reference, debit, credit, office, book, account_id, posted_on, posted_by, approved_by, verification_status)
    SELECT
        _date_from,
        NULL,
        'Beginning balance on this fiscal year.',
        NULL,
        SUM
        (
            CASE transactions.transaction_details.tran_type
            WHEN 'Cr' THEN amount_in_local_currency
            ELSE amount_in_local_currency * -1 
            END            
        ) as credit,
        NULL,
        NULL,
        NULL,
        NULL,
        NULL,
        NULL,
        NULL
    FROM transactions.transaction_master
    INNER JOIN transactions.transaction_details
    ON transactions.transaction_master.transaction_master_id = transactions.transaction_details.transaction_master_id
    WHERE
        transactions.transaction_master.verification_status_id > 0
    AND
        transactions.transaction_master.value_date < _date_from
    AND
       transactions.transaction_master.office_id IN (SELECT * FROM office.get_office_ids(_office_id)) 
    AND
       transactions.transaction_details.account_id = ANY(_accounts);

    INSERT INTO temp_account_statement(value_date, tran_code, statement_reference, debit, credit)
    SELECT _date_to, '', format('Add: Net Profit as on %1$s.', _date_to::text), 0, _net_profit;

    INSERT INTO temp_account_statement(value_date, tran_code, statement_reference, debit, credit)
    SELECT _date_to, '', 'Add: Income Tax provison.', 0, _itp;

--     DELETE FROM temp_account_statement
--     WHERE COALESCE(temp_account_statement.debit, 0) = 0
--     AND COALESCE(temp_account_statement.credit, 0) = 0;
    

    UPDATE temp_account_statement SET 
    debit = temp_account_statement.credit * -1,
    credit = 0
    WHERE temp_account_statement.credit < 0;


    INSERT INTO temp_account_statement(value_date, tran_code, statement_reference, debit, credit, office, book, account_id, posted_on, posted_by, approved_by, verification_status)
    SELECT
        transactions.transaction_master.value_date,
        transactions.transaction_master. transaction_code,
        transactions.transaction_details.statement_reference,
        CASE transactions.transaction_details.tran_type
        WHEN 'Dr' THEN amount_in_local_currency / _factor
        ELSE NULL END,
        CASE transactions.transaction_details.tran_type
        WHEN 'Cr' THEN amount_in_local_currency / _factor
        ELSE NULL END,
        office.get_office_name_by_id(transactions.transaction_master.office_id),
        transactions.transaction_master.book,
        transactions.transaction_details.account_id,
        transactions.transaction_master.transaction_ts,
        office.get_user_name_by_user_id(COALESCE(transactions.transaction_master.user_id, transactions.transaction_master.sys_user_id)),
        office.get_user_name_by_user_id(transactions.transaction_master.verified_by_user_id),
        transactions.transaction_master.verification_status_id
    FROM transactions.transaction_master
    INNER JOIN transactions.transaction_details
    ON transactions.transaction_master.transaction_master_id = transactions.transaction_details.transaction_master_id
    WHERE
        transactions.transaction_master.verification_status_id > 0
    AND
        transactions.transaction_master.value_date >= _date_from
    AND
        transactions.transaction_master.value_date <= _date_to
    AND
       transactions.transaction_master.office_id IN (SELECT * FROM office.get_office_ids(_office_id)) 
    AND
       transactions.transaction_details.account_id = ANY(_accounts)
    ORDER BY 
        transactions.transaction_master.value_date,
        transactions.transaction_master.last_verified_on;


    UPDATE temp_account_statement
    SET balance = c.balance
    FROM
    (
        SELECT
            temp_account_statement.id, 
            SUM(COALESCE(c.credit, 0)) 
            - 
            SUM(COALESCE(c.debit,0)) As balance
        FROM temp_account_statement
        LEFT JOIN temp_account_statement AS c 
            ON (c.id <= temp_account_statement.id)
        GROUP BY temp_account_statement.id
        ORDER BY temp_account_statement.id
    ) AS c
    WHERE temp_account_statement.id = c.id;

    UPDATE temp_account_statement SET 
        account_number = core.accounts.account_number,
        account = core.accounts.account_name
    FROM core.accounts
    WHERE temp_account_statement.account_id = core.accounts.account_id;


    UPDATE temp_account_statement SET debit = NULL WHERE temp_account_statement.debit = 0;
    UPDATE temp_account_statement SET credit = NULL WHERE temp_account_statement.credit = 0;

    RETURN QUERY
    SELECT * FROM temp_account_statement
    ORDER BY id;    
END
$$
LANGUAGE plpgsql;


--SELECT * FROM transactions.get_retained_earnings_statement('7/16/2015', 2, 1000);

--SELECT * FROM transactions.get_retained_earnings('7/16/2015', 2, 100);

