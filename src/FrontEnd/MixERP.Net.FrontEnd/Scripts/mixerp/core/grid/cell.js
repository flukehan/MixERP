var sumOfColumn = function (tableSelector, columnIndex) {
    var total = 0;

    $(tableSelector).find('tr').each(function () {
        var value = parseFloat2($('td', this).eq(columnIndex).text());
        total += value;
    });

    return total;
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
    var result = confirm(Resources.Questions.AreYouSure());

    if (result) {
        cell.closest("tr").remove();
    }
};