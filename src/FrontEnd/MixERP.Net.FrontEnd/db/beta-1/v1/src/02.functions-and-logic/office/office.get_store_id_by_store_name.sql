CREATE FUNCTION office.get_store_id_by_store_name(text)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN
    (
        SELECT store_id
        FROM office.stores
        WHERE store_name=$1
    );
END
$$
LANGUAGE plpgsql;
