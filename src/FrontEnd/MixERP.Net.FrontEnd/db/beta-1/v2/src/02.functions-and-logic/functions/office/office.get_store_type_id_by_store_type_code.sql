DROP FUNCTION IF EXISTS office.get_store_type_id_by_store_type_code(text);

CREATE FUNCTION office.get_store_type_id_by_store_type_code(_store_type_code text)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN store_type_id
    FROM office.store_types
    WHERE store_type_code=$1;
END
$$
LANGUAGE plpgsql;