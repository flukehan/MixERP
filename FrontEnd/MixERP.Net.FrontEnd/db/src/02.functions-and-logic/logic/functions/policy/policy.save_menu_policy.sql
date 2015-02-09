DROP FUNCTION IF EXISTS policy.save_menu_policy
(
    _user_id        integer,
    _office_id      integer,
    _menu_ids       int[]
);

CREATE FUNCTION policy.save_menu_policy
(
    _user_id        integer,
    _office_id      integer,
    _menu_ids       int[]
)
RETURNS void
VOLATILE AS
$$
BEGIN
    DELETE FROM policy.menu_access
    WHERE NOT menu_id = ANY(_menu_ids)
    AND user_id = _user_id
    AND office_id = _office_id;

    WITH menus
    AS
    (
        SELECT explode_array(_menu_ids) AS _menu_id
    )
    
    INSERT INTO policy.menu_access(user_id, office_id, menu_id)
    SELECT _user_id, _office_id, _menu_id
    FROM menus
    WHERE menu_id NOT IN
    (
        SELECT _menu_id
        FROM menus
    )
    AND user_id = _user_id
    AND office_id = _office_id;

    RETURN;
END
$$
LANGUAGE plpgsql;
