CREATE VIEW core.cash_flow_setup_scrud_view
AS
SELECT 
 core.cash_flow_setup.cash_flow_setup_id, 
 core.cash_flow_headings.cash_flow_heading_code || '('|| core.cash_flow_headings.cash_flow_heading_name||')' AS cash_flow_heading, 
 core.account_masters.account_master_code || '('|| core.account_masters.account_master_name||')' AS account_master
FROM 
core.cash_flow_setup
INNER JOIN core.cash_flow_headings
ON  core.cash_flow_setup.cash_flow_heading_id =core.cash_flow_headings.cash_flow_heading_id
INNER JOIN core.account_masters
ON core.cash_flow_setup.account_master_id = core.account_masters.account_master_id;

