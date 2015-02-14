DROP FUNCTION IF EXISTS core.get_salesperson_id_by_salesperson_code(text);

CREATE FUNCTION core.get_salesperson_id_by_salesperson_code(text)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN salesperson_id
    FROM core.salespersons
    WHERE salesperson_code=$1;
END
$$
LANGUAGE plpgsql;