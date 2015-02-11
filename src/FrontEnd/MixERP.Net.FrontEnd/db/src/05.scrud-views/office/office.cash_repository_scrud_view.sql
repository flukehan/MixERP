DROP VIEW IF EXISTS office.cash_repository_scrud_view;
CREATE VIEW office.cash_repository_scrud_view
AS
SELECT
office.cash_repositories.cash_repository_id,
office.offices.office_code || ' (' || office.offices.office_name || ') ' AS office,
office.cash_repositories.cash_repository_code,
office.cash_repositories.cash_repository_name,
parent_cash_repository.cash_repository_code || ' (' || parent_cash_repository.cash_repository_name || ') ' AS parent_cash_repository,
office.cash_repositories.description

FROM office.cash_repositories
INNER JOIN office.offices
ON office.cash_repositories.office_id = office.offices.office_id
LEFT JOIN office.cash_repositories AS parent_cash_repository
ON office.cash_repositories.parent_cash_repository_id = parent_cash_repository.parent_cash_repository_id;




