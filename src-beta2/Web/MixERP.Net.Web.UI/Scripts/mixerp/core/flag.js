jQuery.fn.getTotalColumns = function () {
    var grid = $($(this).selector);
    var row = grid.find("tr").eq(1);

    var colCount = 0;

    row.find("td").each(function () {
        if ($(this).attr('colspan')) {
            colCount += +$(this).attr('colspan');
        } else {
            colCount++;
        }
    });

    return colCount;
};

function createFlaggedRows(grid, bgColorColumnPos, fgColorColumnPos) {
    if (!bgColorColumnPos) {
        bgColorColumnPos = grid.getTotalColumns() - 1;
    };

    if (!fgColorColumnPos) {
        fgColorColumnPos = grid.getTotalColumns();
    };

    //Iterate through all the rows of the grid.
    grid.find("tr").each(function () {
        var row = $(this);

        //Read the color value from the associated column.
        var background = row.find("td:nth-child(" + bgColorColumnPos + ")").html();
        var foreground = row.find("td:nth-child(" + fgColorColumnPos + ")").html();

        if (background) {
            if (background !== '&nbsp;') {
                row.css("background", background);

                //Iterate through all the columns of the current row.
                row.find("td").each(function () {
                    //Prevent border display by unsetting the border information for each cell.
                    $(this).css("border", "none");
                });
            };
        };

        if (foreground) {
            if (foreground !== '&nbsp;') {
                row.find("td").css("color", foreground);
            };
        };

        row.find(":nth-child(" + bgColorColumnPos + ")").hide();
        row.find(":nth-child(" + fgColorColumnPos + ")").hide();
    });
};
