CREATE VIEW office.work_center_view
AS
SELECT
    office.work_centers.work_center_id,
    office.offices.office_code || ' (' || office.offices.office_name || ')' AS office,
    office.work_centers.work_center_code,
    office.work_centers.work_center_name
FROM office.work_centers
INNER JOIN office.offices
ON office.work_centers.office_id = office.offices.office_id;
