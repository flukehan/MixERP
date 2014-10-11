DROP FUNCTION IF EXISTS core.is_valid_item_id(integer);

CREATE FUNCTION core.is_valid_item_id(integer)
RETURNS boolean
AS
$$
BEGIN
        IF EXISTS(SELECT 1 FROM core.items WHERE item_id=$1) THEN
                RETURN true;
        END IF;

        RETURN false;
END
$$
LANGUAGE plpgsql;