<%@ Page Title="" Language="C#" MasterPageFile="~/ContentMaster.Master" AutoEventWireup="true" CodeBehind="DatabaseStatistics.aspx.cs" Inherits="MixERP.Net.FrontEnd.Setup.Admin.DatabaseStatistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <asp:PlaceHolder ID="ScrudPlaceholder" runat="server" />

    <h1>Maintenance</h1>
    <hr class="hr" />
    <asp:Literal ID="MessageLiteral" runat="server" />

    <asp:Button ID="VacuumButton" runat="server" CssClass="button" OnClick="VacuumButton_Click" />
    <asp:Button ID="FullVacuumButton" runat="server" CssClass="button" OnClick="FullVacuumButton_Click" />
    <asp:Button ID="AnalyzeButton" runat="server" CssClass="button" OnClick="AnalyzeButton_Click" />

    <br />
    <br />


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceholder" runat="server">
</asp:Content>
