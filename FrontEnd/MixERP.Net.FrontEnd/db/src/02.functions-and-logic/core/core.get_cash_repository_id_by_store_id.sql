DROP FUNCTION IF EXISTS core.get_cash_repository_id_by_store_id(_store_id integer);

CREATE FUNCTION core.get_cash_repository_id_by_store_id(_store_id integer)
RETURNS bigint
STABLE
AS
$$
BEGIN
    RETURN
        default_cash_repository_id
    FROM
        office.stores
    WHERE
        office.stores.store_id=$1;
END
$$
LANGUAGE plpgsql;


