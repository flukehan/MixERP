CREATE VIEW office.cost_center_scrud_view
AS
SELECT
    office.cost_centers.cost_center_id,
    office.cost_centers.cost_center_code,
    office.cost_centers.cost_center_name
FROM
    office.cost_centers;
