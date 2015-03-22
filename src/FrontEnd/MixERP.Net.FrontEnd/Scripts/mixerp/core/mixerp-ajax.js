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

jQuery.fn.bindAjaxData = function (ajaxData, skipSelect, selectedValue) {
    "use strict";
    var selected;
    var targetControl = $(this);
    targetControl.empty();

    if (ajaxData.length === 0) {
        appendItem(targetControl, "", window.noneLocalized);
        return;
    };

    if (!skipSelect) {
        appendItem(targetControl, "", window.selectLocalized);
    }

    $.each(ajaxData, function () {
        selected = false;

        if (selectedValue) {
            if (this.Value === selectedValue) {
                selected = true;
            };
        };

        appendItem(targetControl, this.Value, this.Text, selected);
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

var ajaxDataBind = function (url, targetControl, data, selectedValue, associatedControl, callback) {
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
            targetControl.bindAjaxData(msg.d, false, selectedValue);
        };

        if (targetControl.length > 1) {
            targetControl.each(function () {
                $(this).bindAjaxData(msg.d, false, selectedValue);
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
        var err = JSON.parse(xhr.responseText).Message;
        if (err) {
            return err;
        };

        return xhr.responseText;
    }

    return "";
};