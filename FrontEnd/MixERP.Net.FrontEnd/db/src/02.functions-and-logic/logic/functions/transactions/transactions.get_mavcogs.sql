DROP FUNCTION IF EXISTS transactions.get_mavcogs(_item_id integer, _store_id integer, _base_quantity decimal, _factor decimal(24, 4));

CREATE FUNCTION transactions.get_mavcogs(_item_id integer, _store_id integer, _base_quantity decimal, _factor decimal(24, 4))
RETURNS decimal(24, 4)
AS
$$
        DECLARE _base_unit_cost money_strict;
BEGIN
        CREATE TEMPORARY TABLE temp_staging
        (
                id              SERIAL NOT NULL,
                value_date      date,
                audit_ts        TIMESTAMP WITH TIME ZONE,
                base_quantity   decimal,
                price           decimal
                
        ) ON COMMIT DROP;


        INSERT INTO temp_staging(value_date, audit_ts, base_quantity, price)
        SELECT value_date, audit_ts, 
        CASE WHEN tran_type = 'Dr' THEN
        base_quantity ELSE base_quantity  * -1 END, 
        CASE WHEN tran_type = 'Dr' THEN
        (price * quantity/base_quantity)
        ELSE
        0
        END
        FROM transactions.verified_stock_details_view
        WHERE item_id = $1
        AND store_id=$2
        order by value_date, audit_ts, stock_detail_id;




        WITH RECURSIVE stock_transaction(id, base_quantity, price, sum_m, sum_base_quantity, last_id) AS 
        (
          SELECT id, base_quantity, price, base_quantity * price, base_quantity, id
          FROM temp_staging WHERE id = 1
          UNION ALL
          SELECT child.id, child.base_quantity, 
                 CASE WHEN child.base_quantity < 0 then parent.sum_m / parent.sum_base_quantity ELSE child.price END, 
                 parent.sum_m + CASE WHEN child.base_quantity < 0 then parent.sum_m / parent.sum_base_quantity ELSE child.price END * child.base_quantity,
                 parent.sum_base_quantity + child.base_quantity,
                 child.id 
          FROM temp_staging child JOIN stock_transaction parent on child.id = parent.last_id + 1
        )

        SELECT 
                --base_quantity,                                                        --left for debuging purpose
                --price,                                                                --left for debuging purpose
                --base_quantity * price AS amount,                                      --left for debuging purpose
                --SUM(base_quantity * price) OVER(ORDER BY id) AS cv_amount,            --left for debuging purpose
                --SUM(base_quantity) OVER(ORDER BY id) AS cv_quantity,                  --left for debuging purpose
                SUM(base_quantity * price) OVER(ORDER BY id)  / SUM(base_quantity) OVER(ORDER BY id) INTO _base_unit_cost
        FROM stock_transaction
        ORDER BY id DESC
        LIMIT 1;

        RETURN _base_unit_cost * _factor * _base_quantity;
END
$$
LANGUAGE plpgsql;

