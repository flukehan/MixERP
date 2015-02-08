DROP VIEW IF EXISTS core.recurring_invoice_setup_scrud_view;
CREATE VIEW core.recurring_invoice_setup_scrud_view
AS
SELECT 
  core.recurring_invoice_setup.recurring_invoice_setup_id, 
  core.recurring_invoices.recurring_invoice_code || ' (' || core.recurring_invoices.recurring_invoice_name || ')' AS recurring_invoice,
  core.parties.party_code || ' (' || core.parties.party_name || ')' AS party,
  core.recurring_invoice_setup.starts_from, 
  core.recurring_invoice_setup.ends_on, 
  core.recurring_invoice_setup.recurring_amount, 
  core.payment_terms.payment_term_code || ' (' || core.payment_terms.payment_term_name || ')' AS payment_term
FROM 
  core.recurring_invoice_setup
INNER JOIN core.recurring_invoices
ON core.recurring_invoice_setup.recurring_invoice_id = core.recurring_invoices.recurring_invoice_id
INNER JOIN core.parties ON 
core.recurring_invoice_setup.party_id = core.parties.party_id
INNER JOIN core.payment_terms ON 
core.recurring_invoice_setup.payment_term_id = core.payment_terms.payment_term_id;