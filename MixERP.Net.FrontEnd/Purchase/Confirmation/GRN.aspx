<%@ Page Title="" Language="C#" MasterPageFile="~/ContentMaster.Master" AutoEventWireup="true" CodeBehind="GRN.aspx.cs" Inherits="MixERP.Net.FrontEnd.Purchase.Confirmation.GRN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <mixerp:TransactionChecklist runat="server"
        DisplayWithdrawButton="true"
        DisplayViewInvoiceButton="true"
        DisplayEmailInvoiceButton="true"
        DisplayCustomerInvoiceButton="false"
        DisplayPrintReceiptButton="false"
        DisplayPrintGLEntryButton="true"
        DisplayAttachmentButton="true"
        InvoicePath="~/Purchase/Confirmation/DirectPurchaseInvoice.aspx"
        GLAdvicePath="~/Finance/Confirmation/GLAdvice.aspx"        
         />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceholder" runat="server">
</asp:Content>
