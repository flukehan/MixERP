CREATE VIEW core.cash_flow_heading_scrud_view
AS
SELECT 
  core.cash_flow_headings.cash_flow_heading_id, 
  core.cash_flow_headings.cash_flow_heading_code, 
  core.cash_flow_headings.cash_flow_heading_name, 
  core.cash_flow_headings.cash_flow_heading_type, 
  core.cash_flow_headings.is_debit, 
  core.cash_flow_headings.is_sales, 
  core.cash_flow_headings.is_purchase
FROM 
  core.cash_flow_headings;
