var tableToJSON = function (grid) {
    var colData = [];
    var rowData = [];

    var rows = grid.find("tr:not(:last-child)");

    rows.each(function () {
        var row = $(this);

        colData = [];

        row.find("td:not(:last-child)").each(function () {
            colData.push($(this).text());
        });

        rowData.push(colData);
    });

    var data = JSON.stringify(rowData);

    return data;
};