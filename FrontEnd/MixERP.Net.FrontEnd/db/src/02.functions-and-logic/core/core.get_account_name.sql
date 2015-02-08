--Todo:Rename to core.get_account_name_by_account_id
CREATE FUNCTION core.get_account_name(bigint)
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
