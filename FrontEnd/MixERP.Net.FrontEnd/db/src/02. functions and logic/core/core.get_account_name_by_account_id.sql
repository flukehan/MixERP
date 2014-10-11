DROP FUNCTION IF EXISTS core.get_account_name_by_account_id(bigint);

CREATE FUNCTION core.get_account_name_by_account_id(bigint)
RETURNS text
AS
$$
BEGIN
    RETURN
    (
        SELECT account_name
        FROM core.accounts
        WHERE account_id=$1
    );
END
$$
LANGUAGE plpgsql;

