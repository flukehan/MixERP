var transactionGridView = $("#TransactionGridView");
var printButton = $("#PrintButton");

$(document).ready(function () {
    updateFlagColor();
    updatePreviewButtons();
});

printButton.click(function () {
    var templatePath = "/Reports/Print.html";
    var headerPath = "/Reports/Assets/Header.aspx";
    var title = $("h1").html();
    var targetControlId = "TransactionGridView";
    var date = now;
    var windowName = "Journal";
    var offsetFirst = 2;
    var offsetLast = 2;

    printGridView(templatePath, headerPath, title, targetControlId, date, user, office, windowName, offsetFirst, offsetLast);
});

var updateFlagColor = function () {
    var grid = $("#TransactionGridView");
    createFlaggedRows(grid);
};

var updatePreviewButtons = function () {
    var grid = $("#TransactionGridView");

    grid.find("tbody tr").each(function () {
        var row = $(this);
        var book = row.find("td:nth-child(5)").html();
        var glReportIcon = row.find("td:first-child").find(".icon.print");
        var icon = row.find("td:first-child").find(".icon.grid.layout");
        if (!
     (book.substring(0, 5).toLowerCase() === "sales"
         || book.substring(0, 8).toLowerCase() === "purchase")) {
            icon.hide();
        }
        if (book.substring(0, 9).toLowerCase() === "inventory" || book.toLowerCase() === "opening.inventory") {
            icon.show();
            glReportIcon.hide();
        }
        if (book.substring(0, 16).toLowerCase() === "purchase.payment" || book.substring(0, 13).toLowerCase() === "sales.receipt") {
            icon.hide();
        }
    });
};

var getSelectedItems = function () {
    //Set the position of the column which contains the checkbox.
    var checkBoxColumnPosition = "2";

    //Set the position of the column which contains id.
    var idColumnPosition = "3";

    var selection = getSelectedCheckBoxItemIds(checkBoxColumnPosition, idColumnPosition, transactionGridView);

    if (selection.length > 0) {
        $("#SelectedValuesHidden").val(selection.join(','));
        return true;
    } else {
        $.notify(Resources.Titles.NothingSelected(), "error");
        return false;
    }
};

transactionGridView.find('tr').click(function () {
    var checkBox = $(this).find('td input:checkbox');
    toogleSelection(checkBox);
});

function showCheckList(element) {
    var current = parseFloat2($(element).parent().parent().find("td:nth-child(3)").html());
    var url = "Confirmation/JournalVoucher.mix?TranId=" + current;

    window.location = url;
};

function showPreview(element) {
    var current = parseFloat2($(element).parent().parent().find("td:nth-child(3)").html());
    showWindow('Reports/GLAdviceReport.mix?TranId=' + current);
    return false;
};

function showStockDetail(element) {
    var current = parseFloat2($(element).parent().parent().find("td:nth-child(3)").html());
    showWindow('Reports/StockTransactionReport.mix?TranId=' + current);
    return false;
};