DROP VIEW IF EXISTS core.salesperson_selector_view;

CREATE VIEW core.salesperson_selector_view
AS
SELECT
    salesperson_id,
    salesperson_code,
    salesperson_name,
    address,
    contact_number,
    commission_rate,
    account_name
FROM
    core.salespersons,
    core.accounts
WHERE
    core.salespersons.account_id = core.accounts.account_id;

