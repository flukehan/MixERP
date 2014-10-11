CREATE FUNCTION core.get_menu_id(menu_code text)
RETURNS INTEGER
AS
$$
BEGIN
    RETURN
    (
        SELECT core.menus.menu_id
        FROM core.menus
        WHERE core.menus.menu_code=$1
    );
END
$$
LANGUAGE plpgsql;
