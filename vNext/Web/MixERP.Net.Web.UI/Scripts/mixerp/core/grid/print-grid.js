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