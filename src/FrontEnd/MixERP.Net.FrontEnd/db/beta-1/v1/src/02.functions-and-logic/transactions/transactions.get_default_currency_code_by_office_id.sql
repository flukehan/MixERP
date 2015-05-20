DROP FUNCTION IF EXISTS transactions.get_default_currency_code_by_office_id(office_id integer);

CREATE FUNCTION transactions.get_default_currency_code_by_office_id(office_id integer)
RETURNS national character varying(12)
AS
$$
BEGIN
    RETURN
    (
        SELECT office.offices.currency_code 
        FROM office.offices
        WHERE office.offices.office_id = $1
        
    );
END
$$
LANGUAGE plpgsql;
