$(window).load(function () {
    var grid = $("#TransferRequestGridView");
    var header = grid.find("thead tr");
    var rows = grid.find("tbody tr");

    var checkListUrl = "Confirmation/TransferRequest.mix?TranId=%s";
    var reportUrl = "Reports/InventoryTransferRequestReport.mix?TranId=%1$s";

    if (window.checkListUrlOverride) {
        checkListUrl = window.checkListUrlOverride;
    };

    if (window.reportUrlOverride) {
        reportUrl = window.reportUrlOverride;
    };

    var iconTemplate = "<a href=\"" + checkListUrl + "\" title=\"%s\">" +
                            "<i class=\"list icon\"></i>" +
                        "</a>" +
                        "<a title=\"%s\" onclick=\"showWindow('" + reportUrl + "');\">" +
                            "<i class=\"print icon\"></i>" +
                        "</a>" +
                        "<a title=\"%s\" onclick=\"window.scroll(0);\">" +
                            "<i class=\"arrow up icon\"></i>" +
                        "</a>";

    if (header.length) {
        header.prepend("<th>" + Resources.Titles.Actions() + "</th><th>" + Resources.Titles.Select() + "</th>");
        rows.prepend("<td></td><td><div class='ui toggle checkbox'><input type='checkbox' /><label></label></div></td>");

        rows.click(function () {
            var el = $(this);
            toogleSelection(el.find("input"));
        });


        rows.each(function () {
            var iconCell = $(this).find("td:first-child");
            var tranId = $(this).find("td:nth-child(3)").html();

            var template = sprintf(iconTemplate, tranId, Resources.Labels.GoToChecklistWindow(), Resources.Titles.Print(), Resources.Labels.GoToTop());

            iconCell.html(template);

        });
    };

    createFlaggedRows(grid);
});

var getSelectedItems = function () {
    //Get the grid instance.
    var grid = $("#TransferRequestGridView");

    //Set the position of the column which contains the checkbox.
    var checkBoxColumnPosition = "2";

    //Set the position of the column which contains id.
    var idColumnPosition = "3";

    var selection = getSelectedCheckBoxItemIds(checkBoxColumnPosition, idColumnPosition, grid);

    if (selection.length > 0) {
        $("#SelectedValuesHidden").val(selection.join(','));
        return true;
    } else {
        $.notify(Resources.Titles.NothingSelected(), "error");
        return false;
    }
};
