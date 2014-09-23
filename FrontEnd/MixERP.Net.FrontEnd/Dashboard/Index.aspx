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
along with MixERP.  If not, see <http://www.gnu.org/licenses />.
--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MixERPMaster.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MixERP.Net.FrontEnd.Dashboard.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">

    <div id="sortable-container" class="container-fluid vpad16">
        <asp:PlaceHolder ID="WidgetPlaceholder" runat="server" />
    </div>

    <div style="clear: both;"></div>
    <br />

    <asp:Button ID="SavePositionButton"
        runat="server"
        Text="Save Position"
        Style="margin-left: 12px;"
        CssClass="btn btn-default btn-sm" />

    <asp:Button ID="ResetPositionButton"
        runat="server"
        Text="Reset Position"
        CssClass="btn btn-default btn-sm" />

    <asp:Button ID="GoToWidgetManagerButton"
        runat="server"
        Text="Go to Widget Manager (Todo: Admin Only)"
        CssClass="btn btn-default btn-sm" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceholder" runat="server">
</asp:Content>