<%-- 
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/ContentMaster.Master" AutoEventWireup="true" CodeBehind="Parties.aspx.cs" Inherits="MixERP.Net.FrontEnd.Inventory.Setup.Parties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContentPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSheetContentPlaceholder" runat="server">
    <style type="text/css">
        #menu-table {
            width: 100%;
        }

        .content, iframe
        {
            height:100%;
            width:100%;
        }

        iframe
        {
            border:none;
            width:100%;
        }

        .container
        {
            background-color: white;
        }

    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <iframe src="PartiesPopup.aspx"></iframe>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScriptContentPlaceholder" runat="server">
</asp:Content>