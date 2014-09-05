CREATE FUNCTION core.get_account_name(integer)
RETURNS text
AS
$$
BEGIN
	RETURN
	(
		SELECT
			account_name
		FROM	
			core.accounts
		WHERE
			account_id=$1
	);
END
$$
LANGUAGE plpgsql;
