SELECT * FROM core.create_menu('API Access Policy', '~/Modules/BackOffice/Policy/ApiAccess.mix', 'SAA', 2, core.get_menu_id('SPM'));

--FRENCH
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'fr', 'API Stratégie d''accès');


--GERMAN
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'de', 'API-Richtlinien');

--RUSSIAN
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'ru', 'Политика доступа API');

--JAPANESE
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'ja', 'APIのアクセスポリシー');

--SPANISH
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'es', 'API Política de Acceso');

--DUTCH
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'nl', 'API Access Policy');

--SIMPLIFIED CHINESE
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'zh', 'API访问策略');

--PORTUGUESE
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'pt', 'Política de Acesso API');

--SWEDISH
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'sv', 'API Access Policy');

--MALAY
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'ms', 'Dasar Akses API');

--INDONESIAN
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'id', 'API Kebijakan Access');

--FILIPINO
SELECT * FROM core.create_menu_locale(core.get_menu_id('SAA'), 'fil', 'API Patakaran sa Pag-access');