CREATE FUNCTION core.get_price_type_name_by_price_type_id(integer)
RETURNS text
AS
$$
BEGIN
    RETURN
    (
        SELECT price_type_name
        FROM core.price_types
        WHERE price_type_id=$1
    );
END
$$
LANGUAGE plpgsql;
