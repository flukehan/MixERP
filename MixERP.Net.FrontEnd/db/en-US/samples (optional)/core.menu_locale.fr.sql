INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('DB'), 'fr', 'tableau de bord' UNION ALL
SELECT core.get_menu_id('SA'), 'fr', 'ventes' UNION ALL
SELECT core.get_menu_id('PU'), 'fr', 'acheter' UNION ALL
SELECT core.get_menu_id('ITM'), 'fr', 'Produits et Articles' UNION ALL
SELECT core.get_menu_id('FI'), 'fr', 'Finances' UNION ALL
SELECT core.get_menu_id('MF'), 'fr', 'fabrication' UNION ALL
SELECT core.get_menu_id('CRM'), 'fr', 'CRM' UNION ALL
SELECT core.get_menu_id('SE'), 'fr', 'Paramètres de configuration' UNION ALL
SELECT core.get_menu_id('POS'), 'fr', 'POS' UNION ALL
SELECT core.get_menu_id('SAQ'), 'fr', 'Ventes & Devis' UNION ALL
SELECT core.get_menu_id('DRS'), 'fr', 'vente directe' UNION ALL
SELECT core.get_menu_id('SQ'), 'fr', 'Offre de vente' UNION ALL
SELECT core.get_menu_id('SO'), 'fr', 'commande' UNION ALL
SELECT core.get_menu_id('SD'), 'fr', 'Livraison Sans Commande' UNION ALL
SELECT core.get_menu_id('ISD'), 'fr', 'Facture pour les ventes Livraison' UNION ALL
SELECT core.get_menu_id('RFC'), 'fr', 'Réception de la clientèle' UNION ALL
SELECT core.get_menu_id('SR'), 'fr', 'ventes Retour' UNION ALL
SELECT core.get_menu_id('SSM'), 'fr', 'Configuration et Maintenance' UNION ALL
SELECT core.get_menu_id('ABS'), 'fr', 'Bonus dalle pour les agents' UNION ALL
SELECT core.get_menu_id('BSD'), 'fr', 'Bonus Slab Détails' UNION ALL
SELECT core.get_menu_id('SSA'), 'fr', 'Agents de vente' UNION ALL
SELECT core.get_menu_id('BSA'), 'fr', 'Bonus dalle Affectation' UNION ALL
SELECT core.get_menu_id('SAR'), 'fr', 'Rapports de vente' UNION ALL
SELECT core.get_menu_id('SAR-SVSI'), 'fr', 'Voir la facture de vente' UNION ALL
SELECT core.get_menu_id('CM'), 'fr', 'Gestion de la Caisse' UNION ALL
SELECT core.get_menu_id('ASC'), 'fr', 'attribuer Caissier' UNION ALL
SELECT core.get_menu_id('POSS'), 'fr', 'Configuration de POS' UNION ALL
SELECT core.get_menu_id('STT'), 'fr', 'Types de magasins' UNION ALL
SELECT core.get_menu_id('STO'), 'fr', 'magasins' UNION ALL
SELECT core.get_menu_id('SCR'), 'fr', 'Configuration espace d''archivage automatique' UNION ALL
SELECT core.get_menu_id('SCS'), 'fr', 'Configuration du compteur' UNION ALL
SELECT core.get_menu_id('PUQ'), 'fr', 'Achat & Devis' UNION ALL
SELECT core.get_menu_id('DRP'), 'fr', 'Achat direct' UNION ALL
SELECT core.get_menu_id('PO'), 'fr', 'Bon de commande' UNION ALL
SELECT core.get_menu_id('GRN'), 'fr', 'GRN contre PO' UNION ALL
SELECT core.get_menu_id('PAY'), 'fr', 'Facture d''achat contre GRN' UNION ALL
SELECT core.get_menu_id('PAS'), 'fr', 'Paiement à Fournisseur' UNION ALL
SELECT core.get_menu_id('PR'), 'fr', 'achat de retour' UNION ALL
SELECT core.get_menu_id('PUR'), 'fr', 'Rapports d''achat' UNION ALL
SELECT core.get_menu_id('IIM'), 'fr', 'Les mouvements des stocks' UNION ALL
SELECT core.get_menu_id('STJ'), 'fr', 'Journal de transfert de stock' UNION ALL
SELECT core.get_menu_id('STA'), 'fr', 'Ajustements de stock' UNION ALL
SELECT core.get_menu_id('ISM'), 'fr', 'Configuration et Maintenance' UNION ALL
SELECT core.get_menu_id('PT'), 'fr', 'Types de fête' UNION ALL
SELECT core.get_menu_id('PA'), 'fr', 'Les comptes des partis' UNION ALL
SELECT core.get_menu_id('PSA'), 'fr', 'Adresse de livraison' UNION ALL
SELECT core.get_menu_id('SSI'), 'fr', 'Point de maintenance' UNION ALL
SELECT core.get_menu_id('ICP'), 'fr', 'coût prix' UNION ALL
SELECT core.get_menu_id('ISP'), 'fr', 'prix de vente' UNION ALL
SELECT core.get_menu_id('SSG'), 'fr', 'Groupes des ouvrages' UNION ALL
SELECT core.get_menu_id('SSB'), 'fr', 'marques' UNION ALL
SELECT core.get_menu_id('UOM'), 'fr', 'Unités de mesure' UNION ALL
SELECT core.get_menu_id('CUOM'), 'fr', 'Unités composées de mesure' UNION ALL
SELECT core.get_menu_id('SHI'), 'fr', 'Renseignements sur l''expéditeur' UNION ALL
SELECT core.get_menu_id('FTT'), 'fr', 'Transactions et Modèles' UNION ALL
SELECT core.get_menu_id('JVN'), 'fr', 'Journal Chèques Entrée' UNION ALL
SELECT core.get_menu_id('TTR'), 'fr', 'Transaction modèle' UNION ALL
SELECT core.get_menu_id('STN'), 'fr', 'Instructions permanentes' UNION ALL
SELECT core.get_menu_id('UER'), 'fr', 'Taux de change mise à jour' UNION ALL
SELECT core.get_menu_id('RBA'), 'fr', 'Concilier compte bancaire' UNION ALL
SELECT core.get_menu_id('FVV'), 'fr', 'Vérification bon' UNION ALL
SELECT core.get_menu_id('FTDM'), 'fr', 'Transaction Document Manager' UNION ALL
SELECT core.get_menu_id('FSM'), 'fr', 'Configuration et Maintenance' UNION ALL
SELECT core.get_menu_id('COA'), 'fr', 'Tableau des comptes' UNION ALL
SELECT core.get_menu_id('CUR'), 'fr', 'Gestion des devises' UNION ALL
SELECT core.get_menu_id('CBA'), 'fr', 'Comptes bancaires' UNION ALL
SELECT core.get_menu_id('PGM'), 'fr', 'Cartographie de GL produit' UNION ALL
SELECT core.get_menu_id('BT'), 'fr', 'Budgets et objectifs' UNION ALL
SELECT core.get_menu_id('AGS'), 'fr', 'Vieillissement Dalles' UNION ALL
SELECT core.get_menu_id('TTY'), 'fr', 'Types d''impôt' UNION ALL
SELECT core.get_menu_id('TS'), 'fr', 'Configuration de l''impôt' UNION ALL
SELECT core.get_menu_id('CC'), 'fr', 'Centres de coûts' UNION ALL
SELECT core.get_menu_id('MFW'), 'fr', 'Flux de travail de fabrication' UNION ALL
SELECT core.get_menu_id('MFWSF'), 'fr', 'Prévisions de ventes' UNION ALL
SELECT core.get_menu_id('MFWMPS'), 'fr', 'Le calendrier de production de Maître' UNION ALL
SELECT core.get_menu_id('MFS'), 'fr', 'Configuration de fabrication' UNION ALL
SELECT core.get_menu_id('MFSWC'), 'fr', 'Centres de travail' UNION ALL
SELECT core.get_menu_id('MFSBOM'), 'fr', 'Nomenclatures' UNION ALL
SELECT core.get_menu_id('MFR'), 'fr', 'Rapports de fabrication' UNION ALL
SELECT core.get_menu_id('MFRGNR'), 'fr', 'Exigences bruts et nets' UNION ALL
SELECT core.get_menu_id('MFRCVSL'), 'fr', 'Capacité vs plomb' UNION ALL
SELECT core.get_menu_id('MFRSFP'), 'fr', 'Planification d''atelier' UNION ALL
SELECT core.get_menu_id('MFRPOS'), 'fr', 'Suivi de commande de production' UNION ALL
SELECT core.get_menu_id('CRMM'), 'fr', 'CRM principal' UNION ALL
SELECT core.get_menu_id('CRML'), 'fr', 'Ajouter un nouveau chef' UNION ALL
SELECT core.get_menu_id('CRMO'), 'fr', 'Ajouter une nouvelle opportunité' UNION ALL
SELECT core.get_menu_id('CRMC'), 'fr', 'Autre plomb à la relance' UNION ALL
SELECT core.get_menu_id('CRMFL'), 'fr', 'Suivi plomb' UNION ALL
SELECT core.get_menu_id('CRMFO'), 'fr', 'possibilité de Suivi' UNION ALL
SELECT core.get_menu_id('CSM'), 'fr', 'Configuration et Maintenance' UNION ALL
SELECT core.get_menu_id('CRMLS'), 'fr', 'Sources plomb Configuration' UNION ALL
SELECT core.get_menu_id('CRMLST'), 'fr', 'Configuration de l''état de plomb' UNION ALL
SELECT core.get_menu_id('CRMOS'), 'fr', 'Possibilité de configuration Etapes' UNION ALL
SELECT core.get_menu_id('SMP'), 'fr', 'Paramètres divers' UNION ALL
SELECT core.get_menu_id('TRF'), 'fr', 'drapeaux' UNION ALL
SELECT core.get_menu_id('SEAR'), 'fr', 'Rapports de vérification' UNION ALL
SELECT core.get_menu_id('SEAR-LV'), 'fr', 'Connectez-vous Voir' UNION ALL
SELECT core.get_menu_id('SOS'), 'fr', 'Installation d''Office' UNION ALL
SELECT core.get_menu_id('SOB'), 'fr', 'Bureau & Direction installation' UNION ALL
SELECT core.get_menu_id('SDS'), 'fr', 'Département installation' UNION ALL
SELECT core.get_menu_id('SRM'), 'fr', 'Gestion des rôles' UNION ALL
SELECT core.get_menu_id('SUM'), 'fr', 'Gestion des utilisateurs' UNION ALL
SELECT core.get_menu_id('SFY'), 'fr', 'Exercice information' UNION ALL
SELECT core.get_menu_id('SFR'), 'fr', 'Fréquence et gestion Exercice' UNION ALL
SELECT core.get_menu_id('SPM'), 'fr', 'Gestion des politiques' UNION ALL
SELECT core.get_menu_id('SVV'), 'fr', 'Politique sur la vérification Bon' UNION ALL
SELECT core.get_menu_id('SAV'), 'fr', 'Politique sur la vérification automatique' UNION ALL
SELECT core.get_menu_id('SMA'), 'fr', 'Politique Accès au menu' UNION ALL
SELECT core.get_menu_id('SAP'), 'fr', 'GL Politique d''accès' UNION ALL
SELECT core.get_menu_id('SSP'), 'fr', 'Politique de magasin' UNION ALL
SELECT core.get_menu_id('SWI'), 'fr', 'commutateurs' UNION ALL
SELECT core.get_menu_id('SAT'), 'fr', 'Outils d''administration' UNION ALL
SELECT core.get_menu_id('SQL'), 'fr', 'SQL Query Tool' UNION ALL
SELECT core.get_menu_id('DBSTAT'), 'fr', 'Statistiques de base de données' UNION ALL
SELECT core.get_menu_id('BAK'), 'fr', 'Base de données de sauvegarde' UNION ALL
SELECT core.get_menu_id('RES'), 'fr', 'restaurer la base de données' UNION ALL
SELECT core.get_menu_id('PWD'), 'fr', 'Changer d''utilisateur Mot de passe' UNION ALL
SELECT core.get_menu_id('NEW'), 'fr', 'nouvelle entreprise';