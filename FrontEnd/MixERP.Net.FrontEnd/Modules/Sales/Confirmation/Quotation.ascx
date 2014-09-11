<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Quotation.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Confirmation.Quotation"
    OverridePath="/Modules/Sales/Quotation.mix" %>
<mixerp:TransactionChecklist runat="server"
    DisplayWithdrawButton="false"
    DisplayViewInvoiceButton="true"
    DisplayEmailInvoiceButton="true"
    DisplayCustomerInvoiceButton="true"
    DisplayPrintReceiptButton="true"
    DisplayPrintGlEntryButton="true"
    DisplayAttachmentButton="true"
    IsNonGlTransaction="true"
    InvoicePath="~/Modules/Sales/Reports/DirectSalesInvoiceReport.mix"
    CustomerInvoicePath="~/Modules/Sales/Reports/CustomerInvoiceReport.mix"
    GlAdvicePath="~/Modules/Finance/Reports/GLAdviceReport.mix" />