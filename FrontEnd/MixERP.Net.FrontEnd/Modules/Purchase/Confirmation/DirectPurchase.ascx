<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DirectPurchase.ascx.cs" Inherits="MixERP.Net.Core.Modules.Purchase.Confirmation.DirectPurchase"
    OverridePath="/Modules/Purchase/DirectPurchase.html" %>
<mixerp:TransactionChecklist runat="server"
    DisplayWithdrawButton="true"
    DisplayViewInvoiceButton="true"
    DisplayEmailInvoiceButton="true"
    DisplayCustomerInvoiceButton="false"
    DisplayPrintReceiptButton="false"
    DisplayPrintGlEntryButton="true"
    DisplayAttachmentButton="true"
    InvoicePath="~/Modules/Purchase/Confirmation/DirectPurchaseInvoice.html"
    GlAdvicePath="~/Finance/Confirmation/GLAdvice.aspx" />