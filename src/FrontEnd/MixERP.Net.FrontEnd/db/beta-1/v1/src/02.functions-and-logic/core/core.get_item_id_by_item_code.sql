CREATE FUNCTION core.get_item_id_by_item_code(text)
RETURNS integer
AS
$$
BEGIN
    RETURN
    (
        SELECT
            item_id
        FROM
            core.items
        WHERE 
            core.items.item_code=$1
    );
END
$$
LANGUAGE plpgsql;
