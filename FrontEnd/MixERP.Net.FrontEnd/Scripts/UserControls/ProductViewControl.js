/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

/*jshint -W098*/
/*global createFlaggedRows, getSelectedCheckBoxItemIds, nothingSelectedLocalized, popUnder, shortcut, toogleSelection*/

var flagPopunder = $("#flag-popunder");
var flagButton = $("#flagButton");

$(document).ready(function () {
    //var filterDiv = $("#FilterDiv").width();

    //var margin = 0;
    //var width = filterDiv - margin;

    //$("#GridPanel").css("width", width + "px");
    //$("#GridPanel").css("background-color", "red");
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