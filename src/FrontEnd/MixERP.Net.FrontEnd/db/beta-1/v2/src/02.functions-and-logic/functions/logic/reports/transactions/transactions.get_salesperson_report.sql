DROP FUNCTION IF EXISTS transactions.get_salesperson_report(_office_id integer, _factor integer);

CREATE FUNCTION transactions.get_salesperson_report(_office_id integer, _factor integer)
RETURNS TABLE
(
    id                  integer,
    salesperson_id      integer,
    salesperson_name    text,
    total_sales         decimal(24)
)
AS
$$
    DECLARE fy_start    date;
BEGIN
    DROP TABLE IF EXISTS temp_sales;
    
    CREATE TEMPORARY TABLE temp_sales
    (
        id                  SERIAL,
        salesperson_id      integer,
        salesperson_name    text,
        total_sales         decimal(24)
    ) ON COMMIT DROP;

    fy_start := core.get_fiscal_year_start_date(_office_id);
    
    INSERT INTO temp_sales(salesperson_id, salesperson_name, total_sales)
    SELECT
        transactions.verified_stock_transaction_view.salesperson_id, 
        '', 
        SUM(transactions.verified_stock_transaction_view.amount) / _factor
    FROM transactions.verified_stock_transaction_view
    WHERE book LIKE 'Sales%'
    AND value_date >= fy_start
    AND transactions.verified_stock_transaction_view.office_id IN (SELECT * FROM office.get_office_ids(_office_id)) 
    GROUP BY transactions.verified_stock_transaction_view.salesperson_id
    ORDER BY 3 DESC
    LIMIT 5;

    UPDATE temp_sales
    SET salesperson_name = core.salespersons.salesperson_name
    FROM core.salespersons
    WHERE core.salespersons.salesperson_id = temp_sales.salesperson_id;
    
    RETURN QUERY
    SELECT * FROM temp_sales
    ORDER BY id;
END
$$
LANGUAGE plpgsql;

--SELECT * FROM transactions.get_salesperson_report(1, 1000);