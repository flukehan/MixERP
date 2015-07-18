DROP VIEW IF EXISTS core.payment_term_scrud_view;

CREATE VIEW core.payment_term_scrud_view
AS
SELECT
    core.payment_terms.payment_term_id,
    core.payment_terms.payment_term_code,
    core.payment_terms.payment_term_name,
    core.payment_terms.due_on_date,
    core.payment_terms.due_days,
    due_frequency.frequency_code || ' (' || due_frequency.frequency_name || ')' AS due_frequency,
    core.payment_terms.grace_period,
    core.late_fee.late_fee_code || ' (' || core.late_fee.late_fee_name || ')' AS late_fee,
    late_fee_posting_frequency.frequency_code || ' (' || late_fee_posting_frequency.frequency_name || ')' AS late_fee_posting_frequency
FROM core.payment_terms
LEFT JOIN core.frequencies AS due_frequency
ON core.payment_terms.due_frequency_id=due_frequency.frequency_id
LEFT JOIN core.frequencies AS late_fee_posting_frequency 
ON core.payment_terms.late_fee_posting_frequency_id=late_fee_posting_frequency.frequency_id
LEFT JOIN core.late_fee
ON core.payment_terms.late_fee_id=core.late_fee.late_fee_id;
