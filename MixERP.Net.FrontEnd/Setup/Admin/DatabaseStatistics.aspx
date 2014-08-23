<%-- 
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/ContentMaster.Master" AutoEventWireup="true" CodeBehind="DatabaseStatistics.aspx.cs" Inherits="MixERP.Net.FrontEnd.Setup.Admin.DatabaseStatistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <asp:PlaceHolder ID="ScrudPlaceholder" runat="server" />

    <h1>Maintenance</h1>
    
    <asp:Literal ID="MessageLiteral" runat="server" />

    <asp:Button ID="VacuumButton" runat="server" CssClass="button" OnClick="VacuumButton_Click" />
    <asp:Button ID="FullVacuumButton" runat="server" CssClass="button" OnClick="FullVacuumButton_Click" />
    <asp:Button ID="AnalyzeButton" runat="server" CssClass="button" OnClick="AnalyzeButton_Click" />

    <br />
    <br />


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceholder" runat="server">
</asp:Content>
