CREATE VIEW core.recurring_invoice_scrud_view
AS
SELECT 
  core.recurring_invoices.recurring_invoice_id, 
  core.recurring_invoices.recurring_invoice_code, 
  core.recurring_invoices.recurring_invoice_name,
  core.items.item_code || '('|| core.items.item_name||')' AS item,
  core.compound_items.compound_item_code || ' (' || core.compound_items.compound_item_name || ')' AS compound_item,
  core.frequencies.frequency_code || '('|| core.frequencies.frequency_name||')' AS recurring_frequency,
  core.recurring_invoices.recurring_amount, 
  core.recurring_invoices.auto_trigger_on_sales
FROM 
  core.recurring_invoices
LEFT JOIN core.items 
ON core.recurring_invoices.item_id = core.items.item_id
INNER JOIN core.frequencies
ON core.recurring_invoices.recurring_frequency_id = core.frequencies.frequency_id
LEFT JOIN core.compound_items
ON core.recurring_invoices.compound_item_id=core.compound_items.compound_item_id;