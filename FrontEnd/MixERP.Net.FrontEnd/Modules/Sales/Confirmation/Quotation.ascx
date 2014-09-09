<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Quotation.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Confirmation.Quotation"
    OverridePath="/Modules/Sales/Quotation.html" %>
<mixerp:TransactionChecklist runat="server"
    DisplayWithdrawButton="false"
    DisplayViewInvoiceButton="true"
    DisplayEmailInvoiceButton="true"
    DisplayCustomerInvoiceButton="true"
    DisplayPrintReceiptButton="true"
    DisplayPrintGLEntryButton="true"
    DisplayAttachmentButton="true"
    IsNonGLTransaction="true"
    InvoicePath="~/Modules/Sales/Reports/DirectSalesInvoiceReport.html"
    CustomerInvoicePath="~/Modules/Sales/Reports/CustomerInvoiceReport.html"
    GLAdvicePath="~/Finance/Confirmation/GLAdvice.aspx" />