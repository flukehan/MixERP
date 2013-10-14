//var localizedAreYouSure = 'Are you sure?';
//var localizedNothingSelected = 'Nothing selected!';
//var reportTemplatePath = "/Reports/Print.html?";
//var reportHeaderPath = "/Reports/Assets/Header.aspx";
//var date = Date.now;
//var containerMargin = 340;
var formGridViewId = "FormGridView";
var gridPanelId = "GridPanel";
var userIdHiddenId = "UserIdHidden";
var officeCodeHiddenId = "OfficeCodeHidden";
var titleLabelId = "TitleLabel";
var formPanelId = "FormPanel";
var cancelButtonId = "CancelButton";
var showCompactButtonId = "ShowCompactButton";
var showAllButtonId = "ShowAllButton";
var addButtonId = "AddButton";
var editButtonId = "EditButton";
var deleteButtonId = "DeleteButton";


var showCompact = function () {
    window.location = window.location.pathname + '?show=compact';
}

var showAll = function () {
    window.location = window.location.pathname + '?show=all';
}

var confirmAction = function () {
    var c = confirm(localizedAreYouSure);

    if (c) {
        var selected = selectedValue();

        if (selected == undefined) {
            alert(localizedNothingSelected);
            return false;
        }
        return true;
    }
    else {
        return false;
    }
}

var selectedValue = function () {
    return $('[id^="SelectRadio"]:checked').val();
}

var selectNode = function (id) {
    $('[id^="SelectRadio"]').removeAttr("checked");
    $("#" + id).attr("checked", "checked");
}



var printThis = function () {
    //Append the report template with a random number to prevent caching.
    var randomnumber = Math.floor(Math.random() * 1200)
    reportTemplatePath += "?" + randomnumber;

    //Load report template from the path.
    var report = $.get(reportTemplatePath, function () { }).done(function (data) {

        //Load report header template.
        var report = $.get(reportHeaderPath, function () { }).done(function (header) {
            var table = $("#" + formGridViewId).clone();
            var user = $("#" + userIdHiddenId).val();
            var office = $("#" + officeCodeHiddenId).val();

            $(table).find("tr.tableFloatingHeader").remove();

            $(table).find("th:first").remove();
            $(table).find("td:first-child").remove();

            table = "<table border='1' class='preview'>" + table.html() + "</table>";

            data = data.replace("{ReportHeading}", $("#" + titleLabelId).html());
            data = data.replace("{PrintDate}", date);
            data = data.replace("{UserName}", user);
            data = data.replace("{OfficeCode}", office);
            data = data.replace("{Table}", table);
            data = data.replace("{Header}", header);


            //Creating and opening a new window to display the report.
            var w = window.open();
            w.moveTo(0, 0);
            w.resizeTo(screen.width, screen.height);

            //Writing the report to the window.
            w.document.writeln(data);

            //Report sent to the browser.
        });
    });
}

Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
    //Fired on each ASP.net AJAX request.
    initialize();
});

$(document).ready(function () {
    initialize();
});

var initialize = function () {
    //Adjusting panel size.
    var gridPanel = $('#' + gridPanelId);
    gridPanel.css("width", $(window).width() - parseInt(containerMargin));
    adjustSpinnerSize();

    //Registering grid row click event to automatically select the radio.
    $('#' + formGridViewId + ' tr').click(function () {
        //Grid row was clicked. Now, searching the radio button.
        var radio = $(this).find('td input:radio')

        //The radio button was found.
        selectNode(radio.attr("id"));
    });
}

function adjustSpinnerSize() {
    //Adjusting AJAX Spinner Size.

    $(".ajax-container").height($(document).height());
    //Todo: Adjust spinner to page.height, not doc height
    //and adjust the x and y coordinates depending upon the
    //current scroll position.
}




function UpdateTableHeaders() {
    $("div.floating-header").each(function () {
        var originalHeaderRow = $(".tableFloatingHeaderOriginal", this);
        var floatingHeaderRow = $(".tableFloatingHeader", this);
        var offset = $(this).offset();
        var scrollTop = $(window).scrollTop();
        if ((scrollTop > offset.top) && (scrollTop < offset.top + $(this).height())) {
            floatingHeaderRow.css("visibility", "visible");
            floatingHeaderRow.css("top", Math.min(scrollTop - offset.top, $(this).height() - floatingHeaderRow.height()) + "px");

            // Copy cell widths from original header
            $("th", floatingHeaderRow).each(function (index) {
                var cellWidth = $("th", originalHeaderRow).eq(index).css('width');
                $(this).css('width', cellWidth);
            });

            // Copy row width from whole table
            floatingHeaderRow.css("width", $("table.grid").css("width"));
        }
        else {
            floatingHeaderRow.css("visibility", "hidden");
            floatingHeaderRow.css("top", "0px");
        }
    });
}

$(document).ready(function () {
    $("table.grid").each(function () {
        $(this).wrap("<div class=\"floating-header\" style=\"position:relative\"></div>");

        var originalHeaderRow = $("tr:first", this)
        originalHeaderRow.before(originalHeaderRow.clone());
        var clonedHeaderRow = $("tr:first", this)

        clonedHeaderRow.addClass("tableFloatingHeader");
        clonedHeaderRow.css("position", "absolute");
        clonedHeaderRow.css("top", "0px");
        clonedHeaderRow.css("left", $(this).css("margin-left"));
        clonedHeaderRow.css("visibility", "hidden");

        originalHeaderRow.addClass("tableFloatingHeaderOriginal");
    });
    UpdateTableHeaders();
    $(window).scroll(UpdateTableHeaders);
    $(window).resize(UpdateTableHeaders);
});

var addNew = function () {
    $('#' + formGridViewId + 'tr').find('td input:radio').removeAttr('checked');
    $('#form1').each(function () {
        this.reset();
    });

    $('#' + gridPanelId).hide(500);
    $('#' + formPanelId).show(500);

    //Prevent postback
    return false;
}

$(document).ready(function () {
    shortcut.add("ESC", function () {
        if (!$('#' + formPanelId).is(':hidden')) {

            if ($("#colorbox").css("display") != "block") {
                var result = confirm('Are you sure?');
                if (result) {
                    $('#' + cancelButtonId).click();
                }
            }
        }
    });

    shortcut.add("ALT+C", function () {
        $('#' + showCompactButtonId).click();
    });

    shortcut.add("CTRL+S", function () {
        $('#' + showAllButtonId).click();
    });

    shortcut.add("ALT+A", function () {
        $('#' + addButtonId).click();
    });

    shortcut.add("CTRL+E", function () {
        $('#' + editButtonId).click();
    });

    shortcut.add("CTRL+D", function () {
        $('#' + deleteButtonId).click();
    });

    shortcut.add("CTRL+P", function () {
        printThis();
    });

});
