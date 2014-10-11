CREATE FUNCTION core.get_shipper_name_by_shipper_id(integer)
RETURNS text
AS
$$
BEGIN
    RETURN
    (
        SELECT company_name
        FROM core.shippers
        WHERE shipper_id=$1
    );
END
$$
LANGUAGE plpgsql;
