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

function prepareChart(datasourceId, canvasId, legendId, type, log) {
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