CREATE VIEW core.agent_view
AS
SELECT
	agent_id,
	agent_code,
	agent_name,
	address,
	contact_number,
	commission_rate,
	account_name
FROM
	core.agents,
	core.accounts
WHERE
	core.agents.account_id = core.accounts.account_id;
