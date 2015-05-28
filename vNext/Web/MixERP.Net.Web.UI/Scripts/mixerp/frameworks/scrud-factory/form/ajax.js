function scrudInsert() {
    var url = window.location.pathname + "/add";
    var fields = getFields();

    var data = window.appendParameter("", "fields", fields);

    data = getData(data);

    $.ajax({
        url: url,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: data,
        success: function () {
            reloadPage();
        },
        error: function (xhr) {
            alert(xhr.responseText);
        }
    });
};

function scrudUpdate(id) {
    var url = window.location.pathname + "/update";
    var fields = getFields();

    var data = window.appendParameter("", "id", id);
    data = window.appendParameter(data, "fields", fields);

    data = getData(data);

    $.ajax({
        url: url,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: data,
        success: function () {
            reloadPage();
        },
        error: function (xhr) {
            alert(xhr.responseText);
        }
    });
};