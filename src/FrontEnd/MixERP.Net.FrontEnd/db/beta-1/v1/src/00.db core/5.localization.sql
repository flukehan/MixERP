CREATE TABLE localization.resources
(
    resource_id         SERIAL PRIMARY KEY,
    path                text,
    key                 text,
    value               text
);

CREATE UNIQUE INDEX resources_path_key_uix
ON localization.resources(UPPER(path), UPPER(key));

CREATE INDEX resources_path_key_inx
ON localization.resources(path, key);

CREATE INDEX resources_path_inx
ON localization.resources(path);

CREATE INDEX resources_key_inx
ON localization.resources(key);

CREATE TABLE localization.cultures
(
    culture_code        text PRIMARY KEY,
    culture_name        text
);

INSERT INTO localization.cultures
SELECT 'de-DE',     'German (Germany)'              UNION ALL
SELECT 'en-GB',     'English (United Kingdom)'      UNION ALL
SELECT 'es-ES',     'Spanish (Spain)'               UNION ALL
SELECT 'fil-PH',    'Filipino (Philippines)'        UNION ALL
SELECT 'fr-FR',     'French (France)'               UNION ALL
SELECT 'id-ID',     'Indonesian (Indonesia)'        UNION ALL
SELECT 'ja-JP',     'Japanese (Japan)'              UNION ALL
SELECT 'ms-MY',     'Malay (Malaysia)'              UNION ALL
SELECT 'nl-NL',     'Dutch (Netherlands)'           UNION ALL
SELECT 'pt-PT',     'Portuguese (Portugal)'         UNION ALL
SELECT 'ru-RU',     'Russian (Russia)'              UNION ALL
SELECT 'sv-SE',     'Swedish (Sweden)'              UNION ALL
SELECT 'zh-CN',     'Simplified Chinese (China)';


CREATE TABLE localization.localized_resources
(
    id                  SERIAL PRIMARY KEY,
    culture_code        text REFERENCES localization.cultures,
    key                 text,
    value               text
);

CREATE UNIQUE INDEX localized_resources_culture_key_uix
ON localization.localized_resources(UPPER(culture_code), UPPER(key));

CREATE FUNCTION localization.add_resource
(
    path                text,
    key                 text,
    value               text
)
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM localization.resources WHERE localization.resources.path=$1 AND localization.resources.key=$2) THEN
        INSERT INTO localization.resources(path, key, value)
        SELECT $1, $2, $3;
    END IF;
END
$$
LANGUAGE plpgsql;

CREATE FUNCTION localization.get_localization_table
(
    culture_code        text
)
RETURNS TABLE
(
    row_number          bigint,
    key                 text,
    invariant_resource  text,
    value               text
)
AS
$$
BEGIN   
    CREATE TEMPORARY TABLE t
    (
        key                 text,
        invariant_resource  text,
        value               text
    );
    INSERT INTO t(key, invariant_resource, value)
    SELECT
        DISTINCT localization.resources.key,
        localization.resources.value as invariant_resource,
        localization.localized_resources.value
    FROM localization.resources
    LEFT JOIN localization.localized_resources
    ON localization.resources.key = localization.localized_resources.key
    AND localization.localized_resources.culture_code = $1;

    RETURN QUERY 
    SELECT 
        row_number() OVER(ORDER BY t.key ~ '^[[:upper:]][^[:upper:]]' DESC, t.key),
        t.key,
        t.invariant_resource,
        t.value
    FROM t
    ORDER BY t.key ~ '^[[:upper:]][^[:upper:]]' DESC, t.key;
END
$$
LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION localization.add_localized_resource(text, text, text)
RETURNS void AS
$$
BEGIN
    IF EXISTS
    (
        SELECT 1 FROM localization.localized_resources 
        WHERE localization.localized_resources.culture_code=$1 
        AND localization.localized_resources.key=$2
    ) THEN
        UPDATE localization.localized_resources
        SET value=$3
        WHERE localization.localized_resources.culture_code=$1 AND key=$2;

        RETURN;
    END IF;

    INSERT INTO localization.localized_resources(culture_code, key, value)
    SELECT $1, $2, $3;
END
$$
LANGUAGE plpgsql VOLATILE
COST 100;

CREATE VIEW localization.localized_resources_view
AS
SELECT
    REPLACE(localization.resources.path, '.resx', '') || '.' || localization.localized_resources.culture_code || '.resx' AS resource,
    localization.resources.key,
    localization.localized_resources.culture_code,
    localization.localized_resources.value
FROM localization.resources
INNER JOIN localization.localized_resources
ON localization.localized_resources.key = localization.resources.key
ORDER BY PATH;