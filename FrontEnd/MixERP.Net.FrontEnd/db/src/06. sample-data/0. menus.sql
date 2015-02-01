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
UNION ALL SELECT 'Product GL Mapping', '~/Modules/Finance/Setup/ProductGLMapping.mix', 'PGM', 2, core.get_menu_id('FSM')
UNION ALL SELECT 'Budgets & Targets', '~/Modules/Finance/Setup/BudgetAndTarget.mix', 'BT', 2, core.get_menu_id('FSM')
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
UNION ALL SELECT 'Switches', '~/Modules/BackOffice/Policy/Switches.mix', 'SWI', 2, core.get_menu_id('SPM')
UNION ALL SELECT 'Admin Tools', NULL, 'SAT', 1, core.get_menu_id('BO')
UNION ALL SELECT 'SQL Query Tool', '~/Modules/BackOffice/Admin/Query.mix', 'SQL', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'Database Statistics', '~/Modules/BackOffice/Admin/DatabaseStatistics.mix', 'DBSTAT', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'Backup Database', '~/Modules/BackOffice/Admin/DatabaseBackup.mix', 'BAK', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'Change User Password', '~/Modules/BackOffice/Admin/ChangePassword.mix', 'PWD', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'New Company', '~/Modules/BackOffice/Admin/NewCompany.mix', 'NEW', 2, core.get_menu_id('SAT')
UNION ALL SELECT 'One Time Setup', NULL, 'OTS', 1, core.get_menu_id('BO')
UNION ALL SELECT 'Opening Inventory', '~/Modules/BackOffice/OTS/OpeningInventory.mix', 'OTSI', 2, core.get_menu_id('OTS');


INSERT INTO policy.menu_access(office_id, menu_id, user_id)
SELECT office.get_office_id_by_office_code('MoF-NY-BK'), core.menus.menu_id, office.get_user_id_by_user_name('binod')
FROM core.menus

UNION ALL
SELECT office.get_office_id_by_office_code('MoF-NY-RV'), core.menus.menu_id, office.get_user_id_by_user_name('binod')
FROM core.menus

UNION ALL
SELECT office.get_office_id_by_office_code('MoF-NP-KTM'), core.menus.menu_id, office.get_user_id_by_user_name('binod')
FROM core.menus

UNION ALL
SELECT office.get_office_id_by_office_code('MoF-NY-BK'), core.menus.menu_id, office.get_user_id_by_user_name('nirvan')
FROM core.menus

UNION ALL
SELECT office.get_office_id_by_office_code('MoF-NY-RV'), core.menus.menu_id, office.get_user_id_by_user_name('nirvan')
FROM core.menus

UNION ALL
SELECT office.get_office_id_by_office_code('MoF-NP-KTM'), core.menus.menu_id, office.get_user_id_by_user_name('nirvan')
FROM core.menus

UNION ALL

SELECT office.get_office_id_by_office_code('MoF-NY-BK'), core.menus.menu_id, office.get_user_id_by_user_name('demo')
FROM core.menus

UNION ALL
SELECT office.get_office_id_by_office_code('MoF-NY-RV'), core.menus.menu_id, office.get_user_id_by_user_name('demo')
FROM core.menus

UNION ALL
SELECT office.get_office_id_by_office_code('MoF-NP-KTM'), core.menus.menu_id, office.get_user_id_by_user_name('demo')
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
SELECT core.get_menu_id('PU'), 'fr', 'achat' UNION ALL
SELECT core.get_menu_id('ITM'), 'fr', 'Produits et Articles' UNION ALL
SELECT core.get_menu_id('FI'), 'fr', 'financement' UNION ALL
SELECT core.get_menu_id('BO'), 'fr', 'Back-Office' UNION ALL
SELECT core.get_menu_id('SAQ'), 'fr', 'Sales & Devis' UNION ALL
SELECT core.get_menu_id('DRS'), 'fr', 'Ventes directes' UNION ALL
SELECT core.get_menu_id('SQ'), 'fr', 'Devis de vente' UNION ALL
SELECT core.get_menu_id('SO'), 'fr', 'Commande client' UNION ALL
SELECT core.get_menu_id('SD'), 'fr', 'Vente livraison' UNION ALL
SELECT core.get_menu_id('RFC'), 'fr', 'Réception du client' UNION ALL
SELECT core.get_menu_id('SR'), 'fr', 'Retour sur les ventes' UNION ALL
SELECT core.get_menu_id('SSM'), 'fr', 'Le programme d''installation & entretien' UNION ALL
SELECT core.get_menu_id('ABS'), 'fr', 'Dalle de bonus pour les vendeurs' UNION ALL
SELECT core.get_menu_id('BSD'), 'fr', 'Détails du bonus dalle' UNION ALL
SELECT core.get_menu_id('SST'), 'fr', 'Équipes de vente' UNION ALL
SELECT core.get_menu_id('SSA'), 'fr', 'Vendeurs/vendeuses' UNION ALL
SELECT core.get_menu_id('BSA'), 'fr', 'Affectation de dalle de bonus' UNION ALL
SELECT core.get_menu_id('LF'), 'fr', 'Frais de retard' UNION ALL
SELECT core.get_menu_id('PAT'), 'fr', 'Conditions de paiement' UNION ALL
SELECT core.get_menu_id('RI'), 'fr', 'Factures récurrentes' UNION ALL
SELECT core.get_menu_id('RIS'), 'fr', 'Paramètres des factures récurrentes' UNION ALL
SELECT core.get_menu_id('SAR'), 'fr', 'Rapports sur les ventes' UNION ALL
SELECT core.get_menu_id('SAR-TSI'), 'fr', 'Haut de la page points de vente' UNION ALL
SELECT core.get_menu_id('PUQ'), 'fr', 'Achat & citation' UNION ALL
SELECT core.get_menu_id('DRP'), 'fr', 'Achat direct' UNION ALL
SELECT core.get_menu_id('PO'), 'fr', 'Bon de commande' UNION ALL
SELECT core.get_menu_id('PRO'), 'fr', 'Achat Reorder' UNION ALL
SELECT core.get_menu_id('GRN'), 'fr', 'Entrée GRN' UNION ALL
SELECT core.get_menu_id('PR'), 'fr', 'Achat de retour' UNION ALL
SELECT core.get_menu_id('PUR'), 'fr', 'Rapports d''achat' UNION ALL
SELECT core.get_menu_id('IIM'), 'fr', 'Mouvements de stock' UNION ALL
SELECT core.get_menu_id('STJ'), 'fr', 'Feuille de transfert de stock' UNION ALL
SELECT core.get_menu_id('STA'), 'fr', 'Ajustements de stocks' UNION ALL
SELECT core.get_menu_id('ISM'), 'fr', 'Le programme d''installation & entretien' UNION ALL
SELECT core.get_menu_id('STT'), 'fr', 'Types de magasins' UNION ALL
SELECT core.get_menu_id('STO'), 'fr', 'Magasins' UNION ALL
SELECT core.get_menu_id('SCS'), 'fr', 'Installation de compteur' UNION ALL
SELECT core.get_menu_id('PT'), 'fr', 'Types de partie' UNION ALL
SELECT core.get_menu_id('PA'), 'fr', 'Comptes de tiers' UNION ALL
SELECT core.get_menu_id('PSA'), 'fr', 'Adresses d''expédition' UNION ALL
SELECT core.get_menu_id('SSI'), 'fr', 'Gestion des Articles' UNION ALL
SELECT core.get_menu_id('SSC'), 'fr', 'Composé d''éléments' UNION ALL
SELECT core.get_menu_id('SSCD'), 'fr', 'Détails de l''élément composé' UNION ALL
SELECT core.get_menu_id('ICP'), 'fr', 'Prix de revient' UNION ALL
SELECT core.get_menu_id('ISP'), 'fr', 'Prix de vente' UNION ALL
SELECT core.get_menu_id('SIG'), 'fr', 'Groupes d''articles' UNION ALL
SELECT core.get_menu_id('SIT'), 'fr', 'Types d''éléments' UNION ALL
SELECT core.get_menu_id('SSB'), 'fr', 'Marques' UNION ALL
SELECT core.get_menu_id('UOM'), 'fr', 'Unités de mesure' UNION ALL
SELECT core.get_menu_id('CUOM'), 'fr', 'Composé d''unités de mesure' UNION ALL
SELECT core.get_menu_id('SHI'), 'fr', 'Informations de l''expéditeur' UNION ALL
SELECT core.get_menu_id('IR'), 'fr', 'Rapports' UNION ALL
SELECT core.get_menu_id('IAS'), 'fr', 'Relevé de compte de l''inventaire' UNION ALL
SELECT core.get_menu_id('FTT'), 'fr', 'Modèles de & de transactions' UNION ALL
SELECT core.get_menu_id('JVN'), 'fr', 'Bon écriture' UNION ALL
SELECT core.get_menu_id('UER'), 'fr', 'Mise à jour des taux de change' UNION ALL
SELECT core.get_menu_id('FVV'), 'fr', 'Vérification du bon' UNION ALL
SELECT core.get_menu_id('EOD'), 'fr', 'Fin de l''opération de la journée' UNION ALL
SELECT core.get_menu_id('FSM'), 'fr', 'Le programme d''installation & entretien' UNION ALL
SELECT core.get_menu_id('COA'), 'fr', 'Plan comptable' UNION ALL
SELECT core.get_menu_id('CUR'), 'fr', 'Gestion de la devise' UNION ALL
SELECT core.get_menu_id('CBA'), 'fr', 'Comptes bancaires' UNION ALL
SELECT core.get_menu_id('PGM'), 'fr', 'Produit GL cartographie' UNION ALL
SELECT core.get_menu_id('BT'), 'fr', 'Les budgets des cibles &' UNION ALL
SELECT core.get_menu_id('AGS'), 'fr', 'Vieillissement des dalles' UNION ALL
SELECT core.get_menu_id('CFH'), 'fr', 'Positions de trésorerie' UNION ALL
SELECT core.get_menu_id('CFS'), 'fr', 'Configuration des flux de trésorerie' UNION ALL
SELECT core.get_menu_id('CC'), 'fr', 'Centres de coûts' UNION ALL
SELECT core.get_menu_id('FIR'), 'fr', 'Rapports' UNION ALL
SELECT core.get_menu_id('AS'), 'fr', 'Relevé de compte' UNION ALL
SELECT core.get_menu_id('TB'), 'fr', 'Balance de vérification' UNION ALL
SELECT core.get_menu_id('PLA'), 'fr', 'Profit & compte de la perte' UNION ALL
SELECT core.get_menu_id('BS'), 'fr', 'Bilan' UNION ALL
SELECT core.get_menu_id('RET'), 'fr', 'Des Bénéfices Non Répartis' UNION ALL
SELECT core.get_menu_id('CF'), 'fr', 'Flux de trésorerie' UNION ALL
SELECT core.get_menu_id('BOTC'), 'fr', 'Configuration de l''impôt' UNION ALL
SELECT core.get_menu_id('TXM'), 'fr', 'Maître de l''impôt' UNION ALL
SELECT core.get_menu_id('TXA'), 'fr', 'Administration fiscale' UNION ALL
SELECT core.get_menu_id('STXT'), 'fr', 'Types de taxe de vente' UNION ALL
SELECT core.get_menu_id('STST'), 'fr', 'État des Taxes de vente' UNION ALL
SELECT core.get_menu_id('CTST'), 'fr', 'Taxes de vente de comtés' UNION ALL
SELECT core.get_menu_id('STX'), 'fr', 'Taxes de vente' UNION ALL
SELECT core.get_menu_id('STXD'), 'fr', 'Détails de la taxe de vente' UNION ALL
SELECT core.get_menu_id('TXEXT'), 'fr', 'Types exonérés de taxe' UNION ALL
SELECT core.get_menu_id('STXEX'), 'fr', 'Exempte de la taxe de vente' UNION ALL
SELECT core.get_menu_id('STXEXD'), 'fr', 'Détails exonéré de taxe de vente' UNION ALL
SELECT core.get_menu_id('SMP'), 'fr', 'Divers paramètres' UNION ALL
SELECT core.get_menu_id('TRF'), 'fr', 'Drapeaux' UNION ALL
SELECT core.get_menu_id('SEAR'), 'fr', 'Rapports d''audit' UNION ALL
SELECT core.get_menu_id('SEAR-LV'), 'fr', 'Vue de l''ouverture de session' UNION ALL
SELECT core.get_menu_id('SOS'), 'fr', 'Installation de Office' UNION ALL
SELECT core.get_menu_id('SOB'), 'fr', 'Bureau & de la direction générale de la configuration' UNION ALL
SELECT core.get_menu_id('SCR'), 'fr', 'Installation de dépôt comptant' UNION ALL
SELECT core.get_menu_id('SDS'), 'fr', 'Département installation' UNION ALL
SELECT core.get_menu_id('SRM'), 'fr', 'Gestion des rôles' UNION ALL
SELECT core.get_menu_id('SUM'), 'fr', 'Gestion des utilisateurs' UNION ALL
SELECT core.get_menu_id('SES'), 'fr', 'Configuration de l''entité' UNION ALL
SELECT core.get_menu_id('SIS'), 'fr', 'Installation de l''industrie' UNION ALL
SELECT core.get_menu_id('SCRS'), 'fr', 'Programme d''installation de pays' UNION ALL
SELECT core.get_menu_id('SSS'), 'fr', 'Installation de l''État' UNION ALL
SELECT core.get_menu_id('SCTS'), 'fr', 'Comté de Setup' UNION ALL
SELECT core.get_menu_id('SFY'), 'fr', 'Informations de l''exercice' UNION ALL
SELECT core.get_menu_id('SFR'), 'fr', 'Fréquence & la gestion de l''exercice' UNION ALL
SELECT core.get_menu_id('SPM'), 'fr', 'Gestion des stratégies de' UNION ALL
SELECT core.get_menu_id('SVV'), 'fr', 'Politique sur la vérification bon' UNION ALL
SELECT core.get_menu_id('SAV'), 'fr', 'Politique sur la vérification automatique' UNION ALL
SELECT core.get_menu_id('SMA'), 'fr', 'Stratégie d''accès menu' UNION ALL
SELECT core.get_menu_id('SAP'), 'fr', 'Stratégie d''accès GL' UNION ALL
SELECT core.get_menu_id('SSP'), 'fr', 'Politique de boutique' UNION ALL
SELECT core.get_menu_id('SWI'), 'fr', 'Commutateurs' UNION ALL
SELECT core.get_menu_id('SAT'), 'fr', 'Outils d''administration' UNION ALL
SELECT core.get_menu_id('SQL'), 'fr', 'Outils d''administration' UNION ALL
SELECT core.get_menu_id('DBSTAT'), 'fr', 'Outil de requête SQL' UNION ALL
SELECT core.get_menu_id('BAK'), 'fr', 'Sauvegarde base de données' UNION ALL
SELECT core.get_menu_id('PWD'), 'fr', 'Changer mot de passe utilisateur' UNION ALL
SELECT core.get_menu_id('NEW'), 'fr', 'Nouvelle société' UNION ALL
SELECT core.get_menu_id('OTS'), 'fr', 'Un réglage de l''heure' UNION ALL
SELECT core.get_menu_id('OTSI'), 'fr', 'Stock d''ouverture';
