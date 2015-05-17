DROP FUNCTION IF EXISTS core.create_menu_locale
(
    _menu_id            integer,
    _culture            text,
    _menu_text          text
);

CREATE FUNCTION core.create_menu_locale
(
    _menu_id            integer,
    _culture            text,
    _menu_text          text
)
RETURNS void
VOLATILE
AS
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT * FROM core.menu_locale
        WHERE menu_id = _menu_id
        AND culture = _culture
    ) THEN
        INSERT INTO core.menu_locale(menu_id, culture, menu_text)
        SELECT _menu_id, _culture, _menu_text;
    END IF;

    UPDATE core.menu_locale
    SET
        menu_text       = _menu_text
    WHERE menu_id = _menu_id
    AND culture = _culture;
END
$$
LANGUAGE plpgsql;
