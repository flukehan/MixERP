COMMENT ON TABLE core.account_masters IS 
'This table contains categories in which General Ledger (G.L) Account belongs to & collectively they form the Chart of Accounts.
The category in this table cannot be edited by users. Thus, a user-interface for this table is not available.
This table facilitates creating useful reports such as Profit & Loss A/c. and Balance Sheet.';
COMMENT ON COLUMN core.account_masters.account_master_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.account_masters.account_master_code IS 'The unique alphanumeric code that generally abbreviates the value of account master name.';
COMMENT ON COLUMN core.account_masters.account_master_name IS 'The name of account master, which is also a unique field.';
COMMENT ON COLUMN core.account_masters.parent_account_master_id IS 'The name of account master, which is also a unique field.';
COMMENT ON COLUMN core.account_masters.normally_debit IS 'Select "Yes" if the account has nature of Debit balance or vice-versa.';



COMMENT ON TABLE core.accounts IS 'This table stores information on General Ledger (G.L) Account.';
COMMENT ON COLUMN core.accounts.account_id IS 'The primary key of this table, which is also a bigserial field.';
COMMENT ON COLUMN core.accounts.account_master_id IS 'The foreign key to table core.account_masters.';
COMMENT ON COLUMN core.accounts.account_number IS 'The unique numeric value assigned to the account name which is similar to account id.';
COMMENT ON COLUMN core.accounts.external_code IS '';
COMMENT ON COLUMN core.accounts.confidential IS 'Limits the access to the particular account among various uesers.';
COMMENT ON COLUMN core.accounts.currency_code IS 'Foreign key to the table core.currencies.';
COMMENT ON COLUMN core.accounts.account_name IS 'The name of account master, which is also a unique field.';
COMMENT ON COLUMN core.accounts.description IS 'Description about the account.';
COMMENT ON COLUMN core.accounts.sys_type IS '';
COMMENT ON COLUMN core.accounts.parent_account_id IS 'Foreign key to the table core.accounts.';
COMMENT ON COLUMN core.accounts.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.accounts.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.ageing_slabs IS '';
COMMENT ON COLUMN core.ageing_slabs.ageing_slab_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.ageing_slabs.ageing_slab_name IS '';
COMMENT ON COLUMN core.ageing_slabs.from_days IS '';
COMMENT ON COLUMN core.ageing_slabs.to_days IS '';


COMMENT ON TABLE core.attachment_lookup IS '';
COMMENT ON COLUMN core.attachment_lookup.attachment_lookup_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.attachment_lookup.book IS '';
COMMENT ON COLUMN core.attachment_lookup.resource IS '';
COMMENT ON COLUMN core.attachment_lookup.resource_key IS '';


COMMENT ON TABLE core.attachments IS '';
COMMENT ON COLUMN core.attachments.attachment_id IS 'The primary key of this table, which is also a bigserial field.';
COMMENT ON COLUMN core.attachments.user_id IS 'Foreign key to the table office.users.';
COMMENT ON COLUMN core.attachments.resource IS '';
COMMENT ON COLUMN core.attachments.resource_key IS '';
COMMENT ON COLUMN core.attachments.resource_id IS '';
COMMENT ON COLUMN core.attachments.original_file_name IS 'The name given to the attached file, which is also a unique field ';
COMMENT ON COLUMN core.attachments.file_extension IS 'The extension of the attached file.';
COMMENT ON COLUMN core.attachments.file_path IS 'The location of the file.';
COMMENT ON COLUMN core.attachments.comment IS 'Comment on the attachment.';
COMMENT ON COLUMN core.attachments.added_on IS 'Time & date when the attachment was added.';


COMMENT ON TABLE policy.auto_verification_policy IS '';
COMMENT ON COLUMN policy.auto_verification_policy.user_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN policy.auto_verification_policy.verify_sales_transactions IS '';
COMMENT ON COLUMN policy.auto_verification_policy.sales_verification_limit IS '';
COMMENT ON COLUMN policy.auto_verification_policy.verify_purchase_transactions IS '';
COMMENT ON COLUMN policy.auto_verification_policy.purchase_verification_limit IS '';
COMMENT ON COLUMN policy.auto_verification_policy.verify_gl_transactions IS '';
COMMENT ON COLUMN policy.auto_verification_policy.gl_verification_limit IS '';
COMMENT ON COLUMN policy.auto_verification_policy.effective_from IS '';
COMMENT ON COLUMN policy.auto_verification_policy.ends_on IS '';
COMMENT ON COLUMN policy.auto_verification_policy.is_active IS '';
COMMENT ON COLUMN policy.auto_verification_policy.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN policy.auto_verification_policy.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';

 
COMMENT ON TABLE core.bank_accounts IS 'This table stores information on various Bank A/cs and other associated information.';
COMMENT ON COLUMN core.bank_accounts.account_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.bank_accounts.maintained_by_user_id IS 'Foreign key to the table office.users.';
COMMENT ON COLUMN core.bank_accounts.bank_name IS 'The name of the bank.';
COMMENT ON COLUMN core.bank_accounts.bank_branch IS 'The name of the branch.';
COMMENT ON COLUMN core.bank_accounts.bank_contact_number IS 'The contact number of the bank.';
COMMENT ON COLUMN core.bank_accounts.bank_address IS 'The address of the bank.';
COMMENT ON COLUMN core.bank_accounts.bank_account_number IS 'The bank account number.';
COMMENT ON COLUMN core.bank_accounts.bank_account_type IS 'The type of bank account.';
COMMENT ON COLUMN core.bank_accounts.relationship_officer_name IS 'The name of  the relationship officer.';
COMMENT ON COLUMN core.bank_accounts.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.bank_accounts.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.bonus_slab_details IS 'This table stores information on various rate of bonus.';
COMMENT ON COLUMN core.bonus_slab_details.bonus_slab_detail_id IS 'The primary key of the table, which is also a serial field.';
COMMENT ON COLUMN core.bonus_slab_details.bonus_slab_id IS 'Foreign key to this table.';
COMMENT ON COLUMN core.bonus_slab_details.amount_from IS 'The minimum amount of sales to qualify for the bonus slab.';
COMMENT ON COLUMN core.bonus_slab_details.amount_to IS 'The maximum amount in the bonus slab.';
COMMENT ON COLUMN core.bonus_slab_details.bonus_rate IS 'The rate of bonus assigned to the bonus slab.';
COMMENT ON COLUMN core.bonus_slab_details.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.bonus_slab_details.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.bonus_slabs IS 'This table stores information on bonus slabs.';
COMMENT ON COLUMN core.bonus_slabs.bonus_slab_id IS 'The primary key of the table, which is also a serial field';
COMMENT ON COLUMN core.bonus_slabs.bonus_slab_code IS 'Code given to the column.';
COMMENT ON COLUMN core.bonus_slabs.bonus_slab_name IS 'Name of the colum, which is a also unique field.';
COMMENT ON COLUMN core.bonus_slabs.effective_from IS 'The effective date of the bonus slab.';
COMMENT ON COLUMN core.bonus_slabs.ends_on IS 'The ending date of the bonus slab.';
COMMENT ON COLUMN core.bonus_slabs.checking_frequency_id IS 'Time-interval for calculation of bonus';
COMMENT ON COLUMN core.bonus_slabs.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.bonus_slabs.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.brands IS 'This table stores information on various brands the entity is dealing.';
COMMENT ON COLUMN core.brands.brand_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.brands.brand_code IS 'The code of the brand, which is also a unique field.';
COMMENT ON COLUMN core.brands.brand_name IS 'The name of the brand.';
COMMENT ON COLUMN core.brands.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.brands.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.cash_flow_headings IS 'This table stores information on various categories in the Cash Flow Statement.';
COMMENT ON COLUMN core.cash_flow_headings.cash_flow_heading_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.cash_flow_headings.cash_flow_heading_code IS 'The code given to the Cash Flow heading.';
COMMENT ON COLUMN core.cash_flow_headings.cash_flow_heading_name IS 'The name of the Cash Flow heading, which is also a unique field,';
COMMENT ON COLUMN core.cash_flow_headings.cash_flow_heading_type IS 'A single character assigned to Cash Flow heading, which define its type.';


COMMENT ON TABLE office.cash_repositories IS 'This table stores information related to cash repositories.';
COMMENT ON COLUMN office.cash_repositories.cash_repository_id IS 'The primary key of this table, which is also a bigserial field.';
COMMENT ON COLUMN office.cash_repositories.office_id IS 'Foreign key to the table office.offices.';
COMMENT ON COLUMN office.cash_repositories.cash_repository_code IS 'The code given to cash repository.';
COMMENT ON COLUMN office.cash_repositories.cash_repository_name IS 'The  name given to cash repository,which is also a unique field.';
COMMENT ON COLUMN office.cash_repositories.parent_cash_repository_id IS 'Foreign key to the table office.cash_repositories.';
COMMENT ON COLUMN office.cash_repositories.description IS 'Description on cash repository.';
COMMENT ON COLUMN office.cash_repositories.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN office.cash_repositories.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE office.cashiers IS 'This table stores information related to cashier.';
COMMENT ON COLUMN office.cashiers.cashier_id IS 'The primary key of this table, which is also a bigserial field.';
COMMENT ON COLUMN office.cashiers.counter_id IS 'Foreign key to the table office.counters.';
COMMENT ON COLUMN office.cashiers.user_id IS 'Foreign key to the table office.users.';
COMMENT ON COLUMN office.cashiers.assigned_by_user_id IS 'Foreign key to the table office.users.';
COMMENT ON COLUMN office.cashiers.transaction_date IS 'The date on which the transaction occured.';
COMMENT ON COLUMN office.cashiers.closed IS '';


COMMENT ON TABLE core.compound_item_details IS 'This table stores information on compound item and combination of items which composites compound item.';
COMMENT ON COLUMN core.compound_item_details.compound_item_detail_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.compound_item_details.compound_item_id IS 'Foreign key to the table core.comound_items';
COMMENT ON COLUMN core.compound_item_details.item_id IS 'Foreign key to the table core.items';
COMMENT ON COLUMN core.compound_item_details.unit_id IS 'Foreign key to the table core.units.';
COMMENT ON COLUMN core.compound_item_details.quantity IS 'The quantity composition of compound items.';
COMMENT ON COLUMN core.compound_item_details.price IS 'The price detail of compound items.';
COMMENT ON COLUMN core.compound_item_details.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.compound_item_details.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.compound_items IS 'This table stores information on combination on compund items and other associated information to it.';
COMMENT ON COLUMN core.compound_items.compound_item_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.compound_items.compound_item_code IS 'Code given to the compound item.';
COMMENT ON COLUMN core.compound_items.compound_item_name IS 'The name given to compound item, which is a unique field';
COMMENT ON COLUMN core.compound_items.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.compound_items.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.compound_units IS 'This table stores information on the units, value of the items that forms the compound items.';
COMMENT ON COLUMN core.compound_units.compound_unit_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.compound_units.base_unit_id IS 'Foreign key to the table core.units.';
COMMENT ON COLUMN core.compound_units.value IS '';
COMMENT ON COLUMN core.compound_units.compare_unit_id IS '';
COMMENT ON COLUMN core.compound_units.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.compound_units.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';

/* THIS TABLE*/
COMMENT ON TABLE core.config IS '';
COMMENT ON COLUMN core.config.config_id IS 'The primary key of this table.';
COMMENT ON COLUMN core.config.config_name IS 'The name given to the , which is also a unique field.';


COMMENT ON TABLE office.configuration IS '';
COMMENT ON COLUMN office.configuration.configuration_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN office.configuration.config_id IS 'Foreign key to the table core.config.';
COMMENT ON COLUMN office.configuration.office_id IS '';
COMMENT ON COLUMN office.configuration.value IS '';
COMMENT ON COLUMN office.configuration.configuration_details IS 'Details on configuration.';
COMMENT ON COLUMN office.configuration.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN office.configuration.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE office.cost_centers IS '';
COMMENT ON COLUMN office.cost_centers.cost_center_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN office.cost_centers.cost_center_code IS '';
COMMENT ON COLUMN office.cost_centers.cost_center_name IS '';
COMMENT ON COLUMN office.cost_centers.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN office.cost_centers.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE office.counters IS '';
COMMENT ON COLUMN office.counters.counter_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN office.counters.store_id IS '';
COMMENT ON COLUMN office.counters.cash_repository_id IS '';
COMMENT ON COLUMN office.counters.counter_code IS '';
COMMENT ON COLUMN office.counters.counter_name IS '';
COMMENT ON COLUMN office.counters.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN office.counters.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.counties IS 'This table stores information on counties, their code, the state they belong etc.';
COMMENT ON COLUMN core.counties.county_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.counties.county_code IS 'The code given to the county,which is a unique field.';
COMMENT ON COLUMN core.counties.county_name IS 'The name of the county, which is a unique field.';
COMMENT ON COLUMN core.counties.state_id IS 'State code of the county.';
COMMENT ON COLUMN core.counties.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.counties.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.countries IS 'This table stores information on list of countries and their their code.';
COMMENT ON COLUMN core.countries.country_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.countries.country_code IS 'The code of the country.';
COMMENT ON COLUMN core.countries.country_name IS 'The name of the country.';
COMMENT ON COLUMN core.countries.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.countries.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.county_sales_taxes IS 'This table stores information on sales tax of the county.';
COMMENT ON COLUMN core.county_sales_taxes.county_sales_tax_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.county_sales_taxes.county_sales_tax_code IS 'The code given to county sale tax.';
COMMENT ON COLUMN core.county_sales_taxes.county_sales_tax_name IS 'The name given to county sales tax, which is a unique field';
COMMENT ON COLUMN core.county_sales_taxes.county_id IS 'Foreign key to the table core.counties.';
COMMENT ON COLUMN core.county_sales_taxes.entity_id IS 'Foreign key to the table core.entities.';
COMMENT ON COLUMN core.county_sales_taxes.industry_id IS 'Foreign key to the table core.industries.';
COMMENT ON COLUMN core.county_sales_taxes.item_group_id IS 'Foreign key to the table core.item_groups.';
COMMENT ON COLUMN core.county_sales_taxes.rate IS 'Rate of sales tax of the county.';
COMMENT ON COLUMN core.county_sales_taxes.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.county_sales_taxes.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.currencies IS 'This table stores information on currency.';
COMMENT ON COLUMN core.currencies.currency_code IS 'The primary key of this table, code of the currency.';
COMMENT ON COLUMN core.currencies.currency_symbol IS 'Symbol of the currency.';
COMMENT ON COLUMN core.currencies.currency_name IS 'Name of the currency.';
COMMENT ON COLUMN core.currencies.hundredth_name IS 'Name given to hundredth part of the currency.';
COMMENT ON COLUMN core.currencies.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.currencies.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE transactions.customer_receipts IS 'This table stores information on receipts from customers.';
COMMENT ON COLUMN transactions.customer_receipts.receipt_id IS 'The primary key of this table, which is also a bigserial field.';
COMMENT ON COLUMN transactions.customer_receipts.transaction_master_id IS 'Foreign key to the table transactions.transaction_master.';
COMMENT ON COLUMN transactions.customer_receipts.party_id IS 'Foreign key to the table core.parties.';
COMMENT ON COLUMN transactions.customer_receipts.currency_code IS 'Foreign key to the table core.currencies.';
COMMENT ON COLUMN transactions.customer_receipts.amount IS 'Amount received from the customer.';
COMMENT ON COLUMN transactions.customer_receipts.er_debit IS '';
COMMENT ON COLUMN transactions.customer_receipts.er_credit IS '';
COMMENT ON COLUMN transactions.customer_receipts.cash_repository_id IS 'The id of the cash repository used.';
COMMENT ON COLUMN transactions.customer_receipts.posted_date IS 'The date on which the transaction was posted.';
COMMENT ON COLUMN transactions.customer_receipts.bank_account_id IS 'Foreign key to the table core.bank_accounts.';
COMMENT ON COLUMN transactions.customer_receipts.bank_instrument_code IS 'Code of the cheque received.';
COMMENT ON COLUMN transactions.customer_receipts.bank_tran_code IS 'The transaction code while the cheque is received.';


COMMENT ON TABLE transactions.day_operation IS 'This table stores information on the transaction, date & time of its occurance.';
COMMENT ON COLUMN transactions.day_operation.day_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN transactions.day_operation.office_id IS 'Foreign key to the table office.offices.';
COMMENT ON COLUMN transactions.day_operation.value_date IS '';
COMMENT ON COLUMN transactions.day_operation.started_on IS 'Date & Time when the transaction was started.';
COMMENT ON COLUMN transactions.day_operation.started_by IS 'Foreign key to the table office.users.';
COMMENT ON COLUMN transactions.day_operation.completed_on IS 'Date & Time when the tranaction was completed.';
COMMENT ON COLUMN transactions.day_operation.completed_by IS 'Foreign key to the table office.users.';
COMMENT ON COLUMN transactions.day_operation.completed IS 'Select "Yes" if the opration is completed or vice-versa.';


COMMENT ON TABLE transactions.day_operation_routines IS '';
COMMENT ON COLUMN transactions.day_operation_routines.day_operation_routine_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN transactions.day_operation_routines.day_id IS 'Foreign key to the transactions_routines.';
COMMENT ON COLUMN transactions.day_operation_routines.routine_id IS '';
COMMENT ON COLUMN transactions.day_operation_routines.started_on IS '';
COMMENT ON COLUMN transactions.day_operation_routines.completed_on IS '';


COMMENT ON TABLE office.departments IS 'This table stores information on lists of deparments and other associated information.';
COMMENT ON COLUMN office.departments.department_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN office.departments.department_code IS 'Code given to the department, which is a unique field.';
COMMENT ON COLUMN office.departments.department_name IS 'Name given to the department, which is a unique field.';
COMMENT ON COLUMN office.departments.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN office.departments.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.entities IS 'List of entities.';
COMMENT ON COLUMN core.entities.entity_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.entities.entity_name IS 'The name of the type of entity, which is a unique field.';
COMMENT ON COLUMN core.entities.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.entities.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.exchange_rate_details IS 'Details on exchange rate.';
COMMENT ON COLUMN core.exchange_rate_details.exchange_rate_detail_id IS 'The primary key of this table, which is also a bigserial field.';
COMMENT ON COLUMN core.exchange_rate_details.exchange_rate_id IS 'Foreign key to the table core.exchange_rates.';
COMMENT ON COLUMN core.exchange_rate_details.local_currency_code IS 'Foreign key to the table core.currencies';
COMMENT ON COLUMN core.exchange_rate_details.foreign_currency_code IS 'Foreign key to the table core.currencies';/* there is no cloumn foreign currency code in core.currencies */
COMMENT ON COLUMN core.exchange_rate_details.unit IS '';
COMMENT ON COLUMN core.exchange_rate_details.exchange_rate IS '';


COMMENT ON TABLE core.exchange_rates IS 'Update exchange rates.';
COMMENT ON COLUMN core.exchange_rates.exchange_rate_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.exchange_rates.updated_on IS 'Date & time of the last update of exchange rate.';
COMMENT ON COLUMN core.exchange_rates.office_id IS 'Foreign key to the table office.offices.';
COMMENT ON COLUMN core.exchange_rates.status IS '';


COMMENT ON TABLE audit.failed_logins IS '';
COMMENT ON COLUMN audit.failed_logins.failed_login_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN audit.failed_logins.user_id IS '';
COMMENT ON COLUMN audit.failed_logins.user_name IS '';
COMMENT ON COLUMN audit.failed_logins.office_id IS '';
COMMENT ON COLUMN audit.failed_logins.browser IS '';
COMMENT ON COLUMN audit.failed_logins.ip_address IS '';
COMMENT ON COLUMN audit.failed_logins.failed_date_time IS '';
COMMENT ON COLUMN audit.failed_logins.remote_user IS '';
COMMENT ON COLUMN audit.failed_logins.details IS '';


COMMENT ON TABLE core.fiscal_year IS 'Information on fiscal year.';
COMMENT ON COLUMN core.fiscal_year.fiscal_year_code IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.fiscal_year.fiscal_year_name IS 'Current fiscal year, which is a unique field.';
COMMENT ON COLUMN core.fiscal_year.starts_from IS 'The date fiscal year begins.';
COMMENT ON COLUMN core.fiscal_year.ends_on IS 'The date fiscal year ends.';
COMMENT ON COLUMN core.fiscal_year.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.fiscal_year.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.flag_types IS 'Mark rows of a table with difernt color, so that viewing is easier.';
COMMENT ON COLUMN core.flag_types.flag_type_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.flag_types.flag_type_name IS 'Name of the flag.';
COMMENT ON COLUMN core.flag_types.background_color IS 'Back-ground color of the flagged transaction.';
COMMENT ON COLUMN core.flag_types.foreground_color IS 'The foreground color of the flagged transaction.';
COMMENT ON COLUMN core.flag_types.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.flag_types.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.flags IS 'Flags are used by users to mark transactions. The flags created by a user is not visible to others.';
COMMENT ON COLUMN core.flags.flag_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.flags.user_id IS 'Foreign key to the table office.users.';
COMMENT ON COLUMN core.flags.flag_type_id IS 'Foreign key to the table core.flag_types.';
COMMENT ON COLUMN core.flags.resource IS '';
COMMENT ON COLUMN core.flags.resource_key IS '';
COMMENT ON COLUMN core.flags.resource_id IS '';
COMMENT ON COLUMN core.flags.flagged_on IS '';


COMMENT ON TABLE core.frequencies IS 'Interval of time for posting a transaction.';
COMMENT ON COLUMN core.frequencies.frequency_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.frequencies.frequency_code IS 'The code given to the interval of time.';
COMMENT ON COLUMN core.frequencies.frequency_name IS 'The name given to the interval of time, which is unique field.';


COMMENT ON TABLE core.frequency_setups IS 'Setup time interval of posting a transaction automatically.';
COMMENT ON COLUMN core.frequency_setups.frequency_setup_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.frequency_setups.fiscal_year_code IS 'Foreign key to the table core.fiscal_year.';
COMMENT ON COLUMN core.frequency_setups.frequency_setup_code IS ''; /*Does the time interval has to be only 1 month.*/
COMMENT ON COLUMN core.frequency_setups.value_date IS '';
COMMENT ON COLUMN core.frequency_setups.frequency_id IS 'Foreign key to the table core.frequencies.';
COMMENT ON COLUMN core.frequency_setups.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.frequency_setups.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.income_tax_setup IS 'Tax setup.';
COMMENT ON COLUMN core.income_tax_setup.income_tax_setup_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.income_tax_setup.office_id IS '';
COMMENT ON COLUMN core.income_tax_setup.effective_from IS 'The effective date of the tax rate.';
COMMENT ON COLUMN core.income_tax_setup.tax_rate IS 'The tax rate applicable.';
COMMENT ON COLUMN core.income_tax_setup.tax_authority_id IS '';
COMMENT ON COLUMN core.income_tax_setup.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.income_tax_setup.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.industries IS 'Details on industry the firm belongs.';
COMMENT ON COLUMN core.industries.industry_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.industries.industry_name IS 'The name of the industry, which is a unique field.';
COMMENT ON COLUMN core.industries.parent_industry_id IS '';
COMMENT ON COLUMN core.industries.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.industries.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.item_cost_prices IS 'This table stores information on cost price of a item and other associated informations.';
COMMENT ON COLUMN core.item_cost_prices.item_cost_price_id IS 'The primary key of this table, which is also a bigserial field.';
COMMENT ON COLUMN core.item_cost_prices.item_id IS 'Foreign key to the table core.items.';
COMMENT ON COLUMN core.item_cost_prices.entry_ts IS '';
COMMENT ON COLUMN core.item_cost_prices.unit_id IS 'Foreign key to the table core.units.';
COMMENT ON COLUMN core.item_cost_prices.party_id IS 'Foreign key to the table core.parties';
COMMENT ON COLUMN core.item_cost_prices.lead_time_in_days IS 'Time taken by the good to arrive in stock after it is ordered.';
COMMENT ON COLUMN core.item_cost_prices.includes_tax IS 'Tick if tax is to be included on the price of the item.';
COMMENT ON COLUMN core.item_cost_prices.price IS 'Cost price of the item.';
COMMENT ON COLUMN core.item_cost_prices.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.item_cost_prices.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.item_groups IS '';
COMMENT ON COLUMN core.item_groups.item_group_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.item_groups.item_group_code IS '';
COMMENT ON COLUMN core.item_groups.item_group_name IS '';
COMMENT ON COLUMN core.item_groups.exclude_from_purchase IS '';
COMMENT ON COLUMN core.item_groups.exclude_from_sales IS '';
COMMENT ON COLUMN core.item_groups.sales_tax_id IS '';
COMMENT ON COLUMN core.item_groups.sales_account_id IS '';
COMMENT ON COLUMN core.item_groups.sales_discount_account_id IS '';
COMMENT ON COLUMN core.item_groups.sales_return_account_id IS '';
COMMENT ON COLUMN core.item_groups.purchase_account_id IS '';
COMMENT ON COLUMN core.item_groups.purchase_discount_account_id IS '';
COMMENT ON COLUMN core.item_groups.inventory_account_id IS '';
COMMENT ON COLUMN core.item_groups.cost_of_goods_sold_account_id IS '';
COMMENT ON COLUMN core.item_groups.parent_item_group_id IS '';
COMMENT ON COLUMN core.item_groups.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.item_groups.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.item_selling_prices IS 'This table stores information on selling price of an item and other information.
 PLEASE NOTE :

    THESE ARE THE MOST EFFECTIVE STOCK ITEM PRICES.
    THE PRICE IN THIS CATALOG IS ACTUALLY PICKED UP AT THE TIME OF PURCHASE AND SALES.

    A STOCK ITEM PRICE MAY BE DIFFERENT FOR DIFFERENT UNITS.
    FURTHER, A STOCK ITEM WOULD BE SOLD AT A HIGHER PRICE WHEN SOLD LOOSE THAN WHAT IT WOULD ACTUALLY COST IN A
    COMPOUND UNIT.

    EXAMPLE, ONE CARTOON (20 BOTTLES) OF BEER BOUGHT AS A UNIT
    WOULD COST 25% LESS FROM THE SAME STORE.';
COMMENT ON COLUMN core.item_selling_prices.item_selling_price_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.item_selling_prices.item_id IS 'Foreign key to the table core.items.';
COMMENT ON COLUMN core.item_selling_prices.unit_id IS 'Foreign key to the table core.items.';
COMMENT ON COLUMN core.item_selling_prices.party_type_id IS '';
COMMENT ON COLUMN core.item_selling_prices.price_type_id IS '';
COMMENT ON COLUMN core.item_selling_prices.includes_tax IS '';
COMMENT ON COLUMN core.item_selling_prices.price IS '';
COMMENT ON COLUMN core.item_selling_prices.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.item_selling_prices.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.item_types IS '';
COMMENT ON COLUMN core.item_types.item_type_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.item_types.item_type_code IS '';
COMMENT ON COLUMN core.item_types.item_type_name IS '';
COMMENT ON COLUMN core.item_types.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.item_types.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.items IS 'This table stores information on items and other associated information.';
COMMENT ON COLUMN core.items.item_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.items.item_code IS 'Code given to the item.';
COMMENT ON COLUMN core.items.item_name IS 'Name given to the item.';
COMMENT ON COLUMN core.items.item_group_id IS '';
COMMENT ON COLUMN core.items.item_type_id IS 'Foreign key to the table core.item_groups.';
COMMENT ON COLUMN core.items.brand_id IS 'Foreign key to the table core.brands.';
COMMENT ON COLUMN core.items.preferred_supplier_id IS 'Foreign key to the table core.parties.';
COMMENT ON COLUMN core.items.lead_time_in_days IS 'Days taken for the shipment of the ordered goods.';
COMMENT ON COLUMN core.items.weight_in_grams IS 'Weight of an individual item in the unit of measure gram.';
COMMENT ON COLUMN core.items.width_in_centimeters IS 'Width of an individual item in centimeter.';
COMMENT ON COLUMN core.items.height_in_centimeters IS 'Height of an individual item in centimeter.';
COMMENT ON COLUMN core.items.length_in_centimeters IS 'Length of an individual item in centimeter.';
COMMENT ON COLUMN core.items.machinable IS 'Select "Yes" if the particular item can be handled by machine while shipping.';
COMMENT ON COLUMN core.items.preferred_shipping_mail_type_id IS 'Foreign key to the table core.shipping_mail_types.';
COMMENT ON COLUMN core.items.shipping_package_shape_id IS 'Foreign key to the table core.shipping_package_shapes.';
COMMENT ON COLUMN core.items.unit_id IS 'Foreign key to the table core.items.';
COMMENT ON COLUMN core.items.hot_item IS 'Hot item will be featured on e-commerce website. Select "Yes" if the item is hot item or vice-versa.';
COMMENT ON COLUMN core.items.cost_price IS 'Cost price of the item.';
COMMENT ON COLUMN core.items.cost_price_includes_tax IS 'Select "Yes" if the cost price includes tax or vice-versa.';
COMMENT ON COLUMN core.items.selling_price IS 'Selling price of the item.';
COMMENT ON COLUMN core.items.selling_price_includes_tax IS 'Select "Yes" if the selling price includes tax or vice-versa.';
COMMENT ON COLUMN core.items.sales_tax_id IS 'Foreign key to the table core.sales_taxes.';
COMMENT ON COLUMN core.items.reorder_unit_id IS 'Foreign key to the table core.units.';
COMMENT ON COLUMN core.items.reorder_level IS 'The level of stock on which re-order needs to be placed.';
COMMENT ON COLUMN core.items.reorder_quantity IS 'The quantity of good that needs to be ordered as the stock reaches re-order level.';
COMMENT ON COLUMN core.items.maintain_stock IS 'Select "Yes" if you want to maintain the record of transfer (in & out) of stock items.Selecting "Yes" prohibits the user from maintaining negative stock level.';
COMMENT ON COLUMN core.items.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.items.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.late_fee IS 'This table stores information on late fees and other associated information.';
COMMENT ON COLUMN core.late_fee.late_fee_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.late_fee.late_fee_code IS 'Late fee code.';
COMMENT ON COLUMN core.late_fee.late_fee_name IS 'The name given to the late fee code, which is a unique field.';
COMMENT ON COLUMN core.late_fee.is_flat_amount IS 'Select "Yes" if the late fee amount is fixed or vice-versa.';
COMMENT ON COLUMN core.late_fee.rate IS 'The rate on which late fee will be charged.';
COMMENT ON COLUMN core.late_fee.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.late_fee.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE crm.lead_sources IS '';
COMMENT ON COLUMN crm.lead_sources.lead_source_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN crm.lead_sources.lead_source_code IS '';
COMMENT ON COLUMN crm.lead_sources.lead_source_name IS '';
COMMENT ON COLUMN crm.lead_sources.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN crm.lead_sources.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE crm.lead_statuses IS '';
COMMENT ON COLUMN crm.lead_statuses.lead_status_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN crm.lead_statuses.lead_status_code IS '';
COMMENT ON COLUMN crm.lead_statuses.lead_status_name IS '';
COMMENT ON COLUMN crm.lead_statuses.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN crm.lead_statuses.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE policy.lock_outs IS '';
COMMENT ON COLUMN policy.lock_outs.lock_out_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN policy.lock_outs.user_id IS '';
COMMENT ON COLUMN policy.lock_outs.lock_out_time IS '';
COMMENT ON COLUMN policy.lock_outs.lock_out_till IS '';


COMMENT ON TABLE audit.logged_actions IS '';
COMMENT ON COLUMN audit.logged_actions.event_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN audit.logged_actions.schema_name IS '';
COMMENT ON COLUMN audit.logged_actions.table_name IS '';
COMMENT ON COLUMN audit.logged_actions.relid IS '';
COMMENT ON COLUMN audit.logged_actions.session_user_name IS '';
COMMENT ON COLUMN audit.logged_actions.application_user_name IS '';
COMMENT ON COLUMN audit.logged_actions.action_tstamp_tx IS '';
COMMENT ON COLUMN audit.logged_actions.action_tstamp_stm IS '';
COMMENT ON COLUMN audit.logged_actions.action_tstamp_clk IS '';
COMMENT ON COLUMN audit.logged_actions.transaction_id IS '';
COMMENT ON COLUMN audit.logged_actions.application_name IS '';
COMMENT ON COLUMN audit.logged_actions.client_addr IS '';
COMMENT ON COLUMN audit.logged_actions.client_port IS '';
COMMENT ON COLUMN audit.logged_actions.client_query IS '';
COMMENT ON COLUMN audit.logged_actions.action IS '';
COMMENT ON COLUMN audit.logged_actions.row_data IS '';
COMMENT ON COLUMN audit.logged_actions.changed_fields IS '';
COMMENT ON COLUMN audit.logged_actions.statement_only IS '';


COMMENT ON TABLE audit.logins IS '';
COMMENT ON COLUMN audit.logins.login_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN audit.logins.user_id IS 'Foreign key to the table office.users.';
COMMENT ON COLUMN audit.logins.office_id IS 'Foreign key to the table office.offices.';
COMMENT ON COLUMN audit.logins.browser IS 'The name of the broser used to run the application.';
COMMENT ON COLUMN audit.logins.ip_address IS 'The IP address of the of the computer network used.';
COMMENT ON COLUMN audit.logins.login_date_time IS '';
COMMENT ON COLUMN audit.logins.remote_user IS '';
COMMENT ON COLUMN audit.logins.culture IS '';


COMMENT ON TABLE policy.menu_access IS '';
COMMENT ON COLUMN policy.menu_access.access_id IS 'The primary key of this table.';
COMMENT ON COLUMN policy.menu_access.office_id IS 'Foreign key to the table office.offices.';
COMMENT ON COLUMN policy.menu_access.menu_id IS 'Foreign key to the table core.menus.';
COMMENT ON COLUMN policy.menu_access.user_id IS 'Foreign key to the table office.users';


COMMENT ON TABLE core.menu_locale IS '';
COMMENT ON COLUMN core.menu_locale.menu_locale_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.menu_locale.menu_id IS '';
COMMENT ON COLUMN core.menu_locale.culture IS '';
COMMENT ON COLUMN core.menu_locale.menu_text IS '';


COMMENT ON TABLE policy.menu_policy IS '';
COMMENT ON COLUMN policy.menu_policy.policy_id IS 'The primary key of this table, which is also a bigserial field.';
COMMENT ON COLUMN policy.menu_policy.menu_id IS 'Foreign key to the table core.menus.';
COMMENT ON COLUMN policy.menu_policy.office_id IS 'Foreign key to the table office.offices.';
COMMENT ON COLUMN policy.menu_policy.inherit_in_child_offices IS '';
COMMENT ON COLUMN policy.menu_policy.role_id IS '';
COMMENT ON COLUMN policy.menu_policy.user_id IS '';
COMMENT ON COLUMN policy.menu_policy.scope IS '';


COMMENT ON TABLE core.menus IS 'This table stores information on the easy to use menu of MixERP.';
COMMENT ON COLUMN core.menus.menu_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.menus.menu_text IS 'The name of the menu.';
COMMENT ON COLUMN core.menus.url IS 'The  location of the menu.';
COMMENT ON COLUMN core.menus.menu_code IS 'The code of the menu, which is also a unique field.';
/*COMMENT ON COLUMN core.menus.level IS '';*/
COMMENT ON COLUMN core.menus.parent_menu_id IS 'Foreign key to the table core.menus.';
COMMENT ON COLUMN core.menus.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.menus.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE transactions.non_gl_stock_details IS '';
COMMENT ON COLUMN transactions.non_gl_stock_details.non_gl_stock_detail_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN transactions.non_gl_stock_details.non_gl_stock_master_id IS '';
COMMENT ON COLUMN transactions.non_gl_stock_details.value_date IS '';
COMMENT ON COLUMN transactions.non_gl_stock_details.item_id IS 'Foreign key to the table core.items.';
COMMENT ON COLUMN transactions.non_gl_stock_details.quantity IS '';
COMMENT ON COLUMN transactions.non_gl_stock_details.unit_id IS 'Foreign key to the table core.items.';
COMMENT ON COLUMN transactions.non_gl_stock_details.base_quantity IS '';
COMMENT ON COLUMN transactions.non_gl_stock_details.base_unit_id IS '';
COMMENT ON COLUMN transactions.non_gl_stock_details.price IS '';
COMMENT ON COLUMN transactions.non_gl_stock_details.discount IS '';
COMMENT ON COLUMN transactions.non_gl_stock_details.shipping_charge IS '';
COMMENT ON COLUMN transactions.non_gl_stock_details.sales_tax_id IS '';
COMMENT ON COLUMN transactions.non_gl_stock_details.tax IS '';
COMMENT ON COLUMN transactions.non_gl_stock_details.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN transactions.non_gl_stock_details.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE transactions.non_gl_stock_master IS 'This table stores information of quotations which were upgraded to order(s).';
COMMENT ON COLUMN transactions.non_gl_stock_master.non_gl_stock_master_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN transactions.non_gl_stock_master.value_date IS '';
COMMENT ON COLUMN transactions.non_gl_stock_master.book IS '';
COMMENT ON COLUMN transactions.non_gl_stock_master.party_id IS '';
COMMENT ON COLUMN transactions.non_gl_stock_master.price_type_id IS '';
COMMENT ON COLUMN transactions.non_gl_stock_master.transaction_ts IS '';
COMMENT ON COLUMN transactions.non_gl_stock_master.login_id IS 'Foreign key to the table audit.logins.';
COMMENT ON COLUMN transactions.non_gl_stock_master.user_id IS 'Foreign key to the table office.users.';
COMMENT ON COLUMN transactions.non_gl_stock_master.office_id IS 'Foreign key to the table office.offices.';
COMMENT ON COLUMN transactions.non_gl_stock_master.reference_number IS '';
COMMENT ON COLUMN transactions.non_gl_stock_master.statement_reference IS '';
COMMENT ON COLUMN transactions.non_gl_stock_master.non_taxable IS '';
COMMENT ON COLUMN transactions.non_gl_stock_master.salesperson_id IS 'Foreign key to the table core.salespersons.';
COMMENT ON COLUMN transactions.non_gl_stock_master.shipper_id IS 'Foreign key to the table core.shippers.';
COMMENT ON COLUMN transactions.non_gl_stock_master.shipping_address_id IS 'Foreign key to the table core.shipping_addresses.';
COMMENT ON COLUMN transactions.non_gl_stock_master.shipping_charge IS '';
COMMENT ON COLUMN transactions.non_gl_stock_master.store_id IS 'Foreign key to the table office.stores.';
COMMENT ON COLUMN transactions.non_gl_stock_master.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN transactions.non_gl_stock_master.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE transactions.non_gl_stock_master_relations IS '';
COMMENT ON COLUMN transactions.non_gl_stock_master_relations.non_gl_stock_master_relation_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN transactions.non_gl_stock_master_relations.order_non_gl_stock_master_id IS 'Foreign key to the table transactions.non_gl_stock_master.';
COMMENT ON COLUMN transactions.non_gl_stock_master_relations.quotation_non_gl_stock_master_id IS 'Foreign key to the table transactions.non_gl_stock_master.';


COMMENT ON TABLE transactions.non_gl_stock_tax_details IS '';
COMMENT ON COLUMN transactions.non_gl_stock_tax_details.non_gl_stock_detail_id IS 'The primary key of this table.';
COMMENT ON COLUMN transactions.non_gl_stock_tax_details.sales_tax_detail_id IS 'Foreign key to the table core.state_sales_taxes';
COMMENT ON COLUMN transactions.non_gl_stock_tax_details.state_sales_tax_id IS 'Foreign key to the table core.county_sales_taxes.';
COMMENT ON COLUMN transactions.non_gl_stock_tax_details.county_sales_tax_id IS 'Foreign key to the table core.county_sales_taxes';
COMMENT ON COLUMN transactions.non_gl_stock_tax_details.principal IS '';
COMMENT ON COLUMN transactions.non_gl_stock_tax_details.rate IS '';
COMMENT ON COLUMN transactions.non_gl_stock_tax_details.tax IS '';


COMMENT ON TABLE office.offices IS 'This table stores information on various braches of the entity.';
COMMENT ON COLUMN office.offices.office_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN office.offices.office_code IS 'The code given to the office.';
COMMENT ON COLUMN office.offices.office_name IS 'The name given to the office which is also a unique field.';
COMMENT ON COLUMN office.offices.nick_name IS 'Another name of the office.';
COMMENT ON COLUMN office.offices.registration_date IS 'The date of registration of the office.';
COMMENT ON COLUMN office.offices.currency_code IS 'Foreign key to the table core.currencies.';
COMMENT ON COLUMN office.offices.po_box IS 'The Post Box number of the office.';
COMMENT ON COLUMN office.offices.address_line_1 IS 'The address of the office.';
COMMENT ON COLUMN office.offices.address_line_2 IS 'The address of the office.';
COMMENT ON COLUMN office.offices.street IS 'The name of the street where the office is located.';
COMMENT ON COLUMN office.offices.city IS 'The name of the city where the office is located.';
COMMENT ON COLUMN office.offices.state IS 'The name of the state where the office is located.';
COMMENT ON COLUMN office.offices.zip_code IS 'ZIP code of the office.';
COMMENT ON COLUMN office.offices.country IS 'The name of the county where the office is located.';
COMMENT ON COLUMN office.offices.phone IS 'Phone number of the office.';
COMMENT ON COLUMN office.offices.fax IS 'Fax number of the office.';
COMMENT ON COLUMN office.offices.email IS 'E-mail address of the office.';
COMMENT ON COLUMN office.offices.url IS 'Web address of the office.';
COMMENT ON COLUMN office.offices.registration_number IS 'The registration number of the enntity..';
COMMENT ON COLUMN office.offices.pan_number IS 'Permanent Office Number of the entity.';
COMMENT ON COLUMN office.offices.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN office.offices.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';
COMMENT ON COLUMN office.offices.parent_office_id IS 'Foreign key to the table office.offices.';


COMMENT ON TABLE crm.opportunity_stages IS '';
COMMENT ON COLUMN crm.opportunity_stages.opportunity_stage_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN crm.opportunity_stages.opportunity_stage_code IS '';
COMMENT ON COLUMN crm.opportunity_stages.opportunity_stage_name IS '';
COMMENT ON COLUMN crm.opportunity_stages.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN crm.opportunity_stages.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.parties IS 'This table stores information on parties and information associated with them.';
COMMENT ON COLUMN core.parties.party_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.parties.party_type_id IS 'Foreign key to the table core.party_types.';
COMMENT ON COLUMN core.parties.party_code IS 'Code given to the party.';
COMMENT ON COLUMN core.parties.first_name IS 'First name of the party.';
COMMENT ON COLUMN core.parties.middle_name IS 'Middle name of the party.';
COMMENT ON COLUMN core.parties.last_name IS 'Family name of the party.';
COMMENT ON COLUMN core.parties.party_name IS 'Full-name of the party.';
COMMENT ON COLUMN core.parties.date_of_birth IS 'Date of birth of the party.';
COMMENT ON COLUMN core.parties.entity_id IS 'Foreign key to the table core.entities.';
COMMENT ON COLUMN core.parties.industry_id IS 'Foreign key to the table core.industries.';
COMMENT ON COLUMN core.parties.country_id IS 'Foreign key to the table core.countries.';
COMMENT ON COLUMN core.parties.state_id IS 'Foreign key to the table core.states.';
COMMENT ON COLUMN core.parties.zip_code IS 'Zip code of the party.';
COMMENT ON COLUMN core.parties.address_line_1 IS 'The address of the party.';
COMMENT ON COLUMN core.parties.address_line_2 IS 'The address of the party.';
COMMENT ON COLUMN core.parties.street IS 'The street name where the party is located.';
COMMENT ON COLUMN core.parties.city IS 'The city name where the party is located.';
COMMENT ON COLUMN core.parties.phone IS 'The phone number of the party.';
COMMENT ON COLUMN core.parties.fax IS 'The fax number of the party.';
COMMENT ON COLUMN core.parties.cell IS 'Cell-phone number of the party.';
COMMENT ON COLUMN core.parties.email IS 'E-mail address of the party.';
COMMENT ON COLUMN core.parties.url IS 'Web address of the party. ';
COMMENT ON COLUMN core.parties.pan_number IS 'Permanent Address Number of the party.';
COMMENT ON COLUMN core.parties.sst_number IS '';
COMMENT ON COLUMN core.parties.cst_number IS '';
COMMENT ON COLUMN core.parties.currency_code IS 'Foreign key to the table core.currencies.';
COMMENT ON COLUMN core.parties.allow_credit IS 'Select "Yes" if you want to allow credit to the party.';
COMMENT ON COLUMN core.parties.maximum_credit_period IS 'Maximum credit period ';
COMMENT ON COLUMN core.parties.maximum_credit_amount IS '';
COMMENT ON COLUMN core.parties.account_id IS 'Foreign key to the table core.accounts.';
COMMENT ON COLUMN core.parties.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.parties.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.party_types IS 'This table stores information on types of parties and other infornation associated with it like weather a party is Agent/Customer/Dealer/Supplier..';
COMMENT ON COLUMN core.party_types.party_type_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.party_types.party_type_code IS 'Code given to the party';
COMMENT ON COLUMN core.party_types.party_type_name IS 'Name of the party which is a unique field.';
COMMENT ON COLUMN core.party_types.is_supplier IS 'Selct "Yes" if the party is supplier or vice-versa.';
COMMENT ON COLUMN core.party_types.account_id IS 'Foreign key to the table core.accounts.When a new party is added, this becomes the parent account.';
COMMENT ON COLUMN core.party_types.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.party_types.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.payment_terms IS 'This table stores information on terms of payment like due date, grace period and information associated with it.';
COMMENT ON COLUMN core.payment_terms.payment_term_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.payment_terms.payment_term_code IS 'Code given to the terms of payment.';
COMMENT ON COLUMN core.payment_terms.payment_term_name IS 'The name given to the term of payment, which is also a unique field.';
COMMENT ON COLUMN core.payment_terms.due_on_date IS '';
COMMENT ON COLUMN core.payment_terms.due_days IS '';
COMMENT ON COLUMN core.payment_terms.due_frequency_id IS '';
COMMENT ON COLUMN core.payment_terms.grace_peiod IS '';
COMMENT ON COLUMN core.payment_terms.late_fee_id IS '';
COMMENT ON COLUMN core.payment_terms.late_fee_posting_frequency_id IS '';
COMMENT ON COLUMN core.payment_terms.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.payment_terms.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.price_types IS '';
COMMENT ON COLUMN core.price_types.price_type_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.price_types.price_type_code IS '';
COMMENT ON COLUMN core.price_types.price_type_name IS '';
COMMENT ON COLUMN core.price_types.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.price_types.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.recurring_invoice_setup IS '';
COMMENT ON COLUMN core.recurring_invoice_setup.recurring_invoice_setup_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.recurring_invoice_setup.recurring_invoice_id IS '';
COMMENT ON COLUMN core.recurring_invoice_setup.party_id IS '';
COMMENT ON COLUMN core.recurring_invoice_setup.starts_from IS '';
COMMENT ON COLUMN core.recurring_invoice_setup.ends_on IS '';
COMMENT ON COLUMN core.recurring_invoice_setup.recurring_amount IS '';
COMMENT ON COLUMN core.recurring_invoice_setup.payment_term_id IS '';
COMMENT ON COLUMN core.recurring_invoice_setup.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.recurring_invoice_setup.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.recurring_invoices IS '';
COMMENT ON COLUMN core.recurring_invoices.recurring_invoice_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.recurring_invoices.recurring_invoice_code IS '';
COMMENT ON COLUMN core.recurring_invoices.recurring_invoice_name IS '';
COMMENT ON COLUMN core.recurring_invoices.item_id IS 'Foreign key to the table core.items.';
COMMENT ON COLUMN core.recurring_invoices.compound_item_id IS '';
COMMENT ON COLUMN core.recurring_invoices.recurring_frequency_id IS '';
COMMENT ON COLUMN core.recurring_invoices.recurring_amount IS '';
COMMENT ON COLUMN core.recurring_invoices.auto_trigger_on_sales IS '';
COMMENT ON COLUMN core.recurring_invoices.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.recurring_invoices.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE office.roles IS '';
COMMENT ON COLUMN office.roles.role_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN office.roles.role_code IS '';
COMMENT ON COLUMN office.roles.role_name IS '';
COMMENT ON COLUMN office.roles.is_admin IS '';
COMMENT ON COLUMN office.roles.is_system IS '';
COMMENT ON COLUMN office.roles.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN office.roles.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.rounding_methods IS 'This table stores information on rounding off the amount. ';
COMMENT ON COLUMN core.rounding_methods.rounding_method_code IS 'The primary key of this table.';
COMMENT ON COLUMN core.rounding_methods.rounding_method_name IS 'The column that describes this table.';


COMMENT ON TABLE transactions.routines IS '';
COMMENT ON COLUMN transactions.routines.routine_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN transactions.routines.order IS '';
COMMENT ON COLUMN transactions.routines.routine_code IS '';
COMMENT ON COLUMN transactions.routines.routine_name IS '';
COMMENT ON COLUMN transactions.routines.status IS '';


COMMENT ON TABLE core.sales_tax_details IS 'This table stores information on sales tax and other information associated with it.';
COMMENT ON COLUMN core.sales_tax_details.sales_tax_detail_id IS '';
COMMENT ON COLUMN core.sales_tax_details.sales_tax_id IS '';
COMMENT ON COLUMN core.sales_tax_details.sales_tax_type_id IS '';
COMMENT ON COLUMN core.sales_tax_details.priority IS '';
COMMENT ON COLUMN core.sales_tax_details.sales_tax_detail_code IS '';
COMMENT ON COLUMN core.sales_tax_details.sales_tax_detail_name IS '';
COMMENT ON COLUMN core.sales_tax_details.based_on_shipping_address IS '';
COMMENT ON COLUMN core.sales_tax_details.check_nexus IS '';
COMMENT ON COLUMN core.sales_tax_details.applied_on_shipping_charge IS '';
COMMENT ON COLUMN core.sales_tax_details.state_sales_tax_id IS '';
COMMENT ON COLUMN core.sales_tax_details.county_sales_tax_id IS '';
COMMENT ON COLUMN core.sales_tax_details.tax_rate_type_code IS '';
COMMENT ON COLUMN core.sales_tax_details.rate IS '';
COMMENT ON COLUMN core.sales_tax_details.reporting_tax_authority_id IS '';
COMMENT ON COLUMN core.sales_tax_details.collecting_tax_authority_id IS '';
COMMENT ON COLUMN core.sales_tax_details.collecting_account_id IS '';
COMMENT ON COLUMN core.sales_tax_details.use_tax_collecting_account_id IS '';
COMMENT ON COLUMN core.sales_tax_details.rounding_method_code IS '';
COMMENT ON COLUMN core.sales_tax_details.rounding_decimal_places IS '';
COMMENT ON COLUMN core.sales_tax_details.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.sales_tax_details.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.sales_tax_exempt_details IS 'This table stores information on sales tax exemption and other information associated with it.';
COMMENT ON COLUMN core.sales_tax_exempt_details.sales_tax_exempt_detail_id IS '';
COMMENT ON COLUMN core.sales_tax_exempt_details.sales_tax_exempt_id IS '';
COMMENT ON COLUMN core.sales_tax_exempt_details.entity_id IS '';
COMMENT ON COLUMN core.sales_tax_exempt_details.industry_id IS '';
COMMENT ON COLUMN core.sales_tax_exempt_details.party_id IS '';
COMMENT ON COLUMN core.sales_tax_exempt_details.party_type_id IS '';
COMMENT ON COLUMN core.sales_tax_exempt_details.item_id IS 'Foreign key to the table core.items.';
COMMENT ON COLUMN core.sales_tax_exempt_details.item_group_id IS '';
COMMENT ON COLUMN core.sales_tax_exempt_details.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.sales_tax_exempt_details.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.sales_tax_exempts IS 'This table stores information on sales tax exemption and other information associated with it.';
COMMENT ON COLUMN core.sales_tax_exempts.sales_tax_exempt_id IS 'The primary key of the table, which is also a serial field.';
COMMENT ON COLUMN core.sales_tax_exempts.tax_master_id IS 'Foreign key to the table core.tax_master.';
COMMENT ON COLUMN core.sales_tax_exempts.sales_tax_exempt_code IS 'Sakes tax exemption code.';
COMMENT ON COLUMN core.sales_tax_exempts.sales_tax_exempt_name IS 'The name given to the sales tax exemption.';
COMMENT ON COLUMN core.sales_tax_exempts.tax_exempt_type_id IS 'Foreign key to the table core.tax_exempt_types.';
COMMENT ON COLUMN core.sales_tax_exempts.store_id IS 'Foreign key to the table office.stores.';
COMMENT ON COLUMN core.sales_tax_exempts.sales_tax_id IS 'Foreign key to the table core.sales_taxes.';
COMMENT ON COLUMN core.sales_tax_exempts.valid_from IS 'The effective date of the sales exemption cstegory.';
COMMENT ON COLUMN core.sales_tax_exempts.valid_till IS 'The last date of the sales exemption category.';
COMMENT ON COLUMN core.sales_tax_exempts.price_from IS 'The minimum sales amount required to fall into the sales exemption category.';
COMMENT ON COLUMN core.sales_tax_exempts.price_to IS 'The maximum sales amount that falls into the sales exemption category.';
COMMENT ON COLUMN core.sales_tax_exempts.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.sales_tax_exempts.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.sales_tax_types IS 'Type of sales tax and information associated with it.';
COMMENT ON COLUMN core.sales_tax_types.sales_tax_type_id IS 'The primary key of the table, which is also a serial field.';
COMMENT ON COLUMN core.sales_tax_types.sales_tax_type_code IS 'The code given to the sales tax type.';
COMMENT ON COLUMN core.sales_tax_types.sales_tax_type_name IS 'The name given to the type of a sales tax.';
COMMENT ON COLUMN core.sales_tax_types.is_vat IS 'Select "Yes" if the sales tax is VAT.';
COMMENT ON COLUMN core.sales_tax_types.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.sales_tax_types.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.sales_taxes IS 'Details on sales tax.';
COMMENT ON COLUMN core.sales_taxes.sales_tax_id IS 'Primary key of the table, which is also a serial field.';
COMMENT ON COLUMN core.sales_taxes.tax_master_id IS 'Foreign key to the table core.tax_master.';
COMMENT ON COLUMN core.sales_taxes.office_id IS 'Foreign key to the table office.offices.';
COMMENT ON COLUMN core.sales_taxes.sales_tax_code IS 'The code given to the sales tax.';
COMMENT ON COLUMN core.sales_taxes.sales_tax_name IS 'The name given to the sales tax, which is also a unique field.';
COMMENT ON COLUMN core.sales_taxes.is_exemption IS '';
COMMENT ON COLUMN core.sales_taxes.tax_base_amount_type_code IS 'Foreign key to the table core.tax_base_amount_types.';
COMMENT ON COLUMN core.sales_taxes.rate IS 'Sales tax rate.';
COMMENT ON COLUMN core.sales_taxes.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.sales_taxes.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.sales_teams IS 'Details on sales team.';
COMMENT ON COLUMN core.sales_teams.sales_team_id IS 'The primary key of the table, which is also a serial field.';
COMMENT ON COLUMN core.sales_teams.sales_team_code IS 'The code given to the sales team.';
COMMENT ON COLUMN core.sales_teams.sales_team_name IS 'The name given to the sales team, which is also a unique field.';
COMMENT ON COLUMN core.sales_teams.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.sales_teams.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.salesperson_bonus_setups IS 'Setup bonuses to salespersons.';
COMMENT ON COLUMN core.salesperson_bonus_setups.salesperson_bonus_setup_id IS 'The primary key of the table, which is also a serial field.';
COMMENT ON COLUMN core.salesperson_bonus_setups.salesperson_id IS 'The primary key from the table office.users (foreign key to this table).';
COMMENT ON COLUMN core.salesperson_bonus_setups.bonus_slab_id IS 'A unique numeric value assigned to the bonus slab';
COMMENT ON COLUMN core.salesperson_bonus_setups.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.salesperson_bonus_setups.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.salespersons IS 'Details on sales person and other information associated with it.';
COMMENT ON COLUMN core.salespersons.salesperson_id IS 'The primay key of the table.';
COMMENT ON COLUMN core.salespersons.sales_team_id IS 'The id given to the sales person.';
COMMENT ON COLUMN core.salespersons.salesperson_code IS 'The code given to the sales person.';
COMMENT ON COLUMN core.salespersons.salesperson_name IS 'The name of the sales person, which is also a unique field.';
COMMENT ON COLUMN core.salespersons.address IS 'The address of the sales person.';
COMMENT ON COLUMN core.salespersons.contact_number IS 'The contact number of the sales person.';
COMMENT ON COLUMN core.salespersons.commission_rate IS 'The rate of comission assigned to the sales person.';
COMMENT ON COLUMN core.salespersons.account_id IS 'Foreign key to the table core.accounts.';
COMMENT ON COLUMN core.salespersons.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.salespersons.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.shippers IS 'Details on shippers (shipping company) and other information associated with it.';
COMMENT ON COLUMN core.shippers.shipper_id IS 'The primary key of the table, which is also a bigserial field.';
COMMENT ON COLUMN core.shippers.shipper_code IS 'The code of the shipper.';
COMMENT ON COLUMN core.shippers.company_name IS 'Tha name of the shipping company. ';
COMMENT ON COLUMN core.shippers.shipper_name IS 'The name of the shipper.';
COMMENT ON COLUMN core.shippers.po_box IS 'The P.O Box number of the shipper.';
COMMENT ON COLUMN core.shippers.address_line_1 IS 'Address 1.';
COMMENT ON COLUMN core.shippers.address_line_2 IS 'Address 2.';
COMMENT ON COLUMN core.shippers.street IS 'The street name where the shipper operates.';
COMMENT ON COLUMN core.shippers.city IS 'The city name of the city where the shipper operates.';
COMMENT ON COLUMN core.shippers.state IS 'The name of the state where the shipper operates.';
COMMENT ON COLUMN core.shippers.country IS 'The name of the county where the shipper operates.';
COMMENT ON COLUMN core.shippers.phone IS 'The phone number of the shipper.';
COMMENT ON COLUMN core.shippers.fax IS 'The fax number of the shipper.';
COMMENT ON COLUMN core.shippers.cell IS 'The cell number of the shipper.';
COMMENT ON COLUMN core.shippers.email IS 'The e-mail address of the shipper.';
COMMENT ON COLUMN core.shippers.url IS 'The web address of the shipper.';
COMMENT ON COLUMN core.shippers.contact_person IS 'The contact person or agent of the shipping company.';
COMMENT ON COLUMN core.shippers.contact_po_box IS 'The P.O. Box number of the agent.';
COMMENT ON COLUMN core.shippers.contact_address_line_1 IS 'Address 1 of the shipping agent.';
COMMENT ON COLUMN core.shippers.contact_address_line_2 IS 'Address 2 of the shipping agent.';
COMMENT ON COLUMN core.shippers.contact_street IS 'The street name where the shipping agent resides.';
COMMENT ON COLUMN core.shippers.contact_city IS 'THe city name where the shipping agent resides.';
COMMENT ON COLUMN core.shippers.contact_state IS '';
COMMENT ON COLUMN core.shippers.contact_country IS '';
COMMENT ON COLUMN core.shippers.contact_email IS '';
COMMENT ON COLUMN core.shippers.contact_phone IS '';
COMMENT ON COLUMN core.shippers.contact_cell IS '';
COMMENT ON COLUMN core.shippers.factory_address IS '';
COMMENT ON COLUMN core.shippers.pan_number IS '';
COMMENT ON COLUMN core.shippers.sst_number IS '';
COMMENT ON COLUMN core.shippers.cst_number IS '';
COMMENT ON COLUMN core.shippers.account_id IS 'Foreign key to the table core.accounts.';
COMMENT ON COLUMN core.shippers.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.shippers.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.shipping_addresses IS 'Details on shipping address and other information associated with it.';
COMMENT ON COLUMN core.shipping_addresses.shipping_address_id IS '';
COMMENT ON COLUMN core.shipping_addresses.shipping_address_code IS '';
COMMENT ON COLUMN core.shipping_addresses.party_id IS '';
COMMENT ON COLUMN core.shipping_addresses.country_id IS '';
COMMENT ON COLUMN core.shipping_addresses.state_id IS '';
COMMENT ON COLUMN core.shipping_addresses.zip_code IS '';
COMMENT ON COLUMN core.shipping_addresses.address_line_1 IS '';
COMMENT ON COLUMN core.shipping_addresses.address_line_2 IS '';
COMMENT ON COLUMN core.shipping_addresses.street IS '';
COMMENT ON COLUMN core.shipping_addresses.city IS '';
COMMENT ON COLUMN core.shipping_addresses.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.shipping_addresses.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.shipping_mail_types IS 'Details on shipping mail type (Express, priority mail, parcel post etc.).';
COMMENT ON COLUMN core.shipping_mail_types.shipping_mail_type_id IS 'The primary key of the table.';
COMMENT ON COLUMN core.shipping_mail_types.shipping_mail_type_code IS 'The id of the shipping mail type.';
COMMENT ON COLUMN core.shipping_mail_types.shipping_mail_type_name IS 'The name given to the shipping mail type, which is also a unique field.';
COMMENT ON COLUMN core.shipping_mail_types.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.shipping_mail_types.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.shipping_package_shapes IS 'Details on shipping package and other information assiciated with it.';
COMMENT ON COLUMN core.shipping_package_shapes.shipping_package_shape_id IS 'Primary key of the table.';
COMMENT ON COLUMN core.shipping_package_shapes.shipping_package_shape_code IS 'Code given to the shipping package shape.';
COMMENT ON COLUMN core.shipping_package_shapes.shipping_package_shape_name IS 'Name given to the shipping package shape, which is a unique field.';
COMMENT ON COLUMN core.shipping_package_shapes.is_rectangular IS 'Select "t" if the shape of the package is rectangular or vice-versa.';
COMMENT ON COLUMN core.shipping_package_shapes.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.shipping_package_shapes.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.state_sales_taxes IS 'Details on sales tax of the state in the U.S.A and other information associated with it.';
COMMENT ON COLUMN core.state_sales_taxes.state_sales_tax_id IS 'The primary key of the table.';
COMMENT ON COLUMN core.state_sales_taxes.state_sales_tax_code IS 'The code given to the state sales tax.';
COMMENT ON COLUMN core.state_sales_taxes.state_sales_tax_name IS 'The  name of the sales tax, which is also a unique field.';
COMMENT ON COLUMN core.state_sales_taxes.state_id IS 'Foreign key to the table core.states.';
COMMENT ON COLUMN core.state_sales_taxes.entity_id IS 'Foreign key to the table core.entities.';
COMMENT ON COLUMN core.state_sales_taxes.industry_id IS 'Foreign key to the table core.industries.';
COMMENT ON COLUMN core.state_sales_taxes.item_group_id IS 'Foreign key to the table core.item_groups.';
COMMENT ON COLUMN core.state_sales_taxes.rate IS 'Tax rate of the state.';
COMMENT ON COLUMN core.state_sales_taxes.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.state_sales_taxes.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.states IS 'Information on the states of the U.S.A.';
COMMENT ON COLUMN core.states.state_id IS 'Primary key of the table.';
COMMENT ON COLUMN core.states.country_id IS 'Foreign key to the table core.states.';
COMMENT ON COLUMN core.states.state_code IS 'The code word given to the state name.';
COMMENT ON COLUMN core.states.state_name IS 'Tha name of the states in the U.S.A, which is a unique field.';
COMMENT ON COLUMN core.states.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.states.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE transactions.stock_details IS '';
COMMENT ON COLUMN transactions.stock_details.stock_detail_id IS '';
COMMENT ON COLUMN transactions.stock_details.value_date IS '';
COMMENT ON COLUMN transactions.stock_details.stock_master_id IS '';
COMMENT ON COLUMN transactions.stock_details.tran_type IS '';
COMMENT ON COLUMN transactions.stock_details.store_id IS '';
COMMENT ON COLUMN transactions.stock_details.item_id IS 'Foreign key to the table core.items.';
COMMENT ON COLUMN transactions.stock_details.quantity IS '';
COMMENT ON COLUMN transactions.stock_details.unit_id IS 'Foreign key to the table core.items.';
COMMENT ON COLUMN transactions.stock_details.base_quantity IS '';
COMMENT ON COLUMN transactions.stock_details.base_unit_id IS '';
COMMENT ON COLUMN transactions.stock_details.price IS '';
COMMENT ON COLUMN transactions.stock_details.cost_of_goods_sold IS '';
COMMENT ON COLUMN transactions.stock_details.discount IS '';
COMMENT ON COLUMN transactions.stock_details.shipping_charge IS '';
COMMENT ON COLUMN transactions.stock_details.sales_tax_id IS '';
COMMENT ON COLUMN transactions.stock_details.tax IS '';
COMMENT ON COLUMN transactions.stock_details.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN transactions.stock_details.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE transactions.stock_master IS '';
COMMENT ON COLUMN transactions.stock_master.stock_master_id IS '';
COMMENT ON COLUMN transactions.stock_master.transaction_master_id IS '';
COMMENT ON COLUMN transactions.stock_master.value_date IS '';
COMMENT ON COLUMN transactions.stock_master.party_id IS '';
COMMENT ON COLUMN transactions.stock_master.salesperson_id IS '';
COMMENT ON COLUMN transactions.stock_master.price_type_id IS '';
COMMENT ON COLUMN transactions.stock_master.is_credit IS '';
COMMENT ON COLUMN transactions.stock_master.payment_term_id IS '';
COMMENT ON COLUMN transactions.stock_master.shipper_id IS '';
COMMENT ON COLUMN transactions.stock_master.shipping_address_id IS '';
COMMENT ON COLUMN transactions.stock_master.shipping_charge IS '';
COMMENT ON COLUMN transactions.stock_master.store_id IS '';
COMMENT ON COLUMN transactions.stock_master.non_taxable IS '';
COMMENT ON COLUMN transactions.stock_master.cash_repository_id IS '';
COMMENT ON COLUMN transactions.stock_master.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN transactions.stock_master.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE transactions.stock_master_non_gl_relations IS 'This table stores information of Non GL Stock Transactions such as orders and quotations
which were upgraded to deliveries or invoices.';
COMMENT ON COLUMN transactions.stock_master_non_gl_relations.stock_master_non_gl_relation_id IS '';
COMMENT ON COLUMN transactions.stock_master_non_gl_relations.stock_master_id IS '';
COMMENT ON COLUMN transactions.stock_master_non_gl_relations.non_gl_stock_master_id IS '';


COMMENT ON TABLE transactions.stock_return IS '';
COMMENT ON COLUMN transactions.stock_return.sales_return_id IS '';
COMMENT ON COLUMN transactions.stock_return.transaction_master_id IS '';
COMMENT ON COLUMN transactions.stock_return.return_transaction_master_id IS '';


COMMENT ON TABLE transactions.stock_tax_details IS '';
COMMENT ON COLUMN transactions.stock_tax_details.stock_detail_id IS '';
COMMENT ON COLUMN transactions.stock_tax_details.sales_tax_detail_id IS '';
COMMENT ON COLUMN transactions.stock_tax_details.state_sales_tax_id IS '';
COMMENT ON COLUMN transactions.stock_tax_details.county_sales_tax_id IS '';
COMMENT ON COLUMN transactions.stock_tax_details.principal IS '';
COMMENT ON COLUMN transactions.stock_tax_details.rate IS '';
COMMENT ON COLUMN transactions.stock_tax_details.tax IS '';


COMMENT ON TABLE policy.store_policies IS 'STORE POLICY DEFINES THE RIGHT OF USERS TO ACCESS A STORE.
    AN ADMINISTRATOR CAN ACCESS ALL THE stores, BY DEFAULT.';
COMMENT ON COLUMN policy.store_policies.store_policy_id IS '';
COMMENT ON COLUMN policy.store_policies.written_by_user_id IS '';
COMMENT ON COLUMN policy.store_policies.status IS '';
COMMENT ON COLUMN policy.store_policies.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN policy.store_policies.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE policy.store_policy_details IS '';
COMMENT ON COLUMN policy.store_policy_details.store_policy_detail_id IS '';
COMMENT ON COLUMN policy.store_policy_details.store_policy_id IS '';
COMMENT ON COLUMN policy.store_policy_details.user_id IS '';
COMMENT ON COLUMN policy.store_policy_details.store_id IS '';
COMMENT ON COLUMN policy.store_policy_details.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN policy.store_policy_details.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE office.store_types IS '';
COMMENT ON COLUMN office.store_types.store_type_id IS '';
COMMENT ON COLUMN office.store_types.store_type_code IS '';
COMMENT ON COLUMN office.store_types.store_type_name IS '';
COMMENT ON COLUMN office.store_types.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN office.store_types.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE office.stores IS '';
COMMENT ON COLUMN office.stores.store_id IS '';
COMMENT ON COLUMN office.stores.office_id IS '';
COMMENT ON COLUMN office.stores.store_code IS '';
COMMENT ON COLUMN office.stores.store_name IS '';
COMMENT ON COLUMN office.stores.address IS '';
COMMENT ON COLUMN office.stores.store_type_id IS '';
COMMENT ON COLUMN office.stores.allow_sales IS '';
COMMENT ON COLUMN office.stores.sales_tax_id IS '';
COMMENT ON COLUMN office.stores.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN office.stores.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.tax_authorities IS 'Details on tax authorities, its address and other information associated with it.';
COMMENT ON COLUMN core.tax_authorities.tax_authority_id IS 'The primary key of the table.';
COMMENT ON COLUMN core.tax_authorities.tax_master_id IS 'Foreign key to the table core.tax_master.';
COMMENT ON COLUMN core.tax_authorities.tax_authority_code IS 'The code given to the tax authority.';
COMMENT ON COLUMN core.tax_authorities.tax_authority_name IS 'The name of the tax authority, which is a unique field.';
COMMENT ON COLUMN core.tax_authorities.country_id IS 'Foreign key to the table core.countries.';
COMMENT ON COLUMN core.tax_authorities.state_id IS 'Foreign key to the table core.states.';
COMMENT ON COLUMN core.tax_authorities.zip_code IS 'ZIP code of the tax office.';
COMMENT ON COLUMN core.tax_authorities.address_line_1 IS 'Address 1.';
COMMENT ON COLUMN core.tax_authorities.address_line_2 IS 'Address 2.';
COMMENT ON COLUMN core.tax_authorities.street IS 'Street address.';
COMMENT ON COLUMN core.tax_authorities.city IS'Name of the city.';
COMMENT ON COLUMN core.tax_authorities.phone IS 'Phone number of the tax office.';
COMMENT ON COLUMN core.tax_authorities.fax IS 'Fax number of the tax office.';
COMMENT ON COLUMN core.tax_authorities.cell IS 'Cell phone number of the tax office.';
COMMENT ON COLUMN core.tax_authorities.email IS 'E-mail address of the tax office.';
COMMENT ON COLUMN core.tax_authorities.url IS 'Web address of the tax office.';
COMMENT ON COLUMN core.tax_authorities.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.tax_authorities.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.tax_base_amount_types IS 'Information on various tax base that are applicable.';
COMMENT ON COLUMN core.tax_base_amount_types.tax_base_amount_type_code IS 'The code of the tax base.';
COMMENT ON COLUMN core.tax_base_amount_types.tax_base_amount_type_name IS 'The name given to the tax base, which is also a unique field.';


COMMENT ON TABLE core.tax_exempt_types IS 'Various kinds of tax exemptions that are applicable.';
COMMENT ON COLUMN core.tax_exempt_types.tax_exempt_type_id IS 'The primary key of the table, which is also a serial field.';
COMMENT ON COLUMN core.tax_exempt_types.tax_exempt_type_code IS 'The code given to the tax exemption type.';
COMMENT ON COLUMN core.tax_exempt_types.tax_exempt_type_name IS 'The name given to the tax exemption type, which is also a unique field.';
COMMENT ON COLUMN core.tax_exempt_types.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.tax_exempt_types.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.tax_master IS 'Information on the applicable tax rates of various countries (tax master)supported by the software. ';
COMMENT ON COLUMN core.tax_master.tax_master_id IS 'The primary key of the table, which is also a serial field.';
COMMENT ON COLUMN core.tax_master.tax_master_code IS 'The code of the code master.';
COMMENT ON COLUMN core.tax_master.tax_master_name IS 'The name of the tax master.';
COMMENT ON COLUMN core.tax_master.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.tax_master.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.tax_rate_types IS 'The code and the name given to various types tax rates (flat rate or percentage basis).';
COMMENT ON COLUMN core.tax_rate_types.tax_rate_type_code IS 'The primary key of the table.';
COMMENT ON COLUMN core.tax_rate_types.tax_rate_type_name IS 'The name given to the tax-rate type, which is also a unique field.';


COMMENT ON TABLE transactions.transaction_details IS '';
COMMENT ON COLUMN transactions.transaction_details.transaction_detail_id IS '';
COMMENT ON COLUMN transactions.transaction_details.transaction_master_id IS '';
COMMENT ON COLUMN transactions.transaction_details.value_date IS '';
COMMENT ON COLUMN transactions.transaction_details.tran_type IS '';
COMMENT ON COLUMN transactions.transaction_details.account_id IS 'Foreign key to the table core.accounts.';
COMMENT ON COLUMN transactions.transaction_details.statement_reference IS '';
COMMENT ON COLUMN transactions.transaction_details.cash_repository_id IS '';
COMMENT ON COLUMN transactions.transaction_details.currency_code IS 'Foreign key to the table core.currencies.';
COMMENT ON COLUMN transactions.transaction_details.amount_in_currency IS '';
COMMENT ON COLUMN transactions.transaction_details.local_currency_code IS '';
COMMENT ON COLUMN transactions.transaction_details.er IS '';
COMMENT ON COLUMN transactions.transaction_details.amount_in_local_currency IS '';
COMMENT ON COLUMN transactions.transaction_details.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN transactions.transaction_details.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE transactions.transaction_master IS '';
COMMENT ON COLUMN transactions.transaction_master.transaction_master_id IS '';
COMMENT ON COLUMN transactions.transaction_master.transaction_counter IS '';
COMMENT ON COLUMN transactions.transaction_master.transaction_code IS '';
COMMENT ON COLUMN transactions.transaction_master.book IS '';
COMMENT ON COLUMN transactions.transaction_master.value_date IS '';
COMMENT ON COLUMN transactions.transaction_master.transaction_ts IS '';
COMMENT ON COLUMN transactions.transaction_master.login_id IS '';
COMMENT ON COLUMN transactions.transaction_master.user_id IS '';
COMMENT ON COLUMN transactions.transaction_master.sys_user_id IS '';
COMMENT ON COLUMN transactions.transaction_master.office_id IS '';
COMMENT ON COLUMN transactions.transaction_master.cost_center_id IS '';
COMMENT ON COLUMN transactions.transaction_master.reference_number IS '';
COMMENT ON COLUMN transactions.transaction_master.statement_reference IS '';
COMMENT ON COLUMN transactions.transaction_master.last_verified_on IS '';
COMMENT ON COLUMN transactions.transaction_master.verified_by_user_id IS '';
COMMENT ON COLUMN transactions.transaction_master.verification_status_id IS '';
COMMENT ON COLUMN transactions.transaction_master.verification_reason IS '';
COMMENT ON COLUMN transactions.transaction_master.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN transactions.transaction_master.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.units IS 'Contains units of measure.';
COMMENT ON COLUMN core.units.unit_id IS 'The primary key of this table, which is also a serial column.';
COMMENT ON COLUMN core.units.unit_code IS 'The case insensitive unique code which denotes the unit name.';
COMMENT ON COLUMN core.units.unit_name IS 'The case insensitive unique column which denotes the unit of measure.';
COMMENT ON COLUMN core.units.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.units.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE office.users IS 'The users table contains users accounts and their login information. It also contains a sys user account which does not have a password.
The sys user account is a special account used by the MixERP workflow to perform routine tasks. The sys user cannot have a valid password
or cannot be allowed to log in interactively.';
COMMENT ON COLUMN office.users.user_id IS 'The primary key of the table, which is also a serial field.';
COMMENT ON COLUMN office.users.role_id IS '';
COMMENT ON COLUMN office.users.office_id IS '';
COMMENT ON COLUMN office.users.user_name IS '';
COMMENT ON COLUMN office.users.full_name IS '';
COMMENT ON COLUMN office.users.password IS '';
COMMENT ON COLUMN office.users.elevated IS '';
COMMENT ON COLUMN office.users.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN office.users.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.verification_statuses IS 'Verification statuses are integer values used to represent the state of a transaction.
For example, a verification status of value "0" would mean that the transaction has not yet been verified.
A negative value indicates that the transaction was rejected, whereas a positive value means approved.

Remember:
1. Only approved transactions appear on ledgers and final reports.
2. Cash repository balance is maintained on the basis of LIFO principle. 

   This means that cash balance is affected (reduced) on your repository as soon as a credit transaction is posted,
   without the transaction being approved on the first place. If you reject the transaction, the cash balance then increases.
   This also means that the cash balance is not affected (increased) on your repository as soon as a debit transaction is posted.
   You will need to approve the transaction.

   It should however be noted that the cash repository balance might be less than the total cash shown on your balance sheet,
   if you have pending transactions to verify. You cannot perform EOD operation if you have pending verifications.';
COMMENT ON COLUMN core.verification_statuses.verification_status_id IS 'The primary key of this table.';
COMMENT ON COLUMN core.verification_statuses.verification_status_name IS 'The name of verification status, which is a unique field.';


COMMENT ON TABLE policy.voucher_verification_policy IS '';
COMMENT ON COLUMN policy.voucher_verification_policy.user_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN policy.voucher_verification_policy.can_verify_sales_transactions IS '';
COMMENT ON COLUMN policy.voucher_verification_policy.sales_verification_limit IS '';
COMMENT ON COLUMN policy.voucher_verification_policy.can_verify_purchase_transactions IS '';
COMMENT ON COLUMN policy.voucher_verification_policy.purchase_verification_limit IS '';
COMMENT ON COLUMN policy.voucher_verification_policy.can_verify_gl_transactions IS '';
COMMENT ON COLUMN policy.voucher_verification_policy.gl_verification_limit IS '';
COMMENT ON COLUMN policy.voucher_verification_policy.can_self_verify IS '';
COMMENT ON COLUMN policy.voucher_verification_policy.self_verification_limit IS '';
COMMENT ON COLUMN policy.voucher_verification_policy.effective_from IS '';
COMMENT ON COLUMN policy.voucher_verification_policy.ends_on IS '';
COMMENT ON COLUMN policy.voucher_verification_policy.is_active IS '';
COMMENT ON COLUMN policy.voucher_verification_policy.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN policy.voucher_verification_policy.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE office.work_centers IS '';
COMMENT ON COLUMN office.work_centers.work_center_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN office.work_centers.office_id IS '';
COMMENT ON COLUMN office.work_centers.work_center_code IS '';
COMMENT ON COLUMN office.work_centers.work_center_name IS '';
COMMENT ON COLUMN office.work_centers.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN office.work_centers.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.zip_code_types IS 'Types of ZIP code.';
COMMENT ON COLUMN core.zip_code_types.zip_code_type_id IS 'The primary key of this table, which is also a serial field.';
COMMENT ON COLUMN core.zip_code_types.type IS 'The type of ZIP code, which is also a unique field.';
COMMENT ON COLUMN core.zip_code_types.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.zip_code_types.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


COMMENT ON TABLE core.zip_codes IS 'This table stores information on the ZIP code.';
COMMENT ON COLUMN core.zip_codes.zip_code_id IS 'The primary key of this table, which is also a bigserial field.';
COMMENT ON COLUMN core.zip_codes.state_id IS 'Foreign key to the table core.states';
COMMENT ON COLUMN core.zip_codes.code IS 'ZIP code of the area.';
COMMENT ON COLUMN core.zip_codes.zip_code_type_id IS 'Foreign key to the table core.zip_code_types.';
COMMENT ON COLUMN core.zip_codes.city IS 'Name of the city,';
COMMENT ON COLUMN core.zip_codes.lat IS 'Latitude of the area.';
COMMENT ON COLUMN core.zip_codes.lon IS 'Longitude of the area.';
COMMENT ON COLUMN core.zip_codes.x_axis IS 'X-axis of the ZIP code.';
COMMENT ON COLUMN core.zip_codes.y_axis IS 'Y-axis of the ZIP code. ';
COMMENT ON COLUMN core.zip_codes.z_axis IS 'Z-axis of the ZIP code.';
COMMENT ON COLUMN core.zip_codes.audit_user_id IS 'Contains the id of the user who last inserted or updated the corresponding row.';
COMMENT ON COLUMN core.zip_codes.audit_ts IS 'Contains the date and timestamp of the last insert or update action.';


