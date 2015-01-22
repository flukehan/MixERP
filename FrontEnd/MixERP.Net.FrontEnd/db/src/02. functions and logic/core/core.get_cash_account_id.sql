--TODO:DROP THIS FUNCTION
CREATE FUNCTION core.get_cash_account_id()
RETURNS bigint
STABLE
AS
$$
BEGIN
    RETURN account_id
    FROM core.accounts
    WHERE account_master_id = 10101
    ORDER BY account_id ASC
    LIMIT 1;
END
$$
LANGUAGE plpgsql;

COMMENT ON FUNCTION core.get_cash_account_id() IS 'This function is now obsolete, core.get_cash_account_id_by_store_id(_store_id integer) should be used instead.';