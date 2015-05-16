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

var accountNumberSelect = $("#AccountNumberSelect");
var accountNumberInputText = $("#AccountNumberInputText");
var statementGridView = $("#StatementGridView");
var printIcon = $(".print.icon");
var printButton = $("#PrintButton");
var fromDateTextBox = $("#FromDateTextBox");
var toDateTextBox = $("#ToDateTextBox");

printIcon.click(function() {
    var selected = $(this).parent().parent().find("td:nth-child(3)").html().trim();
    if (!isNullOrWhiteSpace(selected)) {
        var report = "/Modules/Finance/Reports/GLAdviceReport.mix?TranCode=" + selected;
        showWindow(report);
    };
});

function getSelectedItems() {
    var checkBoxColumnPosition = "2";
    var idColumnPosition = "3";

    var selection = getSelectedCheckBoxItemIds(checkBoxColumnPosition, idColumnPosition, statementGridView);

    if (selection.length > 0) {
        $("#SelectedValuesHidden").val(selection.join(','));
        return true;
    } else {
        $.notify(nothingSelectedLocalized, "error");
        return false;
    }
};

$(document).ready(function() {
    loadAccounts();
    createCascadingPair(accountNumberSelect, accountNumberInputText);
    createFlaggedRows(statementGridView);
});

function loadAccounts() {
    var selected = accountNumberInputText.val();

    url = "/Modules/Finance/Services/AccountData.asmx/ListAccounts";
    ajaxDataBind(url, accountNumberSelect, null, selected, accountNumberInputText);
};

statementGridView.find('tr').click(function() {
    var checkBox = $(this).find('td input:checkbox');
    toogleSelection(checkBox);
});

printButton.click(function() {
    var report = "AccountStatementReport.mix?AccountNumber={0}&From={1}&To={2}";
    var accountNumber = accountNumberInputText.val();
    var from = Date.parseExact(fromDateTextBox.val(), window.shortDateFormat).toDateString();
    var to = Date.parseExact(toDateTextBox.val(), window.shortDateFormat).toDateString();

    report = String.format(report, accountNumber, from, to);
    showWindow(report).toISOString();
});

function reconcile() {
    var checkBoxColumnPosition = "2";
    var dateColumnPosition = "5";
    var tranCodeColumnPosition = "3";

    var selection = getSelectedCheckBoxItemIds(checkBoxColumnPosition, dateColumnPosition, statementGridView, 1)[0];
    var tranCode = getSelectedCheckBoxItemIds(checkBoxColumnPosition, tranCodeColumnPosition, statementGridView, 1)[0];

    var parsedDate = Date.parseExact(selection, window.shortDateFormat);


    if (parsedDate) {
        var year = parsedDate.getFullYear();
        var month = parsedDate.getMonth() + 1;
        var day = parsedDate.getDate();

        $("#CurrentBookDateInputText").val(selection);

        $("#YearInputText").val(year);
        $("#MonthInputText").val(month);
        $("#DayInputText").val(day);

        $("#TranCodeInputText").val(tranCode);

        $("#ReconcileModal").modal("show");
    };

    return false;//Prevent postback
};
