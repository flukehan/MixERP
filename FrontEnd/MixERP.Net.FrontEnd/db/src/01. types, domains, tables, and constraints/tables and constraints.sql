--Todo: Indexing has not been properly thought of, as of now.

CREATE TABLE core.verification_statuses
(
    verification_status_id                  smallint NOT NULL PRIMARY KEY,
    verification_status_name                national character varying(128) NOT NULL
);

COMMENT ON TABLE core.verification_statuses IS 
'Verification statuses are integer values used to represent the state of a transaction.
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
   if you have pending transactions to verify. You cannot perform EOD operation if you have pending verifications.
';

CREATE UNIQUE INDEX verification_statuses_verification_status_name_uix
ON core.verification_statuses(UPPER(verification_status_name));



CREATE TABLE office.users
(
    user_id                                 SERIAL NOT NULL PRIMARY KEY,
    role_id                                 smallint NOT NULL,
    office_id                               integer NOT NULL,
    user_name                               national character varying(50) NOT NULL,
    full_name                               national character varying(100) NOT NULL,
    password                                text NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL 
                                            DEFAULT(NOW())
);

COMMENT ON TABLE office.users IS
'
The users table contains users accounts and their login information. It also contains a sys user account which does not have a password.
The sys user account is a special account used by the MixERP workflow to perform routine tasks. The sys user cannot have a valid password
or cannot be allowed to log in interactively.';

CREATE TABLE office.departments
(
    department_id SERIAL                    NOT NULL PRIMARY KEY,
    department_code                         national character varying(12) NOT NULL,
    department_name                         national character varying(50) NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL 
                                            DEFAULT(NOW())
);


CREATE TABLE core.flag_types
(
    flag_type_id                            SERIAL NOT NULL PRIMARY KEY,
    flag_type_name                          national character varying(24) NOT NULL,
    background_color                        color NOT NULL,
    foreground_color                        color NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL
                                            DEFAULT(NOW())
);

COMMENT ON TABLE core.flag_types IS 'Flags are used by users to mark transactions. The flags created by a user is not visible to others.';

CREATE TABLE core.flags
(
    flag_id                                 BIGSERIAL NOT NULL PRIMARY KEY,
    user_id                                 integer NOT NULL REFERENCES office.users(user_id),
    flag_type_id                            integer NOT NULL REFERENCES core.flag_types(flag_type_id),
    resource                                text, --Fully qualified resource name. Example: transactions.non_gl_stock_master.
    resource_key                            text, --The unique idenfier for lookup. Example: non_gl_stock_master_id,
    resource_id                             integer, --The value of the unique identifier to lookup for,
    flagged_on                              TIMESTAMP WITH TIME ZONE NULL 
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX flags_user_id_resource_resource_id_uix
ON core.flags(user_id, UPPER(resource), UPPER(resource_key), resource_id);

CREATE TABLE core.attachment_lookup
(
        attachment_lookup_id                SERIAL NOT NULL PRIMARY KEY,
        book                                national character varying(50) NOT NULL,
        resource                            text NOT NULL,
        resource_key                        text NOT NULL        
);

CREATE UNIQUE INDEX attachment_lookup_book_uix
ON core.attachment_lookup(lower(book));

CREATE UNIQUE INDEX attachment_lookup_resource_resource_key_uix
ON core.attachment_lookup(lower(book), lower(resource_key));


CREATE TABLE core.attachments
(
    attachment_id                           BIGSERIAL NOT NULL PRIMARY KEY,
    user_id                                 integer NOT NULL 
                                            REFERENCES office.users(user_id),
    resource                                text, --Fully qualified resource name. Example: transactions.non_gl_stock_master.
    resource_key                            text, --The unique idenfier for lookup. Example: non_gl_stock_master_id,
    resource_id                             integer, --The value of the unique identifier to lookup for,
    original_file_name                      text NOT NULL,
    file_extension                          national character varying(12) NOT NULL,
    file_path                               text NOT NULL,
    comment                                 national character varying(96) NOT NULL  
                                            CONSTRAINT attachments_comment_df 
                                            DEFAULT(''),
    added_on                                TIMESTAMP WITH TIME ZONE NOT NULL  
                                            CONSTRAINT attachments_added_on_df 
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX attachments_file_path_uix
ON core.attachments(UPPER(file_path));


CREATE TABLE core.currencies
(
    currency_code                           national character varying(12) NOT NULL PRIMARY KEY,
    currency_symbol                         national character varying(12) NOT NULL,
    currency_name                           national character varying(48) NOT NULL UNIQUE,
    hundredth_name                          national character varying(48) NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL 
                                            DEFAULT(NOW())
);



CREATE TABLE office.offices
(
    office_id                               SERIAL NOT NULL PRIMARY KEY,
    office_code                             national character varying(12) NOT NULL,
    office_name                             national character varying(150) NOT NULL,
    nick_name                               national character varying(50) NULL,
    registration_date                       date NOT NULL,
    currency_code                           national character varying(12) NOT NULL REFERENCES core.currencies(currency_code),
    po_box                                  national character varying(128) NULL,
    address_line_1                          national character varying(128) NULL,   
    address_line_2                          national character varying(128) NULL,
    street                                  national character varying(50) NULL,
    city                                    national character varying(50) NULL,
    state                                   national character varying(50) NULL,
    zip_code                                national character varying(24) NULL,
    country                                 national character varying(50) NULL,
    phone                                   national character varying(24) NULL,
    fax                                     national character varying(24) NULL,
    email                                   national character varying(128) NULL,
    url                                     national character varying(50) NULL,
    registration_number                     national character varying(24) NULL,
    pan_number                              national character varying(24) NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL 
                                            DEFAULT(NOW()),
    parent_office_id                        integer NULL REFERENCES office.offices(office_id)
);

ALTER TABLE office.users
ADD FOREIGN KEY(office_id) REFERENCES office.offices(office_id);

CREATE UNIQUE INDEX offices_office_code_uix
ON office.offices(UPPER(office_code));

CREATE UNIQUE INDEX offices_office_name_uix
ON office.offices(UPPER(office_name));

CREATE UNIQUE INDEX offices_nick_name_uix
ON office.offices(UPPER(nick_name));






CREATE TABLE core.exchange_rates
(
    exchange_rate_id                        BIGSERIAL NOT NULL PRIMARY KEY,
    updated_on                              TIMESTAMP WITH TIME ZONE NOT NULL   
                                            CONSTRAINT exchange_rates_updated_on_df 
                                            DEFAULT(NOW()),
    office_id                               integer NOT NULL REFERENCES office.offices(office_id),
    status                                  BOOLEAN NOT NULL   
                                            CONSTRAINT exchange_rates_status_df 
                                            DEFAULT(true)
);

CREATE TABLE core.exchange_rate_details
(
    exchange_rate_detail_id                 BIGSERIAL NOT NULL PRIMARY KEY,
    exchange_rate_id                        bigint NOT NULL REFERENCES core.exchange_rates(exchange_rate_id),
    local_currency_code                     national character varying(12) NOT NULL REFERENCES core.currencies(currency_code),
    foreign_currency_code                   national character varying(12) NOT NULL REFERENCES core.currencies(currency_code),
    unit                                    integer_strict NOT NULL,
    exchange_rate                           decimal_strict NOT NULL
);


CREATE UNIQUE INDEX departments_department_code_uix
ON office.departments(UPPER(department_code));

CREATE UNIQUE INDEX departments_department_name_uix
ON office.departments(UPPER(department_name));


CREATE TABLE office.roles
(
    role_id SERIAL                          NOT NULL PRIMARY KEY,
    role_code                               national character varying(12) NOT NULL,
    role_name                               national character varying(50) NOT NULL,
    is_admin                                boolean NOT NULL   
                                            CONSTRAINT roles_is_admin_df 
                                            DEFAULT(false),
    is_system                               boolean NOT NULL   
                                            CONSTRAINT roles_is_system_df 
                                            DEFAULT(false),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL 
                                            DEFAULT(NOW())
);

ALTER TABLE office.users
ADD FOREIGN KEY(role_id) REFERENCES office.roles(role_id);


CREATE UNIQUE INDEX roles_role_code_uix
ON office.roles(UPPER(role_code));

CREATE UNIQUE INDEX roles_role_name_uix
ON office.roles(UPPER(role_name));


CREATE UNIQUE INDEX users_user_name_uix
ON office.users(UPPER(user_name));


CREATE TABLE audit.logins
(
    login_id                                BIGSERIAL NOT NULL PRIMARY KEY,
    user_id                                 integer NOT NULL REFERENCES office.users(user_id),
    office_id                               integer NOT NULL REFERENCES office.offices(office_id),
    browser                                 national character varying(500) NOT NULL,
    ip_address                              national character varying(50) NOT NULL,
    login_date_time                         TIMESTAMP WITH TIME ZONE NOT NULL 
                                            DEFAULT(NOW()),
    remote_user                             national character varying(50) NOT NULL,
    culture                                 national character varying(12) NOT NULL
);


CREATE TABLE audit.failed_logins
(
    failed_login_id                         BIGSERIAL NOT NULL PRIMARY KEY,
    user_id                                 integer NULL REFERENCES office.users(user_id),
    user_name                               national character varying(50) NOT NULL,
    office_id                               integer NULL REFERENCES office.offices(office_id),
    browser                                 national character varying(500) NOT NULL,
    ip_address                              national character varying(50) NOT NULL,
    failed_date_time                        TIMESTAMP WITH TIME ZONE NOT NULL 
                                            DEFAULT(NOW()),
    remote_user                             national character varying(50) NOT NULL,
    details                                 national character varying(250) NULL
);


CREATE TABLE policy.lock_outs
(
    lock_out_id                             BIGSERIAL NOT NULL PRIMARY KEY,
    user_id                                 integer NOT NULL REFERENCES office.users(user_id),
    lock_out_time                           TIMESTAMP WITH TIME ZONE NOT NULL 
                                            DEFAULT(NOW()),
    lock_out_till                           TIMESTAMP WITH TIME ZONE NOT NULL 
                                            DEFAULT(NOW() + '5 minutes'::interval)
);


CREATE TABLE core.price_types
(
    price_type_id                           SERIAL  NOT NULL PRIMARY KEY,
    price_type_code                         national character varying(12) NOT NULL,
    price_type_name                         national character varying(50) NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL  
                                            DEFAULT(NOW())
);


CREATE UNIQUE INDEX price_types_price_type_code_uix
ON core.price_types(UPPER(price_type_code));

CREATE UNIQUE INDEX price_types_price_type_name_uix
ON core.price_types(UPPER(price_type_name));


CREATE TABLE core.menus
(
    menu_id                                 SERIAL NOT NULL PRIMARY KEY,
    menu_text                               national character varying(250) NOT NULL,
    url                                     national character varying(250) NULL,
    menu_code                               national character varying(12) NOT NULL,
    level                                   smallint NOT NULL,
    parent_menu_id                          integer NULL REFERENCES core.menus(menu_id),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL  
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX menus_menu_code_uix
ON core.menus(UPPER(menu_code));

CREATE TABLE core.menu_locale
(
    menu_locale_id                          SERIAL NOT NULL PRIMARY KEY,
    menu_id                                 integer NOT NULL REFERENCES core.menus(menu_id),
    culture                                 national character varying(12) NOT NULL,
    menu_text                               national character varying(250) NOT NULL
);

CREATE UNIQUE INDEX menu_locale_menu_id_culture_uix
ON core.menu_locale(menu_id, LOWER(culture));

CREATE TABLE policy.menu_policy
(
    policy_id                               SERIAL NOT NULL PRIMARY KEY,
    menu_id                                 integer NOT NULL REFERENCES core.menus(menu_id),
    office_id                               integer NULL REFERENCES office.offices(office_id),
    inherit_in_child_offices                boolean NOT NULL  
                                            DEFAULT(false),
    role_id                                 integer NULL REFERENCES office.roles(role_id),
    user_id                                 integer NULL REFERENCES office.users(user_id),
    scope                                   national character varying(12) NOT NULL
                                            CONSTRAINT menu_policy_scope_chk
                                            CHECK(scope IN('Allow','Deny'))
    
);

CREATE TABLE policy.menu_access
(
    access_id                               BIGSERIAL NOT NULL PRIMARY KEY,
    office_id                               integer NOT NULL REFERENCES office.offices(office_id),
    menu_id                                 integer NOT NULL REFERENCES core.menus(menu_id),
    user_id                                 integer NULL REFERENCES office.users(user_id)   
);

    
CREATE TABLE core.frequencies
(
    frequency_id                            SERIAL NOT NULL PRIMARY KEY,
    frequency_code                          national character varying(12) NOT NULL,
    frequency_name                          national character varying(50) NOT NULL
);


CREATE UNIQUE INDEX frequencies_frequency_code_uix
ON core.frequencies(UPPER(frequency_code));

CREATE UNIQUE INDEX frequencies_frequency_name_uix
ON core.frequencies(UPPER(frequency_name));


CREATE TABLE core.fiscal_year
(
    fiscal_year_code                        national character varying(12) NOT NULL PRIMARY KEY,
    fiscal_year_name                        national character varying(50) NOT NULL,
    starts_from                             date NOT NULL,
    ends_on                                 date NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL  
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX fiscal_year_fiscal_year_name_uix
ON core.fiscal_year(UPPER(fiscal_year_name));

CREATE UNIQUE INDEX fiscal_year_starts_from_uix
ON core.fiscal_year(starts_from);

CREATE UNIQUE INDEX fiscal_year_ends_on_uix
ON core.fiscal_year(ends_on);


CREATE TABLE core.frequency_setups
(
    frequency_setup_id                      SERIAL NOT NULL PRIMARY KEY,
    fiscal_year_code                        national character varying(12) NOT NULL REFERENCES core.fiscal_year(fiscal_year_code),
    value_date                              date NOT NULL UNIQUE,
    frequency_id                            integer NOT NULL REFERENCES core.frequencies(frequency_id),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL  
                                            DEFAULT(NOW())
);

--TODO: Validation constraints for core.frequency_setups

CREATE TABLE core.units
(
    unit_id                                 SERIAL NOT NULL PRIMARY KEY,
    unit_code                               national character varying(12) NOT NULL,
    unit_name                               national character varying(50) NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL  
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX units_unit_code_uix
ON core.units(UPPER(unit_code));

CREATE UNIQUE INDEX units_unit_name_uix
ON core.units(UPPER(unit_name));


CREATE TABLE core.compound_units
(
    compound_unit_id                        SERIAL NOT NULL PRIMARY KEY,
    base_unit_id                            integer NOT NULL REFERENCES core.units(unit_id),
    value                                   smallint NOT NULL,
    compare_unit_id                         integer NOT NULL REFERENCES core.units(unit_id),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL  
                                            DEFAULT(NOW()),
                                            CONSTRAINT compound_units_chk
                                            CHECK(base_unit_id != compare_unit_id)
);

CREATE UNIQUE INDEX compound_units_info_uix
ON core.compound_units(base_unit_id, compare_unit_id);

CREATE TABLE core.account_masters
(
    account_master_id                       SERIAL NOT NULL PRIMARY KEY,
    account_master_code                     national character varying(3) NOT NULL,
    account_master_name                     national character varying(40) NOT NULL 
);

CREATE UNIQUE INDEX account_master_code_uix
ON core.account_masters(UPPER(account_master_code));

CREATE UNIQUE INDEX account_master_name_uix
ON core.account_masters(UPPER(account_master_name));



CREATE TABLE core.accounts
(
    account_id                              BIGSERIAL NOT NULL PRIMARY KEY,
    account_master_id                       integer NOT NULL REFERENCES core.account_masters(account_master_id),
    account_code                            national character varying(12) NOT NULL,
    external_code                           national character varying(12) NULL   
                                            CONSTRAINT accounts_external_code_df  
                                            DEFAULT(''),
    confidential                            boolean NOT NULL   
                                            CONSTRAINT accounts_confidential_df  
                                            DEFAULT(false),
    currency_code                           national character varying(12) NOT NULL REFERENCES core.currencies(currency_code),
    account_name                            national character varying(100) NOT NULL,
    description                             national character varying(200) NULL,
    sys_type                                boolean NOT NULL   
                                            CONSTRAINT accounts_sys_type_df  
                                            DEFAULT(false),
    is_cash                                 boolean NOT NULL   
                                            CONSTRAINT accounts_is_cash_df   
                                            DEFAULT(false),
    parent_account_id                       bigint NULL REFERENCES core.accounts(account_id),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL 
                                            DEFAULT(NOW())
);


CREATE UNIQUE INDEX accountsCode_uix
ON core.accounts(UPPER(account_code));

CREATE UNIQUE INDEX accounts_Name_uix
ON core.accounts(UPPER(account_name));


CREATE TABLE core.account_parameters
(
    account_parameter_id                    SERIAL NOT NULL PRIMARY KEY,
    parameter_name                          national character varying(128) NOT NULL,
    account_id                              bigint NOT NULL REFERENCES core.accounts(account_id),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX account_parameters_parameter_name_uix
ON core.account_parameters(UPPER(parameter_name));

CREATE TABLE core.bank_accounts
(
    account_id                              bigint NOT NULL PRIMARY KEY REFERENCES core.accounts(account_id),
    maintained_by_user_id                   integer NOT NULL REFERENCES office.users(user_id),
    bank_name                               national character varying(128) NOT NULL,
    bank_branch                             national character varying(128) NOT NULL,
    bank_contact_number                     national character varying(128) NULL,
    bank_address                            text NULL,
    bank_account_code                       national character varying(128) NULL,
    bank_account_type                       national character varying(128) NULL,
    relationship_officer_name               national character varying(128) NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);


CREATE TABLE core.sales_teams
(
    sales_team_id                           SERIAL NOT NULL PRIMARY KEY,
    sales_team_code                         national character varying(12),
    sales_team_name                         national character varying(50),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX sales_teams_sales_team_code_uix
ON core.sales_teams(UPPER(sales_team_code));

CREATE UNIQUE INDEX sales_teams_sales_team_name_uix
ON core.sales_teams(UPPER(sales_team_name));

CREATE TABLE core.salespersons
(
    salesperson_id                          SERIAL NOT NULL PRIMARY KEY,
    sales_team_id                           integer NOT NULL REFERENCES core.sales_teams(sales_team_id),
    salesperson_code                        national character varying(12) NOT NULL,
    salesperson_name                        national character varying(100) NOT NULL,
    address                                 national character varying(100) NOT NULL,
    contact_number                          national character varying(50) NOT NULL,
    commission_rate                         decimal_strict2 NOT NULL   
                                            DEFAULT(0),
    account_id                              bigint NOT NULL REFERENCES core.accounts(account_id),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX salespersons_salesperson_name_uix
ON core.salespersons(UPPER(salesperson_name));

CREATE TABLE core.bonus_slabs
(
    bonus_slab_id                           SERIAL NOT NULL PRIMARY KEY,
    bonus_slab_code                         national character varying(12) NOT NULL,
    bonus_slab_name                         national character varying(50) NOT NULL,
    checking_frequency_id                   integer NOT NULL REFERENCES core.frequencies(frequency_id),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX bonus_slabs_bonus_slab_code_uix
ON core.bonus_slabs(UPPER(bonus_slab_code));


CREATE UNIQUE INDEX bonus_slabs_bonus_slab_name_uix
ON core.bonus_slabs(UPPER(bonus_slab_name));



CREATE TABLE core.bonus_slab_details
(
    bonus_slab_detail_id                    SERIAL NOT NULL PRIMARY KEY,
    bonus_slab_id                           integer NOT NULL REFERENCES core.bonus_slabs(bonus_slab_id),
    amount_from                             money_strict NOT NULL,
    amount_to                               money_strict NOT NULL,
    bonus_rate                              decimal_strict NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW()),
                                            CONSTRAINT bonus_slab_details_amounts_chk 
                                            CHECK(amount_to>amount_from)
);


CREATE TABLE core.salesperson_bonus_setups
(
    salesperson_bonus_setup_id SERIAL       NOT NULL PRIMARY KEY,
    salesperson_id                          integer NOT NULL REFERENCES core.salespersons(salesperson_id),
    bonus_slab_id                           integer NOT NULL REFERENCES core.bonus_slabs(bonus_slab_id),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX salesperson_bonus_setups_uix
ON core.salesperson_bonus_setups(salesperson_id, bonus_slab_id);

CREATE TABLE core.ageing_slabs
(
    ageing_slab_id SERIAL NOT NULL PRIMARY KEY,
    ageing_slab_name national character varying(24) NOT NULL,
    from_days integer NOT NULL,
    to_days integer NOT NULL CHECK(to_days > 0)
);

CREATE UNIQUE INDEX ageing_slabs_ageing_slab_name_uix
ON core.ageing_slabs(UPPER(ageing_slab_name));


CREATE TABLE core.party_types
(
    party_type_id                           SERIAL NOT NULL PRIMARY KEY,
    party_type_code                         national character varying(12) NOT NULL, 
    party_type_name                         national character varying(50) NOT NULL,
    is_supplier                             boolean NOT NULL   
                                            CONSTRAINT party_types_is_supplier_df   
                                            DEFAULT(false),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL 
                                              
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX party_types_party_type_code_uix
ON core.party_types(UPPER(party_type_code));

CREATE UNIQUE INDEX party_types_party_type_name_uix
ON core.party_types(UPPER(party_type_name));


CREATE TABLE core.parties
(
    party_id BIGSERIAL                      NOT NULL PRIMARY KEY,
    party_type_id                           smallint NOT NULL REFERENCES core.party_types(party_type_id),
    party_code                              national character varying(12) NULL,
    first_name                              national character varying(50) NOT NULL,
    middle_name                             national character varying(50) NULL,
    last_name                               national character varying(50) NOT NULL,
    party_name                              text NULL,
    date_of_birth                           date NULL,
    po_box                                  national character varying(128) NULL,
    address_line_1                          national character varying(128) NULL,   
    address_line_2                          national character varying(128) NULL,
    street                                  national character varying(50) NULL,
    city                                    national character varying(50) NULL,
    state                                   national character varying(50) NULL,
    country                                 national character varying(50) NULL,
    phone                                   national character varying(24) NULL,
    fax                                     national character varying(24) NULL,
    cell                                    national character varying(24) NULL,
    email                                   national character varying(128) NULL,
    url                                     national character varying(50) NULL,
    pan_number                              national character varying(50) NULL,
    sst_number                              national character varying(50) NULL,
    cst_number                              national character varying(50) NULL,
    currency_code                           national character varying(12) NOT NULL REFERENCES core.currencies(currency_code),
    allow_credit                            boolean NULL,
    maximum_credit_period                   smallint NULL,
    maximum_credit_amount                   money_strict2 NULL,
    charge_interest                         boolean NULL,
    interest_rate                           decimal NULL,
    interest_compounding_frequency_id       smallint NULL REFERENCES core.frequencies(frequency_id),
    account_id                              bigint NULL REFERENCES core.accounts(account_id),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);


CREATE UNIQUE INDEX parties_party_code_uix
ON core.parties(UPPER(party_code));

CREATE TABLE core.shipping_addresses
(
    shipping_address_id                     BIGSERIAL NOT NULL PRIMARY KEY,
    shipping_address_code                   national character varying(24) NOT NULL,
    party_id                                bigint NOT NULL REFERENCES core.parties(party_id),
    po_box                                  national character varying(128) NULL,
    address_line_1                          national character varying(128) NULL,   
    address_line_2                          national character varying(128) NULL,
    street                                  national character varying(128) NULL,
    city                                    national character varying(128) NOT NULL,
    state                                   national character varying(128) NOT NULL,
    country                                 national character varying(128) NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX shipping_addresses_shipping_address_code_uix
ON core.shipping_addresses(UPPER(shipping_address_code), party_id);



CREATE TABLE core.brands
(
    brand_id SERIAL                         NOT NULL PRIMARY KEY,
    brand_code                              national character varying(12) NOT NULL,
    brand_name                              national character varying(150) NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX brands_brand_code_uix
ON core.brands(UPPER(brand_code));

CREATE UNIQUE INDEX brands_brand_name_uix
ON core.brands(UPPER(brand_name));



CREATE TABLE core.shippers
(
    shipper_id                              BIGSERIAL NOT NULL PRIMARY KEY,
    shipper_code                            national character varying(12) NULL,
    company_name                            national character varying(128) NOT NULL,
    shipper_name                            national character varying(150) NULL,
    po_box                                  national character varying(128) NULL,
    address_line_1                          national character varying(128) NULL,   
    address_line_2                          national character varying(128) NULL,
    street                                  national character varying(50) NULL,
    city                                    national character varying(50) NULL,
    state                                   national character varying(50) NULL,
    country                                 national character varying(50) NULL,
    phone                                   national character varying(50) NULL,
    fax                                     national character varying(50) NULL,
    cell                                    national character varying(50) NULL,
    email                                   national character varying(128) NULL,
    url                                     national character varying(50) NULL,
    contact_person                          national character varying(50) NULL,
    contact_po_box                          national character varying(128) NULL,
    contact_address_line_1                  national character varying(128) NULL,   
    contact_address_line_2                  national character varying(128) NULL,
    contact_street                          national character varying(50) NULL,
    contact_city                            national character varying(50) NULL,
    contact_state                           national character varying(50) NULL,
    contact_country                         national character varying(50) NULL,
    contact_email                           national character varying(128) NULL,
    contact_phone                           national character varying(50) NULL,
    contact_cell                            national character varying(50) NULL,
    factory_address                         national character varying(250) NULL,
    pan_number                              national character varying(50) NULL,
    sst_number                              national character varying(50) NULL,
    cst_number                              national character varying(50) NULL,
    account_id                              bigint NOT NULL REFERENCES core.accounts(account_id),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL 
                                              
                                            DEFAULT(NOW())
);


CREATE UNIQUE INDEX shippers_shipper_code_uix
ON core.shippers(UPPER(shipper_code));


CREATE TABLE core.tax_types
(
    tax_type_id                             SERIAL  NOT NULL PRIMARY KEY,
    tax_type_code                           national character varying(12) NOT NULL,
    tax_type_name                           national character varying(50) NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX tax_types_tax_type_code_uix
ON core.tax_types(UPPER(tax_type_code));

CREATE UNIQUE INDEX tax_types_tax_type_name_uix
ON core.tax_types(UPPER(tax_type_name));


CREATE TABLE core.taxes
(
    tax_id SERIAL                           NOT NULL PRIMARY KEY,
    tax_type_id                             smallint NOT NULL REFERENCES core.tax_types(tax_type_id),
    tax_code                                national character varying(12) NOT NULL,
    tax_name                                national character varying(50) NOT NULL,
    rate                                    decimal NOT NULL,
    account_id                              bigint NOT NULL REFERENCES core.accounts(account_id),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);



CREATE UNIQUE INDEX taxes_tax_code_uix
ON core.taxes(UPPER(tax_code));

CREATE UNIQUE INDEX taxes_tax_name_uix
ON core.taxes(UPPER(tax_name));


CREATE TABLE core.item_types
(
    item_type_id                            SERIAL NOT NULL PRIMARY KEY,
    item_type_code                          national character varying(12) NOT NULL,
    item_type_name                          national character varying(50) NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX item_type_item_type_code_uix
ON core.item_types(UPPER(item_type_code));


CREATE UNIQUE INDEX item_type_item_type_name_uix
ON core.item_types(UPPER(item_type_name));

CREATE TABLE core.item_groups
(
    item_group_id                           SERIAL NOT NULL PRIMARY KEY,
    item_group_code                         national character varying(12) NOT NULL,
    item_group_name                         national character varying(50) NOT NULL,
    exclude_from_purchase                   boolean NOT NULL   
                                            CONSTRAINT item_groups_exclude_from_purchase_df   
                                            DEFAULT(false),
    exclude_from_sales                      boolean NOT NULL   
                                            CONSTRAINT item_groups_exclude_from_sales_df   
                                            DEFAULT(false),
    tax_id                                  smallint NOT NULL REFERENCES core.taxes(tax_id),
    parent_item_group_id                    integer NULL REFERENCES core.item_groups(item_group_id),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);


CREATE UNIQUE INDEX item_groups_item_group_code_uix
ON core.item_groups(UPPER(item_group_code));

CREATE UNIQUE INDEX item_groups_item_group_name_uix
ON core.item_groups(UPPER(item_group_name));



CREATE TABLE core.shipping_mail_types
(
    shipping_mail_type_id                   SERIAL NOT NULL PRIMARY KEY,
    shipping_mail_type_code                 national character varying(12) NOT NULL,
    shipping_mail_type_name                 national character varying(64) NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX shipping_mail_types_shipping_mail_type_code_uix
ON core.shipping_mail_types(UPPER(shipping_mail_type_code));

CREATE UNIQUE INDEX shipping_mail_types_shipping_mail_type_name_uix
ON core.shipping_mail_types(UPPER(shipping_mail_type_name));






CREATE TABLE core.shipping_package_shapes
(
    shipping_package_shape_id               SERIAL NOT NULL PRIMARY KEY,
    shipping_package_shape_code             national character varying(12) NOT NULL,
    shipping_package_shape_name             national character varying(64) NOT NULL,
    is_rectangular                          boolean NOT NULL   
                                            DEFAULT(false),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())    
);



CREATE UNIQUE INDEX shipping_package_shapes_shipping_package_shape_code_uix
ON core.shipping_package_shapes(UPPER(shipping_package_shape_code));

CREATE UNIQUE INDEX shipping_package_shapes_shipping_package_shape_name_uix
ON core.shipping_package_shapes(UPPER(shipping_package_shape_name));


CREATE TABLE core.items
(
    item_id                                 SERIAL NOT NULL PRIMARY KEY,
    item_code                               national character varying(12) NOT NULL,
    item_name                               national character varying(150) NOT NULL,
    item_group_id                           integer NOT NULL REFERENCES core.item_groups(item_group_id),
    brand_id                                integer NOT NULL REFERENCES core.brands(brand_id),
    preferred_supplier_id                   bigint NOT NULL REFERENCES core.parties(party_id),
    lead_time_in_days                       integer NOT NULL   
                                            DEFAULT(0),
    weight_in_grams                         float NOT NULL   
                                            DEFAULT(0),  
    width_in_centimeters                    float NOT NULL   
                                            DEFAULT(0),
    height_in_centimeters                   float NOT NULL   
                                            DEFAULT(0),
    length_in_centimeters                   float NOT NULL   
                                            DEFAULT(0),
    machinable                              boolean NOT NULL   
                                            DEFAULT(false),
    preferred_shipping_mail_type_id         integer NULL REFERENCES core.shipping_mail_types(shipping_mail_type_id),
    shipping_package_shape_id               integer NULL REFERENCES core.shipping_package_shapes(shipping_package_shape_id),    
    unit_id                                 integer NOT NULL REFERENCES core.units(unit_id),
    hot_item                                boolean NOT NULL,
    cost_price                              money_strict NOT NULL,
    cost_price_includes_tax                 boolean NOT NULL   
                                            CONSTRAINT items_cost_price_includes_tax_df                                               
                                            DEFAULT(false),
    selling_price                           money_strict NOT NULL,
    selling_price_includes_tax              boolean NOT NULL   
                                            CONSTRAINT items_selling_price_includes_tax_df 
                                            DEFAULT(false),
    tax_id                                  integer NOT NULL REFERENCES core.taxes(tax_id),
    reorder_unit_id                         integer NOT NULL REFERENCES core.units(unit_id),
    reorder_level                           integer NOT NULL,
    reorder_quantity                        integer NOT NULL
                                            CONSTRAINT items_reorder_quantity_df
                                            DEFAULT(0),
    maintain_stock                          boolean NOT NULL   
                                            DEFAULT(true),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL 
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX items_item_name_uix
ON core.items(UPPER(item_name));

CREATE TABLE core.compound_items
(
    compound_item_id                        SERIAL NOT NULL PRIMARY KEY,
    compound_item_code                      national character varying(12) NOT NULL,
    compound_item_name                      national character varying(150) NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())        
);

CREATE UNIQUE INDEX compound_items_compound_item_code_uix
ON core.compound_items(LOWER(compound_item_code));


CREATE UNIQUE INDEX compound_items_compound_item_name_uix
ON core.compound_items(LOWER(compound_item_name));

CREATE TABLE core.compound_item_details
(
    compound_item_detail_id                 SERIAL NOT NULL PRIMARY KEY,
    compound_item_id                        integer NOT NULL REFERENCES core.compound_items(compound_item_id),
    item_id                                 integer NOT NULL REFERENCES core.items(item_id),
    unit_id                                 integer NOT NULL REFERENCES core.units(unit_id),
    quantity                                integer_strict NOT NULL,
    price                                   money_strict NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())        
);

CREATE UNIQUE INDEX compound_item_details_item_id_uix
ON core.compound_item_details(compound_item_id, item_id);

/*******************************************************************
    PLEASE NOTE :

    THESE ARE THE MOST EFFECTIVE STOCK ITEM PRICES.
    THE PRICE IN THIS CATALOG IS ACTUALLY
    PICKED UP AT THE TIME OF PURCHASE AND SALES.

    A STOCK ITEM PRICE MAY BE DIFFERENT FOR DIFFERENT UNITS.
    FURTHER, A STOCK ITEM WOULD BE SOLD AT A HIGHER PRICE
    WHEN SOLD LOOSE THAN WHAT IT WOULD ACTUALLY COST IN A
    COMPOUND UNIT.

    EXAMPLE, ONE CARTOON (20 BOTTLES) OF BEER BOUGHT AS A UNIT
    WOULD COST 25% LESS FROM THE SAME STORE.

*******************************************************************/

CREATE TABLE core.item_selling_prices
(   
    item_selling_price_id                   BIGSERIAL NOT NULL PRIMARY KEY,
    item_id                                 integer NOT NULL REFERENCES core.items(item_id),
    unit_id                                 integer NOT NULL REFERENCES core.units(unit_id),
    party_type_id                           smallint NULL REFERENCES core.party_types(party_type_id), 
    price_type_id                           smallint NULL REFERENCES core.price_types(price_type_id),
    includes_tax                            boolean NOT NULL   
                                            CONSTRAINT item_selling_prices_includes_tax_df   
                                            DEFAULT('No'),
    price                                   money_strict NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);




CREATE TABLE core.item_cost_prices
(   
    item_cost_price_id                      BIGSERIAL NOT NULL PRIMARY KEY,
    item_id                                 integer NOT NULL REFERENCES core.items(item_id),
    entry_ts                                TIMESTAMP WITH TIME ZONE NOT NULL   
                                            DEFAULT(NOW()),
    unit_id                                 integer NOT NULL REFERENCES core.units(unit_id),
    party_id                                bigint NULL REFERENCES core.parties(party_id),
    lead_time_in_days                       integer NOT NULL   
                                            DEFAULT(0),
    includes_tax                            boolean NOT NULL   
                                            CONSTRAINT item_cost_prices_includes_tax_df   
                                            DEFAULT('No'),
    price                                   money_strict NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);



CREATE TABLE office.store_types
(
    store_type_id                           SERIAL NOT NULL PRIMARY KEY,
    store_type_code                         national character varying(12) NOT NULL,
    store_type_name                         national character varying(50) NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX store_types_Code_uix
ON office.store_types(UPPER(store_type_code));


CREATE UNIQUE INDEX store_types_Name_uix
ON office.store_types(UPPER(store_type_name));




CREATE TABLE office.stores
(
    store_id SERIAL                         NOT NULL PRIMARY KEY,
    office_id                               integer NOT NULL REFERENCES office.offices(office_id),
    store_code                              national character varying(12) NOT NULL,
    store_name                              national character varying(50) NOT NULL,
    address                                 national character varying(50) NULL,
    store_type_id                           integer NOT NULL REFERENCES office.store_types(store_type_id),
    allow_sales                             boolean NOT NULL   
                                            DEFAULT(true),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);


CREATE UNIQUE INDEX stores_store_code_uix
ON office.stores(UPPER(store_code));

CREATE UNIQUE INDEX stores_store_name_uix
ON office.stores(UPPER(store_name));



CREATE TABLE office.cash_repositories
(
    cash_repository_id                      BIGSERIAL NOT NULL PRIMARY KEY,
    office_id                               integer NOT NULL REFERENCES office.offices(office_id),
    cash_repository_code                    national character varying(12) NOT NULL,
    cash_repository_name                    national character varying(50) NOT NULL,
    parent_cash_repository_id               integer NULL REFERENCES office.cash_repositories(cash_repository_id),
    description                             national character varying(100) NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);


CREATE UNIQUE INDEX cash_repositories_cash_repository_code_uix
ON office.cash_repositories(UPPER(cash_repository_code));

CREATE UNIQUE INDEX cash_repositories_cash_repository_name_uix
ON office.cash_repositories(UPPER(cash_repository_name));


 
CREATE TABLE office.counters
(
    counter_id                              SERIAL NOT NULL PRIMARY KEY,
    store_id                                smallint NOT NULL REFERENCES office.stores(store_id),
    cash_repository_id                      integer NOT NULL REFERENCES office.cash_repositories(cash_repository_id),
    counter_code                            national character varying(12) NOT NULL,
    counter_name                            national character varying(50) NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);


CREATE UNIQUE INDEX counters_counter_code_uix
ON office.counters(UPPER(counter_code));

CREATE UNIQUE INDEX counters_counter_name_uix
ON office.counters(UPPER(counter_name));


CREATE TABLE office.cost_centers
(
    cost_center_id                          SERIAL NOT NULL PRIMARY KEY,
    cost_center_code                        national character varying(24) NOT NULL,
    cost_center_name                        national character varying(50) NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX cost_centers_cost_center_code_uix
ON office.cost_centers(UPPER(cost_center_code));

CREATE UNIQUE INDEX cost_centers_cost_center_name_uix
ON office.cost_centers(UPPER(cost_center_name));


CREATE TABLE office.cashiers
(
    cashier_id                              BIGSERIAL NOT NULL PRIMARY KEY,
    counter_id                              integer NOT NULL REFERENCES office.counters(counter_id),
    user_id                                 integer NOT NULL REFERENCES office.users(user_id),
    assigned_by_user_id                     integer NOT NULL REFERENCES office.users(user_id),
    transaction_date                        date NOT NULL,
    closed                                  boolean NOT NULL
);

CREATE UNIQUE INDEX Cashiers_user_id_TDate_uix
ON office.cashiers(user_id ASC, transaction_date DESC);


/*******************************************************************
    STORE POLICY DEFINES THE RIGHT OF USERS TO ACCESS A STORE.
    AN ADMINISTRATOR CAN ACCESS ALL THE stores, BY DEFAULT.
*******************************************************************/

CREATE TABLE policy.store_policies
(
    store_policy_id                         BIGSERIAL NOT NULL PRIMARY KEY,
    written_by_user_id                      integer NOT NULL REFERENCES office.users(user_id),
    status                                  boolean NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);

CREATE TABLE policy.store_policy_details
(
    store_policy_detail_id                  BIGSERIAL NOT NULL PRIMARY KEY,
    store_policy_id                         integer NOT NULL REFERENCES policy.store_policies(store_policy_id),
    user_id                                 integer NOT NULL REFERENCES office.users(user_id),
    store_id                                smallint NOT NULL REFERENCES office.stores(store_id),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);


CREATE TABLE core.item_opening_inventory
(
    item_opening_inventory_id               BIGSERIAL NOT NULL PRIMARY KEY,
    entry_ts                                TIMESTAMP WITH TIME ZONE NOT NULL,
    item_id                                 integer NOT NULL REFERENCES core.items(item_id),
    store_id                                smallint NOT NULL REFERENCES office.stores(store_id),
    unit_id                                 integer NOT NULL REFERENCES core.units(unit_id),
    quantity                                integer NOT NULL,
    amount                                  money_strict NOT NULL,
    base_unit_id                            integer NOT NULL REFERENCES core.units(unit_id),
    base_quantity                           decimal NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);





CREATE TABLE transactions.transaction_master
(
    transaction_master_id                   BIGSERIAL NOT NULL PRIMARY KEY,
    transaction_counter                     integer NOT NULL, --Sequence of transactions of a date
    transaction_code                        national character varying(50) NOT NULL,
    book                                    national character varying(50) NOT NULL, --Transaction book. Ex. Sales, Purchase, Journal
    value_date                              date NOT NULL,
    transaction_ts                          TIMESTAMP WITH TIME ZONE NOT NULL   
                                            DEFAULT(NOW()),
    login_id                                bigint NOT NULL REFERENCES audit.logins(login_id),
    user_id                                 integer NOT NULL REFERENCES office.users(user_id),
    sys_user_id                             integer NULL REFERENCES office.users(user_id),
    office_id                               integer NOT NULL REFERENCES office.offices(office_id),
    cost_center_id                          integer NULL REFERENCES office.cost_centers(cost_center_id),
    reference_number                        national character varying(24) NULL,
    statement_reference                     text NULL,
    last_verified_on                        TIMESTAMP WITH TIME ZONE NULL, 
    verified_by_user_id                     integer NULL REFERENCES office.users(user_id),
    verification_status_id                  smallint NOT NULL REFERENCES core.verification_statuses(verification_status_id)   
                                            DEFAULT(0/*Awaiting verification*/),
    verification_reason                     national character varying(128) NOT NULL   
                                            CONSTRAINT transaction_master_verification_reason_df   
                                            DEFAULT(''),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL                                               
                                            DEFAULT(NOW()),
                                            CONSTRAINT transaction_master_login_id_sys_user_id_chk
                                                CHECK
                                                (
                                                    (
                                                        login_id IS NULL AND sys_user_id IS NOT NULL
                                                    )

                                                    OR

                                                    (
                                                        login_id IS NOT NULL AND sys_user_id IS NULL
                                                    )
                                                )
);

CREATE UNIQUE INDEX transaction_master_transaction_code_uix
ON transactions.transaction_master(UPPER(transaction_code));



CREATE TABLE transactions.transaction_details
(
    transaction_detail_id                   BIGSERIAL NOT NULL PRIMARY KEY,
    transaction_master_id                   bigint NOT NULL REFERENCES transactions.transaction_master(transaction_master_id),
    tran_type                               transaction_type NOT NULL,
    account_id                              bigint NOT NULL REFERENCES core.accounts(account_id),
    statement_reference                     text NULL,
    cash_repository_id                      integer NULL REFERENCES office.cash_repositories(cash_repository_id),
    currency_code                           national character varying(12) NULL REFERENCES core.currencies(currency_code),
    amount_in_currency                      money_strict NOT NULL,
    local_currency_code                     national character varying(12) NULL REFERENCES core.currencies(currency_code),
    er                                      decimal_strict NOT NULL,
    amount_in_local_currency                money_strict NOT NULL,  
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);

CREATE TABLE transactions.customer_receipts
(
    receipt_id                              BIGSERIAL NOT NULL PRIMARY KEY,
    transaction_master_id                   bigint NOT NULL REFERENCES transactions.transaction_master(transaction_master_id),
    party_id                                bigint NOT NULL REFERENCES core.parties(party_id),
    currency_code                           national character varying(12) NOT NULL REFERENCES core.currencies(currency_code),
    amount                                  money_strict NOT NULL,
    er_debit                                decimal_strict NOT NULL,
    er_credit                               decimal_strict NOT NULL,
    cash_repository_id                      integer NULL REFERENCES office.cash_repositories(cash_repository_id),
    posted_date                             date NULL,
    bank_account_id                         bigint NULL REFERENCES core.bank_accounts(account_id),
    bank_instrument_code                    national character varying(128) NULL   
                                            CONSTRAINT customer_receipt_bank_instrument_code_df   
                                            DEFAULT(''),
    bank_tran_code                          national character varying(128) NULL   
                                            CONSTRAINT customer_receipt_bank_tran_code_df   
                                            DEFAULT('')
);

CREATE INDEX customer_receipts_transaction_master_id_inx
ON transactions.customer_receipts(transaction_master_id);

CREATE INDEX customer_receipts_party_id_inx
ON transactions.customer_receipts(party_id);

CREATE INDEX customer_receipts_currency_code_inx
ON transactions.customer_receipts(currency_code);

CREATE INDEX customer_receipts_cash_repository_id_inx
ON transactions.customer_receipts(cash_repository_id);

CREATE INDEX customer_receipts_posted_date_inx
ON transactions.customer_receipts(posted_date);

CREATE INDEX customer_receipts_bank_account_id_inx
ON transactions.customer_receipts(bank_account_id);


CREATE TABLE transactions.stock_master
(
    stock_master_id                         BIGSERIAL NOT NULL PRIMARY KEY,
    transaction_master_id                   bigint NOT NULL REFERENCES transactions.transaction_master(transaction_master_id),
    party_id                                bigint NULL REFERENCES core.parties(party_id),
    salesperson_id                          integer NULL REFERENCES core.salespersons(salesperson_id),
    price_type_id                           integer NULL REFERENCES core.price_types(price_type_id),
    is_credit                               boolean NOT NULL   
                                            CONSTRAINT stock_master_is_credit_df   
                                            DEFAULT(false),
    shipper_id                              integer NULL REFERENCES core.shippers(shipper_id),
    shipping_address_id                     integer NULL REFERENCES core.shipping_addresses(shipping_address_id),
    shipping_charge                         money_strict2 NOT NULL   
                                            CONSTRAINT stock_master_shipping_charge_df   
                                            DEFAULT(0),
    store_id                                integer NULL REFERENCES office.stores(store_id),
    cash_repository_id                      integer NULL REFERENCES office.cash_repositories(cash_repository_id),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX stock_master_transaction_master_id_uix
ON transactions.stock_master(transaction_master_id);


CREATE TABLE transactions.stock_details
(
    stock_master_detail_id                  BIGSERIAL NOT NULL PRIMARY KEY,
    stock_master_id                         bigint NOT NULL REFERENCES transactions.stock_master(stock_master_id),
    tran_type                               transaction_type NOT NULL,
    store_id                                integer NULL REFERENCES office.stores(store_id),
    item_id                                 integer NOT NULL REFERENCES core.items(item_id),
    quantity                                integer NOT NULL,
    unit_id                                 integer NOT NULL REFERENCES core.units(unit_id),
    base_quantity                           decimal NOT NULL,
    base_unit_id                            integer NOT NULL REFERENCES core.units(unit_id),
    price                                   money_strict NOT NULL,
    discount                                money_strict2 NOT NULL   
                                            CONSTRAINT stock_details_discount_df   
                                            DEFAULT(0),
    tax_rate                                decimal NOT NULL   
                                            CONSTRAINT stock_details_tax_rate_df   
                                            DEFAULT(0),
    tax                                     money_strict2 NOT NULL   
                                            CONSTRAINT stock_details_tax_df   
                                            DEFAULT(0),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);


CREATE TABLE transactions.stock_return
(
    sales_return_id                         BIGSERIAL NOT NULL PRIMARY KEY, 
    transaction_master_id                   bigint NOT NULL REFERENCES transactions.transaction_master(transaction_master_id),
    return_transaction_master_id            bigint NOT NULL REFERENCES transactions.transaction_master(transaction_master_id)
);



--TODO
CREATE TABLE transactions.non_gl_stock_master
(
    non_gl_stock_master_id                  BIGSERIAL NOT NULL PRIMARY KEY,
    value_date                              date NOT NULL,
    book                                    national character varying(48) NOT NULL,
    party_id                                bigint NULL REFERENCES core.parties(party_id),
    price_type_id                           integer NULL REFERENCES core.price_types(price_type_id),
    transaction_ts                          TIMESTAMP WITH TIME ZONE NOT NULL   
                                            DEFAULT(NOW()),
    login_id                                bigint NOT NULL REFERENCES audit.logins(login_id),
    user_id                                 integer NOT NULL REFERENCES office.users(user_id),
    office_id                               integer NOT NULL REFERENCES office.offices(office_id),
    reference_number                        national character varying(24) NULL,
    statement_reference                     text NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);


CREATE TABLE transactions.non_gl_stock_details
(
    non_gl_stock_detail_id                  BIGSERIAL NOT NULL PRIMARY KEY,
    non_gl_stock_master_id                  bigint NOT NULL REFERENCES transactions.non_gl_stock_master(non_gl_stock_master_id),
    item_id                                 integer NOT NULL REFERENCES core.items(item_id),
    quantity                                integer NOT NULL,
    unit_id                                 integer NOT NULL REFERENCES core.units(unit_id),
    base_quantity                           decimal NOT NULL,
    base_unit_id                            integer NOT NULL REFERENCES core.units(unit_id),
    price                                   money_strict NOT NULL,
    discount                                money_strict2 NOT NULL   
                                            CONSTRAINT non_gl_stock_details_discount_df   
                                            DEFAULT(0),
    tax_rate                                decimal NOT NULL   
                                            CONSTRAINT non_gl_stock_details_tax_rate_df   
                                            DEFAULT(0),
    tax                                     money_strict2 NOT NULL   
                                            CONSTRAINT non_gl_stock_details_tax_df   
                                            DEFAULT(0),
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);


--This table stores information of quotations
--which were upgraded to order(s).
CREATE TABLE transactions.non_gl_stock_master_relations
(
    non_gl_stock_master_relation_id         BIGSERIAL NOT NULL PRIMARY KEY, 
    order_non_gl_stock_master_id            bigint NOT NULL REFERENCES transactions.non_gl_stock_master(non_gl_stock_master_id),
    quotation_non_gl_stock_master_id        bigint NOT NULL REFERENCES transactions.non_gl_stock_master(non_gl_stock_master_id)
);


--This table stores information of Non GL Stock Transactions such as orders and quotations
--which were upgraded to deliveries or invoices.
CREATE TABLE transactions.stock_master_non_gl_relations
(
    stock_master_non_gl_relation_id         BIGSERIAL NOT NULL PRIMARY KEY, 
    stock_master_id                         bigint NOT NULL REFERENCES transactions.stock_master(stock_master_id),
    non_gl_stock_master_id                  bigint NOT NULL REFERENCES transactions.non_gl_stock_master(non_gl_stock_master_id)
);


CREATE TABLE crm.lead_sources
(
    lead_source_id                          SERIAL NOT NULL PRIMARY KEY,
    lead_source_code                        national character varying(12) NOT NULL,
    lead_source_name                        national character varying(128) NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX lead_sources_lead_source_code_uix
ON crm.lead_sources(UPPER(lead_source_code));


CREATE UNIQUE INDEX lead_sources_lead_source_name_uix
ON crm.lead_sources(UPPER(lead_source_name));



CREATE TABLE crm.lead_statuses
(
    lead_status_id                          SERIAL NOT NULL PRIMARY KEY,
    lead_status_code                        national character varying(12) NOT NULL,
    lead_status_name                        national character varying(128) NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX lead_statuses_lead_status_code_uix
ON crm.lead_statuses(UPPER(lead_status_code));


CREATE UNIQUE INDEX lead_statuses_lead_status_name_uix
ON crm.lead_statuses(UPPER(lead_status_name));



CREATE TABLE crm.opportunity_stages
(
    opportunity_stage_id                    SERIAL  NOT NULL PRIMARY KEY,
    opportunity_stage_code                  national character varying(12) NOT NULL,
    opportunity_stage_name                  national character varying(50) NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);


CREATE UNIQUE INDEX opportunity_stages_opportunity_stage_code_uix
ON crm.opportunity_stages(UPPER(opportunity_stage_code));

CREATE UNIQUE INDEX opportunity_stages_opportunity_stage_name_uix
ON crm.opportunity_stages(UPPER(opportunity_stage_name));





CREATE TABLE core.switch_categories
(
    switch_category_id                      SERIAL NOT NULL PRIMARY KEY,
    switch_category_name                    national character varying(128) NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX switch_categories_switch_category_name_uix
ON core.switch_categories(UPPER(switch_category_name));

CREATE TABLE office.work_centers
(
    work_center_id                          SERIAL NOT NULL PRIMARY KEY,
    office_id                               integer NOT NULL REFERENCES office.offices(office_id),
    work_center_code                        national character varying(12) NOT NULL,
    work_center_name                        national character varying(128) NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);

CREATE UNIQUE INDEX work_centers_work_center_code_uix
ON office.work_centers(UPPER(work_center_code));

CREATE UNIQUE INDEX work_centers_work_center_name_uix
ON office.work_centers(UPPER(work_center_name));


CREATE TABLE policy.voucher_verification_policy
(
    user_id                                 integer NOT NULL PRIMARY KEY REFERENCES office.users(user_id),
    can_verify_sales_transactions           boolean NOT NULL   
                                            CONSTRAINT voucher_verification_policy_verify_sales_df 
                                            DEFAULT(false),
    sales_verification_limit                money_strict2 NOT NULL   
                                            CONSTRAINT voucher_verification_policy_sales_verification_limit_df 
                                            DEFAULT(0),
    can_verify_purchase_transactions        boolean NOT NULL   
                                            CONSTRAINT voucher_verification_policy_verify_purchase_df 
                                            DEFAULT(false),
    purchase_verification_limit             money_strict2 NOT NULL   
                                            CONSTRAINT voucher_verification_policy_purchase_verification_limit_df 
                                            DEFAULT(0),
    can_verify_gl_transactions              boolean NOT NULL   
                                            CONSTRAINT voucher_verification_policy_verify_gl_df 
                                            DEFAULT(false),
    gl_verification_limit                   money_strict2 NOT NULL   
                                            CONSTRAINT voucher_verification_policy_gl_verification_limit_df 
                                            DEFAULT(0),
    can_self_verify                         boolean NOT NULL   
                                            CONSTRAINT voucher_verification_policy_verify_self_df 
                                            DEFAULT(false),
    self_verification_limit                 money_strict2 NOT NULL   
                                            CONSTRAINT voucher_verification_policy_self_verification_limit_df 
                                            DEFAULT(0),
    effective_from                          date NOT NULL,
    ends_on                                 date NOT NULL,
    is_active                               boolean NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL 
                                            DEFAULT(NOW())
);


CREATE TABLE policy.auto_verification_policy
(
    user_id                                 integer NOT NULL PRIMARY KEY REFERENCES office.users(user_id),
    verify_sales_transactions               boolean NOT NULL   
                                            CONSTRAINT auto_verification_policy_verify_sales_df   
                                            DEFAULT(false),
    sales_verification_limit                money_strict2 NOT NULL   
                                            CONSTRAINT auto_verification_policy_sales_verification_limit_df   
                                            DEFAULT(0),
    verify_purchase_transactions            boolean NOT NULL   
                                            CONSTRAINT auto_verification_policy_verify_purchase_df   
                                            DEFAULT(false),
    purchase_verification_limit             money_strict2 NOT NULL   
                                            CONSTRAINT auto_verification_policy_purchase_verification_limit_df   
                                            DEFAULT(0),
    verify_gl_transactions                  boolean NOT NULL   
                                            CONSTRAINT auto_verification_policy_verify_gl_df   
                                            DEFAULT(false),
    gl_verification_limit                   money_strict2 NOT NULL   
                                            CONSTRAINT auto_verification_policy_gl_verification_limit_df   
                                            DEFAULT(0),
    effective_from                          date NOT NULL,
    ends_on                                 date NOT NULL,
    is_active                               boolean NOT NULL,
    audit_user_id                           integer NULL REFERENCES office.users(user_id),
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL   
                                            DEFAULT(NOW())
);

