INSERT INTO policy.auto_verification_policy
SELECT 2, true, 0, true, 0, true, 0, '1-1-2010', '1-1-2020', true;

INSERT INTO core.config
SELECT 1, 'Inventory System' UNION ALL
SELECT 2, 'COGS Calculation Method';

INSERT INTO office.configuration(config_id, office_id, configuration, configuration_details)
SELECT 1, office_id, 'Perpetual', ''
FROM office.offices
WHERE parent_office_id IS NOT NULL;


INSERT INTO office.configuration(config_id, office_id, configuration, configuration_details)
SELECT 2, office_id, 'LIFO', ''
FROM office.offices
WHERE parent_office_id IS NOT NULL;



