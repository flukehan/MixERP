CREATE FUNCTION office.is_sys_user(integer)
RETURNS boolean
AS
$$
BEGIN
	IF EXISTS
	(
		SELECT * FROM office.users
		WHERE user_id=$1
		AND role_id IN
		(
			SELECT office.roles.role_id FROM office.roles WHERE office.roles.role_code='SYST'
		)
	) THEN
		RETURN true;
	END IF;

	RETURN false;
END
$$
LANGUAGE plpgsql;


ALTER TABLE transactions.transaction_master
ADD CONSTRAINT transaction_master_sys_user_id_chk CHECK(sys_user_id IS NULL OR office.is_sys_user(sys_user_id)=true);
