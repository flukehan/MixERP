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

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductControl.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.Products.ProductControl" %>
<%@ Import Namespace="MixERP.Net.Common.Helpers" %>

<style type="text/css">
    table input[type=radio] {
        margin: 4px;
    }

    #ProductGridView th:nth-child(1) {
        width: 7%;
    }

    #ProductGridView th:nth-child(2) {
        width: 21%;
    }

    #ProductGridView th:nth-child(3) {
        width: 5%;
    }

    #ProductGridView th:nth-child(4) {
        width: 10%;
    }

    #ProductGridView th:nth-child(5) {
        width: 10%;
    }

    #ProductGridView th:nth-child(6) {
        width: 10%;
    }

    #ProductGridView th:nth-child(7) {
        width: 6%;
    }

    #ProductGridView th:nth-child(8) {
        width: 7%;
    }

    #ProductGridView th:nth-child(9) {
        width: 7%;
    }

    #ProductGridView th:nth-child(10) {
        width: 10%;
    }

    #ProductGridView th:nth-child(11) {
        width: 7%;
    }

    table.form {
        width: 100%;
    }

        table.form td:nth-child(1), table.form td:nth-child(2), table.form td:nth-child(3), table.form td:nth-child(6) {
            width: 10%;
        }

        table.form td:nth-child(5) {
            max-width: 10%;
        }

        table.form td:nth-child(4) {
            width: 24%;
        }

        table.form .ui.toggle.checkbox {
            width: 150px;
        }
</style>

<asp:PlaceHolder runat="server" ID="Placeholder1"></asp:PlaceHolder>


<script src="/Scripts/UserControls/ProductControl.js"></script>