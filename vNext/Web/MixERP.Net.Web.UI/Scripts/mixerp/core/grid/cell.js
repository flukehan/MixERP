var sumOfColumn = function (tableSelector, columnIndex) {
    var total = 0;

    $(tableSelector).find('tr').each(function () {
        var value = parseFormattedNumber($('td', this).eq(columnIndex).text());
        total += parseFloat2(value);
    });

    return $.number(total, currencyDecimalPlaces, decimalSeparator, thousandSeparator);
};

var getColumnText = function (row, columnIndex) {
    return row.find("td:eq(" + columnIndex + ")").text();
};

var setColumnText = function (row, columnIndex, value) {
    row.find("td:eq(" + columnIndex + ")").html(value);
};

var toggleDanger = function (cell) {
    var row = cell.closest("tr");
    row.toggleClass("negative");
};

var addDanger = function (row) {
    row.removeClass("negative");
    row.addClass("negative");
};

var toggleSuccess = function (cell) {
    var row = cell.closest("tr");
    row.toggleClass("positive");
};

var removeRow = function (cell) {
    var result = confirm(areYouSureLocalized);

    if (result) {
        cell.closest("tr").remove();
    }
};