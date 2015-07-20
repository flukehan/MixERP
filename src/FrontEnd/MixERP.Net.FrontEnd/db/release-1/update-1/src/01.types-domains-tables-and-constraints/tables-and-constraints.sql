DROP INDEX IF EXISTS policy.menu_access_uix;


CREATE UNIQUE INDEX menu_access_uix
ON policy.menu_access(office_id, menu_id, user_id);

ALTER TABLE policy.auto_verification_policy
DROP CONSTRAINT IF EXISTS auto_verification_policy_pkey;

ALTER TABLE policy.auto_verification_policy
ADD PRIMARY KEY(user_id, office_id);

DO
$$
BEGIN
    IF
    (
        SELECT count(*)
        FROM   pg_attribute 
        WHERE  attrelid = 'core.parties'::regclass
        AND    attname IN ('party_name', 'company_name')
        AND    NOT attisdropped
    ) = 1 THEN
        ALTER TABLE core.parties
        RENAME COLUMN party_name to company_name;
        
        ALTER TABLE core.parties
        ADD COLUMN party_name text;
        
        UPDATE core.parties
        SET party_name = company_name;

        ALTER TABLE core.parties
        ALTER COLUMN company_name DROP NOT NULL;
        
        UPDATE core.parties
        SET company_name = NULL;

        ALTER TABLE core.parties
        DROP CONSTRAINT IF EXISTS parties_customer_name_chk;

        ALTER TABLE core.parties
        ADD CONSTRAINT parties_customer_name_chk
        CHECK(CASE WHEN COALESCE(company_name, '') = '' THEN  COALESCE(first_name, '') != '' AND COALESCE(last_name, '') != '' END);
    END IF;
END
$$
LANGUAGE plpgsql;

ALTER TABLE core.parties
ALTER COLUMN first_name DROP NOT NULL;

ALTER TABLE core.parties
ALTER COLUMN last_name DROP NOT NULL;

ALTER TABLE core.parties
ALTER COLUMN company_name DROP NOT NULL;


DO
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT 1
        FROM   pg_attribute 
        WHERE  attrelid = 'office.users'::regclass
        AND    attname = 'store_id'
        AND    NOT attisdropped
    ) THEN
        ALTER TABLE office.users
        ADD COLUMN store_id integer REFERENCES office.stores(store_id)
        CONSTRAINT users_store_id_chk 
        CHECK
        (
            office.get_office_id_by_store_id(store_id) IS NULL OR
            office.get_office_id_by_store_id(store_id) = office_id
        );
    END IF;    
END
$$
LANGUAGE plpgsql;

DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1 
        FROM   pg_catalog.pg_class c
        JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
        WHERE  n.nspname = 'transactions'
        AND    c.relname = 'inventory_transfer_requests'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE transactions.inventory_transfer_requests
        (
            inventory_transfer_request_id               BIGSERIAL NOT NULL PRIMARY KEY,
            office_id                                   integer NOT NULL REFERENCES office.offices(office_id),
            user_id                                     integer NOT NULL REFERENCES office.users(user_id),
            login_id                                    bigint NOT NULL REFERENCES audit.logins(login_id),
            store_id                                    integer NOT NULL REFERENCES office.stores(store_id),
            value_date                                  date NOT NULL,
            transaction_ts                              TIMESTAMP WITH TIME ZONE DEFAULT(now()),
            reference_number                            national character varying(24) NOT NULL,
            statement_reference                         text,
            authorization_status_id                     smallint NOT NULL REFERENCES core.verification_statuses(verification_status_id)
                                                        DEFAULT(0)
                                                        CONSTRAINT inventory_transfer_requests_withdrawn_chk
                                                        CHECK(CASE WHEN authorization_status_id = -1 THEN delivered=false AND received=false AND user_id = authorized_by_user_id END),
            authorized_by_user_id                       integer REFERENCES office.users(user_id),
            authorized_on                               TIMESTAMP WITH TIME ZONE,
            authorization_reason                        national character varying(128),
            received                                    boolean NOT NULL DEFAULT(FALSE),
            received_by_user_id                         integer REFERENCES office.users(user_id),
            received_on                                 TIMESTAMP WITH TIME ZONE,
            delivered                                   boolean NOT NULL DEFAULT(FALSE),
            delivered_by_user_id                        integer REFERENCES office.users(user_id),
            delivered_on                                TIMESTAMP WITH TIME ZONE,
            audit_ts                                    TIMESTAMP WITH TIME ZONE DEFAULT(now())
        );
    END IF;    
END
$$
LANGUAGE plpgsql;

DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1 
        FROM   pg_catalog.pg_class c
        JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
        WHERE  n.nspname = 'transactions'
        AND    c.relname = 'inventory_transfer_request_details'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE transactions.inventory_transfer_request_details
        (
            inventory_transfer_request_detail_id        BIGSERIAL NOT NULL PRIMARY KEY,
            inventory_transfer_request_id               bigint NOT NULL REFERENCES transactions.inventory_transfer_requests(inventory_transfer_request_id),
            value_date                                  date NOT NULL,
            item_id                                     integer NOT NULL REFERENCES core.items(item_id),
            quantity                                    integer NOT NULL,
            unit_id                                     integer NOT NULL REFERENCES core.units(unit_id),
            base_quantity                               numeric NOT NULL,
            base_unit_id                                integer NOT NULL REFERENCES core.units(unit_id)
        );
    END IF;    
END
$$
LANGUAGE plpgsql;


DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1 
        FROM   pg_catalog.pg_class c
        JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
        WHERE  n.nspname = 'config'
        AND    c.relname = 'attachment_factory'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE config.attachment_factory
        (
            key                 text PRIMARY KEY,
            value               text,
            audit_user_id       integer NULL REFERENCES office.users(user_id),
            audit_ts            TIMESTAMP WITH TIME ZONE NULL 
                                DEFAULT(NOW())
        );

        INSERT INTO config.attachment_factory
        SELECT 'AttachmentsDirectory',              '/Resource/Static/Attachments/' UNION ALL
        SELECT 'UploadHandlerUrl',                  '~/FileUploadHanlder.ashx' UNION ALL
        SELECT 'UndoUploadServiceUrl',              '~/FileUploadHanlder.asmx/UndoUpload' UNION ALL
        SELECT 'AllowedExtensions',                 'jpg,jpeg,gif,png,tif,doc,docx,xls,xlsx,pdf';
    END IF;


    IF NOT EXISTS (
        SELECT 1 
        FROM   pg_catalog.pg_class c
        JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
        WHERE  n.nspname = 'config'
        AND    c.relname = 'currency_layer'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE config.currency_layer
        (
            key                 text PRIMARY KEY,
            value               text,
            description         text,
            audit_user_id       integer NULL REFERENCES office.users(user_id),
            audit_ts            TIMESTAMP WITH TIME ZONE NULL 
                                DEFAULT(NOW())
        );

        INSERT INTO config.currency_layer
        SELECT 'Enabled',                           'true', '' UNION ALL
        SELECT 'UserAgent',                         'MixERP', '' UNION ALL
        SELECT 'MediaType',                         'application/json', '' UNION ALL
        SELECT 'APIAccessKey',                      '', '' UNION ALL
        SELECT 'APIUrl',                            'http://apilayer.net/api/live', '' UNION ALL
        SELECT 'AccessKeyName',                     'access_key', '' UNION ALL
        SELECT 'CurrenciesKey',                     'currencies', '' UNION ALL
        SELECT 'SourceKey',                         'source', '' UNION ALL
        SELECT 'FormatKey',                         'format', '' UNION ALL
        SELECT 'DecimalPlaces',                     '4', '' UNION ALL
        SELECT 'DefaultFormat',                     '1', '1 = JSON' UNION ALL
        SELECT 'ResultSubKey',                      'quotes', 'The sub-key which contains list of converted currencies' UNION ALL
        SELECT 'RemoveSourceCurrencyFromResult',    'true', 'Currencylayer prepends source currency on all result items. This must be set to true unless this behavior is changed in the future.';
    END IF;

    IF NOT EXISTS (
        SELECT 1 
        FROM   pg_catalog.pg_class c
        JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
        WHERE  n.nspname = 'config'
        AND    c.relname = 'db_paramters'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE config.db_paramters
        (
            key                 text PRIMARY KEY,
            value               text,
            audit_user_id       integer NULL REFERENCES office.users(user_id),
            audit_ts            TIMESTAMP WITH TIME ZONE NULL 
                                DEFAULT(NOW())
        );

        INSERT INTO config.db_paramters
        SELECT 'AccountMasterDisplayField', 'account_master_code + '' ('' + account_master_name + '')''' UNION ALL
        SELECT 'AccountDisplayField', 'account_number + '' ('' + account_name + '')''' UNION ALL
        SELECT 'SalespersonDisplayField', 'salesperson_code + '' ('' + salesperson_name + '')''' UNION ALL
        SELECT 'SalesTeamDisplayField', 'sales_team_code + '' ('' + sales_team_name + '')''' UNION ALL
        SELECT 'BankAccountDisplayField', 'bank_name + '' ('' + bank_branch + '')''' UNION ALL
        SELECT 'BonusSlabDisplayField', 'bonus_slab_name' UNION ALL
        SELECT 'BrandDisplayField', 'brand_code + '' ('' + brand_name + '')''' UNION ALL
        SELECT 'CardTypeDisplayField', 'card_type_code + '' ('' + card_type_name + '')''' UNION ALL
        SELECT 'CashRepositoryDisplayField', 'cash_repository_code + '' ('' + cash_repository_name + '')''' UNION ALL
        SELECT 'CashFlowHeadingDisplayField', 'cash_flow_heading_code + '' ('' + cash_flow_heading_name + '')''' UNION ALL
        SELECT 'CompoundItemDisplayField', 'compound_item_code + '' ('' + compound_item_name + '')''' UNION ALL
        SELECT 'CostCenterDisplayField', 'cost_center_code + '' ('' + cost_center_name + '')''' UNION ALL
        SELECT 'CountryDisplayField', 'country_code + '' ('' + country_name + '')''' UNION ALL
        SELECT 'CountyDisplayField', 'county_code + '' ('' + county_name + '')''' UNION ALL
        SELECT 'CountySalesTaxDisplayField', 'county_sales_tax_code + '' ('' + county_sales_tax_name + '')''' UNION ALL
        SELECT 'CurrencyDisplayField', 'currency_symbol + '' ('' + currency_code + ''/'' + currency_name + '')''' UNION ALL
        SELECT 'CustomerDisplayField', 'last_name + '', '' + fist_name + '' '' + middle_name' UNION ALL
        SELECT 'DepartmentDisplayField', 'department_code + '' ('' + department_name + '')''' UNION ALL
        SELECT 'EntityDisplayField', 'entity_name' UNION ALL
        SELECT 'FrequencyDisplayField', 'frequency_code' UNION ALL
        SELECT 'FiscalYearDisplayField', 'fiscal_year_code + '' ('' + fiscal_year_name + '')''' UNION ALL
        SELECT 'IndustryDisplayField', 'industry_name' UNION ALL
        SELECT 'ItemDisplayField', 'item_code + '' ('' + item_name + '')''' UNION ALL
        SELECT 'ItemTypeDisplayField', 'item_type_code + '' ('' + item_type_name + '')''' UNION ALL
        SELECT 'ItemGroupDisplayField', 'item_group_code + '' ('' + item_group_name + '')''' UNION ALL
        SELECT 'LateFeeDisplayField', 'late_fee_code + '' ('' + late_fee_name + '')''' UNION ALL
        SELECT 'OfficeDisplayField', 'office_code + '' ('' + office_name + '')''' UNION ALL
        SELECT 'PartyDisplayField', 'party_code + '' ('' + party_name + '')''' UNION ALL
        SELECT 'PartyTypeDisplayField', 'party_type_code + '' ('' + party_type_name + '')''' UNION ALL
        SELECT 'PaymentCardDisplayField', 'payment_card_code + '' ('' + payment_card_name + '')''' UNION ALL
        SELECT 'PaymentTermDisplayField', 'payment_term_code + '' ('' + payment_term_name + '')''' UNION ALL
        SELECT 'PriceTypeDisplayField', 'price_type_code + '' ('' + price_type_name + '')''' UNION ALL
        SELECT 'RecurrenceTypeDisplayField', 'recurrence_type_code + '' ('' + recurrence_type_name + '')''' UNION ALL
        SELECT 'RecurringInvoiceDisplayField', 'recurring_invoice_code + '' ('' + recurring_invoice_name + '')''' UNION ALL
        SELECT 'RoleDisplayField', 'role_code + '' ('' + role_name + '')''' UNION ALL
        SELECT 'RoundingMethodCodeDisplayField', 'rounding_method_code + '' ('' + rounding_method_name + '')''' UNION ALL
        SELECT 'SalesTaxDisplayField', 'sales_tax_code + '' ('' + sales_tax_name + '')''' UNION ALL
        SELECT 'SalesTaxExemptDisplayField', 'sales_tax_exempt_code + '' ('' + sales_tax_exempt_name + '')''' UNION ALL
        SELECT 'SalesTaxTypeDisplayField', 'sales_tax_type_code + '' ('' + sales_tax_type_name + '')''' UNION ALL
        SELECT 'StateSalesTaxDisplayField', 'state_sales_tax_code + '' ('' + state_sales_tax_name + '')''' UNION ALL
        SELECT 'ShipperDisplayField', 'company_name' UNION ALL
        SELECT 'ShippingMailTypeDisplayField', 'shipping_mail_type_code + '' ('' + shipping_mail_type_name + '')''' UNION ALL
        SELECT 'ShippingPackageShapeDisplayField', 'shipping_package_shape_code + '' ('' + shipping_package_shape_name + '')''' UNION ALL
        SELECT 'StateDisplayField', 'state_code + '' ('' + state_name + '')''' UNION ALL
        SELECT 'StoreDisplayField', 'store_name' UNION ALL
        SELECT 'StoreTypeDisplayField', 'store_type_name' UNION ALL
        SELECT 'TaxAuthorityDisplayField', 'tax_authority_code + '' ('' + tax_authority_name + '')''' UNION ALL
        SELECT 'TaxBaseAmountTypeDisplayField', 'tax_base_amount_type_code + '' ('' + tax_base_amount_type_name + '')''' UNION ALL
        SELECT 'TaxExemptTypeDisplayField', 'tax_exempt_type_code + '' ('' + tax_exempt_type_name + '')''' UNION ALL
        SELECT 'TaxRateTypeDisplayField', 'tax_rate_type_code + '' ('' + tax_rate_type_name + '')''' UNION ALL
        SELECT 'TaxDisplayField', 'tax_name' UNION ALL
        SELECT 'TaxTypeDisplayField', 'tax_type_code + '' ('' + tax_type_name + '')''' UNION ALL
        SELECT 'TaxMasterDisplayField', 'tax_master_code + '' ('' + tax_master_name + '')''' UNION ALL
        SELECT 'TransactionTypeDisplayField', 'transaction_type_code + '' ('' + transaction_type_name + '')''' UNION ALL
        SELECT 'UnitDisplayField', 'unit_name' UNION ALL
        SELECT 'UserDisplayField', 'user_name';
    END IF;


    IF NOT EXISTS (
        SELECT 1 
        FROM   pg_catalog.pg_class c
        JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
        WHERE  n.nspname = 'config'
        AND    c.relname = 'messaging'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE config.messaging
        (
            key                 text PRIMARY KEY,
            value               text,
            audit_user_id       integer NULL REFERENCES office.users(user_id),
            audit_ts            TIMESTAMP WITH TIME ZONE NULL 
                                DEFAULT(NOW())
        );

        INSERT INTO config.messaging
        SELECT 'FromDisplayName',                   'MixERP' UNION ALL
        SELECT 'FromEmailAddress',                  'mixerp@localhost' UNION ALL
        SELECT 'SmtpDeliveryMethod',                'SpecifiedPickupDirectory' UNION ALL
        SELECT 'SpecifiedPickupDirectoryLocation',  '~/Resource/Static/Emails' UNION ALL
        SELECT 'SMTPHost',                          'smtp-mail.outlook.com' UNION ALL
        SELECT 'SMTPPort',                          '587' UNION ALL
        SELECT 'SMTPEnableSSL',                     'true' UNION ALL
        SELECT 'SMTPUserName',                      '' UNION ALL
        SELECT 'SMTPPassword',                      '';
    END IF;


    IF NOT EXISTS (
        SELECT 1 
        FROM   pg_catalog.pg_class c
        JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
        WHERE  n.nspname = 'config'
        AND    c.relname = 'mixerp'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE config.mixerp
        (
            key                 text PRIMARY KEY,
            value               text,
            description         text,
            audit_user_id       integer NULL REFERENCES office.users(user_id),
            audit_ts            TIMESTAMP WITH TIME ZONE NULL 
                                DEFAULT(NOW())
        );

        INSERT INTO config.mixerp
        SELECT 'MinimumLogLevel', 'Information', '' UNION ALL
        SELECT 'ApplicationLogDirectory', 'C:\mixerp-logs', 'Must be a physical path and application pool identity user must be able to write to it.' UNION ALL
        SELECT 'Mode', 'Development', '';
    END IF;


    IF NOT EXISTS (
        SELECT 1 
        FROM   pg_catalog.pg_class c
        JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
        WHERE  n.nspname = 'config'
        AND    c.relname = 'open_exchange_rates'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE config.open_exchange_rates
        (
            key                 text PRIMARY KEY,
            value               text,
            description         text,
            audit_user_id       integer NULL REFERENCES office.users(user_id),
            audit_ts            TIMESTAMP WITH TIME ZONE NULL 
                                DEFAULT(NOW())
        );

        INSERT INTO config.open_exchange_rates
        SELECT 'Enabled', 'true', '' UNION ALL
        SELECT 'UserAgent', 'MixERP', '' UNION ALL
        SELECT 'MediaType', 'application/json', '' UNION ALL
        SELECT 'AppId', '', '' UNION ALL
        SELECT 'APIUrl', 'http://openexchangerates.org/api/latest.json', '' UNION ALL
        SELECT 'AppIdKey', 'app_id', '' UNION ALL
        SELECT 'CurrenciesKey', 'symbols', '' UNION ALL
        SELECT 'SpecificCurrencies', 'false', '' UNION ALL
        SELECT 'BaseCurrencyKey', 'base', '' UNION ALL
        SELECT 'DecimalPlaces', '4', '' UNION ALL
        SELECT 'ResultSubKey', 'rates', 'The sub-key which contains list of converted currencies';
    END IF;

    IF NOT EXISTS (
        SELECT 1 
        FROM   pg_catalog.pg_class c
        JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
        WHERE  n.nspname = 'config'
        AND    c.relname = 'scrud_factory'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE config.scrud_factory
        (
            key                 text PRIMARY KEY,
            value               text,
            audit_user_id       integer NULL REFERENCES office.users(user_id),
            audit_ts            TIMESTAMP WITH TIME ZONE NULL 
                                DEFAULT(NOW())
        );

        INSERT INTO config.scrud_factory
        SELECT 'CommandPanelCssClass', 'vpad16' UNION ALL
        SELECT 'CommandPanelButtonCssClass', 'small ui button' UNION ALL
        SELECT 'SelectButtonIconCssClass', '' UNION ALL
        SELECT 'CompactButtonIconCssClass', '' UNION ALL
        SELECT 'AllButtonIconCssClass', '' UNION ALL
        SELECT 'AddButtonIconCssClass', '' UNION ALL
        SELECT 'EditButtonIconCssClass', '' UNION ALL
        SELECT 'DeleteButtonIconCssClass', '' UNION ALL
        SELECT 'PrintButtonIconCssClass', '' UNION ALL
        SELECT 'DescriptionCssClass', 'ui large purple header' UNION ALL
        SELECT 'ErrorCssClass', 'error-message' UNION ALL
        SELECT 'ExpressionSeparator', '-->' UNION ALL
        SELECT 'FailiureCssClass', 'big error' UNION ALL
        SELECT 'FormCssClass', 'form-panel ui segment' UNION ALL
        SELECT 'FormPanelCssClass', 'ui form' UNION ALL
        SELECT 'GridPanelCssClass', 'segment' UNION ALL
        SELECT 'GridViewAlternateRowCssClass', '' UNION ALL
        SELECT 'GridViewCssClass', 'ui celled striped definition sortable table segment' UNION ALL
        SELECT 'GridViewDefaultWidth', '100%' UNION ALL
        SELECT 'GridPanelDefaultWidth', '1000px' UNION ALL
        SELECT 'GridPanelStyle', 'padding:2px;overflow:auto;' UNION ALL
        SELECT 'GridViewRowCssClass', 'gridview-row pointer' UNION ALL
        SELECT 'HeaderPath', '~/Reports/Assets/Header.aspx' UNION ALL
        SELECT 'ItemSelectorAnchorCssClass', '' UNION ALL
        SELECT 'ItemSelectorPath', '~/General/ItemSelector.aspx' UNION ALL
        SELECT 'ItemSelectorSelectAnchorCssClass', 'linkbutton' UNION ALL
        SELECT 'ItemSeparator', ',' UNION ALL
        SELECT 'PagerCssClass', 'ui pagination menu vmargin8' UNION ALL
        SELECT 'PagerCurrentPageCssClass', 'active item' UNION ALL
        SELECT 'PagerPageButtonCssClass', 'item' UNION ALL
        SELECT 'PageSize', '10' UNION ALL
        SELECT 'ResourceClassName', 'ScrudResource' UNION ALL
        SELECT 'ButtonCssClass', 'small ui button' UNION ALL
        SELECT 'SaveButtonCssClass', 'small ui button' UNION ALL
        SELECT 'SuccessCssClass', 'ui large green header' UNION ALL
        SELECT 'TemplatePath', '~/Reports/Print.html' UNION ALL
        SELECT 'TempMediaPath', '~/Media/Temp' UNION ALL
        SELECT 'TitleLabelCssClass', 'title' UNION ALL
        SELECT 'UpdateProgressSpinnerImageCssClass', 'ajax-loader' UNION ALL
        SELECT 'UpdateProgressSpinnerImagePath', '~/Static/images/spinner.gif' UNION ALL
        SELECT 'UpdateProgressTemplateCssClass', 'ajax-container';
    END IF;


    IF NOT EXISTS (
        SELECT 1 
        FROM   pg_catalog.pg_class c
        JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
        WHERE  n.nspname = 'config'
        AND    c.relname = 'switches'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE config.switches
        (
            key                 text PRIMARY KEY,
            value               boolean,
            audit_user_id       integer NULL REFERENCES office.users(user_id),
            audit_ts            TIMESTAMP WITH TIME ZONE NULL 
                                DEFAULT(NOW())
        );

        INSERT INTO config.switches
        SELECT 'AllowParentAccountInGLTransaction', false UNION ALL
        SELECT 'AllowMultipleOpeningInventory', false;
    END IF;
END
$$
LANGUAGE plpgsql;

DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1 
        FROM   pg_catalog.pg_class c
        JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
        WHERE  n.nspname = 'transactions'
        AND    c.relname = 'inventory_transfer_deliveries'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE transactions.inventory_transfer_deliveries
        (
            inventory_transfer_delivery_id              BIGSERIAL NOT NULL PRIMARY KEY,
            inventory_transfer_request_id               bigint NOT NULL REFERENCES transactions.inventory_transfer_requests(inventory_transfer_request_id),
            office_id                                   integer NOT NULL REFERENCES office.offices(office_id),
            user_id                                     integer NOT NULL REFERENCES office.users(user_id),
            login_id                                    bigint NOT NULL REFERENCES audit.logins(login_id),
            source_store_id                             integer NOT NULL REFERENCES office.stores(store_id),
            destination_store_id                        integer NOT NULL REFERENCES office.stores(store_id)
                                                        CONSTRAINT inventory_transfer_deliveries_store_chk
                                                        CHECK(source_store_id <> destination_store_id),
            value_date                                  date NOT NULL,
            transaction_ts                              TIMESTAMP WITH TIME ZONE DEFAULT(now()),
            reference_number                            national character varying(24) NOT NULL,
            statement_reference                         text,
            audit_ts                                    TIMESTAMP WITH TIME ZONE DEFAULT(now())
        );

        CREATE UNIQUE INDEX inventory_transfer_deliveries_inventory_transfer_request_id_uix
        ON transactions.inventory_transfer_deliveries(inventory_transfer_request_id);

    END IF;    
END
$$
LANGUAGE plpgsql;

DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1 
        FROM   pg_catalog.pg_class c
        JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
        WHERE  n.nspname = 'transactions'
        AND    c.relname = 'inventory_transfer_delivery_details'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE transactions.inventory_transfer_delivery_details
        (
            inventory_transfer_delivery_detail_id       BIGSERIAL NOT NULL PRIMARY KEY,
            inventory_transfer_delivery_id              bigint NOT NULL REFERENCES transactions.inventory_transfer_deliveries(inventory_transfer_delivery_id),
            value_date                                  date NOT NULL,
            item_id                                     integer NOT NULL REFERENCES core.items(item_id),
            quantity                                    integer NOT NULL,
            unit_id                                     integer NOT NULL REFERENCES core.units(unit_id),
            base_quantity                               numeric NOT NULL,
            base_unit_id                                integer NOT NULL REFERENCES core.units(unit_id)
        );
    END IF;    
END
$$
LANGUAGE plpgsql;
