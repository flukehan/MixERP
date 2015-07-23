$(document).ready(function () {
    setCurrencyFormat();
    setNumberFormat();

    if (typeof Sys !== "undefined") {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Page_EndRequest);
    }
});

//Fired on ASP.net Ajax Postback
function Page_EndRequest() {
    setCurrencyFormat();
    setNumberFormat();

    if (typeof (AsyncListener) === "function") {
        AsyncListener();
    };
};

var setCurrencyFormat = function () {
    if (typeof currencyDecimalPlaces === "undefined" || typeof decimalSeparator === "undefined" || typeof thousandSeparator === "undefined") {
        return;
    };

    $('input.currency').number(true, currencyDecimalPlaces, decimalSeparator, thousandSeparator);
};

var setNumberFormat = function () {
    if (typeof decimalSeparator === "undefined" || typeof thousandSeparator === "undefined") {
        return;
    };

    $('input.decimal').number(true, currencyDecimalPlaces, decimalSeparator, thousandSeparator);
    $('input.decimal4').number(true, 4, decimalSeparator, thousandSeparator);
    $('input.integer').number(true, 0, decimalSeparator, thousandSeparator);
};

var parseFormattedNumber = function (input) {
    if (typeof window.thousandSeparator === "undefined") {
        window.thousandSeparator = ",";
    };

    if (typeof window.decimalSeparator === "undefined") {
        window.decimalSeparator = ".";
    };

    var result = input.split(thousandSeparator).join("");
    result = result.split(decimalSeparator).join(".");
    return result;
};

var getFormattedNumber = function (input, isInteger) {
    if (typeof window.currencyDecimalPlaces === "undefined") {
        window.currencyDecimalPlaces = 2;
    };

    if (typeof window.thousandSeparator === "undefined") {
        window.thousandSeparator = ",";
    };

    if (typeof window.decimalSeparator === "undefined") {
        window.decimalSeparator = ".";
    };

    var decimalPlaces = currencyDecimalPlaces;

    if (isInteger) {
        decimalPlaces = 0;
    };

    return $.number(input, decimalPlaces, decimalSeparator, thousandSeparator);
};

stringFormat = function () {
    var s = arguments[0];

    for (var i = 0; i < arguments.length - 1; i++) {
        var reg = new RegExp("\\{" + i + "\\}", "gm");
        s = s.replace(reg, arguments[i + 1]);
    };

    return s;
};