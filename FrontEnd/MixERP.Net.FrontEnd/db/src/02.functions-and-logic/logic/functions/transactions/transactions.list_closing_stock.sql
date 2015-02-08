DROP FUNCTION IF EXISTS transactions.list_closing_stock
(
    _store_id               integer
);

CREATE FUNCTION transactions.list_closing_stock
(
    _store_id               integer
)
RETURNS
TABLE
(
    item_id                 integer,
    item_code               text,
    item_name               text,
    unit_id                 integer,
    unit_name               text,
    quantity                integer
)
AS
$$
BEGIN
    DROP TABLE IF EXISTS temp_closing_stock;

    CREATE TEMPORARY TABLE temp_closing_stock
    (
        item_id             integer,
        item_code           text,
        item_name           text,
        unit_id             integer,
        unit_name           text,
        quantity            integer,
        maintain_stock      boolean
    ) ON COMMIT DROP;

    INSERT INTO temp_closing_stock(item_id, unit_id, quantity)
    SELECT 
        transactions.verified_stock_details_view.item_id, 
        transactions.verified_stock_details_view.base_unit_id,
        SUM(CASE WHEN transactions.verified_stock_details_view.tran_type='Dr' THEN transactions.verified_stock_details_view.base_quantity ELSE transactions.verified_stock_details_view.base_quantity * -1 END)
    FROM transactions.verified_stock_details_view
    WHERE transactions.verified_stock_details_view.store_id = _store_id
    GROUP BY transactions.verified_stock_details_view.item_id, transactions.verified_stock_details_view.store_id, transactions.verified_stock_details_view.base_unit_id;

    UPDATE temp_closing_stock SET 
        item_code = core.items.item_code,
        item_name = core.items.item_name,
        maintain_stock = core.items.maintain_stock
    FROM core.items
    WHERE temp_closing_stock.item_id = core.items.item_id;

    DELETE FROM temp_closing_stock WHERE NOT temp_closing_stock.maintain_stock;

    UPDATE temp_closing_stock SET 
        unit_name = core.units.unit_name
    FROM core.units
    WHERE temp_closing_stock.unit_id = core.units.unit_id;

    RETURN QUERY
    SELECT 
        temp_closing_stock.item_id, 
        temp_closing_stock.item_code, 
        temp_closing_stock.item_name, 
        temp_closing_stock.unit_id, 
        temp_closing_stock.unit_name, 
        temp_closing_stock.quantity
    FROM temp_closing_stock
    ORDER BY item_id;
END;
$$
LANGUAGE plpgsql;

COMMENT ON FUNCTION transactions.list_closing_stock(integer) 
IS 'Lists stock items, their respective base units, and closing stock quantity.';

--SELECT * FROM transactions.list_closing_stock(1);