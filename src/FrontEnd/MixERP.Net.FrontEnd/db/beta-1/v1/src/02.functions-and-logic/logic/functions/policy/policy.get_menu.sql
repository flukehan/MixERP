DROP FUNCTION IF EXISTS policy.get_menu
(
    _user_id    integer, 
    _office_id  integer, 
    _culture_   text
);

CREATE FUNCTION policy.get_menu
(
    _user_id    integer, 
    _office_id  integer, 
    _culture_   text
)
RETURNS TABLE
(
    menu_id         integer,
    menu_text       national character varying(250),
    url             national character varying(250),
    menu_code       character varying(12),
    level           smallint,
    parent_menu_id  integer
)
AS
$$
    DECLARE culture_exists boolean = false;
BEGIN    
    IF EXISTS(SELECT * FROM core.menu_locale WHERE culture=$3) THEN
        culture_exists := true;
    END IF;

    IF(NOT culture_exists) THEN
        IF EXISTS(SELECT * FROM core.menu_locale WHERE culture=split_part($3,'-', 1)) THEN
            $3 := split_part($3,'-', 1);
            culture_exists := true;
        END IF;
    END IF;

    IF culture_exists THEN
        RETURN QUERY 
        SELECT
            core.menus.menu_id,
            core.menu_locale.menu_text,
            core.menus.url,
            core.menus.menu_code,
            core.menus.level,
            core.menus.parent_menu_id   
        FROM core.menus
        INNER JOIN policy.menu_access
        ON core.menus.menu_id = policy.menu_access.menu_id
        INNER JOIN core.menu_locale
        ON core.menus.menu_id = core.menu_locale.menu_id
        WHERE policy.menu_access.user_id=$1
        AND policy.menu_access.office_id=$2
        AND core.menu_locale.culture=$3;
    ELSE
        RETURN QUERY 
        SELECT
            core.menus.menu_id,
            core.menus.menu_text,
            core.menus.url,
            core.menus.menu_code,
            core.menus.level,
            core.menus.parent_menu_id   
        FROM core.menus
        INNER JOIN policy.menu_access
        ON core.menus.menu_id = policy.menu_access.menu_id
        WHERE policy.menu_access.user_id=$1
        AND policy.menu_access.office_id=$2;
    END IF;

END
$$
LANGUAGE plpgsql;

--SELECT * FROM policy.get_menu(2, 2, 'de-DE');