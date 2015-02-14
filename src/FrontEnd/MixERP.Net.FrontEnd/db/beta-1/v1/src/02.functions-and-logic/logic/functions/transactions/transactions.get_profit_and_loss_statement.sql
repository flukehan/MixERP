DROP FUNCTION IF EXISTS transactions.get_profit_and_loss_statement
(
    _date_from                      date,
    _date_to                        date,
    _user_id                        integer,
    _office_id                      integer,
    _factor                         integer,
    _compact                        boolean
);

CREATE FUNCTION transactions.get_profit_and_loss_statement
(
    _date_from                      date,
    _date_to                        date,
    _user_id                        integer,
    _office_id                      integer,
    _factor                         integer,
    _compact                        boolean DEFAULT(true)
)
RETURNS json
AS
$$
    DECLARE _sql                    text;
    DECLARE _periods                core.period[];
    DECLARE _json                   json;
    DECLARE this                    RECORD;
    DECLARE _balance                decimal(24, 4);
    DECLARE _is_periodic            boolean = office.is_periodic_inventory(_office_id);
BEGIN    
    DROP TABLE IF EXISTS pl_temp;
    CREATE TEMPORARY TABLE pl_temp
    (
        item_id                     integer PRIMARY KEY,
        item                        text,
        account_id                  integer,
        parent_item_id              integer REFERENCES pl_temp(item_id),
        is_profit                   boolean DEFAULT(false),
        is_summation                boolean DEFAULT(false),
        is_debit                    boolean DEFAULT(false),
        amount                      decimal(24, 4) DEFAULT(0)
    ) ON COMMIT DROP;

    IF(COALESCE(_factor, 0) = 0) THEN
        _factor := 1;
    END IF;

    _periods            := core.get_periods(_date_from, _date_to);

    IF(_periods IS NULL) THEN
        RAISE EXCEPTION 'Invalid period specified.'
        USING ERRCODE='P3009';
    END IF;

    SELECT string_agg(dynamic, '') FROM
    (
            SELECT 'ALTER TABLE pl_temp ADD COLUMN "' || period_name || '" decimal(24, 4) DEFAULT(0);' as dynamic
            FROM explode_array(_periods)
         
    ) periods
    INTO _sql;
    
    EXECUTE _sql;

    --PL structure setup start
    INSERT INTO pl_temp(item_id, item, is_summation, parent_item_id)
    SELECT 1000,   'Revenue',                      true,   NULL::integer   UNION ALL
    SELECT 2000,   'Cost of Sales',                true,   NULL::integer   UNION ALL
    SELECT 2001,   'Opening Stock',                false,  1000            UNION ALL
    SELECT 3000,   'Purchases',                    false,  1000            UNION ALL
    SELECT 4000,   'Closing Stock',                false,  1000            UNION ALL
    SELECT 5000,   'Direct Costs',                 true,   NULL::integer   UNION ALL
    SELECT 6000,   'Gross Profit',                 false,  NULL::integer   UNION ALL
    SELECT 7000,   'Operating Expenses',           true,   NULL::integer   UNION ALL
    SELECT 8000,   'Operating Profit',             false,  NULL::integer   UNION ALL
    SELECT 9000,   'Nonoperating Incomes',         true,   NULL::integer   UNION ALL
    SELECT 10000,  'Financial Incomes',            true,   NULL::integer   UNION ALL
    SELECT 11000,  'Financial Expenses',           true,   NULL::integer   UNION ALL
    SELECT 11100,  'Interest Expenses',            true,   11000           UNION ALL
    SELECT 12000,  'Profit Before Income Taxes',   false,  NULL::integer   UNION ALL
    SELECT 13000,  'Income Taxes',                 true,   NULL::integer   UNION ALL
    SELECT 13001,  'Income Tax Provison',          false,  13000            UNION ALL
    SELECT 14000,  'Net Profit',                   true,   NULL::integer;

    UPDATE pl_temp SET is_debit = true WHERE item_id IN(2001, 3000, 4000);
    UPDATE pl_temp SET is_profit = true WHERE item_id IN(6000,8000, 12000, 14000);
    
    INSERT INTO pl_temp(item_id, account_id, item, parent_item_id, is_debit)
    SELECT id, account_id, account_name, 1000 as parent_item_id, false as is_debit FROM core.get_account_view_by_account_master_id(20100, 1000) UNION ALL--Sales Accounts
    SELECT id, account_id, account_name, 2000 as parent_item_id, true as is_debit FROM core.get_account_view_by_account_master_id(20400, 2001) UNION ALL--COGS Accounts
    SELECT id, account_id, account_name, 5000 as parent_item_id, true as is_debit FROM core.get_account_view_by_account_master_id(20500, 5000) UNION ALL--Direct Cost
    SELECT id, account_id, account_name, 7000 as parent_item_id, true as is_debit FROM core.get_account_view_by_account_master_id(20600, 7000) UNION ALL--Operating Expenses
    SELECT id, account_id, account_name, 9000 as parent_item_id, false as is_debit FROM core.get_account_view_by_account_master_id(20200, 9000) UNION ALL--Nonoperating Incomes
    SELECT id, account_id, account_name, 10000 as parent_item_id, false as is_debit FROM core.get_account_view_by_account_master_id(20300, 10000) UNION ALL--Financial Incomes
    SELECT id, account_id, account_name, 11000 as parent_item_id, true as is_debit FROM core.get_account_view_by_account_master_id(20700, 11000) UNION ALL--Financial Expenses
    SELECT id, account_id, account_name, 11100 as parent_item_id, true as is_debit FROM core.get_account_view_by_account_master_id(20701, 11100) UNION ALL--Interest Expenses
    SELECT id, account_id, account_name, 13000 as parent_item_id, true as is_debit FROM core.get_account_view_by_account_master_id(20800, 13001);--Income Tax Expenses

    IF(NOT _is_periodic) THEN
        DELETE FROM pl_temp WHERE item_id IN(2001, 3000, 4000);
    END IF;
    --PL structure setup end


    FOR this IN SELECT * FROM explode_array(_periods) ORDER BY date_from ASC
    LOOP
        --Updating credit balances of individual GL accounts.
        _sql := 'UPDATE pl_temp SET "' || this.period_name || '"=tran.total_amount
        FROM
        (
            SELECT transactions.verified_transaction_mat_view.account_id,
            SUM(CASE tran_type WHEN ''Cr'' THEN amount_in_local_currency ELSE 0 END) - 
            SUM(CASE tran_type WHEN ''Dr'' THEN amount_in_local_currency ELSE 0 END) AS total_amount
        FROM transactions.verified_transaction_mat_view
        WHERE value_date >=''' || this.date_from::text || ''' AND value_date <=''' || this.date_to::text ||
        ''' AND office_id IN (SELECT * FROM office.get_office_ids(' || _office_id::text || '))
        GROUP BY transactions.verified_transaction_mat_view.account_id
        ) AS tran
        WHERE tran.account_id = pl_temp.account_id';
        EXECUTE _sql;

        --Reversing to debit balance for expense headings.
        _sql := 'UPDATE pl_temp SET "' || this.period_name || '"="' || this.period_name || '"*-1 WHERE is_debit;';
        EXECUTE _sql;

        --Getting purchase and stock balances if this is a periodic inventory system.
        --In perpetual accounting system, one would not need to include these headings 
        --because the COGS A/C would be automatically updated on each transaction.
        IF(_is_periodic) THEN
            _sql := 'UPDATE pl_temp SET "' || this.period_name || '"=transactions.get_closing_stock(''' || (this.date_from::TIMESTAMP - INTERVAL '1 day')::text ||  ''', ' || _office_id::text || ') WHERE item_id=2001;';
            EXECUTE _sql;

            _sql := 'UPDATE pl_temp SET "' || this.period_name || '"=transactions.get_purchase(''' || this.date_from::text ||  ''', ''' || this.date_to::text || ''', ' || _office_id::text || ') *-1 WHERE item_id=3000;';
            EXECUTE _sql;

            _sql := 'UPDATE pl_temp SET "' || this.period_name || '"=transactions.get_closing_stock(''' || this.date_from::text ||  ''', ' || _office_id::text || ') WHERE item_id=4000;';
            EXECUTE _sql;
        END IF;
    END LOOP;

    --Updating the column "amount" on each row by the sum of all periods.
    SELECT 'UPDATE pl_temp SET amount = ' || array_to_string(array_agg('COALESCE("' || period_name || '", 0)'), ' +') || ';'::text INTO _sql
    FROM explode_array(_periods);

    EXECUTE _sql;

    --Updating amount and periodic balances on parent item by the sum of their respective child balances.
    SELECT 'UPDATE pl_temp SET amount = tran.amount, ' || array_to_string(array_agg('"' || period_name || '"=tran."' || period_name || '"'), ',') || 
    ' FROM 
    (
        SELECT parent_item_id,
        SUM(amount) AS amount, '
        || array_to_string(array_agg('SUM("' || period_name || '") AS "' || period_name || '"'), ',') || '
         FROM pl_temp
        GROUP BY parent_item_id
    ) 
    AS tran
        WHERE tran.parent_item_id = pl_temp.item_id;'
    INTO _sql
    FROM explode_array(_periods);
    EXECUTE _sql;

    --Updating Gross Profit.
    --Gross Profit = Revenue - (Cost of Sales + Direct Costs)
    SELECT 'UPDATE pl_temp SET amount = tran.amount, ' || array_to_string(array_agg('"' || period_name || '"=tran."' || period_name || '"'), ',') 
    || ' FROM 
    (
        SELECT
        SUM(CASE item_id WHEN 1000 THEN amount ELSE amount * -1 END) AS amount, '
        || array_to_string(array_agg('SUM(CASE item_id WHEN 1000 THEN "' || period_name || '" ELSE "' || period_name || '" *-1 END) AS "' || period_name || '"'), ',') ||
    '
         FROM pl_temp
         WHERE item_id IN
         (
             1000,2000,5000
         )
    ) 
    AS tran
    WHERE item_id = 6000;'
    INTO _sql
    FROM explode_array(_periods);

    EXECUTE _sql;


    --Updating Operating Profit.
    --Operating Profit = Gross Profit - Operating Expenses
    SELECT 'UPDATE pl_temp SET amount = tran.amount, ' || array_to_string(array_agg('"' || period_name || '"=tran."' || period_name || '"'), ',') 
    || ' FROM 
    (
        SELECT
        SUM(CASE item_id WHEN 6000 THEN amount ELSE amount * -1 END) AS amount, '
        || array_to_string(array_agg('SUM(CASE item_id WHEN 6000 THEN "' || period_name || '" ELSE "' || period_name || '" *-1 END) AS "' || period_name || '"'), ',') ||
    '
         FROM pl_temp
         WHERE item_id IN
         (
             6000, 7000
         )
    ) 
    AS tran
    WHERE item_id = 8000;'
    INTO _sql
    FROM explode_array(_periods);

    EXECUTE _sql;

    --Updating Profit Before Income Taxes.
    --Profit Before Income Taxes = Operating Profit + Nonoperating Incomes + Financial Incomes - Financial Expenses
    SELECT 'UPDATE pl_temp SET amount = tran.amount, ' || array_to_string(array_agg('"' || period_name || '"=tran."' || period_name || '"'), ',') 
    || ' FROM 
    (
        SELECT
        SUM(CASE WHEN item_id IN(11000, 11100) THEN amount *-1 ELSE amount END) AS amount, '
        || array_to_string(array_agg('SUM(CASE WHEN item_id IN(11000, 11100) THEN "' || period_name || '"*-1  ELSE "' || period_name || '" END) AS "' || period_name || '"'), ',') ||
    '
         FROM pl_temp
         WHERE item_id IN
         (
             8000, 9000, 10000, 11000, 11100
         )
    ) 
    AS tran
    WHERE item_id = 12000;'
    INTO _sql
    FROM explode_array(_periods);

    EXECUTE _sql;

    --Updating Income Tax Provison.
    --Income Tax Provison = Profit Before Income Taxes * Income Tax Rate - Paid Income Taxes
    SELECT * INTO this FROM pl_temp WHERE item_id = 12000;
    
    _sql := 'UPDATE pl_temp SET amount = core.get_income_tax_provison_amount(' || _office_id::text || ',' || this.amount::text || ',(SELECT amount FROM pl_temp WHERE item_id = 13000)), ' 
    || array_to_string(array_agg('"' || period_name || '"=core.get_income_tax_provison_amount(' || _office_id::text || ',' || core.get_field(hstore(this.*), period_name) || ', (SELECT "' || period_name || '" FROM pl_temp WHERE item_id = 13000))'), ',')
            || ' WHERE item_id = 13001;'
    FROM explode_array(_periods);

    EXECUTE _sql;

    --Updating amount and periodic balances on parent item by the sum of their respective child balances, once again to add the Income Tax Provison to Income Tax Expenses.
    SELECT 'UPDATE pl_temp SET amount = tran.amount, ' || array_to_string(array_agg('"' || period_name || '"=tran."' || period_name || '"'), ',') 
    || ' FROM 
    (
        SELECT parent_item_id,
        SUM(amount) AS amount, '
        || array_to_string(array_agg('SUM("' || period_name || '") AS "' || period_name || '"'), ',') ||
    '
         FROM pl_temp
        GROUP BY parent_item_id
    ) 
    AS tran
        WHERE tran.parent_item_id = pl_temp.item_id;'
    INTO _sql
    FROM explode_array(_periods);
    EXECUTE _sql;


    --Updating Net Profit.
    --Net Profit = Profit Before Income Taxes - Income Tax Expenses
    SELECT 'UPDATE pl_temp SET amount = tran.amount, ' || array_to_string(array_agg('"' || period_name || '"=tran."' || period_name || '"'), ',') 
    || ' FROM 
    (
        SELECT
        SUM(CASE item_id WHEN 13000 THEN amount *-1 ELSE amount END) AS amount, '
        || array_to_string(array_agg('SUM(CASE item_id WHEN 13000 THEN "' || period_name || '"*-1  ELSE "' || period_name || '" END) AS "' || period_name || '"'), ',') ||
    '
         FROM pl_temp
         WHERE item_id IN
         (
             12000, 13000
         )
    ) 
    AS tran
    WHERE item_id = 14000;'
    INTO _sql
    FROM explode_array(_periods);

    EXECUTE _sql;

    --Removing ledgers having zero balances
    DELETE FROM pl_temp
    WHERE COALESCE(amount, 0) = 0
    AND account_id IS NOT NULL;


    --Dividing by the factor.
    SELECT 'UPDATE pl_temp SET amount = amount /' || _factor::text || ',' || array_to_string(array_agg('"' || period_name || '"="' || period_name || '"/' || _factor::text), ',') || ';'
    INTO _sql
    FROM explode_array(_periods);
    EXECUTE _sql;


    --Converting 0's to NULLS.
    SELECT 'UPDATE pl_temp SET amount = CASE WHEN amount = 0 THEN NULL ELSE amount END,' || array_to_string(array_agg('"' || period_name || '"= CASE WHEN "' || period_name || '" = 0 THEN NULL ELSE "' || period_name || '" END'), ',') || ';'
    INTO _sql
    FROM explode_array(_periods);

    EXECUTE _sql;

    IF(_compact) THEN
        SELECT array_to_json(array_agg(row_to_json(report)))
        INTO _json
        FROM
        (
            SELECT item, amount, is_profit, is_summation
            FROM pl_temp
            ORDER BY item_id
        ) AS report;
    ELSE
        SELECT 
        'SELECT array_to_json(array_agg(row_to_json(report)))
        FROM
        (
            SELECT item, amount,'
            || array_to_string(array_agg('"' || period_name || '"'), ',') ||
            ', is_profit, is_summation FROM pl_temp
            ORDER BY item_id
        ) AS report;'
        INTO _sql
        FROM explode_array(_periods);

        EXECUTE _sql INTO _json ;
    END IF;    

    RETURN _json;
END
$$
LANGUAGE plpgsql;

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
    AND account_master_id >=20100
    AND account_master_id <= 20300;
    
    SELECT SUM(CASE tran_type WHEN 'Dr' THEN amount_in_local_currency ELSE amount_in_local_currency * -1 END)
    INTO _expenses
    FROM transactions.verified_transaction_mat_view
    WHERE value_date >= _date_from AND value_date <= _date_to
    AND account_master_id >=20400
    AND account_master_id <= 20701;
    
    SELECT SUM(CASE tran_type WHEN 'Dr' THEN amount_in_local_currency ELSE amount_in_local_currency * -1 END)
    INTO _tax_paid
    FROM transactions.verified_transaction_mat_view
    WHERE value_date >= _date_from AND value_date <= _date_to
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



--SELECT transactions.get_profit_and_loss_statement('1-1-2000','1-15-2020', 2, 2, 1000,false), transactions.get_net_profit('1-1-2000','1-15-2020', 2, 1000);