DROP FUNCTION IF EXISTS unit_tests.create_dummy_payment_terms();

CREATE FUNCTION unit_tests.create_dummy_payment_terms()
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM core.payment_terms WHERE payment_term_code='dummy-pt01') THEN        
        INSERT INTO core.payment_terms(payment_term_code, payment_term_name, due_on_date, due_days, grace_peiod, late_fee_id, late_fee_posting_frequency_id)
        SELECT 'dummy-pt01', 'Test Mock Payment Term', false, 10, 5, core.get_late_fee_id_by_late_fee_code('dummy-lf01'), core.get_frequency_id_by_frequency_code('EOM');
    END IF;

    RETURN;
END
$$
LANGUAGE plpgsql;