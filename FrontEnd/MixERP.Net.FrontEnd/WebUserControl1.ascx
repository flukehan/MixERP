<%--
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
along with MixERP.  If not, see <http://www.gnu.org/licenses />.
--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControl1.ascx.cs" Inherits="MixERP.Net.FrontEnd.WebUserControl1" %>

<div id="tree">
    <ul id="treeData">
        <li><a title="Dashboard" href="javascript:void(0);" class="primary" id="primaryAnchor1">Dashboard</a></li>
        <li><a title="Sales" href="javascript:void(0);" class="primary" id="primaryAnchor2">Sales</a><ul>
            <li><a href="javascript:void(0);">Sales &amp; Quotation</a><ul>
                <li><a data-menucode="DRS" title="Direct Sales" href="/Modules/Sales/DirectSales.mix">Direct Sales</a></li>
                <li><a data-menucode="SQ" title="Sales Quotation" href="/Modules/Sales/Quotation.mix">Sales Quotation</a></li>
                <li><a data-menucode="SO" title="Sales Order" href="/Modules/Sales/Order.mix">Sales Order</a></li>
                <li><a data-menucode="SD" title="Sales Delivery" href="/Modules/Sales/Delivery.mix">Sales Delivery</a></li>
                <li><a data-menucode="RFC" title="Receipt from Customer" href="/Modules/Sales/Receipt.mix">Receipt from Customer</a></li>
                <li><a data-menucode="SR" title="Sales Return" href="/Modules/Sales/Return.mix">Sales Return</a></li>
            </ul>
            </li>
            <li><a href="javascript:void(0);">Setup &amp; Maintenance</a><ul>
                <li><a data-menucode="ABS" title="Bonus Slab for Salespersons" href="/Modules/Sales/Setup/BonusSlabs.mix">Bonus Slab for Salespersons</a></li>
                <li><a data-menucode="BSD" title="Bonus Slab Details" href="/Modules/Sales/Setup/BonusSlabDetails.mix">Bonus Slab Details</a></li>
                <li><a data-menucode="SST" title="Sales Teams" href="/Modules/Sales/Setup/Teams.mix">Sales Teams</a></li>
                <li><a data-menucode="SSA" title="Salespersons" href="/Modules/Sales/Setup/Salespersons.mix">Salespersons</a></li>
                <li><a data-menucode="BSA" title="Bonus Slab Assignment" href="/Modules/Sales/Setup/BonusSlabAssignment.mix">Bonus Slab Assignment</a></li>
            </ul>
            </li>
            <li><a href="javascript:void(0);">Sales Reports</a><ul>
                <li><a data-menucode="SAR-TSI" title="Top Selling Items" href="/Modules/Sales/Reports/TopSellingItems.mix">Top Selling Items</a></li>
            </ul>
            </li>
        </ul>
        </li>
        <li><a title="Purchase" href="javascript:void(0);" class="primary" id="primaryAnchor3">Purchase</a><ul>
            <li><a href="javascript:void(0);">Purchase &amp; Quotation</a><ul>
                <li><a data-menucode="DRP" title="Direct Purchase" href="/Modules/Purchase/DirectPurchase.mix">Direct Purchase</a></li>
                <li><a data-menucode="PO" title="Purchase Order" href="/Modules/Purchase/Order.mix">Purchase Order</a></li>
                <li><a data-menucode="GRN" title="GRN Entry" href="/Modules/Purchase/GRN.mix">GRN Entry</a></li>
                <li><a data-menucode="PAS" title="Payment to Supplier" href="/Modules/Purchase/Payment.mix">Payment to Supplier</a></li>
                <li><a data-menucode="PR" title="Purchase Return" href="/Modules/Purchase/Return.mix">Purchase Return</a></li>
            </ul>
            </li>
            <li><a href="javascript:void(0);">Purchase Reports</a></li>
        </ul>
        </li>
        <li><a title="Products &amp; Items" href="javascript:void(0);" class="primary" id="primaryAnchor4">Products &amp; Items</a><ul>
            <li><a href="javascript:void(0);">Inventory Movements</a><ul>
                <li><a data-menucode="STJ" title="Stock Transfer Journal" href="/Modules/Inventory/Transfer.mix">Stock Transfer Journal</a></li>
                <li><a data-menucode="STA" title="Stock Adjustments" href="/Modules/Inventory/Adjustment.mix">Stock Adjustments</a></li>
            </ul>
            </li>
            <li><a href="javascript:void(0);">Setup &amp; Maintenance</a><ul>
                <li><a data-menucode="PT" title="Party Types" href="/Modules/Inventory/Setup/PartyTypes.mix">Party Types</a></li>
                <li><a data-menucode="PA" title="Party Accounts" href="/Modules/Inventory/Setup/Parties.mix">Party Accounts</a></li>
                <li><a data-menucode="PSA" title="Shipping Addresses" href="/Modules/Inventory/Setup/ShippingAddresses.mix">Shipping Addresses</a></li>
                <li><a data-menucode="SSI" title="Item Maintenance" href="/Modules/Inventory/Setup/Items.mix">Item Maintenance</a></li>
                <li><a data-menucode="SSC" title="Compound Items" href="/Modules/Inventory/Setup/CompoundItems.mix">Compound Items</a></li>
                <li><a data-menucode="SSCD" title="Compound Item Details" href="/Modules/Inventory/Setup/CompoundItemDetails.mix">Compound Item Details</a></li>
                <li><a data-menucode="ICP" title="Cost Prices" href="/Modules/Inventory/Setup/CostPrices.mix">Cost Prices</a></li>
                <li><a data-menucode="ISP" title="Selling Prices" href="/Modules/Inventory/Setup/SellingPrices.mix">Selling Prices</a></li>
                <li><a data-menucode="SSG" title="Item Groups" href="/Modules/Inventory/Setup/ItemGroups.mix">Item Groups</a></li>
                <li><a data-menucode="SSB" title="Brands" href="/Modules/Inventory/Setup/Brands.mix">Brands</a></li>
                <li><a data-menucode="UOM" title="Units of Measure" href="/Modules/Inventory/Setup/UOM.mix">Units of Measure</a></li>
                <li><a data-menucode="CUOM" title="Compound Units of Measure" href="/Modules/Inventory/Setup/CUOM.mix">Compound Units of Measure</a></li>
                <li><a data-menucode="SHI" title="Shipper Information" href="/Modules/Inventory/Setup/Shippers.mix">Shipper Information</a></li>
            </ul>
            </li>
        </ul>
        </li>
        <li><a title="Finance" href="javascript:void(0);" class="primary" id="primaryAnchor5">Finance</a><ul>
            <li><a href="javascript:void(0);">Transactions &amp; Templates</a><ul>
                <li><a data-menucode="JVN" title="Journal Voucher Entry" href="/Modules/Finance/JournalVoucher.mix">Journal Voucher Entry</a></li>
                <li><a data-menucode="TTR" title="Template Transaction" href="/Modules/Finance/TemplateTransaction.mix">Template Transaction</a></li>
                <li><a data-menucode="STN" title="Standing Instructions" href="/Modules/Finance/StandingInstructions.mix">Standing Instructions</a></li>
                <li><a data-menucode="UER" title="Update Exchange Rates" href="/Modules/Finance/UpdateExchangeRates.mix">Update Exchange Rates</a></li>
                <li><a data-menucode="RBA" title="Reconcile Bank Account" href="/Modules/Finance/BankReconciliation.mix">Reconcile Bank Account</a></li>
                <li><a data-menucode="FVV" title="Voucher Verification" href="/Modules/Finance/VoucherVerification.mix">Voucher Verification</a></li>
                <li><a data-menucode="FTDM" title="Transaction Document Manager" href="/Modules/Finance/TransactionDocumentManager.mix">Transaction Document Manager</a></li>
            </ul>
            </li>
            <li><a href="javascript:void(0);">Setup &amp; Maintenance</a><ul>
                <li><a data-menucode="COA" title="Chart of Accounts" href="/Modules/Finance/Setup/COA.mix">Chart of Accounts</a></li>
                <li><a data-menucode="CUR" title="Currency Management" href="/Modules/Finance/Setup/Currencies.mix">Currency Management</a></li>
                <li><a data-menucode="CBA" title="Bank Accounts" href="/Modules/Finance/Setup/BankAccounts.mix">Bank Accounts</a></li>
                <li><a data-menucode="PGM" title="Product GL Mapping" href="/Modules/Finance/Setup/ProductGLMapping.mix">Product GL Mapping</a></li>
                <li><a data-menucode="BT" title="Budgets &amp; Targets" href="/Modules/Finance/Setup/BudgetAndTarget.mix">Budgets &amp; Targets</a></li>
                <li><a data-menucode="AGS" title="Ageing Slabs" href="/Modules/Finance/Setup/AgeingSlabs.mix">Ageing Slabs</a></li>
                <li><a data-menucode="TTY" title="Tax Types" href="/Modules/Finance/Setup/TaxTypes.mix">Tax Types</a></li>
                <li><a data-menucode="TS" title="Tax Setup" href="/Modules/Finance/Setup/TaxSetup.mix">Tax Setup</a></li>
                <li><a data-menucode="CC" title="Cost Centers" href="/Modules/Finance/Setup/CostCenters.mix">Cost Centers</a></li>
            </ul>
            </li>
        </ul>
        </li>
        <li><a title="Manufacturing" href="javascript:void(0);" class="primary" id="primaryAnchor6">Manufacturing</a><ul>
            <li><a href="javascript:void(0);">Manufacturing Workflow</a><ul>
                <li><a data-menucode="MFWSF" title="Sales Forecast" href="/Modules/Manufacturing/Workflow/SalesForecast.mix">Sales Forecast</a></li>
                <li><a data-menucode="MFWMPS" title="Master Production Schedule" href="/Modules/Manufacturing/Workflow/MasterProductionSchedule.mix">Master Production Schedule</a></li>
            </ul>
            </li>
            <li><a href="javascript:void(0);">Manufacturing Setup</a><ul>
                <li><a data-menucode="MFSWC" title="Work Centers" href="/Modules/Manufacturing/Setup/WorkCenters.mix">Work Centers</a></li>
                <li><a data-menucode="MFSBOM" title="Bills of Material" href="/Modules/Manufacturing/Setup/BillsOfMaterial.mix">Bills of Material</a></li>
            </ul>
            </li>
            <li><a href="javascript:void(0);">Manufacturing Reports</a><ul>
                <li><a data-menucode="MFRGNR" title="Gross &amp; Net Requirements" href="/Modules/Manufacturing/Reports/GrossAndNetRequirements.mix">Gross &amp; Net Requirements</a></li>
                <li><a data-menucode="MFRCVSL" title="Capacity vs Lead" href="/Modules/Manufacturing/Reports/CapacityVersusLead.mix">Capacity vs Lead</a></li>
                <li><a data-menucode="MFRSFP" title="Shop Floor Planning" href="/Modules/Manufacturing/Reports/ShopFloorPlanning.mix">Shop Floor Planning</a></li>
                <li><a data-menucode="MFRPOS" title="Production Order Status" href="/Modules/Manufacturing/Reports/ProductionOrderStatus.mix">Production Order Status</a></li>
            </ul>
            </li>
        </ul>
        </li>
        <li><a title="CRM" href="javascript:void(0);" class="primary" id="primaryAnchor7">CRM</a><ul>
            <li><a href="javascript:void(0);">CRM Main</a><ul>
                <li><a data-menucode="CRML" title="Add a New Lead" href="/Modules/CRM/Lead.mix">Add a New Lead</a></li>
                <li><a data-menucode="CRMO" title="Add a New Opportunity" href="/Modules/CRM/Opportunity.mix">Add a New Opportunity</a></li>
                <li><a data-menucode="CRMC" title="Convert Lead to Opportunity" href="/Modules/CRM/ConvertLeadToOpportunity.mix">Convert Lead to Opportunity</a></li>
                <li><a data-menucode="CRMFL" title="Lead Follow Up" href="/Modules/CRM/LeadFollowUp.mix">Lead Follow Up</a></li>
                <li><a data-menucode="CRMFO" title="Opportunity Follow Up" href="/Modules/CRM/OpportunityFollowUp.mix">Opportunity Follow Up</a></li>
            </ul>
            </li>
            <li><a href="javascript:void(0);">Setup &amp; Maintenance</a><ul>
                <li><a data-menucode="CRMLS" title="Lead Sources Setup" href="/Modules/CRM/Setup/LeadSources.mix">Lead Sources Setup</a></li>
                <li><a data-menucode="CRMLST" title="Lead Status Setup" href="/Modules/CRM/Setup/LeadStatuses.mix">Lead Status Setup</a></li>
                <li><a data-menucode="CRMOS" title="Opportunity Stages Setup" href="/Modules/CRM/Setup/OpportunityStages.mix">Opportunity Stages Setup</a></li>
            </ul>
            </li>
        </ul>
        </li>
        <li><a title="Back Office" href="javascript:void(0);" class="primary" id="primaryAnchor8">Back Office</a><ul>
            <li><a href="javascript:void(0);">Miscellaneous Parameters</a><ul>
                <li><a data-menucode="TRF" title="Flags" href="/Modules/BackOffice/Flags.mix">Flags</a></li>
            </ul>
            </li>
            <li><a href="javascript:void(0);">Audit Reports</a><ul>
                <li><a data-menucode="SEAR-LV" title="Login View" href="/Reports/Office.Login.xml">Login View</a></li>
            </ul>
            </li>
            <li><a href="javascript:void(0);">Office Setup</a><ul>
                <li><a data-menucode="SOB" title="Office &amp; Branch Setup" href="/Modules/BackOffice/Offices.mix">Office &amp; Branch Setup</a></li>
                <li><a data-menucode="SCR" title="Cash Repository Setup" href="/Modules/BackOffice/CashRepositories.mix">Cash Repository Setup</a></li>
                <li><a data-menucode="SDS" title="Department Setup" href="/Modules/BackOffice/Departments.mix">Department Setup</a></li>
                <li><a data-menucode="SRM" title="Role Management" href="/Modules/BackOffice/Roles.mix">Role Management</a></li>
                <li><a data-menucode="SUM" title="User Management" href="/Modules/BackOffice/Users.mix">User Management</a></li>
                <li><a data-menucode="SFY" title="Fiscal Year Information" href="/Modules/BackOffice/FiscalYear.mix">Fiscal Year Information</a></li>
                <li><a data-menucode="SFR" title="Frequency &amp; Fiscal Year Management" href="/Modules/BackOffice/Frequency.mix">Frequency &amp; Fiscal Year Management</a></li>
            </ul>
            </li>
            <li><a href="javascript:void(0);">Policy Management</a><ul>
                <li><a data-menucode="SVV" title="Voucher Verification Policy" href="/Modules/BackOffice/Policy/VoucherVerification.mix">Voucher Verification Policy</a></li>
                <li><a data-menucode="SAV" title="Automatic Verification Policy" href="/Modules/BackOffice/Policy/AutoVerification.mix">Automatic Verification Policy</a></li>
                <li><a data-menucode="SMA" title="Menu Access Policy" href="/Modules/BackOffice/Policy/MenuAccess.mix">Menu Access Policy</a></li>
                <li><a data-menucode="SAP" title="GL Access Policy" href="/Modules/BackOffice/Policy/GLAccess.mix">GL Access Policy</a></li>
                <li><a data-menucode="SSP" title="Store Policy" href="/Modules/BackOffice/Policy/Store.mix">Store Policy</a></li>
                <li><a data-menucode="SWI" title="Switches" href="/Modules/BackOffice/Policy/Switches.mix">Switches</a></li>
            </ul>
            </li>
            <li><a href="javascript:void(0);">Admin Tools</a><ul>
                <li><a data-menucode="SQL" title="SQL Query Tool" href="/Modules/BackOffice/Admin/Query.mix">SQL Query Tool</a></li>
                <li><a data-menucode="DBSTAT" title="Database Statistics" href="/Modules/BackOffice/Admin/DatabaseStatistics.mix">Database Statistics</a></li>
                <li><a data-menucode="BAK" title="Backup Database" href="/Modules/BackOffice/Admin/Backup.mix">Backup Database</a></li>
                <li><a data-menucode="RES" title="Restore Database" href="/Modules/BackOffice/Admin/Restore.mix">Restore Database</a></li>
                <li><a data-menucode="PWD" title="Change User Password" href="/Modules/BackOffice/Admin/ChangePassword.mix">Change User Password</a></li>
                <li><a data-menucode="NEW" title="New Company" href="/Modules/BackOffice/Admin/NewCompany.mix">New Company</a></li>
            </ul>
            </li>
        </ul>
        </li>
        <li><a title="POS" href="javascript:void(0);" class="primary" id="primaryAnchor9">POS</a><ul>
            <li><a href="javascript:void(0);">Cashier Management</a><ul>
                <li><a data-menucode="ASC" title="Assign Cashier" href="/Modules/POS/AssignCashier.mix">Assign Cashier</a></li>
            </ul>
            </li>
            <li><a href="javascript:void(0);">POS Setup</a><ul>
                <li><a data-menucode="STT" title="Store Types" href="/Modules/POS/Setup/StoreTypes.mix">Store Types</a></li>
                <li><a data-menucode="STO" title="Stores" href="/Modules/POS/Setup/Stores.mix">Stores</a></li>
                <li><a data-menucode="SCS" title="Counter Setup" href="/Modules/BackOffice/Counters.mix">Counter Setup</a></li>
            </ul>
            </li>
        </ul>
        </li>
    </ul>
</div>