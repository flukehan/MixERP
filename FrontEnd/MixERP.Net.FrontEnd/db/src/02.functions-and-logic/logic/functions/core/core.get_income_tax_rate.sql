DROP FUNCTION IF EXISTS core.get_income_tax_rate(_office_id integer);

CREATE FUNCTION core.get_income_tax_rate(_office_id integer)
RETURNS real
AS
$$
BEGIN
    RETURN 19.00;--TODO
END
$$
LANGUAGE plpgsql;


