DROP VIEW IF EXISTS office.store_scrud_view;
CREATE VIEW office.store_scrud_view
AS
SELECT 
  office.stores.store_id, 
  office.offices.office_code || '('|| office.offices.office_name||')' AS office, 
  office.stores.store_code, 
  office.stores.store_name, 
  office.stores.address, 
  office.store_types.store_type_code || '('|| office.store_types.store_type_name||')' AS store_type, 
  office.stores.allow_sales, 
  core.sales_taxes.sales_tax_code || '('|| core.sales_taxes.sales_tax_name||')' AS sales_tax,
  core.accounts.account_number || '('|| core.accounts.account_name||')' AS account,
  office.cash_repositories.cash_repository_code || '('|| office.cash_repositories.cash_repository_name||')' AS cash_repository 
FROM 
  office.stores
INNER JOIN office.offices
ON office.stores.office_id = office.offices.office_id
INNER JOIN office.store_types
ON office.stores.store_type_id = office.store_types.store_type_id
INNER JOIN core.sales_taxes
ON office.stores.sales_tax_id = core.sales_taxes.sales_tax_id
INNER JOIN core.accounts
ON office.stores.default_cash_account_id = core.accounts.account_id
INNER JOIN office.cash_repositories
ON office.stores.default_cash_repository_id = office.cash_repositories.cash_repository_id;

