DROP FUNCTION IF EXISTS core.is_cash_account_id(_account_id bigint);

CREATE FUNCTION core.is_cash_account_id(_account_id bigint)
RETURNS boolean
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT 1 FROM core.accounts WHERE account_master_id IN(10101)
        AND account_id=$1
    ) THEN
        RETURN true;
    END IF;
    RETURN false;
END
$$
LANGUAGE plpgsql;
