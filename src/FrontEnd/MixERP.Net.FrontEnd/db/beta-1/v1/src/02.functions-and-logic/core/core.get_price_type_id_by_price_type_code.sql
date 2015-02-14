DROP FUNCTION IF EXISTS core.get_price_type_id_by_price_type_code(text);

CREATE FUNCTION core.get_price_type_id_by_price_type_code(text)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN price_type_id
    FROM core.price_types
    WHERE price_type_code=$1;
END
$$
LANGUAGE plpgsql;