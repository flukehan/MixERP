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
/*global addDanger, ajaxDataBind, appendItem, appendParameter, duplicateEntryLocalized, getAjax, getAjaxErrorMessage, getColumnText, getData, gridViewEmptyWarningLocalized, invalidCostCenterWarningLocalized, invalidDateWarningLocalized, isDate, isNullOrWhiteSpace, logError, makeDirty, removeDirty, repaint, sumOfColumn, tableToJSON, uploadedFilesHidden, shortcut, parseFloat2 */

if (typeof invalidCostCenterWarningLocalized == "undefined") {
    invalidCostCenterWarningLocalized = "Invalid cost center.";
};

//Controls
var addInputButton = $("#AddInputButton");
var accountCodeInputText = $("#AccountCodeInputText");
var accountSelect = $("#AccountSelect");
var attachmentDiv = $("#AttachmentDiv");
var attachmentLabel = $("#AttachmentLabel");

var cashRepositorySelect = $("#CashRepositorySelect");
var costCenterDropDownList = $("#CostCenterDropDownList");
var creditInputText = $("#CreditInputText");

var creditTotalTextBox = $("#CreditTotalTextBox");
var currencySelect = $("#CurrencySelect");

var debitInputText = $("#DebitInputText");
var debitTotalTextBox = $("#DebitTotalTextBox");

var errorLabelBottom = $("#ErrorLabelBottom");
var erInputText = $("#ERInputText");

var lcCreditInputText = $("#LCCreditInputText");
var lcDebitInputText = $("#LCDebitInputText");

var postButton = $("#PostButton");

var referenceNumberInputText = $("#ReferenceNumberInputText");

var statementReferenceInputText = $("#StatementReferenceInputText");

var transactionGridView = $("#TransactionGridView");
var transactionGridViewHidden = $("#TransactionGridViewHidden");

var valueDateTextBox = $("#ValueDateTextBox");

//Variables
var accountCode = "";
var account = "";
var attachments;

var cashRepositoryCode = "";
var costCenterId = 0;
var credit = 0;
var currencyCode = '';

var debit = 0;

var lcCredit = 0;
var lcDebit = 0;
var er = 0.00;

var valueDate = "";

var url = "";
var data = "";
var referenceNumber = "";
var statementReference = "";

//Page Load Event
$(document).ready(function () {
    "use strict";
    addShortcuts();
    initializeAjaxData();
});

function initializeAjaxData() {
    loadAccounts();
    loadCostCenters();
};

function loadAccounts() {
    url = "/Modules/Finance/Services/AccountData.asmx/GetAccounts";
    ajaxDataBind(url, accountSelect);
};

function loadCashRepositories() {
    accountCode = accountSelect.getSelectedValue();

    url = "/Modules/Finance/Services/AccountData.asmx/GetCashRepositoriesByAccountCode";
    data = appendParameter("", "accountCode", accountCode);
    data = getData(data);

    var repoAjax = getAjax(url, data);

    repoAjax.success(function (msg) {
        $.when(cashRepositorySelect.bindAjaxData(msg.d)).done(function () {
            if (cashRepositorySelect.children('option').length === 1) {
                loadCurrenciesByAccountCode(accountSelect.getSelectedValue());
                return;
            };

            loadCurrencies();
        });
    });

    repoAjax.error(function (xhr) {
        var err = $.parseJSON(xhr.responseText);
        appendItem(cashRepositorySelect, 0, err.Message);
    });
};

function loadCostCenters() {
    url = "/Modules/Finance/Services/AccountData.asmx/GetCostCenters";
    ajaxDataBind(url, costCenterDropDownList);
};

function loadCurrencies() {
    url = "/Modules/Finance/Services/AccountData.asmx/GetCurrencies";
    ajaxDataBind(url, currencySelect);
};

function loadCurrenciesByAccountCode(accountCode) {
    url = "/Modules/Finance/Services/AccountData.asmx/GetCurrenciesByAccountCode";
    data = appendParameter("", "accountCode", accountCode);
    data = getData(data);

    ajaxDataBind(url, currencySelect, data);
};

//Control Events
accountSelect.change(function () {
    accountCodeInputText.val(accountSelect.getSelectedValue());
});

accountSelect.blur(function () {
    loadCashRepositories();
});

addInputButton.click(function () {
    statementReference = statementReferenceInputText.val();
    accountCode = accountCodeInputText.val();
    account = accountSelect.getSelectedText();
    cashRepositoryCode = cashRepositorySelect.getSelectedValue();

    currencyCode = currencySelect.getSelectedValue();

    debit = parseFloat2(debitInputText.val());
    credit = parseFloat2(creditInputText.val());

    er = parseFloat2(erInputText.val());

    lcDebit = parseFloat2(lcDebitInputText.val());
    lcCredit = parseFloat2(lcCreditInputText.val());

    if (isNullOrWhiteSpace(statementReference)) {
        makeDirty(statementReferenceInputText);
        return;
    };

    removeDirty(statementReferenceInputText);

    if (isNullOrWhiteSpace(accountCodeInputText.val())) {
        makeDirty(accountCodeInputText);
        return;
    };

    removeDirty(accountCodeInputText);

    if (isNullOrWhiteSpace(accountSelect.getSelectedText())) {
        makeDirty(accountSelect);
        return;
    };

    removeDirty(accountSelect);

    if ((debit > 0 && credit > 0) || (debit === 0 && credit === 0)) {
        makeDirty(debitInputText);
        makeDirty(creditInputText);
        return;
    };

    if ((lcDebit > 0 && lcCredit > 0) || (lcDebit === 0 && lcCredit === 0)) {
        makeDirty(lcDebitInputText);
        makeDirty(lcCreditInputText);
        return;
    };

    if (lcDebit < 0) {
        makeDirty(lcDebitInputText);
        return;
    };

    if (lcCredit < 0) {
        makeDirty(lcCreditInputText);
        return;
    };

    removeDirty(debitInputText);
    removeDirty(creditInputText);
    removeDirty(lcDebitInputText);
    removeDirty(lcCreditInputText);
    removeDirty(cashRepositorySelect);

    if (cashRepositorySelect.find("option").size() > 1 && isNullOrWhiteSpace(cashRepositorySelect.getSelectedValue())) {
        $.notify("Invalid cash repository specified.");
        return;
    }

    var ajaxAccountCodeExists = accountCodeExists(accountCode);
    var ajaxIsCash = isCash(accountCode);
    var ajaxCashRepositoryCodeExists = cashRepositoryCodeExists(cashRepositoryCode);

    var ajaxHasBalance;

    ajaxAccountCodeExists.error(function (xhr) {
        logAjaxErrorMessage(xhr);
    });

    ajaxCashRepositoryCodeExists.fail(function (xhr) {
        logAjaxErrorMessage(xhr);
    });

    ajaxIsCash.fail(function (xhr) {
        logAjaxErrorMessage(xhr);
    });

    ajaxAccountCodeExists.success(function (ajaxAccountCodeExistsResult) {
        var accountCodeExists = ajaxAccountCodeExistsResult.d;

        if (!accountCodeExists) {
            $.notify(String.format("Account code '{0}' does not exist.", accountCode), "error");
            makeDirty(accountCodeInputText);
            return;
        };

        ajaxIsCash.success(function (ajaxIsCashResult) {
            var isCash = ajaxIsCashResult.d;

            if (!isCash && !isNullOrWhiteSpace(cashRepositoryCode)) {
                $.notify("Invalid cash repository specified.");
                makeDirty(cashRepositorySelect);
                return;
            };

            if (!isCash) {
                addRow(statementReference, accountCode, account, "", debit, credit, er, lcDebit, lcCredit);
                return;
            };

            if (isNullOrWhiteSpace(cashRepositoryCode)) {
                makeDirty(cashRepositorySelect);
                $.notify("Invalid cash repository specified.", "error");
                return;
            };

            ajaxCashRepositoryCodeExists.success(function (ajaxCashRepositoryCodeExistsResult) {
                var cashRepositoryCodeExists = ajaxCashRepositoryCodeExistsResult.d;

                if (!cashRepositoryCodeExists) {
                    $.notify(String.format("Cash repository '{0}' does not exist.", cashRepositoryCode), "error");
                    makeDirty(cashRepositorySelect);
                    return;
                };

                if (debit > 0) {
                    addRow(statementReference, accountCode, account, cashRepositoryCode, debit, credit, er, lcDebit, lcCredit, true);
                    return;
                };

                if (credit > 0 && isCash) {
                    ajaxHasBalance = hasBalance(cashRepositoryCode, currencyCode, credit);
                    ajaxHasBalance.fail(function (xhr) {
                        logAjaxErrorMessage(xhr);
                    });

                    ajaxHasBalance.success(function (hasBalanceResult) {
                        var hasBalance = hasBalanceResult.d;

                        if (!hasBalance) {
                            $.notify(String.format("Not enough balance in the cash repository '{0}'.", cashRepositoryCode), "error");
                            makeDirty(cashRepositorySelect);
                            return;
                        };

                        addRow(statementReference, accountCode, account, cashRepositoryCode, debit, credit, er, lcDebit, lcCredit, true);
                    });
                };
            });
        });
    });
});

var addRow = function (statementReference, accountCode, account, cashRepository, debit, credit, er, lcDebit, lcCredit, isCash) {
    var grid = transactionGridView;
    var rows = grid.find("tr:not(:first-child):not(:last-child)");

    rows.each(function () {
        var row = $(this);

        if (!isCash) {
            if (getColumnText(row, 1) === accountCode) {
                $.notify(duplicateEntryLocalized);
                makeDirty(accountCodeInputText);
                return;
            }
        };

        if (getColumnText(row, 3) === cashRepository) {
            $.notify(duplicateEntryLocalized);
            makeDirty(accountCodeInputText);
            return;
        }
    });

    var html = "<tr class='grid2-row'><td>" + statementReference + "</td><td>" + accountCode + "</td><td>" + account + "</td><td>" + cashRepository + "</td><td>" + currencyCode + "</td><td class='text-right'>" + debit + "</td><td class='text-right'>" + credit + "</td>"
            + "<td class='text-right'>" + er + "</td><td class='text-right'>" + lcDebit + "</td><td class='text-right'>" + lcCredit + "</td>"
            + "<td><a class='pointer' onclick='removeRow($(this));'><i class='ui delete icon'></i></a><a class='pointer' onclick='toggleDanger($(this));'><i class='ui pointer check mark icon'></a></i><a class='pointer' onclick='toggleSuccess($(this));'><i class='ui pointer thumbs up icon'></i></a></td></tr>";
    grid.find("tr:last").before(html);

    summate();

    lcDebitInputText.val("");
    lcCreditInputText.val("");
    debitInputText.val("");
    creditInputText.val("");

    creditInputText.prop("disabled", false);

    repaint();
    statementReferenceInputText.focus();
};

attachmentLabel.on("click", function () {
    "use strict";
    attachmentDiv.toggle(500);
});

currencySelect.blur(function () {
    var ajaxGetExchangeRate = getExchangeRate(currencySelect.getSelectedValue());

    ajaxGetExchangeRate.done(function (msg) {
        erInputText.val(msg.d);
    });

    ajaxGetExchangeRate.fail(function (xhr) {
        logAjaxErrorMessage(xhr);
    });
});

debitInputText.blur(function () {
    debit = parseFloat2(debitInputText.val());

    if (debit > 0) {
        creditInputText.prop("disabled", true);
        erInputText.focus();
        return;
    };

    creditInputText.prop("disabled", false);
});

debitInputText.keyup(function () {
    UpdateLocalCurrencies();
});

creditInputText.keyup(function () {
    UpdateLocalCurrencies();
});

erInputText.keyup(function () {
    UpdateLocalCurrencies();
});

function UpdateLocalCurrencies() {
    er = parseFloat2(erInputText.val());

    if (er > 0) {
        lcDebitInputText.val(parseFloat2(debitInputText.val() * er));
        lcCreditInputText.val(parseFloat2(creditInputText.val() * er));
    };
};

postButton.click(function () {
    if (validate()) {
        post();
    };
});

var post = function () {
    var ajaxPostJournalTransaction = postpostJournalTransaction(valueDate, referenceNumber, data, costCenterId, attachments);

    ajaxPostJournalTransaction.done(function (response) {
        var id = response.d;
        window.location = "/Finance/Confirmation/JournalVoucher.aspx?TranId=" + id;
    });

    ajaxPostJournalTransaction.fail(function (jqXHR) {
        var errorMessage = getAjaxErrorMessage(jqXHR);
        errorLabelBottom.html(errorMessage);
        logError(errorMessage);
    });
};

var postpostJournalTransaction = function (valueDate, referenceNumber, data, costCenterId, attachments) {
    debugger;
    var d = "";
    d = appendParameter(d, "valueDate", valueDate);
    d = appendParameter(d, "referenceNumber", referenceNumber);
    d = appendParameter(d, "data", data);
    d = appendParameter(d, "costCenterId", costCenterId);
    d = appendParameter(d, "attachmentsJSON", attachments);
    d = getData(d);

    url = "/Services/Finance/JournalVoucher.asmx/Save";
    return getAjax(url, d);
};

var validate = function () {
    valueDate = valueDateTextBox.val();

    errorLabelBottom.html("");

    removeDirty(valueDateTextBox);
    removeDirty(referenceNumberInputText);
    removeDirty(statementReferenceInputText);
    removeDirty(accountCodeInputText);
    removeDirty(accountSelect);
    removeDirty(cashRepositorySelect);
    removeDirty(lcDebitInputText);
    removeDirty(lcCreditInputText);
    removeDirty(costCenterDropDownList);

    if (!isDate(valueDate)) {
        makeDirty(valueDateTextBox);
        errorLabelBottom.html(invalidDateWarningLocalized);
        return false;
    };

    if (parseFloat2(costCenterDropDownList.getSelectedValue()) <= 0) {
        makeDirty(costCenterDropDownList);
        errorLabelBottom.html(invalidCostCenterWarningLocalized);
        return false;
    };

    if (transactionGridView.find("tr").length === 2) {
        errorLabelBottom.html(gridViewEmptyWarningLocalized);
        return false;
    };

    var rows = transactionGridView.find("tr:not(:first-child):not(:last-child)");

    if (rows.each(function () {
        var row = $(this);

        debit = parseFloat2(getColumnText(row, 8));
        credit = parseFloat2(getColumnText(row, 9));

        if (debit > 0 && credit > 0) {
            addDanger(row);
            return false;
    };

        if (debit < 0 || credit < 0) {
            addDanger(row);
            return false;
    };

        if (debit === 0 && credit === 0) {
            addDanger(row);
            return false;
    };

        return true;
    }) === false) {
        return false;
    };

    summate();

    if (parseFloat2(debitTotalTextBox) !== parseFloat2(creditTotalTextBox)) {
        $.notify("Referencing sides are not equal.", "error");
        return false;
    };

    referenceNumber = referenceNumberInputText.getSelectedValue();
    transactionGridViewHidden.val(tableToJSON(transactionGridView));
    costCenterId = parseFloat2(costCenterDropDownList.getSelectedValue());

    data = transactionGridViewHidden.val();
    attachments = uploadedFilesHidden.val();

    return true;
};

//GridView Data Function

//GridView Manipulation

//Ajax Requests
function accountCodeExists(accountCode) {
    url = "/Modules/Finance/Services/AccountData.asmx/AccountCodeExists";
    data = appendParameter("", "accountCode", accountCode);
    data = getData(data);

    return getAjax(url, data);
};

function cashRepositoryCodeExists(cashRepositoryCode) {
    url = "/Modules/Finance/Services/AccountData.asmx/CashRepositoryCodeExists";
    data = appendParameter("", "cashRepositoryCode", cashRepositoryCode);
    data = getData(data);

    return getAjax(url, data);
};

function isCash(accountCode) {
    url = "/Modules/Finance/Services/AccountData.asmx/IsCashAccount";
    data = appendParameter("", "accountCode", accountCode);
    data = getData(data);

    return getAjax(url, data);
};

function getExchangeRate(currencyCode) {
    url = "/Modules/Finance/Services/JournalVoucher.asmx/GetExchangeRate";
    data = appendParameter("", "currencyCode", currencyCode);
    data = getData(data);

    return getAjax(url, data);
};

function hasBalance(cashRepositoryCode, currencyCode, credit) {
    url = "/Modules/Finance/Services/AccountData.asmx/HasBalance";
    data = appendParameter("", "cashRepositoryCode", cashRepositoryCode);
    data = appendParameter(data, "currencyCode", currencyCode);
    data = appendParameter(data, "credit", credit);
    data = getData(data);

    return getAjax(url, data);
};

//Boolean Validation

//Validation Helper Functions

//Logic & Validation
var summate = function () {
    var debitTotal = sumOfColumn("#" + transactionGridView.attr("id"), 8);
    var creditTotal = sumOfColumn("#" + transactionGridView.attr("id"), 9);

    debitTotalTextBox.val(debitTotal);
    creditTotalTextBox.val(creditTotal);
};

//Utilities
var addShortcuts = function () {
    "use strict";

    shortcut.add("CTRL+ALT+T", function () {
        $('#AccountCodeTextBox').focus();
    });

    shortcut.add("CTRL+ALT+A", function () {
        $('#AccountDropDownList').focus();
    });

    shortcut.add("CTRL+ALT+S", function () {
        $('#StatementReferenceTextBox').focus();
    });

    shortcut.add("CTRL+ALT+D", function () {
        $('#lcDebitTextBox').focus();
    });

    shortcut.add("CTRL+ALT+C", function () {
        $('#lcCreditTextBox').focus();
    });

    shortcut.add("CTRL+ENTER", function () {
        $('#AddButton').click();
    });
};