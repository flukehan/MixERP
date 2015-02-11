INSERT INTO core.menus(menu_text, url, menu_code, level)
SELECT 'Sales', '~/Modules/Sales/Index.mix', 'SA', 0 UNION ALL
SELECT 'Purchase', '~/Modules/Purchase/Index.mix', 'PU', 0 UNION ALL
SELECT 'Products & Items', '~/Modules/Inventory/Index.mix', 'ITM', 0 UNION ALL
SELECT 'Finance', '~/Modules/Finance/Index.mix', 'FI', 0 UNION ALL
SELECT 'Back Office', '~/Modules/BackOffice/Index.mix', 'BO', 0;


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
UNION ALL SELECT 'Late Fees', '~/Modules/Sales/Setup/LateFees.mix', 'LF', 2, core.get_menu_id('SSM')
UNION ALL SELECT 'Payment Terms', '~/Modules/Sales/Setup/PaymentTerms.mix', 'PAT', 2, core.get_menu_id('SSM')
UNION ALL SELECT 'Recurring Invoices', '~/Modules/Sales/Setup/RecurringInvoices.mix', 'RI', 2, core.get_menu_id('SSM')
UNION ALL SELECT 'Recurring Invoice Setup', '~/Modules/Sales/Setup/RecurringInvoiceSetup.mix', 'RIS', 2, core.get_menu_id('SSM')
UNION ALL SELECT 'Sales Reports', NULL, 'SAR', 1, core.get_menu_id('SA')
UNION ALL SELECT 'Top Selling Items', '~/Modules/Sales/Reports/TopSellingItems.mix', 'SAR-TSI', 2, core.get_menu_id('SAR')
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
UNION ALL SELECT 'Store Types', '~/Modules/Inventory/Setup/StoreTypes.mix', 'STT', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Stores', '~/Modules/Inventory/Setup/Stores.mix', 'STO', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Counter Setup', '~/Modules/BackOffice/Counters.mix', 'SCS', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Party Types', '~/Modules/Inventory/Setup/PartyTypes.mix', 'PT', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Party Accounts', '~/Modules/Inventory/Setup/Parties.mix', 'PA', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Shipping Addresses', '~/Modules/Inventory/Setup/ShippingAddresses.mix', 'PSA', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Item Maintenance', '~/Modules/Inventory/Setup/Items.mix', 'SSI', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Compound Items', '~/Modules/Inventory/Setup/CompoundItems.mix', 'SSC', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Compound Item Details', '~/Modules/Inventory/Setup/CompoundItemDetails.mix', 'SSCD', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Cost Prices', '~/Modules/Inventory/Setup/CostPrices.mix', 'ICP', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Selling Prices', '~/Modules/Inventory/Setup/SellingPrices.mix', 'ISP', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Item Groups', '~/Modules/Inventory/Setup/ItemGroups.mix', 'SIG', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Item Types', '~/Modules/Inventory/Setup/ItemTypes.mix', 'SIT', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Brands', '~/Modules/Inventory/Setup/Brands.mix', 'SSB', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Units of Measure', '~/Modules/Inventory/Setup/UOM.mix', 'UOM', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Compound Units of Measure', '~/Modules/Inventory/Setup/CUOM.mix', 'CUOM', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Shipper Information', '~/Modules/Inventory/Setup/Shippers.mix', 'SHI', 2, core.get_menu_id('ISM')
UNION ALL SELECT 'Reports', NULL, 'IR', 1, core.get_menu_id('ITM')
UNION ALL SELECT 'Inventory Account Statement', '~/Modules/Inventory/Reports/AccountStatement.mix', 'IAS', 2, core.get_menu_id('IR')
UNION ALL SELECT 'Transactions & Templates', NULL, 'FTT', 1, core.get_menu_id('FI')
UNION ALL SELECT 'Journal Voucher Entry', '~/Modules/Finance/JournalVoucher.mix', 'JVN', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Update Exchange Rates', '~/Modules/Finance/UpdateExchangeRates.mix', 'UER', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Voucher Verification', '~/Modules/Finance/VoucherVerification.mix', 'FVV', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'End of Day Operation', '~/Modules/Finance/EODOperation.mix', 'EOD', 2, core.get_menu_id('FTT')
UNION ALL SELECT 'Setup & Maintenance', NULL, 'FSM', 1, core.get_menu_id('FI')
UNION ALL SELECT 'Chart of Accounts', '~/Modules/Finance/Setup/COA.mix', 'COA', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Currency Management', '~/Modules/Finance/Setup/Currencies.mix', 'CUR', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Bank Accounts', '~/Modules/Finance/Setup/BankAccounts.mix', 'CBA', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Ageing Slabs', '~/Modules/Finance/Setup/AgeingSlabs.mix', 'AGS', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Cash Flow Headings', '~/Modules/Finance/Setup/CashFlowHeadings.mix', 'CFH', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Cash Flow Setup', '~/Modules/Finance/Setup/CashFlowSetup.mix', 'CFS', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Cost Centers', '~/Modules/Finance/Setup/CostCenters.mix', 'CC', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Reports', NULL, 'FIR', 1, core.get_menu_id('FI')
UNION ALL SELECT 'Account Statement', '~/Modules/Finance/Reports/AccountStatement.mix', 'AS', 2, core.get_menu_id('FIR')
UNION ALL SELECT 'Trial Balance', '~/Modules/Finance/Reports/TrialBalance.mix', 'TB', 2, core.get_menu_id('FIR')
UNION ALL SELECT 'Profit & Loss Account', '~/Modules/Finance/Reports/ProfitAndLossAccount.mix', 'PLA', 2, core.get_menu_id('FIR')
UNION ALL SELECT 'Retained Earnings Statement', '~/Modules/Finance/Reports/RetainedEarnings.mix', 'RET', 2, core.get_menu_id('FIR')
UNION ALL SELECT 'Balance Sheet', '~/Modules/Finance/Reports/BalanceSheet.mix', 'BS', 2, core.get_menu_id('FIR')
UNION ALL SELECT 'Cash Flow', '~/Modules/Finance/Reports/CashFlow.mix', 'CF', 2, core.get_menu_id('FIR')
UNION ALL SELECT 'Tax Configuration', NULL, 'BOTC', 1, core.get_menu_id('BO')
UNION ALL SELECT 'Tax Master', '~/Modules/BackOffice/Tax/TaxMaster.mix', 'TXM', 2, core.get_menu_id('BOTC')
UNION ALL SELECT 'Tax Authorities', '~/Modules/BackOffice/Tax/TaxAuthorities.mix', 'TXA', 2, core.get_menu_id('BOTC')
UNION ALL SELECT 'Sales Tax Types', '~/Modules/BackOffice/Tax/SalesTaxTypes.mix', 'STXT', 2, core.get_menu_id('BOTC')
UNION ALL SELECT 'State Sales Taxes', '~/Modules/BackOffice/Tax/StateSalesTaxes.mix', 'STST', 2, core.get_menu_id('BOTC')
UNION ALL SELECT 'Counties Sales Taxes', '~/Modules/BackOffice/Tax/CountySalesTaxes.mix', 'CTST', 2, core.get_menu_id('BOTC')
UNION ALL SELECT 'Sales Taxes', '~/Modules/BackOffice/Tax/SalesTaxes.mix', 'STX', 2, core.get_menu_id('BOTC')
UNION ALL SELECT 'Sales Tax Details', '~/Modules/BackOffice/Tax/SalesTaxDetails.mix', 'STXD', 2, core.get_menu_id('BOTC')
UNION ALL SELECT 'Tax Exempt Types', '~/Modules/BackOffice/Tax/TaxExemptTypes.mix', 'TXEXT', 2, core.get_menu_id('BOTC')
UNION ALL SELECT 'Sales Tax Exempts', '~/Modules/BackOffice/Tax/SalesTaxExempts.mix', 'STXEX', 2, core.get_menu_id('BOTC')
UNION ALL SELECT 'Sales Tax Exempt Details', '~/Modules/BackOffice/Tax/SalesTaxExemptDetails.mix', 'STXEXD', 2, core.get_menu_id('BOTC')
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
UNION ALL SELECT 'Entity Setup', '~/Modules/BackOffice/Entities.mix', 'SES', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'Industry Setup', '~/Modules/BackOffice/Industries.mix', 'SIS', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'Country Setup', '~/Modules/BackOffice/Countries.mix', 'SCRS', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'State Setup', '~/Modules/BackOffice/States.mix', 'SSS', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'County Setup', '~/Modules/BackOffice/Counties.mix', 'SCTS', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'Fiscal Year Information', '~/Modules/BackOffice/FiscalYear.mix', 'SFY', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'Frequency & Fiscal Year Management', '~/Modules/BackOffice/Frequency.mix', 'SFR', 2, core.get_menu_id('SOS')
UNION ALL SELECT 'Policy Management', NULL, 'SPM', 1, core.get_menu_id('BO')
UNION ALL SELECT 'Voucher Verification Policy', '~/Modules/BackOffice/Policy/VoucherVerification.mix', 'SVV', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'Automatic Verification Policy', '~/Modules/BackOffice/Policy/AutoVerification.mix', 'SAV', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'Menu Access Policy', '~/Modules/BackOffice/Policy/MenuAccess.mix', 'SMA', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'GL Access Policy', '~/Modules/BackOffice/Policy/GLAccess.mix', 'SAP', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'Store Policy', '~/Modules/BackOffice/Policy/Store.mix', 'SSP', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'Admin Tools', NULL, 'SAT', 1, core.get_menu_id('BO')
UNION ALL SELECT 'Database Statistics', '~/Modules/BackOffice/Admin/DatabaseStatistics.mix', 'DBSTAT', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'Backup Database', '~/Modules/BackOffice/Admin/DatabaseBackup.mix', 'BAK', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'Change User Password', '~/Modules/BackOffice/Admin/ChangePassword.mix', 'PWD', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'Check Updates', '~/Modules/BackOffice/Admin/CheckUpdates.mix', 'UPD', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'Translate MixERP', '~/Modules/BackOffice/Admin/LocalizeMixERP.mix', 'TRA', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'One Time Setup', NULL, 'OTS', 1, core.get_menu_id('BO')
UNION ALL SELECT 'Opening Inventory', '~/Modules/BackOffice/OTS/OpeningInventory.mix', 'OTSI', 2, core.get_menu_id('OTS');


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
--FRENCH
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('SA'), 'fr-FR', 'ventes' UNION ALL
SELECT core.get_menu_id('PU'), 'fr-FR', 'achat' UNION ALL
SELECT core.get_menu_id('ITM'), 'fr-FR', 'Produits et Articles' UNION ALL
SELECT core.get_menu_id('FI'), 'fr-FR', 'financement' UNION ALL
SELECT core.get_menu_id('BO'), 'fr-FR', 'Back-Office' UNION ALL
SELECT core.get_menu_id('SAQ'), 'fr-FR', 'Sales & Devis' UNION ALL
SELECT core.get_menu_id('DRS'), 'fr-FR', 'Ventes directes' UNION ALL
SELECT core.get_menu_id('SQ'), 'fr-FR', 'Devis de vente' UNION ALL
SELECT core.get_menu_id('SO'), 'fr-FR', 'Commande client' UNION ALL
SELECT core.get_menu_id('SD'), 'fr-FR', 'Vente livraison' UNION ALL
SELECT core.get_menu_id('RFC'), 'fr-FR', 'Réception du client' UNION ALL
SELECT core.get_menu_id('SR'), 'fr-FR', 'Retour sur les ventes' UNION ALL
SELECT core.get_menu_id('SSM'), 'fr-FR', 'Le programme d''installation & entretien' UNION ALL
SELECT core.get_menu_id('ABS'), 'fr-FR', 'Dalle de bonus pour les vendeurs' UNION ALL
SELECT core.get_menu_id('BSD'), 'fr-FR', 'Détails du bonus dalle' UNION ALL
SELECT core.get_menu_id('SST'), 'fr-FR', 'Équipes de vente' UNION ALL
SELECT core.get_menu_id('SSA'), 'fr-FR', 'Vendeurs/vendeuses' UNION ALL
SELECT core.get_menu_id('BSA'), 'fr-FR', 'Affectation de dalle de bonus' UNION ALL
SELECT core.get_menu_id('LF'), 'fr-FR', 'Frais de retard' UNION ALL
SELECT core.get_menu_id('PAT'), 'fr-FR', 'Conditions de paiement' UNION ALL
SELECT core.get_menu_id('RI'), 'fr-FR', 'Factures récurrentes' UNION ALL
SELECT core.get_menu_id('RIS'), 'fr-FR', 'Paramètres des factures récurrentes' UNION ALL
SELECT core.get_menu_id('SAR'), 'fr-FR', 'Rapports sur les ventes' UNION ALL
SELECT core.get_menu_id('SAR-TSI'), 'fr-FR', 'Haut de la page points de vente' UNION ALL
SELECT core.get_menu_id('PUQ'), 'fr-FR', 'Achat & citation' UNION ALL
SELECT core.get_menu_id('DRP'), 'fr-FR', 'Achat direct' UNION ALL
SELECT core.get_menu_id('PO'), 'fr-FR', 'Bon de commande' UNION ALL
SELECT core.get_menu_id('PRO'), 'fr-FR', 'Achat Reorder' UNION ALL
SELECT core.get_menu_id('GRN'), 'fr-FR', 'Entrée GRN' UNION ALL
SELECT core.get_menu_id('PR'), 'fr-FR', 'Achat de retour' UNION ALL
SELECT core.get_menu_id('PUR'), 'fr-FR', 'Rapports d''achat' UNION ALL
SELECT core.get_menu_id('IIM'), 'fr-FR', 'Mouvements de stock' UNION ALL
SELECT core.get_menu_id('STJ'), 'fr-FR', 'Feuille de transfert de stock' UNION ALL
SELECT core.get_menu_id('STA'), 'fr-FR', 'Ajustements de stocks' UNION ALL
SELECT core.get_menu_id('ISM'), 'fr-FR', 'Le programme d''installation & entretien' UNION ALL
SELECT core.get_menu_id('STT'), 'fr-FR', 'Types de magasins' UNION ALL
SELECT core.get_menu_id('STO'), 'fr-FR', 'Magasins' UNION ALL
SELECT core.get_menu_id('SCS'), 'fr-FR', 'Installation de compteur' UNION ALL
SELECT core.get_menu_id('PT'), 'fr-FR', 'Types de partie' UNION ALL
SELECT core.get_menu_id('PA'), 'fr-FR', 'Comptes de tiers' UNION ALL
SELECT core.get_menu_id('PSA'), 'fr-FR', 'Adresses d''expédition' UNION ALL
SELECT core.get_menu_id('SSI'), 'fr-FR', 'Gestion des Articles' UNION ALL
SELECT core.get_menu_id('SSC'), 'fr-FR', 'Composé d''éléments' UNION ALL
SELECT core.get_menu_id('SSCD'), 'fr-FR', 'Détails de l''élément composé' UNION ALL
SELECT core.get_menu_id('ICP'), 'fr-FR', 'Prix de revient' UNION ALL
SELECT core.get_menu_id('ISP'), 'fr-FR', 'Prix de vente' UNION ALL
SELECT core.get_menu_id('SIG'), 'fr-FR', 'Groupes d''articles' UNION ALL
SELECT core.get_menu_id('SIT'), 'fr-FR', 'Types d''éléments' UNION ALL
SELECT core.get_menu_id('SSB'), 'fr-FR', 'Marques' UNION ALL
SELECT core.get_menu_id('UOM'), 'fr-FR', 'Unités de mesure' UNION ALL
SELECT core.get_menu_id('CUOM'), 'fr-FR', 'Composé d''unités de mesure' UNION ALL
SELECT core.get_menu_id('SHI'), 'fr-FR', 'Informations de l''expéditeur' UNION ALL
SELECT core.get_menu_id('IR'), 'fr-FR', 'Rapports' UNION ALL
SELECT core.get_menu_id('IAS'), 'fr-FR', 'Relevé de compte de l''inventaire' UNION ALL
SELECT core.get_menu_id('FTT'), 'fr-FR', 'Modèles de & de transactions' UNION ALL
SELECT core.get_menu_id('JVN'), 'fr-FR', 'Bon écriture' UNION ALL
SELECT core.get_menu_id('UER'), 'fr-FR', 'Mise à jour des taux de change' UNION ALL
SELECT core.get_menu_id('FVV'), 'fr-FR', 'Vérification du bon' UNION ALL
SELECT core.get_menu_id('EOD'), 'fr-FR', 'Fin de l''opération de la journée' UNION ALL
SELECT core.get_menu_id('FSM'), 'fr-FR', 'Le programme d''installation & entretien' UNION ALL
SELECT core.get_menu_id('COA'), 'fr-FR', 'Plan comptable' UNION ALL
SELECT core.get_menu_id('CUR'), 'fr-FR', 'Gestion de la devise' UNION ALL
SELECT core.get_menu_id('CBA'), 'fr-FR', 'Comptes bancaires' UNION ALL
SELECT core.get_menu_id('AGS'), 'fr-FR', 'Vieillissement des dalles' UNION ALL
SELECT core.get_menu_id('CFH'), 'fr-FR', 'Positions de trésorerie' UNION ALL
SELECT core.get_menu_id('CFS'), 'fr-FR', 'Configuration des flux de trésorerie' UNION ALL
SELECT core.get_menu_id('CC'), 'fr-FR', 'Centres de coûts' UNION ALL
SELECT core.get_menu_id('FIR'), 'fr-FR', 'Rapports' UNION ALL
SELECT core.get_menu_id('AS'), 'fr-FR', 'Relevé de compte' UNION ALL
SELECT core.get_menu_id('TB'), 'fr-FR', 'Balance de vérification' UNION ALL
SELECT core.get_menu_id('PLA'), 'fr-FR', 'Profit & compte de la perte' UNION ALL
SELECT core.get_menu_id('BS'), 'fr-FR', 'Bilan' UNION ALL
SELECT core.get_menu_id('RET'), 'fr-FR', 'Des Bénéfices Non Répartis' UNION ALL
SELECT core.get_menu_id('CF'), 'fr-FR', 'Flux de trésorerie' UNION ALL
SELECT core.get_menu_id('BOTC'), 'fr-FR', 'Configuration de l''impôt' UNION ALL
SELECT core.get_menu_id('TXM'), 'fr-FR', 'Maître de l''impôt' UNION ALL
SELECT core.get_menu_id('TXA'), 'fr-FR', 'Administration fiscale' UNION ALL
SELECT core.get_menu_id('STXT'), 'fr-FR', 'Types de taxe de vente' UNION ALL
SELECT core.get_menu_id('STST'), 'fr-FR', 'État des Taxes de vente' UNION ALL
SELECT core.get_menu_id('CTST'), 'fr-FR', 'Taxes de vente de comtés' UNION ALL
SELECT core.get_menu_id('STX'), 'fr-FR', 'Taxes de vente' UNION ALL
SELECT core.get_menu_id('STXD'), 'fr-FR', 'Détails de la taxe de vente' UNION ALL
SELECT core.get_menu_id('TXEXT'), 'fr-FR', 'Types exonérés de taxe' UNION ALL
SELECT core.get_menu_id('STXEX'), 'fr-FR', 'Exempte de la taxe de vente' UNION ALL
SELECT core.get_menu_id('STXEXD'), 'fr-FR', 'Détails exonéré de taxe de vente' UNION ALL
SELECT core.get_menu_id('SMP'), 'fr-FR', 'Divers paramètres' UNION ALL
SELECT core.get_menu_id('TRF'), 'fr-FR', 'Drapeaux' UNION ALL
SELECT core.get_menu_id('SEAR'), 'fr-FR', 'Rapports d''audit' UNION ALL
SELECT core.get_menu_id('SEAR-LV'), 'fr-FR', 'Vue de l''ouverture de session' UNION ALL
SELECT core.get_menu_id('SOS'), 'fr-FR', 'Installation de Office' UNION ALL
SELECT core.get_menu_id('SOB'), 'fr-FR', 'Bureau & de la direction générale de la configuration' UNION ALL
SELECT core.get_menu_id('SCR'), 'fr-FR', 'Installation de dépôt comptant' UNION ALL
SELECT core.get_menu_id('SDS'), 'fr-FR', 'Département installation' UNION ALL
SELECT core.get_menu_id('SRM'), 'fr-FR', 'Gestion des rôles' UNION ALL
SELECT core.get_menu_id('SUM'), 'fr-FR', 'Gestion des utilisateurs' UNION ALL
SELECT core.get_menu_id('SES'), 'fr-FR', 'Configuration de l''entité' UNION ALL
SELECT core.get_menu_id('SIS'), 'fr-FR', 'Installation de l''industrie' UNION ALL
SELECT core.get_menu_id('SCRS'), 'fr-FR', 'Programme d''installation de pays' UNION ALL
SELECT core.get_menu_id('SSS'), 'fr-FR', 'Installation de l''État' UNION ALL
SELECT core.get_menu_id('SCTS'), 'fr-FR', 'Comté de Setup' UNION ALL
SELECT core.get_menu_id('SFY'), 'fr-FR', 'Informations de l''exercice' UNION ALL
SELECT core.get_menu_id('SFR'), 'fr-FR', 'Fréquence & la gestion de l''exercice' UNION ALL
SELECT core.get_menu_id('SPM'), 'fr-FR', 'Gestion des stratégies de' UNION ALL
SELECT core.get_menu_id('SVV'), 'fr-FR', 'Politique sur la vérification bon' UNION ALL
SELECT core.get_menu_id('SAV'), 'fr-FR', 'Politique sur la vérification automatique' UNION ALL
SELECT core.get_menu_id('SMA'), 'fr-FR', 'Stratégie d''accès menu' UNION ALL
SELECT core.get_menu_id('SAP'), 'fr-FR', 'Stratégie d''accès GL' UNION ALL
SELECT core.get_menu_id('SSP'), 'fr-FR', 'Politique de boutique' UNION ALL
SELECT core.get_menu_id('SAT'), 'fr-FR', 'Outils d''administration' UNION ALL
SELECT core.get_menu_id('DBSTAT'), 'fr-FR', 'Outil de requête SQL' UNION ALL
SELECT core.get_menu_id('BAK'), 'fr-FR', 'Sauvegarde base de données' UNION ALL
SELECT core.get_menu_id('PWD'), 'fr-FR', 'Changer mot de passe utilisateur' UNION ALL
SELECT core.get_menu_id('UPD'), 'fr-FR', 'Vérifiez Mise à jour' UNION ALL
SELECT core.get_menu_id('TRA'), 'fr-FR', 'Traduire MixERP' UNION ALL
SELECT core.get_menu_id('OTS'), 'fr-FR', 'Un réglage de l''heure' UNION ALL
SELECT core.get_menu_id('OTSI'), 'fr-FR', 'Stock d''ouverture';

--GERMAN
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('STA'), 'de-DE', 'Auf Anpassungen' UNION ALL
SELECT core.get_menu_id('PWD'), 'de-DE', 'Benutzerpasswort ändern' UNION ALL
SELECT core.get_menu_id('UER'), 'de-DE', 'Update Wechselkurse' UNION ALL
SELECT core.get_menu_id('BO'), 'de-DE', 'Back Office' UNION ALL
SELECT core.get_menu_id('BAK'), 'de-DE', 'Datenbank sichern' UNION ALL
SELECT core.get_menu_id('TB'), 'de-DE', 'Rohbilanz' UNION ALL
SELECT core.get_menu_id('BS'), 'de-DE', 'Bilanz' UNION ALL
SELECT core.get_menu_id('TRF'), 'de-DE', 'Flaggen' UNION ALL
SELECT core.get_menu_id('BSA'), 'de-DE', 'Bonus Slab Zuordnung' UNION ALL
SELECT core.get_menu_id('BSD'), 'de-DE', 'Bonus Slab-Details' UNION ALL
SELECT core.get_menu_id('CC'), 'de-DE', 'Kostenstellen' UNION ALL
SELECT core.get_menu_id('PU'), 'de-DE', 'Kauf' UNION ALL
SELECT core.get_menu_id('PUQ'), 'de-DE', 'Einkauf & Quotation' UNION ALL
SELECT core.get_menu_id('DRP'), 'de-DE', 'Direktkauf' UNION ALL
SELECT core.get_menu_id('PRO'), 'de-DE', 'Kauf Reorder' UNION ALL
SELECT core.get_menu_id('PR'), 'de-DE', 'Kauf Return' UNION ALL
SELECT core.get_menu_id('SVV'), 'de-DE', 'Gutschein Verification Politik' UNION ALL
SELECT core.get_menu_id('PAT'), 'de-DE', 'Zahlungsbedingungen' UNION ALL
SELECT core.get_menu_id('BOTC'), 'de-DE', 'Steuerkonfiguration' UNION ALL
SELECT core.get_menu_id('SSM'), 'de-DE', 'Einrichtung und Wartung' UNION ALL
SELECT core.get_menu_id('ISM'), 'de-DE', 'Einrichtung und Wartung' UNION ALL
SELECT core.get_menu_id('FSM'), 'de-DE', 'Einrichtung und Wartung' UNION ALL
SELECT core.get_menu_id('CBA'), 'de-DE', 'Bankkonten' UNION ALL
SELECT core.get_menu_id('PA'), 'de-DE', 'Party-Accounts' UNION ALL
SELECT core.get_menu_id('SQ'), 'de-DE', 'Vertrieb Quotation' UNION ALL
SELECT core.get_menu_id('SA'), 'de-DE', 'Vertrieb' UNION ALL
SELECT core.get_menu_id('PLA'), 'de-DE', 'Gewinn- und Verlustrechnung' UNION ALL
SELECT core.get_menu_id('SSCD'), 'de-DE', 'Verbindung Einzelteil-Details' UNION ALL
SELECT core.get_menu_id('STXD'), 'de-DE', 'Umsatzsteuer-Details' UNION ALL
SELECT core.get_menu_id('PSA'), 'de-DE', 'Lieferadressen' UNION ALL
SELECT core.get_menu_id('SEAR-LV'), 'de-DE', 'Login Zeige' UNION ALL
SELECT core.get_menu_id('SD'), 'de-DE', 'Vertrieb Lieferung' UNION ALL
SELECT core.get_menu_id('SST'), 'de-DE', 'Vertriebsteams' UNION ALL
SELECT core.get_menu_id('SOB'), 'de-DE', 'Büro & Filiale einrichten' UNION ALL
SELECT core.get_menu_id('DBSTAT'), 'de-DE', 'Datenbankstatistik' UNION ALL
SELECT core.get_menu_id('IAS'), 'de-DE', 'Bestandskontoauszug' UNION ALL
SELECT core.get_menu_id('AS'), 'de-DE', 'Kontoauszug' UNION ALL
SELECT core.get_menu_id('RI'), 'de-DE', 'Wiederkehrende Rechnungen' UNION ALL
SELECT core.get_menu_id('SAT'), 'de-DE', 'Admin Tools' UNION ALL
SELECT core.get_menu_id('EOD'), 'de-DE', 'End of Day Betrieb' UNION ALL
SELECT core.get_menu_id('FI'), 'de-DE', 'Finanzen' UNION ALL
SELECT core.get_menu_id('TXEXT'), 'de-DE', 'Steuerbefreiung Typen' UNION ALL
SELECT core.get_menu_id('SFY'), 'de-DE', 'Geschäftsjahresinformationen' UNION ALL
SELECT core.get_menu_id('TXA'), 'de-DE', 'Steuerbehörden' UNION ALL
SELECT core.get_menu_id('CF'), 'de-DE', 'Cashflow' UNION ALL
SELECT core.get_menu_id('CFH'), 'de-DE', 'Kapitalflussüberschriften' UNION ALL
SELECT core.get_menu_id('SRM'), 'de-DE', 'Rollenverwaltung' UNION ALL
SELECT core.get_menu_id('SUM'), 'de-DE', 'Benutzerverwaltung' UNION ALL
SELECT core.get_menu_id('SFR'), 'de-DE', 'Frequenz & Geschäftsjahr Verwaltung' UNION ALL
SELECT core.get_menu_id('CUR'), 'de-DE', 'Währungsmanagement' UNION ALL
SELECT core.get_menu_id('SPM'), 'de-DE', 'Policy Management' UNION ALL
SELECT core.get_menu_id('SAP'), 'de-DE', 'Hauptbuch-Richtlinien' UNION ALL
SELECT core.get_menu_id('SIG'), 'de-DE', 'Artikelgruppen' UNION ALL
SELECT core.get_menu_id('STXEXD'), 'de-DE', 'Sales Tax Exempt Einzelheiten' UNION ALL
SELECT core.get_menu_id('CTST'), 'de-DE', 'Grafschaft Umsatzsteuer' UNION ALL
SELECT core.get_menu_id('STST'), 'de-DE', 'State Sales Taxes' UNION ALL
SELECT core.get_menu_id('STX'), 'de-DE', 'Umsatzsteuer' UNION ALL
SELECT core.get_menu_id('SHI'), 'de-DE', 'Shipper Informationen' UNION ALL
SELECT core.get_menu_id('SOS'), 'de-DE', 'Office Setup' UNION ALL
SELECT core.get_menu_id('OTSI'), 'de-DE', 'Öffnungs Inventar' UNION ALL
SELECT core.get_menu_id('STXEX'), 'de-DE', 'Umsatzsteuer befreit' UNION ALL
SELECT core.get_menu_id('SSI'), 'de-DE', 'Artikelpflege' UNION ALL
SELECT core.get_menu_id('SSC'), 'de-DE', 'Compound Artikel' UNION ALL
SELECT core.get_menu_id('SAR-TSI'), 'de-DE', 'Meistverkaufte Artikel' UNION ALL
SELECT core.get_menu_id('STJ'), 'de-DE', 'Umlagerung Blatt' UNION ALL
SELECT core.get_menu_id('JVN'), 'de-DE', 'Journal Gutschein Eintrag' UNION ALL
SELECT core.get_menu_id('AGS'), 'de-DE', 'Ageing Brammen' UNION ALL
SELECT core.get_menu_id('SSB'), 'de-DE', 'Brands' UNION ALL
SELECT core.get_menu_id('GRN'), 'de-DE', 'Wareneingang Hinweis Eintrag' UNION ALL
SELECT core.get_menu_id('TXM'), 'de-DE', 'Steuern Meister' UNION ALL
SELECT core.get_menu_id('IIM'), 'de-DE', 'Lagerbewegungen' UNION ALL
SELECT core.get_menu_id('OTS'), 'de-DE', 'Eine Zeiteinstellung' UNION ALL
SELECT core.get_menu_id('PO'), 'de-DE', 'Auftragsbestätigung' UNION ALL
SELECT core.get_menu_id('ISP'), 'de-DE', 'Verkaufs Preise' UNION ALL
SELECT core.get_menu_id('SMP'), 'de-DE', 'Verschiedene Parameter' UNION ALL
SELECT core.get_menu_id('SO'), 'de-DE', 'Sales Order' UNION ALL
SELECT core.get_menu_id('COA'), 'de-DE', 'Kontenplan' UNION ALL
SELECT core.get_menu_id('SSP'), 'de-DE', 'Speicher-Politik' UNION ALL
SELECT core.get_menu_id('SMA'), 'de-DE', 'Menü-Richtlinien' UNION ALL
SELECT core.get_menu_id('SAV'), 'de-DE', 'Automatische Verifikation Politik' UNION ALL
SELECT core.get_menu_id('ICP'), 'de-DE', 'Kosten Preise' UNION ALL
SELECT core.get_menu_id('ITM'), 'de-DE', 'Produkte & Angebote' UNION ALL
SELECT core.get_menu_id('RFC'), 'de-DE', 'Empfang vom Kunden' UNION ALL
SELECT core.get_menu_id('IR'), 'de-DE', 'Berichte' UNION ALL
SELECT core.get_menu_id('FIR'), 'de-DE', 'Berichte' UNION ALL
SELECT core.get_menu_id('SEAR'), 'de-DE', 'Prüfungsberichte' UNION ALL
SELECT core.get_menu_id('PUR'), 'de-DE', 'Kauf Berichte' UNION ALL
SELECT core.get_menu_id('SAR'), 'de-DE', 'Verkaufsberichte' UNION ALL
SELECT core.get_menu_id('SR'), 'de-DE', 'Absatzertrag' UNION ALL
SELECT core.get_menu_id('SCS'), 'de-DE', 'Zähler-Setup' UNION ALL
SELECT core.get_menu_id('SCTS'), 'de-DE', 'Grafschaft-Setup' UNION ALL
SELECT core.get_menu_id('SDS'), 'de-DE', 'Abteilung einrichten' UNION ALL
SELECT core.get_menu_id('SCR'), 'de-DE', 'Barzahlung Repository einrichten' UNION ALL
SELECT core.get_menu_id('SES'), 'de-DE', 'Entity-Setup' UNION ALL
SELECT core.get_menu_id('SSS'), 'de-DE', 'staatliche Einrichtung' UNION ALL
SELECT core.get_menu_id('SIS'), 'de-DE', 'Industrie-Setup' UNION ALL
SELECT core.get_menu_id('RIS'), 'de-DE', 'Wiederkehrende Rechnung einrichten' UNION ALL
SELECT core.get_menu_id('SCRS'), 'de-DE', 'Land-Setup' UNION ALL
SELECT core.get_menu_id('ABS'), 'de-DE', 'Bonus Bramme für Aussendienst' UNION ALL
SELECT core.get_menu_id('STO'), 'de-DE', 'Shops' UNION ALL
SELECT core.get_menu_id('LF'), 'de-DE', 'Späte Gebühren' UNION ALL
SELECT core.get_menu_id('STT'), 'de-DE', 'Shop Typen' UNION ALL
SELECT core.get_menu_id('STXT'), 'de-DE', 'Umsatzsteuerarten' UNION ALL
SELECT core.get_menu_id('PT'), 'de-DE', 'Party-Typen' UNION ALL
SELECT core.get_menu_id('FTT'), 'de-DE', 'Transaktionen und Vorlagen' UNION ALL
SELECT core.get_menu_id('UOM'), 'de-DE', 'Maßeinheiten' UNION ALL
SELECT core.get_menu_id('CUOM'), 'de-DE', 'Verbindung Maßeinheiten' UNION ALL
SELECT core.get_menu_id('DRS'), 'de-DE', 'Direct Sales' UNION ALL
SELECT core.get_menu_id('SAQ'), 'de-DE', 'Vertrieb Quotation' UNION ALL
SELECT core.get_menu_id('SSA'), 'de-DE', 'Aussendienst' UNION ALL
SELECT core.get_menu_id('FVV'), 'de-DE', 'Gutschein Verification';

--RUSSIAN
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('ABS'), 'ru-RU', 'Бонус Плиты для продавцов' UNION ALL
SELECT core.get_menu_id('AGS'), 'ru-RU', 'Старение плиты' UNION ALL
SELECT core.get_menu_id('AS'), 'ru-RU', 'Выписка по счету' UNION ALL
SELECT core.get_menu_id('BAK'), 'ru-RU', 'Резервное копирование базы данных' UNION ALL
SELECT core.get_menu_id('BO'), 'ru-RU', 'бэк-офис' UNION ALL
SELECT core.get_menu_id('BOTC'), 'ru-RU', 'Налоговый конфигурации' UNION ALL
SELECT core.get_menu_id('BS'), 'ru-RU', 'баланс' UNION ALL
SELECT core.get_menu_id('BSA'), 'ru-RU', 'Бонус Плиты Назначение' UNION ALL
SELECT core.get_menu_id('BSD'), 'ru-RU', 'Бонус Плиты Подробности' UNION ALL
SELECT core.get_menu_id('CBA'), 'ru-RU', 'Банковские счета' UNION ALL
SELECT core.get_menu_id('CC'), 'ru-RU', 'МВЗ' UNION ALL
SELECT core.get_menu_id('CF'), 'ru-RU', 'Денежный Поток' UNION ALL
SELECT core.get_menu_id('CFH'), 'ru-RU', 'Денежные средства Заголовки потока' UNION ALL
SELECT core.get_menu_id('COA'), 'ru-RU', 'План счетов' UNION ALL
SELECT core.get_menu_id('CTST'), 'ru-RU', 'Графство налог с продаж' UNION ALL
SELECT core.get_menu_id('CUOM'), 'ru-RU', 'Составные единицы измерения' UNION ALL
SELECT core.get_menu_id('CUR'), 'ru-RU', 'Валюта управления' UNION ALL
SELECT core.get_menu_id('DBSTAT'), 'ru-RU', 'Статистика базы данных' UNION ALL
SELECT core.get_menu_id('DRP'), 'ru-RU', 'Прямая Покупка' UNION ALL
SELECT core.get_menu_id('DRS'), 'ru-RU', 'Прямые продажи' UNION ALL
SELECT core.get_menu_id('EOD'), 'ru-RU', 'Конец операционного дня' UNION ALL
SELECT core.get_menu_id('FI'), 'ru-RU', 'финансы' UNION ALL
SELECT core.get_menu_id('FIR'), 'ru-RU', 'Отчеты' UNION ALL
SELECT core.get_menu_id('FSM'), 'ru-RU', 'Настройка и обслуживание' UNION ALL
SELECT core.get_menu_id('FTT'), 'ru-RU', 'Операции и шаблоны' UNION ALL
SELECT core.get_menu_id('FVV'), 'ru-RU', 'Ваучер Проверка' UNION ALL
SELECT core.get_menu_id('GRN'), 'ru-RU', 'Товары Поступило Примечание Вступление' UNION ALL
SELECT core.get_menu_id('IAS'), 'ru-RU', 'Выписка по счету Инвентарь' UNION ALL
SELECT core.get_menu_id('ICP'), 'ru-RU', 'Стоимость Цены' UNION ALL
SELECT core.get_menu_id('IIM'), 'ru-RU', 'Инвентаризация движения' UNION ALL
SELECT core.get_menu_id('IR'), 'ru-RU', 'Отчеты' UNION ALL
SELECT core.get_menu_id('ISM'), 'ru-RU', 'Настройка и обслуживание' UNION ALL
SELECT core.get_menu_id('ISP'), 'ru-RU', 'Отпускные цены' UNION ALL
SELECT core.get_menu_id('ITM'), 'ru-RU', 'Продукты и товары' UNION ALL
SELECT core.get_menu_id('JVN'), 'ru-RU', 'Журнал Ваучер запись' UNION ALL
SELECT core.get_menu_id('LF'), 'ru-RU', 'Штраф за просрочку платежей' UNION ALL
SELECT core.get_menu_id('OTS'), 'ru-RU', 'Один Время установки' UNION ALL
SELECT core.get_menu_id('OTSI'), 'ru-RU', 'Открытие Инвентарь' UNION ALL
SELECT core.get_menu_id('PA'), 'ru-RU', 'Вечеринка счета' UNION ALL
SELECT core.get_menu_id('PAT'), 'ru-RU', 'условия платежа' UNION ALL
SELECT core.get_menu_id('PLA'), 'ru-RU', 'Прибыль и убытках' UNION ALL
SELECT core.get_menu_id('PO'), 'ru-RU', 'Заказ На Покупку' UNION ALL
SELECT core.get_menu_id('PR'), 'ru-RU', 'Покупка Возврат' UNION ALL
SELECT core.get_menu_id('PRO'), 'ru-RU', 'Покупка Reorder' UNION ALL
SELECT core.get_menu_id('PSA'), 'ru-RU', 'Доставка Адреса' UNION ALL
SELECT core.get_menu_id('PT'), 'ru-RU', 'Вечеринка Типы' UNION ALL
SELECT core.get_menu_id('PU'), 'ru-RU', 'покупка' UNION ALL
SELECT core.get_menu_id('PUQ'), 'ru-RU', 'Покупка и цитаты' UNION ALL
SELECT core.get_menu_id('PUR'), 'ru-RU', 'Покупка Отчеты' UNION ALL
SELECT core.get_menu_id('PWD'), 'ru-RU', 'Изменить пользователя Пароль' UNION ALL
SELECT core.get_menu_id('RFC'), 'ru-RU', 'Получении от клиента' UNION ALL
SELECT core.get_menu_id('RI'), 'ru-RU', 'Повторяющиеся Счета' UNION ALL
SELECT core.get_menu_id('RIS'), 'ru-RU', 'Повторяющиеся установки Счет' UNION ALL
SELECT core.get_menu_id('SA'), 'ru-RU', 'продажа' UNION ALL
SELECT core.get_menu_id('SAP'), 'ru-RU', 'Политика доступа GL' UNION ALL
SELECT core.get_menu_id('SAQ'), 'ru-RU', 'Цитата продаж' UNION ALL
SELECT core.get_menu_id('SAR'), 'ru-RU', 'Отчеты по продажам' UNION ALL
SELECT core.get_menu_id('SAR-TSI'), 'ru-RU', 'Самые продаваемые товары' UNION ALL
SELECT core.get_menu_id('SAT'), 'ru-RU', 'Действия администратора Инструменты' UNION ALL
SELECT core.get_menu_id('SAV'), 'ru-RU', 'Политика Автоматическая проверка' UNION ALL
SELECT core.get_menu_id('SCR'), 'ru-RU', 'Настройка наличными Repository' UNION ALL
SELECT core.get_menu_id('SCRS'), 'ru-RU', 'Страна Setup' UNION ALL
SELECT core.get_menu_id('SCS'), 'ru-RU', 'Счетчик установки' UNION ALL
SELECT core.get_menu_id('SCTS'), 'ru-RU', 'Настройка County' UNION ALL
SELECT core.get_menu_id('SD'), 'ru-RU', 'продажи Доставка' UNION ALL
SELECT core.get_menu_id('SDS'), 'ru-RU', 'Настройка Департамент' UNION ALL
SELECT core.get_menu_id('SEAR'), 'ru-RU', 'Финансовые отчеты' UNION ALL
SELECT core.get_menu_id('SEAR-LV'), 'ru-RU', 'Войти Посмотреть' UNION ALL
SELECT core.get_menu_id('SES'), 'ru-RU', 'Entity Setup' UNION ALL
SELECT core.get_menu_id('SFR'), 'ru-RU', 'Управление частотой и финансовый год' UNION ALL
SELECT core.get_menu_id('SFY'), 'ru-RU', 'Финансовый год Информация' UNION ALL
SELECT core.get_menu_id('SHI'), 'ru-RU', 'Грузовладелец информация' UNION ALL
SELECT core.get_menu_id('SIG'), 'ru-RU', 'Группы товаров' UNION ALL
SELECT core.get_menu_id('SIS'), 'ru-RU', 'Настройка Промышленность' UNION ALL
SELECT core.get_menu_id('SMA'), 'ru-RU', 'Меню политика доступа' UNION ALL
SELECT core.get_menu_id('SMP'), 'ru-RU', 'Разное параметры' UNION ALL
SELECT core.get_menu_id('SO'), 'ru-RU', 'продажи Заказать' UNION ALL
SELECT core.get_menu_id('SOB'), 'ru-RU', 'Управление и отделения установки' UNION ALL
SELECT core.get_menu_id('SOS'), 'ru-RU', 'Программа установки Office' UNION ALL
SELECT core.get_menu_id('SPM'), 'ru-RU', 'Управление политиками' UNION ALL
SELECT core.get_menu_id('SQ'), 'ru-RU', 'Цитата продаж' UNION ALL
SELECT core.get_menu_id('SR'), 'ru-RU', 'продажи Вернуться' UNION ALL
SELECT core.get_menu_id('SRM'), 'ru-RU', 'Роль управления' UNION ALL
SELECT core.get_menu_id('SSA'), 'ru-RU', 'Продавцы' UNION ALL
SELECT core.get_menu_id('SSB'), 'ru-RU', 'Бренды' UNION ALL
SELECT core.get_menu_id('SSC'), 'ru-RU', 'Составные товары' UNION ALL
SELECT core.get_menu_id('SSCD'), 'ru-RU', 'Соединение Пункт подробности' UNION ALL
SELECT core.get_menu_id('SSI'), 'ru-RU', 'Пункт технического обслуживания' UNION ALL
SELECT core.get_menu_id('SSM'), 'ru-RU', 'Настройка и обслуживание' UNION ALL
SELECT core.get_menu_id('SSP'), 'ru-RU', 'Политика магазина' UNION ALL
SELECT core.get_menu_id('SSS'), 'ru-RU', 'Государственный Setup' UNION ALL
SELECT core.get_menu_id('SST'), 'ru-RU', 'Продажи команды' UNION ALL
SELECT core.get_menu_id('STA'), 'ru-RU', 'Сток Корректировки' UNION ALL
SELECT core.get_menu_id('STJ'), 'ru-RU', 'Перемещение запаса журнал' UNION ALL
SELECT core.get_menu_id('STO'), 'ru-RU', 'магазины' UNION ALL
SELECT core.get_menu_id('STST'), 'ru-RU', 'Государственные налогов с продаж' UNION ALL
SELECT core.get_menu_id('STT'), 'ru-RU', 'Типы магазин' UNION ALL
SELECT core.get_menu_id('STX'), 'ru-RU', 'налог с продаж' UNION ALL
SELECT core.get_menu_id('STXD'), 'ru-RU', 'Налог на продажу Подробнее' UNION ALL
SELECT core.get_menu_id('STXEX'), 'ru-RU', 'Налог на продажу льготников' UNION ALL
SELECT core.get_menu_id('STXEXD'), 'ru-RU', 'Налог на продажу Освобожденные Подробнее' UNION ALL
SELECT core.get_menu_id('STXT'), 'ru-RU', 'Типы Налог на продажу' UNION ALL
SELECT core.get_menu_id('SUM'), 'ru-RU', 'Управление пользователями' UNION ALL
SELECT core.get_menu_id('SVV'), 'ru-RU', 'Политика Ваучер Проверка' UNION ALL
SELECT core.get_menu_id('TB'), 'ru-RU', 'пробный баланс' UNION ALL
SELECT core.get_menu_id('TRF'), 'ru-RU', 'Флаги' UNION ALL
SELECT core.get_menu_id('TXA'), 'ru-RU', 'Налоговые органы' UNION ALL
SELECT core.get_menu_id('TXEXT'), 'ru-RU', 'Освобождаются от налогообложения Типы' UNION ALL
SELECT core.get_menu_id('TXM'), 'ru-RU', 'Налоговый Мастер' UNION ALL
SELECT core.get_menu_id('UER'), 'ru-RU', 'Update Wechselkurse' UNION ALL
SELECT core.get_menu_id('UOM'), 'ru-RU', 'Единицы измерения';

--JAPANESE

INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('ABS'), 'ja-JP', '販売員のためのボーナススラブ' UNION ALL
SELECT core.get_menu_id('AGS'), 'ja-JP', '高齢スラブ' UNION ALL
SELECT core.get_menu_id('AS'), 'ja-JP', '取引明細書' UNION ALL
SELECT core.get_menu_id('BAK'), 'ja-JP', 'バックアップ·データベース' UNION ALL
SELECT core.get_menu_id('BO'), 'ja-JP', 'バックオフィス' UNION ALL
SELECT core.get_menu_id('BOTC'), 'ja-JP', '税の設定' UNION ALL
SELECT core.get_menu_id('BS'), 'ja-JP', 'バランスシート' UNION ALL
SELECT core.get_menu_id('BSA'), 'ja-JP', 'ボーナススラブの割り当て' UNION ALL
SELECT core.get_menu_id('BSD'), 'ja-JP', 'ボーナススラブ詳細' UNION ALL
SELECT core.get_menu_id('CBA'), 'ja-JP', '銀行口座' UNION ALL
SELECT core.get_menu_id('CC'), 'ja-JP', '原価センタ' UNION ALL
SELECT core.get_menu_id('CF'), 'ja-JP', '現金流量' UNION ALL
SELECT core.get_menu_id('CFH'), 'ja-JP', 'キャッシュフロー見出し' UNION ALL
SELECT core.get_menu_id('COA'), 'ja-JP', '勘定科目一覧表' UNION ALL
SELECT core.get_menu_id('CTST'), 'ja-JP', '郡の売上税' UNION ALL
SELECT core.get_menu_id('CUOM'), 'ja-JP', 'メジャーの化合物単位' UNION ALL
SELECT core.get_menu_id('CUR'), 'ja-JP', '通貨管理' UNION ALL
SELECT core.get_menu_id('DBSTAT'), 'ja-JP', 'データベース統計' UNION ALL
SELECT core.get_menu_id('DRP'), 'ja-JP', '直接購入' UNION ALL
SELECT core.get_menu_id('DRS'), 'ja-JP', '直販' UNION ALL
SELECT core.get_menu_id('EOD'), 'ja-JP', 'デイ操作の終了' UNION ALL
SELECT core.get_menu_id('FI'), 'ja-JP', '金融' UNION ALL
SELECT core.get_menu_id('FIR'), 'ja-JP', 'レポート' UNION ALL
SELECT core.get_menu_id('FSM'), 'ja-JP', 'セットアップとメンテナンス' UNION ALL
SELECT core.get_menu_id('FTT'), 'ja-JP', '取引＆テンプレート' UNION ALL
SELECT core.get_menu_id('FVV'), 'ja-JP', 'バウチャーの検証' UNION ALL
SELECT core.get_menu_id('GRN'), 'ja-JP', 'GRNエントリー' UNION ALL
SELECT core.get_menu_id('IAS'), 'ja-JP', '在庫勘定書' UNION ALL
SELECT core.get_menu_id('ICP'), 'ja-JP', 'コスト価格' UNION ALL
SELECT core.get_menu_id('IIM'), 'ja-JP', '在庫変動' UNION ALL
SELECT core.get_menu_id('IR'), 'ja-JP', 'レポート' UNION ALL
SELECT core.get_menu_id('ISM'), 'ja-JP', 'セットアップとメンテナンス' UNION ALL
SELECT core.get_menu_id('ISP'), 'ja-JP', '販売価格' UNION ALL
SELECT core.get_menu_id('ITM'), 'ja-JP', '製品＆アイテム' UNION ALL
SELECT core.get_menu_id('JVN'), 'ja-JP', 'ジャーナルバウチャーエントリー' UNION ALL
SELECT core.get_menu_id('LF'), 'ja-JP', '延滞料' UNION ALL
SELECT core.get_menu_id('OTS'), 'ja-JP', 'ワンタイムの設定' UNION ALL
SELECT core.get_menu_id('OTSI'), 'ja-JP', 'オープニングインベントリ' UNION ALL
SELECT core.get_menu_id('PA'), 'ja-JP', 'パーティーのアカウント' UNION ALL
SELECT core.get_menu_id('PAT'), 'ja-JP', '支払条件' UNION ALL
SELECT core.get_menu_id('PLA'), 'ja-JP', '損益勘定' UNION ALL
SELECT core.get_menu_id('PO'), 'ja-JP', '注文書' UNION ALL
SELECT core.get_menu_id('PR'), 'ja-JP', '購入戻る' UNION ALL
SELECT core.get_menu_id('PRO'), 'ja-JP', '購入並べ替え' UNION ALL
SELECT core.get_menu_id('PSA'), 'ja-JP', '配送先住所' UNION ALL
SELECT core.get_menu_id('PT'), 'ja-JP', 'パーティーの種類' UNION ALL
SELECT core.get_menu_id('PU'), 'ja-JP', '購入' UNION ALL
SELECT core.get_menu_id('PUQ'), 'ja-JP', '購入＆見積' UNION ALL
SELECT core.get_menu_id('PUR'), 'ja-JP', '購入レポート' UNION ALL
SELECT core.get_menu_id('PWD'), 'ja-JP', 'ユーザーパスワードの変更' UNION ALL
SELECT core.get_menu_id('RFC'), 'ja-JP', 'お客様からの領収書' UNION ALL
SELECT core.get_menu_id('RI'), 'ja-JP', '定期的な請求書' UNION ALL
SELECT core.get_menu_id('RIS'), 'ja-JP', '経常請求書のセットアップ' UNION ALL
SELECT core.get_menu_id('SA'), 'ja-JP', 'セールス' UNION ALL
SELECT core.get_menu_id('SAP'), 'ja-JP', 'GLアクセスポリシー' UNION ALL
SELECT core.get_menu_id('SAQ'), 'ja-JP', 'セールス＆見積' UNION ALL
SELECT core.get_menu_id('SAR'), 'ja-JP', '営業レポート' UNION ALL
SELECT core.get_menu_id('SAR-TSI'), 'ja-JP', '人気商品ランキング' UNION ALL
SELECT core.get_menu_id('SAT'), 'ja-JP', '管理ツール' UNION ALL
SELECT core.get_menu_id('SAV'), 'ja-JP', '自動検証ポリシー' UNION ALL
SELECT core.get_menu_id('SCR'), 'ja-JP', '現金リポジトリのセットアップ' UNION ALL
SELECT core.get_menu_id('SCRS'), 'ja-JP', '国のセットアップ' UNION ALL
SELECT core.get_menu_id('SCS'), 'ja-JP', 'カウンターのセットアップ' UNION ALL
SELECT core.get_menu_id('SCTS'), 'ja-JP', '郡のセットアップ' UNION ALL
SELECT core.get_menu_id('SD'), 'ja-JP', '販売配達' UNION ALL
SELECT core.get_menu_id('SDS'), 'ja-JP', '部署のセットアップ' UNION ALL
SELECT core.get_menu_id('SEAR'), 'ja-JP', '監査レポート' UNION ALL
SELECT core.get_menu_id('SEAR-LV'), 'ja-JP', 'ログインを見る' UNION ALL
SELECT core.get_menu_id('SES'), 'ja-JP', 'エンティティのセットアップ' UNION ALL
SELECT core.get_menu_id('SFR'), 'ja-JP', '周波数＆会計年度の経営' UNION ALL
SELECT core.get_menu_id('SFY'), 'ja-JP', '年度情報' UNION ALL
SELECT core.get_menu_id('SHI'), 'ja-JP', '荷送人情報' UNION ALL
SELECT core.get_menu_id('SIG'), 'ja-JP', 'アイテムのグループ' UNION ALL
SELECT core.get_menu_id('SIS'), 'ja-JP', '業界のセットアップ' UNION ALL
SELECT core.get_menu_id('SMA'), 'ja-JP', 'メニューアクセスポリシー' UNION ALL
SELECT core.get_menu_id('SMP'), 'ja-JP', 'その他のパラメータ' UNION ALL
SELECT core.get_menu_id('SO'), 'ja-JP', '販売注文' UNION ALL
SELECT core.get_menu_id('SOB'), 'ja-JP', 'オフィス＆支店セットアップ' UNION ALL
SELECT core.get_menu_id('SOS'), 'ja-JP', 'Officeセットアップ' UNION ALL
SELECT core.get_menu_id('SPM'), 'ja-JP', '政策管理' UNION ALL
SELECT core.get_menu_id('SQ'), 'ja-JP', '販売見積' UNION ALL
SELECT core.get_menu_id('SR'), 'ja-JP', '販売戻る' UNION ALL
SELECT core.get_menu_id('SRM'), 'ja-JP', 'ロール管理' UNION ALL
SELECT core.get_menu_id('SSA'), 'ja-JP', '販売員' UNION ALL
SELECT core.get_menu_id('SSB'), 'ja-JP', 'ブランド' UNION ALL
SELECT core.get_menu_id('SSC'), 'ja-JP', '複合アイテム' UNION ALL
SELECT core.get_menu_id('SSCD'), 'ja-JP', '複合商品詳細' UNION ALL
SELECT core.get_menu_id('SSI'), 'ja-JP', 'アイテムのメンテナンス' UNION ALL
SELECT core.get_menu_id('SSM'), 'ja-JP', 'セットアップとメンテナンス' UNION ALL
SELECT core.get_menu_id('SSP'), 'ja-JP', 'ストアポリシー' UNION ALL
SELECT core.get_menu_id('SSS'), 'ja-JP', '国家のセットアップ' UNION ALL
SELECT core.get_menu_id('SST'), 'ja-JP', 'セールスチーム' UNION ALL
SELECT core.get_menu_id('STA'), 'ja-JP', 'ストック調整' UNION ALL
SELECT core.get_menu_id('STJ'), 'ja-JP', '株式移転ジャーナル' UNION ALL
SELECT core.get_menu_id('STO'), 'ja-JP', 'ストア' UNION ALL
SELECT core.get_menu_id('STST'), 'ja-JP', '状態の売上税' UNION ALL
SELECT core.get_menu_id('STT'), 'ja-JP', 'ストア型' UNION ALL
SELECT core.get_menu_id('STX'), 'ja-JP', '売上税' UNION ALL
SELECT core.get_menu_id('STXD'), 'ja-JP', '消費税の詳細' UNION ALL
SELECT core.get_menu_id('STXEX'), 'ja-JP', '売上税免除' UNION ALL
SELECT core.get_menu_id('STXEXD'), 'ja-JP', '売上税免除詳細' UNION ALL
SELECT core.get_menu_id('STXT'), 'ja-JP', '売上税タイプ' UNION ALL
SELECT core.get_menu_id('SUM'), 'ja-JP', 'ユーザー管理' UNION ALL
SELECT core.get_menu_id('SVV'), 'ja-JP', 'バウチャーの検証方針' UNION ALL
SELECT core.get_menu_id('TB'), 'ja-JP', '試算表' UNION ALL
SELECT core.get_menu_id('TRF'), 'ja-JP', 'フラグ' UNION ALL
SELECT core.get_menu_id('TXA'), 'ja-JP', '税務当局' UNION ALL
SELECT core.get_menu_id('TXEXT'), 'ja-JP', '税免除の種類' UNION ALL
SELECT core.get_menu_id('TXM'), 'ja-JP', '税マスター' UNION ALL
SELECT core.get_menu_id('UER'), 'ja-JP', '更新の為替レート' UNION ALL
SELECT core.get_menu_id('UOM'), 'ja-JP', '測定の単位';


--SPANISH

INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('ABS'), 'es-ES', 'Losa bonificación sobre los Vendedores' UNION ALL
SELECT core.get_menu_id('AGS'), 'es-ES', 'Losas Envejecimiento' UNION ALL
SELECT core.get_menu_id('AS'), 'es-ES', 'Estado de Cuenta' UNION ALL
SELECT core.get_menu_id('BAK'), 'es-ES', 'Base de datos de copia de seguridad' UNION ALL
SELECT core.get_menu_id('BO'), 'es-ES', 'Back Office' UNION ALL
SELECT core.get_menu_id('BOTC'), 'es-ES', 'Configuración de Impuestos' UNION ALL
SELECT core.get_menu_id('BS'), 'es-ES', 'el balance' UNION ALL
SELECT core.get_menu_id('BSA'), 'es-ES', 'Bono Slab Asignación' UNION ALL
SELECT core.get_menu_id('BSD'), 'es-ES', 'Bono Slab Detalles' UNION ALL
SELECT core.get_menu_id('CBA'), 'es-ES', 'Cuentas bancarias' UNION ALL
SELECT core.get_menu_id('CC'), 'es-ES', 'Centros de costes' UNION ALL
SELECT core.get_menu_id('CF'), 'es-ES', 'Flujo De Fondos' UNION ALL
SELECT core.get_menu_id('CFH'), 'es-ES', 'Cash Flow encabezamientos' UNION ALL
SELECT core.get_menu_id('COA'), 'es-ES', 'Plan General de Contabilidad' UNION ALL
SELECT core.get_menu_id('CTST'), 'es-ES', 'Impuestos Condados de venta' UNION ALL
SELECT core.get_menu_id('CUOM'), 'es-ES', 'Unidades compuestas de Medida' UNION ALL
SELECT core.get_menu_id('CUR'), 'es-ES', 'Gestión de moneda' UNION ALL
SELECT core.get_menu_id('DBSTAT'), 'es-ES', 'Base de Estadísticas' UNION ALL
SELECT core.get_menu_id('DRP'), 'es-ES', 'Compra Directa' UNION ALL
SELECT core.get_menu_id('DRS'), 'es-ES', 'Venta Directa' UNION ALL
SELECT core.get_menu_id('EOD'), 'es-ES', 'Al final del día de operación' UNION ALL
SELECT core.get_menu_id('FI'), 'es-ES', 'Finanzas' UNION ALL
SELECT core.get_menu_id('FIR'), 'es-ES', 'Informes' UNION ALL
SELECT core.get_menu_id('FSM'), 'es-ES', 'Creación y Mantenimiento' UNION ALL
SELECT core.get_menu_id('FTT'), 'es-ES', 'Transacciones y plantillas' UNION ALL
SELECT core.get_menu_id('FVV'), 'es-ES', 'Verificación Voucher' UNION ALL
SELECT core.get_menu_id('GRN'), 'es-ES', 'Productos de Entrada Nota Recibido' UNION ALL
SELECT core.get_menu_id('IAS'), 'es-ES', 'Estado de Cuenta de Inventario' UNION ALL
SELECT core.get_menu_id('ICP'), 'es-ES', 'Los precios de coste' UNION ALL
SELECT core.get_menu_id('IIM'), 'es-ES', 'Movimientos de Inventario' UNION ALL
SELECT core.get_menu_id('IR'), 'es-ES', 'Informes' UNION ALL
SELECT core.get_menu_id('ISM'), 'es-ES', 'Creación y Mantenimiento' UNION ALL
SELECT core.get_menu_id('ISP'), 'es-ES', 'los precios de venta' UNION ALL
SELECT core.get_menu_id('ITM'), 'es-ES', 'Productos y Artículos' UNION ALL
SELECT core.get_menu_id('JVN'), 'es-ES', 'Entrada Comprobante de Diario' UNION ALL
SELECT core.get_menu_id('LF'), 'es-ES', 'Recargos' UNION ALL
SELECT core.get_menu_id('OTS'), 'es-ES', 'Una configuración de la hora' UNION ALL
SELECT core.get_menu_id('OTSI'), 'es-ES', 'Inventario de apertura' UNION ALL
SELECT core.get_menu_id('PA'), 'es-ES', 'Cuentas del Partido' UNION ALL
SELECT core.get_menu_id('PAT'), 'es-ES', 'Condiciones de pago' UNION ALL
SELECT core.get_menu_id('PLA'), 'es-ES', 'Winst- en verliesrekening...' UNION ALL
SELECT core.get_menu_id('PO'), 'es-ES', 'Orden De Compra' UNION ALL
SELECT core.get_menu_id('PR'), 'es-ES', 'Compra de Retorno' UNION ALL
SELECT core.get_menu_id('PRO'), 'es-ES', 'Compra de reorden' UNION ALL
SELECT core.get_menu_id('PSA'), 'es-ES', 'Direcciones de Envío' UNION ALL
SELECT core.get_menu_id('PT'), 'es-ES', 'Tipos Party' UNION ALL
SELECT core.get_menu_id('PU'), 'es-ES', 'Compra' UNION ALL
SELECT core.get_menu_id('PUQ'), 'es-ES', 'Compra y Cotización' UNION ALL
SELECT core.get_menu_id('PUR'), 'es-ES', 'Informes de Compra' UNION ALL
SELECT core.get_menu_id('PWD'), 'es-ES', 'Cambiar contraseña de usuario' UNION ALL
SELECT core.get_menu_id('RFC'), 'es-ES', 'Recibo del Cliente' UNION ALL
SELECT core.get_menu_id('RI'), 'es-ES', 'facturas recurrentes' UNION ALL
SELECT core.get_menu_id('RIS'), 'es-ES', 'Configuración Factura Recurrente' UNION ALL
SELECT core.get_menu_id('SA'), 'es-ES', 'venta' UNION ALL
SELECT core.get_menu_id('SAP'), 'es-ES', 'Política de Acceso General Ledger' UNION ALL
SELECT core.get_menu_id('SAQ'), 'es-ES', 'Ventas y Cotización' UNION ALL
SELECT core.get_menu_id('SAR'), 'es-ES', 'Informes de ventas' UNION ALL
SELECT core.get_menu_id('SAR-TSI'), 'es-ES', 'Top artículos más vendidos' UNION ALL
SELECT core.get_menu_id('SAT'), 'es-ES', 'Herramientas de administración' UNION ALL
SELECT core.get_menu_id('SAV'), 'es-ES', 'Política Automático de Verificación' UNION ALL
SELECT core.get_menu_id('SCR'), 'es-ES', 'Configuración del depósito de efectivo' UNION ALL
SELECT core.get_menu_id('SCRS'), 'es-ES', 'Configuración de país' UNION ALL
SELECT core.get_menu_id('SCS'), 'es-ES', 'Configuración Contador' UNION ALL
SELECT core.get_menu_id('SCTS'), 'es-ES', 'Configuración del Condado' UNION ALL
SELECT core.get_menu_id('SD'), 'es-ES', 'Entrega Ventas' UNION ALL
SELECT core.get_menu_id('SDS'), 'es-ES', 'afdeling Setup' UNION ALL
SELECT core.get_menu_id('SEAR'), 'es-ES', 'Informes de Auditoría' UNION ALL
SELECT core.get_menu_id('SEAR-LV'), 'es-ES', 'Entrar Ver' UNION ALL
SELECT core.get_menu_id('SES'), 'es-ES', 'Configuración entidad' UNION ALL
SELECT core.get_menu_id('SFR'), 'es-ES', 'Gestión de Frecuencias y el año fiscal' UNION ALL
SELECT core.get_menu_id('SFY'), 'es-ES', 'Información Fiscal Año' UNION ALL
SELECT core.get_menu_id('SHI'), 'es-ES', 'Información Shipper' UNION ALL
SELECT core.get_menu_id('SIG'), 'es-ES', 'los grupos de artículos' UNION ALL
SELECT core.get_menu_id('SIS'), 'es-ES', 'Configuración de la Industria' UNION ALL
SELECT core.get_menu_id('SMA'), 'es-ES', 'Menú política de acceso' UNION ALL
SELECT core.get_menu_id('SMP'), 'es-ES', 'Parámetros Varios' UNION ALL
SELECT core.get_menu_id('SO'), 'es-ES', 'Orden de Venta' UNION ALL
SELECT core.get_menu_id('SOB'), 'es-ES', 'Instalación de Office y Poder' UNION ALL
SELECT core.get_menu_id('SOS'), 'es-ES', 'instalación de Office' UNION ALL
SELECT core.get_menu_id('SPM'), 'es-ES', 'Gestión de Políticas' UNION ALL
SELECT core.get_menu_id('SQ'), 'es-ES', 'Cita Ventas' UNION ALL
SELECT core.get_menu_id('SR'), 'es-ES', 'Volver Ventas' UNION ALL
SELECT core.get_menu_id('SRM'), 'es-ES', 'Administración de funciones' UNION ALL
SELECT core.get_menu_id('SSA'), 'es-ES', 'vendedores' UNION ALL
SELECT core.get_menu_id('SSB'), 'es-ES', 'Marcas' UNION ALL
SELECT core.get_menu_id('SSC'), 'es-ES', 'compuesto Artículos' UNION ALL
SELECT core.get_menu_id('SSCD'), 'es-ES', 'Compuesto Detalles del artículo' UNION ALL
SELECT core.get_menu_id('SSI'), 'es-ES', 'Mantenimiento de artículos' UNION ALL
SELECT core.get_menu_id('SSM'), 'es-ES', 'Creación y Mantenimiento' UNION ALL
SELECT core.get_menu_id('SSP'), 'es-ES', 'política de la tienda' UNION ALL
SELECT core.get_menu_id('SSS'), 'es-ES', 'Configuración Estado' UNION ALL
SELECT core.get_menu_id('SST'), 'es-ES', 'equipo de ventas' UNION ALL
SELECT core.get_menu_id('STA'), 'es-ES', 'Ajustes de archivo' UNION ALL
SELECT core.get_menu_id('STJ'), 'es-ES', 'Diario Stock Transfer' UNION ALL
SELECT core.get_menu_id('STO'), 'es-ES', 'Tiendas' UNION ALL
SELECT core.get_menu_id('STST'), 'es-ES', 'Impuestos estatales' UNION ALL
SELECT core.get_menu_id('STT'), 'es-ES', 'Tipo de tienda' UNION ALL
SELECT core.get_menu_id('STX'), 'es-ES', 'impuestos a las Ventas' UNION ALL
SELECT core.get_menu_id('STXD'), 'es-ES', 'Detalles de impuesto sobre las ventas' UNION ALL
SELECT core.get_menu_id('STXEX'), 'es-ES', 'Exime de impuestos de ventas' UNION ALL
SELECT core.get_menu_id('STXEXD'), 'es-ES', 'SalesTax Detalles Exentos' UNION ALL
SELECT core.get_menu_id('STXT'), 'es-ES', 'Tipos de Impuestos de Ventas' UNION ALL
SELECT core.get_menu_id('SUM'), 'es-ES', 'Gestión de usuarios' UNION ALL
SELECT core.get_menu_id('SVV'), 'es-ES', 'Vale Política de Verificación' UNION ALL
SELECT core.get_menu_id('TB'), 'es-ES', 'balance' UNION ALL
SELECT core.get_menu_id('TRF'), 'es-ES', 'Banderas' UNION ALL
SELECT core.get_menu_id('TXA'), 'es-ES', 'Agencia Tributaria' UNION ALL
SELECT core.get_menu_id('TXEXT'), 'es-ES', 'Impuestos Tipos Exentos' UNION ALL
SELECT core.get_menu_id('TXM'), 'es-ES', 'Maestro de Impuestos' UNION ALL
SELECT core.get_menu_id('UER'), 'es-ES', 'Actualización Cotizaciones' UNION ALL
SELECT core.get_menu_id('UOM'), 'es-ES', 'Unidades de Medida';

--DUTCH
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('ABS'), 'nl-NL', 'Bonus Slab voor Verkopers' UNION ALL
SELECT core.get_menu_id('AGS'), 'nl-NL', 'Vergrijzing Platen' UNION ALL
SELECT core.get_menu_id('AS'), 'nl-NL', 'rekeningafschrift' UNION ALL
SELECT core.get_menu_id('BAK'), 'nl-NL', 'backup Database' UNION ALL
SELECT core.get_menu_id('BO'), 'nl-NL', 'Back Office' UNION ALL
SELECT core.get_menu_id('BOTC'), 'nl-NL', 'belasting Configuratie' UNION ALL
SELECT core.get_menu_id('BS'), 'nl-NL', 'balans' UNION ALL
SELECT core.get_menu_id('BSA'), 'nl-NL', 'Bonus Slab Opdracht' UNION ALL
SELECT core.get_menu_id('BSD'), 'nl-NL', 'Bonus Slab Details' UNION ALL
SELECT core.get_menu_id('CBA'), 'nl-NL', 'bankrekeningen' UNION ALL
SELECT core.get_menu_id('CC'), 'nl-NL', 'Kostenplaatsen' UNION ALL
SELECT core.get_menu_id('CF'), 'nl-NL', 'Geldstroom' UNION ALL
SELECT core.get_menu_id('CFH'), 'nl-NL', 'Cash Flow Koppen' UNION ALL
SELECT core.get_menu_id('COA'), 'nl-NL', 'Rekeningschema' UNION ALL
SELECT core.get_menu_id('CTST'), 'nl-NL', 'Provincies Sales Belastingen' UNION ALL
SELECT core.get_menu_id('CUOM'), 'nl-NL', 'Verbinding meeteenheden' UNION ALL
SELECT core.get_menu_id('CUR'), 'nl-NL', 'valuta management' UNION ALL
SELECT core.get_menu_id('DBSTAT'), 'nl-NL', 'Database statistieken' UNION ALL
SELECT core.get_menu_id('DRP'), 'nl-NL', 'direct Aankoop' UNION ALL
SELECT core.get_menu_id('DRS'), 'nl-NL', 'Direct Sales' UNION ALL
SELECT core.get_menu_id('EOD'), 'nl-NL', 'Einde van de dag Operatie' UNION ALL
SELECT core.get_menu_id('FI'), 'nl-NL', 'Financiën' UNION ALL
SELECT core.get_menu_id('FIR'), 'nl-NL', 'rapporten' UNION ALL
SELECT core.get_menu_id('FSM'), 'nl-NL', 'Installatie & Onderhoud' UNION ALL
SELECT core.get_menu_id('FTT'), 'nl-NL', 'Transactions & Templates' UNION ALL
SELECT core.get_menu_id('FVV'), 'nl-NL', 'voucher Verificatie' UNION ALL
SELECT core.get_menu_id('GRN'), 'nl-NL', 'Goederen Ontvangen Opmerking Entry' UNION ALL
SELECT core.get_menu_id('IAS'), 'nl-NL', 'Inventaris rekeningafschrift' UNION ALL
SELECT core.get_menu_id('ICP'), 'nl-NL', 'kostprijzen' UNION ALL
SELECT core.get_menu_id('IIM'), 'nl-NL', 'inventaris Movements' UNION ALL
SELECT core.get_menu_id('IR'), 'nl-NL', 'rapporten' UNION ALL
SELECT core.get_menu_id('ISM'), 'nl-NL', 'Installatie & Onderhoud' UNION ALL
SELECT core.get_menu_id('ISP'), 'nl-NL', 'verkoopprijs' UNION ALL
SELECT core.get_menu_id('ITM'), 'nl-NL', 'Producten en Artikelen' UNION ALL
SELECT core.get_menu_id('JVN'), 'nl-NL', 'Journal Voucher Entry' UNION ALL
SELECT core.get_menu_id('LF'), 'nl-NL', 'late Vergoedingen' UNION ALL
SELECT core.get_menu_id('OTS'), 'nl-NL', 'One Time Setup' UNION ALL
SELECT core.get_menu_id('OTSI'), 'nl-NL', 'Het openen van Inventory' UNION ALL
SELECT core.get_menu_id('PA'), 'nl-NL', 'partij Accounts' UNION ALL
SELECT core.get_menu_id('PAT'), 'nl-NL', 'Betaalvoorwaarden' UNION ALL
SELECT core.get_menu_id('PLA'), 'nl-NL', 'Winst- en verliesrekening' UNION ALL
SELECT core.get_menu_id('PO'), 'nl-NL', 'direct Aankoop' UNION ALL
SELECT core.get_menu_id('PR'), 'nl-NL', 'aankoop Return' UNION ALL
SELECT core.get_menu_id('PRO'), 'nl-NL', 'aankoop opnieuw ordenen' UNION ALL
SELECT core.get_menu_id('PSA'), 'nl-NL', 'afleveradres' UNION ALL
SELECT core.get_menu_id('PT'), 'nl-NL', 'partij Types' UNION ALL
SELECT core.get_menu_id('PU'), 'nl-NL', 'aankoop' UNION ALL
SELECT core.get_menu_id('PUQ'), 'nl-NL', 'Inkoop & Offerte' UNION ALL
SELECT core.get_menu_id('PUR'), 'nl-NL', 'aankoop Rapporten' UNION ALL
SELECT core.get_menu_id('PWD'), 'nl-NL', 'Change User Password' UNION ALL
SELECT core.get_menu_id('RFC'), 'nl-NL', 'Ontvangst van de klant' UNION ALL
SELECT core.get_menu_id('RI'), 'nl-NL', 'terugkerende facturen' UNION ALL
SELECT core.get_menu_id('RIS'), 'nl-NL', 'Terugkerende Invoice Setup' UNION ALL
SELECT core.get_menu_id('SA'), 'nl-NL', 'Sales' UNION ALL
SELECT core.get_menu_id('SAP'), 'nl-NL', 'GL Access Policy' UNION ALL
SELECT core.get_menu_id('SAQ'), 'nl-NL', 'Sales & Quotation' UNION ALL
SELECT core.get_menu_id('SAR'), 'nl-NL', 'Sales Reports' UNION ALL
SELECT core.get_menu_id('SAR-TSI'), 'nl-NL', 'Top Selling Items' UNION ALL
SELECT core.get_menu_id('SAT'), 'nl-NL', 'Admin Tools' UNION ALL
SELECT core.get_menu_id('SAV'), 'nl-NL', 'Automatisch Verificatie Beleid' UNION ALL
SELECT core.get_menu_id('SCR'), 'nl-NL', 'Cash Repository Setup' UNION ALL
SELECT core.get_menu_id('SCRS'), 'nl-NL', 'land Setup' UNION ALL
SELECT core.get_menu_id('SCS'), 'nl-NL', 'Counter Setup' UNION ALL
SELECT core.get_menu_id('SCTS'), 'nl-NL', 'County Setup' UNION ALL
SELECT core.get_menu_id('SD'), 'nl-NL', 'Sales Delivery' UNION ALL
SELECT core.get_menu_id('SDS'), 'nl-NL', 'afdeling Setup' UNION ALL
SELECT core.get_menu_id('SEAR'), 'nl-NL', 'auditrapporten' UNION ALL
SELECT core.get_menu_id('SEAR-LV'), 'nl-NL', 'Inloggen View' UNION ALL
SELECT core.get_menu_id('SES'), 'nl-NL', 'entiteit Setup' UNION ALL
SELECT core.get_menu_id('SFR'), 'nl-NL', 'Frequentie & boekjaar management' UNION ALL
SELECT core.get_menu_id('SFY'), 'nl-NL', 'Fiscale Jaar Informatie' UNION ALL
SELECT core.get_menu_id('SHI'), 'nl-NL', 'verlader Informatie' UNION ALL
SELECT core.get_menu_id('SIG'), 'nl-NL', 'Item Groepen' UNION ALL
SELECT core.get_menu_id('SIS'), 'nl-NL', 'industrie Setup' UNION ALL
SELECT core.get_menu_id('SMA'), 'nl-NL', 'Menu Access Policy' UNION ALL
SELECT core.get_menu_id('SMP'), 'nl-NL', 'Diverse parameters' UNION ALL
SELECT core.get_menu_id('SO'), 'nl-NL', 'Sales Order' UNION ALL
SELECT core.get_menu_id('SOB'), 'nl-NL', 'Office & Branch Setup' UNION ALL
SELECT core.get_menu_id('SOS'), 'nl-NL', 'Office Setup' UNION ALL
SELECT core.get_menu_id('SPM'), 'nl-NL', 'Policy Management' UNION ALL
SELECT core.get_menu_id('SQ'), 'nl-NL', 'Sales Offerte' UNION ALL
SELECT core.get_menu_id('SR'), 'nl-NL', 'Sales Return' UNION ALL
SELECT core.get_menu_id('SRM'), 'nl-NL', 'rol management' UNION ALL
SELECT core.get_menu_id('SSA'), 'nl-NL', 'verkopers' UNION ALL
SELECT core.get_menu_id('SSB'), 'nl-NL', 'merk' UNION ALL
SELECT core.get_menu_id('SSC'), 'nl-NL', 'verbinding items' UNION ALL
SELECT core.get_menu_id('SSCD'), 'nl-NL', 'Verbinding Item Details' UNION ALL
SELECT core.get_menu_id('SSI'), 'nl-NL', 'Item Onderhoud' UNION ALL
SELECT core.get_menu_id('SSM'), 'nl-NL', 'Installatie & Onderhoud' UNION ALL
SELECT core.get_menu_id('SSP'), 'nl-NL', 'Store-beleid' UNION ALL
SELECT core.get_menu_id('SSS'), 'nl-NL', 'Staat Setup' UNION ALL
SELECT core.get_menu_id('SST'), 'nl-NL', 'Sales Teams' UNION ALL
SELECT core.get_menu_id('STA'), 'nl-NL', 'Stock Aanpassingen' UNION ALL
SELECT core.get_menu_id('STJ'), 'nl-NL', 'Stock Transfer Journal' UNION ALL
SELECT core.get_menu_id('STO'), 'nl-NL', 'winkel' UNION ALL
SELECT core.get_menu_id('STST'), 'nl-NL', 'Staat Sales Belastingen' UNION ALL
SELECT core.get_menu_id('STT'), 'nl-NL', 'Store Type' UNION ALL
SELECT core.get_menu_id('STX'), 'nl-NL', 'verkoop Belastingen' UNION ALL
SELECT core.get_menu_id('STXD'), 'nl-NL', 'verkoop Belastingen Detail' UNION ALL
SELECT core.get_menu_id('STXEX'), 'nl-NL', 'Verkoop Tax vrijstelt' UNION ALL
SELECT core.get_menu_id('STXEXD'), 'nl-NL', 'Verkoop vrijgesteld van belasting Details' UNION ALL
SELECT core.get_menu_id('STXT'), 'nl-NL', 'Verkoop Belasting Types' UNION ALL
SELECT core.get_menu_id('SUM'), 'nl-NL', 'user Management' UNION ALL
SELECT core.get_menu_id('SVV'), 'nl-NL', 'Voucher Verificatie Beleid' UNION ALL
SELECT core.get_menu_id('TB'), 'nl-NL', 'Trial Balance' UNION ALL
SELECT core.get_menu_id('TRF'), 'nl-NL', 'vlaggen' UNION ALL
SELECT core.get_menu_id('TXA'), 'nl-NL', 'Belastingdienst' UNION ALL
SELECT core.get_menu_id('TXEXT'), 'nl-NL', 'Vrijgesteld van belasting Types' UNION ALL
SELECT core.get_menu_id('TXM'), 'nl-NL', 'belasting Master' UNION ALL
SELECT core.get_menu_id('UER'), 'nl-NL', 'Bijwerken Wisselkoersen' UNION ALL
SELECT core.get_menu_id('UOM'), 'nl-NL', 'Maateenheden';

--SIMPLIFIED CHINESE
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('ABS'), 'zh-CN', '奖金为平板销售人员' UNION ALL
SELECT core.get_menu_id('AGS'), 'zh-CN', '老龄板坯' UNION ALL
SELECT core.get_menu_id('AS'), 'zh-CN', '帐户对帐单' UNION ALL
SELECT core.get_menu_id('BAK'), 'zh-CN', '备份数据库' UNION ALL
SELECT core.get_menu_id('BO'), 'zh-CN', '后台' UNION ALL
SELECT core.get_menu_id('BOTC'), 'zh-CN', '税务配置' UNION ALL
SELECT core.get_menu_id('BS'), 'zh-CN', '资产负债表' UNION ALL
SELECT core.get_menu_id('BSA'), 'zh-CN', '奖金分配板' UNION ALL
SELECT core.get_menu_id('BSD'), 'zh-CN', '奖励板详细' UNION ALL
SELECT core.get_menu_id('CBA'), 'zh-CN', '银行账户' UNION ALL
SELECT core.get_menu_id('CC'), 'zh-CN', '成本中心' UNION ALL
SELECT core.get_menu_id('CF'), 'zh-CN', '现金周转' UNION ALL
SELECT core.get_menu_id('CFH'), 'zh-CN', '现金流标题' UNION ALL
SELECT core.get_menu_id('COA'), 'zh-CN', '科目表' UNION ALL
SELECT core.get_menu_id('CTST'), 'zh-CN', '县销售税' UNION ALL
SELECT core.get_menu_id('CUOM'), 'zh-CN', '计量单位复合' UNION ALL
SELECT core.get_menu_id('CUR'), 'zh-CN', '货币管理' UNION ALL
SELECT core.get_menu_id('DBSTAT'), 'zh-CN', '数据库统计' UNION ALL
SELECT core.get_menu_id('DRP'), 'zh-CN', '直接购买' UNION ALL
SELECT core.get_menu_id('DRS'), 'zh-CN', '直销' UNION ALL
SELECT core.get_menu_id('EOD'), 'zh-CN', '天运结束' UNION ALL
SELECT core.get_menu_id('FI'), 'zh-CN', '金融' UNION ALL
SELECT core.get_menu_id('FIR'), 'zh-CN', '报告' UNION ALL
SELECT core.get_menu_id('FSM'), 'zh-CN', '安装与维护' UNION ALL
SELECT core.get_menu_id('FTT'), 'zh-CN', '交易和模板' UNION ALL
SELECT core.get_menu_id('FVV'), 'zh-CN', '优惠券验证' UNION ALL
SELECT core.get_menu_id('GRN'), 'zh-CN', '收货票据' UNION ALL
SELECT core.get_menu_id('IAS'), 'zh-CN', '库存帐户对帐单' UNION ALL
SELECT core.get_menu_id('ICP'), 'zh-CN', '成本价格' UNION ALL
SELECT core.get_menu_id('IIM'), 'zh-CN', '库存变动' UNION ALL
SELECT core.get_menu_id('IR'), 'zh-CN', '报告' UNION ALL
SELECT core.get_menu_id('ISM'), 'zh-CN', '安装与维护' UNION ALL
SELECT core.get_menu_id('ISP'), 'zh-CN', '销售价格' UNION ALL
SELECT core.get_menu_id('ITM'), 'zh-CN', '产品与项目' UNION ALL
SELECT core.get_menu_id('JVN'), 'zh-CN', '杂志凭证录入' UNION ALL
SELECT core.get_menu_id('LF'), 'zh-CN', '滞纳金' UNION ALL
SELECT core.get_menu_id('OTS'), 'zh-CN', '一时间设置' UNION ALL
SELECT core.get_menu_id('OTSI'), 'zh-CN', '期初库存' UNION ALL
SELECT core.get_menu_id('PA'), 'zh-CN', '党的账户' UNION ALL
SELECT core.get_menu_id('PAT'), 'zh-CN', '付款条款' UNION ALL
SELECT core.get_menu_id('PLA'), 'zh-CN', '损益表' UNION ALL
SELECT core.get_menu_id('PO'), 'zh-CN', '采购订单' UNION ALL
SELECT core.get_menu_id('PR'), 'zh-CN', '购买返回' UNION ALL
SELECT core.get_menu_id('PRO'), 'zh-CN', '购买重新排序' UNION ALL
SELECT core.get_menu_id('PSA'), 'zh-CN', '送货地址' UNION ALL
SELECT core.get_menu_id('PT'), 'zh-CN', '党的类型' UNION ALL
SELECT core.get_menu_id('PU'), 'zh-CN', '购买' UNION ALL
SELECT core.get_menu_id('PUQ'), 'zh-CN', '购买＆报价' UNION ALL
SELECT core.get_menu_id('PUR'), 'zh-CN', '购买报告' UNION ALL
SELECT core.get_menu_id('PWD'), 'zh-CN', '更改用户密码' UNION ALL
SELECT core.get_menu_id('RFC'), 'zh-CN', '单据' UNION ALL
SELECT core.get_menu_id('RI'), 'zh-CN', '经常性发票' UNION ALL
SELECT core.get_menu_id('RIS'), 'zh-CN', '经常性发票设置' UNION ALL
SELECT core.get_menu_id('SA'), 'zh-CN', '销售' UNION ALL
SELECT core.get_menu_id('SAP'), 'zh-CN', '总帐访问策略' UNION ALL
SELECT core.get_menu_id('SAQ'), 'zh-CN', '销售报价' UNION ALL
SELECT core.get_menu_id('SAR'), 'zh-CN', '销售报告' UNION ALL
SELECT core.get_menu_id('SAR-TSI'), 'zh-CN', '最畅销的项目' UNION ALL
SELECT core.get_menu_id('SAT'), 'zh-CN', '管理工具' UNION ALL
SELECT core.get_menu_id('SAV'), 'zh-CN', '自动验证策略' UNION ALL
SELECT core.get_menu_id('SCR'), 'zh-CN', '现金库安装' UNION ALL
SELECT core.get_menu_id('SCRS'), 'zh-CN', '国家设置' UNION ALL
SELECT core.get_menu_id('SCS'), 'zh-CN', '计数器设置' UNION ALL
SELECT core.get_menu_id('SCTS'), 'zh-CN', '县设置' UNION ALL
SELECT core.get_menu_id('SD'), 'zh-CN', '销售出库单' UNION ALL
SELECT core.get_menu_id('SDS'), 'zh-CN', '部门设置' UNION ALL
SELECT core.get_menu_id('SEAR'), 'zh-CN', '审计报告' UNION ALL
SELECT core.get_menu_id('SEAR-LV'), 'zh-CN', '登录查看' UNION ALL
SELECT core.get_menu_id('SES'), 'zh-CN', '实体设置' UNION ALL
SELECT core.get_menu_id('SFR'), 'zh-CN', '频率和会计年度管理' UNION ALL
SELECT core.get_menu_id('SFY'), 'zh-CN', '财年资料' UNION ALL
SELECT core.get_menu_id('SHI'), 'zh-CN', '发货人信息' UNION ALL
SELECT core.get_menu_id('SIG'), 'zh-CN', '项目组' UNION ALL
SELECT core.get_menu_id('SIS'), 'zh-CN', '行业设置' UNION ALL
SELECT core.get_menu_id('SMA'), 'zh-CN', '菜单访问策略' UNION ALL
SELECT core.get_menu_id('SMP'), 'zh-CN', '其他参数' UNION ALL
SELECT core.get_menu_id('SO'), 'zh-CN', '销售订单' UNION ALL
SELECT core.get_menu_id('SOB'), 'zh-CN', '办公室及分公司安装' UNION ALL
SELECT core.get_menu_id('SOS'), 'zh-CN', '办公室 格局' UNION ALL
SELECT core.get_menu_id('SPM'), 'zh-CN', '策略管理' UNION ALL
SELECT core.get_menu_id('SQ'), 'zh-CN', '销售报价' UNION ALL
SELECT core.get_menu_id('SR'), 'zh-CN', '销售退货' UNION ALL
SELECT core.get_menu_id('SRM'), 'zh-CN', '角色管理' UNION ALL
SELECT core.get_menu_id('SSA'), 'zh-CN', '营业员' UNION ALL
SELECT core.get_menu_id('SSB'), 'zh-CN', '品牌' UNION ALL
SELECT core.get_menu_id('SSC'), 'zh-CN', '复合项目' UNION ALL
SELECT core.get_menu_id('SSCD'), 'zh-CN', '复合项目详情' UNION ALL
SELECT core.get_menu_id('SSI'), 'zh-CN', '项目维护' UNION ALL
SELECT core.get_menu_id('SSM'), 'zh-CN', '安装与维护' UNION ALL
SELECT core.get_menu_id('SSP'), 'zh-CN', '存储策略' UNION ALL
SELECT core.get_menu_id('SSS'), 'zh-CN', '国家设置' UNION ALL
SELECT core.get_menu_id('SST'), 'zh-CN', '销售团队' UNION ALL
SELECT core.get_menu_id('STA'), 'zh-CN', '库存调整' UNION ALL
SELECT core.get_menu_id('STJ'), 'zh-CN', '股权转让杂志' UNION ALL
SELECT core.get_menu_id('STO'), 'zh-CN', '店' UNION ALL
SELECT core.get_menu_id('STST'), 'zh-CN', '州销售税' UNION ALL
SELECT core.get_menu_id('STT'), 'zh-CN', '商铺类型' UNION ALL
SELECT core.get_menu_id('STX'), 'zh-CN', '销售税' UNION ALL
SELECT core.get_menu_id('STXD'), 'zh-CN', '销售税细节' UNION ALL
SELECT core.get_menu_id('STXEX'), 'zh-CN', '销售税豁免' UNION ALL
SELECT core.get_menu_id('STXEXD'), 'zh-CN', '销售税豁免详细' UNION ALL
SELECT core.get_menu_id('STXT'), 'zh-CN', '销售税类型' UNION ALL
SELECT core.get_menu_id('SUM'), 'zh-CN', '用户管理' UNION ALL
SELECT core.get_menu_id('SVV'), 'zh-CN', '券验证策略' UNION ALL
SELECT core.get_menu_id('TB'), 'zh-CN', 'Trial Balance' UNION ALL
SELECT core.get_menu_id('TRF'), 'zh-CN', '旗' UNION ALL
SELECT core.get_menu_id('TXA'), 'zh-CN', '税务机关' UNION ALL
SELECT core.get_menu_id('TXEXT'), 'zh-CN', '免税类型' UNION ALL
SELECT core.get_menu_id('TXM'), 'zh-CN', '税务硕士' UNION ALL
SELECT core.get_menu_id('UER'), 'zh-CN', '更新汇率' UNION ALL
SELECT core.get_menu_id('UOM'), 'zh-CN', '计量单位';

--PORTUGUESE

INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('ABS'), 'pt-PT', 'Slab Bonus para vendedores' UNION ALL
SELECT core.get_menu_id('AGS'), 'pt-PT', 'Lajes Envelhecimento' UNION ALL
SELECT core.get_menu_id('AS'), 'pt-PT', 'Extrato de Conta' UNION ALL
SELECT core.get_menu_id('BAK'), 'pt-PT', 'backup Database' UNION ALL
SELECT core.get_menu_id('BO'), 'pt-PT', 'Back Office' UNION ALL
SELECT core.get_menu_id('BOTC'), 'pt-PT', 'Configuração do Imposto' UNION ALL
SELECT core.get_menu_id('BS'), 'pt-PT', 'Balanço' UNION ALL
SELECT core.get_menu_id('BSA'), 'pt-PT', 'Bonus Slab Assignment' UNION ALL
SELECT core.get_menu_id('BSD'), 'pt-PT', 'Bonus Slab Detalhes' UNION ALL
SELECT core.get_menu_id('CBA'), 'pt-PT', 'Contas Bancárias' UNION ALL
SELECT core.get_menu_id('CC'), 'pt-PT', 'Centros de custo' UNION ALL
SELECT core.get_menu_id('CF'), 'pt-PT', 'Fluxo De Caixa' UNION ALL
SELECT core.get_menu_id('CFH'), 'pt-PT', 'Fluxo de Caixa Headings' UNION ALL
SELECT core.get_menu_id('COA'), 'pt-PT', 'Plano de Contas' UNION ALL
SELECT core.get_menu_id('CTST'), 'pt-PT', 'Impostos Concelhos de vendas' UNION ALL
SELECT core.get_menu_id('CUOM'), 'pt-PT', 'Units compostas de medida' UNION ALL
SELECT core.get_menu_id('CUR'), 'pt-PT', 'Gestão de moeda' UNION ALL
SELECT core.get_menu_id('DBSTAT'), 'pt-PT', 'Estatísticas do Banco de Dados' UNION ALL
SELECT core.get_menu_id('DRP'), 'pt-PT', 'Compra Direta' UNION ALL
SELECT core.get_menu_id('DRS'), 'pt-PT', 'Vendas Diretas' UNION ALL
SELECT core.get_menu_id('EOD'), 'pt-PT', 'Fim da Operação Dia' UNION ALL
SELECT core.get_menu_id('FI'), 'pt-PT', 'finanças' UNION ALL
SELECT core.get_menu_id('FIR'), 'pt-PT', 'relatórios' UNION ALL
SELECT core.get_menu_id('FSM'), 'pt-PT', 'Configuração e Manutenção' UNION ALL
SELECT core.get_menu_id('FTT'), 'pt-PT', 'Transactions & Templates' UNION ALL
SELECT core.get_menu_id('FVV'), 'pt-PT', 'Verificação de Vouchers' UNION ALL
SELECT core.get_menu_id('GRN'), 'pt-PT', 'Mercadorias entrada de nota recebida' UNION ALL
SELECT core.get_menu_id('IAS'), 'pt-PT', 'Extrato da conta de estoques' UNION ALL
SELECT core.get_menu_id('ICP'), 'pt-PT', 'preços de custo' UNION ALL
SELECT core.get_menu_id('IIM'), 'pt-PT', 'Movimentos de inventário' UNION ALL
SELECT core.get_menu_id('IR'), 'pt-PT', 'relatórios' UNION ALL
SELECT core.get_menu_id('ISM'), 'pt-PT', 'Configuração e Manutenção' UNION ALL
SELECT core.get_menu_id('ISP'), 'pt-PT', 'Os preços de venda' UNION ALL
SELECT core.get_menu_id('ITM'), 'pt-PT', 'Produtos e Itens' UNION ALL
SELECT core.get_menu_id('JVN'), 'pt-PT', 'Jornal Vale Entry' UNION ALL
SELECT core.get_menu_id('LF'), 'pt-PT', 'taxas atrasadas' UNION ALL
SELECT core.get_menu_id('OTS'), 'pt-PT', 'One Time Setup' UNION ALL
SELECT core.get_menu_id('OTSI'), 'pt-PT', 'Inventário de abertura' UNION ALL
SELECT core.get_menu_id('PA'), 'pt-PT', 'Contas do partido' UNION ALL
SELECT core.get_menu_id('PAT'), 'pt-PT', 'Condições de pagamento' UNION ALL
SELECT core.get_menu_id('PLA'), 'pt-PT', 'Demonstração de Resultados' UNION ALL
SELECT core.get_menu_id('PO'), 'pt-PT', 'Ordem De Compra' UNION ALL
SELECT core.get_menu_id('PR'), 'pt-PT', 'compra Retorno' UNION ALL
SELECT core.get_menu_id('PRO'), 'pt-PT', 'compra Reordenar' UNION ALL
SELECT core.get_menu_id('PSA'), 'pt-PT', 'Endereços para envio' UNION ALL
SELECT core.get_menu_id('PT'), 'pt-PT', 'Tipos partido' UNION ALL
SELECT core.get_menu_id('PU'), 'pt-PT', 'compra' UNION ALL
SELECT core.get_menu_id('PUQ'), 'pt-PT', 'Compra & Cotação' UNION ALL
SELECT core.get_menu_id('PUR'), 'pt-PT', 'Relatórios de compra' UNION ALL
SELECT core.get_menu_id('PWD'), 'pt-PT', 'Alterar senha do usuário' UNION ALL
SELECT core.get_menu_id('RFC'), 'pt-PT', 'Recibo do Cliente' UNION ALL
SELECT core.get_menu_id('RI'), 'pt-PT', 'facturas recorrentes' UNION ALL
SELECT core.get_menu_id('RIS'), 'pt-PT', 'Setup Invoice Recorrente' UNION ALL
SELECT core.get_menu_id('SA'), 'pt-PT', 'de vendas' UNION ALL
SELECT core.get_menu_id('SAP'), 'pt-PT', 'GL Política de Acesso' UNION ALL
SELECT core.get_menu_id('SAQ'), 'pt-PT', 'Vendas e cotação' UNION ALL
SELECT core.get_menu_id('SAR'), 'pt-PT', 'Relatórios de vendas' UNION ALL
SELECT core.get_menu_id('SAR-TSI'), 'pt-PT', 'Itens mais vendidos' UNION ALL
SELECT core.get_menu_id('SAT'), 'pt-PT', 'Ferramentas de Administração' UNION ALL
SELECT core.get_menu_id('SAV'), 'pt-PT', 'Política de verificação automática' UNION ALL
SELECT core.get_menu_id('SCR'), 'pt-PT', 'Setup Dinheiro Repository' UNION ALL
SELECT core.get_menu_id('SCRS'), 'pt-PT', 'Setup país' UNION ALL
SELECT core.get_menu_id('SCS'), 'pt-PT', 'Setup contador' UNION ALL
SELECT core.get_menu_id('SCTS'), 'pt-PT', 'Setup County' UNION ALL
SELECT core.get_menu_id('SD'), 'pt-PT', 'Entrega de Vendas' UNION ALL
SELECT core.get_menu_id('SDS'), 'pt-PT', 'Setup Departamento' UNION ALL
SELECT core.get_menu_id('SEAR'), 'pt-PT', 'Relatórios de Auditoria' UNION ALL
SELECT core.get_menu_id('SEAR-LV'), 'pt-PT', 'Entrada Vista' UNION ALL
SELECT core.get_menu_id('SES'), 'pt-PT', 'Setup Entity' UNION ALL
SELECT core.get_menu_id('SFR'), 'pt-PT', 'Gestão de frequência e de Ano Fiscal' UNION ALL
SELECT core.get_menu_id('SFY'), 'pt-PT', 'Fiscal Informações Ano' UNION ALL
SELECT core.get_menu_id('SHI'), 'pt-PT', 'Informações shipper' UNION ALL
SELECT core.get_menu_id('SIG'), 'pt-PT', 'Grupos de itens' UNION ALL
SELECT core.get_menu_id('SIS'), 'pt-PT', 'Setup indústria' UNION ALL
SELECT core.get_menu_id('SMA'), 'pt-PT', 'Política de acesso ao menu' UNION ALL
SELECT core.get_menu_id('SMP'), 'pt-PT', 'Parâmetros Diversos' UNION ALL
SELECT core.get_menu_id('SO'), 'pt-PT', 'pedido de Vendas' UNION ALL
SELECT core.get_menu_id('SOB'), 'pt-PT', 'Escritório & Filial Setup' UNION ALL
SELECT core.get_menu_id('SOS'), 'pt-PT', 'Instalação do Office' UNION ALL
SELECT core.get_menu_id('SPM'), 'pt-PT', 'Gestão de Políticas' UNION ALL
SELECT core.get_menu_id('SQ'), 'pt-PT', 'cotação de vendas' UNION ALL
SELECT core.get_menu_id('SR'), 'pt-PT', 'Retorno de Vendas' UNION ALL
SELECT core.get_menu_id('SRM'), 'pt-PT', 'Gerenciamento de funções' UNION ALL
SELECT core.get_menu_id('SSA'), 'pt-PT', 'vendedores' UNION ALL
SELECT core.get_menu_id('SSB'), 'pt-PT', 'marcas' UNION ALL
SELECT core.get_menu_id('SSC'), 'pt-PT', 'Itens Composto' UNION ALL
SELECT core.get_menu_id('SSCD'), 'pt-PT', 'Detalhes Composto item' UNION ALL
SELECT core.get_menu_id('SSI'), 'pt-PT', 'item de manutenção' UNION ALL
SELECT core.get_menu_id('SSM'), 'pt-PT', 'Configuração e Manutenção' UNION ALL
SELECT core.get_menu_id('SSP'), 'pt-PT', 'Política da loja' UNION ALL
SELECT core.get_menu_id('SSS'), 'pt-PT', 'Setup Estado' UNION ALL
SELECT core.get_menu_id('SST'), 'pt-PT', 'equipes de Vendas' UNION ALL
SELECT core.get_menu_id('STA'), 'pt-PT', 'ajuste de estoques' UNION ALL
SELECT core.get_menu_id('STJ'), 'pt-PT', 'Jornal da Transferência' UNION ALL
SELECT core.get_menu_id('STO'), 'pt-PT', 'Stores' UNION ALL
SELECT core.get_menu_id('STST'), 'pt-PT', 'Impostos estaduais sobre vendas' UNION ALL
SELECT core.get_menu_id('STT'), 'pt-PT', 'tipos de armazenamento' UNION ALL
SELECT core.get_menu_id('STX'), 'pt-PT', 'Impostos sobre Vendas' UNION ALL
SELECT core.get_menu_id('STXD'), 'pt-PT', 'Detalhes de imposto sobre vendas' UNION ALL
SELECT core.get_menu_id('STXEX'), 'pt-PT', 'Isenta de imposto sobre vendas' UNION ALL
SELECT core.get_menu_id('STXEXD'), 'pt-PT', 'Imposto sobre vendas Detalhes Isentos' UNION ALL
SELECT core.get_menu_id('STXT'), 'pt-PT', 'Tipos de imposto sobre vendas' UNION ALL
SELECT core.get_menu_id('SUM'), 'pt-PT', 'Gerenciamento de usuários' UNION ALL
SELECT core.get_menu_id('SVV'), 'pt-PT', 'Comprovante Política de Verificação' UNION ALL
SELECT core.get_menu_id('TB'), 'pt-PT', 'Balancete' UNION ALL
SELECT core.get_menu_id('TRF'), 'pt-PT', 'bandeiras' UNION ALL
SELECT core.get_menu_id('TXA'), 'pt-PT', 'Fisco' UNION ALL
SELECT core.get_menu_id('TXEXT'), 'pt-PT', 'Fiscais Tipos Isentos' UNION ALL
SELECT core.get_menu_id('TXM'), 'pt-PT', 'Mestre Tax' UNION ALL
SELECT core.get_menu_id('UER'), 'pt-PT', 'Atualização de Taxas de Câmbio' UNION ALL
SELECT core.get_menu_id('UOM'), 'pt-PT', 'Unidades de Medida';

--SWEDISH

INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('ABS'), 'sv-SE', 'Bonus Slab för Försäljare' UNION ALL
SELECT core.get_menu_id('AGS'), 'sv-SE', 'åldrande Plattor' UNION ALL
SELECT core.get_menu_id('AS'), 'sv-SE', 'Kontoutdrag' UNION ALL
SELECT core.get_menu_id('BAK'), 'sv-SE', 'backup Database' UNION ALL
SELECT core.get_menu_id('BO'), 'sv-SE', 'Tillbaka Office' UNION ALL
SELECT core.get_menu_id('BOTC'), 'sv-SE', 'Skatte Konfiguration' UNION ALL
SELECT core.get_menu_id('BS'), 'sv-SE', 'bALANSRÄKNING' UNION ALL
SELECT core.get_menu_id('BSA'), 'sv-SE', 'Bonus Slab Assignment' UNION ALL
SELECT core.get_menu_id('BSD'), 'sv-SE', 'Bonus Slab Detaljer' UNION ALL
SELECT core.get_menu_id('CBA'), 'sv-SE', 'Bankkonton' UNION ALL
SELECT core.get_menu_id('CC'), 'sv-SE', 'Kostnadsställen' UNION ALL
SELECT core.get_menu_id('CF'), 'sv-SE', 'Cash Flow' UNION ALL
SELECT core.get_menu_id('CFH'), 'sv-SE', 'Cash Flow Rubriker' UNION ALL
SELECT core.get_menu_id('COA'), 'sv-SE', 'Kontoplan' UNION ALL
SELECT core.get_menu_id('CTST'), 'sv-SE', 'Län Försäljnings Skatter' UNION ALL
SELECT core.get_menu_id('CUOM'), 'sv-SE', 'Sammansatta måttenheter' UNION ALL
SELECT core.get_menu_id('CUR'), 'sv-SE', 'Valutahantering' UNION ALL
SELECT core.get_menu_id('DBSTAT'), 'sv-SE', 'databas Statistik' UNION ALL
SELECT core.get_menu_id('DRP'), 'sv-SE', 'Direkt Inköp' UNION ALL
SELECT core.get_menu_id('DRS'), 'sv-SE', 'direktförsäljning' UNION ALL
SELECT core.get_menu_id('EOD'), 'sv-SE', 'Slut på Dag Operation' UNION ALL
SELECT core.get_menu_id('FI'), 'sv-SE', 'Finans' UNION ALL
SELECT core.get_menu_id('FIR'), 'sv-SE', 'Rapporter' UNION ALL
SELECT core.get_menu_id('FSM'), 'sv-SE', 'Uppställning & Underhåll' UNION ALL
SELECT core.get_menu_id('FTT'), 'sv-SE', 'Transaktioner & Mallar' UNION ALL
SELECT core.get_menu_id('FVV'), 'sv-SE', 'Voucher Verifiering' UNION ALL
SELECT core.get_menu_id('GRN'), 'sv-SE', 'Varor mottagna anteckning' UNION ALL
SELECT core.get_menu_id('IAS'), 'sv-SE', 'Kontoutdrag Inventory' UNION ALL
SELECT core.get_menu_id('ICP'), 'sv-SE', 'Kostnads Priser' UNION ALL
SELECT core.get_menu_id('IIM'), 'sv-SE', 'Inventering Rörelser' UNION ALL
SELECT core.get_menu_id('IR'), 'sv-SE', 'Rapporter' UNION ALL
SELECT core.get_menu_id('ISM'), 'sv-SE', 'Uppställning & Underhåll' UNION ALL
SELECT core.get_menu_id('ISP'), 'sv-SE', 'försäljningspriser' UNION ALL
SELECT core.get_menu_id('ITM'), 'sv-SE', 'Produkter & Artiklar' UNION ALL
SELECT core.get_menu_id('JVN'), 'sv-SE', 'Journal Voucher Entry' UNION ALL
SELECT core.get_menu_id('LF'), 'sv-SE', 'förseningsavgifter' UNION ALL
SELECT core.get_menu_id('OTS'), 'sv-SE', 'One Time Setup' UNION ALL
SELECT core.get_menu_id('OTSI'), 'sv-SE', 'Öppning Inventory' UNION ALL
SELECT core.get_menu_id('PA'), 'sv-SE', 'Party-konton' UNION ALL
SELECT core.get_menu_id('PAT'), 'sv-SE', 'Betalningsvillkor' UNION ALL
SELECT core.get_menu_id('PLA'), 'sv-SE', 'Resultaträkning' UNION ALL
SELECT core.get_menu_id('PO'), 'sv-SE', 'Inköpsorder' UNION ALL
SELECT core.get_menu_id('PR'), 'sv-SE', 'Inköps Return' UNION ALL
SELECT core.get_menu_id('PRO'), 'sv-SE', 'Inköps Omsortera' UNION ALL
SELECT core.get_menu_id('PSA'), 'sv-SE', 'Frakt Adresser' UNION ALL
SELECT core.get_menu_id('PT'), 'sv-SE', 'Parti Typer' UNION ALL
SELECT core.get_menu_id('PU'), 'sv-SE', 'Inköp' UNION ALL
SELECT core.get_menu_id('PUQ'), 'sv-SE', 'Inköp & Offert' UNION ALL
SELECT core.get_menu_id('PUR'), 'sv-SE', 'Inköps Rapporter' UNION ALL
SELECT core.get_menu_id('PWD'), 'sv-SE', 'Ändra användarlösenord' UNION ALL
SELECT core.get_menu_id('RFC'), 'sv-SE', 'Kvitto från kund' UNION ALL
SELECT core.get_menu_id('RI'), 'sv-SE', 'Återkommande fakturor' UNION ALL
SELECT core.get_menu_id('RIS'), 'sv-SE', 'Återkommande Faktura Setup' UNION ALL
SELECT core.get_menu_id('SA'), 'sv-SE', 'Försäljning' UNION ALL
SELECT core.get_menu_id('SAP'), 'sv-SE', 'GL Access Policy' UNION ALL
SELECT core.get_menu_id('SAQ'), 'sv-SE', 'Försäljning & Offert' UNION ALL
SELECT core.get_menu_id('SAR'), 'sv-SE', 'Försäljningsrapporter' UNION ALL
SELECT core.get_menu_id('SAR-TSI'), 'sv-SE', 'Top köpta' UNION ALL
SELECT core.get_menu_id('SAT'), 'sv-SE', 'admin Tools' UNION ALL
SELECT core.get_menu_id('SAV'), 'sv-SE', 'Automatisk Verifiering Policy' UNION ALL
SELECT core.get_menu_id('SCR'), 'sv-SE', 'Cash Repository Setup' UNION ALL
SELECT core.get_menu_id('SCRS'), 'sv-SE', 'Land Setup' UNION ALL
SELECT core.get_menu_id('SCS'), 'sv-SE', 'Counter Setup' UNION ALL
SELECT core.get_menu_id('SCTS'), 'sv-SE', 'län Setup' UNION ALL
SELECT core.get_menu_id('SD'), 'sv-SE', 'Försäljnings Leverans' UNION ALL
SELECT core.get_menu_id('SDS'), 'sv-SE', 'Department Setup' UNION ALL
SELECT core.get_menu_id('SEAR'), 'sv-SE', 'revisionsrapporter' UNION ALL
SELECT core.get_menu_id('SEAR-LV'), 'sv-SE', 'Inloggning View' UNION ALL
SELECT core.get_menu_id('SES'), 'sv-SE', 'Entity Setup' UNION ALL
SELECT core.get_menu_id('SFR'), 'sv-SE', 'Frekvens & Räkenskapsårets Hantering' UNION ALL
SELECT core.get_menu_id('SFY'), 'sv-SE', 'Räkenskapsårets Information' UNION ALL
SELECT core.get_menu_id('SHI'), 'sv-SE', 'avsändaren Information' UNION ALL
SELECT core.get_menu_id('SIG'), 'sv-SE', 'artikelgrupper' UNION ALL
SELECT core.get_menu_id('SIS'), 'sv-SE', 'Bransch Setup' UNION ALL
SELECT core.get_menu_id('SMA'), 'sv-SE', 'Meny Access Policy' UNION ALL
SELECT core.get_menu_id('SMP'), 'sv-SE', 'Diverse parametrar' UNION ALL
SELECT core.get_menu_id('SO'), 'sv-SE', 'kundorder' UNION ALL
SELECT core.get_menu_id('SOB'), 'sv-SE', 'Kontor & Branch Setup' UNION ALL
SELECT core.get_menu_id('SOS'), 'sv-SE', 'Office Setup' UNION ALL
SELECT core.get_menu_id('SPM'), 'sv-SE', 'Principhantering' UNION ALL
SELECT core.get_menu_id('SQ'), 'sv-SE', 'Försäljnings Offert' UNION ALL
SELECT core.get_menu_id('SR'), 'sv-SE', 'Sales Return' UNION ALL
SELECT core.get_menu_id('SRM'), 'sv-SE', 'Roll hantering' UNION ALL
SELECT core.get_menu_id('SSA'), 'sv-SE', 'försäljare' UNION ALL
SELECT core.get_menu_id('SSB'), 'sv-SE', 'varumärken' UNION ALL
SELECT core.get_menu_id('SSC'), 'sv-SE', 'Sammansatta artiklar' UNION ALL
SELECT core.get_menu_id('SSCD'), 'sv-SE', 'Förening Objekt Information' UNION ALL
SELECT core.get_menu_id('SSI'), 'sv-SE', 'Punkt Underhåll' UNION ALL
SELECT core.get_menu_id('SSM'), 'sv-SE', 'Uppställning & Underhåll' UNION ALL
SELECT core.get_menu_id('SSP'), 'sv-SE', 'Affär Policy' UNION ALL
SELECT core.get_menu_id('SSS'), 'sv-SE', 'State Setup' UNION ALL
SELECT core.get_menu_id('SST'), 'sv-SE', 'Sälj Teams' UNION ALL
SELECT core.get_menu_id('STA'), 'sv-SE', 'Aktie Justeringar' UNION ALL
SELECT core.get_menu_id('STJ'), 'sv-SE', 'Omlagring Journal' UNION ALL
SELECT core.get_menu_id('STO'), 'sv-SE', 'butiker' UNION ALL
SELECT core.get_menu_id('STST'), 'sv-SE', 'Statliga Försäljnings Skatter' UNION ALL
SELECT core.get_menu_id('STT'), 'sv-SE', 'Förvara Typer' UNION ALL
SELECT core.get_menu_id('STX'), 'sv-SE', 'Försäljnings Skatter' UNION ALL
SELECT core.get_menu_id('STXD'), 'sv-SE', 'Försäljningsskatte Detaljer' UNION ALL
SELECT core.get_menu_id('STXEX'), 'sv-SE', 'Försäljningsskatte undantar' UNION ALL
SELECT core.get_menu_id('STXEXD'), 'sv-SE', 'Försäljnings skattefri Detaljer' UNION ALL
SELECT core.get_menu_id('STXT'), 'sv-SE', 'Försäljningsskattetyper' UNION ALL
SELECT core.get_menu_id('SUM'), 'sv-SE', 'Användarhantering' UNION ALL
SELECT core.get_menu_id('SVV'), 'sv-SE', 'Voucher Verifiering Policy' UNION ALL
SELECT core.get_menu_id('TB'), 'sv-SE', 'Trial Balance' UNION ALL
SELECT core.get_menu_id('TRF'), 'sv-SE', 'Flaggor' UNION ALL
SELECT core.get_menu_id('TXA'), 'sv-SE', 'Skatteverket' UNION ALL
SELECT core.get_menu_id('TXEXT'), 'sv-SE', 'Skatteundantagna Typer' UNION ALL
SELECT core.get_menu_id('TXM'), 'sv-SE', 'Skatte ledar-' UNION ALL
SELECT core.get_menu_id('UER'), 'sv-SE', 'Uppdatera valutakurser' UNION ALL
SELECT core.get_menu_id('UOM'), 'sv-SE', 'Måttenheter';

--MALAYASIAN
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('ABS'), 'ms-MY', 'Slab Bonus untuk jurujual' UNION ALL
SELECT core.get_menu_id('AGS'), 'ms-MY', 'papak Penuaan' UNION ALL
SELECT core.get_menu_id('AS'), 'ms-MY', 'Penyata Akaun' UNION ALL
SELECT core.get_menu_id('BAK'), 'ms-MY', 'sandaran Pangkalan Data' UNION ALL
SELECT core.get_menu_id('BO'), 'ms-MY', 'Pejabat Kembali' UNION ALL
SELECT core.get_menu_id('BOTC'), 'ms-MY', 'Konfigurasi cukai' UNION ALL
SELECT core.get_menu_id('BS'), 'ms-MY', 'Kunci Kira-kira' UNION ALL
SELECT core.get_menu_id('BSA'), 'ms-MY', 'Bonus Tugasan Slab' UNION ALL
SELECT core.get_menu_id('BSD'), 'ms-MY', 'Bonus Slab Butiran' UNION ALL
SELECT core.get_menu_id('CBA'), 'ms-MY', 'Akaun Bank' UNION ALL
SELECT core.get_menu_id('CC'), 'ms-MY', 'Pusat kos' UNION ALL
SELECT core.get_menu_id('CF'), 'ms-MY', 'Aliran tunai' UNION ALL
SELECT core.get_menu_id('CFH'), 'ms-MY', 'Aliran Tunai Tajuk' UNION ALL
SELECT core.get_menu_id('COA'), 'ms-MY', 'Carta Akaun' UNION ALL
SELECT core.get_menu_id('CTST'), 'ms-MY', 'Daerah-daerah Jualan Cukai' UNION ALL
SELECT core.get_menu_id('CUOM'), 'ms-MY', 'Unit perkarangan Langkah' UNION ALL
SELECT core.get_menu_id('CUR'), 'ms-MY', 'Pengurusan mata Wang' UNION ALL
SELECT core.get_menu_id('DBSTAT'), 'ms-MY', 'Statistik Pangkalan Data' UNION ALL
SELECT core.get_menu_id('DRP'), 'ms-MY', 'Pembelian Terus' UNION ALL
SELECT core.get_menu_id('DRS'), 'ms-MY', 'Jualan Langsung' UNION ALL
SELECT core.get_menu_id('EOD'), 'ms-MY', 'Akhir Operasi Hari' UNION ALL
SELECT core.get_menu_id('FI'), 'ms-MY', 'Kewangan' UNION ALL
SELECT core.get_menu_id('FIR'), 'ms-MY', 'laporan' UNION ALL
SELECT core.get_menu_id('FSM'), 'ms-MY', 'Persediaan & Penyelenggaraan' UNION ALL
SELECT core.get_menu_id('FTT'), 'ms-MY', 'Urusniaga & Templates' UNION ALL
SELECT core.get_menu_id('FVV'), 'ms-MY', 'baucer Pengesahan' UNION ALL
SELECT core.get_menu_id('GRN'), 'ms-MY', 'Barang Diterima Nota Entry' UNION ALL
SELECT core.get_menu_id('IAS'), 'ms-MY', 'Penyata Akaun Inventori' UNION ALL
SELECT core.get_menu_id('ICP'), 'ms-MY', 'Harga kos' UNION ALL
SELECT core.get_menu_id('IIM'), 'ms-MY', 'Pergerakan inventori' UNION ALL
SELECT core.get_menu_id('IR'), 'ms-MY', 'laporan' UNION ALL
SELECT core.get_menu_id('ISM'), 'ms-MY', 'Persediaan & Penyelenggaraan' UNION ALL
SELECT core.get_menu_id('ISP'), 'ms-MY', 'Menjual Harga' UNION ALL
SELECT core.get_menu_id('ITM'), 'ms-MY', 'Produk & Barangan' UNION ALL
SELECT core.get_menu_id('JVN'), 'ms-MY', 'Journal Voucher Entry' UNION ALL
SELECT core.get_menu_id('LF'), 'ms-MY', 'Bayaran Lewat' UNION ALL
SELECT core.get_menu_id('OTS'), 'ms-MY', 'Satu Persediaan Masa' UNION ALL
SELECT core.get_menu_id('OTSI'), 'ms-MY', 'Inventori membuka' UNION ALL
SELECT core.get_menu_id('PA'), 'ms-MY', 'Akaun Pihak' UNION ALL
SELECT core.get_menu_id('PAT'), 'ms-MY', 'Terma pembayaran' UNION ALL
SELECT core.get_menu_id('PLA'), 'ms-MY', 'Untung & Rugi' UNION ALL
SELECT core.get_menu_id('PO'), 'ms-MY', 'Pesanan Pembelian' UNION ALL
SELECT core.get_menu_id('PR'), 'ms-MY', 'pembelian Pulangan' UNION ALL
SELECT core.get_menu_id('PRO'), 'ms-MY', 'pembelian Pesanan Semula' UNION ALL
SELECT core.get_menu_id('PSA'), 'ms-MY', 'Alamat Penghantaran' UNION ALL
SELECT core.get_menu_id('PT'), 'ms-MY', 'Jenis parti' UNION ALL
SELECT core.get_menu_id('PU'), 'ms-MY', 'pembelian' UNION ALL
SELECT core.get_menu_id('PUQ'), 'ms-MY', 'Pembelian & Sebut Harga' UNION ALL
SELECT core.get_menu_id('PUR'), 'ms-MY', 'Laporan pembelian' UNION ALL
SELECT core.get_menu_id('PWD'), 'ms-MY', 'Tukar Pengguna Kata Laluan' UNION ALL
SELECT core.get_menu_id('RFC'), 'ms-MY', 'Penerimaan daripada Pelanggan' UNION ALL
SELECT core.get_menu_id('RI'), 'ms-MY', 'Invois berulang' UNION ALL
SELECT core.get_menu_id('RIS'), 'ms-MY', 'Berulang Persediaan Invois' UNION ALL
SELECT core.get_menu_id('SA'), 'ms-MY', 'jualan' UNION ALL
SELECT core.get_menu_id('SAP'), 'ms-MY', 'Dasar Akses GL' UNION ALL
SELECT core.get_menu_id('SAQ'), 'ms-MY', 'Jualan & Sebut Harga' UNION ALL
SELECT core.get_menu_id('SAR'), 'ms-MY', 'jualan Laporan' UNION ALL
SELECT core.get_menu_id('SAR-TSI'), 'ms-MY', 'Item Jualan Top' UNION ALL
SELECT core.get_menu_id('SAT'), 'ms-MY', 'Alat admin' UNION ALL
SELECT core.get_menu_id('SAV'), 'ms-MY', 'Dasar Pengesahan automatik' UNION ALL
SELECT core.get_menu_id('SCR'), 'ms-MY', 'Repository Tunai Persediaan' UNION ALL
SELECT core.get_menu_id('SCRS'), 'ms-MY', 'negara Persediaan' UNION ALL
SELECT core.get_menu_id('SCS'), 'ms-MY', 'Persediaan kaunter' UNION ALL
SELECT core.get_menu_id('SCTS'), 'ms-MY', 'County Persediaan' UNION ALL
SELECT core.get_menu_id('SD'), 'ms-MY', 'Penghantaran jualan' UNION ALL
SELECT core.get_menu_id('SDS'), 'ms-MY', 'Jabatan Persediaan' UNION ALL
SELECT core.get_menu_id('SEAR'), 'ms-MY', 'Laporan Audit' UNION ALL
SELECT core.get_menu_id('SEAR-LV'), 'ms-MY', 'Log masuk View' UNION ALL
SELECT core.get_menu_id('SES'), 'ms-MY', 'entiti Persediaan' UNION ALL
SELECT core.get_menu_id('SFR'), 'ms-MY', 'Pengurusan Frekuensi & Tahun Anggaran' UNION ALL
SELECT core.get_menu_id('SFY'), 'ms-MY', 'Tahun fiskal Maklumat' UNION ALL
SELECT core.get_menu_id('SHI'), 'ms-MY', 'penghantar Maklumat' UNION ALL
SELECT core.get_menu_id('SIG'), 'ms-MY', 'Kumpulan Perkara' UNION ALL
SELECT core.get_menu_id('SIS'), 'ms-MY', 'industri Persediaan' UNION ALL
SELECT core.get_menu_id('SMA'), 'ms-MY', 'Dasar Akses Menu' UNION ALL
SELECT core.get_menu_id('SMP'), 'ms-MY', 'Parameter Pelbagai' UNION ALL
SELECT core.get_menu_id('SO'), 'ms-MY', 'Perintah jualan' UNION ALL
SELECT core.get_menu_id('SOB'), 'ms-MY', 'Pejabat & Cawangan Persediaan' UNION ALL
SELECT core.get_menu_id('SOS'), 'ms-MY', 'Pejabat Persediaan' UNION ALL
SELECT core.get_menu_id('SPM'), 'ms-MY', 'Pengurusan Polisi' UNION ALL
SELECT core.get_menu_id('SQ'), 'ms-MY', 'Sebut Harga jualan' UNION ALL
SELECT core.get_menu_id('SR'), 'ms-MY', 'jualan Pulangan' UNION ALL
SELECT core.get_menu_id('SRM'), 'ms-MY', 'Pengurusan peranan' UNION ALL
SELECT core.get_menu_id('SSA'), 'ms-MY', 'jurujual' UNION ALL
SELECT core.get_menu_id('SSB'), 'ms-MY', 'jenama' UNION ALL
SELECT core.get_menu_id('SSC'), 'ms-MY', 'Item kompaun' UNION ALL
SELECT core.get_menu_id('SSCD'), 'ms-MY', 'Butiran Kompaun Perkara' UNION ALL
SELECT core.get_menu_id('SSI'), 'ms-MY', 'Penyelenggaraan Perkara' UNION ALL
SELECT core.get_menu_id('SSM'), 'ms-MY', 'Persediaan & Penyelenggaraan' UNION ALL
SELECT core.get_menu_id('SSP'), 'ms-MY', 'Dasar Store' UNION ALL
SELECT core.get_menu_id('SSS'), 'ms-MY', 'Persediaan Negeri' UNION ALL
SELECT core.get_menu_id('SST'), 'ms-MY', 'Pasukan jualan' UNION ALL
SELECT core.get_menu_id('STA'), 'ms-MY', 'Pelarasan saham' UNION ALL
SELECT core.get_menu_id('STJ'), 'ms-MY', 'Pemindahan Saham Journal' UNION ALL
SELECT core.get_menu_id('STO'), 'ms-MY', 'kedai' UNION ALL
SELECT core.get_menu_id('STST'), 'ms-MY', 'Negeri Cukai Jualan' UNION ALL
SELECT core.get_menu_id('STT'), 'ms-MY', 'Jenis kedai' UNION ALL
SELECT core.get_menu_id('STX'), 'ms-MY', 'Cukai jualan' UNION ALL
SELECT core.get_menu_id('STXD'), 'ms-MY', 'Butiran Cukai Jualan' UNION ALL
SELECT core.get_menu_id('STXEX'), 'ms-MY', 'Mengecualikan Cukai Jualan' UNION ALL
SELECT core.get_menu_id('STXEXD'), 'ms-MY', 'Cukai Jualan Butiran Dikecualikan' UNION ALL
SELECT core.get_menu_id('STXT'), 'ms-MY', 'Jenis Cukai Jualan' UNION ALL
SELECT core.get_menu_id('SUM'), 'ms-MY', 'Pengurusan Pengguna' UNION ALL
SELECT core.get_menu_id('SVV'), 'ms-MY', 'Dasar Pengesahan baucar' UNION ALL
SELECT core.get_menu_id('TB'), 'ms-MY', 'Imbangan Duga' UNION ALL
SELECT core.get_menu_id('TRF'), 'ms-MY', 'bendera' UNION ALL
SELECT core.get_menu_id('TXA'), 'ms-MY', 'Pihak Berkuasa cukai' UNION ALL
SELECT core.get_menu_id('TXEXT'), 'ms-MY', 'Cukai Jenis Dikecualikan' UNION ALL
SELECT core.get_menu_id('TXM'), 'ms-MY', 'Master cukai' UNION ALL
SELECT core.get_menu_id('UER'), 'ms-MY', 'Kadar Pertukaran Update' UNION ALL
SELECT core.get_menu_id('UOM'), 'ms-MY', 'Unit Tindakan';

--INDONESIAN
INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('ABS'), 'id-ID', 'Slab bonus untuk Staf Penjualan' UNION ALL
SELECT core.get_menu_id('AGS'), 'id-ID', 'Lempengan Penuaan' UNION ALL
SELECT core.get_menu_id('AS'), 'id-ID', 'rekening' UNION ALL
SELECT core.get_menu_id('BAK'), 'id-ID', 'backup database' UNION ALL
SELECT core.get_menu_id('BO'), 'id-ID', 'Back Office' UNION ALL
SELECT core.get_menu_id('BOTC'), 'id-ID', 'Konfigurasi pajak' UNION ALL
SELECT core.get_menu_id('BS'), 'id-ID', 'neraca Keuangan' UNION ALL
SELECT core.get_menu_id('BSA'), 'id-ID', 'Bonus Slab Tugas' UNION ALL
SELECT core.get_menu_id('BSD'), 'id-ID', 'Bonus Slab Detail' UNION ALL
SELECT core.get_menu_id('CBA'), 'id-ID', 'Rekening Bank' UNION ALL
SELECT core.get_menu_id('CC'), 'id-ID', 'Pusat biaya' UNION ALL
SELECT core.get_menu_id('CF'), 'id-ID', 'Arus Kas' UNION ALL
SELECT core.get_menu_id('CFH'), 'id-ID', 'Arus Kas Pos' UNION ALL
SELECT core.get_menu_id('COA'), 'id-ID', 'Bagan Akun' UNION ALL
SELECT core.get_menu_id('CTST'), 'id-ID', 'Kabupaten Penjualan Pajak' UNION ALL
SELECT core.get_menu_id('CUOM'), 'id-ID', 'Unit senyawa Ukur' UNION ALL
SELECT core.get_menu_id('CUR'), 'id-ID', 'Manajemen Mata Uang' UNION ALL
SELECT core.get_menu_id('DBSTAT'), 'id-ID', 'Statistik database' UNION ALL
SELECT core.get_menu_id('DRP'), 'id-ID', 'Pembelian langsung' UNION ALL
SELECT core.get_menu_id('DRS'), 'id-ID', 'Penjualan Langsung' UNION ALL
SELECT core.get_menu_id('EOD'), 'id-ID', 'Akhir Hari Operasi' UNION ALL
SELECT core.get_menu_id('FI'), 'id-ID', 'keuangan' UNION ALL
SELECT core.get_menu_id('FIR'), 'id-ID', 'laporan' UNION ALL
SELECT core.get_menu_id('FSM'), 'id-ID', 'Pengaturan & Maintenance' UNION ALL
SELECT core.get_menu_id('FTT'), 'id-ID', 'Transaksi & Template' UNION ALL
SELECT core.get_menu_id('FVV'), 'id-ID', 'Verifikasi voucher' UNION ALL
SELECT core.get_menu_id('GRN'), 'id-ID', 'GRN masuk' UNION ALL
SELECT core.get_menu_id('IAS'), 'id-ID', 'Rekening persediaan' UNION ALL
SELECT core.get_menu_id('ICP'), 'id-ID', 'Harga biaya' UNION ALL
SELECT core.get_menu_id('IIM'), 'id-ID', 'Mutasi persediaan' UNION ALL
SELECT core.get_menu_id('IR'), 'id-ID', 'laporan' UNION ALL
SELECT core.get_menu_id('ISM'), 'id-ID', 'Pengaturan & Maintenance' UNION ALL
SELECT core.get_menu_id('ISP'), 'id-ID', 'Jual Harga' UNION ALL
SELECT core.get_menu_id('ITM'), 'id-ID', 'Produk & Produk' UNION ALL
SELECT core.get_menu_id('JVN'), 'id-ID', 'Jurnal Voucher Masuk' UNION ALL
SELECT core.get_menu_id('LF'), 'id-ID', 'akhir Biaya' UNION ALL
SELECT core.get_menu_id('OTS'), 'id-ID', 'One Time Pengaturan' UNION ALL
SELECT core.get_menu_id('OTSI'), 'id-ID', 'membuka Persediaan' UNION ALL
SELECT core.get_menu_id('PA'), 'id-ID', 'Akun Partai' UNION ALL
SELECT core.get_menu_id('PAT'), 'id-ID', 'Syarat Pembayaran' UNION ALL
SELECT core.get_menu_id('PLA'), 'id-ID', 'Laba & Rugi' UNION ALL
SELECT core.get_menu_id('PO'), 'id-ID', 'Purchase Order' UNION ALL
SELECT core.get_menu_id('PR'), 'id-ID', 'pembelian Kembali' UNION ALL
SELECT core.get_menu_id('PRO'), 'id-ID', 'pembelian perekam' UNION ALL
SELECT core.get_menu_id('PSA'), 'id-ID', 'Alamat pengiriman' UNION ALL
SELECT core.get_menu_id('PT'), 'id-ID', 'Jenis Partai' UNION ALL
SELECT core.get_menu_id('PU'), 'id-ID', 'pembelian' UNION ALL
SELECT core.get_menu_id('PUQ'), 'id-ID', 'Pembelian & Quotation' UNION ALL
SELECT core.get_menu_id('PUR'), 'id-ID', 'Laporan pembelian' UNION ALL
SELECT core.get_menu_id('PWD'), 'id-ID', 'Ubah Password Pengguna' UNION ALL
SELECT core.get_menu_id('RFC'), 'id-ID', 'Penerimaan dari Pelanggan' UNION ALL
SELECT core.get_menu_id('RI'), 'id-ID', 'Faktur berulang' UNION ALL
SELECT core.get_menu_id('RIS'), 'id-ID', 'Berulang Faktur Pengaturan' UNION ALL
SELECT core.get_menu_id('SA'), 'id-ID', 'penjualan' UNION ALL
SELECT core.get_menu_id('SAP'), 'id-ID', 'GL Kebijakan Access' UNION ALL
SELECT core.get_menu_id('SAQ'), 'id-ID', 'Penjualan & Quotation' UNION ALL
SELECT core.get_menu_id('SAR'), 'id-ID', 'Laporan penjualan' UNION ALL
SELECT core.get_menu_id('SAR-TSI'), 'id-ID', 'Top Selling Produk' UNION ALL
SELECT core.get_menu_id('SAT'), 'id-ID', 'Alat admin' UNION ALL
SELECT core.get_menu_id('SAV'), 'id-ID', 'Kebijakan Verifikasi Otomatis' UNION ALL
SELECT core.get_menu_id('SCR'), 'id-ID', 'Repository kas Pengaturan' UNION ALL
SELECT core.get_menu_id('SCRS'), 'id-ID', 'negara Pengaturan' UNION ALL
SELECT core.get_menu_id('SCS'), 'id-ID', 'kontra Pengaturan' UNION ALL
SELECT core.get_menu_id('SCTS'), 'id-ID', 'county Pengaturan' UNION ALL
SELECT core.get_menu_id('SD'), 'id-ID', 'penjualan Pengiriman' UNION ALL
SELECT core.get_menu_id('SDS'), 'id-ID', 'Departemen Pengaturan' UNION ALL
SELECT core.get_menu_id('SEAR'), 'id-ID', 'Laporan Audit' UNION ALL
SELECT core.get_menu_id('SEAR-LV'), 'id-ID', 'Login View' UNION ALL
SELECT core.get_menu_id('SES'), 'id-ID', 'entitas Pengaturan' UNION ALL
SELECT core.get_menu_id('SFR'), 'id-ID', 'Manajemen Frekuensi & Fiskal Tahun' UNION ALL
SELECT core.get_menu_id('SFY'), 'id-ID', 'Fiskal Informasi Tahun' UNION ALL
SELECT core.get_menu_id('SHI'), 'id-ID', 'pengirim Informasi' UNION ALL
SELECT core.get_menu_id('SIG'), 'id-ID', 'Item Grup' UNION ALL
SELECT core.get_menu_id('SIS'), 'id-ID', 'Pengaturan industri' UNION ALL
SELECT core.get_menu_id('SMA'), 'id-ID', 'Menu Akses Kebijakan' UNION ALL
SELECT core.get_menu_id('SMP'), 'id-ID', 'Parameter lain-lain' UNION ALL
SELECT core.get_menu_id('SO'), 'id-ID', 'Pesanan penjualan' UNION ALL
SELECT core.get_menu_id('SOB'), 'id-ID', 'Kantor Cabang & Pengaturan' UNION ALL
SELECT core.get_menu_id('SOS'), 'id-ID', 'kantor Pengaturan' UNION ALL
SELECT core.get_menu_id('SPM'), 'id-ID', 'Kebijakan Manajemen' UNION ALL
SELECT core.get_menu_id('SQ'), 'id-ID', 'sales Quotation' UNION ALL
SELECT core.get_menu_id('SR'), 'id-ID', 'penjualan Kembali' UNION ALL
SELECT core.get_menu_id('SRM'), 'id-ID', 'Manajemen peran' UNION ALL
SELECT core.get_menu_id('SSA'), 'id-ID', 'penjual' UNION ALL
SELECT core.get_menu_id('SSB'), 'id-ID', 'merek' UNION ALL
SELECT core.get_menu_id('SSC'), 'id-ID', 'senyawa Item' UNION ALL
SELECT core.get_menu_id('SSCD'), 'id-ID', 'Senyawa Item detail' UNION ALL
SELECT core.get_menu_id('SSI'), 'id-ID', 'Item Maintenance' UNION ALL
SELECT core.get_menu_id('SSM'), 'id-ID', 'Pengaturan & Maintenance' UNION ALL
SELECT core.get_menu_id('SSP'), 'id-ID', 'Kebijakan toko' UNION ALL
SELECT core.get_menu_id('SSS'), 'id-ID', 'Pengaturan negara' UNION ALL
SELECT core.get_menu_id('SST'), 'id-ID', 'penjualan Tim' UNION ALL
SELECT core.get_menu_id('STA'), 'id-ID', 'Penyesuaian saham' UNION ALL
SELECT core.get_menu_id('STJ'), 'id-ID', 'Jurnal transfer saham' UNION ALL
SELECT core.get_menu_id('STO'), 'id-ID', 'toko' UNION ALL
SELECT core.get_menu_id('STST'), 'id-ID', 'Penjualan negara Pajak' UNION ALL
SELECT core.get_menu_id('STT'), 'id-ID', 'Jenis toko' UNION ALL
SELECT core.get_menu_id('STX'), 'id-ID', 'penjualan Pajak' UNION ALL
SELECT core.get_menu_id('STXD'), 'id-ID', 'Rincian Pajak Penjualan' UNION ALL
SELECT core.get_menu_id('STXEX'), 'id-ID', 'Membebaskan Pajak Penjualan' UNION ALL
SELECT core.get_menu_id('STXEXD'), 'id-ID', 'Pajak Penjualan Detail Bebaskan' UNION ALL
SELECT core.get_menu_id('STXT'), 'id-ID', 'Jenis Pajak Penjualan' UNION ALL
SELECT core.get_menu_id('SUM'), 'id-ID', 'Manajemen pengguna' UNION ALL
SELECT core.get_menu_id('SVV'), 'id-ID', 'Kebijakan Verifikasi Voucher' UNION ALL
SELECT core.get_menu_id('TB'), 'id-ID', 'Neraca Saldo' UNION ALL
SELECT core.get_menu_id('TRF'), 'id-ID', 'Flags' UNION ALL
SELECT core.get_menu_id('TXA'), 'id-ID', 'Kantor Pajak' UNION ALL
SELECT core.get_menu_id('TXEXT'), 'id-ID', 'Jenis Bebaskan Pajak' UNION ALL
SELECT core.get_menu_id('TXM'), 'id-ID', 'Guru pajak' UNION ALL
SELECT core.get_menu_id('UER'), 'id-ID', 'Perbarui Tukar' UNION ALL
SELECT core.get_menu_id('UOM'), 'id-ID', 'Satuan Ukur';

--FILIPINO

INSERT INTO core.menu_locale(menu_id, culture, menu_text)
SELECT core.get_menu_id('ABS'), 'fil-PH', 'Bonus laha para sa Salesperson' UNION ALL
SELECT core.get_menu_id('AGS'), 'fil-PH', 'Pagtanda Slabs' UNION ALL
SELECT core.get_menu_id('AS'), 'fil-PH', 'Statement ng Account' UNION ALL
SELECT core.get_menu_id('BAK'), 'fil-PH', 'backup Database' UNION ALL
SELECT core.get_menu_id('BO'), 'fil-PH', 'Bumalik Office' UNION ALL
SELECT core.get_menu_id('BOTC'), 'fil-PH', 'Configuration ng Buwis' UNION ALL
SELECT core.get_menu_id('BS'), 'fil-PH', 'balanse Sheet' UNION ALL
SELECT core.get_menu_id('BSA'), 'fil-PH', 'Bonus tilad Pagtatalaga' UNION ALL
SELECT core.get_menu_id('BSD'), 'fil-PH', 'Mga Detalye ng Bonus na tilad' UNION ALL
SELECT core.get_menu_id('CBA'), 'fil-PH', 'bank Account' UNION ALL
SELECT core.get_menu_id('CC'), 'fil-PH', 'Sentro ng Gastos' UNION ALL
SELECT core.get_menu_id('CF'), 'fil-PH', 'Daloy ng cash' UNION ALL
SELECT core.get_menu_id('CFH'), 'fil-PH', 'Mga Heading Daloy ng Cash' UNION ALL
SELECT core.get_menu_id('COA'), 'fil-PH', 'Tsart ng Account' UNION ALL
SELECT core.get_menu_id('CTST'), 'fil-PH', 'Mga county Sales Buwis' UNION ALL
SELECT core.get_menu_id('CUOM'), 'fil-PH', 'Compound Unit ng Pagsukat' UNION ALL
SELECT core.get_menu_id('CUR'), 'fil-PH', 'Pamamahala ng Salapi' UNION ALL
SELECT core.get_menu_id('DBSTAT'), 'fil-PH', 'Istatistika ng Database' UNION ALL
SELECT core.get_menu_id('DRP'), 'fil-PH', 'Direktang Pagbili' UNION ALL
SELECT core.get_menu_id('DRS'), 'fil-PH', 'Direktang Benta' UNION ALL
SELECT core.get_menu_id('EOD'), 'fil-PH', 'Katapusan ng Araw ng operasyon' UNION ALL
SELECT core.get_menu_id('FI'), 'fil-PH', 'pananalapi' UNION ALL
SELECT core.get_menu_id('FIR'), 'fil-PH', 'Mga Ulat' UNION ALL
SELECT core.get_menu_id('FSM'), 'fil-PH', 'Pag-setup at Pagpapanatili' UNION ALL
SELECT core.get_menu_id('FTT'), 'fil-PH', 'Mga transaksyon at Template' UNION ALL
SELECT core.get_menu_id('FVV'), 'fil-PH', 'voucher Pag-verify' UNION ALL
SELECT core.get_menu_id('GRN'), 'fil-PH', 'GRN Entry' UNION ALL
SELECT core.get_menu_id('IAS'), 'fil-PH', 'Pahayag Imbentaryo Account' UNION ALL
SELECT core.get_menu_id('ICP'), 'fil-PH', 'Ang mga presyo ng Gastos' UNION ALL
SELECT core.get_menu_id('IIM'), 'fil-PH', 'mga paggalaw ng Imbentaryo' UNION ALL
SELECT core.get_menu_id('IR'), 'fil-PH', 'Mga Ulat' UNION ALL
SELECT core.get_menu_id('ISM'), 'fil-PH', 'Pag-setup at Pagpapanatili' UNION ALL
SELECT core.get_menu_id('ISP'), 'fil-PH', 'Pagbebenta ng Mga Presyo' UNION ALL
SELECT core.get_menu_id('ITM'), 'fil-PH', 'Mga Produkto at Mga Item' UNION ALL
SELECT core.get_menu_id('JVN'), 'fil-PH', 'Journal Entry Voucher' UNION ALL
SELECT core.get_menu_id('LF'), 'fil-PH', 'Mga huling Bayarin' UNION ALL
SELECT core.get_menu_id('OTS'), 'fil-PH', 'Isang Oras sa Pag-setup' UNION ALL
SELECT core.get_menu_id('OTSI'), 'fil-PH', 'Pagbubukas ng Imbentaryo' UNION ALL
SELECT core.get_menu_id('PA'), 'fil-PH', 'Party Account' UNION ALL
SELECT core.get_menu_id('PAT'), 'fil-PH', 'Mga Tuntunin sa Pagbabayad' UNION ALL
SELECT core.get_menu_id('PLA'), 'fil-PH', 'Profit & Pagkawala ng Account' UNION ALL
SELECT core.get_menu_id('PO'), 'fil-PH', 'Purchase Order' UNION ALL
SELECT core.get_menu_id('PR'), 'fil-PH', 'Bumili ng Return' UNION ALL
SELECT core.get_menu_id('PRO'), 'fil-PH', 'Bumili ng Muling mag-order' UNION ALL
SELECT core.get_menu_id('PSA'), 'fil-PH', 'Address ng Pagpapadala' UNION ALL
SELECT core.get_menu_id('PT'), 'fil-PH', 'Mga Uri ng Party' UNION ALL
SELECT core.get_menu_id('PU'), 'fil-PH', 'pagbili' UNION ALL
SELECT core.get_menu_id('PUQ'), 'fil-PH', 'Pagbili at panipi' UNION ALL
SELECT core.get_menu_id('PUR'), 'fil-PH', 'Ulat ng Pagbili' UNION ALL
SELECT core.get_menu_id('PWD'), 'fil-PH', 'Baguhin ang User Password' UNION ALL
SELECT core.get_menu_id('RFC'), 'fil-PH', 'Resibo mula sa Customer' UNION ALL
SELECT core.get_menu_id('RI'), 'fil-PH', 'umuulit na mga Invoice' UNION ALL
SELECT core.get_menu_id('RIS'), 'fil-PH', 'Umuulit na Pag-setup ng Invoice' UNION ALL
SELECT core.get_menu_id('SA'), 'fil-PH', 'Sales' UNION ALL
SELECT core.get_menu_id('SAP'), 'fil-PH', 'GL Patakaran sa Pag-access' UNION ALL
SELECT core.get_menu_id('SAQ'), 'fil-PH', 'Benta at panipi' UNION ALL
SELECT core.get_menu_id('SAR'), 'fil-PH', 'Mga Ulat sa Benta' UNION ALL
SELECT core.get_menu_id('SAR-TSI'), 'fil-PH', 'Pinakamabentang Item' UNION ALL
SELECT core.get_menu_id('SAT'), 'fil-PH', 'Mga Tool ng Admin' UNION ALL
SELECT core.get_menu_id('SAV'), 'fil-PH', 'Patakaran sa Awtomatikong Pag-verify' UNION ALL
SELECT core.get_menu_id('SCR'), 'fil-PH', 'Cash imbakan Setup' UNION ALL
SELECT core.get_menu_id('SCRS'), 'fil-PH', 'Setup bansa' UNION ALL
SELECT core.get_menu_id('SCS'), 'fil-PH', 'counter-setup' UNION ALL
SELECT core.get_menu_id('SCTS'), 'fil-PH', 'Setup ng county' UNION ALL
SELECT core.get_menu_id('SD'), 'fil-PH', 'Paghahatid ng Sales' UNION ALL
SELECT core.get_menu_id('SDS'), 'fil-PH', 'Kagawaran Setup' UNION ALL
SELECT core.get_menu_id('SEAR'), 'fil-PH', 'Ulat ng pag-audit' UNION ALL
SELECT core.get_menu_id('SEAR-LV'), 'fil-PH', 'Tingnan ang Pag-login' UNION ALL
SELECT core.get_menu_id('SES'), 'fil-PH', 'entity Setup' UNION ALL
SELECT core.get_menu_id('SFR'), 'fil-PH', 'Pamamahala Taon Dalas & Pananalapi' UNION ALL
SELECT core.get_menu_id('SFY'), 'fil-PH', 'Tama sa Pananalapi Impormasyon Taon' UNION ALL
SELECT core.get_menu_id('SHI'), 'fil-PH', 'embarkador Impormasyon' UNION ALL
SELECT core.get_menu_id('SIG'), 'fil-PH', 'Mga Pangkat item' UNION ALL
SELECT core.get_menu_id('SIS'), 'fil-PH', 'Setup ng industriya' UNION ALL
SELECT core.get_menu_id('SMA'), 'fil-PH', 'Menu Patakaran sa Pag-access' UNION ALL
SELECT core.get_menu_id('SMP'), 'fil-PH', 'Sari-saring Parameter' UNION ALL
SELECT core.get_menu_id('SO'), 'fil-PH', 'Pagkakasunod-sunod ng Sales' UNION ALL
SELECT core.get_menu_id('SOB'), 'fil-PH', 'Setup ng Opisina at Sangay' UNION ALL
SELECT core.get_menu_id('SOS'), 'fil-PH', 'Setup ng Office' UNION ALL
SELECT core.get_menu_id('SPM'), 'fil-PH', 'Pamamahala ng patakaran' UNION ALL
SELECT core.get_menu_id('SQ'), 'fil-PH', 'Sales panipi' UNION ALL
SELECT core.get_menu_id('SR'), 'fil-PH', 'Sales Return' UNION ALL
SELECT core.get_menu_id('SRM'), 'fil-PH', 'Pamamahala ng Tungkulin' UNION ALL
SELECT core.get_menu_id('SSA'), 'fil-PH', 'Mga Salesperson' UNION ALL
SELECT core.get_menu_id('SSB'), 'fil-PH', 'Mga Tatak' UNION ALL
SELECT core.get_menu_id('SSC'), 'fil-PH', 'Compound Item' UNION ALL
SELECT core.get_menu_id('SSCD'), 'fil-PH', 'Mga Detalye Compound Item' UNION ALL
SELECT core.get_menu_id('SSI'), 'fil-PH', 'Pagpapanatili item' UNION ALL
SELECT core.get_menu_id('SSM'), 'fil-PH', 'Pag-setup at Pagpapanatili' UNION ALL
SELECT core.get_menu_id('SSP'), 'fil-PH', 'Patakaran sa Store' UNION ALL
SELECT core.get_menu_id('SSS'), 'fil-PH', 'Setup ng estado' UNION ALL
SELECT core.get_menu_id('SST'), 'fil-PH', 'Sales Mga Koponan' UNION ALL
SELECT core.get_menu_id('STA'), 'fil-PH', 'stock Adjustments' UNION ALL
SELECT core.get_menu_id('STJ'), 'fil-PH', 'Stock Transfer Journal' UNION ALL
SELECT core.get_menu_id('STO'), 'fil-PH', 'Tindahan' UNION ALL
SELECT core.get_menu_id('STST'), 'fil-PH', 'Benta ng estado ang mga buwis' UNION ALL
SELECT core.get_menu_id('STT'), 'fil-PH', 'Mga Uri ng Store' UNION ALL
SELECT core.get_menu_id('STX'), 'fil-PH', 'Sales Buwis' UNION ALL
SELECT core.get_menu_id('STXD'), 'fil-PH', 'Mga Detalye ng Buwis sa Pagbebenta' UNION ALL
SELECT core.get_menu_id('STXEX'), 'fil-PH', 'Hindi isinasama ang mga Buwis sa Pagbebenta' UNION ALL
SELECT core.get_menu_id('STXEXD'), 'fil-PH', 'Mga Detalye Exempt Buwis sa Pagbebenta' UNION ALL
SELECT core.get_menu_id('STXT'), 'fil-PH', 'Mga Uri ng Buwis sa Pagbebenta' UNION ALL
SELECT core.get_menu_id('SUM'), 'fil-PH', 'Pamamahala ng Gumagamit' UNION ALL
SELECT core.get_menu_id('SVV'), 'fil-PH', 'Patakaran sa Pag-verify ng Voucher' UNION ALL
SELECT core.get_menu_id('TB'), 'fil-PH', 'Pagsubok Balanse' UNION ALL
SELECT core.get_menu_id('TRF'), 'fil-PH', 'Ang Flag' UNION ALL
SELECT core.get_menu_id('TXA'), 'fil-PH', 'Awtoridad ng Buwis' UNION ALL
SELECT core.get_menu_id('TXEXT'), 'fil-PH', 'Tax Exempt Mga Uri' UNION ALL
SELECT core.get_menu_id('TXM'), 'fil-PH', 'Buwis Master' UNION ALL
SELECT core.get_menu_id('UER'), 'fil-PH', 'I-update ang mga rate Exchange' UNION ALL
SELECT core.get_menu_id('UOM'), 'fil-PH', 'Unit ng Pagsukat';
