
INSERT INTO core.menus(menu_text, url, menu_code, level)
SELECT 'Sales', '~/Modules/Sales/Index.mix', 'SA', 0 UNION ALL
SELECT 'Purchase', '~/Modules/Purchase/Index.mix', 'PU', 0 UNION ALL
SELECT 'Products & Items', '~/Modules/Inventory/Index.mix', 'ITM', 0 UNION ALL
SELECT 'Finance', '~/Modules/Finance/Index.mix', 'FI', 0 UNION ALL
SELECT 'Manufacturing', '~/Modules/Manufacturing/Index.mix', 'MF', 0 UNION ALL
SELECT 'CRM', '~/Modules/CRM/Index.mix', 'CRM', 0 UNION ALL
SELECT 'Back Office', '~/Modules/BackOffice/Index.mix', 'BO', 0 UNION ALL
SELECT 'POS', '~/Modules/POS/Index.mix', 'POS', 0;


INSERT INTO core.menus(menu_text, url, menu_code, level, parent_menu_id)
          SELECT 'Sales & Quotation', NULL, 'SAQ', 1, core.get_menu_id('SA')
UNION ALL SELECT 'Direct Sales', '~/Modules/Sales/DirectSales.mix', 'DRS', 2, core.get_menu_id('SAQ')
UNION ALL SELECT 'Sales Quotation', '~/Modules/Sales/Quotation.mix', 'SQ', 2, core.get_menu_id('SAQ')
UNION ALL SELECT 'Sales Order', '~/Modules/Sales/Order.mix', 'SO', 2, core.get_menu_id('SAQ')
UNION ALL SELECT 'Sales Delivery', '~/Modules/Sales/Delivery.mix', 'SD', 2, core.get_menu_id('SAQ')
UNION ALL SELECT 'Receipt from Customer', '~/Modules/Sales/Receipt.mix', 'RFC', 2, core.get_menu_id('SAQ')
UNION ALL SELECT 'Sales Return', '~/Modules/Sales/Return.mix', 'SR', 2, core.get_menu_id('SAQ')
UNION ALL SELECT 'Setup & Maintenance', NULL, 'SSM', 1, core.get_menu_id('SA')
UNION ALL SELECT 'Bonus Slab for Salespersons', '~/Modules/Sales/Setup/BonusSlabs.mix', 'ABS', 2, core.get_menu_id('SSM')
UNION ALL SELECT 'Bonus Slab Details', '~/Modules/Sales/Setup/BonusSlabDetails.mix', 'BSD', 2, core.get_menu_id('SSM')
UNION ALL SELECT 'Sales Teams', '~/Modules/Sales/Setup/Teams.mix', 'SST', 2, core.get_menu_id('SSM')
UNION ALL SELECT 'Salespersons', '~/Modules/Sales/Setup/Salespersons.mix', 'SSA', 2, core.get_menu_id('SSM')
UNION ALL SELECT 'Bonus Slab Assignment', '~/Modules/Sales/Setup/BonusSlabAssignment.mix', 'BSA', 2, core.get_menu_id('SSM')
UNION ALL SELECT 'Sales Reports', NULL, 'SAR', 1, core.get_menu_id('SA')
UNION ALL SELECT 'Top Selling Items', '~/Modules/Sales/Reports/TopSellingItems.mix', 'SAR-TSI', 2, core.get_menu_id('SAR')
UNION ALL SELECT 'Cashier Management', NULL, 'CM', 1, core.get_menu_id('POS')
UNION ALL SELECT 'Assign Cashier', '~/Modules/POS/AssignCashier.mix', 'ASC', 2, core.get_menu_id('CM')
UNION ALL SELECT 'POS Setup', NULL, 'POSS', 1, core.get_menu_id('POS')
UNION ALL SELECT 'Store Types', '~/Modules/POS/Setup/StoreTypes.mix', 'STT', 2, core.get_menu_id('POSS')
UNION ALL SELECT 'Stores', '~/Modules/POS/Setup/Stores.mix', 'STO', 2, core.get_menu_id('POSS')
UNION ALL SELECT 'Counter Setup', '~/Modules/BackOffice/Counters.mix', 'SCS', 2, core.get_menu_id('POSS')
UNION ALL SELECT 'Purchase & Quotation', NULL, 'PUQ', 1, core.get_menu_id('PU')
UNION ALL SELECT 'Direct Purchase', '~/Modules/Purchase/DirectPurchase.mix', 'DRP', 2, core.get_menu_id('PUQ')
UNION ALL SELECT 'Purchase Order', '~/Modules/Purchase/Order.mix', 'PO', 2, core.get_menu_id('PUQ')
UNION ALL SELECT 'Purchase Reorder', '~/Modules/Purchase/Reorder.mix', 'PRO', 2, core.get_menu_id('PUQ')
UNION ALL SELECT 'GRN Entry', '~/Modules/Purchase/GRN.mix', 'GRN', 2, core.get_menu_id('PUQ')
UNION ALL SELECT 'Purchase Return', '~/Modules/Purchase/Return.mix', 'PR', 2, core.get_menu_id('PUQ')
UNION ALL SELECT 'Purchase Reports', NULL, 'PUR', 1, core.get_menu_id('PU')
UNION ALL SELECT 'Inventory Movements', NULL, 'IIM', 1, core.get_menu_id('ITM')
UNION ALL SELECT 'Stock Transfer Journal', '~/Modules/Inventory/Transfer.mix', 'STJ', 2, core.get_menu_id('IIM')
UNION ALL SELECT 'Stock Adjustments', '~/Modules/Inventory/Adjustment.mix', 'STA', 2, core.get_menu_id('IIM')
UNION ALL SELECT 'Setup & Maintenance', NULL, 'ISM', 1, core.get_menu_id('ITM')
UNION ALL SELECT 'Party Types', '~/Modules/Inventory/Setup/PartyTypes.mix', 'PT', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Party Accounts', '~/Modules/Inventory/Setup/Parties.mix', 'PA', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Shipping Addresses', '~/Modules/Inventory/Setup/ShippingAddresses.mix', 'PSA', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Item Maintenance', '~/Modules/Inventory/Setup/Items.mix', 'SSI', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Compound Items', '~/Modules/Inventory/Setup/CompoundItems.mix', 'SSC', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Compound Item Details', '~/Modules/Inventory/Setup/CompoundItemDetails.mix', 'SSCD', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Cost Prices', '~/Modules/Inventory/Setup/CostPrices.mix', 'ICP', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Selling Prices', '~/Modules/Inventory/Setup/SellingPrices.mix', 'ISP', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Item Groups', '~/Modules/Inventory/Setup/ItemGroups.mix', 'SSG', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Brands', '~/Modules/Inventory/Setup/Brands.mix', 'SSB', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Units of Measure', '~/Modules/Inventory/Setup/UOM.mix', 'UOM', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Compound Units of Measure', '~/Modules/Inventory/Setup/CUOM.mix', 'CUOM', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Shipper Information', '~/Modules/Inventory/Setup/Shippers.mix', 'SHI', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Transactions & Templates', NULL, 'FTT', 1, core.get_menu_id('FI')
UNION ALL SELECT 'Journal Voucher Entry', '~/Modules/Finance/JournalVoucher.mix', 'JVN', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Template Transaction', '~/Modules/Finance/TemplateTransaction.mix', 'TTR', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Standing Instructions', '~/Modules/Finance/StandingInstructions.mix', 'STN', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Update Exchange Rates', '~/Modules/Finance/UpdateExchangeRates.mix', 'UER', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Reconcile Bank Account', '~/Modules/Finance/BankReconciliation.mix', 'RBA', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Voucher Verification', '~/Modules/Finance/VoucherVerification.mix', 'FVV', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Transaction Document Manager', '~/Modules/Finance/TransactionDocumentManager.mix', 'FTDM', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Setup & Maintenance', NULL, 'FSM', 1, core.get_menu_id('FI')
UNION ALL SELECT 'Chart of Accounts', '~/Modules/Finance/Setup/COA.mix', 'COA', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Currency Management', '~/Modules/Finance/Setup/Currencies.mix', 'CUR', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Bank Accounts', '~/Modules/Finance/Setup/BankAccounts.mix', 'CBA', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Product GL Mapping', '~/Modules/Finance/Setup/ProductGLMapping.mix', 'PGM', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Budgets & Targets', '~/Modules/Finance/Setup/BudgetAndTarget.mix', 'BT', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Ageing Slabs', '~/Modules/Finance/Setup/AgeingSlabs.mix', 'AGS', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Tax Types', '~/Modules/Finance/Setup/TaxTypes.mix', 'TTY', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Tax Setup', '~/Modules/Finance/Setup/TaxSetup.mix', 'TS', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Cost Centers', '~/Modules/Finance/Setup/CostCenters.mix', 'CC', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Manufacturing Workflow', NULL, 'MFW', 1, core.get_menu_id('MF')
UNION ALL SELECT 'Sales Forecast', '~/Modules/Manufacturing/Workflow/SalesForecast.mix', 'MFWSF', 2, core.get_menu_id('MFW')
UNION ALL SELECT 'Master Production Schedule', '~/Modules/Manufacturing/Workflow/MasterProductionSchedule.mix', 'MFWMPS', 2, core.get_menu_id('MFW')
UNION ALL SELECT 'Manufacturing Setup', NULL, 'MFS', 1, core.get_menu_id('MF')
UNION ALL SELECT 'Work Centers', '~/Modules/Manufacturing/Setup/WorkCenters.mix', 'MFSWC', 2, core.get_menu_id('MFS')
UNION ALL SELECT 'Bills of Material', '~/Modules/Manufacturing/Setup/BillsOfMaterial.mix', 'MFSBOM', 2, core.get_menu_id('MFS')
UNION ALL SELECT 'Manufacturing Reports', NULL, 'MFR', 1, core.get_menu_id('MF')
UNION ALL SELECT 'Gross & Net Requirements', '~/Modules/Manufacturing/Reports/GrossAndNetRequirements.mix', 'MFRGNR', 2, core.get_menu_id('MFR')
UNION ALL SELECT 'Capacity vs Lead', '~/Modules/Manufacturing/Reports/CapacityVersusLead.mix', 'MFRCVSL', 2, core.get_menu_id('MFR')
UNION ALL SELECT 'Shop Floor Planning', '~/Modules/Manufacturing/Reports/ShopFloorPlanning.mix', 'MFRSFP', 2, core.get_menu_id('MFR')
UNION ALL SELECT 'Production Order Status', '~/Modules/Manufacturing/Reports/ProductionOrderStatus.mix', 'MFRPOS', 2, core.get_menu_id('MFR')
UNION ALL SELECT 'CRM Main', NULL, 'CRMM', 1, core.get_menu_id('CRM')
UNION ALL SELECT 'Add a New Lead', '~/Modules/CRM/Lead.mix', 'CRML', 2, core.get_menu_id('CRMM')
UNION ALL SELECT 'Add a New Opportunity', '~/Modules/CRM/Opportunity.mix', 'CRMO', 2, core.get_menu_id('CRMM')
UNION ALL SELECT 'Convert Lead to Opportunity', '~/Modules/CRM/ConvertLeadToOpportunity.mix', 'CRMC', 2, core.get_menu_id('CRMM')
UNION ALL SELECT 'Lead Follow Up', '~/Modules/CRM/LeadFollowUp.mix', 'CRMFL', 2, core.get_menu_id('CRMM')
UNION ALL SELECT 'Opportunity Follow Up', '~/Modules/CRM/OpportunityFollowUp.mix', 'CRMFO', 2, core.get_menu_id('CRMM')
UNION ALL SELECT 'Setup & Maintenance', NULL, 'CSM', 1, core.get_menu_id('CRM')
UNION ALL SELECT 'Lead Sources Setup', '~/Modules/CRM/Setup/LeadSources.mix', 'CRMLS', 2, core.get_menu_id('CSM')
UNION ALL SELECT 'Lead Status Setup', '~/Modules/CRM/Setup/LeadStatuses.mix', 'CRMLST', 2, core.get_menu_id('CSM')
UNION ALL SELECT 'Opportunity Stages Setup', '~/Modules/CRM/Setup/OpportunityStages.mix', 'CRMOS', 2, core.get_menu_id('CSM')
UNION ALL SELECT 'Miscellaneous Parameters', NULL, 'SMP', 1, core.get_menu_id('BO')
UNION ALL SELECT 'Flags', '~/Modules/BackOffice/Flags.mix', 'TRF', 2, core.get_menu_id('SMP')
UNION ALL SELECT 'Audit Reports', NULL, 'SEAR', 1, core.get_menu_id('BO')
UNION ALL SELECT 'Login View', '~/Reports/Office.Login.xml', 'SEAR-LV', 2, core.get_menu_id('SEAR')
UNION ALL SELECT 'Office Setup', NULL, 'SOS', 1, core.get_menu_id('BO')
UNION ALL SELECT 'Office & Branch Setup', '~/Modules/BackOffice/Offices.mix', 'SOB', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'Cash Repository Setup', '~/Modules/BackOffice/CashRepositories.mix', 'SCR', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'Department Setup', '~/Modules/BackOffice/Departments.mix', 'SDS', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'Role Management', '~/Modules/BackOffice/Roles.mix', 'SRM', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'User Management', '~/Modules/BackOffice/Users.mix', 'SUM', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'Fiscal Year Information', '~/Modules/BackOffice/FiscalYear.mix', 'SFY', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'Frequency & Fiscal Year Management', '~/Modules/BackOffice/Frequency.mix', 'SFR', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'Policy Management', NULL, 'SPM', 1, core.get_menu_id('BO')
UNION ALL SELECT 'Voucher Verification Policy', '~/Modules/BackOffice/Policy/VoucherVerification.mix', 'SVV', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'Automatic Verification Policy', '~/Modules/BackOffice/Policy/AutoVerification.mix', 'SAV', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'Menu Access Policy', '~/Modules/BackOffice/Policy/MenuAccess.mix', 'SMA', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'GL Access Policy', '~/Modules/BackOffice/Policy/GLAccess.mix', 'SAP', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'Store Policy', '~/Modules/BackOffice/Policy/Store.mix', 'SSP', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'Switches', '~/Modules/BackOffice/Policy/Switches.mix', 'SWI', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'Admin Tools', NULL, 'SAT', 1, core.get_menu_id('BO')
UNION ALL SELECT 'SQL Query Tool', '~/Modules/BackOffice/Admin/Query.mix', 'SQL', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'Database Statistics', '~/Modules/BackOffice/Admin/DatabaseStatistics.mix', 'DBSTAT', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'Backup Database', '~/Modules/BackOffice/Admin/Backup.mix', 'BAK', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'Restore Database', '~/Modules/BackOffice/Admin/Restore.mix', 'RES', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'Change User Password', '~/Modules/BackOffice/Admin/ChangePassword.mix', 'PWD', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'New Company', '~/Modules/BackOffice/Admin/NewCompany.mix', 'NEW', 2, core.get_menu_id('SAT');


INSERT INTO policy.menu_access(office_id, menu_id, user_id)
SELECT office.get_office_id_by_office_code('MoF-NY-BK'), core.menus.menu_id, office.get_user_id_by_user_name('binod')
FROM core.menus

UNION ALL
SELECT office.get_office_id_by_office_code('MoF-NY-MEM'), core.menus.menu_id, office.get_user_id_by_user_name('binod')
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
SELECT core.get_menu_id('SA'), 'fr', 'ventes' UNION ALL
SELECT core.get_menu_id('PU'), 'fr', 'acheter' UNION ALL
SELECT core.get_menu_id('ITM'), 'fr', 'Produits et Articles' UNION ALL
SELECT core.get_menu_id('FI'), 'fr', 'Finances' UNION ALL
SELECT core.get_menu_id('MF'), 'fr', 'fabrication' UNION ALL
SELECT core.get_menu_id('CRM'), 'fr', 'CRM' UNION ALL
SELECT core.get_menu_id('BO'), 'fr', 'Paramètres de configuration' UNION ALL
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
SELECT core.get_menu_id('PRO'), 'fr', 'Achat Reorder' UNION ALL
SELECT core.get_menu_id('GRN'), 'fr', 'GRN contre PO' UNION ALL
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

