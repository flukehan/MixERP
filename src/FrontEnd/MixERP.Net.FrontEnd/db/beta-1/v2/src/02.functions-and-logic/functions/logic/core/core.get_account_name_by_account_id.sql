DROP FUNCTION IF EXISTS core.get_account_name_by_account_id(bigint);

CREATE FUNCTION core.get_account_name_by_account_id(_account_id bigint)
RETURNS text
STABLE
AS
$$
BEGIN
    RETURN account_name
    FROM core.accounts
    WHERE account_id=$1;
END
$$
LANGUAGE plpgsql;

