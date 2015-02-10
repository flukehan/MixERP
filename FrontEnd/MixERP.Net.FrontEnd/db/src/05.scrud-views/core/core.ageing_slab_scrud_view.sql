CREATE VIEW core.ageing_slab_scrud_view
AS
SELECT 
  ageing_slabs.ageing_slab_id, 
  ageing_slabs.ageing_slab_name, 
  ageing_slabs.from_days, 
  ageing_slabs.to_days
FROM 
  core.ageing_slabs;
