function getSelectedCheckBoxItemIds(checkBoxColumnPosition, itemIdColumnPosition, grid) {
    var selection = [];

    //Iterate through each row to investigate the selection.
    grid.find("tr").each(function () {
        //Get an instance of the current row in this loop.
        var row = $(this);

        //Get the instance of the cell which contains the checkbox.
        var checkBoxContainer = row.select("td:nth-child(" + checkBoxColumnPosition + ")");

        //Get the instance of the checkbox from the container.
        var checkBox = checkBoxContainer.find("input");

        if (checkBox) {
            //Check if the checkbox was selected or checked.
            if (checkBox.prop("checked")) {
                //Get ID from the associated cell.
                var id = row.find("td:nth-child(" + itemIdColumnPosition + ")").html();

                //Add the ID to the array.
                selection.push(id);
            }
        }
    });

    return selection;
};