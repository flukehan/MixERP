DROP FUNCTION IF EXISTS core.get_account_id_by_account_name(text);

CREATE FUNCTION core.get_account_id_by_account_name(text)
RETURNS bigint
STABLE
AS
$$
BEGIN
    RETURN
		account_id
    FROM core.accounts
    WHERE account_name=$1;
END
$$
LANGUAGE plpgsql;
