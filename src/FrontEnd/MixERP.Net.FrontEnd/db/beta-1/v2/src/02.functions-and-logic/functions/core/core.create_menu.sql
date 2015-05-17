DROP FUNCTION IF EXISTS core.create_menu
(
    _menu_text          text,
    _url                text,
    _menu_code          text,
    _level              integer,
    _parent_menu_id     integer
);

CREATE FUNCTION core.create_menu
(
    _menu_text          text,
    _url                text,
    _menu_code          text,
    _level              integer,
    _parent_menu_id     integer
)
RETURNS void
VOLATILE
AS
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT * FROM core.menus
        WHERE menu_code = _menu_code
    ) THEN
        INSERT INTO core.menus(menu_text, url, menu_code, level, parent_menu_id)
        SELECT _menu_text, _url, _menu_code, _level, _parent_menu_id;
    END IF;

    UPDATE core.menus
    SET 
        menu_text       = _menu_text, 
        url             = _url, 
        level           = _level,
        parent_menu_id  = _parent_menu_id
    WHERE menu_code=_menu_code;    
END
$$
LANGUAGE plpgsql;
