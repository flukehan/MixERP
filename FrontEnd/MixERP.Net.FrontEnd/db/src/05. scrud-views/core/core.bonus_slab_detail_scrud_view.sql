CREATE VIEW core.bonus_slab_detail_scrud_view
AS
SELECT
    bonus_slab_detail_id,
    core.bonus_slab_details.bonus_slab_id,
    core.bonus_slabs.bonus_slab_name AS slab_name,
    amount_from,
    amount_to,
    bonus_rate
FROM
    core.bonus_slab_details,
    core.bonus_slabs
WHERE
    core.bonus_slab_details.bonus_slab_id = core.bonus_slabs.bonus_slab_id;
