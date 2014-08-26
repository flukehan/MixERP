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

var debitTotalTextBox = $("#DebitTotalTextBox");
var debitTextBox = $("#DebitTextBox");

var postButton = $("#PostButton");

var referenceNumberTextBox = $("#ReferenceNumberTextBox");

var statementReferenceTextBox = $("#StatementReferenceTextBox");

var transactionGridView = $("#TransactionGridView");

var valueDateTextBox = $("#ValueDateTextBox");


//Variables
var accountCode = "";
var account = "";

var cashRepositoryCode = "";

var credit = 0;

var debit = 0;

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
    url = "/Services/AccountData.asmx/GetAccounts";
    var accountAjax = getAjax(url);

    accountAjax.success(function (msg) {
        accountDropDownList.bindAjaxData(msg.d);
    });

    accountAjax.error(function (xhr) {
        var err = $.parseJSON(xhr.responseText);
        appendItem(accountDropDownList, 0, err.Message);
    });
};

function loadCashRepositories() {
    accountCode = accountDropDownList.getSelectedValue();

    url = "/Services/AccountData.asmx/GetCashRepositoriesByAccountCode";
    data = appendParameter("", "accountCode", accountCode);
    data = getData(data);

    var repoAjax = getAjax(url, data);

    repoAjax.success(function (msg) {
        cashRepositoryDropDownList.bindAjaxData(msg.d);
    });

    repoAjax.error(function (xhr) {
        var err = $.parseJSON(xhr.responseText);
        appendItem(cashRepositoryDropDownList, 0, err.Message);
    });
};

function loadCostCenters() {
    url = "/Services/AccountData.asmx/GetCostCenters";
    var costCenterAjax = getAjax(url);

    costCenterAjax.success(function (msg) {
        costCenterDropDownList.bindAjaxData(msg.d);
    });

    costCenterAjax.error(function (xhr) {
        var err = $.parseJSON(xhr.responseText);
        appendItem(costCenterDropDownList, 0, err.Message);
    });
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
    debit = parseFloat2(debitTextBox.val());
    credit = parseFloat2(creditTextBox.val());

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

    if (debit < 0) {
        makeDirty(debitTextBox);
        return;
    };

    if (credit < 0) {
        makeDirty(creditTextBox);
        return;
    };

    removeDirty(debitTextBox);
    removeDirty(creditTextBox);

    if (cashRepositoryDropDownList.find("option").size() > 1 && isNullOrWhiteSpace(cashRepositoryDropDownList.getSelectedValue())) {
        $.notify("Invalid cash repository specified.");
        return;
    }


    var ajaxAccountCodeExists = accountCodeExists(accountCode);
    var ajaxIsCash = isCash(accountCode);
    var ajaxCashRepositoryCodeExists = cashRepositoryCodeExists(cashRepositoryCode);

    var ajaxHasBalance;

    if (credit > 0) {
        ajaxHasBalance = hasBalance(cashRepositoryCode, credit);
    };

    ajaxAccountCodeExists.error(function (xhr) {
        logError(getAjaxErrorMessage(xhr));
    });

    ajaxCashRepositoryCodeExists.fail(function (xhr) {
        logError(getAjaxErrorMessage(xhr));
    });

    ajaxIsCash.fail(function (xhr) {
        logError(getAjaxErrorMessage(xhr));
    });

    if (credit > 0) {
        ajaxHasBalance.fail(function (xhr) {
            logError(getAjaxErrorMessage(xhr));
        });
    };


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
                addRow(statementReference, accountCode, account, "", debit, credit);
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
                    addRow(statementReference, accountCode, account, cashRepositoryCode, debit, credit);
                    return;
                };

                ajaxHasBalance.success(function (hasBalanceResult) {
                    var hasBalance = hasBalanceResult.d;

                    if (!hasBalance) {
                        $.notify(String.format("Not enough balance in the cash repository '{0}'.", cashRepositoryCode), "error");
                        makeDirty(cashRepositoryDropDownList);
                        return;
                    };

                    addRow(statementReference, accountCode, account, cashRepositoryCode, debit, credit);
                });


            });

        });

    });
});


var addRow = function (statementReference, accountCode, account, cashRepository, debit, credit) {
    var grid = transactionGridView;
    var rows = grid.find("tr:not(:first-child):not(:last-child)");

    rows.each(function () {
        var row = $(this);

        if (getColumnText(row, 1) == accountCode) {
            $.notify("Duplicate entry.");
            makeDirty(itemCodeTextBox);
            return;
        }
    });


    var html = "<tr class='grid2-row'><td>" + statementReference + "</td><td>" + accountCode + "</td><td>" + account + "</td><td>" + cashRepository + "</td><td class='text-right'>" + debit + "</td><td class='text-right'>" + credit + "</td>" 
            + "<td><span class='glyphicon glyphicon-remove-circle pointer span-icon' onclick='removeRow($(this));'></span><span class='glyphicon glyphicon-ok-sign pointer span-icon' onclick='toggleDanger($(this));'></span><span class='glyphicon glyphicon glyphicon-thumbs-up pointer span-icon' onclick='toggleSuccess($(this));'></span></td></tr>";
    grid.find("tr:last").before(html);


    summate();

    statementReferenceTextBox.val("");
    accountCodeTextBox.val(1);
    debitTextBox.val("");
    creditTextBox.val("");
    repaint();
    statementReferenceTextBox.focus();
};

function summate() {

};

attachmentLabel.on("click", function () {
    "use strict";
    attachmentDiv.toggle(500);
});

debitTextBox.focus(function () {
    "use strict";
    getDebit();
});

creditTextBox.focus(function () {
    "use strict";
    getCredit();
});



var getDebit = function () {
    "use strict";
    var drTotal = parseFloat2(debitTotalTextBox.val());
    var crTotal = parseFloat2(creditTotalTextBox.val());


    if (crTotal > drTotal) {
        if (debitTextBox.val() === '' && creditTextBox.val() === '') {
            debitTextBox.val(crTotal - drTotal);
        };
    };
};

var getCredit = function () {
    "use strict";
    var drTotal = parseFloat2(debitTotalTextBox.val());
    var crTotal = parseFloat2(creditTotalTextBox.val());

    if (drTotal > crTotal) {
        if (debitTextBox.val() === '' && creditTextBox.val() === '') {
            creditTextBox.val(drTotal - crTotal);
        };
    };
};


//GridView Data Function


//GridView Manipulation


//Ajax Requests
function accountCodeExists(accountCode) {
    url = "/Services/AccountData.asmx/AccountCodeExists";
    data = appendParameter("", "accountCode", accountCode);
    data = getData(data);


    return getAjax(url, data);
};

function cashRepositoryCodeExists(cashRepositoryCode) {
    url = "/Services/AccountData.asmx/CashRepositoryCodeExists";
    data = appendParameter("", "cashRepositoryCode", cashRepositoryCode);
    data = getData(data);

    return getAjax(url, data);
};


function isCash(accountCode) {
    url = "/Services/AccountData.asmx/IsCashAccount";
    data = appendParameter("", "accountCode", accountCode);
    data = getData(data);


    return getAjax(url, data);
};

function hasBalance(cashRepositoryCode, credit) {
    url = "/Services/AccountData.asmx/HasBalance";
    data = appendParameter("", "cashRepositoryCode", cashRepositoryCode);
    data = appendParameter(data, "credit", credit);
    data = getData(data);

    return getAjax(url, data);
};

//Boolean Validation


//Validation Helper Functions


//Logic & Validation
var summate = function () {
    var debitTotal = sumOfColumn("#" + transactionGridView.attr("id"), 4);
    var creditTotal = sumOfColumn("#" + transactionGridView.attr("id"), 5);

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
        $('#DebitTextBox').focus();
    });

    shortcut.add("CTRL+ALT+C", function () {
        $('#CreditTextBox').focus();
    });

    shortcut.add("CTRL+ENTER", function () {
        $('#AddButton').click();
    });
};