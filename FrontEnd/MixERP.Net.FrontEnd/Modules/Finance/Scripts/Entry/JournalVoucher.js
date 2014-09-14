/*jshint -W032 */
/*global shortcut, parseFloat2*/

//Controls
var addButton = $("#AddButton");
var accountCodeTextBox = $("#AccountCodeTextBox");
var accountDropDownList = $("#AccountDropDownList");
var attachmentDiv = $("#AttachmentDiv");
var attachmentLabel = $("#AttachmentLabel");

var cashRepositoryDropDownList = $("#CashRepositoryDropDownList");
var costCenterDropDownList = $("#CostCenterDropDownList");
var creditTextBox = $("#CreditTextBox");

var creditTotalTextBox = $("#CreditTotalTextBox");
var currencyDropDownList = $("#CurrencyDropDownList");

var debitTextBox = $("#DebitTextBox");
var debitTotalTextBox = $("#DebitTotalTextBox");

var errorLabelBottom = $("#ErrorLabelBottom");
var erTextBox = $("#ERTextBox");

var lcCreditTextBox = $("#LCCreditTextBox");
var lcDebitTextBox = $("#LCDebitTextBox");

var postButton = $("#PostButton");

var referenceNumberTextBox = $("#ReferenceNumberTextBox");

var statementReferenceTextBox = $("#StatementReferenceTextBox");

var transactionGridView = $("#TransactionGridView");
var transactionGridViewHidden = $("#TransactionGridViewHidden");

var valueDateTextBox = $("#ValueDateTextBox");

//Variables
var accountCode = "";
var account = "";
var attachments;

var cashRepositoryCode = "";

var credit = 0;
var currencyCode = '';

var debit = 0;

var lcCredit = 0;
var lcDebit = 0;
var er = 0.00;

var url = "";
var data = "";
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
    ajaxDataBind(url, accountDropDownList);
};

function loadCashRepositories() {
    accountCode = accountDropDownList.getSelectedValue();

    url = "/Modules/Finance/Services/AccountData.asmx/GetCashRepositoriesByAccountCode";
    data = appendParameter("", "accountCode", accountCode);
    data = getData(data);

    var repoAjax = getAjax(url, data);

    repoAjax.success(function (msg) {
        $.when(cashRepositoryDropDownList.bindAjaxData(msg.d)).done(function () {
            if (cashRepositoryDropDownList.children('option').length == 1) {
                loadCurrenciesByAccountCode(accountDropDownList.getSelectedValue());
                return;
            };

            loadCurrencies();
        });
    });

    repoAjax.error(function (xhr) {
        var err = $.parseJSON(xhr.responseText);
        appendItem(cashRepositoryDropDownList, 0, err.Message);
    });
};

function loadCostCenters() {
    url = "/Modules/Finance/Services/AccountData.asmx/GetCostCenters";
    ajaxDataBind(url, costCenterDropDownList);
};

function loadCurrencies() {
    url = "/Modules/Finance/Services/AccountData.asmx/GetCurrencies";
    ajaxDataBind(url, currencyDropDownList);
};

function loadCurrenciesByAccountCode(accountCode) {
    url = "/Modules/Finance/Services/AccountData.asmx/GetCurrenciesByAccountCode";
    data = appendParameter("", "accountCode", accountCode);
    data = getData(data);

    ajaxDataBind(url, currencyDropDownList, data);
};

//Control Events
accountDropDownList.change(function () {
    accountCodeTextBox.val(accountDropDownList.getSelectedValue());
});

accountDropDownList.blur(function () {
    loadCashRepositories();
});

addButton.click(function () {
    statementReference = statementReferenceTextBox.val();
    accountCode = accountCodeTextBox.val();
    account = accountDropDownList.getSelectedText();
    cashRepositoryCode = cashRepositoryDropDownList.getSelectedValue();

    currencyCode = currencyDropDownList.getSelectedValue();

    debit = parseFloat2(debitTextBox.val());
    credit = parseFloat2(creditTextBox.val());

    er = parseFloat2(erTextBox.val());

    lcDebit = parseFloat2(lcDebitTextBox.val());
    lcCredit = parseFloat2(lcCreditTextBox.val());

    if (isNullOrWhiteSpace(statementReference)) {
        makeDirty(statementReferenceTextBox);
        return;
    };

    removeDirty(statementReferenceTextBox);

    if (isNullOrWhiteSpace(accountCodeTextBox.val())) {
        makeDirty(accountCodeTextBox);
        return;
    };

    removeDirty(accountCodeTextBox);

    if (isNullOrWhiteSpace(accountDropDownList.getSelectedText())) {
        makeDirty(accountDropDownList);
        return;
    };

    removeDirty(accountDropDownList);

    if ((debit > 0 && credit > 0) || (debit == 0 && credit == 0)) {
        makeDirty(debitTextBox);
        makeDirty(creditTextBox);
        return;
    };

    if ((lcDebit > 0 && lcCredit > 0) || (lcDebit == 0 && lcCredit == 0)) {
        makeDirty(lcDebitTextBox);
        makeDirty(lcCreditTextBox);
        return;
    };

    if (lcDebit < 0) {
        makeDirty(lcDebitTextBox);
        return;
    };

    if (lcCredit < 0) {
        makeDirty(lcCreditTextBox);
        return;
    };

    removeDirty(debitTextBox);
    removeDirty(creditTextBox);
    removeDirty(lcDebitTextBox);
    removeDirty(lcCreditTextBox);
    removeDirty(cashRepositoryDropDownList);

    if (cashRepositoryDropDownList.find("option").size() > 1 && isNullOrWhiteSpace(cashRepositoryDropDownList.getSelectedValue())) {
        $.notify("Invalid cash repository specified.");
        return;
    }

    var ajaxAccountCodeExists = accountCodeExists(accountCode);
    var ajaxIsCash = isCash(accountCode);
    var ajaxCashRepositoryCodeExists = cashRepositoryCodeExists(cashRepositoryCode);

    var ajaxHasBalance;

    ajaxAccountCodeExists.error(function (xhr) {
        logError(getAjaxErrorMessage(xhr));
    });

    ajaxCashRepositoryCodeExists.fail(function (xhr) {
        logError(getAjaxErrorMessage(xhr));
    });

    ajaxIsCash.fail(function (xhr) {
        logError(getAjaxErrorMessage(xhr));
    });

    ajaxAccountCodeExists.success(function (ajaxAccountCodeExistsResult) {
        var accountCodeExists = ajaxAccountCodeExistsResult.d;

        if (!accountCodeExists) {
            $.notify(String.format("Account code '{0}' does not exist.", accountCode), "error");
            makeDirty(accountCodeTextBox);
            return;
        };

        ajaxIsCash.success(function (ajaxIsCashResult) {
            var isCash = ajaxIsCashResult.d;

            if (!isCash && !isNullOrWhiteSpace(cashRepositoryCode)) {
                $.notify("Invalid cash repository specified.");
                makeDirty(cashRepositoryDropDownList);
                return;
            };

            if (!isCash) {
                addRow(statementReference, accountCode, account, "", debit, credit, er, lcDebit, lcCredit);
                return;
            };

            if (isNullOrWhiteSpace(cashRepositoryCode)) {
                makeDirty(cashRepositoryDropDownList);
                $.notify("Invalid cash repository specified.", "error");
                return;
            };

            ajaxCashRepositoryCodeExists.success(function (ajaxCashRepositoryCodeExistsResult) {
                var cashRepositoryCodeExists = ajaxCashRepositoryCodeExistsResult.d;

                if (!cashRepositoryCodeExists) {
                    $.notify(String.format("Cash repository '{0}' does not exist.", cashRepositoryCode), "error");
                    makeDirty(cashRepositoryDropDownList);
                    return;
                };

                if (debit > 0) {
                    addRow(statementReference, accountCode, account, cashRepositoryCode, debit, credit, er, lcDebit, lcCredit, true);
                    return;
                };

                if (credit > 0 && isCash) {
                    ajaxHasBalance = hasBalance(cashRepositoryCode, currencyCode, credit);
                    ajaxHasBalance.fail(function (xhr) {
                        logError(getAjaxErrorMessage(xhr));
                    });

                    ajaxHasBalance.success(function (hasBalanceResult) {
                        var hasBalance = hasBalanceResult.d;

                        if (!hasBalance) {
                            $.notify(String.format("Not enough balance in the cash repository '{0}'.", cashRepositoryCode), "error");
                            makeDirty(cashRepositoryDropDownList);
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
            if (getColumnText(row, 1) == accountCode) {
                $.notify(duplicateEntryLocalized);
                makeDirty(itemCodeTextBox);
                return;
            }
        };

        if (getColumnText(row, 3) == cashRepository) {
            $.notify(duplicateEntryLocalized);
            makeDirty(itemCodeTextBox);
            return;
        }
    });

    var html = "<tr class='grid2-row'><td>" + statementReference + "</td><td>" + accountCode + "</td><td>" + account + "</td><td>" + cashRepository + "</td><td>" + currencyCode + "</td><td class='text-right'>" + debit + "</td><td class='text-right'>" + credit + "</td>"
            + "<td class='text-right'>" + er + "</td><td class='text-right'>" + lcDebit + "</td><td class='text-right'>" + lcCredit + "</td>"
            + "<td><span class='glyphicon glyphicon-remove-circle pointer span-icon' onclick='removeRow($(this));'></span><span class='glyphicon glyphicon-ok-sign pointer span-icon' onclick='toggleDanger($(this));'></span><span class='glyphicon glyphicon glyphicon-thumbs-up pointer span-icon' onclick='toggleSuccess($(this));'></span></td></tr>";
    grid.find("tr:last").before(html);

    summate();

    lcDebitTextBox.val("");
    lcCreditTextBox.val("");
    debitTextBox.val("");
    creditTextBox.val("");

    creditTextBox.prop("disabled", false);

    repaint();
    statementReferenceTextBox.focus();
};

attachmentLabel.on("click", function () {
    "use strict";
    attachmentDiv.toggle(500);
});

currencyDropDownList.blur(function () {
    var ajaxGetExchangeRate = getExchangeRate(currencyDropDownList.getSelectedValue());

    ajaxGetExchangeRate.done(function (msg) {
        erTextBox.val(msg.d);
    });

    ajaxGetExchangeRate.fail(function (xhr) {
        logError(getAjaxErrorMessage(xhr));
    });
});

debitTextBox.blur(function () {
    debit = parseFloat2(debitTextBox.val());

    if (debit > 0) {
        creditTextBox.prop("disabled", true);
        erTextBox.focus();
        return;
    };

    creditTextBox.prop("disabled", false);
});

debitTextBox.keyup(function () {
    er = parseFloat2(erTextBox.val());
    if (er > 0) {
        lcDebitTextBox.val(parseFloat2(debitTextBox.val() * er));
    };
});

creditTextBox.keyup(function () {
    er = parseFloat2(erTextBox.val());
    if (er > 0) {
        lcCreditTextBox.val(parseFloat2(creditTextBox.val() * er));
    };
});

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
        var errorMessage = JSON.parse(jqXHR.responseText).Message;
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
    removeDirty(referenceNumberTextBox);
    removeDirty(statementReferenceTextBox);
    removeDirty(accountCodeTextBox);
    removeDirty(accountDropDownList);
    removeDirty(cashRepositoryDropDownList);
    removeDirty(lcDebitTextBox);
    removeDirty(lcCreditTextBox);
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

    if (transactionGridView.find("tr").length == 2) {
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

        if (debit == 0 && credit == 0) {
            addDanger(row);
            return false;
    };

        return true;
    }) == false) {
        return false;
    };

    summate();

    if (parseFloat2(debitTotalTextBox) != parseFloat2(creditTotalTextBox)) {
        $.notify("Referencing sides are not equal.", "error");
        return false;
    };

    referenceNumber = referenceNumberTextBox.getSelectedValue();
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