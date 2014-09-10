var dueAmountTextBox = $("#DueAmountTextBox");
var currencyTextBox = $("#CurrencyTextBox");
var currencyDropDownList = $("#CurrencyDropDownList");
var amountTextBox = $("#AmountTextBox");
var exchangeRateTextBox = $("#ExchangeRateTextBox ");
var baseAmountTextBox = $("#BaseAmountTextBox");
var finalDueAmountTextBox = $("#FinalDueAmountTextBox");
var cashRadio = $("#CashRadio");
var bankRadio = $("#BankRadio");
var cashRepositoryDropDownList = $("#CashRepositoryDropDownList");
var bankDropDownList = $("#BankDropDownList");
var postedDateTextBox = $("#PostedDateTextBox");
var instrumentCodeTextBox = $("#InstrumentCodeTextBox");
var transactionCodeTextBox = $("#TransactionCodeTextBox");
var statementReferenceTextBox = $("#StatementReferenceTextBox");
var saveButton = $("#SaveButton");
var receiptTypeDiv = $("#ReceiptType");

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
    getER();
    amountTextBox.focus();
};

$(document).ready(function () {
    $("#receipt").appendTo("#home");
    loadCurrencies();
});

//Control Events
currencyDropDownList.blur(function () {
    getER();
});

function getER() {
    var ajaxGetExchangeRate = getExchangeRate(currencyDropDownList.getSelectedValue(), currencyTextBox.val());

    ajaxGetExchangeRate.done(function (msg) {
        exchangeRateTextBox.val(msg.d);
    });

    ajaxGetExchangeRate.fail(function (xhr) {
        logError(getAjaxErrorMessage(xhr));
    });
};

amountTextBox.keyup(function () {
    updateTotal();
});

exchangeRateTextBox.keyup(function () {
    updateTotal();
});

function updateTotal() {
    var due = parseFloat2(dueAmountTextBox.val());
    var amount = parseFloat2(amountTextBox.val());
    var er = parseFloat2(exchangeRateTextBox.val());
    var toBase = amount * er;

    var remainingDue = due - toBase;

    baseAmountTextBox.val(toBase);
    finalDueAmountTextBox.val(remainingDue);
};

var toggleTransactionType = function (e) {
    if (e.find("input").attr("id") == "BankRadio") {
        if (!$("#BankFormGroup").is(":visible"));
        {
            $("#BankFormGroup").show(500);
            $("#CashFormGroup").hide();
            return;
        }
    };

    if (e.find("input").attr("id") == "CashRadio") {
        if (!$("#CashFormGroup").is(":visible"));
        {
            $("#CashFormGroup").show(500);
            $("#BankFormGroup").hide();
            return;
        }
    };
};

function loadCurrencies() {
    url = "/Modules/Sales/Services/Currencies.asmx/GetCurrencies";

    var currencyAjax = getAjax(url);

    currencyAjax.success(function (msg) {
        currencyDropDownList.bindAjaxData(msg.d, true);
    });

    currencyAjax.error(function (xhr) {
        var err = $.parseJSON(xhr.responseText);
        appendItem(currencyDropDownList, 0, err.Message);
    });
};

//Ajax Requests
function getExchangeRate(sourceCurrencyCode, destinationCurrencyCode) {
    url = "/Modules/Sales/Services/Currencies.asmx/GetExchangeRate";
    data = appendParameter("", "sourceCurrencyCode", sourceCurrencyCode);
    data = appendParameter(data, "destinationCurrencyCode", destinationCurrencyCode);
    data = getData(data);

    return getAjax(url, data);
};