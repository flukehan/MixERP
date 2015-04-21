DROP FUNCTION IF EXISTS office.get_store_name_by_store_id(integer);

CREATE FUNCTION office.get_store_name_by_store_id(_store_id integer)
RETURNS text
AS
$$
BEGIN
    RETURN
    (
        SELECT store_name
        FROM office.stores
        WHERE store_id=$1
    );
END
$$
LANGUAGE plpgsql;
