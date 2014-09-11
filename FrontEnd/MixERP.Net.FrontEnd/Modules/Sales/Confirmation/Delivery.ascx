<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Delivery.ascx.cs" Inherits="MixERP.Net.Core.Modules.Sales.Confirmation.Delivery"
    OverridePath="/Modules/Sales/Delivery.mix" %>
<mixerp:TransactionChecklist runat="server"
    DisplayWithdrawButton="true"
    DisplayViewInvoiceButton="true"
    DisplayEmailInvoiceButton="true"
    DisplayCustomerInvoiceButton="true"
    DisplayPrintReceiptButton="true"
    DisplayPrintGlEntryButton="true"
    DisplayAttachmentButton="true"
    InvoicePath="~/Modules/Sales/Reports/DeliveryReport.mix"
    CustomerInvoicePath="~/Modules/Sales/Reports/DeliveryNoteReport.mix"
    GlAdvicePath="~/Modules/Finance/Reports/GLAdviceReport.mix" />