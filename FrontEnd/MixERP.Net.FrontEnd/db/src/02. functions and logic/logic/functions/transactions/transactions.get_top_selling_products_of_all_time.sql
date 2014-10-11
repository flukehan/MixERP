
DROP FUNCTION IF EXISTS transactions.get_top_selling_products_of_all_time(top int);

CREATE FUNCTION transactions.get_top_selling_products_of_all_time(top int)
RETURNS TABLE
(
        id              integer,
        item_id         integer,
        item_code       text,
        item_name       text,
        total_sales     numeric
)
AS
$$
BEGIN
        CREATE TEMPORARY TABLE IF NOT EXISTS top_selling_products_of_all_time
        (
                id              integer,
                item_id         integer,
                item_code       text,
                item_name       text,
                total_sales     numeric
        ) ON COMMIT DROP;

        INSERT INTO top_selling_products_of_all_time(id, item_id, total_sales)
        SELECT ROW_NUMBER() OVER(), *
        FROM
        (
                SELECT         
                        transactions.verified_stock_transaction_view.item_id, 
                        SUM((price * quantity) - discount + tax) AS sales_amount
                FROM transactions.verified_stock_transaction_view
                GROUP BY transactions.verified_stock_transaction_view.item_id
                ORDER BY 2 DESC
                LIMIT $1
        ) t;

        UPDATE top_selling_products_of_all_time AS t
        SET 
                item_code = core.items.item_code,
                item_name = core.items.item_name
        FROM core.items
        WHERE t.item_id = core.items.item_id;
        

        RETURN QUERY
        SELECT * FROM top_selling_products_of_all_time;
END
$$
LANGUAGE plpgsql;

DROP FUNCTION IF EXISTS transactions.get_top_selling_products_of_all_time();

CREATE FUNCTION transactions.get_top_selling_products_of_all_time()
RETURNS TABLE
(
        id              integer,
        item_id         integer,
        item_code       text,
        item_name       text,
        total_sales     numeric
)
AS
$$
BEGIN
        RETURN QUERY
        SELECT * FROM transactions.get_top_selling_products_of_all_time(5);
END
$$
LANGUAGE plpgsql;


--SELECT * FROM transactions.get_top_selling_products_of_all_time();

