<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DirectPurchase.ascx.cs" Inherits="MixERP.Net.Core.Modules.Purchase.Confirmation.DirectPurchase"
    OverridePath="/Modules/Purchase/DirectPurchase.mix" %>
<mixerp:TransactionChecklist runat="server"
    DisplayWithdrawButton="true"
    DisplayViewInvoiceButton="true"
    DisplayEmailInvoiceButton="true"
    DisplayCustomerInvoiceButton="false"
    DisplayPrintReceiptButton="false"
    DisplayPrintGlEntryButton="true"
    DisplayAttachmentButton="true"
    InvoicePath="~/Modules/Purchase/Confirmation/DirectPurchaseInvoice.mix"
    GlAdvicePath="~/Modules/Finance/Reports/GLAdviceReport.mix" />