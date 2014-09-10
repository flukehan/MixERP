CREATE FUNCTION core.get_account_id_by_shipper_id(integer)
RETURNS bigint
AS
$$
BEGIN
	RETURN
	(
		SELECT
			core.shippers.account_id
		FROM
			core.shippers
		WHERE
			core.shippers.shipper_id=$1
	);
END
$$
LANGUAGE plpgsql;
