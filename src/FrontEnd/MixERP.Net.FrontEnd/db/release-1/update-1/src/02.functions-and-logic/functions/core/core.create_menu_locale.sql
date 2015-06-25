DROP FUNCTION IF EXISTS core.create_menu_locale
(
    _menu_code          text,
    _culture            text,
    _menu_text          text
);

CREATE FUNCTION core.create_menu_locale
(
    _menu_code          text,
    _culture            text,
    _menu_text          text
)
RETURNS void
VOLATILE
AS
$$
    DECLARE _menu_id    integer = core.get_menu_id_by_menu_code(_menu_code);
BEGIN
    PERFORM core.create_menu_locale(_menu_id, _culture, _menu_text);
END
$$
LANGUAGE plpgsql;
