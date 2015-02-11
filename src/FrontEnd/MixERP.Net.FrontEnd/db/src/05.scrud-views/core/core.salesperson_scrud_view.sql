CREATE VIEW core.salesperson_scrud_view
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
