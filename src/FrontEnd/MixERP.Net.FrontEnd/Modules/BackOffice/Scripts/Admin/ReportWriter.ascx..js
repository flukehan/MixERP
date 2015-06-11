var activeElement;
var dataSources = [];
var gridViews = [];
var types = ["Number", "Text", "Date"];
var parameterNameLocalized = "Parameter Name";
var parameterTypeLocalized = "Parameter Type";
var testValueLocalized = "Test Value";
var removeThisLocalized = "Remove This";
var addAnotherLocalized = "Add Another";
var totalsLocalized = "Totals";
var dataSourceIndexSelect = $("#DataSourceIndexSelect");
var saveGridViewButton = $("#SaveGridViewButton");
var addGridViewCheckBox = $("#AddGridViewCheckBox");
var gridViewCssClassInputText = $("#GridViewCssClassInputText");
var gridViewStyleInputText = $("#GridViewStyleInputText");
var addFieldButton = $("#AddFieldButton");
var fieldDataSourceIndexSelect = $("#FieldDataSourceIndexSelect");
var fieldNameInputText = $("#FieldNameInputText");
var addResourceButton = $("#AddResourceButton");
var resourceClassNameInputText = $("#ResourceClassNameInputText");
var resourceNameInputText = $("#ResourceNameInputText");
var saveButton = $("#SaveButton");
var titleInputText = $("#TitleInputText");
var fileNameInputText = $("#FileNameInputText");
var topSectionTextArea = $("#TopSectionTextArea");
var bodyTextArea = $("#BodyTextArea");
var bottomSectionTextArea = $("#BottomSectionTextArea");
var menuCodeInputText = $("#MenuCodeInputText");
var parentMenuCodeInputText = $("#ParentMenuCodeInputText");

saveButton.click(function() {
    if (!confirm("Are you sure?")) {
        return;
    };

    var title = titleInputText.val();
    var fileName = fileNameInputText.val();
    var menuCode = menuCodeInputText.val();
    var parentMenuCode = parentMenuCodeInputText.val();
    var topSection = topSectionTextArea.val();
    var body = bodyTextArea.val();
    var bottomSection = bottomSectionTextArea.val();


    var ajaxSaveReport = saveReport(title, fileName, menuCode, parentMenuCode, topSection, body, bottomSection, dataSources, gridViews);

    ajaxSaveReport.success(function(msg) {
        alert(msg.d);
    });

    ajaxSaveReport.fail(function(xhr) {
        alert(xhr.responseText);
    });

});


addResourceButton.click(function() {
    var resourceClass = resourceClassNameInputText.val();
    var resourceName = resourceNameInputText.val();

    var resource = "{Resources." + resourceClass + "." + resourceName + "}";

    insertAtCursor(activeElement, resource);
    $("#ResourceModal").modal("hide");
});

addFieldButton.click(function() {
    var index = parseInt2(fieldDataSourceIndexSelect.getSelectedValue());
    var fieldName = fieldNameInputText.val();

    var field = "{DataSource[" + index + "]." + fieldName + "}";

    insertAtCursor(activeElement, field);
    $("#FieldModal").modal("hide");
});

saveGridViewButton.click(function() {
    var gridView = new Object();

    var index = parseInt2(dataSourceIndexSelect.getSelectedValue());
    var addGridView = addGridViewCheckBox.is(":checked");
    var cssClass = gridViewCssClassInputText.val();
    var style = gridViewStyleInputText.val();

    for (var i = 0; i < gridViews.length; i++) {
        if (gridViews[i].DataSourceIndex === index) {
            gridViews.splice(i, 1);
        };
    };

    if (addGridView) {
        gridView.DataSourceIndex = index;
        gridView.CssClass = cssClass;
        gridView.Style = style;
        gridViews.push(gridView);
        return;
    };

    if (cssClass || style) {
        alert("Cannot set styling information for the current data source.");
        return;
    };
});

dataSourceIndexSelect.blur(function() {
    var index = parseInt2(dataSourceIndexSelect.getSelectedValue());

    addGridViewCheckBox.removeProp("checked");
    gridViewCssClassInputText.val("");
    gridViewStyleInputText.val("");


    for (var i = 0; i < gridViews.length; i++) {
        if (gridViews[i].DataSourceIndex === index) {
            addGridViewCheckBox.prop("checked", "checked");
            gridViewCssClassInputText.val(gridViews[i].CssClass);
            gridViewStyleInputText.val(gridViews[i].Style);
            return;
        };
    };
});

$(document).ready(function() {
    addParameter();
    var height = $(window).height() - 250;
    $(".attached.tab.segment").css("height", height + "px");
});

function addDataSource() {
    saveDataSource();
    var total = getTotalDataSourceCounter();
    $(".datasource.label .current").html(total + 1);
    $(".datasource.label .total").html(total + 1);
    loadDataSource(total);
};


function updateDataSourceIndices() {
    var total = getTotalDataSourceCounter();
    var items = "";

    for (var i = 0; i < total; i++) {
        items += "<option value='" + i + "'> DataSource #" + i + "</option>";
    };

    fieldDataSourceIndexSelect.html(items);
    dataSourceIndexSelect.html(items);
};


function testDataSource() {
    saveDataSource();
    var dataSource = getDataSource();

    var ajaxGetTable = getTable(dataSource.Query, dataSource.Parameters);

    ajaxGetTable.success(function(msg) {
        $("#data-source").html(getCurrentDataSourceCounter());
        var result = eval(msg.d);
        var runningTotalFieldIndices = $(".running-total-fields input").val();
        var runningTotalTextIndex = $(".running-total-text input").val();

        appendTable(result, $("#GridPanel"), runningTotalFieldIndices, runningTotalTextIndex);
        $("#PreviewModal").modal("show");
    });

    ajaxGetTable.fail(function(xhr) {
        alert(xhr.responseText);
    });
};

function appendTable(object, target, runningTotalFieldIndices, runningTotalTextIndex) {
    var firstRow = object[0];
    var fields = runningTotalFieldIndices.split(" ").join("").split(",");
    var columns = 0;

    var table = $("<table />");
    var tableHeader = $("<thead/>");
    var headerRow = $("<tr/>");
    var tableBody = $("<tbody/>");
    var tableFooter = $("<tfoot/>");
    var footerRow = $("<tr/>");

    for (var headerColumn in firstRow) {
        if (firstRow.hasOwnProperty(headerColumn)) {
            var headerCell = $("<th/>");
            headerCell.html(headerColumn);
            headerRow.append(headerCell);
        };

        columns++;
    };

    tableHeader.append(headerRow);

    table.append($(tableHeader));

    $.each(object, function(index, value) {
        var newRow = $("<tr/>");

        $.each(value, function(key, val) {
            var column = $("<td/>");
            column.html(val);
            newRow.append(column);
        });

        $(tableBody).append(newRow);
    });

    table.append(tableBody);

    if (runningTotalTextIndex) {
        var runningTotalCell = $("<td/>");
        runningTotalCell.html(totalsLocalized);
        runningTotalCell.prop("colspan", parseInt2(runningTotalTextIndex) + 1);
        runningTotalCell.addClass("text-right");

        footerRow.append(runningTotalCell);

        for (var i = parseInt2(runningTotalTextIndex) + 1; i < columns; ++i) {
            var footerCell = $("<td/>");

            if (fields.indexOf(i.toString()) >= 0) {
                var sum = sumOfColumn(table, i);
                footerCell.html(sum);
            };

            footerRow.append(footerCell);
        };

        tableFooter.append(footerRow);
        table.append(tableFooter);
    };

    table.addClass("ui nowrap striped celled compact table");
    target.html("").append(table);
};

function saveDataSource() {
    var currentIndex = getCurrentDataSourceCounter();
    dataSources[currentIndex - 1] = getDataSource();
    updateDataSourceIndices();
};

function getDataSource() {
    var dataSource = new Object();
    dataSource.Query = $("#QueryTextArea").val();
    dataSource.RunningTotalFieldIndices = $(".running-total-fields input").val();
    dataSource.RunningTotalTextColumnIndex = $(".running-total-text input").val();
    dataSource.Parameters = getParameters();

    return dataSource;
};

function showPreviousDataSource() {
    saveDataSource();
    var currentIndex = getCurrentDataSourceCounter() - 1;

    currentIndex--;

    if (currentIndex < 0) {
        currentIndex = 0;
    };

    loadDataSource(currentIndex);
};

function showNextDataSource() {
    saveDataSource();
    var total = getTotalDataSourceCounter();
    var currentIndex = getCurrentDataSourceCounter() - 1;

    currentIndex++;

    if (currentIndex > total - 1) {
        currentIndex = total - 1;
    };

    loadDataSource(currentIndex);
};

function loadDataSource(index) {
    var dataSource = dataSources[index];
    $("#QueryTextArea").val("");
    $(".running-total-fields input").val("");
    $(".running-total-text input").val("");
    $("#Parameters").html("");

    if (dataSource) {
        setCurrentDataSourceCounter(index + 1);
        $("#QueryTextArea").val(dataSource.Query);
        $(".running-total-fields input").val(dataSource.RunningTotalFieldIndices);
        $(".running-total-text input").val(dataSource.RunningTotalTextColumnIndex);

        $("#Parameters").html("");

        if (dataSource.Parameters) {
            for (j = 0; j < dataSource.Parameters.length; j++) {
                addParameter(dataSource.Parameters[j].Name, dataSource.Parameters[j].Type, dataSource.Parameters[j].TestValue);
            };
        };
    } else {
        addParameter();
    };
};

function getCurrentDataSourceCounter() {
    return parseInt2($(".datasource.label .current").html());
};

function setCurrentDataSourceCounter(val) {
    $(".datasource.label .current").html(val);
};

function getTotalDataSourceCounter() {
    return parseInt2($(".datasource.label .total").html());
};

function setTotalDataSourceCounter(value) {
    return $(".datasource.label .total").html(parseInt2(value));
};

function addParameter(name, type, testVal) {
    var targetContainer = $("#Parameters");
    var parameterName = "<div class=\"name field\"><label>" + parameterNameLocalized + "</label><input type=\"text\"";

    if (name) {
        parameterName += " value='" + name + "' ";
    };

    parameterName += " /></div>";

    var parameterType = "<div class=\"field\"><label>" + parameterTypeLocalized + "</label><select>";

    for (i = 0; i < types.length; i++) {
        parameterType += "<option";

        if (type === types[i]) {
            parameterType += " selected ";
        };

        parameterType += ">";
        parameterType += types[i];
        parameterType += "</option>";
    }

    parameterType += "</select></div>";

    var testValue = "<div class=\"test field\"><label>" + testValueLocalized + "</label><input type=\"text\"";

    if (testVal) {
        testValue += " value='" + testVal + "' ";
    };

    testValue += " /></div>";

    var buttons = "<div class=\"field\">" +
        "<label>&nbsp;</label>" +
        "<button class=\"ui button\" onclick='removeParameter($(this));'><i class=\"minus circle icon\"></i>" + removeThisLocalized + "</button>" +
        "<button class=\"ui button\" onclick=\"addParameter()\"><i class=\"plus circle icon\"></i>" + addAnotherLocalized + "</button>" +
        "</div>";

    var div = "<div class=\"four parameter fields\">";
    div += parameterName;
    div += parameterType;
    div += testValue;
    div += buttons;
    div += "</div>";
    targetContainer.append(div);
};

function removeParameter(element) {
    var count = $(".parameter.fields").length;

    if (count > 1) {
        element.parent().parent().remove();
    };
};

$("form").submit(function() {
    return false;
});

$("textarea").keydown(function(e) {
    var keyCode = e.keyCode || e.which;

    if (keyCode === 9) {
        e.preventDefault();
        var start = this.selectionStart;
        var end = this.selectionEnd;

        // set textarea value to: text before caret + tab + text after caret
        spaces = "    ";
        this.value = this.value.substring(0, start) + spaces + this.value.substring(end);

        // put caret at right position again
        this.selectionStart = this.selectionEnd = start + spaces.length;
    }
});


function getParameters() {
    var parameters = [];

    $(".parameter.fields").each(function() {
        var name = $(this).find(".name").find("input").val();
        var type = $(this).find("select").getSelectedText();
        var testValue = $(this).find(".test").find("input").val();

        var parameter = getParameter(name, type, testValue);
        parameters.push(parameter);
    });

    return parameters;
};

function getParameter(name, type, testValue) {
    var parameter = new Object();
    parameter.Name = name;
    parameter.Type = type;
    parameter.TestValue = testValue;

    return parameter;
};

function insertTable() {
    var table = "<table>\n" +
        "   <tr>\n" +
        "       <td>\n" +
        "               \n" +
        "       </td>\n" +
        "       <td>\n" +
        "               \n" +
        "       </td>\n" +
        "   </tr>\n" +
        "   <tr>\n" +
        "       <td>\n" +
        "               \n" +
        "       </td>\n" +
        "       <td>\n" +
        "               \n" +
        "       </td>\n" +
        "   </tr>\n" +
        "</table>\n";

    insertAtCursor(activeElement, table);
};

function setActiveElement(el) {
    activeElement = el;
};

function insertAtCursor(myField, myValue) {
    debugger;
    if (document.selection) {
        myField.focus();
        sel = document.selection.createRange();
        sel.text = myValue;
    }
    //MOZILLA and others
    else if (myField.selectionStart || myField.selectionStart === "0") {
        var startPos = myField.selectionStart;
        var endPos = myField.selectionEnd;
        myField.value = myField.value.substring(0, startPos)
            + myValue
            + myField.value.substring(endPos, myField.value.length);
    } else {
        myField.value += myValue;
    };
};

function showReports() {
    var ajaxGetReports = getReports();

    ajaxGetReports.success(function(msg) {
        createRows(msg.d);
        $("#ReportModal").modal("show");
    });

    ajaxGetReports.fail(function(xhr) {
        alert(xhr.responseText);
    });
};

function createRows(collection) {
    var body = $("#ReportModal table tbody");
    body.html("");

    for (var i = 0; i < collection.length; i++) {
        var row = $("<tr/>");

        var nameCell = $("<td/>");
        var createdOnCell = $("<td/>");
        var lastAccessOnCell = $("<td/>");
        var lastWrittenOnCell = $("<td/>");

        var nameAnchor = $("<a/>");
        nameAnchor.html(collection[i].FileName);
        nameAnchor.attr("onclick", "openReport(this);");


        nameCell.append(nameAnchor).appendTo(row);
        createdOnCell.html(collection[i].CreatedOn).appendTo(row);
        lastAccessOnCell.html(collection[i].LastAccessedOn).appendTo(row);
        lastWrittenOnCell.html(collection[i].LastWrittenOn).appendTo(row);

        body.append(row);
    };
};

function openReport(el) {
    var path = $(el).html();

    if (!path) {
        return;
    };

    var ajaxGetDefinition = getDefinition(path);

    ajaxGetDefinition.success(function(msg) {
        loadDefinition(msg.d);
        $("#ReportModal").modal("hide");
    });

    ajaxGetDefinition.fail(function(xhr) {
        alert(xhr.responseText);
    });
};

function loadDefinition(definition) {
    titleInputText.val(definition.Title);
    fileNameInputText.val(definition.FileName);

    if (definition.MenuCode) {
        menuCodeInputText.val(definition.MenuCode);
    };

    if (definition.ParentMenuCode) {
        parentMenuCodeInputText.val(definition.ParentMenuCode);
    };

    if (definition.TopSection) {
        topSectionTextArea.val(definition.TopSection);
    };

    if (definition.Body) {
        bodyTextArea.val(definition.Body);
    };


    if (definition.BottomSection) {
        bottomSectionTextArea.val(definition.BottomSection);
    };

    if (definition.DataSources) {
        dataSources = definition.DataSources;
        var total = definition.DataSources.length;
        setTotalDataSourceCounter(total.toString());
        loadDataSource(0);
    };

    if (definition.GridViews) {
        gridViews = definition.GridViews;
    };
};

function getReports() {
    var url = "/Modules/BackOffice/Services/Admin/ReportWriter.asmx/GetReports";
    return getAjax(url);
};

function getDefinition(fileName) {
    var url = "/Modules/BackOffice/Services/Admin/ReportWriter.asmx/GetDefinition";
    var data = appendParameter("", "fileName", fileName);
    data = getData(data);

    return getAjax(url, data);
};


function saveReport(title, fileName, menuCode, parentMenuCode, topSection, body, bottomSection, dataSources, gridViews) {
    var url = "/Modules/BackOffice/Services/Admin/ReportWriter.asmx/SaveReport";
    var data = appendParameter("", "title", title);
    data = appendParameter(data, "fileName", fileName);
    data = appendParameter(data, "menuCode", menuCode);
    data = appendParameter(data, "parentMenuCode", parentMenuCode);
    data = appendParameter(data, "topSection", topSection);
    data = appendParameter(data, "body", body);
    data = appendParameter(data, "bottomSection", bottomSection);
    data = appendParameter(data, "dataSources", JSON.stringify(dataSources));
    data = appendParameter(data, "gridViews", JSON.stringify(gridViews));

    data = getData(data);

    return getAjax(url, data);
};

function getTable(sql, parameters) {
    var url = "/Modules/BackOffice/Services/Admin/ReportWriter.asmx/GetTable";
    var data = appendParameter("", "sql", sql);
    data = appendParameter(data, "parameters", JSON.stringify(parameters));

    data = getData(data);

    return getAjax(url, data);
};
