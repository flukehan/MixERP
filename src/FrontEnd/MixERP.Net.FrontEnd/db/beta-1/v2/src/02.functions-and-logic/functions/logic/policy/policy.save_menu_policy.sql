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
    WHERE policy.menu_access.menu_id NOT IN(SELECT * from explode_array(_menu_ids))
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
    WHERE _menu_id NOT IN
    (
        SELECT menu_id
        FROM policy.menu_access
        WHERE policy.menu_access.user_id = _user_id
        AND policy.menu_access.office_id = _office_id
    );

    RETURN;
END
$$
LANGUAGE plpgsql;

--SELECT * FROM policy.save_menu_policy(2, 2, string_to_array('106, 107', ',')::varchar[]::int[]);