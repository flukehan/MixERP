/********************************************************************************
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
***********************************************************************************/

/*jshint -W032 */

var itemSelect = $("#ItemSelect");
var storeSelect = $("#StoreSelect");
var itemCodeInputText = $("#ItemCodeInputText");
var statementGridView = $("#StatementGridView");
var printIcon = $(".print.icon");
var printButton = $("#PrintButton");
var fromDateTextBox = $("#FromDateTextBox");
var toDateTextBox = $("#ToDateTextBox");
var storeIdHidden = $("#StoreIdHidden");

printIcon.click(function () {
    var selected = $(this).parent().parent().find("td:nth-child(3)").html().trim();
    if (!isNullOrWhiteSpace(selected)) {
        var report = "/Modules/Inventory/Reports/InventoryAdvice.mix?TranCode=" + selected;
        showWindow(report);
    };
});


storeSelect.blur(function () {
    storeIdHidden.val(storeSelect.getSelectedValue());
});

function getSelectedItems() {
    var checkBoxColumnPosition = "2";
    var idColumnPosition = "3";

    var selection = getSelectedCheckBoxItemIds(checkBoxColumnPosition, idColumnPosition, statementGridView);

    if (selection.length > 0) {
        $("#SelectedValuesHidden").val(selection.join(','));
        return true;
    } else {
        $.notify(Resources.Titles.NothingSelected(), "error");
        return false;
    }
};

$(document).ready(function () {
    loadItems();
    loadStores();
    createCascadingPair(itemSelect, itemCodeInputText);
    createFlaggedRows(statementGridView);
});

function loadItems() {
    var selected = itemCodeInputText.val();
    var data = appendParameter("", "tranBook", "Statement");
    data = getData(data);

    url = "/Modules/Inventory/Services/ItemData.asmx/GetItems";
    ajaxDataBind(url, itemSelect, data, selected, itemCodeInputText, null, "ItemCode", "ItemName");
};


function loadStores() {
    if (storeSelect.length) {
        url = "/Modules/Inventory/Services/ItemData.asmx/GetStores";
        ajaxDataBind(url, storeSelect, null, storeIdHidden.val());
    };
};


statementGridView.find('tr').click(function () {
    var checkBox = $(this).find('td input:checkbox');
    toogleSelection(checkBox);
});

printButton.click(function () {
    var report = "AccountStatementReport.mix?ItemCode={0}&StoreId={1}&From={2}&To={3}";
    var itemCode = itemCodeInputText.val();
    var storeId = storeIdHidden.val();
    var from = Date.parseExact(fromDateTextBox.val(), window.shortDateFormat).toDateString();
    var to = Date.parseExact(toDateTextBox.val(), window.shortDateFormat).toDateString();


    if (isNullOrWhiteSpace(itemCode)) {
        return;
    };

    if (isNullOrWhiteSpace(storeId)) {
        return;
    };

    report = String.format(report, itemCode, storeId, from, to);
    showWindow(report);
});