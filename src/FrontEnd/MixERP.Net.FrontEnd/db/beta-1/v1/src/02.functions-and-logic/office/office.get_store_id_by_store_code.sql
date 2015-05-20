DROP FUNCTION IF EXISTS office.get_store_id_by_store_code(text);

CREATE FUNCTION office.get_store_id_by_store_code(text)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN
    (
        SELECT store_id
        FROM office.stores
        WHERE store_code=$1
    );
END
$$
LANGUAGE plpgsql;
