--This table should not be localized
INSERT INTO core.config
SELECT 1, 'Inventory System' UNION ALL
SELECT 2, 'COGS Calculation Method';

--This table should not be localized
INSERT INTO office.configuration(config_id, office_id, value, configuration_details)
SELECT 1, office_id, 'Perpetual', ''
FROM office.offices
WHERE parent_office_id IS NOT NULL;

--This table should not be localized
INSERT INTO office.configuration(config_id, office_id, value, configuration_details)
SELECT 2, office_id, 'LIFO', ''
FROM office.offices
WHERE parent_office_id IS NOT NULL;



