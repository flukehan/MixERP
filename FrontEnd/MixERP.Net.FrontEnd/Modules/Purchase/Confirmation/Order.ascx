<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Order.ascx.cs" Inherits="MixERP.Net.Core.Modules.Purchase.Confirmation.Order"
    OverridePath="/Modules/Purchase/Order.mix" %>

<mixerp:TransactionChecklist runat="server"
    DisplayWithdrawButton="true"
    DisplayViewInvoiceButton="true"
    DisplayEmailInvoiceButton="true"
    DisplayCustomerInvoiceButton="false"
    DisplayPrintReceiptButton="false"
    DisplayPrintGlEntryButton="true"
    DisplayAttachmentButton="true"
    InvoicePath="~/Modules/Purchase/Confirmation/DirectPurchaseInvoice.mix"
    GlAdvicePath="~/Modules/Finance/Confirmation/GLAdvice.mix" />