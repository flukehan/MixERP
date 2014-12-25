DROP FUNCTION IF EXISTS transactions.is_normally_debit(_account_id bigint);

CREATE FUNCTION transactions.is_normally_debit(_account_id bigint)
RETURNS boolean
AS
$$
BEGIN
    RETURN
        normally_debit
    FROM
        core.accounts
    WHERE
        account_id = $1;
END
$$
LANGUAGE plpgsql;