DROP FUNCTION IF EXISTS core.get_account_view_by_account_master_id
(
    _account_master_id      integer,
    _row_number             integer
);

CREATE FUNCTION core.get_account_view_by_account_master_id
(
    _account_master_id      integer,
    _row_number             integer
)
RETURNS table
(
    id                      bigint,
    account_id              bigint,
    account_name            text    
)
AS
$$
BEGIN
    RETURN QUERY
    SELECT ROW_NUMBER() OVER (ORDER BY accounts.account_id) +_row_number, * FROM 
    (
        SELECT core.accounts.account_id, core.get_account_name_by_account_id(core.accounts.account_id)
        FROM core.accounts
        WHERE core.accounts.account_master_id = _account_master_id
    ) AS accounts;    
END;
$$
LANGUAGE plpgsql;

















