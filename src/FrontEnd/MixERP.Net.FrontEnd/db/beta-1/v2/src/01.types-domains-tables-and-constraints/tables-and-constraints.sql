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
        ON policy.api_access_policy(user_id, poco_type_name, http_action_code, valid_till);
    
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
    recurring_frequency_id, 
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
        REFERENCES core.accounts(account_id)
        CONSTRAINT bonus_slab_account_id_df
        DEFAULT(core.get_account_id_by_account_number('40230'));
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
        ADD COLUMN account_id bigint NOT NULL 
        REFERENCES core.accounts(account_id)
        CONSTRAINT late_fee_account_id_df
        DEFAULT(core.get_account_id_by_account_number('30300'));
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

