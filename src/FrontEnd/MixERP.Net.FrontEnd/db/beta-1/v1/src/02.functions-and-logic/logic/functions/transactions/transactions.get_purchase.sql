DROP FUNCTION IF EXISTS transactions.get_purchase
(
    _date_from          date,
    _date_to            date,
    _office_id          integer
);

CREATE FUNCTION transactions.get_purchase
(
    _date_from          date,
    _date_to            date,
    _office_id          integer
)
RETURNS decimal(24, 4)
AS
$$
BEGIN
    RETURN 0;--TODO
END
$$
LANGUAGE plpgsql;

SELECT transactions.get_purchase('2-3-30', '1-1-10', 2);