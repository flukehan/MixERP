CREATE FUNCTION core.get_cash_account_id()
RETURNS integer
AS
$$
BEGIN
	RETURN
	(
		SELECT account_id
		FROM core.accounts
		WHERE is_cash=true
		LIMIT 1
	);
END
$$
LANGUAGE plpgsql;
