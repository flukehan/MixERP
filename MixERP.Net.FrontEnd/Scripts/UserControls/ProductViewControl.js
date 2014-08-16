$(document).ready(function () {
    var contentWidth = $("#content").width();
    var menuWidth = $("#menu2").width();

    var margin = 20;
    var width = contentWidth - menuWidth - margin;

    $("#GridPanel").css("width", width + "px");

});

var updateFlagColor = function () {
    //Get an instance of the form grid.

    var grid = $("#ProductViewGridView");

    //Set position of the column which contains color value.
    var bgColorColumnPos = "13";
    var fgColorColumnPos = "14";

    //Iterate through all the rows of the grid.
    grid.find("tr").each(function () {

        //Get the current row instance from the loop.
        var row = $(this);

        //Read the color value from the associated column.
        var background = row.find("td:nth-child(" + bgColorColumnPos + ")").html();
        var foreground = row.find("td:nth-child(" + fgColorColumnPos + ")").html();

        if (background) {
            if (background != '&nbsp;') {
                row.css("background", background);

                //Iterate through all the columns of the current row.
                row.find("td").each(function () {
                    //Prevent border display by unsetting the border information for each cell.
                    $(this).css("border", "none");

                });
            }
        }

        if (foreground) {
            if (foreground != '&nbsp;') {
                row.find("td").css("color", foreground);
            }
        }

        row.find(":nth-child(" + bgColorColumnPos + ")").hide();
        row.find(":nth-child(" + fgColorColumnPos + ")").hide();
    });
};

updateFlagColor();

var getSelectedItems = function () {
    var selection = [];

    //Get the grid instance.
    var grid = $("#ProductViewGridView");

    //Set the position of the column which contains the checkbox.
    var checkBoxColumnPosition = "2";

    //Set the position of the column which contains id.
    var idColumnPosition = "3";

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
                var id = row.find("td:nth-child(" + idColumnPosition + ")").html();

                //Add the ID to the array.
                selection.push(id);
            }
        }
    });


    if (selection.length > 0) {
        $("#SelectedValuesHidden").val(selection.join(','));
        return true;
    } else {
        $.notify("<%= Labels.NothingSelected %>", "error");
        return false;
    }
};

//Get FlagButton instance.
var flagButton = $("#flagButton");

flagButton.click(function () {
    //Get flag div instance which will be displayed under the button.
    var popunder = $("#flag-popunder");

    //Get FlagButton's position and height information.
    var left = $(this).position().left;
    var top = $(this).position().top;
    var height = $(this).height();

    //Margin in pixels.
    var margin = 12;

    popunder.css("left", left);
    popunder.css("top", top + height + margin);
    popunder.show(500);
});


$('#ProductViewGridView tr').click(function () {
    //console.log('Grid row was clicked. Now, searching the radio button.');
    var checkBox = $(this).find('td input:checkbox');
    //console.log('The check box was found.');
    toogleSelection(checkBox.attr("id"));
});

var toogleSelection = function (id) {

    var property = $("#" + id).prop("checked");

    if (property) {
        $("#" + id).prop("checked", false);
    } else {
        $("#" + id).prop("checked", true);
    }

    logToConsole(JSON.stringify($("#" + id).attr("checked")));

    logToConsole('Radio button selection was "' + id + '" toggled.');
};



$(document).ready(function () {
    shortcut.add("ALT+O", function () {
        $('#OfficeTextBox').foucs();
    });

    shortcut.add("CTRL+ENTER", function () {
        $('#ShowButton').click();
    });
});



function DropDown(el) {
    this.dd = el;
    this.placeholder = this.dd.children('span');
    this.opts = this.dd.find('ul.dropdown > li');
    this.val = '';
    this.index = -1;
    this.initEvents();
}

DropDown.prototype = {
    initEvents: function () {
        var obj = this;

        obj.dd.on('click', function (event) {
            $(this).toggleClass('active');
            event.stopPropagation();
        });

        obj.opts.on('click', function () {
            var opt = $(this);
            obj.val = opt.text();
            obj.index = opt.index();
            obj.placeholder.text(obj.val);
        });
    },

    getValue: function () {
        return this.val;
    },
    getIndex: function () {
        return this.index;
    }
};

$(function () {
    $(document).click(function () {
        // all dropdowns
        $('.wrapper-dropdown-5').removeClass('active');
    });

});