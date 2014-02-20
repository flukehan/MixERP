<%-- 
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemSelector.aspx.cs" Inherits="MixERP.Net.FrontEnd.General.ItemSelector" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Scrud Item Selector</title>
    <link href="/Themes/purple/stylesheets/main.css" rel="stylesheet" />
    <script src="//code.jquery.com/jquery-1.9.1.js" type="text/javascript"></script>
    <style type="text/css">
        html, body, form {
            height: 100%;
        }

        form {
            background-color: white !important;
            padding: 12px;
        }

        .grid td, .grid th {
            white-space: nowrap;
        }

        .filter {
            width: 172px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <mixerp:ScrudItemSelector runat="server"
            TopPanelCssClass="vpad8"
            TopPanelTableCssClass="valignmiddle"
            FilterDropDownListCssClass="filter"
            FilterTextBoxCssClass=""
            ButtonCssClass="button"
            GridViewCssClass="grid"
            GridViewPagerCssClass="gridpager"
            GridViewRowCssClass="row"
            GridViewAlternateRowCssClass="alt" />
    </form>
</body>
</html>
