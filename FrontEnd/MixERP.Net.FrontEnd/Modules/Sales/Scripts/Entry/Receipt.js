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

/*jshint -W032, -W098*/
/*global getAjax, getAjaxErrorMessage, logError, ajaxDataBind, ajaxUpdateVal, appendParameter, exchangeRateLocalized, getData, parseFloat2, parseInt2, partyDropDownList, repaint*/

var dueAmountTextBox = $("#DueAmountTextBox");
var costCenterDropDownList = $("#CostCenterDropDownList");
var currencyTextBox = $("#CurrencyTextBox");
var currencyDropDownList = $("#CurrencyDropDownList");
var amountTextBox = $("#AmountTextBox");
var amountInHomeCurrencyTextBox = $("#AmountInHomeCurrencyTextBox");
var debitExchangeRateTextBox = $("#DebitExchangeRateTextBox");
var debitExchangeRateTextBoxLabel = $("label[for='DebitExchangeRateTextBox']");

var creditExchangeRateTextBox = $("#CreditExchangeRateTextBox");
var creditExchangeRateTextBoxLabel = $("label[for='CreditExchangeRateTextBox']");

var baseAmountTextBox = $("#BaseAmountTextBox");
var finalDueAmountTextBox = $("#FinalDueAmountTextBox");

var instrumentCodeTextBox = $("#InstrumentCodeTextBox");

var cashRadio = $("#CashRadio");
var bankRadio = $("#BankRadio");
var cashRepositoryDropDownList = $("#CashRepositoryDropDownList");
var bankDropDownList = $("#BankDropDownList");
var postedDateTextBox = $("#PostedDateTextBox");
var referenceNumberTextBox = $("#ReferenceNumberTextBox");

var instrumentCodeTextBox = $("#InstrumentCodeTextBox");
var transactionCodeTextBox = $("#TransactionCodeTextBox");
var statementReferenceTextBox = $("#StatementReferenceTextBox");

var saveButton = $("#SaveButton");
var receiptTypeDiv = $("#ReceiptType");

//Variables
var homeCurrency = "";
var url = "";
var data = "";

receiptTypeDiv.find("label").click(function () {
    toggleTransactionType($(this)); repaint();
});

//Define a new callback function to subscribe to
//GoButton's click event
//in PartyControl.
var goButtonCallBack = function () {
    var totalDueAmountHidden = $("#TotalDueAmountHidden");
    var defaultCurrencySpan = $("#DefaultCurrencySpan");

    dueAmountTextBox.val(totalDueAmountHidden.val());
    currencyTextBox.val(defaultCurrencySpan.html());

    var ajaxGetHomeCurrency = getHomeCurrency();

    ajaxGetHomeCurrency.done(function (msg) {
        homeCurrency = msg.d;
        getExchangeRates();
        amountTextBox.focus();
    });

    ajaxGetHomeCurrency.fail(function (xhr) {
        logAjaxErrorMessage(xhr);
    });
};

$(document).ready(function () {
    $("#receipt").appendTo("#home");
    loadCurrencies();
    loadCashRepositories();
    loadBankAccounts();
    loadCostCenters();
});

//Control Events

saveButton.click(function () {
    var partyCode = partyDropDownList.getSelectedValue();
    var currencyCode = currencyDropDownList.getSelectedValue();
    var amount = parseFloat2(amountTextBox.val());
    var debitExchangeRate = parseFloat2(debitExchangeRateTextBox.val());
    var creditExchangeRate = parseFloat2(creditExchangeRateTextBox.val());
    var referenceNumber = referenceNumberTextBox.val();
    var statementReference = statementReferenceTextBox.val();
    var costCenterId = parseInt2(costCenterDropDownList.getSelectedValue());
    var cashRepositoryId = parseInt2(cashRepositoryDropDownList.getSelectedValue());
    var postedDate = postedDateTextBox.val();
    var bankAccountId = parseInt2(bankDropDownList.getSelectedValue());
    var bankInstrumentCode = instrumentCodeTextBox.val();
    var bankTransactionCode = transactionCodeTextBox.val();

    var ajaxSaveReceipt = saveReceipt(partyCode, currencyCode, amount, debitExchangeRate, creditExchangeRate, referenceNumber, statementReference, costCenterId, cashRepositoryId, postedDate, bankAccountId, bankInstrumentCode, bankTransactionCode);

    ajaxSaveReceipt.success(function (msg) {
        var id = msg.d;
        window.location = "/Modules/Sales/Confirmation/Receipt.mix?TranId=" + id;
    });

    ajaxSaveReceipt.fail(function (xhr) {
        logAjaxErrorMessage(xhr);
    });
});

function saveReceipt(partyCode, currencyCode, amount, debitExchangeRate, creditExchangeRate, referenceNumber, statementReference, costCenterId, cashRepositoryId, postedDate, bankAccountId, bankInstrumentCode, bankTransactionCode) {
    url = "/Modules/Sales/Services/Receipt/TransactionPosting.asmx/Save";
    data = appendParameter("", "partyCode", partyCode);
    data = appendParameter(data, "currencyCode", currencyCode);
    data = appendParameter(data, "amount", amount);
    data = appendParameter(data, "debitExchangeRate", debitExchangeRate);
    data = appendParameter(data, "creditExchangeRate", creditExchangeRate);
    data = appendParameter(data, "referenceNumber", referenceNumber);
    data = appendParameter(data, "statementReference", statementReference);
    data = appendParameter(data, "costCenterId", costCenterId);
    data = appendParameter(data, "cashRepositoryId", cashRepositoryId);
    data = appendParameter(data, "postedDate", postedDate);
    data = appendParameter(data, "bankAccountId", bankAccountId);
    data = appendParameter(data, "bankInstrumentCode", bankInstrumentCode);
    data = appendParameter(data, "bankTransactionCode", bankTransactionCode);
    data = getData(data);

    return getAjax(url, data);
};

currencyDropDownList.blur(function () {
    getExchangeRates();
});

function getExchangeRates() {
    if (exchangeRateLocalized) {
        debitExchangeRateTextBoxLabel.html(String.format("{0} ({1} - {2})", exchangeRateLocalized, currencyDropDownList.getSelectedValue(), homeCurrency));
        creditExchangeRateTextBoxLabel.html(String.format("{0} ({1} - {2})", exchangeRateLocalized, homeCurrency, currencyTextBox.val()));
    };

    getER(debitExchangeRateTextBox, currencyDropDownList.getSelectedValue(), homeCurrency);
    getER(creditExchangeRateTextBox, homeCurrency, currencyTextBox.val());
}

function getER(associatedControl, sourceCurrencyCode, destinationCurrencyCode) {
    url = "/Modules/Sales/Services/Receipt/Currencies.asmx/GetExchangeRate";
    data = appendParameter("", "sourceCurrencyCode", sourceCurrencyCode);
    data = appendParameter(data, "destinationCurrencyCode", destinationCurrencyCode);
    data = getData(data);

    ajaxUpdateVal(url, associatedControl, data);
};

amountTextBox.keyup(function () {
    updateTotal();
});

debitExchangeRateTextBox.keyup(function () {
    updateTotal();
});

function updateTotal() {
    var due = parseFloat2(dueAmountTextBox.val());
    var amount = parseFloat2(amountTextBox.val());
    var er = parseFloat2(debitExchangeRateTextBox.val());
    var er2 = parseFloat2(creditExchangeRateTextBox.val());
    var toHomeCurrency = amount * er;

    amountInHomeCurrencyTextBox.val(toHomeCurrency);

    var toBase = toHomeCurrency * er2;

    var remainingDue = due - toBase;

    baseAmountTextBox.val(toBase);

    finalDueAmountTextBox.val(remainingDue);

    finalDueAmountTextBox.removeClass("alert-danger");

    if (remainingDue < 0) {
        finalDueAmountTextBox.addClass("alert-danger");
    };
};

var toggleTransactionType = function (e) {
    if (e.find("input").attr("id") === "BankRadio") {
        if (!$("#BankFormGroup").is(":visible"));
        {
            $("#BankFormGroup").show(500);
            $("#CashFormGroup").hide();
            return;
        }
    };

    if (e.find("input").attr("id") === "CashRadio") {
        if (!$("#CashFormGroup").is(":visible"));
        {
            $("#CashFormGroup").show(500);
            $("#BankFormGroup").hide();
            return;
        }
    };
};

function loadCostCenters() {
    url = "/Modules/Sales/Services/Receipt/Accounts.asmx/GetCostCenters";
    ajaxDataBind(url, costCenterDropDownList);
};

function loadCurrencies() {
    url = "/Modules/Sales/Services/Receipt/Currencies.asmx/GetCurrencies";
    ajaxDataBind(url, currencyDropDownList);
};

function loadCashRepositories() {
    url = "/Modules/Sales/Services/Receipt/Accounts.asmx/GetCashRepositories";
    ajaxDataBind(url, cashRepositoryDropDownList);
};

function loadBankAccounts() {
    url = "/Modules/Sales/Services/Receipt/Accounts.asmx/GetBankAccounts";
    ajaxDataBind(url, bankDropDownList);
};

//Ajax Requests

function getHomeCurrency() {
    url = "/Modules/Sales/Services/Receipt/Currencies.asmx/GetHomeCurrency";
    return getAjax(url);
};