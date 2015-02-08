CREATE FUNCTION core.get_account_master_id_by_account_id(bigint)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN core.accounts.account_master_id
    FROM core.accounts
    WHERE core.accounts.account_id= $1;
END
$$
LANGUAGE plpgsql;


