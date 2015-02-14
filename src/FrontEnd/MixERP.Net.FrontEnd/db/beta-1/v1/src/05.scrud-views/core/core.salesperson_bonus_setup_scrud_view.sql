CREATE VIEW core.salesperson_bonus_setup_scrud_view
AS
SELECT
    salesperson_bonus_setup_id,
    salesperson_name,
    bonus_slab_name
FROM
    core.salesperson_bonus_setups,
    core.salespersons,
    core.bonus_slabs
WHERE
    core.salesperson_bonus_setups.salesperson_id = core.salespersons.salesperson_id
AND
    core.salesperson_bonus_setups.bonus_slab_id = core.bonus_slabs.bonus_slab_id;
