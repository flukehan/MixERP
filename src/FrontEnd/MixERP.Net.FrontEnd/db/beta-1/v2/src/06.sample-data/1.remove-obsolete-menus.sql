DELETE FROM core.menu_locale
WHERE menu_id = core.get_menu_id_by_menu_code('TRA');

DELETE FROM policy.menu_access
WHERE menu_id = core.get_menu_id_by_menu_code('TRA');

DELETE FROM core.menus
WHERE menu_code = 'TRA';
