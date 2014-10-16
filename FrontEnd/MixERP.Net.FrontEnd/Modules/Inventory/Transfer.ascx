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
    var errorLabel = $("#ErrorLabel");
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
    var valueDateTextBox = $("#ValueDateTextBox");
    var url = "";
    var data = "";

    $(document).ready(function () {
        loadStores();
        loadItems();
    });

    addButton.click(function () {
        addRow();
    });

    var addRow = function () {
        itemCodeInputText.val(itemSelect.getSelectedValue());

        var tranType = transactionTypeSelect.getSelectedValue();
        var storeName = storeSelect.getSelectedText();
        var itemCode = itemCodeInputText.val();
        var itemName = itemSelect.getSelectedText();
        var unitName = unitSelect.getSelectedText();
        var quantity = parseInt2(quantityInputText.val());

        if (isNullOrWhiteSpace(tranType) || tranType === selectLocalized) {
            makeDirty(transactionTypeSelect);
            return;
        };

        if (isNullOrWhiteSpace(storeName) || storeName === selectLocalized) {
            makeDirty(storeSelect);
            return;
        };

        if (isNullOrWhiteSpace(itemCode)) {
            makeDirty(itemCodeInputText);
            return;
        };

        if (isNullOrWhiteSpace(itemName) || itemName === selectLocalized) {
            makeDirty(itemSelect);
            return;
        };

        if (isNullOrWhiteSpace(unitName) || unitName === selectLocalized) {
            makeDirty(unitSelect);
            return;
        };

        if (quantity <= 0) {
            makeDirty(quantityInputText);
            return;
        };

        removeDirty(transactionTypeSelect);
        removeDirty(storeSelect);
        removeDirty(itemCodeInputText);
        removeDirty(itemSelect);
        removeDirty(unitSelect);
        removeDirty(quantityInputText);

        appendToTable(tranType, storeName, itemCode, itemName, unitName, quantity);
        itemCodeInputText.val("");
        quantityInputText.val("");
        transactionTypeSelect.focus();
    };

    function appendToTable(tranType, storeName, itemCode, itemName, unitName, quantity) {
        var rows = transferGridView.find("tr:not(:first-child):not(:last-child)");
        var match = false;

        rows.each(function () {
            var row = $(this);
            if (getColumnText(row, 0) !== tranType &&
                getColumnText(row, 1) === storeName &&
                getColumnText(row, 2) === itemCode) {
                $.notify(duplicateEntryLocalized);

                makeDirty(itemSelect);
                match = true;
            };

            if (getColumnText(row, 0) === tranType &&
                getColumnText(row, 1) === storeName &&
                getColumnText(row, 2) === itemCode &&
                getColumnText(row, 4) === unitName) {

                setColumnText(row, 5, parseFloat2(getColumnText(row, 5)) + quantity);

                addDanger(row);
                match = true;
                return;
            }

        });

        if (!match) {
            var html = "<tr class='grid2-row'><td>" + tranType + "</td><td>" + storeName + "</td><td>" + itemCode + "</td><td>" + itemName + "</td><td>" + unitName + "</td><td class='text-right'>" + quantity + "</td>"
                + "</td><td><span class='glyphicon glyphicon-remove-circle pointer span-icon' onclick='removeRow($(this));'></span><span class='glyphicon glyphicon-ok-sign pointer span-icon' onclick='toggleDanger($(this));'></span><span class='glyphicon glyphicon glyphicon-thumbs-up pointer span-icon' onclick='toggleSuccess($(this));'></span></td></tr>";
            transferGridView.find("tr:last").before(html);
        }
    };

    saveButton.click(function () {
        errorLabel.html("");

        if (transferGridView.find("tr").length === 2) {
            errorLabel.html(gridViewEmptyWarningLocalized);
            return;
        };

        var valueDate = valueDateTextBox.val();
        var referenceNumber = referenceNumberInputText.val();
        var statementReference = statementReferenceTextArea.val();
        var tableData = tableToJSON(transferGridView);

        var ajaxSaveTransfer = SaveTransfer(valueDate, referenceNumber, statementReference, tableData);

        ajaxSaveTransfer.success(function (msg) {
            alert(msg.d);
        });

        ajaxSaveTransfer.fail(function (xhr) {
            logAjaxErrorMessage(xhr);
        });

    });

    function SaveTransfer(valueDate, referenceNumber, statementReference, tableData) {
        url = "/Modules/Inventory/Services/Entry/Transfer.asmx/Save";

        data = appendParameter("", "valueDate", valueDate);
        data = appendParameter(data, "referenceNumber", referenceNumber);
        data = appendParameter(data, "statementReference", statementReference);
        data = appendParameter(data, "data", tableData);
        data = getData(data);

        return getAjax(url, data);
    };

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
