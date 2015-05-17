DROP FUNCTION IF EXISTS core.get_income_tax_rate(_office_id integer);
DROP FUNCTION IF EXISTS office.get_income_tax_rate(_office_id integer);

CREATE FUNCTION office.get_income_tax_rate(_office_id integer)
RETURNS real
AS
$$
BEGIN
    RETURN income_tax_rate
    FROM office.offices
    WHERE office_id = $1;
END
$$
LANGUAGE plpgsql;

--SELECT * FROM office.get_income_tax_rate(2);