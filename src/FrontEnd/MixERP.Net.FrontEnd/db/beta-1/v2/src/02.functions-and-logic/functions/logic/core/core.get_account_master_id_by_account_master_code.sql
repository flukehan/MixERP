DROP FUNCTION IF EXISTS core.get_account_master_id_by_account_master_code(text);

CREATE FUNCTION core.get_account_master_id_by_account_master_code(_account_master_code text)
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN core.account_masters.account_master_id
    FROM core.account_masters
    WHERE core.account_masters.account_master_code = $1;
END
$$
LANGUAGE plpgsql;
