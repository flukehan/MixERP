/*jshint -W098*/
/*global createFlaggedRows, getSelectedCheckBoxItemIds, nothingSelectedLocalized, popUnder, shortcut, toogleSelection*/

var flagPopunder = $("#flag-popunder");
var flagButton = $("#flagButton");

$(document).ready(function () {
    var contentWidth = $("#content").width();
    var menuWidth = $("#menu2").width();

    var margin = 20;
    var width = contentWidth - menuWidth - margin;

    $("#GridPanel").css("width", width + "px");
    updateFlagColor();
});

var updateFlagColor = function () {
    //Get an instance of the form grid.
    var grid = $("#ProductViewGridView");
    createFlaggedRows(grid);
};

var getSelectedItems = function () {
    //Get the grid instance.
    var grid = $("#ProductViewGridView");

    //Set the position of the column which contains the checkbox.
    var checkBoxColumnPosition = "2";

    //Set the position of the column which contains id.
    var idColumnPosition = "3";

    var selection = getSelectedCheckBoxItemIds(checkBoxColumnPosition, idColumnPosition, grid);

    if (selection.length > 0) {
        $("#SelectedValuesHidden").val(selection.join(','));
        return true;
    } else {
        $.notify(nothingSelectedLocalized, "error");
        return false;
    }
};

flagButton.click(function () {
    popUnder(flagPopunder, flagButton);
});

$('#ProductViewGridView tr').click(function () {
    //console.log('Grid row was clicked. Now, searching the radio button.');
    var checkBox = $(this).find('td input:checkbox');
    //console.log('The check box was found.');
    toogleSelection(checkBox.attr("id"));
});

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