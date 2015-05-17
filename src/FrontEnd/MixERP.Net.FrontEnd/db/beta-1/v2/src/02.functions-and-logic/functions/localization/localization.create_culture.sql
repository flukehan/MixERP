DROP FUNCTION IF EXISTS localization.create_culture
(
    _culture_code text,
    _culture_name text
);

CREATE FUNCTION localization.create_culture
(
    _culture_code text,
    _culture_name text
)
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT * FROM localization.cultures
        WHERE culture_code = _culture_code
    ) THEN
        INSERT INTO localization.cultures(culture_code, culture_name)
        SELECT _culture_code, _culture_name;
    END IF;
END
$$
LANGUAGE plpgsql;