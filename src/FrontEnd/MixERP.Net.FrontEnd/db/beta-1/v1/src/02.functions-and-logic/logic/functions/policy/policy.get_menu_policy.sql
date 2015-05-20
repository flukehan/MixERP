DROP FUNCTION IF EXISTS policy.get_menu_policy
(
    _user_id        integer,
    _office_id      integer,
    _culture        text
);

CREATE FUNCTION policy.get_menu_policy
(
    _user_id        integer,
    _office_id      integer,
    _culture        text
)
RETURNS TABLE
(
    row_number      bigint,
    access          boolean,
    menu_id         integer,
    menu_code       text,
    menu_text       text,
    url             text
)
STABLE AS
$$
    DECLARE culture_exists boolean = false;
BEGIN
    IF EXISTS(SELECT * FROM core.menu_locale WHERE culture=$3) THEN
        culture_exists := true;
    END IF;


    IF culture_exists THEN
        RETURN QUERY 
        SELECT
            row_number() OVER(ORDER BY core.menus.menu_id),
            CASE WHEN policy.menu_access.access_id IS NOT NULL THEN true ELSE false END as access,
            core.menus.menu_id,
            core.menus.menu_code::text, 
            core.menu_locale.menu_text::text, 
            core.menus.url::text
        FROM core.menus
        INNER JOIN core.menu_locale
        ON core.menus.menu_id = core.menu_locale.menu_id
        LEFT JOIN policy.menu_access
        ON core.menus.menu_id = policy.menu_access.menu_id
        WHERE policy.menu_access.user_id = $1
        AND policy.menu_access.office_id = $2
        AND core.menu_locale.culture = $3
        ORDER BY core.menus.menu_id;

        RETURN;
    END IF;
    
    RETURN QUERY
    SELECT
        row_number() OVER(ORDER BY core.menus.menu_id),
        CASE WHEN policy.menu_access.access_id IS NOT NULL THEN true ELSE false END as access,
        core.menus.menu_id,
        core.menus.menu_code::text, 
        core.menus.menu_text::text, 
        core.menus.url::text
    FROM core.menus
    LEFT JOIN policy.menu_access
    ON core.menus.menu_id = policy.menu_access.menu_id
    AND policy.menu_access.user_id = $1
    AND policy.menu_access.office_id = $2
    ORDER BY core.menus.menu_id;

    RETURN;
END
$$
LANGUAGE plpgsql;
