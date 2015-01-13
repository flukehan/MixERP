DROP FUNCTION IF EXISTS transactions.get_balance_sheet
(
    _previous_period                date,
    _current_period                 date,
    _user_id                        integer,
    _office_id                      integer,
    _factor                         integer
);

CREATE FUNCTION transactions.get_balance_sheet
(
    _previous_period                date,
    _current_period                 date,
    _user_id                        integer,
    _office_id                      integer,
    _factor                         integer
)
RETURNS TABLE
(
    id                              bigint,
    item                            text,
    previous_period                 decimal(24, 4),
    current_period                  decimal(24, 4),
    account_id                      integer,
    account_number                  text,
    is_retained_earning             boolean
)
AS
$$
    DECLARE this                    RECORD;
    DECLARE _date_from              date;
BEGIN
    _date_from := core.get_fiscal_year_start_date(_office_id);

    IF(COALESCE(_factor, 0) = 0) THEN
        _factor := 1;
    END IF;

    DROP TABLE IF EXISTS bs_temp;
    CREATE TEMPORARY TABLE bs_temp
    (
        item_id                     int PRIMARY KEY,
        item                        text,
        account_number              text,
        account_id                  integer,
        child_accounts              integer[],
        parent_item_id              integer REFERENCES bs_temp(item_id),
        is_debit                    boolean DEFAULT(false),
        previous_period             decimal(24, 4) DEFAULT(0),
        current_period              decimal(24, 4) DEFAULT(0),
        sort                        int,
        skip                        boolean DEFAULT(false),
        is_retained_earning         boolean DEFAULT(false)
    ) ON COMMIT DROP;
    
    --BS structure setup start
    INSERT INTO bs_temp(item_id, item, parent_item_id)
    SELECT  1,       'Assets',                              NULL::numeric   UNION ALL
    SELECT  10100,   'Current Assets',                      1               UNION ALL
    SELECT  10101,   'Cash A/C',                            1               UNION ALL
    SELECT  10102,   'Bank A/C',                            1               UNION ALL
    SELECT  10110,   'Accounts Receivable',                 10100           UNION ALL
    SELECT  10200,   'Fixed Assets',                        1               UNION ALL
    SELECT  10201,   'Property, Plants, and Equipments',    10201           UNION ALL
    SELECT  10300,   'Other Assets',                        1               UNION ALL
    SELECT  14900,   'Liabilities & Shareholders'' Equity', NULL            UNION ALL
    SELECT  15000,   'Current Liabilities',                 14900           UNION ALL
    SELECT  15010,   'Accounts Payable',                    15000           UNION ALL
    SELECT  15011,   'Salary Payable',                      15000           UNION ALL
    SELECT  15100,   'Long-Term Liabilities',               14900           UNION ALL
    SELECT  15200,   'Shareholders'' Equity',               14900           UNION ALL
    SELECT  15300,   'Retained Earnings',                   15200;

    UPDATE bs_temp SET is_debit = true WHERE bs_temp.item_id <= 10300;
    UPDATE bs_temp SET is_retained_earning = true WHERE bs_temp.item_id = 15300;
    
    INSERT INTO bs_temp(item_id, account_id, account_number, parent_item_id, item, is_debit, child_accounts)
    SELECT 
        row_number() OVER(ORDER BY core.accounts.account_master_id) + (core.accounts.account_master_id * 100) AS id,
        core.accounts.account_id,
        core.accounts.account_number,
        core.accounts.account_master_id,
        core.accounts.account_name,
        core.account_masters.normally_debit,
        array_agg(agg)
    FROM core.accounts
    INNER JOIN core.account_masters
    ON core.accounts.account_master_id = core.account_masters.account_master_id,
    core.get_account_ids(core.accounts.account_id) as agg
    WHERE parent_account_id IN
    (
        SELECT core.accounts.account_id
        FROM core.accounts
        WHERE core.accounts.sys_type
        AND core.accounts.account_master_id BETWEEN 10100 AND 15200
    )
    AND core.accounts.account_master_id BETWEEN 10100 AND 15200
    GROUP BY core.accounts.account_id, core.account_masters.normally_debit
    ORDER BY account_master_id;


    --Updating credit balances of individual GL accounts.
    UPDATE bs_temp SET previous_period = tran.previous_period
    FROM
    (
        SELECT 
            bs_temp.account_id,         
            SUM(CASE tran_type WHEN 'Cr' THEN amount_in_local_currency ELSE amount_in_local_currency * -1 END) AS previous_period
        FROM bs_temp
        INNER JOIN transactions.verified_transaction_mat_view
        ON transactions.verified_transaction_mat_view.account_id = ANY(bs_temp.child_accounts)
        WHERE value_date <=_previous_period
        AND office_id IN (SELECT * FROM office.get_office_ids(_office_id))
        GROUP BY bs_temp.account_id
    ) AS tran
    WHERE bs_temp.account_id = tran.account_id;

    --Updating credit balances of individual GL accounts.
    UPDATE bs_temp SET current_period = tran.current_period
    FROM
    (
        SELECT 
            bs_temp.account_id,         
            SUM(CASE tran_type WHEN 'Cr' THEN amount_in_local_currency ELSE amount_in_local_currency * -1 END) AS current_period
        FROM bs_temp
        INNER JOIN transactions.verified_transaction_mat_view
        ON transactions.verified_transaction_mat_view.account_id = ANY(bs_temp.child_accounts)
        WHERE value_date <=_current_period
        AND office_id IN (SELECT * FROM office.get_office_ids(_office_id))
        GROUP BY bs_temp.account_id
    ) AS tran
    WHERE bs_temp.account_id = tran.account_id;


    --Dividing by the factor.
    UPDATE bs_temp SET 
        previous_period = bs_temp.previous_period / _factor,
        current_period = bs_temp.current_period / _factor;

    --Upading balance of retained earnings
    UPDATE bs_temp SET 
        previous_period = transactions.get_retained_earnings(_previous_period, _office_id, _factor),
        current_period = transactions.get_retained_earnings(_current_period, _office_id, _factor)
    WHERE bs_temp.item_id = 15300;

    --Reversing assets to debit balance.
    UPDATE bs_temp SET 
        previous_period=bs_temp.previous_period*-1,
        current_period=bs_temp.current_period*-1 
    WHERE bs_temp.is_debit;



    FOR this IN 
    SELECT * FROM bs_temp 
    WHERE COALESCE(bs_temp.previous_period, 0) + COALESCE(bs_temp.current_period, 0) != 0 
    AND bs_temp.account_id IS NOT NULL
    LOOP
        UPDATE bs_temp SET skip = true WHERE this.account_id = ANY(bs_temp.child_accounts)
        AND bs_temp.account_id != this.account_id;
    END LOOP;

    --Updating current period amount on GL parent item by the sum of their respective child balances.
    WITH running_totals AS
    (
        SELECT bs_temp.parent_item_id,
        SUM(COALESCE(bs_temp.previous_period, 0)) AS previous_period,
        SUM(COALESCE(bs_temp.current_period, 0)) AS current_period
        FROM bs_temp
        WHERE NOT skip
        AND parent_item_id IS NOT NULL
        GROUP BY bs_temp.parent_item_id
    )
    UPDATE bs_temp SET 
        previous_period = running_totals.previous_period,
        current_period = running_totals.current_period
    FROM running_totals
    WHERE running_totals.parent_item_id = bs_temp.item_id
    AND bs_temp.item_id
    IN
    (
        SELECT parent_item_id FROM running_totals
    );


    --Updating sum amount on parent item by the sum of their respective child balances.
    UPDATE bs_temp SET 
        previous_period = tran.previous_period,
        current_period = tran.current_period
    FROM 
    (
        SELECT bs_temp.parent_item_id,
        SUM(bs_temp.previous_period) AS previous_period,
        SUM(bs_temp.current_period) AS current_period
        FROM bs_temp
        WHERE bs_temp.parent_item_id IS NOT NULL
        GROUP BY bs_temp.parent_item_id
    ) 
    AS tran 
    WHERE tran.parent_item_id = bs_temp.item_id
    AND tran.parent_item_id IS NOT NULL;


    --Updating sum amount on grandparents.
    UPDATE bs_temp SET 
        previous_period = tran.previous_period,
        current_period = tran.current_period
    FROM 
    (
        SELECT bs_temp.parent_item_id,
        SUM(bs_temp.previous_period) AS previous_period,
        SUM(bs_temp.current_period) AS current_period
        FROM bs_temp
        WHERE bs_temp.parent_item_id IS NOT NULL
        GROUP BY bs_temp.parent_item_id
    ) 
    AS tran 
    WHERE tran.parent_item_id = bs_temp.item_id;

    --Removing ledgers having zero balances
    DELETE FROM bs_temp
    WHERE COALESCE(bs_temp.previous_period, 0) + COALESCE(bs_temp.current_period, 0) = 0
    AND bs_temp.account_id IS NOT NULL;

    --Converting 0's to NULLS.
    UPDATE bs_temp SET previous_period = CASE WHEN bs_temp.previous_period = 0 THEN NULL ELSE bs_temp.previous_period END;
    UPDATE bs_temp SET current_period = CASE WHEN bs_temp.current_period = 0 THEN NULL ELSE bs_temp.current_period END;
    
    UPDATE bs_temp SET sort = bs_temp.item_id WHERE bs_temp.item_id < 15400;
    UPDATE bs_temp SET sort = bs_temp.parent_item_id WHERE bs_temp.item_id >= 15400;

    RETURN QUERY
    SELECT
        row_number() OVER(order by bs_temp.sort, bs_temp.item_id) AS id,
        bs_temp.item,
        bs_temp.previous_period,
        bs_temp.current_period,
        bs_temp.account_id,
        bs_temp.account_number,
        bs_temp.is_retained_earning
    FROM bs_temp;
END;
$$
LANGUAGE plpgsql;

--SELECT * FROM transactions.get_balance_sheet('7/17/2014', '7/16/2015', 2, 2, 1000);