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

<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/BackendMaster.Master" AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="MixERP.Net.FrontEnd.Modules.Update" %>
<%@ Import Namespace="MixERP.Net.Updater.Api" %>
<%@ Import Namespace="MixERP.Net.Updater" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <div class="ui large pink header">An Update Is Available</div>
    <div class="ui divider"></div>

    <table class="ui compact table">
        <tbody>
        <tr>
            <td>Id</td>
            <td><%= this._release.Id %></td>
        </tr>
        <tr>
            <td>Version Name</td>
            <td><%= this._release.Name %></td>
        </tr>
        <tr>
            <td>Tag Name</td>
            <td><%= this._release.TagName %></td>
        </tr>
        <tr>
            <td>Created At</td>
            <td><%= this._release.CreatedAt %></td>
        </tr>
        <tr>
            <td>Published At</td>
            <td><%= this._release.PublishedAt %></td>
        </tr>
        <tr>
            <td>Body</td>
            <td><%= this._release.Body %></td>
        </tr>
        </tbody>
    </table>

    <asp:Button runat="server" ID="UpdateButton" Text="Update" CssClass="ui pink button"/>

    <script runat="server">
        private Release _release;

        protected async void Page_Init(object sender, EventArgs e)
        {
            UpdateManager updater = new UpdateManager();
            _release = await updater.GetLatestRelease();

            if (_release == null)
            {
                _release = new Release();
            }
            
            this.OverridePath = "/Dashboard/Index.aspx";
        }

    </script>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceholder" runat="server">
</asp:Content>