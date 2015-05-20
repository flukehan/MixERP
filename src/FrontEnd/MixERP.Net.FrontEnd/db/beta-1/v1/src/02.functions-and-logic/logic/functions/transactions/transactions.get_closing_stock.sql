DROP FUNCTION IF EXISTS transactions.get_closing_stock
(
    _on_date            date,
    _office_id          integer
);

CREATE FUNCTION transactions.get_closing_stock
(
    _on_date            date,
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