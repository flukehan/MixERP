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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Adjustment.ascx.cs" Inherits="MixERP.Net.Core.Modules.Inventory.Adjustment" %>

<asp:PlaceHolder runat="server" ID="Placeholder1"></asp:PlaceHolder>
<script type="text/javascript">
    var showButotn = $("#ShowButton");
    var storeHidden = $("#StoreHidden");
    var storeSelect = $("#StoreSelect");
    var grid = $("#grid");
    var actualInputText = $(".actual");
    var differenceInputText = $(".difference");

    function ShowButton_ClientClick() {
        var selected = storeSelect.getSelectedValue();
        storeHidden.val(selected);

        if (!parseInt2(selected)) {
            return false;
        };

        return true;
    };

    $(document).ready(function () {
        loadStores();
        createControls();

        var actualInputText = $(".actual");
        var differenceInputText = $(".difference");

        actualInputText.number(true, 0, decimalSeparator, thousandSeparator);
        differenceInputText.number(true, 0, decimalSeparator, thousandSeparator);

        actualInputText.blur(function () {
            var quantity = parseInt2($(this).parent().parent().find("td:nth-last-child(3)").html());
            var actual = parseInt2($(this).val());

            if (actual > quantity) {
                makeDirty($(this).parent());
                return;
            };

            removeDirty($(this).parent());

            $(this).parent().parent().find("td:nth-last-child(1) input").val(quantity - actual);
        });

        differenceInputText.blur(function () {
            var quantity = parseInt2($(this).parent().parent().find("td:nth-last-child(3)").html());
            var difference = parseInt2($(this).val());

            if (difference > quantity) {
                makeDirty($(this).parent());
                return;
            };

            removeDirty($(this).parent());

            $(this).parent().parent().find("td:nth-last-child(2) input").val(quantity - difference);
        });

    });

    function createControls() {
        var actual = "<input type='text' tabindex='{0}' class='actual' />";
        var difference = "<input type='text' tabindex='{0}' class='difference' />";

        var rows = grid.find("tbody tr");
        rows.each(function () {

            $(this).find("td:nth-last-child(2)").html(String.format(actual, $(this).index() + 1));
            $(this).find("td:nth-last-child(1)").html(String.format(difference, $(this).index() + rows.length + 1));
        });
    };

    function loadStores() {
        if (storeSelect.length) {
            url = "/Modules/Inventory/Services/ItemData.asmx/GetStores";
            ajaxDataBind(url, storeSelect, null, storeHidden.val());
        };
    };


</script>
