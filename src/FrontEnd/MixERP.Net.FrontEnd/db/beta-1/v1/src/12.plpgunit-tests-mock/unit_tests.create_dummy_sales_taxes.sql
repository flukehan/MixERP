DROP FUNCTION IF EXISTS unit_tests.create_dummy_sales_taxes();

CREATE FUNCTION unit_tests.create_dummy_sales_taxes()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.tax_master WHERE tax_master_code='dummy-tm01') THEN
        INSERT INTO core.tax_master(tax_master_code, tax_master_name)
        SELECT 'dummy-tm01', 'Dummy Tax Master';
    END IF;
    
    IF NOT EXISTS(SELECT 1 FROM core.sales_taxes WHERE sales_tax_code='dummy-stx01') THEN
        INSERT INTO core.sales_taxes(tax_master_id, office_id, sales_tax_code, sales_tax_name, is_exemption, tax_base_amount_type_code, rate)
        SELECT 
            core.get_tax_master_id_by_tax_master_code('dummy-tm01'), 
            office.get_office_id_by_office_code('dummy-off01'),
            'dummy-stx01',
            'Dummy Sales Tax',
            false,
            'P',
            12.4;
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;

--SELECT * FROM unit_tests.create_dummy_sales_tax()