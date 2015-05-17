DROP FUNCTION IF EXISTS core.get_account_id_by_shipper_id(integer);

CREATE FUNCTION core.get_account_id_by_shipper_id(_shipper_id integer)
RETURNS bigint
STABLE
AS
$$
BEGIN
    RETURN
        core.shippers.account_id
    FROM
        core.shippers
    WHERE
        core.shippers.shipper_id=$1;
END
$$
LANGUAGE plpgsql;
