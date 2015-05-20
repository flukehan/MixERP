DROP FUNCTION IF EXISTS unit_tests.create_dummy_stores();

CREATE FUNCTION unit_tests.create_dummy_stores()
RETURNS void
AS
$$
    DECLARE _cash_account_id bigint;
BEGIN
    IF NOT EXISTS(SELECT 1 FROM office.stores WHERE store_code='dummy-st01') THEN
        INSERT INTO office.stores(store_code, store_name, office_id, store_type_id, allow_sales, sales_tax_id, default_cash_account_id, default_cash_repository_id)
        SELECT 
            'dummy-st01', 
            'Test Mock Store',
            office.get_office_id_by_office_code('dummy-off01'),
            office.get_store_type_id_by_store_type_code('dummy-st01'),
            true,
            core.get_sales_tax_id_by_sales_tax_code('dummy-stx01'),
            core.get_account_id_by_account_number('dummy-acc06'),
            office.get_cash_repository_id_by_cash_repository_code('dummy-cr01');
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

