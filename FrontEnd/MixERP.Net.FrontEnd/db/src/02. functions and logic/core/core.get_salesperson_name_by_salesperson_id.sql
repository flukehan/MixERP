CREATE FUNCTION core.get_salesperson_name_by_salesperson_id(integer)
RETURNS text
AS
$$
BEGIN
    RETURN
    (
        SELECT salesperson_name
        FROM core.salespersons
        WHERE salesperson_id=$1
    );
END
$$
LANGUAGE plpgsql;
