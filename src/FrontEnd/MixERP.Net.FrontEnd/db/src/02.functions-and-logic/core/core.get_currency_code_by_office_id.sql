DROP FUNCTION IF EXISTS core.get_currency_code_by_office_id(integer);

CREATE FUNCTION core.get_currency_code_by_office_id(office_id integer)
RETURNS text
STABLE
AS
$$
BEGIN
    RETURN office.offices.currency_code
    FROM office.offices
    WHERE office.offices.office_id=$1;
END
$$
LANGUAGE plpgsql;

