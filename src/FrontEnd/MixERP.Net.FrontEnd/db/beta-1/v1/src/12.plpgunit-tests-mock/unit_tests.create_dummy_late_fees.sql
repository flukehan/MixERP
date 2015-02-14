DROP FUNCTION IF EXISTS unit_tests.create_dummy_late_fees();

CREATE FUNCTION unit_tests.create_dummy_late_fees()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.late_fee WHERE late_fee_code='dummy-lf01') THEN        
        INSERT INTO core.late_fee(late_fee_code, late_fee_name, is_flat_amount, rate)
        SELECT 'dummy-lf01', 'Test Mock Late Fee', false, 22;
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;