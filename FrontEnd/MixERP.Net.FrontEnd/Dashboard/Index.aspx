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

<%@ Page Title="" Language="C#" MasterPageFile="~/MenuMaster.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MixERP.Net.FrontEnd.Dashboard.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">

    <div style="max-width: 1092px; margin: auto;">
        <h1 style="margin: 12px;">Binod, Welcome to MixERP Dashboard (Todo Page)</h1>

        <div id="sortable-container">
            <asp:PlaceHolder ID="WidgetPlaceholder" runat="server" />
        </div>

        <div style="clear: both;"></div>
        <br />

        <div class="vpad16">
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
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceholder" runat="server">
    <script type="text/javascript">

        $('#sortable-container').sortable({ placeholder: "ui-state-highlight", helper: 'clone', handle: 'h3' });

        $(document).ready(function () {
            var height = 0;

            $('#sortable-container').each(function () {

                var items = $(this).find(".sortable-item");

                items.each(function () {
                    if ($(this).height() > height) {
                        height = $(this).height();
                    }
                });

                var margin = 0;

                items.each(function () {
                    margin = $(this).find(".panel-heading").height();
                    $(this).find(".panel-body").css("height", height - margin + "px");
                });
            });

        });
    </script>
</asp:Content>