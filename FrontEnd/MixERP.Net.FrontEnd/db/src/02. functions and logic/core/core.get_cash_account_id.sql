CREATE FUNCTION core.get_cash_account_id()
RETURNS bigint
AS
$$
BEGIN
    RETURN
    (
        SELECT account_id
        FROM core.accounts
        WHERE account_master_id = 10101
        LIMIT 1
    );
END
$$
LANGUAGE plpgsql;
