CREATE VIEW core.bonus_slab_scrud_view
AS
SELECT
    core.bonus_slabs.bonus_slab_id,
    core.bonus_slabs.bonus_slab_code,
    core.bonus_slabs.bonus_slab_name,
    core.bonus_slabs.effective_from,
    core.bonus_slabs.ends_on,
    core.frequencies.frequency_code || '('||core.frequencies.frequency_name||')' AS checking_frequency
FROM
core.bonus_slabs
INNER JOIN core.frequencies
ON core.bonus_slabs.checking_frequency_id = core.frequencies.frequency_id;