DROP FUNCTION IF EXISTS core.get_second_root_account_id(integer, integer);
DROP FUNCTION IF EXISTS core.get_root_account_id(integer, integer);
DROP FUNCTION IF EXISTS core.get_root_account_id(bigint, bigint);

CREATE FUNCTION core.get_root_account_id(_account_id bigint, _parent bigint default 0)
RETURNS integer
AS
$$
    DECLARE _parent_account_id bigint;
BEGIN
    SELECT 
        parent_account_id
        INTO _parent_account_id
    FROM core.accounts
    WHERE account_id=$1;

    

    IF(_parent_account_id IS NULL) THEN
        RETURN $1;
    ELSE
        RETURN core.get_root_account_id(_parent_account_id, $1);
    END IF; 
END
$$
LANGUAGE plpgsql;