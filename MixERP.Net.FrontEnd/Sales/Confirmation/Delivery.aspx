<%@ Page Title="" Language="C#" MasterPageFile="~/ContentMaster.Master" AutoEventWireup="true" CodeBehind="Delivery.aspx.cs" Inherits="MixERP.Net.FrontEnd.Sales.Confirmation.Delivery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <mixerp:TransactionChecklist runat="server"
        DisplayWithdrawButton="true"
        DisplayViewInvoiceButton="true"
        DisplayEmailInvoiceButton="true"
        DisplayCustomerInvoiceButton="true"
        DisplayPrintReceiptButton="true"
        DisplayPrintGLEntryButton="true"
        DisplayAttachmentButton="true"
        InvoicePath="~/Sales/Confirmation/ReportDelivery.aspx"
        CustomerInvoicePath="~/Sales/Confirmation/ReportDeliveryNote.aspx"
        GLAdvicePath="~/Finance/Confirmation/GLAdvice.aspx"
         />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceholder" runat="server">
</asp:Content>
