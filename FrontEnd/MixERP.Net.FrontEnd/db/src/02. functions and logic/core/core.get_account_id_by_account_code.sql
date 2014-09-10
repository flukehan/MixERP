CREATE FUNCTION core.get_account_id_by_account_code(text)
RETURNS bigint
AS
$$
BEGIN
	RETURN
	(
		SELECT account_id
		FROM core.accounts
		WHERE account_code=$1
	);
END
$$
LANGUAGE plpgsql;
