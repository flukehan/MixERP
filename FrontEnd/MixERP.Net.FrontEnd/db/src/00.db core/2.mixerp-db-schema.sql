DROP SCHEMA IF EXISTS audit CASCADE;
DROP SCHEMA IF EXISTS core CASCADE;
DROP SCHEMA IF EXISTS office CASCADE;
DROP SCHEMA IF EXISTS policy CASCADE;
DROP SCHEMA IF EXISTS transactions CASCADE;
DROP SCHEMA IF EXISTS crm CASCADE;
DROP SCHEMA IF EXISTS mrp CASCADE;


CREATE SCHEMA audit;
COMMENT ON SCHEMA audit IS 'Contains audit related objects';

CREATE SCHEMA core;
COMMENT ON SCHEMA core IS 'Contains objects related to the core module. The core module is the default MixERP schema.';

CREATE SCHEMA office;
COMMENT ON SCHEMA office IS 'Contains objects related to office.';

CREATE SCHEMA policy;
COMMENT ON SCHEMA office IS 'Contains objects related to MixERP''s policy engine and workflow.';

CREATE SCHEMA transactions;
COMMENT ON SCHEMA office IS 'Contains objects related to transaction posting.';

CREATE SCHEMA crm;
COMMENT ON SCHEMA office IS 'Contains objects related to customer relationship management.';

CREATE SCHEMA mrp;
COMMENT ON SCHEMA office IS 'Contains objects related to material resource planning.';
