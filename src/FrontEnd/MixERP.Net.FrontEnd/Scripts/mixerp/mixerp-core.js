///#source 1 1 /Scripts/mixerp/core/dom/cascading-pair.js
function createCascadingPair(select, input) {
    input.blur(function () {
        selectDropDownListByValue(this.id, select.attr("id"));
    });

    select.change(function () {
        input.val(select.getSelectedValue());
    });
};

var selectDropDownListByValue = function (textBoxId, dropDownListId) {
    var listControl = $("#" + dropDownListId);
    var textBox = $("#" + textBoxId);
    var selectedValue = textBox.val();
    var exists;

    if (isNullOrWhiteSpace(textBox.val())) {
        return;
    };

    if (listControl.length) {
        listControl.find('option').each(function () {
            if (this.value === selectedValue) {
                exists = true;
            }
        });
    }

    if (exists) {
        listControl.val(selectedValue).trigger('change');
    } else {
        textBox.val('').trigger('change');
    }

    triggerChange(dropDownListId);
};
///#source 1 1 /Scripts/mixerp/core/dom/checkable.js
var toogleSelection = function (element) {
    var property = element.prop("checked");

    if (property) {
        element.prop("checked", false);
    } else {
        element.prop("checked", true);
    }
};
///#source 1 1 /Scripts/mixerp/core/dom/document.js
function getDocHeight(margin) {
    var D = document;
    var height = Math.max(
        Math.max(D.body.scrollHeight, D.documentElement.scrollHeight),
        Math.max(D.body.offsetHeight, D.documentElement.offsetHeight),
        Math.max(D.body.clientHeight, D.documentElement.clientHeight)
    );

    if (margin) {
        height += parseInt2(margin);
    };

    return height;
};

var repaint = function () {
    setTimeout(function () {
        $(document).trigger('resize');
    }, 1000);
};
///#source 1 1 /Scripts/mixerp/core/dom/events.js
var triggerChange = function (controlId) {
    var element = document.getElementById(controlId);

    if ('createEvent' in document) {
        var evt = document.createEvent("HTMLEvents");
        evt.initEvent("change", false, true);
        element.dispatchEvent(evt);
    } else {
        if ("fireEvent" in element)
            element.fireEvent("onchange");
    }
};

var triggerClick = function (controlId) {
    var element = document.getElementById(controlId);

    if ('createEvent' in document) {
        var evt = document.createEvent("HTMLEvents");
        evt.initEvent("click", false, true);
        element.dispatchEvent(evt);
    } else {
        if ("fireEvent" in element)
            element.fireEvent("onclick");
    }
};

///#source 1 1 /Scripts/mixerp/core/dom/loader.js
function addLoader(el) {
    $(el).addClass('loading');
};

function removeLoader(el) {
    $(el).removeClass('loading');
};
///#source 1 1 /Scripts/mixerp/core/dom/popunder.js
function popUnder(div, button) {
    div.removeClass("initially hidden");
    div.show(500);

    div.position({
        my: "left top",
        at: "left bottom",
        of: button
    });
};
///#source 1 1 /Scripts/mixerp/core/dom/select.js
jQuery.fn.getSelectedItem = function () {
    var listItem = $(this[0]);
    return listItem.find("option:selected");
};

jQuery.fn.getSelectedValue = function () {
    return $(this[0]).getSelectedItem().val();
};

jQuery.fn.getSelectedText = function () {
    return $(this[0]).getSelectedItem().text();
};

jQuery.fn.setSelectedText = function (text) {
    var target = $(this).find("option").filter(function () {
        return this.text === text;
    });

    target.prop('selected', true);
};
///#source 1 1 /Scripts/mixerp/core/dom/visibility.js
function setVisible(targetControl, visible, timeout) {
    if (visible) {
        targetControl.show(timeout);
        return;
    };

    targetControl.hide(timeout);
};

function addNotification(message, onclick) {
    var count = parseInt2($("#NotificationMenu span").addClass("ui red label").html());
    count++;
    $("#NotificationMenu span").addClass("ui red label").html(count);

    var item = $("<div />");
    item.attr("class", "item");

    if (onclick) {
        item.attr("onclick", onclick);
    };

    item.html(message);

    $("#Notification").append(item);
};
///#source 1 1 /Scripts/mixerp/core/grid/cell.js
var sumOfColumn = function (tableSelector, columnIndex) {
    var total = 0;

    $(tableSelector).find('tr').each(function () {
        var value = parseFloat2($('td', this).eq(columnIndex).text());
        total += value;
    });

    return total;
};

var getColumnText = function (row, columnIndex) {
    return row.find("td:eq(" + columnIndex + ")").text();
};

var setColumnText = function (row, columnIndex, value) {
    row.find("td:eq(" + columnIndex + ")").html(value);
};

var toggleDanger = function (cell) {
    var row = cell.closest("tr");
    row.toggleClass("negative");
};

var addDanger = function (row) {
    row.removeClass("negative");
    row.addClass("negative");
};

var toggleSuccess = function (cell) {
    var row = cell.closest("tr");
    row.toggleClass("positive");
};

var removeRow = function (cell) {
    var result = confirm(Resources.Questions.AreYouSure());

    if (result) {
        cell.closest("tr").remove();
    }
};
///#source 1 1 /Scripts/mixerp/core/grid/grid.js
function getSelectedCheckBoxItemIds(checkBoxColumnPosition, itemIdColumnPosition, grid, offset) {
    var selection = [];

    if (!offset) {
        offset = 0;
    };

    //Iterate through each row to investigate the selection.
    grid.find("tr").each(function (i) {
        if (i > offset) {
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
                };
            };
        };
    });

    return selection;
};
///#source 1 1 /Scripts/mixerp/core/grid/print-grid.js
var printGridView = function (templatePath, headerPath, reportTitle, gridViewId, printedDate, user, office, windowName, offset, offsetLast) {
    //Load report template from the path.
    $.get(templatePath, function () { }).done(function (data) {
        //Load report header template.
        $.get(headerPath, function () { }).done(function (header) {
            var table = $("#" + gridViewId).clone();

            table.find("tr.tableFloatingHeader").remove();

            table.find("th:nth-child(-n" + offset + ")").remove();
            table.find("td:nth-child(-n" + offset + ")").remove();

            table.find("th:nth-last-child(-n" + offsetLast + ")").remove();
            table.find("td:nth-last-child(-n" + offsetLast + ")").remove();

            table.find("td").removeAttr("style");
            table.find("tr").removeAttr("style");

            table = "<table border='1' class='preview'>" + table.html() + "</table>";

            data = data.replace("{Header}", header);
            data = data.replace("{ReportHeading}", reportTitle);
            data = data.replace("{PrintDate}", printedDate);
            data = data.replace("{UserName}", user);
            data = data.replace("{OfficeCode}", office);
            data = data.replace("{Table}", table);

            //Creating and opening a new window to display the report.
            var w = window.open('', windowName,
                + ',menubar=0'
                + ',toolbar=0'
                + ',status=0'
                + ',scrollbars=1'
                + ',resizable=0');
            w.moveTo(0, 0);
            w.resizeTo(screen.width, screen.height);

            //Writing the report to the window.
            w.document.writeln(data);
            w.document.close();

            //Report sent to the browser.
        });
    });
};
///#source 1 1 /Scripts/mixerp/core/libraries/chartjs.js
function shuffle(o) {
    for (var j, x, i = o.length; i; j = Math.floor(Math.random() * i), x = o[--i], o[i] = o[j], o[j] = x);
    return o;
};

var chartColors = shuffle(["#DF0101", "#DF3A01", "#DF7401", "#DBA901", "#D7DF01", "#A5DF00", "#74DF00", "#3ADF00", "#01DF74", "#01DFA5", "#01DFD7", "#01A9DB", "#0174DF", "#013ADF", "#0101DF", "#3A01DF", "#7401DF", "#A901DB", "#DF01D7", "#DF01A5", "#DF0174", "#DF013A", "#6E6E6E"]);

function getFillColor(index) {
    var color = hexToRgb(chartColors[index]);
    var opacity = 0.8;
    return "rgba(" + color.r + "," + color.g + "," + color.b + "," + opacity + ")";
};

function hexToRgb(hex) {
    // Expand shorthand form (e.g. "03F") to full form (e.g. "0033FF")
    var shorthandRegex = /^#?([a-f\d])([a-f\d])([a-f\d])$/i;
    hex = hex.replace(shorthandRegex, function (m, r, g, b) {
        return r + r + g + g + b + b;
    });

    var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
    return result ? {
        r: parseInt(result[1], 16),
        g: parseInt(result[2], 16),
        b: parseInt(result[3], 16)
    } : null;
};

function prepareChart(datasourceId, canvasId, legendId, type, log) {
    chartColors = shuffle(chartColors);
    var table = $("#" + datasourceId);
    var labels = [];
    var data = [];
    var datasets = [];
    var title;
    var index = 0;

    //Loop through the table header for labels.
    table.find("tr:first-child th:not(:first-child)").each(function () {
        //Create labels from header row columns.
        labels.push($(this).html());
    });

    //Loop through each row of the table body.
    table.find("tr").not(":first").each(function () {
        //Get an instance of the current row
        var row = $(this);

        //The first column of each row is the legend.
        title = row.find(":first-child").html();

        //Reset the data object's value from the previous iteration.
        data = [];
        //Loop through the row columns.
        row.find(":not(:first-child)").each(function () {
            //Get data from this row.
            data.push(parseFloat2($(this).html()));
        });

        //Create a new dataset representing this row.
        var dataset =
        {
            fillColor: getFillColor(index),
            strokeColor: chartColors[index],
            pointColor: chartColors[index],
            data: data,
            title: title
        };

        //Add the dataset object to the array object.
        datasets.push(dataset);

        if (log) {
            console.log(JSON.stringify(datasets));
        }

        index++;
    });

    table.remove();

    var reportData = {
        labels: labels,
        datasets: datasets
    };

    var ctx = document.getElementById(canvasId).getContext("2d");

    switch (type) {
        case "line":
            new Chart(ctx).Line(reportData);
            break;
        case "radar":
            new Chart(ctx).Radar(reportData);
            break;
        default:
            new Chart(ctx).Bar(reportData);
            break;
    }

    legend(document.getElementById(legendId), reportData);
    table.hide();
}

function preparePieChart(datasourceId, canvasId, legendId, type, hide, titleColumnIndex, valueColumnIndex) {
    chartColors = shuffle(chartColors);
    var table = $("#" + datasourceId);
    var value;
    var data = [];

    if (typeof titleColumnIndex === "undefined") {
        titleColumnIndex = 0;
    };

    if (typeof valueColumnIndex === "undefined") {
        valueColumnIndex = 1;
    };

    //Reset the counter.
    var counter = 0;

    //Loop through each row of the table body.
    table.find("tr").not(":first").each(function () {
        //Get an instance of the current row
        var row = $(this);

        //The first column of each row is the legend.
        var title = row.find("td:eq(" + parseInt2(titleColumnIndex) + ")").html();

        //The first column of each row is the legend.
        value = parseInt(row.find("td:eq(" + parseInt2(valueColumnIndex) + ")").html());

        var dataset = {
            value: value,
            color: chartColors[counter],
            title: title
        };

        //Add the dataset object to the array object.
        data.push(dataset);
        counter++;
    });

    var ctx = document.getElementById(canvasId).getContext("2d");

    switch (type) {
        case "doughnut":
            new Chart(ctx).Doughnut(data);
            break;
        case "polar":
            new Chart(ctx).PolarArea(data);
            break;
        default:
            new Chart(ctx).Pie(data);
            break;
    }

    legend(document.getElementById(legendId), data);
    if (hide) {
        table.hide();
    };
};
///#source 1 1 /Scripts/mixerp/core/libraries/colorbox.js
var showWindow = function (url) {
    $.colorbox({ width: +$('html').width() * 0.7, height: +$('html').height() * 0.7, iframe: true, href: url });
};
///#source 1 1 /Scripts/mixerp/core/libraries/jquery-notify.js
function displayMessage(a, b) {
    $.notify(a, b);
};

function displaySucess() {
    $.notify(Resources.Labels.TaskCompletedSuccessfully(), "success");
};

var logError = function (a, b) {
    //Todo
    $.notify(a, b);
};

function logAjaxErrorMessage(xhr) {
    logError(getAjaxErrorMessage(xhr));
};
///#source 1 1 /Scripts/mixerp/core/libraries/semantic-ui.js
//Semantic UI Tab Support
$(document).ready(function () {
    var tabItems = $('.tabular .item');

    if (tabItems && tabItems.length > 0) {
        tabItems.tab();
    };

    //Semantic UI Button Support
    var buttons = $(".buttons .button");

    buttons.on("click", function () {
        buttons.removeClass("active");
        $(this).addClass("active");
    });
});
///#source 1 1 /Scripts/mixerp/core/prototype/string.js
if (!String.prototype.format) {
// ReSharper disable once NativeTypePrototypeExtending
    String.prototype.format = function () {
        var args = arguments;
        return this.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] !== 'undefined'
                ? args[number]
                : match;
        });
    };
};
///#source 1 1 /Scripts/mixerp/core/aspnet-validation.js
var validateByControlId = function (controlId) {
    if (typeof Page_ClientValidate === "function") {
        Page_ClientValidate(controlId);
    };
};
///#source 1 1 /Scripts/mixerp/core/conversion.js
var parseFloat2 = function (arg) {
    if (typeof (arg) === "undefined") {
        return 0;
    };

    var input = arg;

    if (currencySymbol) {
        input = input.toString().replace(currencySymbol, "");
    };

    var val = parseFloat(parseFormattedNumber(input.toString()) || 0);

    if (isNaN(val)) {
        val = 0;
    }

    return val;
};

var parseInt2 = function (arg) {
    if (typeof (arg) === "undefined") {
        return 0;
    };

    var val = parseInt(parseFormattedNumber(arg.toString()) || 0);

    if (isNaN(val)) {
        val = 0;
    }

    return val;
};

function parseDate(str) {
    return new Date(Date.parse(str));
};

function parseSerializedDate(str) {
    str = str.replace(/[^0-9 +]/g, '');
    return new Date(parseInt(str));
};
///#source 1 1 /Scripts/mixerp/core/date-expressions.js
function dateAdd(dt, expression, number) {
    var d = Date.parseExact(dt, shortDateFormat);
    var ret = new Date();

    if (expression === "d") {
        ret = new Date(d.getFullYear(), d.getMonth(), d.getDate() + parseInt(number));
    }

    if (expression === "m") {
        ret = new Date(d.getFullYear(), d.getMonth() + parseInt(number), d.getDate());
    }

    if (expression === "y") {
        ret = new Date(d.getFullYear() + parseInt(number), d.getMonth(), d.getDate());
    }

    return ret.toString(shortDateFormat);
};

$(document).ready(function () {
    $(".date").blur(function () {
        if (today === "") return;
        var control = $(this);
        var value = control.val().trim().toLowerCase();
        var result;
        var number;

        if (value === "d") {
            result = dateAdd(today, "d", 0);
            control.val(result).trigger('change');
            validateByControlId(control.attr("id"));
            return;
        }

        if (value === "m" || value === "+m") {
            control.val(dateAdd(today, "m", 1)).trigger('change');
            validateByControlId(control.attr("id"));
            return;
        }

        if (value === "w" || value === "+w") {
            control.val(dateAdd(today, "d", 7)).trigger('change');
            validateByControlId(control.attr("id"));
            return;
        }

        if (value === "y" || value === "+y") {
            control.val(dateAdd(today, "y", 1)).trigger('change');
            validateByControlId(control.attr("id"));
            return;
        }

        if (value === "-d") {
            control.val(dateAdd(today, "d", -1)).trigger('change');
            validateByControlId(control.attr("id"));
            return;
        }

        if (value === "+d") {
            control.val(dateAdd(today, "d", 1)).trigger('change');
            validateByControlId(control.attr("id"));
            return;
        }

        if (value === "-w") {
            control.val(dateAdd(today, "d", -7)).trigger('change');
            validateByControlId(control.attr("id"));
            return;
        }

        if (value === "-m") {
            control.val(dateAdd(today, "m", -1)).trigger('change');
            validateByControlId(control.attr("id"));
            return;
        }

        if (value === "-y") {
            control.val(dateAdd(today, "y", -1)).trigger('change');
            validateByControlId(control.attr("id"));
            return;
        }

        if (value.indexOf("d") >= 0) {
            number = parseInt(value.replace("d"));
            control.val(dateAdd(today, "d", number)).trigger('change');
            validateByControlId(control.attr("id"));
            return;
        }

        if (value.indexOf("w") >= 0) {
            number = parseInt(value.replace("w"));
            control.val(dateAdd(today, "d", number * 7)).trigger('change');
            validateByControlId(control.attr("id"));
            return;
        }

        if (value.indexOf("m") >= 0) {
            number = parseInt(value.replace("m"));
            control.val(dateAdd(today, "m", number)).trigger('change');
            validateByControlId(control.attr("id"));
            return;
        }

        if (value.indexOf("y") >= 0) {
            number = parseInt(value.replace("y"));
            control.val(dateAdd(today, "y", number)).trigger('change');
            validateByControlId(control.attr("id"));
            return;
        }
    });
});
///#source 1 1 /Scripts/mixerp/core/browser.js
function supportsBrowserStorage() {
    try {
        return 'localStorage' in window && window['localStorage'] !== null;
    } catch (e) {
        return false;
    }
};
///#source 1 1 /Scripts/mixerp/core/flag.js
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

///#source 1 1 /Scripts/mixerp/core/json.js
var tableToJSON = function (grid) {
    var colData = [];
    var rowData = [];

    var rows = grid.find("tr:not(:last-child)");

    rows.each(function () {
        var row = $(this);

        colData = [];

        row.find("td:not(:last-child)").each(function () {
            colData.push($(this).text());
        });

        rowData.push(colData);
    });

    var data = JSON.stringify(rowData);

    return data;
};
///#source 1 1 /Scripts/mixerp/core/localization.js
$(document).ready(function () {
    setCurrencyFormat();
    setNumberFormat();

    if (typeof Sys !== "undefined") {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Page_EndRequest);
    }
});

//Fired on ASP.net Ajax Postback
function Page_EndRequest() {
    setCurrencyFormat();
    setNumberFormat();

    if (typeof (AsyncListener) === "function") {
        AsyncListener();
    };
};

var setCurrencyFormat = function () {
    if (typeof currencyDecimalPlaces === "undefined" || typeof decimalSeparator === "undefined" || typeof thousandSeparator === "undefined") {
        return;
    };

    $('input.currency').number(true, currencyDecimalPlaces, decimalSeparator, thousandSeparator);
};

var setNumberFormat = function () {
    if (typeof decimalSeparator === "undefined" || typeof thousandSeparator === "undefined") {
        return;
    };

    $('input.decimal').number(true, currencyDecimalPlaces, decimalSeparator, thousandSeparator);
    $('input.integer').number(true, 0, decimalSeparator, thousandSeparator);
};

var parseFormattedNumber = function (input) {
    if (typeof window.thousandSeparator === "undefined") {
        window.thousandSeparator = ",";
    };

    if (typeof window.decimalSeparator === "undefined") {
        window.decimalSeparator = ".";
    };

    var result = input.split(thousandSeparator).join("");
    result = result.split(decimalSeparator).join(".");
    return result;
};

var getFormattedNumber = function (input, isInteger) {
    if (typeof window.currencyDecimalPlaces === "undefined") {
        window.currencyDecimalPlaces = 2;
    };

    if (typeof window.thousandSeparator === "undefined") {
        window.thousandSeparator = ",";
    };

    if (typeof window.decimalSeparator === "undefined") {
        window.decimalSeparator = ".";
    };

    var decimalPlaces = currencyDecimalPlaces;

    if (isInteger) {
        decimalPlaces = 0;
    };

    return $.number(input, decimalPlaces, decimalSeparator, thousandSeparator);
};

///#source 1 1 /Scripts/mixerp/core/location.js
function getQueryStringByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
};

//http://stackoverflow.com/questions/5999118/add-or-update-query-string-parameter
function updateQueryString(key, value, url) {
    if (!url) url = window.location.href;
    var re = new RegExp("([?&])" + key + "=.*?(&|#|$)(.*)", "gi"),
        hash;

    if (re.test(url)) {
        if (typeof value !== 'undefined' && value !== null)
            return url.replace(re, '$1' + key + "=" + value + '$2$3');
        else {
            hash = url.split('#');
            url = hash[0].replace(re, '$1$3').replace(/(&|\?)$/, '');
            if (typeof hash[1] !== 'undefined' && hash[1] !== null)
                url += '#' + hash[1];
            return url;
        }
    }
    else {
        if (typeof value !== 'undefined' && value !== null) {
            var separator = url.indexOf('?') !== -1 ? '&' : '?';
            hash = url.split('#');
            url = hash[0] + separator + key + '=' + value;
            if (typeof hash[1] !== 'undefined' && hash[1] !== null)
                url += '#' + hash[1];
            return url;
        }
        else
            return url;
    }
};
///#source 1 1 /Scripts/mixerp/core/mixerp-ajax.js
var appendParameter = function (data, parameter, value) {
    if (!isNullOrWhiteSpace(data)) {
        data += ",";
    };

    if (value === undefined) {
        value = "";
    };

    data += JSON.stringify(parameter) + ':' + JSON.stringify(value);

    return data;
};

var getData = function (data) {
    if (data) {
        return "{" + data + "}";
    };

    return null;
};

jQuery.fn.bindAjaxData = function (ajaxData, skipSelect, selectedValue, dataValueField, dataTextField) {
    "use strict";
    var selected;
    var targetControl = $(this);
    targetControl.empty();


    if (ajaxData.length === 0) {
        appendItem(targetControl, "", Resources.Titles.None());
        return;
    };

    if (!skipSelect) {
        appendItem(targetControl, "", Resources.Titles.Select());
    }
   
    if (!dataValueField) {
        dataValueField = "Value";
    };

    if (!dataTextField) {
        dataTextField = "Text";
    };

    $.each(ajaxData, function () {
        
        selected = false;

        if (selectedValue) {
            if (this[dataValueField] === selectedValue) {
                selected = true;
            };
        };
        appendItem(targetControl, this[dataValueField], this[dataTextField], selected);
    });
};

var appendItem = function (dropDownList, value, text, selected) {
    var option = $("<option></option>");
    option.val(value).html(text).trigger('change');

    if (selected) {
        option.prop("selected", true);
    };

    dropDownList.append(option);
};

var getAjax = function (url, data) {
    if (data) {
        return $.ajax({
            type: "POST",
            url: url,
            data: data,
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    return $.ajax({
        type: "POST",
        url: url,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
};

var ajaxUpdateVal = function (url, data, targetControls) {
    var ajax;

    if (data) {
        ajax = getAjax(url, data);
    } else {
        ajax = getAjax(url);
    };

    ajax.success(function (msg) {

        targetControls.each(function () {
            $(this).val(msg.d).trigger('change');
        });

        if (typeof ajaxUpdateValCallback == "function") {
            ajaxUpdateValCallback(targetControls);
        };
    });

    ajax.error(function (xhr) {
        logAjaxErrorMessage(xhr);
    });
};

var ajaxDataBind = function (url, targetControl, data, selectedValue, associatedControl, callback, dataValueField, dataTextField) {
   
    if (!targetControl) {
        return;
    };

    if (targetControl.length === 0) {
        return;
    };

    var ajax;

    if (data) {
        ajax = new getAjax(url, data);
    } else {
        ajax = new getAjax(url);
    };

    ajax.success(function (msg) {

        if (typeof callback === "function") {
            callback();
        };

        if (targetControl.length === 1) {
            targetControl.bindAjaxData(msg.d, false, selectedValue, dataValueField, dataTextField);
        };

        if (targetControl.length > 1) {
            targetControl.each(function () {
                $(this).bindAjaxData(msg.d, false, selectedValue, dataValueField, dataTextField);
            });
        };

        if (associatedControl && associatedControl.val) {
            associatedControl.val(selectedValue).trigger('change');
        };

        if (typeof ajaxDataBindCallBack === "function") {
            ajaxDataBindCallBack(targetControl);
        };
    });

    ajax.error(function (xhr) {
        if (typeof callback === "function") {
            callback();
        };

        var err = $.parseJSON(xhr.responseText);
        appendItem(targetControl, 0, err.Message);
    });
};

var getAjaxErrorMessage = function (xhr) {
    if (xhr) {
        var err;

        try {
            err = JSON.parse(xhr.responseText).Message;
        } catch (e) {
            err = xhr.responseText.Message;
        }

        if (err) {
            return err;
        };

        return xhr.responseText;
    }

    return "";
};
///#source 1 1 /Scripts/mixerp/core/transaction.js
function convertToDebit(balanceInCredit) {
    return balanceInCredit * -1;
};
///#source 1 1 /Scripts/mixerp/core/validation.js
var makeDirty = function (obj) {
    obj.parent().addClass("error");
    obj.focus();
};

var removeDirty = function (obj) {
    obj.parent().removeClass("error");
};

var isNullOrWhiteSpace = function (obj) {
    if ($.isArray(obj)) {
        return isArrayNullOrWhiteSpace(obj) || obj.length === 0;
    } else {
        return (!obj || $.trim(obj) === "");
    }
};

var isArrayNullOrWhiteSpace = function (obj) {
    var checkArray = [];
    if (obj.length > 0) {
        $.each(obj, function (index) {
            var val = obj[index];
            if (!val) {
                checkArray.push(val);
            }
        });
    }
    return checkArray.length > 0;
};

function isDate(val) {
    var d = new Date(val);
    return !isNaN(d.valueOf());
};
///#source 1 1 /Scripts/mixerp/core/window.js
var confirmAction = function () {
    return confirm(Resources.Questions.AreYouSure());
};
