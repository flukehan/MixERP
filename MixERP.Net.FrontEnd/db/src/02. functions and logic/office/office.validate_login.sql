CREATE FUNCTION office.validate_login
(
	user_name text,
	password text
)
RETURNS boolean
AS
$$
BEGIN
	IF EXISTS
	(
		SELECT 1 FROM office.users 
		WHERE office.users.user_name=$1 
		AND office.users.password=$2 
		--The system user should not be allowed to login.
		AND office.users.role_id != 
		(
			SELECT office.roles.role_id 
			FROM office.roles 
			WHERE office.roles.role_code='SYST'
		)
	) THEN
		RETURN true;
	END IF;
	RETURN false;
END
$$
LANGUAGE plpgsql;
