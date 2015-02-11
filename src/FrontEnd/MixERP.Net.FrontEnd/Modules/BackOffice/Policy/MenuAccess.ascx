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

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuAccess.ascx.cs" Inherits="MixERP.Net.Core.Modules.BackOffice.Policy.MenuAccess" %>

<asp:PlaceHolder runat="server" ID="Placeholder1"></asp:PlaceHolder>

<script type="text/javascript">
    $(function() {
        var grid = $("#MenuAccessGridView");
        var checkAllButton = $("#CheckAllButton");
        var uncheckAllButton = $("#UncheckAllButton");


        grid.find("input").removeAttr("disabled");

        checkAllButton.click(function() {
            grid.find("input").prop("checked", true);
        });

        uncheckAllButton.click(function() {
            grid.find("input").prop("checked", false);
        });

        grid.find("tr").click(function() {
            var el = $(this).find("input");
            el.prop("checked", !el.prop("checked"));
        });
    });

    function updateSelection() {
        var selectedMenusHidden = $("#SelectedMenusHidden");
        var selectedElements = $("input:checked");
        var items = [];

        selectedElements.each(function() {
            var menuId = $(this).closest("tr").find("td:nth-child(3)").html();
            items.push(menuId);
        });

        selectedMenusHidden.val(items.join(","));
    };

</script>