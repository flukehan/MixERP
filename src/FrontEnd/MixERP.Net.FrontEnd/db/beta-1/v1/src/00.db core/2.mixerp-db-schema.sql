DROP SCHEMA IF EXISTS audit CASCADE;
DROP SCHEMA IF EXISTS core CASCADE;
DROP SCHEMA IF EXISTS localization CASCADE;
DROP SCHEMA IF EXISTS mrp CASCADE;
DROP SCHEMA IF EXISTS office CASCADE;
DROP SCHEMA IF EXISTS policy CASCADE;
DROP SCHEMA IF EXISTS transactions CASCADE;
DROP SCHEMA IF EXISTS crm CASCADE;

CREATE SCHEMA audit AUTHORIZATION mix_erp;
COMMENT ON SCHEMA audit IS 'Contains audit-related objects.';

CREATE SCHEMA core AUTHORIZATION mix_erp;
COMMENT ON SCHEMA core IS 'Contains objects related to the core module. The core module is the default MixERP schema.';

CREATE SCHEMA crm AUTHORIZATION mix_erp;
COMMENT ON SCHEMA crm IS 'Contains objects related to customer relationship management.';

CREATE SCHEMA localization AUTHORIZATION mix_erp;
COMMENT ON SCHEMA localization IS 'Contains objects related to localizing MixERP.';

CREATE SCHEMA mrp AUTHORIZATION mix_erp;
COMMENT ON SCHEMA mrp IS 'Contains objects related to material resource planning.';

CREATE SCHEMA office AUTHORIZATION mix_erp;
COMMENT ON SCHEMA office IS 'Contains objects related to office.';

CREATE SCHEMA policy AUTHORIZATION mix_erp;
COMMENT ON SCHEMA policy IS 'Contains objects related to MixERP''s policy engine and workflow.';

CREATE SCHEMA transactions AUTHORIZATION mix_erp;
COMMENT ON SCHEMA transactions IS 'Contains objects related to transaction posting.';