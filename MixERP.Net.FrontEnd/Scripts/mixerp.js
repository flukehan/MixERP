function getDocHeight() {
    var D = document;
    return Math.max(
                Math.max(D.body.scrollHeight, D.documentElement.scrollHeight),
                Math.max(D.body.offsetHeight, D.documentElement.offsetHeight),
                Math.max(D.body.clientHeight, D.documentElement.clientHeight)
            );

};

var selectDropDownListByValue = function(textBoxId, dropDownListId) {
    var listControl = $("#" + dropDownListId);
    var textBox = $("#" + textBoxId);
    var selectedValue = textBox.val();
    var exists;

    if (listControl.length) {
        listControl.find('option').each(function() {
            if (this.value == selectedValue) {
                exists = true;
            }
        });
    }

    if (exists) {
        listControl.val(selectedValue).trigger('change');
    } else {
        textBox.val('');
    }

    triggerChange(dropDownListId);
};

var triggerChange = function(controlId) {
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

var parseFloat2 = function(arg) {
    return parseFloat(arg || 0);
};

var confirmAction = function() {
    return confirm(localizedAreYouSure);
};


/******************************************************************************************************
DATE EXPRESSION START
******************************************************************************************************/

var validateByControlId = function(controlId) {
    if (typeof Page_ClientValidate === "function") {
        Page_ClientValidate(controlId);
    } else {
        console.log("The function Page_ClientValidate was not found.");
    }
};

$(document).ready(function () {
    $(".date").blur(function () {
        if (today == "") return;
        var control = $(this);
        var value = control.val().trim().toLowerCase();
        var result;
        var number;

        if (value == "d") {
            result = dateAdd(today, "d", 0);
            control.val(result);
            validateByControlId(control.attr("id"));
            return;
        }

        if (value == "m" || value == "+m") {
            control.val(dateAdd(today, "m", 1));
            validateByControlId(control.attr("id"));
            return;
        }

        if (value == "w" || value == "+w") {
            control.val(dateAdd(today, "d", 7));
            validateByControlId(control.attr("id"));
            return;
        }

        if (value == "y" || value == "+y") {
            control.val(dateAdd(today, "y", 1));
            validateByControlId(control.attr("id"));
            return;
        }

        if (value == "-d") {
            control.val(dateAdd(today, "d", -1));
            validateByControlId(control.attr("id"));
            return;
        }

        if (value == "+d") {
            control.val(dateAdd(today, "d", 1));
            validateByControlId(control.attr("id"));
            return;
        }


        if (value == "-w") {
            control.val(dateAdd(today, "d", -7));
            validateByControlId(control.attr("id"));
            return;
        }

        if (value == "-m") {
            control.val(dateAdd(today, "m", -1));
            validateByControlId(control.attr("id"));
            return;
        }

        if (value == "-y") {
            control.val(dateAdd(today, "y", -1));
            validateByControlId(control.attr("id"));
            return;
        }

        if (value.indexOf("d") >= 0) {
            number = parseInt(value.replace("d"));
            control.val(dateAdd(today, "d", number));
            validateByControlId(control.attr("id"));
            return;
        }

        if (value.indexOf("w") >= 0) {
            number = parseInt(value.replace("w"));
            control.val(dateAdd(today, "d", number * 7));
            validateByControlId(control.attr("id"));
            return;
        }

        if (value.indexOf("m") >= 0) {
            number = parseInt(value.replace("m"));
            control.val(dateAdd(today, "m", number));
            validateByControlId(control.attr("id"));
            return;
        }

        if (value.indexOf("y") >= 0) {
            number = parseInt(value.replace("y"));
            control.val(dateAdd(today, "y", number));
            validateByControlId(control.attr("id"));
            return;
        }
    });
});

function dateAdd(dt, expression, number) {
    var d = Date.parseExact(dt, shortDateFormat);
    var ret = new Date();

    if (expression == "d") {
        ret = new Date(d.getFullYear(), d.getMonth(), d.getDate() + parseInt(number));
    }

    if (expression == "m") {
        ret = new Date(d.getFullYear(), d.getMonth() + parseInt(number), d.getDate());
    }

    if (expression == "y") {
        ret = new Date(d.getFullYear() + parseInt(number), d.getMonth(), d.getDate());
    }

    return ret.toString(shortDateFormat);
};

/******************************************************************************************************
DATE EXPRESSION END
******************************************************************************************************/

var showWindow = function(url) {
    $.colorbox({ width: +$('html').width() * 0.7, height: +$('html').height() * 0.7, iframe: true, href: url });
};


$(document).ready(function () {
    setNumberFormat();

    if (!(typeof Sys === "undefined")) {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Page_EndRequest);
    }
});

function Page_EndRequest() {
    setNumberFormat();
}

var setNumberFormat = function() {
    $('input.number').number(true, decimalPlaces, decimalSeparator, thousandSeparator);
};




/******************************************************************************************************
Chart BEGIN
******************************************************************************************************/
var chartColors = ["#3366CC", "#DC3912", "#109618", "#FF9900", "#990099", "#0099C6", "#DD4477", "#66AA00", "#B82E2E", "#316395", "#994499", "#AAAA11", "#E67300", "#8B0707", "#3B3EAC", "#B77322", "#16D620"];

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

function prepareChart(datasourceId, canvasId, legendId, type) {
    var table = $("#" + datasourceId);
    var labels = [];
    var data = [];
    var datasets = [];
    var title;
    var index = 0;

    //Loop through the table header for labels.
    table.find("thead th").each(function () {

        //Ignore the first column of the header
        if (index > 0) {
            //Create labels from header row columns.
            labels.push($(this).html());
        }

        index++;
    });

    //Reset the counter.
    index = 0;

    //Loop through each row of the table body.
    table.find("tbody tr").each(function () {

        //Get an instance of the current row
        var row = $(this);

        //The first column of each row is the legend.
        title = row.find(">:first-child").html();

        //Reset the data object's value from the previous iteration.
        data = [];
        //Loop through the row columns.
        row.find("td").each(function () {
            //Get data from this row.
            data.push($(this).html());
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

        index++;
    });


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

function preparePieChart(datasourceId, canvasId, legendId, type) {
    var table = $("#" + datasourceId);
    var value;
    var data = [];

    //Reset the counter.
    index = 0;

    //Loop through each row of the table body.
    table.find("tbody tr").each(function () {

        //Get an instance of the current row
        var row = $(this);

        //The first column of each row is the legend.
        title = row.find(">:first-child").html();

        //The first column of each row is the legend.
        value = parseInt(row.find("td").html());


        var dataset = {
            value: value,
            color: chartColors[index],
            title: title
        };

        //Add the dataset object to the array object.
        data.push(dataset);
        index++;
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
    table.hide();
};

/******************************************************************************************************
Chart END
******************************************************************************************************/

var parseFormattedNumber = function(input) {
    var result = input.replace(thousandSeparator, "");
    result = result.replace(decimalSeparator, ".");
    return result;
};

var getFormattedNumber = function(input) {
    var result = input.replace(".", decimalSeparator);
    return result;
};