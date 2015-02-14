DROP FUNCTION IF EXISTS core.get_item_type_id_by_item_type_code(text);

CREATE FUNCTION core.get_item_type_id_by_item_type_code(text)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN item_type_id
    FROM core.item_types
    WHERE item_type_code=$1;
END
$$
LANGUAGE plpgsql;