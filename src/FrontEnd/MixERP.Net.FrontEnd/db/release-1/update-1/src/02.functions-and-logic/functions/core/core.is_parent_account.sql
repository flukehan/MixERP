DROP FUNCTION IF EXISTS core.is_parent_account(parent bigint, child bigint) CASCADE;

CREATE FUNCTION core.is_parent_account(parent bigint, child bigint)
RETURNS boolean
AS
$$      
BEGIN
    IF $1!=$2 THEN
        IF EXISTS
        (
            WITH RECURSIVE account_cte(account_id, path) AS 
            (
                SELECT
                    tn.account_id,  
                    tn.account_id::TEXT AS path
                FROM core.accounts AS tn 
                WHERE tn.parent_account_id IS NULL
                UNION ALL
                SELECT
                    c.account_id, 
                    (p.path || '->' || c.account_id::TEXT)
                FROM account_cte AS p, core.accounts AS c 
                WHERE parent_account_id = p.account_id
            )
            SELECT * FROM
            (
                SELECT regexp_split_to_table(path, '->')
                FROM account_cte AS n WHERE n.account_id = $2
            ) AS items
            WHERE regexp_split_to_table=$1::text
        ) THEN
            RETURN TRUE;
        END IF;
    END IF;
    RETURN false;
END
$$
LANGUAGE plpgsql;


ALTER TABLE core.accounts
ADD CONSTRAINT accounts_parent_account_id_chk
CHECK(parent_account_id IS NULL OR NOT core.is_parent_account(account_id, parent_account_id));