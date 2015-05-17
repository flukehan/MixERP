DROP FUNCTION IF EXISTS transactions.settle_party_due(_party_id bigint, _office_id integer);

CREATE FUNCTION transactions.settle_party_due(_party_id bigint, _office_id integer)
RETURNS void
STRICT VOLATILE
AS
$$
    DECLARE _settled_transactions           bigint[];
    DECLARE _settling_amount                numeric;
    DECLARE _closing_balance                numeric;
    DECLARE _total_sales                    numeric;
    DECLARE _party_account_id               bigint = core.get_account_id_by_party_id(_party_id);
BEGIN   
    --Closing balance of the party
    SELECT
        SUM
        (
            CASE WHEN tran_type = 'Cr' 
            THEN amount_in_local_currency 
            ELSE amount_in_local_currency  * -1 
            END
        ) INTO _closing_balance
    FROM transactions.transaction_details
    INNER JOIN transactions.transaction_master
    ON transactions.transaction_master.transaction_master_id = transactions.transaction_details.transaction_master_id
    WHERE transactions.transaction_master.verification_status_id > 0
    AND transactions.transaction_master.office_id = _office_id
    AND transactions.transaction_details.account_id = _party_account_id;


    --Since party account is receivable, change the balance to debit
    _closing_balance := _closing_balance * -1;

    --Sum of total sales amount
    SELECT 
        SUM
        (
            (transactions.stock_details.quantity * transactions.stock_details.price) 
            - 
            transactions.stock_details.discount 
            + 
            transactions.stock_details.tax + 
            transactions.stock_details.shipping_charge
        ) INTO _total_sales
    FROM transactions.stock_master
    INNER JOIN transactions.stock_details
    ON transactions.stock_master.stock_master_id = transactions.stock_details.stock_master_id
    INNER JOIN transactions.transaction_master
    ON transactions.stock_master.transaction_master_id = transactions.transaction_master.transaction_master_id
    WHERE transactions.transaction_master.book = ANY(ARRAY['Sales.Direct', 'Sales.Delivery'])
    AND transactions.transaction_master.verification_status_id > 0
    AND transactions.transaction_master.office_id = _office_id
    AND party_id = _party_id;


    _settling_amount := _total_sales - _closing_balance;

    --Note--
    --Can take advantage of temporary table instead of CTE here, if this query performs slow in future.--
    WITH all_sales
    AS
    (
        SELECT 
            transactions.stock_master.transaction_master_id,
            SUM
            (
                (transactions.stock_details.quantity * transactions.stock_details.price) 
                - 
                transactions.stock_details.discount 
                + 
                transactions.stock_details.tax + 
                transactions.stock_details.shipping_charge
            ) as due
        FROM transactions.stock_master
        INNER JOIN transactions.stock_details
        ON transactions.stock_master.stock_master_id = transactions.stock_details.stock_master_id
        INNER JOIN transactions.transaction_master
        ON transactions.stock_master.transaction_master_id = transactions.transaction_master.transaction_master_id
        WHERE transactions.transaction_master.book = ANY(ARRAY['Sales.Direct', 'Sales.Delivery'])
        AND transactions.transaction_master.office_id = _office_id
        AND transactions.transaction_master.verification_status_id > 0      --Approved
        AND party_id = _party_id                                            --of this party
        GROUP BY transactions.stock_master.transaction_master_id
    ),
    sales_summary
    AS
    (
        SELECT 
            transaction_master_id, 
            due, 
            SUM(due) OVER(ORDER BY transaction_master_id) AS cumulative_due
        FROM all_sales
    )

    SELECT 
        ARRAY_AGG(transaction_master_id) INTO _settled_transactions
    FROM sales_summary
    WHERE cumulative_due <= _settling_amount;

    UPDATE transactions.stock_master
    SET credit_settled = true
    WHERE transaction_master_id = ANY(_settled_transactions);
END
$$
LANGUAGE plpgsql;

--SELECT * FROM transactions.settle_party_due(1, 2);