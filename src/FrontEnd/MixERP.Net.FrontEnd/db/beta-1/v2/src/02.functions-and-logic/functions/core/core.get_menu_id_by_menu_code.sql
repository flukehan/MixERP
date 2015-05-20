DROP FUNCTION IF EXISTS core.get_menu_id_by_menu_code(national character varying(250));

CREATE FUNCTION core.get_menu_id_by_menu_code(_menu_code national character varying(250))
RETURNS integer
STABLE
AS
$$
BEGIN
    RETURN menu_id
    FROM core.menus
    WHERE menu_code=$1;
END
$$
LANGUAGE plpgsql;
