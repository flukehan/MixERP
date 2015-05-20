DROP FUNCTION IF EXISTS localization.add_localized_resource
(
    _culture_code    text,
    _key             text,
    _value           text
);

DROP FUNCTION IF EXISTS localization.add_localized_resource
(
    _resource_class  text,
    _culture_code    text,
    _key             text,
    _value           text
);

CREATE FUNCTION localization.add_localized_resource
(
    _resource_class  text,
    _culture_code    text,
    _key             text,
    _value           text
)
RETURNS void 
VOLATILE
AS
$$
    DECLARE _resource_id    integer;
BEGIN
    IF(COALESCE(_culture_code, '') = '') THEN
        PERFORM localization.add_resource(_resource_class, _key, _value);
        RETURN;
    END IF;

    SELECT resource_id INTO _resource_id
    FROM localization.resources
    WHERE resource_class = _resource_class
    AND key = _key;

    IF EXISTS
    (
        SELECT 1 FROM localization.localized_resources 
        WHERE localization.localized_resources.resource_id=_resource_id
        AND culture_code = _culture_code
    ) THEN
        UPDATE localization.localized_resources
        SET value=_value
        WHERE localization.localized_resources.resource_id=_resource_id
        AND culture_code = _culture_code;

        RETURN;
    END IF;

    INSERT INTO localization.localized_resources(resource_id, culture_code, value)
    SELECT _resource_id, _culture_code, _value;
END
$$
LANGUAGE plpgsql;