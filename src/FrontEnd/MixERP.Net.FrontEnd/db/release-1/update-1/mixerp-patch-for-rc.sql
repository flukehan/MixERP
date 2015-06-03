-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/release-1/update-1/src/01.types-domains-tables-and-constraints/tables-and-constraints.sql --<--<--
ALTER TABLE policy.auto_verification_policy
DROP CONSTRAINT IF EXISTS auto_verification_policy_pkey;

ALTER TABLE policy.auto_verification_policy
ADD PRIMARY KEY(user_id, office_id);

DO
$$
BEGIN
    IF EXISTS
    (
        SELECT 1
        FROM   pg_attribute 
        WHERE  attrelid = 'core.parties'::regclass
        AND    attname = 'party_name'
        AND    NOT attisdropped
    ) THEN
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


-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/release-1/update-1/src/02.functions-and-logic/functions/logic/core/core.get_party_code.sql --<--<--
CREATE OR REPLACE FUNCTION core.get_party_code
(
    text, --First Name
    text, --Middle Name
    text  --Last Name
)
RETURNS text AS
$$
    DECLARE _party_code TEXT;
BEGIN
    SELECT INTO 
        _party_code 
            party_code
    FROM
        core.parties
    WHERE
        party_code LIKE 
            UPPER(left($1,2) ||
            CASE
                WHEN $2 IS NULL or $2 = '' 
                THEN left($3,3)
            ELSE 
                left($2,1) || left($3,2)
            END 
            || '%')
    ORDER BY party_code desc
    LIMIT 1;

    _party_code :=
                    UPPER
                    (
                        left($1,2)||
                        CASE
                            WHEN $2 IS NULL or $2 = '' 
                            THEN left($3,3)
                        ELSE 
                            left($2,1)||left($3,2)
                        END
                    ) 
                    || '-' ||
                    CASE
                        WHEN _party_code IS NULL 
                        THEN '0001'
                    ELSE 
                        to_char(right(_party_code,4)::national character varying::int +1,'FM0000')
                    END;
    RETURN _party_code;
END;
$$
LANGUAGE 'plpgsql';

CREATE OR REPLACE FUNCTION core.get_party_code
(
    text --Company Name
)
RETURNS text AS
$$
    DECLARE _party_code TEXT;
BEGIN
    SELECT INTO 
        _party_code 
            party_code
    FROM
        core.parties
    WHERE
        party_code LIKE 
            UPPER(left($1,5) || '%')
    ORDER BY party_code desc
    LIMIT 1;

    
    _party_code :=
                    UPPER
                    (left($1,5)) 
                    || '-' ||
                    CASE
                        WHEN _party_code IS NULL 
                        THEN '0001'
                    ELSE 
                        to_char(right(_party_code, 4)::national character varying::int +1,'FM0000')
                    END;
    RETURN _party_code;
END;
$$
LANGUAGE 'plpgsql';

-->-->-- C:/Users/nirvan/Desktop/mixerp/0. GitHub/src/FrontEnd/MixERP.Net.FrontEnd/db/release-1/update-1/src/10.triggers/core/core.party_after_insert_trigger.sql --<--<--
DROP FUNCTION IF EXISTS core.party_after_insert_trigger() CASCADE;

CREATE FUNCTION core.party_after_insert_trigger()
RETURNS TRIGGER
AS
$$
    DECLARE _parent_account_id bigint;
    DECLARE _party_code text;
    DECLARE _account_id bigint;
BEGIN
    IF(COALESCE(NEW.company_name, '') = '') THEN
        _party_code             := core.get_party_code(NEW.first_name, NEW.middle_name, NEW.last_name);
    ELSE
        _party_code             := core.get_party_code(NEW.company_name);
    END IF;
    
    _parent_account_id      := core.get_account_id_by_party_type_id(NEW.party_type_id);

    IF(COALESCE(NEW.party_name, '') = '') THEN
        NEW.party_name := REPLACE(TRIM(COALESCE(NEW.last_name, '') || ', ' || NEW.first_name || ' ' || COALESCE(NEW.middle_name, '')), ' ', '');
    END IF;

    --Create a new account
    IF(NEW.account_id IS NULL) THEN
        INSERT INTO core.accounts(account_master_id, account_number, currency_code, account_name, parent_account_id)
        SELECT core.get_account_master_id_by_account_id(_parent_account_id), _party_code, NEW.currency_code, _party_code || ' (' || NEW.party_name || ')', _parent_account_id
        RETURNING account_id INTO _account_id;
    
        UPDATE core.parties
        SET 
            account_id=_account_id, 
            party_code=_party_code
        WHERE core.parties.party_id=NEW.party_id;

        RETURN NEW;
    END IF;

    UPDATE core.parties
    SET 
        party_code=_party_code
    WHERE core.parties.party_id=NEW.party_id;

    RETURN NEW;
END
$$
LANGUAGE plpgsql;

CREATE TRIGGER party_after_insert_trigger
AFTER INSERT
ON core.parties
FOR EACH ROW EXECUTE PROCEDURE core.party_after_insert_trigger();
