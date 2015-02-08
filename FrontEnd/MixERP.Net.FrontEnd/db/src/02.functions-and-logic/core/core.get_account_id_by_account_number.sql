CREATE FUNCTION core.get_account_id_by_account_number(text)
RETURNS bigint
STABLE
AS
$$
BEGIN
    RETURN
		account_id
    FROM core.accounts
    WHERE account_number=$1;
END
$$
LANGUAGE plpgsql;
