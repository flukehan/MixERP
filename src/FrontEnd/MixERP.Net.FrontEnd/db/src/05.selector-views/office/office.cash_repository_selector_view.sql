DROP VIEW IF EXISTS office.cash_repository_selector_view;

CREATE VIEW office.cash_repository_selector_view
AS
SELECT
    office.cash_repositories.cash_repository_id,
    office.cash_repositories.cash_repository_code,
    office.cash_repositories.cash_repository_name,
    parent_cash_repositories.cash_repository_code parent_cr_code,
    parent_cash_repositories.cash_repository_name parent_cr_name,
    office.cash_repositories.description
FROM
    office.cash_repositories
LEFT OUTER JOIN
    office.cash_repositories AS parent_cash_repositories
ON
    office.cash_repositories.parent_cash_repository_id=parent_cash_repositories.cash_repository_id;

