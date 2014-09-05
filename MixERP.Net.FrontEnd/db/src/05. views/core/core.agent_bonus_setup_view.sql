CREATE VIEW core.agent_bonus_setup_view
AS
SELECT
	agent_bonus_setup_id,
	agent_name,
	bonus_slab_name
FROM
	core.agent_bonus_setups,
	core.agents,
	core.bonus_slabs
WHERE
	core.agent_bonus_setups.agent_id = core.agents.agent_id
AND
	core.agent_bonus_setups.bonus_slab_id = core.bonus_slabs.bonus_slab_id;
