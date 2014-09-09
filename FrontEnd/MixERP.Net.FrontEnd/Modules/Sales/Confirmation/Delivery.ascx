<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Delivery.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Confirmation.Delivery"
    OverridePath="/Modules/Sales/Delivery.html" %>
<mixerp:TransactionChecklist runat="server"
    DisplayWithdrawButton="true"
    DisplayViewInvoiceButton="true"
    DisplayEmailInvoiceButton="true"
    DisplayCustomerInvoiceButton="true"
    DisplayPrintReceiptButton="true"
    DisplayPrintGLEntryButton="true"
    DisplayAttachmentButton="true"
    InvoicePath="~/Modules/Sales/Reports/DeliveryReport.html"
    CustomerInvoicePath="~/Modules/Sales/Reports/DeliveryNoteReport.html"
    GLAdvicePath="~/Finance/Confirmation/GLAdvice.aspx" />