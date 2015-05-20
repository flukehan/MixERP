DROP FUNCTION IF EXISTS core.get_ordered_quantity(_item_id integer, _unit_id integer, _office_id integer);

CREATE FUNCTION core.get_ordered_quantity(_item_id integer, _unit_id integer, _office_id integer)
RETURNS numeric
AS
$$
        DECLARE last_received_on date;
        DECLARE factor decimal(24, 8);
BEGIN
        SELECT 
        MAX(transactions.transaction_master.value_date) INTO last_received_on
        FROM transactions.transaction_master
        INNER JOIN transactions.stock_master
        ON transactions.transaction_master.transaction_master_id = transactions.stock_master.transaction_master_id
        INNER JOIN transactions.stock_details
        ON transactions.stock_master.stock_master_id = transactions.stock_details.stock_master_id
        WHERE transactions.stock_details.item_id = $1
        AND transactions.transaction_master.office_id = $3        
        AND transactions.transaction_master.book like 'Purchase%';

        RAISE NOTICE '%', last_received_on;

        RETURN COALESCE(SUM(quantity * core.convert_unit(unit_id, $2)), 0)
        FROM transactions.non_gl_stock_details
        INNER JOIN transactions.non_gl_stock_master
        ON transactions.non_gl_stock_details.non_gl_stock_master_id = transactions.non_gl_stock_master.non_gl_stock_master_id
        WHERE transactions.non_gl_stock_master.office_id = $3        
        AND item_id = $1
        AND transactions.non_gl_stock_details.value_date > last_received_on
        AND transactions.non_gl_stock_master.book = 'Purchase.Order';
        
END
$$
LANGUAGE plpgsql;



--SELECT core.get_ordered_quantity(17, 1, 2);
