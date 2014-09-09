CREATE VIEW core.bonus_slab_view
AS
SELECT
	bonus_slab_id,
	bonus_slab_code,
	bonus_slab_name,
	checking_frequency_id,
	frequency_name
FROM
core.bonus_slabs, core.frequencies
WHERE
core.bonus_slabs.checking_frequency_id = core.frequencies.frequency_id;
