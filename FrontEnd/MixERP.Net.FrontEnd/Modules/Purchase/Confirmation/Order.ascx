<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Order.ascx.cs" Inherits="MixERP.Net.Core.Modules.Purchase.Confirmation.Order"
    OverridePath="/Modules/Purchase/Order.html" %>

<mixerp:TransactionChecklist runat="server"
    DisplayWithdrawButton="true"
    DisplayViewInvoiceButton="true"
    DisplayEmailInvoiceButton="true"
    DisplayCustomerInvoiceButton="false"
    DisplayPrintReceiptButton="false"
    DisplayPrintGLEntryButton="true"
    DisplayAttachmentButton="true"
    InvoicePath="~/Modules/Purchase/Confirmation/DirectPurchaseInvoice.html"
    GLAdvicePath="~/Finance/Confirmation/GLAdvice.aspx" />