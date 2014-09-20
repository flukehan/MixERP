<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DirectSales.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Confirmation.DirectSales"
    OverridePath="/Modules/Sales/DirectSales.mix" %>
<mixerp:TransactionChecklist runat="server"
    OverridePath="/Modules/Sales/DirectSales.mix"
    DisplayWithdrawButton="true"
    DisplayViewInvoiceButton="true"
    DisplayEmailInvoiceButton="true"
    DisplayCustomerInvoiceButton="true"
    DisplayPrintReceiptButton="true"
    DisplayPrintGlEntryButton="true"
    DisplayAttachmentButton="true"
    InvoicePath="~/Modules/Sales/Reports/DirectSalesInvoiceReport.mix"
    CustomerInvoicePath="~/Modules/Sales/Reports/CustomerInvoiceReport.mix"
    GlAdvicePath="~/Modules/Finance/Reports/GLAdviceReport.mix" />