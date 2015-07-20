DROP VIEW core.recurring_invoice_setup_scrud_view;
CREATE OR REPLACE VIEW core.recurring_invoice_setup_scrud_view 
AS 
SELECT recurring_invoice_setup.recurring_invoice_setup_id,
 ((recurring_invoices.recurring_invoice_code::text || ' ('::text) || recurring_invoices.recurring_invoice_name::text) || ')'::text AS recurring_invoice,
 ((parties.party_code::text || ' ('::text) || 
  CASE WHEN parties.company_name IS NULL THEN parties.party_name
           ELSE parties.company_name
           END) || ')'::text AS party,
recurring_invoice_setup.starts_from,
recurring_invoice_setup.ends_on,
recurring_invoice_setup.recurring_amount,
((payment_terms.payment_term_code::text || ' ('::text) || payment_terms.payment_term_name::text) || ')'::text AS payment_term
FROM core.recurring_invoice_setup
JOIN core.recurring_invoices ON recurring_invoice_setup.recurring_invoice_id = recurring_invoices.recurring_invoice_id
JOIN core.parties ON recurring_invoice_setup.party_id = parties.party_id
JOIN core.payment_terms ON recurring_invoice_setup.payment_term_id = payment_terms.payment_term_id;

