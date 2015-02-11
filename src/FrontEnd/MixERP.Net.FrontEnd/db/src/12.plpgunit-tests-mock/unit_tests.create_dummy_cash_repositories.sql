DROP FUNCTION IF EXISTS unit_tests.create_dummy_cash_repositories();

CREATE FUNCTION unit_tests.create_dummy_cash_repositories()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM office.cash_repositories WHERE cash_repository_code='dummy-cr01') THEN        
        INSERT INTO office.cash_repositories(cash_repository_code, cash_repository_name, office_id)
        SELECT 'dummy-cr01', 'Test Mock Cash Repository', office.get_office_id_by_office_code('dummy-off01');
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

