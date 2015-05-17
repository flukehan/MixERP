DROP FUNCTION IF EXISTS transactions.get_cash_flow_statement
(
    _date_from                      date,
    _date_to                        date,
    _user_id                        integer,
    _office_id                      integer,
    _factor                         integer
);

CREATE FUNCTION transactions.get_cash_flow_statement
(
    _date_from                      date,
    _date_to                        date,
    _user_id                        integer,
    _office_id                      integer,
    _factor                         integer
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
    --We cannot divide by zero.
    IF(COALESCE(_factor, 0) = 0) THEN
        _factor := 1;
    END IF;

    DROP TABLE IF EXISTS cf_temp;
    CREATE TEMPORARY TABLE cf_temp
    (
        item_id                     integer PRIMARY KEY,
        item                        text,
        account_master_id           integer,
        parent_item_id              integer REFERENCES cf_temp(item_id),
        is_summation                boolean DEFAULT(false),
        is_debit                    boolean DEFAULT(false),
        is_sales                    boolean DEFAULT(false),
        is_purchase                 boolean DEFAULT(false)
    ) ON COMMIT DROP;


    _periods            := core.get_periods(_date_from, _date_to);

    IF(_periods IS NULL) THEN
        RAISE EXCEPTION 'Invalid period specified.'
        USING ERRCODE='P3009';
    END IF;

    /**************************************************************************************************************************************************************************************
        CREATING PERIODS
    **************************************************************************************************************************************************************************************/
    SELECT string_agg(dynamic, '') FROM
    (
            SELECT 'ALTER TABLE cf_temp ADD COLUMN "' || period_name || '" decimal(24, 4) DEFAULT(0);' as dynamic
            FROM explode_array(_periods)
         
    ) periods
    INTO _sql;
    
    EXECUTE _sql;

    /**************************************************************************************************************************************************************************************
        CASHFLOW TABLE STRUCTURE START
    **************************************************************************************************************************************************************************************/
    INSERT INTO cf_temp(item_id, item, is_summation, is_debit)
    SELECT  10000,  'Cash and cash equivalents, beginning of period',   false,  true    UNION ALL    
    SELECT  20000,  'Cash flows from operating activities',             true,   false   UNION ALL    
    SELECT  30000,  'Cash flows from investing activities',             true,   false   UNION ALL
    SELECT  40000,  'Cash flows from financing acticities',             true,   false   UNION ALL    
    SELECT  50000,  'Net increase in cash and cash equivalents',        false,  false   UNION ALL    
    SELECT  60000,  'Cash and cash equivalents, end of period',         false,  true;    

    INSERT INTO cf_temp(item_id, item, parent_item_id, is_debit, is_sales, is_purchase)
    SELECT  cash_flow_heading_id,   cash_flow_heading_name, 20000,  is_debit,   is_sales,   is_purchase FROM core.cash_flow_headings WHERE cash_flow_heading_type = 'O' UNION ALL
    SELECT  cash_flow_heading_id,   cash_flow_heading_name, 30000,  is_debit,   is_sales,   is_purchase FROM core.cash_flow_headings WHERE cash_flow_heading_type = 'I' UNION ALL 
    SELECT  cash_flow_heading_id,   cash_flow_heading_name, 40000,  is_debit,   is_sales,   is_purchase FROM core.cash_flow_headings WHERE cash_flow_heading_type = 'F';

    INSERT INTO cf_temp(item_id, item, parent_item_id, is_debit, account_master_id)
    SELECT core.account_masters.account_master_id + 50000, core.account_masters.account_master_name,  core.cash_flow_setup.cash_flow_heading_id, core.cash_flow_headings.is_debit, core.account_masters.account_master_id
    FROM core.cash_flow_setup
    INNER JOIN core.account_masters
    ON core.cash_flow_setup.account_master_id = core.account_masters.account_master_id
    INNER JOIN core.cash_flow_headings
    ON core.cash_flow_setup.cash_flow_heading_id = core.cash_flow_headings.cash_flow_heading_id;

    /**************************************************************************************************************************************************************************************
        CASHFLOW TABLE STRUCTURE END
    **************************************************************************************************************************************************************************************/


    /**************************************************************************************************************************************************************************************
        ITERATING THROUGH PERIODS TO UPDATE TRANSACTION BALANCES
    **************************************************************************************************************************************************************************************/
    FOR this IN SELECT * FROM explode_array(_periods) ORDER BY date_from ASC
    LOOP
        --
        --
        --Opening cash balance.
        --
        --
        _sql := 'UPDATE cf_temp SET "' || this.period_name || '"=
            (
                SELECT
                SUM(CASE tran_type WHEN ''Cr'' THEN amount_in_local_currency ELSE 0 END) - 
                SUM(CASE tran_type WHEN ''Dr'' THEN amount_in_local_currency ELSE 0 END) AS total_amount
            FROM transactions.verified_cash_transaction_mat_view
            WHERE account_master_id IN(10101, 10102) 
            AND value_date <''' || this.date_from::text ||
            ''' AND office_id IN (SELECT * FROM office.get_office_ids(' || _office_id::text || '))
            )
        WHERE cf_temp.item_id = 10000;';

        EXECUTE _sql;

        --
        --
        --Updating debit balances of mapped account master heads.
        --
        --
        _sql := 'UPDATE cf_temp SET "' || this.period_name || '"=tran.total_amount
        FROM
        (
            SELECT transactions.verified_cash_transaction_mat_view.account_master_id,
            SUM(CASE tran_type WHEN ''Dr'' THEN amount_in_local_currency ELSE 0 END) - 
            SUM(CASE tran_type WHEN ''Cr'' THEN amount_in_local_currency ELSE 0 END) AS total_amount
        FROM transactions.verified_cash_transaction_mat_view
        WHERE transactions.verified_cash_transaction_mat_view.book NOT IN (''Sales.Direct'', ''Sales.Receipt'', ''Sales.Delivery'', ''Purchase.Direct'', ''Purchase.Receipt'')
        AND NOT account_master_id IN(10101, 10102) 
        AND value_date >=''' || this.date_from::text || ''' AND value_date <=''' || this.date_to::text ||
        ''' AND office_id IN (SELECT * FROM office.get_office_ids(' || _office_id::text || '))
        GROUP BY transactions.verified_cash_transaction_mat_view.account_master_id
        ) AS tran
        WHERE tran.account_master_id = cf_temp.account_master_id';
        EXECUTE _sql;

        --
        --
        --Updating cash paid to suppliers.
        --
        --
        _sql := 'UPDATE cf_temp SET "' || this.period_name || '"=
        
        (
            SELECT
            SUM(CASE tran_type WHEN ''Dr'' THEN amount_in_local_currency ELSE 0 END) - 
            SUM(CASE tran_type WHEN ''Cr'' THEN amount_in_local_currency ELSE 0 END) 
        FROM transactions.verified_cash_transaction_mat_view
        WHERE transactions.verified_cash_transaction_mat_view.book IN (''Purchase.Direct'', ''Purchase.Receipt'', ''Purchase.Payment'')
        AND NOT account_master_id IN(10101, 10102) 
        AND value_date >=''' || this.date_from::text || ''' AND value_date <=''' || this.date_to::text ||
        ''' AND office_id IN (SELECT * FROM office.get_office_ids(' || _office_id::text || '))
        )
        WHERE cf_temp.is_purchase;';
        EXECUTE _sql;

        --
        --
        --Updating cash received from customers.
        --
        --
        _sql := 'UPDATE cf_temp SET "' || this.period_name || '"=
        
        (
            SELECT
            SUM(CASE tran_type WHEN ''Cr'' THEN amount_in_local_currency ELSE 0 END) - 
            SUM(CASE tran_type WHEN ''Dr'' THEN amount_in_local_currency ELSE 0 END) 
        FROM transactions.verified_cash_transaction_mat_view
        WHERE transactions.verified_cash_transaction_mat_view.book IN (''Sales.Direct'', ''Sales.Receipt'', ''Sales.Delivery'')
        AND account_master_id IN(10101, 10102) 
        AND value_date >=''' || this.date_from::text || ''' AND value_date <=''' || this.date_to::text ||
        ''' AND office_id IN (SELECT * FROM office.get_office_ids(' || _office_id::text || '))
        )
        WHERE cf_temp.is_sales;';
        RAISE NOTICE '%', _SQL;
        EXECUTE _sql;

        --Closing cash balance.
        _sql := 'UPDATE cf_temp SET "' || this.period_name || '"
        =
        (
            SELECT
            SUM(CASE tran_type WHEN ''Cr'' THEN amount_in_local_currency ELSE 0 END) - 
            SUM(CASE tran_type WHEN ''Dr'' THEN amount_in_local_currency ELSE 0 END) AS total_amount
        FROM transactions.verified_cash_transaction_mat_view
        WHERE account_master_id IN(10101, 10102) 
        AND value_date <''' || this.date_to::text ||
        ''' AND office_id IN (SELECT * FROM office.get_office_ids(' || _office_id::text || '))
        ) 
        WHERE cf_temp.item_id = 60000;';

        EXECUTE _sql;

        --Reversing to debit balance for associated headings.
        _sql := 'UPDATE cf_temp SET "' || this.period_name || '"="' || this.period_name || '"*-1 WHERE is_debit=true;';
        EXECUTE _sql;
    END LOOP;



    --Updating periodic balances on parent item by the sum of their respective child balances.
    SELECT 'UPDATE cf_temp SET ' || array_to_string(array_agg('"' || period_name || '"' || '=cf_temp."' || period_name || '" + tran."' || period_name || '"'), ',') || 
    ' FROM 
    (
        SELECT parent_item_id, '
        || array_to_string(array_agg('SUM("' || period_name || '") AS "' || period_name || '"'), ',') || '
         FROM cf_temp
        GROUP BY parent_item_id
    ) 
    AS tran
        WHERE tran.parent_item_id = cf_temp.item_id
        AND cf_temp.item_id NOT IN (10000, 60000);'
    INTO _sql
    FROM explode_array(_periods);

        RAISE NOTICE '%', _SQL;
    EXECUTE _sql;


    SELECT 'UPDATE cf_temp SET ' || array_to_string(array_agg('"' || period_name || '"=tran."' || period_name || '"'), ',') 
    || ' FROM 
    (
        SELECT
            cf_temp.parent_item_id,'
        || array_to_string(array_agg('SUM(CASE is_debit WHEN true THEN "' || period_name || '" ELSE "' || period_name || '" *-1 END) AS "' || period_name || '"'), ',') ||
    '
         FROM cf_temp
         GROUP BY cf_temp.parent_item_id
    ) 
    AS tran
    WHERE cf_temp.item_id = tran.parent_item_id
    AND cf_temp.parent_item_id IS NULL;'
    INTO _sql
    FROM explode_array(_periods);

    EXECUTE _sql;


    --Dividing by the factor.
    SELECT 'UPDATE cf_temp SET ' || array_to_string(array_agg('"' || period_name || '"="' || period_name || '"/' || _factor::text), ',') || ';'
    INTO _sql
    FROM explode_array(_periods);
    EXECUTE _sql;


    --Converting 0's to NULLS.
    SELECT 'UPDATE cf_temp SET ' || array_to_string(array_agg('"' || period_name || '"= CASE WHEN "' || period_name || '" = 0 THEN NULL ELSE "' || period_name || '" END'), ',') || ';'
    INTO _sql
    FROM explode_array(_periods);

    EXECUTE _sql;

    SELECT 
    'SELECT array_to_json(array_agg(row_to_json(report)))
    FROM
    (
        SELECT item, '
        || array_to_string(array_agg('"' || period_name || '"'), ',') ||
        ', is_summation FROM cf_temp
        WHERE account_master_id IS NULL
        ORDER BY item_id
    ) AS report;'
    INTO _sql
    FROM explode_array(_periods);

    EXECUTE _sql INTO _json ;

    RETURN _json;
END
$$
LANGUAGE plpgsql;

--SELECT transactions.get_cash_flow_statement('1-1-2000','1-15-2020', 2, 2, 1)