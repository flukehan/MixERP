DROP FUNCTION IF EXISTS office.get_office_id_by_cash_repository_id(integer);

CREATE FUNCTION office.get_office_id_by_cash_repository_id(integer)
RETURNS integer
AS
$$
BEGIN
        RETURN office_id
        FROM office.cash_repositories
        WHERE cash_repository_id=$1;
END
$$
LANGUAGE plpgsql;


ALTER TABLE office.stores
ADD CONSTRAINT store_default_cash_repository_chk
CHECK(office.get_office_id_by_cash_repository_id(default_cash_repository_id) = office_id);
