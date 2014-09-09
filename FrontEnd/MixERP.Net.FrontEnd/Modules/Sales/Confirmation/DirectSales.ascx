<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DirectSales.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Confirmation.DirectSales"
    OverridePath="/Modules/Sales/DirectSales.html" %>
<mixerp:TransactionChecklist runat="server"
    DisplayWithdrawButton="true"
    DisplayViewInvoiceButton="true"
    DisplayEmailInvoiceButton="true"
    DisplayCustomerInvoiceButton="true"
    DisplayPrintReceiptButton="true"
    DisplayPrintGLEntryButton="true"
    DisplayAttachmentButton="true"
    InvoicePath="~/Modules/Sales/Reports/DirectSalesInvoiceReport.html"
    CustomerInvoicePath="~/Modules/Report/Confirmation/CustomerInvoiceReport.html"
    GLAdvicePath="~/Finance/Confirmation/GLAdvice.aspx" />