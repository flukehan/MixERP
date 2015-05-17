DROP FUNCTION IF EXISTS localization.add_resource
(
    resource_class  text,
    key             text,
    value           text
);

CREATE OR REPLACE FUNCTION localization.add_resource
(
    resource_class  text,
    key             text,
    value           text
)
RETURNS void 
VOLATILE
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM localization.resources WHERE localization.resources.resource_class=$1 AND localization.resources.key=$2) THEN
        INSERT INTO localization.resources(resource_class, key, value)
        SELECT $1, $2, $3;
    END IF;
END
$$
LANGUAGE plpgsql;