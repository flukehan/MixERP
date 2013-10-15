<%-- 
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/ContentMaster.Master" AutoEventWireup="true" CodeBehind="DatabaseStatistics.aspx.cs" Inherits="MixERP.Net.FrontEnd.Setup.Admin.DatabaseStatistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
   <AjaxCTK:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />

    <h1>Maintenance</h1>
    <hr class="hr" />
    <asp:Literal ID="MessageLiteral" runat="server" />

    <asp:Button ID="VacuumButton" runat="server" CssClass="button" OnClick="VacuumButton_Click" />
    <asp:Button ID="FullVacuumButton" runat="server" CssClass="button" OnClick="FullVacuumButton_Click" />
    <asp:Button ID="AnalyzeButton" runat="server" CssClass="button" OnClick="AnalyzeButton_Click" />

    <br />
    <br />


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceHolder" runat="server">
</asp:Content>