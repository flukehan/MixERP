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


SELECT * FROM core.create_menu('Payment Cards', '~/Modules/Finance/Setup/PaymentCards.mix', 'PAC', 2, core.get_menu_id('FSM'));

--FRENCH
SELECT * FROM core.create_menu_locale(core.get_menu_id('PAC'), 'fr', 'Cartes de paiement');


--GERMAN
SELECT * FROM core.create_menu_locale(core.get_menu_id('PAC'), 'de', 'Zahlungskarten');

--RUSSIAN
SELECT * FROM core.create_menu_locale(core.get_menu_id('PAC'), 'ru', 'Платежные карты');

--JAPANESE
SELECT * FROM core.create_menu_locale(core.get_menu_id('PAC'), 'ja', '支払カード');

--SPANISH
SELECT * FROM core.create_menu_locale(core.get_menu_id('PAC'), 'es', 'Tarjetas de pago');

--DUTCH
SELECT * FROM core.create_menu_locale(core.get_menu_id('PAC'), 'nl', 'betaalkaarten');

--SIMPLIFIED CHINESE
SELECT * FROM core.create_menu_locale(core.get_menu_id('PAC'), 'zh', '支付卡');

--PORTUGUESE
SELECT * FROM core.create_menu_locale(core.get_menu_id('PAC'), 'pt', 'Cartões de pagamento');

--SWEDISH
SELECT * FROM core.create_menu_locale(core.get_menu_id('PAC'), 'sv', 'Betalkort');

--MALAY
SELECT * FROM core.create_menu_locale(core.get_menu_id('PAC'), 'ms', 'Kad Pembayaran');

--INDONESIAN
SELECT * FROM core.create_menu_locale(core.get_menu_id('PAC'), 'id', 'Kartu Pembayaran');

--FILIPINO
SELECT * FROM core.create_menu_locale(core.get_menu_id('PAC'), 'fil', 'Mga Card pagbabayad');

SELECT * FROM core.create_menu('Merchant Fee Setup', '~/Modules/Finance/Setup/MerchantFeeSetup.mix', 'MFS', 2, core.get_menu_id('FSM'));

--FRENCH
SELECT * FROM core.create_menu_locale(core.get_menu_id('MFS'), 'fr', 'Configuration de frais de Merchant');


--GERMAN
SELECT * FROM core.create_menu_locale(core.get_menu_id('MFS'), 'de', 'Händler Fee-Setup');

--RUSSIAN
SELECT * FROM core.create_menu_locale(core.get_menu_id('MFS'), 'ru', 'Торговец Стоимость установки');

--JAPANESE
SELECT * FROM core.create_menu_locale(core.get_menu_id('MFS'), 'ja', '加盟店手数料の設定');

--SPANISH
SELECT * FROM core.create_menu_locale(core.get_menu_id('MFS'), 'es', 'Configuración Fee Merchant');

--DUTCH
SELECT * FROM core.create_menu_locale(core.get_menu_id('MFS'), 'nl', 'Merchant Fee Setup');

--SIMPLIFIED CHINESE
SELECT * FROM core.create_menu_locale(core.get_menu_id('MFS'), 'zh', '商家安装费');

--PORTUGUESE
SELECT * FROM core.create_menu_locale(core.get_menu_id('MFS'), 'pt', 'Setup Fee Merchant');

--SWEDISH
SELECT * FROM core.create_menu_locale(core.get_menu_id('MFS'), 'sv', 'Merchant Fee Setup');

--MALAY
SELECT * FROM core.create_menu_locale(core.get_menu_id('MFS'), 'ms', 'Bayaran Merchant Persediaan');

--INDONESIAN
SELECT * FROM core.create_menu_locale(core.get_menu_id('MFS'), 'id', 'Merchant Fee Pengaturan');

--FILIPINO
SELECT * FROM core.create_menu_locale(core.get_menu_id('MFS'), 'fil', 'Setup Bayarin sa Merchant');

SELECT * FROM core.create_menu('Report Writer', '~/Modules/BackOffice/Admin/ReportWriter.mix', 'RW', 2, core.get_menu_id('SAT'));

--FRENCH
SELECT * FROM core.create_menu_locale(core.get_menu_id('RW'), 'fr', 'Report Writer');

--GERMAN
SELECT * FROM core.create_menu_locale(core.get_menu_id('RW'), 'de', 'Report Writer');

--RUSSIAN
SELECT * FROM core.create_menu_locale(core.get_menu_id('RW'), 'ru', 'генератор отчетов');

--JAPANESE
SELECT * FROM core.create_menu_locale(core.get_menu_id('RW'), 'ja', '報告書作成');

--SPANISH
SELECT * FROM core.create_menu_locale(core.get_menu_id('RW'), 'es', 'Report Writer');

--DUTCH
SELECT * FROM core.create_menu_locale(core.get_menu_id('RW'), 'nl', 'Report Writer');

--SIMPLIFIED CHINESE
SELECT * FROM core.create_menu_locale(core.get_menu_id('RW'), 'zh', '报表生成器');

--PORTUGUESE
SELECT * FROM core.create_menu_locale(core.get_menu_id('RW'), 'pt', 'Report Writer');

--SWEDISH
SELECT * FROM core.create_menu_locale(core.get_menu_id('RW'), 'sv', 'Report Writer');

--MALAY
SELECT * FROM core.create_menu_locale(core.get_menu_id('RW'), 'ms', 'Laporan Penulis');

--INDONESIAN
SELECT * FROM core.create_menu_locale(core.get_menu_id('RW'), 'id', 'laporan Penulis');

--FILIPINO
SELECT * FROM core.create_menu_locale(core.get_menu_id('RW'), 'fil', 'Report Writer');
