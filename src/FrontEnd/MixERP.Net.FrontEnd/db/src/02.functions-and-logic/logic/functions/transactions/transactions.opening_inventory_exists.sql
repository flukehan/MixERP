DROP FUNCTION IF EXISTS transactions.opening_inventory_exists
(
    _office_id          integer
);

CREATE FUNCTION transactions.opening_inventory_exists
(
    _office_id          integer
)
RETURNS boolean
STABLE
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT 1
        FROM transactions.transaction_master
        WHERE book = 'Opening.Inventory'
        AND office_id = _office_id
    ) THEN
        RETURN true;
    END IF;

    RETURN false;
END
$$
LANGUAGE plpgsql;