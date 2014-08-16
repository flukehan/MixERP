<%@ Page Title="" Language="C#" MasterPageFile="~/ContentMaster.Master" AutoEventWireup="true" CodeBehind="Delivery.aspx.cs" Inherits="MixERP.Net.FrontEnd.Sales.Delivery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <mixerp:ProductView
        runat="server"
        Book="Sales" SubBook="Delivery"
        Text="<%$Resources:Titles, SalesDelivery %>"
        AddNewUrl="~/Sales/Entry/Delivery.aspx"
        PreviewUrl="~/Sales/Confirmation/ReportSalesDelivery.aspx"
        ChecklistUrl="~/Sales/Confirmation/Delivery.aspx" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceholder" runat="server">
</asp:Content>
