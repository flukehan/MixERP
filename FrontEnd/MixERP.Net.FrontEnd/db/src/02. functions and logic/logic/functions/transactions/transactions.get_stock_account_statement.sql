DROP FUNCTION IF EXISTS transactions.get_stock_account_statement
(
    _value_date_from        date,
    _value_date_to          date,
    _user_id                integer,
    _item_id                integer,
    _store_id               integer
);

CREATE FUNCTION transactions.get_stock_account_statement
(
    _value_date_from        date,
    _value_date_to          date,
    _user_id                integer,
    _item_id                integer,
    _store_id               integer
)
RETURNS TABLE
(
    id                      integer,
    value_date              date,
    tran_code               text,
    statement_reference     text,
    debit                   decimal(24, 4),
    credit                  decimal(24, 4),
    balance                 decimal(24, 4),
    book                    text,
    item_id                 integer,
    item_code               text,
    item_name               text,
    posted_on               TIMESTAMP WITH TIME ZONE,
    posted_by               text,
    approved_by             text,
    verification_status     integer,
    flag_bg                 text,
    flag_fg                 text
)
VOLATILE AS
$$
BEGIN

    DROP TABLE IF EXISTS temp_account_statement;
    CREATE TEMPORARY TABLE temp_account_statement
    (
        id                      SERIAL,
        value_date              date,
        tran_code               text,
        statement_reference     text,
        debit                   decimal(24, 4),
        credit                  decimal(24, 4),
        balance                 decimal(24, 4),
        book                    text,
        item_id                 integer,
        item_code               text,
        item_name               text,
        posted_on               TIMESTAMP WITH TIME ZONE,
        posted_by               text,
        approved_by             text,
        verification_status     integer,
        flag_bg                 text,
        flag_fg                 text
    ) ON COMMIT DROP;

    INSERT INTO temp_account_statement(value_date, statement_reference, debit, item_id)
    SELECT 
        _value_date_from, 
        'Opening Balance', 
        SUM
        (
            CASE transactions.stock_details.tran_type
            WHEN 'Dr' THEN base_quantity
            ELSE base_quantity * -1 
            END            
        ) as debit,
        _item_id
    FROM transactions.stock_details
    INNER JOIN transactions.stock_master
    ON transactions.stock_details.stock_master_id = transactions.stock_master.stock_master_id
    INNER JOIN transactions.transaction_master
    ON transactions.stock_master.transaction_master_id = transactions.transaction_master.transaction_master_id
    WHERE
        transactions.transaction_master.verification_status_id > 0
    AND 
        transactions.transaction_master.value_date < _value_date_from
    AND 
        transactions.stock_details.store_id = _store_id
    AND
        transactions.stock_details.item_id = _item_id;

    DELETE FROM temp_account_statement
    WHERE COALESCE(temp_account_statement.debit, 0) = 0
    AND COALESCE(temp_account_statement.credit, 0) = 0;

    UPDATE temp_account_statement SET 
    debit = temp_account_statement.credit * -1,
    credit = 0
    WHERE temp_account_statement.credit < 0;

    INSERT INTO temp_account_statement(value_date, tran_code, statement_reference, debit, credit, book, item_id, posted_on, posted_by, approved_by, verification_status)
    SELECT
        transactions.transaction_master.value_date,
        transactions.transaction_master.transaction_code,
        transactions.transaction_master.statement_reference,
        CASE transactions.stock_details.tran_type
        WHEN 'Dr' THEN base_quantity
        ELSE 0 END AS debit,
        CASE transactions.stock_details.tran_type
        WHEN 'Cr' THEN base_quantity
        ELSE 0 END AS credit,
        transactions.transaction_master.book,
        transactions.stock_details.item_id,
        transactions.transaction_master.transaction_ts AS posted_on,
        office.get_user_name_by_user_id(COALESCE(transactions.transaction_master.user_id, transactions.transaction_master.sys_user_id)),
        office.get_user_name_by_user_id(transactions.transaction_master.verified_by_user_id),
        transactions.transaction_master.verification_status_id
    FROM transactions.transaction_master
    INNER JOIN transactions.stock_master
    ON transactions.transaction_master.transaction_master_id = transactions.stock_master.transaction_master_Id
    INNER JOIN transactions.stock_details
    ON transactions.stock_master.stock_master_id = transactions.stock_details.stock_master_id
    WHERE
        transactions.transaction_master.verification_status_id > 0
    AND
        transactions.transaction_master.value_date >= _value_date_from
    AND
        transactions.transaction_master.value_date <= _value_date_to
    AND
       transactions.stock_details.store_id = _store_id 
    AND
       transactions.stock_details.item_id = _item_id
    ORDER BY 
        transactions.transaction_master.value_date,
        transactions.transaction_master.last_verified_on;
    
    UPDATE temp_account_statement
    SET balance = c.balance
    FROM
    (
        SELECT
            temp_account_statement.id, 
            SUM(COALESCE(c.debit, 0)) 
            - 
            SUM(COALESCE(c.credit,0)) As balance
        FROM temp_account_statement
        LEFT JOIN temp_account_statement AS c 
            ON (c.id <= temp_account_statement.id)
        GROUP BY temp_account_statement.id
        ORDER BY temp_account_statement.id
    ) AS c
    WHERE temp_account_statement.id = c.id;

    UPDATE temp_account_statement SET 
        item_code = core.items.item_code,
        item_name = core.items.item_name
    FROM core.items
    WHERE temp_account_statement.item_id = core.items.item_id;

    UPDATE temp_account_statement SET
        flag_bg = core.get_flag_background_color(core.get_flag_type_id(_user_id, 'account_statement', 'transaction_code', temp_account_statement.tran_code::text)),
        flag_fg = core.get_flag_foreground_color(core.get_flag_type_id(_user_id, 'account_statement', 'transaction_code', temp_account_statement.tran_code::text));

        
    RETURN QUERY
    SELECT * FROM temp_account_statement;
END
$$
LANGUAGE plpgsql;

DROP FUNCTION IF EXISTS transactions.get_stock_account_statement
(
    _value_date_from        date,
    _value_date_to          date,
    _user_id                integer,
    _item_code              text,
    _store_id               integer
);

CREATE FUNCTION transactions.get_stock_account_statement
(
    _value_date_from        date,
    _value_date_to          date,
    _user_id                integer,
    _item_code              text,
    _store_id               integer
)
RETURNS TABLE
(
    id                      integer,
    value_date              date,
    tran_code               text,
    statement_reference     text,
    debit                   decimal(24, 4),
    credit                  decimal(24, 4),
    balance                 decimal(24, 4),
    book                    text,
    item_id                 integer,
    item_code               text,
    item_name               text,
    posted_on               TIMESTAMP WITH TIME ZONE,
    posted_by               text,
    approved_by             text,
    verification_status     integer,
    flag_bg                 text,
    flag_fg                 text
)
VOLATILE AS
$$
    DECLARE _item_id        integer;
BEGIN

    SELECT core.items.item_id INTO _item_id
    FROM core.items
    WHERE core.items.item_code = _item_code;

    RETURN QUERY
    SELECT * FROM transactions.get_stock_account_statement(_value_date_from, _value_date_to, _user_id, _item_id, _store_id);
END
$$
LANGUAGE plpgsql;

--SELECT * FROM transactions.get_stock_account_statement('1-1-2010', '1-1-2020', 2, 'RMBP', 1);

