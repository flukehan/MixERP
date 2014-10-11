DROP FUNCTION IF EXISTS transactions.get_top_selling_products_by_office(_office_id integer, top integer);

CREATE FUNCTION transactions.get_top_selling_products_by_office(_office_id integer, top integer)
RETURNS TABLE
(
        id              integer,
        office_id       integer,
        office_code     text,
        office_name     text,
        item_id         integer,
        item_code       text,
        item_name       text,
        total_sales     numeric
)
AS
$$
BEGIN
        CREATE TEMPORARY TABLE top_selling_products
        (
                item_id integer
        ) ON COMMIT DROP;

        INSERT INTO top_selling_products
        SELECT t.item_id FROM transactions.get_top_selling_products_of_all_time(top) AS t;


        CREATE TEMPORARY TABLE top_selling_products_by_office
        (
                id              SERIAL,
                office_id       integer,
                office_code     text,
                office_name     text,
                item_id         integer,
                item_code       text,
                item_name       text,
                total_sales     numeric
        ) ON COMMIT DROP;


        INSERT INTO top_selling_products_by_office(office_id, item_id, total_sales)
        SELECT
                transactions.verified_stock_transaction_view.office_id,
                transactions.verified_stock_transaction_view.item_id, 
                SUM((price * quantity) - discount + tax) AS sales_amount
        FROM transactions.verified_stock_transaction_view
        WHERE transactions.verified_stock_transaction_view.item_id IN (SELECT top_selling_products.item_id FROM top_selling_products)
        AND transactions.verified_stock_transaction_view.office_id IN (SELECT * FROM office.get_office_ids(_office_id))
        GROUP BY 
                transactions.verified_stock_transaction_view.office_id, 
                transactions.verified_stock_transaction_view.item_id
        ORDER BY sales_amount DESC, item_id ASC;


        UPDATE top_selling_products_by_office AS t
        SET 
                item_code = core.items.item_code,
                item_name = core.items.item_name
        FROM core.items
        WHERE t.item_id = core.items.item_id;


        UPDATE top_selling_products_by_office AS t
        SET 
                office_code = office.offices.office_code,
                office_name= office.offices.office_name
        FROM office.offices
        WHERE t.office_id = office.offices.office_id;


        RETURN QUERY 
        SELECT * FROM top_selling_products_by_office;
END
$$
LANGUAGE plpgsql;

DROP FUNCTION IF EXISTS transactions.get_top_selling_products_by_office();

CREATE FUNCTION transactions.get_top_selling_products_by_office()
RETURNS TABLE
(
        id              integer,
        office_id       integer,
        office_code     text,
        office_name     text,
        item_id         integer,
        item_code       text,
        item_name       text,
        total_sales     numeric
)
AS
$$
    DECLARE root_office_id integer = 0;
BEGIN
    SELECT office.offices.office_id INTO root_office_id
    FROM office.offices
    WHERE parent_office_id IS NULL
    LIMIT 1;

        RETURN QUERY 
        SELECT * FROM transactions.get_top_selling_products_by_office(root_office_id, 5);
END
$$
LANGUAGE plpgsql;


--SELECT  id, office_code, item_name, total_sales FROM transactions.get_top_selling_products_by_office()
