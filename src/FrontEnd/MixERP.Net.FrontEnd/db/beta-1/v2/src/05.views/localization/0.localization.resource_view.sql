DROP VIEW IF EXISTS localization.resource_view;

CREATE VIEW localization.resource_view
AS
SELECT 
    resource_class, '' as culture, key, value
FROM localization.resources
UNION ALL
SELECT resource_class, culture_code, key, localization.localized_resources.value 
FROM localization.localized_resources
INNER JOIN localization.resources
ON localization.localized_resources.resource_id = localization.resources.resource_id;