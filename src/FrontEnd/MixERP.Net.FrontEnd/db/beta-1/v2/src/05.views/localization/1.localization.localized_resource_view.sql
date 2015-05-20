DROP VIEW IF EXISTS localization.localized_resource_view;

CREATE VIEW localization.localized_resource_view
AS
SELECT
    resource_class || 
    CASE WHEN COALESCE(culture, '') = '' THEN '' ELSE '.' || culture END 
    || '.' || key as key, value 
FROM localization.resource_view;