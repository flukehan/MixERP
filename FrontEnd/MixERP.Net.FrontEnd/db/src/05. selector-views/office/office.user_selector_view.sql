DROP VIEW IF EXISTS office.user_selector_view;

CREATE VIEW office.user_selector_view
AS
SELECT
    office.users.user_id,
    office.users.user_name,
    office.users.full_name,
    office.roles.role_name,
    office.offices.office_name
FROM
    office.users
INNER JOIN office.roles
ON office.users.role_id = office.roles.role_id
INNER JOIN office.offices
ON office.users.office_id = office.offices.office_id;
