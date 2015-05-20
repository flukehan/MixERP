function displayPreview() {
    var backgroundColorTextbox = $("#background_color");
    var foregroundColorTextbox = $("#foreground_color");

    var background = backgroundColorTextbox.val();
    var foreground = foregroundColorTextbox.val();
    var loremIpsum = $(".lorem.ipsum");

    loremIpsum.css("background-color", "#" + background);
    loremIpsum.css("color", "#" + foreground);
};




$(document).ready(function () {
    loadColor();
    displayPreview();
    addColorPicker();

    $('.color').blur(function() {
        $(".lorem.ipsum").hide();
    });
});

function addColorPicker() {
    $('.color').colorpicker({
        parts: 'full',
        alpha: true,
        open: function () {
            $(".lorem.ipsum").show(500);
        },
        select: function () {
            displayPreview();
        },
        colorFormat: 'RGBA'
    });

};

var loadColor = function () {
    //Get an instance of the form grid.
    var grid = $("#FormGridView");

    //Set position of the column which contains color value.
    var bgColorColumnPos = "4";
    var fgColorColumnPos = "5";

    //Iterate through all the rows of the grid.
    grid.find("tr").each(function () {

        //Get the current row instance from the loop.
        var row = $(this);

        //Read the color value from the associated column.
        var background = row.find("td:nth-child(" + bgColorColumnPos + ")").html();
        var foreground = row.find("td:nth-child(" + fgColorColumnPos + ")").html();

        if (background) {
            row.css("background", background);
        }

        if (foreground) {
            row.find("td").css("color", foreground);
        }

        //Iterate through all the columns of the current row.
        row.find("td").each(function () {
            //Prevent border display by unsetting the border information for each cell.
            $(this).css("border", "none");
        });

    });
};