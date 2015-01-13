DROP FUNCTION IF EXISTS transactions.is_normally_debit(_account_id bigint);

CREATE FUNCTION transactions.is_normally_debit(_account_id bigint)
RETURNS boolean
AS
$$
BEGIN
    RETURN
        core.account_masters.normally_debit
    FROM  core.accounts
    INNER JOIN core.account_masters
    ON core.accounts.account_master_id = core.account_masters.account_master_id
    WHERE account_id = $1;
END
$$
LANGUAGE plpgsql;