DROP FUNCTION IF EXISTS core.is_stock_item(item_id integer);

CREATE FUNCTION core.is_stock_item(item_id integer)
RETURNS bool
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT 1 FROM core.items WHERE core.items.item_id=$1 AND maintain_stock=true
    ) THEN
        RETURN true;
    END IF;

    RETURN false;
END
$$
LANGUAGE plpgsql;

DROP FUNCTION IF EXISTS core.is_stock_item(item_code national character varying(12));

CREATE FUNCTION core.is_stock_item(item_code national character varying(12))
RETURNS bool
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT 1 FROM core.items WHERE core.items.item_code=$1 AND maintain_stock=true
    ) THEN
        RETURN true;
    END IF;

    RETURN false;
END
$$
LANGUAGE plpgsql;
