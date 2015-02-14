DROP FUNCTION IF EXISTS core.get_shipper_id_by_shipper_code(text);

CREATE FUNCTION core.get_shipper_id_by_shipper_code(text)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN shipper_id
    FROM core.shippers
    WHERE shipper_code=$1;
END
$$
LANGUAGE plpgsql;