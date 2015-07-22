DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1 
        FROM   pg_catalog.pg_class c
        JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
        WHERE  n.nspname = 'policy'
        AND    c.relname = 'http_actions'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE policy.http_actions
        (
            http_action_code                text NOT NULL PRIMARY KEY
        );

        CREATE UNIQUE INDEX policy_http_action_code_uix
        ON policy.http_actions(UPPER(http_action_code));    
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
        WHERE  n.nspname = 'policy'
        AND    c.relname = 'api_access_policy'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE policy.api_access_policy
        (
            api_access_policy_id            BIGSERIAL NOT NULL PRIMARY KEY,
            user_id                         integer NOT NULL REFERENCES office.users(user_id),
            office_id                       integer NOT NULL REFERENCES office.offices(office_id),
            poco_type_name                  text NOT NULL,
            http_action_code                text NOT NULL REFERENCES policy.http_actions(http_action_code),
            valid_till                      date NOT NULL,
            audit_user_id                   integer NULL REFERENCES office.users(user_id),
            audit_ts                        TIMESTAMP WITH TIME ZONE NULL 
                                            DEFAULT(NOW())    
        );

        CREATE UNIQUE INDEX api_access_policy_uix
        ON policy.api_access_policy(user_id, UPPER(poco_type_name), UPPER(http_action_code), valid_till);
    
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
        WHERE  n.nspname = 'core'
        AND    c.relname = 'recurrence_types'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE core.recurrence_types
        (
            recurrence_type_id              SERIAL NOT NULL PRIMARY KEY,
            recurrence_type_code            national character varying(12) NOT NULL,
            recurrence_type_name            national character varying(50) NOT NULL,
            is_frequency                    boolean NOT NULL,
            audit_user_id                   integer NULL REFERENCES office.users(user_id),
            audit_ts                        TIMESTAMP WITH TIME ZONE NULL 
                                            DEFAULT(NOW())            
        );
    END IF;    
END
$$
LANGUAGE plpgsql;




DROP TABLE IF EXISTS core.recurring_invoices_temp;
DROP TABLE IF EXISTS core.recurring_invoice_setup_temp;

CREATE TABLE core.recurring_invoices_temp
AS
SELECT * FROM core.recurring_invoices;

CREATE TABLE core.recurring_invoice_setup_temp
AS
SELECT * FROM core.recurring_invoice_setup;

DROP TABLE IF EXISTS core.recurring_invoices CASCADE;
DROP TABLE IF EXISTS core.recurring_invoice_setup CASCADE;

CREATE TABLE core.recurring_invoices
(
    recurring_invoice_id                        SERIAL NOT NULL PRIMARY KEY,
    recurring_invoice_code                      national character varying(12) NOT NULL,
    recurring_invoice_name                      national character varying(50) NOT NULL,
    item_id                                     integer NULL REFERENCES core.items(item_id),
    total_duration                              integer NOT NULL DEFAULT(365),
    recurrence_type_id                          integer REFERENCES core.recurrence_types(recurrence_type_id),
    recurring_frequency_id                      integer NOT NULL REFERENCES core.frequencies(frequency_id),
    recurring_duration                          public.integer_strict2 NOT NULL DEFAULT(30),
    recurs_on_same_calendar_date                boolean NOT NULL DEFAULT(true),
    recurring_amount                            public.money_strict NOT NULL,
    account_id                                  bigint REFERENCES core.accounts(account_id),
    payment_term_id                             integer NOT NULL REFERENCES core.payment_terms(payment_term_id),
    auto_trigger_on_sales                       boolean NOT NULL,
    is_active                                   boolean NOT NULL DEFAULT(true),
    statement_reference                         national character varying(100) NOT NULL DEFAULT(''),
    audit_user_id                               integer NULL REFERENCES office.users(user_id),
    audit_ts                                    TIMESTAMP WITH TIME ZONE NULL 
                                                DEFAULT(NOW())
);

CREATE UNIQUE INDEX recurring_invoices_item_id_auto_trigger_on_sales_uix
ON core.recurring_invoices(item_id, auto_trigger_on_sales)
WHERE auto_trigger_on_sales = true;

CREATE TABLE core.recurring_invoice_setup
(
    recurring_invoice_setup_id                  SERIAL NOT NULL PRIMARY KEY,
    recurring_invoice_id                        integer NOT NULL REFERENCES core.recurring_invoices(recurring_invoice_id),
    party_id                                    bigint NOT NULL REFERENCES core.parties(party_id),
    starts_from                                 date NOT NULL,
    ends_on                                     date NOT NULL
                                                CONSTRAINT recurring_invoice_setup_date_chk
                                                CHECK
                                                (
                                                    ends_on >= starts_from
                                                ),
    recurrence_type_id                          integer NOT NULL REFERENCES core.recurrence_types(recurrence_type_id),
    recurring_frequency_id                      integer NULL REFERENCES core.frequencies(frequency_id),
    recurring_duration                          public.integer_strict2 NOT NULL DEFAULT(0),
    recurs_on_same_calendar_date                boolean NOT NULL DEFAULT(true),
    recurring_amount                            public.money_strict NOT NULL,
    account_id                                  bigint REFERENCES core.accounts(account_id),
    payment_term_id                             integer NOT NULL REFERENCES core.payment_terms(payment_term_id),
    is_active                                   boolean NOT NULL DEFAULT(true),
    statement_reference                         national character varying(100) NOT NULL DEFAULT(''),
    audit_user_id                               integer NULL REFERENCES office.users(user_id),
    audit_ts                                    TIMESTAMP WITH TIME ZONE NULL 
                                                DEFAULT(NOW())    
    
);


INSERT INTO core.recurring_invoices
(
    recurring_invoice_id, 
    recurring_invoice_code, 
    recurring_invoice_name, 
    item_id, 
    recurring_frequency_id, 
    recurring_amount,
    auto_trigger_on_sales,
    payment_term_id,
    audit_user_id,
    audit_ts
)
SELECT
    recurring_invoice_id, 
    recurring_invoice_code, 
    recurring_invoice_name, 
    item_id, 
    COALESCE(recurring_frequency_id, 2), 
    recurring_amount,
    auto_trigger_on_sales,
    core.get_payment_term_id_by_payment_term_code('07-D'),
    audit_user_id,
    audit_ts
FROM core.recurring_invoices_temp;

SELECT setval
(
    pg_get_serial_sequence('core.recurring_invoices', 'recurring_invoice_id'), 
    (SELECT MAX(recurring_invoice_id) FROM core.recurring_invoices) + 1
); 


INSERT INTO core.recurring_invoice_setup
(
    recurring_invoice_setup_id,
    recurring_invoice_id,
    party_id,
    starts_from,
    ends_on,
    recurring_amount,
    payment_term_id,
    audit_user_id,
    audit_ts
)
SELECT
    recurring_invoice_setup_id,
    recurring_invoice_id,
    party_id,
    starts_from,
    ends_on,
    recurring_amount,
    payment_term_id,
    audit_user_id,
    audit_ts
FROM core.recurring_invoice_setup_temp;

UPDATE core.recurring_invoices
SET account_id = core.get_account_id_by_account_number('30100');

UPDATE core.recurring_invoice_setup
SET account_id = core.get_account_id_by_account_number('30100');

ALTER TABLE core.recurring_invoices
ALTER COLUMN account_id SET NOT NULL;

ALTER TABLE core.recurring_invoice_setup
ALTER COLUMN account_id SET NOT NULL;

SELECT setval
(
    pg_get_serial_sequence('core.recurring_invoice_setup', 'recurring_invoice_setup_id'), 
    (SELECT MAX(recurring_invoice_setup_id) FROM core.recurring_invoice_setup) + 1
); 

DROP TABLE IF EXISTS core.recurring_invoices_temp;
DROP TABLE IF EXISTS core.recurring_invoice_setup_temp;

DO
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT 1
        FROM   pg_attribute 
        WHERE  attrelid = 'core.bonus_slabs'::regclass
        AND    attname = 'account_id'
        AND    NOT attisdropped
    ) THEN
        ALTER TABLE core.bonus_slabs
        ADD COLUMN account_id bigint NOT NULL 
        REFERENCES core.accounts(account_id);
    END IF;

    IF NOT EXISTS
    (
        SELECT 1
        FROM   pg_attribute 
        WHERE  attrelid = 'core.bonus_slabs'::regclass
        AND    attname = 'statement_reference'
        AND    NOT attisdropped
    ) THEN
        ALTER TABLE core.bonus_slabs
        ADD COLUMN statement_reference national character varying(100) NOT NULL DEFAULT('');
    END IF;

    ALTER TABLE transactions.transaction_master
    ALTER COLUMN login_id DROP NOT NULL;
END
$$
LANGUAGE plpgsql;

DO
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT 1
        FROM   pg_attribute 
        WHERE  attrelid = 'transactions.stock_master'::regclass
        AND    attname = 'credit_settled'
        AND    NOT attisdropped
    ) THEN
        ALTER TABLE transactions.stock_master
        ADD COLUMN credit_settled boolean DEFAULT(false);

        CREATE INDEX stock_master_credit_settled_inx
        ON transactions.stock_master(credit_settled);
    END IF;

    IF NOT EXISTS
    (
        SELECT 1
        FROM   pg_attribute 
        WHERE  attrelid = 'transactions.transaction_master'::regclass
        AND    attname = 'cascading_tran_id'
        AND    NOT attisdropped
    ) THEN
        ALTER TABLE transactions.transaction_master
        ADD COLUMN cascading_tran_id bigint NULL
        REFERENCES transactions.transaction_master(transaction_master_id);

        CREATE INDEX transaction_master_cascading_tran_id_inx
        ON transactions.transaction_master(cascading_tran_id);
    END IF;
END
$$
LANGUAGE plpgsql;

UPDATE core.payment_terms
SET late_fee_posting_frequency_id = due_frequency_id
WHERE late_fee_posting_frequency_id IS NOT NULL
AND due_frequency_id IS NOT NULL
AND late_fee_posting_frequency_id < due_frequency_id;

ALTER TABLE core.payment_terms
DROP CONSTRAINT IF EXISTS payment_terms_late_fee_posting_frequency_id_chk;

ALTER TABLE core.payment_terms
ADD CONSTRAINT payment_terms_late_fee_posting_frequency_id_chk
CHECK(late_fee_posting_frequency_id IS NULL OR late_fee_posting_frequency_id >= due_frequency_id);

DO
$$
BEGIN
    IF EXISTS
    (
        SELECT TRUE
        FROM   pg_attribute 
        WHERE  attrelid = 'core.payment_terms'::regclass
        AND    attname = 'grace_peiod'
        AND    NOT attisdropped
    ) THEN
        ALTER TABLE core.payment_terms
        RENAME COLUMN grace_peiod TO grace_period;
    END IF;
END
$$
LANGUAGE plpgsql;

DO
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT 1
        FROM   pg_attribute 
        WHERE  attrelid = 'core.late_fee'::regclass
        AND    attname = 'account_id'
        AND    NOT attisdropped
    ) THEN
        ALTER TABLE core.late_fee
        ADD COLUMN account_id bigint NULL 
        REFERENCES core.accounts(account_id);
        
        UPDATE core.late_fee
        SET account_id = core.get_account_id_by_account_number('30300')
        WHERE account_id IS NULL;
        
        ALTER TABLE core.late_fee
        ALTER COLUMN account_id SET NOT NULL;
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
        AND    c.relname = 'late_fee'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE transactions.late_fee
        (
            transaction_master_id               bigint PRIMARY KEY REFERENCES transactions.transaction_master(transaction_master_id),
            party_id                            bigint NOT NULL REFERENCES core.parties(party_id),
            value_date                          date NOT NULL,
            late_fee_tran_id                    bigint NOT NULL REFERENCES transactions.transaction_master(transaction_master_id),
            amount                              public.money_strict
        );
    END IF;    
END
$$
LANGUAGE plpgsql;
 
DO
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT 1
        FROM   pg_attribute 
        WHERE  attrelid = 'office.offices'::regclass
        AND    attname = 'income_tax_rate'
        AND    NOT attisdropped
    ) THEN
        ALTER TABLE office.offices
        ADD COLUMN income_tax_rate public.decimal_strict2 NOT NULL
        DEFAULT(0);
    END IF;
END
$$
LANGUAGE plpgsql;

ALTER TABLE core.parties
ALTER COLUMN party_name SET NOT NULL;

DO
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT 1
        FROM   pg_attribute 
        WHERE  attrelid = 'policy.voucher_verification_policy'::regclass
        AND    attname = 'office_id'
        AND    NOT attisdropped
    ) THEN
        ALTER TABLE policy.voucher_verification_policy
        ADD COLUMN office_id integer
        REFERENCES office.offices(office_id);
    END IF;

    IF NOT EXISTS
    (
        SELECT 1
        FROM   pg_attribute 
        WHERE  attrelid = 'policy.auto_verification_policy'::regclass
        AND    attname = 'office_id'
        AND    NOT attisdropped
    ) THEN
        ALTER TABLE policy.auto_verification_policy
        ADD COLUMN office_id integer
        REFERENCES office.offices(office_id);
    END IF;
    
    UPDATE policy.voucher_verification_policy
    SET office_id = (SELECT office_id FROM office.offices LIMIT 1)
    WHERE office_id IS NULL;

    UPDATE policy.auto_verification_policy
    SET office_id = (SELECT office_id FROM office.offices LIMIT 1)
    WHERE office_id IS NULL;

    ALTER TABLE policy.voucher_verification_policy
    ALTER COLUMN office_id SET NOT NULL;

    ALTER TABLE policy.auto_verification_policy
    ALTER COLUMN office_id SET NOT NULL;    
END
$$
LANGUAGE plpgsql;


DO
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT 1
        FROM   pg_attribute 
        WHERE  attrelid = 'core.bank_accounts'::regclass
        AND    attname = 'is_merchant_account'
        AND    NOT attisdropped
    ) THEN
        ALTER TABLE core.bank_accounts
        ADD COLUMN is_merchant_account boolean NOT NULL DEFAULT(false);
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
        WHERE  n.nspname = 'core'
        AND    c.relname = 'card_types'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE core.card_types
        (
            card_type_id                    integer PRIMARY KEY,
            card_type_code                  national character varying(12) NOT NULL,
            card_type_name                  national character varying(100) NOT NULL
        );

        CREATE UNIQUE INDEX card_types_card_type_code_uix
        ON core.card_types(UPPER(card_type_code));

        CREATE UNIQUE INDEX card_types_card_type_name_uix
        ON core.card_types(UPPER(card_type_name));
    END IF;
    
    IF NOT EXISTS (
        SELECT 1 
        FROM   pg_catalog.pg_class c
        JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
        WHERE  n.nspname = 'core'
        AND    c.relname = 'payment_cards'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE core.payment_cards
        (
            payment_card_id                     SERIAL NOT NULL PRIMARY KEY,
            payment_card_code                   national character varying(12) NOT NULL,
            payment_card_name                   national character varying(100) NOT NULL,
            card_type_id                        integer NOT NULL REFERENCES core.card_types(card_type_id),            
            audit_user_id                       integer NULL REFERENCES office.users(user_id),            
            audit_ts                            TIMESTAMP WITH TIME ZONE NULL 
                                                DEFAULT(NOW())            
        );

        CREATE UNIQUE INDEX payment_cards_payment_card_code_uix
        ON core.payment_cards(UPPER(payment_card_code));

        CREATE UNIQUE INDEX payment_cards_payment_card_name_uix
        ON core.payment_cards(UPPER(payment_card_name));    
    END IF;

    IF NOT EXISTS (
        SELECT 1 
        FROM   pg_catalog.pg_class c
        JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
        WHERE  n.nspname = 'core'
        AND    c.relname = 'payment_cards'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE core.payment_cards
        (
            payment_card_id                     SERIAL NOT NULL PRIMARY KEY,
            payment_card_code                   national character varying(12) NOT NULL,
            payment_card_name                   national character varying(100) NOT NULL,
            card_type_id                        integer NOT NULL REFERENCES core.card_types(card_type_id),            
            audit_user_id                       integer NULL REFERENCES office.users(user_id),            
            audit_ts                            TIMESTAMP WITH TIME ZONE NULL 
                                                DEFAULT(NOW())            
        );

        CREATE UNIQUE INDEX payment_cards_payment_card_code_uix
        ON core.payment_cards(UPPER(payment_card_code));

        CREATE UNIQUE INDEX payment_cards_payment_card_name_uix
        ON core.payment_cards(UPPER(payment_card_name));    
    END IF;

    IF NOT EXISTS (
        SELECT 1 
        FROM   pg_catalog.pg_class c
        JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
        WHERE  n.nspname = 'core'
        AND    c.relname = 'merchant_fee_setup'
        AND    c.relkind = 'r'
    ) THEN
        CREATE TABLE core.merchant_fee_setup
        (
            merchant_fee_setup_id               SERIAL NOT NULL PRIMARY KEY,
            merchant_account_id                 bigint NOT NULL REFERENCES core.bank_accounts(account_id),
            payment_card_id                     integer NOT NULL REFERENCES core.payment_cards(payment_card_id),
            rate                                public.decimal_strict NOT NULL,
            customer_pays_fee                   boolean NOT NULL DEFAULT(false),
            account_id                          bigint NOT NULL REFERENCES core.accounts(account_id),
            statement_reference                 national character varying(128) NOT NULL DEFAULT(''),
            audit_user_id                       integer NULL REFERENCES office.users(user_id),            
            audit_ts                            TIMESTAMP WITH TIME ZONE NULL 
                                                DEFAULT(NOW())            
        );

        CREATE UNIQUE INDEX merchant_fee_setup_merchant_account_id_payment_card_id_uix
        ON core.merchant_fee_setup(merchant_account_id, payment_card_id);
    END IF;    
END
$$
LANGUAGE plpgsql;

ALTER TABLE core.item_cost_prices
DROP COLUMN IF EXISTS includes_tax;

ALTER TABLE core.items
DROP COLUMN IF EXISTS cost_price_includes_tax CASCADE;

DO
$$
BEGIN
    IF EXISTS
    (
        SELECT * FROM information_schema.check_constraints
        WHERE constraint_name = 'payment_terms_check'
    ) THEN
        ALTER TABLE core.payment_terms
        RENAME CONSTRAINT payment_terms_check TO payment_terms_chk;
    END IF;
END
$$
LANGUAGE plpgsql;


ALTER TABLE core.ageing_slabs
DROP CONSTRAINT IF EXISTS ageing_slabs_to_days_check;

DROP VIEW IF EXISTS core.ageing_slab_scrud_view;

ALTER TABLE core.ageing_slabs
ALTER COLUMN to_days TYPE public.integer_strict2;


DO
$$
BEGIN
    IF EXISTS
    (
        SELECT TRUE
        FROM   pg_attribute 
        WHERE  attrelid = 'localization.resources'::regclass
        AND    attname = 'path'
        AND    NOT attisdropped
    ) THEN
        ALTER TABLE localization.resources
        RENAME COLUMN path TO resource_class;
    END IF;
END
$$
LANGUAGE plpgsql;

DROP TABLE IF EXISTS localization.localized_resources CASCADE;

CREATE TABLE localization.localized_resources
(
    resource_id             integer NOT NULL REFERENCES localization.resources(resource_id),
    culture_code            text NOT NULL REFERENCES localization.cultures(culture_code),
    value                   text NOT NULL
);

CREATE UNIQUE INDEX localized_resources_culture_key_uix
ON localization.localized_resources
(resource_id, UPPER(culture_code));

DO
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT 1
        FROM   pg_attribute 
        WHERE  attrelid = 'transactions.transaction_master'::regclass
        AND    attname = 'book_date'
        AND    NOT attisdropped
    ) THEN
        ALTER TABLE transactions.transaction_master
        ADD COLUMN book_date date NOT NULL DEFAULT(NOW());
    END IF;
END
$$
LANGUAGE plpgsql;

DROP INDEX IF EXISTS core.compound_units_base_unit_id_uix;

CREATE UNIQUE INDEX compound_units_base_unit_id_uix
ON core.compound_units(base_unit_id);

DO
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT 1
        FROM   pg_attribute 
        WHERE  attrelid = 'core.compound_item_details'::regclass
        AND    attname = 'discount'
        AND    NOT attisdropped
    ) THEN
        ALTER TABLE core.compound_item_details
        ADD COLUMN discount public.money_strict2 NOT NULL DEFAULT(0);
    END IF;
END
$$
LANGUAGE plpgsql;


DROP INDEX IF EXISTS core.item_cost_price_id_uix;

CREATE UNIQUE INDEX item_cost_price_id_uix
ON core.item_cost_prices(item_id,unit_id);


DROP INDEX IF EXISTS core.item_selling_price_id_uix;

CREATE UNIQUE INDEX item_selling_price_id_uix
ON core.item_selling_prices(item_id,unit_id,price_type_id);

ALTER TABLE transactions.non_gl_stock_master
ALTER COLUMN party_id SET NOT NULL;

