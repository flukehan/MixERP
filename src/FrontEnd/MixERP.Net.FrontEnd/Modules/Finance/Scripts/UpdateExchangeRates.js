var exchangeRatesGridView = $("#ExchangeRatesGridView");
var currencyInputText = $("#CurrencyInputText");
var url;
var data;
var moduleSelect = $("#ModuleSelect");
var requestButton = $("#RequestButton");
var saveButton = $("#SaveButton");

$(document).ready(function() {
    GetCurrencies();
    loadModules();
});

function BindCurrencies(currencies) {
    $(currencies).each(function() {
        AddRow(this.CurrencyCode, this.CurrencySymbol, this.CurrencyName, this.HundredthName);
    });
};

function AddRow(currencyCode, currencySymbol, currencyName, hundredthName) {
    exchangeRatesGridView.find("tbody").append(String.format("<tr class='ui form'><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td><input type='text' id='{0}ExchangRateInputText' class='text-right currency' onblur='displayDescription(this);' /></td><td></td></tr>", currencyCode, currencySymbol, currencyName, hundredthName));
};

function GetCurrencies() {
    var ajaxGetExchangeCurrencies = getExchangeCurrencies();

    ajaxGetExchangeCurrencies.success(function(msg) {
        BindCurrencies(msg.d);
    });

    ajaxGetExchangeCurrencies.fail(function(xhr) {
        alert(xhr.responseText);
    });

};

function displayDescription(element) {
    var exchangeRate = parseFloat2($(element).val());

    if (!exchangeRate) {
        return;
    };

    var descriptionCell = $(element).parent().parent().find("td:nth-child(6)");
    var currencyCode = $(element).parent().parent().find("td:nth-child(1)").html();
    var exchangeRateReverse = (1.00 / exchangeRate).toFixed(4);

    var description = exchangeRate + " " + currencyCode + " = 1 " + currencyInputText.val() + " (" + exchangeRateReverse + " " + currencyInputText.val() + " = 1 " + currencyCode + ")";

    descriptionCell.html(description);
};

requestButton.click(function() {
    var moduleName = moduleSelect.getSelectedValue();

    if (moduleName) {
        var ajaxGetExchangeRates = getExchangeRates(moduleName);

        ajaxGetExchangeRates.success(function(msg) {
            updateExchangeRates(msg.d);
        });

        ajaxGetExchangeRates.fail(function(xhr) {
            logAjaxErrorMessage(xhr);
        });

    };
});

function updateExchangeRates(result) {

    for (i = 0; i < result.length; i++) {
        exchangeRatesGridView.find("tbody tr").each(function () {
            var currency = $(this).find("td:first-child").html();

            if (result[i].CurrencyCode === currency) {
                var input = $(this).find("input");
                input.val(result[i].ExchangeRate);
                input.trigger("blur");
                return false;
            };
        });
    };
};

saveButton.click(function () {
    var exchangeRates = [];

    exchangeRatesGridView.find("tbody tr").each(function() {
        var currencyCode = $(this).find("td:first-child").html();
        var rate = parseFloat($(this).find("input").val());

        if (rate) {
            var exchangeRate = new Object();
            exchangeRate.CurrencyCode = currencyCode;
            exchangeRate.Rate = rate;

            exchangeRates.push(exchangeRate);
        };
    });

    var ajaxSaveExchangeRates = saveExchangeRates(exchangeRates);

    ajaxSaveExchangeRates.success(function (msg) {
        if (msg.d) {
            window.location = "/";
        };
    });

    ajaxSaveExchangeRates.fail(function (xhr) {
        logAjaxErrorMessage(xhr);
    });


});

function saveExchangeRates(exchangeRates) {
    url = "/Modules/Finance/Services/CurrencyData.asmx/SaveExchangeRates";
    data = appendParameter("", "exchangeRates", exchangeRates);

    data = getData(data);

    return getAjax(url, data);
};


function loadModules() {
    url = "/Modules/Finance/Services/CurrencyData.asmx/GetModules";
    ajaxDataBind(url, moduleSelect);
};

function getExchangeCurrencies() {
    url = "/Modules/Finance/Services/CurrencyData.asmx/GetExchangeCurrencies";

    return getAjax(url);
};

function getExchangeRates(moduleName) {
    url = "/Modules/Finance/Services/CurrencyData.asmx/GetExchangeRates";
    data = appendParameter("", "moduleName", moduleName);

    data = getData(data);

    return getAjax(url, data);
};