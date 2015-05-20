DROP FUNCTION IF EXISTS transactions.get_cost_of_goods_sold(_item_id integer, _unit_id integer, _store_id integer, _quantity integer);

CREATE FUNCTION transactions.get_cost_of_goods_sold(_item_id integer, _unit_id integer, _store_id integer, _quantity integer)
RETURNS money_strict
AS
$$
    DECLARE _base_quantity decimal;
    DECLARE _base_unit_id integer;
    DECLARE _base_unit_cost money_strict;
    DECLARE _total_sold integer;
    DECLARE _office_id integer      = office.get_office_id_by_store_id($3);
    DECLARE _method text            = office.get_cost_of_good_method(_office_id);
BEGIN
    _base_quantity   = core.get_base_quantity_by_unit_id($2, $4);
    _base_unit_id    = core.get_root_unit_id($2);


    IF(_method = 'MAVCO') THEN
            --RAISE NOTICE '% % % %',_item_id, _store_id, _base_quantity, 1.00;
            RETURN transactions.get_mavcogs(_item_id, _store_id, _base_quantity, 1.00);
    END IF;


    DROP TABLE IF EXISTS temp_cost_of_goods_sold;
    CREATE TEMPORARY TABLE temp_cost_of_goods_sold
    (
            id                     BIGSERIAL,
            stock_detail_id        bigint,
            audit_ts               TIMESTAMP WITH TIME ZONE,
            value_date             date,
            price                  money_strict,
            tran_type              text
                    
    ) ON COMMIT DROP;

    WITH stock_cte AS
    (
        SELECT
            stock_detail_id, 
            audit_ts,
            value_date,
            generate_series(1, base_quantity::integer) AS series,
            (price * quantity) / base_quantity AS price,
            tran_type
        FROM transactions.verified_stock_details_view
        WHERE item_id = $1
        AND store_id = $3
    )
    
    INSERT INTO temp_cost_of_goods_sold(stock_detail_id, audit_ts, value_date, price, tran_type)
    SELECT stock_detail_id, audit_ts, value_date, price, tran_type FROM stock_cte
    ORDER BY value_date, audit_ts, stock_detail_id;

    SELECT COUNT(*) INTO _total_sold 
    FROM temp_cost_of_goods_sold
    WHERE tran_type='Cr';

    IF(_method = 'LIFO') THEN
        SELECT SUM(price) INTO _base_unit_cost
        FROM 
        (
                SELECT price
                FROM temp_cost_of_goods_sold
                WHERE tran_type ='Dr'
                ORDER BY id DESC
                OFFSET _total_sold
                LIMIT _base_quantity
        ) S;
    ELSIF (_method = 'FIFO') THEN
        SELECT SUM(price) INTO _base_unit_cost
        FROM 
        (
                SELECT price
                FROM temp_cost_of_goods_sold
                WHERE tran_type ='Dr'
                ORDER BY id
                OFFSET _total_sold
                LIMIT _base_quantity
        ) S;
    ELSIF (_method != 'MAVCO') THEN
        RAISE EXCEPTION 'Invalid configuration: COGS method.'
        USING ERRCODE='P6010';
    END IF;

    RETURN _base_unit_cost;
END
$$
LANGUAGE PLPGSQL;

-- UPDATE office.configuration
-- SET value = 'MAVCO'
-- WHERE config_id = 2;
-- 
-- 
--SELECT * FROM transactions.get_cost_of_goods_sold(1, 1, 1, 1);


-- 
-- 
-- 
--SELECT * FROM transactions.get_cost_of_goods_sold(1, 7, 1, 1);
