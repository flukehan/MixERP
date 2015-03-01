INSERT INTO core.menus(menu_text, url, menu_code, level, parent_menu_id)
SELECT 'API Access Policy', '~/Modules/BackOffice/Policy/ApiAccess.mix', 'SAA', 2, core.get_menu_id('SPM');

--FRENCH
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'fr', 'API Stratégie d''accès';


--GERMAN
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'de', 'API-Richtlinien';

--RUSSIAN
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'ru', 'Политика доступа API';

--JAPANESE
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'ja', 'APIのアクセスポリシー';

--SPANISH
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'es', 'API Política de Acceso';

--DUTCH
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'nl', 'API Access Policy';

--SIMPLIFIED CHINESE
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'zh', 'API访问策略';

--PORTUGUESE
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'pt', 'Política de Acesso API';

--SWEDISH
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'sv', 'API Access Policy';

--MALAY
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'ms', 'Dasar Akses API';

--INDONESIAN
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'id', 'API Kebijakan Access';

--FILIPINO
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SAA'), 'fil', 'API Patakaran sa Pag-access';