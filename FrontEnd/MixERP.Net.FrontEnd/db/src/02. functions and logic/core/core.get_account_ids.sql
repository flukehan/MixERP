DROP FUNCTION IF EXISTS core.get_account_ids(root_account_id bigint);

CREATE FUNCTION core.get_account_ids(root_account_id bigint)
RETURNS SETOF bigint
STABLE
AS
$$
BEGIN
    RETURN QUERY 
    (
        WITH RECURSIVE account_cte(account_id, path) AS (
         SELECT
            tn.account_id,  tn.account_id::TEXT AS path
            FROM core.accounts AS tn WHERE tn.account_id =$1
        UNION ALL
         SELECT
            c.account_id, (p.path || '->' || c.account_id::TEXT)
            FROM account_cte AS p, core.accounts AS c WHERE parent_account_id = p.account_id
        )

        SELECT account_id FROM account_cte
    );
END
$$LANGUAGE plpgsql;