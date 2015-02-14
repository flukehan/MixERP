DROP FUNCTION IF EXISTS transactions.get_default_currency_code(cash_repository_id integer);

CREATE FUNCTION transactions.get_default_currency_code(cash_repository_id integer)
RETURNS national character varying(12)
AS
$$
BEGIN
    RETURN
    (
        SELECT office.offices.currency_code 
        FROM office.cash_repositories
        INNER JOIN office.offices
        ON office.offices.office_id = office.cash_repositories.office_id
        WHERE office.cash_repositories.cash_repository_id=$1
        
    );
END
$$
LANGUAGE plpgsql;
