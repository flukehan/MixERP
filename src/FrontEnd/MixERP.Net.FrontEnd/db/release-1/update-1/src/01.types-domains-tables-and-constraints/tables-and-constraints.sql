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
