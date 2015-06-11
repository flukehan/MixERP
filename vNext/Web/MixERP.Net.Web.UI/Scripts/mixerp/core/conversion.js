var parseFloat2 = function (arg) {
    if (typeof (arg) === "undefined") {
        return 0;
    };

    var input = arg;

    if (currencySymbol) {
        input = input.toString().replace(currencySymbol, "");
    };

    var val = parseFloat(parseFormattedNumber(input.toString()) || 0);

    if (isNaN(val)) {
        val = 0;
    }

    return val;
};

var parseInt2 = function (arg) {
    if (typeof (arg) === "undefined") {
        return 0;
    };

    var val = parseInt(parseFormattedNumber(arg.toString()) || 0);

    if (isNaN(val)) {
        val = 0;
    }

    return val;
};

function parseDate(str) {
    return new Date(Date.parse(str));
};

function parseSerializedDate(str) {
    str = str.replace(/[^0-9 +]/g, '');
    return new Date(parseInt(str));
};