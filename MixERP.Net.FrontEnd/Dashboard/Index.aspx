<%-- 
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/MenuMaster.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MixERP.Net.FrontEnd.Dashboard.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceholder" runat="server">
    <link href="<%=this.ResolveUrl("~/Scripts/gridster/jquery.gridster.min.css") %>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">

    <div style="width: 1092px; margin: auto;">
        <h1 style="margin-left: 12px;">Binod, Welcome to MixERP Dashboard (Todo Page)</h1>

        <asp:PlaceHolder ID="WidgetPlaceholder" runat="server" />

        <div class="vpad16">
            <asp:Button ID="SavePositionButton"
                runat="server"
                Text="Save Position"
                Style="margin-left: 12px;"
                CssClass="button" />

            <asp:Button ID="ResetPositionButton"
                runat="server"
                Text="Reset Position"
                CssClass="button" />

            <asp:Button ID="GoToWidgetManagerButton"
                runat="server"
                Text="Go to Widget Manager (Todo: Admin Only)"
                CssClass="button" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceholder" runat="server">
    <script type="text/javascript">
        var gridster;

        $(function () {
            gridster = $(".gridster > ul").gridster({
                widget_margins: [10, 10],
                widget_base_dimensions: [116, 122],
                min_cols: 2
            }).data('gridster');
        });
    </script>
    <style type="text/css">
        ul, ol {
            list-style: none;
        }
    </style>
</asp:Content>
