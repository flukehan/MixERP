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
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Transfer.ascx.cs" Inherits="MixERP.Net.Core.Modules.Inventory.Transfer" %>

<style type="text/css">
    #TransferGridView th:nth-child(1) {
        width: 80px;
    }

    #TransferGridView th:nth-child(2) {
        width: 140px;
    }

    #TransferGridView th:nth-child(3) {
        width: 90px;
    }

    #TransferGridView th:nth-child(4) {
    }

    #TransferGridView th:nth-child(5) {
        width: 120px;
    }

    #TransferGridView th:nth-child(6) {
        width: 120px;
    }

    #TransferGridView th:nth-child(7) {
        width: 120px;
    }
</style>

<asp:PlaceHolder runat="server" ID="Placeholder1"></asp:PlaceHolder>
<input type="hidden" id="ItemIdHidden" />

<script type="text/javascript">
    var addButton = $("#AddButton");
    var dateTextBox = $("#DateTextBox");
    var itemIdHidden = $("#ItemIdHidden");
    var itemCodeInputText = $("#ItemCodeInputText");
    var itemSelect = $("#ItemSelect");
    var quantityInputText = $("#QuantityInputText");
    var referenceNumberInputText = $("#ReferenceNumberInputText");
    var saveButton = $("#SaveButton");
    var statementReferenceTextArea = $("#StatementReferenceTextArea");
    var storeSelect = $("#StoreSelect");
    var transactionTypeSelect = $("#TransactionTypeSelect");
    var transferGridView = $("#TransferGridView");
    var unitSelect = $("#UnitSelect");

    $(document).ready(function () {
        loadStores();
        loadItems();
    });

    addButton.click(function () {
        alert("Add");
    });

    itemSelect.change(function () {
        itemCodeInputText.val(itemSelect.getSelectedValue());
        loadUnits();
    });

    itemSelect.blur(function () {
        itemCodeInputText.val(itemSelect.getSelectedValue());
        loadUnits();
    });

    itemCodeInputText.blur(function () {
        itemSelect.val(itemCodeInputText.val());
    });

    //Ajax Data-binding
    function loadStores() {
        if (storeSelect.length) {
            url = "/Modules/Inventory/Services/ItemData.asmx/GetStores";
            ajaxDataBind(url, storeSelect);
        };
    };

    function loadItems() {
        url = "/Modules/Inventory/Services/ItemData.asmx/GetItems";
        data = appendParameter("", "tranBook", "Transfer");
        data = getData(data);

        ajaxDataBind(url, itemSelect, data);
    };

    function loadUnits() {
        var itemCode = itemCodeInputText.val();

        url = "/Modules/Inventory/Services/ItemData.asmx/GetUnits";
        data = appendParameter("", "itemCode", itemCode);
        data = getData(data);

        ajaxDataBind(url, unitSelect, data);
    };

    function ajaxDataBindCallBack(targetControl) {
        if (targetControl.is(itemSelect)) {
            var itemId = parseFloat2(itemIdHidden.val());

            itemIdHidden.val("");

            if (itemId > 0) {
                var targetControls = $([]);
                targetControls.push(itemCodeInputText);
                targetControls.push(itemSelect);

                url = "/Modules/Inventory/Services/ItemData.asmx/GetItemCodeByItemId";
                data = appendParameter("", "itemId", itemId);
                data = getData(data);

                ajaxUpdateVal(url, data, targetControls);
            }

        };
    };

    shortcut.add("F4", function () {
        url = "/Modules/Inventory/Setup/ItemsPopup.mix?modal=1&CallBackFunctionName=loadItems&AssociatedControlId=ItemIdHidden";
        showWindow(url);
    });

    shortcut.add("CTRL+ENTER", function () {
        addButton.trigger('click');
    });
</script>
