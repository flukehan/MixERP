DROP FUNCTION IF EXISTS localization.get_localization_table(text);

CREATE FUNCTION localization.get_localization_table
(
    culture_code        text
)
RETURNS TABLE
(
    id                  bigint,
    resource_class      text,
    key                 text,
    original            text,
    translated          text
)
AS
$$
BEGIN   
    CREATE TEMPORARY TABLE t
    (
        resource_id         integer,
        resource_class      text,
        key                 text,
        original            text,
        translated          text
    ) ON COMMIT DROP;
    
    INSERT INTO t
    SELECT
        localization.resources.resource_id, 
        localization.resources.resource_class, 
        localization.resources.key, 
        localization.resources.value,
        ''
    FROM localization.resources;

    UPDATE t
    SET translated = localization.localized_resources.value
    FROM localization.localized_resources
    WHERE t.resource_id = localization.localized_resources.resource_id
    AND localization.localized_resources.culture_code=$1;

    RETURN QUERY
    SELECT
        row_number() OVER(ORDER BY t.resource_class, t.key),
        t.resource_class, 
        t.key, 
        t.original,
        t.translated
    FROM t;
END
$$
LANGUAGE plpgsql;
