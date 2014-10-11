CREATE VIEW core.tax_scrud_view
AS
SELECT
    tax_id,
    tax_code,
    tax_name,
    rate,
    tax_type_code,
    tax_type_name,
    account_code,
    account_name
FROM
    core.taxes,
    core.accounts,
    core.tax_types
WHERE
    core.taxes.account_id = core.accounts.account_id
AND
    core.taxes.tax_type_id = core.tax_types.tax_type_id;
