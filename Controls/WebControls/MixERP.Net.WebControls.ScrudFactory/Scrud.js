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
};

var showAll = function () {
    window.location = window.location.pathname + '?show=all';
};

var confirmAction = function () {
    var retVal = false;
    var selectedItemValue;

    var confirmed = confirm(localizedAreYouSure);

    if (confirmed) {
        selectedItemValue = getSelectedValue();

        if (selectedItemValue == undefined) {
            alert(localizedNothingSelected);
            retVal = false;
        } else {
            retVal = true;
            if (customFormUrl && keyColumn) {
                window.location = customFormUrl + "?" + keyColumn + "=" + selectedItemValue;
            }
        }
    }
    return retVal;
};

var getSelectedValue = function () {
    return $('[id^="SelectRadio"]:checked').val();
};

var selectNode = function (id) {
    $('[id^="SelectRadio"]').prop("checked", false);
    $("#" + id).prop("checked", true);
};


var printThis = function () {
    //Append the report template with a random number to prevent caching.
    var randomnumber = Math.floor(Math.random() * 1200);
    reportTemplatePath += "?" + randomnumber;

    //Load report template from the path.
    $.get(reportTemplatePath, function () { }).done(function (data) {

        //Load report header template.
        $.get(reportHeaderPath, function () { }).done(function (header) {
            var table = $("#" + formGridViewId).clone();
            var user = $("#" + userIdHiddenId).val();
            var office = $("#" + officeCodeHiddenId).val();

            $(table).find("tr.tableFloatingHeader").remove();

            $(table).find("th:first").remove();
            $(table).find("td:first-child").remove();

            table = "<table border='1' class='preview'>" + table.html() + "</table>";

            data = data.replace("{Header}", header);
            data = data.replace("{ReportHeading}", $("#" + titleLabelId).html());
            data = data.replace("{PrintDate}", date);
            data = data.replace("{UserName}", user);
            data = data.replace("{OfficeCode}", office);
            data = data.replace("{Table}", table);

            //Creating and opening a new window to display the report.
            var w = window.open();
            w.moveTo(0, 0);
            w.resizeTo(screen.width, screen.height);

            //Writing the report to the window.
            w.document.writeln(data);

            //Report sent to the browser.
        });
    });
};

Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
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
        var radio = $(this).find('td input:radio');

        //The radio button was found.
        selectNode(radio.attr("id"));
    });

    saveAndClose();
};

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function saveAndClose() {
    var lastValue = parseFloat2($("#LastValueHidden").val());

    if (lastValue > 0) {
        var ctl = getParameterByName('AssociatedControlId');
        $('#' + ctl, parent.document.body).val(lastValue);
        top.close();
    }
}

function adjustSpinnerSize() {
    //Adjusting AJAX Spinner Size.

    $(".ajax-container").height($(document).height());
    //Todo: Adjust spinner to page.height, not doc height
    //and adjust the x and y coordinates depending upon the
    //current scroll position.
};

function updateTableHeaders() {
    $("div.floating-header").each(function () {
        var originalHeaderRow = $(".tableFloatingHeaderOriginal", this);
        var floatingHeaderRow = $(".tableFloatingHeader", this);
        var offset = $(this).offset();
        var scrollTop = $(window).scrollTop();
        if ((scrollTop > offset.top) && (scrollTop < offset.top + $(this).height())) {
            floatingHeaderRow.css("visibility", "visible");
            floatingHeaderRow.css("z-index", "1");
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
            floatingHeaderRow.css("top", "0");
        }
    });
};

$(document).ready(function () {
    $("table.grid").each(function () {
        $(this).wrap("<div class=\"floating-header\" style=\"position:relative\"></div>");

        var originalHeaderRow = $("tr:first", this);
        originalHeaderRow.before(originalHeaderRow.clone());
        var clonedHeaderRow = $("tr:first", this);

        clonedHeaderRow.addClass("tableFloatingHeader");
        clonedHeaderRow.css("position", "absolute");
        clonedHeaderRow.css("top", "0");
        clonedHeaderRow.css("left", $(this).css("margin-left"));
        clonedHeaderRow.css("visibility", "hidden");

        originalHeaderRow.addClass("tableFloatingHeaderOriginal");
    });
    updateTableHeaders();
    $(window).scroll(updateTableHeaders);
    $(window).resize(updateTableHeaders);
});

var addNew = function () {
    if (customFormUrl) {
        top.location = customFormUrl;
    }

    $('#' + formGridViewId + 'tr').find('td input:radio').prop('checked', false);
    $('#form1').each(function () {
        this.reset();
    });

    $('#' + gridPanelId).hide(500);
    $('#' + formPanelId).show(500);

    //Prevent postback
    return false;
};

$(document).ready(function () {
    shortcut.add("ESC", function () {
        if (!$('#' + formPanelId).is(':hidden')) {

            if ($("#colorbox").css("display") != "block") {
                var result = confirm(localizedAreYouSure);
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
