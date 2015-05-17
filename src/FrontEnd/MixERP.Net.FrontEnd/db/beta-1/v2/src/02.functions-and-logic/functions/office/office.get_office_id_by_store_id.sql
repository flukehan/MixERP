DROP FUNCTION IF EXISTS office.get_office_id_by_store_id(integer);

CREATE FUNCTION office.get_office_id_by_store_id(_store_id integer)
RETURNS integer
AS
$$
BEGIN
        RETURN office_id
        FROM office.stores
        WHERE store_id=$1;
END
$$
LANGUAGE plpgsql;


