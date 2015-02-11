function displayPreview() {
    var background_color_textbox = $("#background_color_textbox");
    var foreground_color_textbox = $("#foreground_color_textbox");

    var background = background_color_textbox.val();
    var foreground = foreground_color_textbox.val();
    $(".lorem.ipsum").css("background-color", "#" + background);
    $(".lorem.ipsum").css("color", "#" + foreground);

};


//This event will be called by ASP.net AJAX during
//asynchronous partial page rendering.
function AsyncListener() {
    //At this point, the GridView should have already been reloaded.
    //So, load color information on the grid once again.
    loadColor();
    displayPreview();
    addColorPicker();
};


$(document).ready(function () {
    loadColor();
    addColorPicker();
});

function scrudCustomValidator() {
    $(".lorem.ipsum").hide(500);
};

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