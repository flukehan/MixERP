
INSERT INTO core.menus(menu_text, url, menu_code, level)
SELECT 'Dashboard', '~/Dashboard/Index.aspx', 'DB', 0 UNION ALL
SELECT 'Sales', '~/Sales/Index.aspx', 'SA', 0 UNION ALL
SELECT 'Purchase', '~/Purchase/Index.aspx', 'PU', 0 UNION ALL
SELECT 'Products & Items', '~/Inventory/Index.aspx', 'ITM', 0 UNION ALL
SELECT 'Finance', '~/Finance/Index.aspx', 'FI', 0 UNION ALL
SELECT 'Manufacturing', '~/Manufacturing/Index.aspx', 'MF', 0 UNION ALL
SELECT 'CRM', '~/CRM/Index.aspx', 'CRM', 0 UNION ALL
SELECT 'Setup Parameters', '~/Setup/Index.aspx', 'SE', 0 UNION ALL
SELECT 'POS', '~/POS/Index.aspx', 'POS', 0;


INSERT INTO core.menus(menu_text, url, menu_code, level, parent_menu_id)
		  SELECT 'Sales & Quotation', NULL, 'SAQ', 1, core.get_menu_id('SA')
UNION ALL SELECT 'Direct Sales', '~/Sales/DirectSales.aspx', 'DRS', 2, core.get_menu_id('SAQ')
UNION ALL SELECT 'Sales Quotation', '~/Sales/Quotation.aspx', 'SQ', 2, core.get_menu_id('SAQ')
UNION ALL SELECT 'Sales Order', '~/Sales/Order.aspx', 'SO', 2, core.get_menu_id('SAQ')
UNION ALL SELECT 'Sales Delivery', '~/Sales/Delivery.aspx', 'SD', 2, core.get_menu_id('SAQ')
UNION ALL SELECT 'Receipt from Customer', '~/Sales/Receipt.aspx', 'RFC', 2, core.get_menu_id('SAQ')
UNION ALL SELECT 'Sales Return', '~/Sales/Return.aspx', 'SR', 2, core.get_menu_id('SAQ')
UNION ALL SELECT 'Setup & Maintenance', NULL, 'SSM', 1, core.get_menu_id('SA')
UNION ALL SELECT 'Bonus Slab for Agents', '~/Sales/Setup/AgentBonusSlabs.aspx', 'ABS', 2, core.get_menu_id('SSM')
UNION ALL SELECT 'Bonus Slab Details', '~/Sales/Setup/AgentBonusSlabDetails.aspx', 'BSD', 2, core.get_menu_id('SSM')
UNION ALL SELECT 'Sales Agents', '~/Sales/Setup/Agents.aspx', 'SSA', 2, core.get_menu_id('SSM')
UNION ALL SELECT 'Bonus Slab Assignment', '~/Sales/Setup/BonusSlabAssignment.aspx', 'BSA', 2, core.get_menu_id('SSM')
UNION ALL SELECT 'Sales Reports', NULL, 'SAR', 1, core.get_menu_id('SA')
UNION ALL SELECT 'View Sales Inovice', '~/Reports/Sales.View.Sales.Invoice.xml', 'SAR-SVSI', 2, core.get_menu_id('SAR')
UNION ALL SELECT 'Cashier Management', NULL, 'CM', 1, core.get_menu_id('POS')
UNION ALL SELECT 'Assign Cashier', '~/POS/AssignCashier.aspx', 'ASC', 2, core.get_menu_id('CM')
UNION ALL SELECT 'POS Setup', NULL, 'POSS', 1, core.get_menu_id('POS')
UNION ALL SELECT 'Store Types', '~/POS/Setup/StoreTypes.aspx', 'STT', 2, core.get_menu_id('POSS')
UNION ALL SELECT 'Stores', '~/POS/Setup/Stores.aspx', 'STO', 2, core.get_menu_id('POSS')
UNION ALL SELECT 'Cash Repository Setup', '~/Setup/CashRepositories.aspx', 'SCR', 2, core.get_menu_id('POSS')
UNION ALL SELECT 'Counter Setup', '~/Setup/Counters.aspx', 'SCS', 2, core.get_menu_id('POSS')
UNION ALL SELECT 'Purchase & Quotation', NULL, 'PUQ', 1, core.get_menu_id('PU')
UNION ALL SELECT 'Direct Purchase', '~/Purchase/DirectPurchase.aspx', 'DRP', 2, core.get_menu_id('PUQ')
UNION ALL SELECT 'Purchase Order', '~/Purchase/Order.aspx', 'PO', 2, core.get_menu_id('PUQ')
UNION ALL SELECT 'GRN Entry', '~/Purchase/GRN.aspx', 'GRN', 2, core.get_menu_id('PUQ')
UNION ALL SELECT 'Purchase Invoice Against GRN', '~/Purchase/Invoice.aspx', 'PAY', 2, core.get_menu_id('PUQ')
UNION ALL SELECT 'Payment to Supplier', '~/Purchase/Payment.aspx', 'PAS', 2, core.get_menu_id('PUQ')
UNION ALL SELECT 'Purchase Return', '~/Purchase/Return.aspx', 'PR', 2, core.get_menu_id('PUQ')
UNION ALL SELECT 'Purchase Reports', NULL, 'PUR', 1, core.get_menu_id('PU')
UNION ALL SELECT 'Inventory Movements', NULL, 'IIM', 1, core.get_menu_id('ITM')
UNION ALL SELECT 'Stock Transfer Journal', '~/Inventory/Transfer.aspx', 'STJ', 2, core.get_menu_id('IIM')
UNION ALL SELECT 'Stock Adjustments', '~/Inventory/Adjustment.aspx', 'STA', 2, core.get_menu_id('IIM')
UNION ALL SELECT 'Setup & Maintenance', NULL, 'ISM', 1, core.get_menu_id('ITM')
UNION ALL SELECT 'Party Types', '~/Inventory/Setup/PartyTypes.aspx', 'PT', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Party Accounts', '~/Inventory/Setup/Parties.aspx', 'PA', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Shipping Addresses', '~/Inventory/Setup/ShippingAddresses.aspx', 'PSA', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Item Maintenance', '~/Inventory/Setup/Items.aspx', 'SSI', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Cost Prices', '~/Inventory/Setup/CostPrices.aspx', 'ICP', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Selling Prices', '~/Inventory/Setup/SellingPrices.aspx', 'ISP', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Item Groups', '~/Inventory/Setup/ItemGroups.aspx', 'SSG', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Brands', '~/Inventory/Setup/Brands.aspx', 'SSB', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Units of Measure', '~/Inventory/Setup/UOM.aspx', 'UOM', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Compound Units of Measure', '~/Inventory/Setup/CUOM.aspx', 'CUOM', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Shipper Information', '~/Inventory/Setup/Shipper.aspx', 'SHI', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Transactions & Templates', NULL, 'FTT', 1, core.get_menu_id('FI')
UNION ALL SELECT 'Journal Voucher Entry', '~/Finance/JournalVoucher.aspx', 'JVN', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Template Transaction', '~/Finance/TemplateTransaction.aspx', 'TTR', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Standing Instructions', '~/Finance/StandingInstructions.aspx', 'STN', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Update Exchange Rates', '~/Finance/UpdateExchangeRates.aspx', 'UER', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Reconcile Bank Account', '~/Finance/BankReconciliation.aspx', 'RBA', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Voucher Verification', '~/Finance/VoucherVerification.aspx', 'FVV', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Transaction Document Manager', '~/Finance/TransactionDocumentManager.aspx', 'FTDM', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Setup & Maintenance', NULL, 'FSM', 1, core.get_menu_id('FI')
UNION ALL SELECT 'Chart of Accounts', '~/Finance/Setup/COA.aspx', 'COA', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Currency Management', '~/Finance/Setup/Currencies.aspx', 'CUR', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Bank Accounts', '~/Finance/Setup/BankAccounts.aspx', 'CBA', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Product GL Mapping', '~/Finance/Setup/ProductGLMapping.aspx', 'PGM', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Budgets & Targets', '~/Finance/Setup/BudgetAndTarget.aspx', 'BT', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Ageing Slabs', '~/Finance/Setup/AgeingSlabs.aspx', 'AGS', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Tax Types', '~/Finance/Setup/TaxTypes.aspx', 'TTY', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Tax Setup', '~/Finance/Setup/TaxSetup.aspx', 'TS', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Cost Centers', '~/Finance/Setup/CostCenters.aspx', 'CC', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Manufacturing Workflow', NULL, 'MFW', 1, core.get_menu_id('MF')
UNION ALL SELECT 'Sales Forecast', '~/Manufacturing/Workflow/SalesForecast.aspx', 'MFWSF', 2, core.get_menu_id('MFW')
UNION ALL SELECT 'Master Production Schedule', '~/Manufacturing/Workflow/MasterProductionSchedule.aspx', 'MFWMPS', 2, core.get_menu_id('MFW')
UNION ALL SELECT 'Manufacturing Setup', NULL, 'MFS', 1, core.get_menu_id('MF')
UNION ALL SELECT 'Work Centers', '~/Manufacturing/Setup/WorkCenters.aspx', 'MFSWC', 2, core.get_menu_id('MFS')
UNION ALL SELECT 'Bills of Material', '~/Manufacturing/Setup/BillsOfMaterial.aspx', 'MFSBOM', 2, core.get_menu_id('MFS')
UNION ALL SELECT 'Manufacturing Reports', NULL, 'MFR', 1, core.get_menu_id('MF')
UNION ALL SELECT 'Gross & Net Requirements', '~/Manufacturing/Reports/GrossAndNetRequirements.aspx', 'MFRGNR', 2, core.get_menu_id('MFR')
UNION ALL SELECT 'Capacity vs Lead', '~/Manufacturing/Reports/CapacityVersusLead.aspx', 'MFRCVSL', 2, core.get_menu_id('MFR')
UNION ALL SELECT 'Shop Floor Planning', '~/Manufacturing/Reports/ShopFloorPlanning.aspx', 'MFRSFP', 2, core.get_menu_id('MFR')
UNION ALL SELECT 'Production Order Status', '~/Manufacturing/Reports/ProductionOrderStatus.aspx', 'MFRPOS', 2, core.get_menu_id('MFR')
UNION ALL SELECT 'CRM Main', NULL, 'CRMM', 1, core.get_menu_id('CRM')
UNION ALL SELECT 'Add a New Lead', '~/CRM/Lead.aspx', 'CRML', 2, core.get_menu_id('CRMM')
UNION ALL SELECT 'Add a New Opportunity', '~/CRM/Opportunity.aspx', 'CRMO', 2, core.get_menu_id('CRMM')
UNION ALL SELECT 'Convert Lead to Opportunity', '~/CRM/ConvertLeadToOpportunity.aspx', 'CRMC', 2, core.get_menu_id('CRMM')
UNION ALL SELECT 'Lead Follow Up', '~/CRM/LeadFollowUp.aspx', 'CRMFL', 2, core.get_menu_id('CRMM')
UNION ALL SELECT 'Opportunity Follow Up', '~/CRM/OpportunityFollowUp.aspx', 'CRMFO', 2, core.get_menu_id('CRMM')
UNION ALL SELECT 'Setup & Maintenance', NULL, 'CSM', 1, core.get_menu_id('CRM')
UNION ALL SELECT 'Lead Sources Setup', '~/CRM/Setup/LeadSources.aspx', 'CRMLS', 2, core.get_menu_id('CSM')
UNION ALL SELECT 'Lead Status Setup', '~/CRM/Setup/LeadStatuses.aspx', 'CRMLST', 2, core.get_menu_id('CSM')
UNION ALL SELECT 'Opportunity Stages Setup', '~/CRM/Setup/OpportunityStages.aspx', 'CRMOS', 2, core.get_menu_id('CSM')
UNION ALL SELECT 'Miscellaneous Parameters', NULL, 'SMP', 1, core.get_menu_id('SE')
UNION ALL SELECT 'Flags', '~/Setup/Flags.aspx', 'TRF', 2, core.get_menu_id('SMP')
UNION ALL SELECT 'Audit Reports', NULL, 'SEAR', 1, core.get_menu_id('SE')
UNION ALL SELECT 'Login View', '~/Reports/Office.Login.xml', 'SEAR-LV', 2, core.get_menu_id('SEAR')
UNION ALL SELECT 'Office Setup', NULL, 'SOS', 1, core.get_menu_id('SE')
UNION ALL SELECT 'Office & Branch Setup', '~/Setup/Offices.aspx', 'SOB', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'Department Setup', '~/Setup/Departments.aspx', 'SDS', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'Role Management', '~/Setup/Roles.aspx', 'SRM', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'User Management', '~/Setup/Users.aspx', 'SUM', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'Fiscal Year Information', '~/Setup/FiscalYear.aspx', 'SFY', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'Frequency & Fiscal Year Management', '~/Setup/Frequency.aspx', 'SFR', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'Policy Management', NULL, 'SPM', 1, core.get_menu_id('SE')
UNION ALL SELECT 'Voucher Verification Policy', '~/Setup/Policy/VoucherVerification.aspx', 'SVV', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'Automatic Verification Policy', '~/Setup/Policy/AutoVerification.aspx', 'SAV', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'Menu Access Policy', '~/Setup/Policy/MenuAccess.aspx', 'SMA', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'GL Access Policy', '~/Setup/Policy/GLAccess.aspx', 'SAP', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'Store Policy', '~/Setup/Policy/Store.aspx', 'SSP', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'Switches', '~/Setup/Policy/Switches.aspx', 'SWI', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'Admin Tools', NULL, 'SAT', 1, core.get_menu_id('SE')
UNION ALL SELECT 'SQL Query Tool', '~/Setup/Admin/Query.aspx', 'SQL', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'Database Statistics', '~/Setup/Admin/DatabaseStatistics.aspx', 'DBSTAT', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'Backup Database', '~/Setup/Admin/Backup.aspx', 'BAK', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'Restore Database', '~/Setup/Admin/Restore.aspx', 'RES', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'Change User Password', '~/Setup/Admin/ChangePassword.aspx', 'PWD', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'New Company', '~/Setup/Admin/NewCompany.aspx', 'NEW', 2, core.get_menu_id('SAT');


INSERT INTO policy.menu_access(office_id, menu_id, user_id)
SELECT office.get_office_id_by_office_code('PES-NY-BK'), core.menus.menu_id, office.get_user_id_by_user_name('binod')
FROM core.menus

UNION ALL
SELECT office.get_office_id_by_office_code('PES-NY-MEM'), core.menus.menu_id, office.get_user_id_by_user_name('binod')
FROM core.menus;




/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

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

