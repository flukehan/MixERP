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
/* WINDOW BEGIN
    scrudAreYouSureLocalized
    scrudNothingSelectedLocalized
    requiredLocalize
    invalidNumberLocalized
    reportTemplatePath
    reportHeaderPath
    customFormUrl
    keyColumn
    decimalSeparator
    thousandSeparator
 */

var gridPanelId = "GridPanel";
var userIdHiddenId = "UserIdHidden";
var formGridViewId = "FormGridView";
var officeCodeHiddenId = "OfficeCodeHidden";
var titleLabelId = "TitleLabel";
var formPanelId = "FormPanel";
var cancelButtonId = "CancelButton";

/*WINDOW END*/


var scrudShowCompact = function () {
    var href = window.location.href;
    href = removeURLParameter(href, "show");
    href = removeURLParameter(href, "edit");
    href = removeURLParameter(href, "page");
    window.location = href;
};

var scrudShowAll = function () {
    var href = window.updateQueryString('show', 'all');
    href = removeURLParameter(href, "edit");
    href = removeURLParameter(href, "page");
    window.location = href;
};

var cancel = function () {
    var confirmed = confirm(window.scrudAreYouSureLocalized);

    if (confirmed) {
        var queryString = scrudGetQueryStringByName("edit");

        if (queryString) {
            window.location = removeURLParameter(window.location.href, "edit");
            return;
        };


        resetForm();
        $("#" + formPanelId).hide();
        $("#" + gridPanelId).show();

    };
};

var scrudEdit = function () {
    if (scrudConfirmAction()) {
        window.location = window.updateQueryString('edit', scrudGetSelectedRadioValue());
    };
};

//bobince from stackoverflow
// http://stackoverflow.com/questions/1634748/how-can-i-delete-a-query-string-parameter-in-javascript
function removeURLParameter(url, parameter) {
    //prefer to use l.search if you have a location/link object
    var urlparts = url.split('?');
    if (urlparts.length >= 2) {

        var prefix = encodeURIComponent(parameter) + '=';
        var pars = urlparts[1].split(/[&;]/g);

        //reverse iteration as may be destructive
        for (var i = pars.length; i-- > 0;) {
            //idiom for string.startsWith
            if (pars[i].lastIndexOf(prefix, 0) !== -1) {
                pars.splice(i, 1);
            };
        };

        url = urlparts[0] + '?' + pars.join('&');
        return url;
    } else {
        return url;
    };
};
var scrudConfirmAction = function () {
    var retVal = false;
    var selectedItemValue;

    var confirmed = confirm(window.scrudAreYouSureLocalized);

    if (confirmed) {
        selectedItemValue = scrudGetSelectedRadioValue();

        if (selectedItemValue == undefined) {
            alert(window.scrudNothingSelectedLocalized);
            retVal = false;
        } else {
            retVal = true;
            if (window.customFormUrl && window.keyColumn) {
                window.location = window.customFormUrl + "?" + window.keyColumn + "=" + selectedItemValue;
            }
        }
    }
    return retVal;
};

var scrudSelectAndClose = function () {
    var lastValueHidden = $("#LastValueHidden");
    lastValueHidden.val(scrudGetSelectedRadioValue());
    scrudSaveAndClose();
};

var scrudGetSelectedRadioValue = function () {
    return $('[id^="SelectRadio"]:checked').val();
};

var scrudSelectRadioById = function (id) {
    $('[id^="SelectRadio"]').prop("checked", false);
    $("#" + id).prop("checked", true);
};

var scrudPrintGridView = function () {
    var user = $("#" + userIdHiddenId).val();
    var office = $("#" + officeCodeHiddenId).val();
    var title = $("#" + titleLabelId).html();

    window.printGridView(window.reportTemplatePath, window.reportHeaderPath, title, formGridViewId, window.date, user, office, 'ScrudReport', 1, 0);
};


function windowFallback() {
    if (typeof window.scrudAreYouSureLocalized === "undefined") {
        window.scrudAreYouSureLocalized = "Do you really want to do this?";
    };

    if (typeof window.scrudNothingSelectedLocalized === "undefined") {
        window.scrudNothingSelectedLocalized = "Nothing was selected!";
    };

    if (typeof window.requiredLocalized === "undefined") {
        window.requiredLocalized = "This field should not be empty.";
    };

    if (typeof window.invalidNumberLocalized === "undefined") {
        window.invalidNumberLocalized = "This does not look like a number.";
    };

    if (typeof window.decimalSeparator === "undefined") {
        window.decimalSeparator = ".";
    };

    if (typeof window.thousandSeparator === "undefined") {
        window.thousandSeparator = ",";
    };
};

var scrudInitialize = function () {
    //Registering grid row click event to automatically select the radio.
    $('#' + formGridViewId + ' tr').click(function () {
        //Grid row was clicked. Now, searching the radio button.
        var radio = $(this).find('td input:radio');

        //The radio button was found.
        scrudSelectRadioById(radio.attr("id"));
    });

    scrudSaveAndClose();
    scrudLayout();
    scrudOnServerError();

    var queryString = scrudGetQueryStringByName("edit");

    if (queryString) {
        $("#" + gridPanelId).hide();
        $("#" + formPanelId).show();
    };


    var formPanel = $("#" + formPanelId);
    var required = formPanel.find("[required]");
    var vType = formPanel.find("[data-vtype]");


    required.blur(function () {
        validateRequired($(this));
    });

    vType.blur(function () {
        validateType($(this));
    });

    formPanel.on("reset", function () {
        $(".error-message").remove();
    });

};

function scrudOnServerError() {
    var errorField = $("#ScrudError");
    if (errorField.length) {
        displayForm();
    };
};

function scrudLayout() {
    var gridPanel = $("#GridPanel");
    var scrudUpdatePanel = $("#ScrudUpdatePanel");

    if (gridPanel && scrudUpdatePanel) {
        gridPanel.css("width", scrudUpdatePanel.width() + "px");
    };
};

function scrudGetQueryStringByName(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function getParent() {
    var parent;

    if (window.opener && window.opener.document) {
        parent = window.opener;
    }

    if (parent == undefined) {
        parent = window.parent;
    }

    return parent;
};

function scrudSaveAndClose() {
    var parent = getParent();

    if (parent) {
        var lastValue = window.parseFloat2($("#LastValueHidden").val());
        var ctl = scrudGetQueryStringByName('AssociatedControlId');
        var associatedControl = parent.$('#' + ctl);
        var callBackFunctionName = scrudGetQueryStringByName('CallBackFunctionName');

        if (lastValue > 0) {
            associatedControl.val(lastValue);

            if (callBackFunctionName) {
                parent[callBackFunctionName]();
            };

            closeWindow();
        }
    }
}

function closeWindow() {
    if (window.opener && window.opener.document) {
        window.close();
    } else {
        parent.jQuery.colorbox.close();
    };
};

function scrudDispalyLoading() {
    $("#" + formPanelId).find("div.segment").addClass("loading");
};
function scrudRemoveLoading() {
    $("#" + formPanelId).find("div.segment").removeClass("loading");
};


$(document).ready(function () {
    scrudInitialize();
    windowFallback();
    addShortcuts();
});

var scrudAddNew = function () {
    if (window.customFormUrl) {
        window.location = window.customFormUrl;
    }

    $('#' + formGridViewId + 'tr').find('td input:radio').prop('checked', false);
    resetForm();

    displayForm();

    scrudRepaint();

    if (typeof window.scrudAddNewCallBack === "function") {
        window.scrudAddNewCallBack();
    };
};

function resetForm() {
    $("#" + formPanelId).each(function () {
        this.reset();
    });
};

function displayForm() {
    $('#' + gridPanelId).hide(500);
    $('#' + formPanelId).show(500);

    if (typeof window.scrudFormDisplayedCallBack === "function") {
        window.scrudFormDisplayedCallBack();
    };
};

var scrudRepaint = function () {
    setTimeout(function () {
        $(document).trigger('resize');
    }, 1000);
};


function addShortcuts() {
    window.shortcut.add("ESC", function () {
        if ($('#' + formPanelId).is(':hidden')) {
            return;
        };

        if ($("#colorbox").css("display") === "block") {
            return;
        };

        if ($("body").attr("class") === "modal-open") {
            return;
        };

        var result = confirm(window.scrudAreYouSureLocalized);
        if (result) {
            $('#' + cancelButtonId).click();
        }
    });

    window.shortcut.add("RETURN", function () {
        scrudSelectAndClose();
    });

    window.shortcut.add("CTRL+SHIFT+C", function () {
        scrudShowCompact();
    });

    window.shortcut.add("CTRL+SHIFT+S", function () {
        scrudShowAll();
    });

    window.shortcut.add("CTRL+SHIFT+A", function () {
        return (scrudAddNew());
    });

    window.shortcut.add("CTRL+SHIFT+E", function () {
        $('#EditButtontop').click();
    });

    window.shortcut.add("CTRL+SHIFT+D", function () {
        $('#DeleteButtontop').click();
    });

    window.shortcut.add("CTRL+SHIFT+P", function () {
        scrudPrintGridView();
    });
};

function scrudValidate() {
    var formPanel = $("#" + formPanelId);
    var required = formPanel.find("[required]");
    var vType = formPanel.find("[data-vtype]");

    required.each(function () {
        validateRequired($(this));
    });

    vType.each(function () {
        validateType($(this));
    });

    if ($(".error-message").length) {
        return false;
    };

    return true;
};

function save() {
    var validation = scrudClientValidation();

    if (!validation) {
        return;
    };


    var id = scrudGetQueryStringByName("edit");

    if (id) {
        scrudUpdate(id);
        return;
    };

    scrudInsert();
};



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

function reloadPage() {
    var href = location.href;
    href = removeURLParameter(href, "edit");
    window.location = href;
};


function scrudDelete() {
    alert("Deleting");
};

function scrudClientValidation() {
    if (scrudValidate()) {
        scrudDispalyLoading();

        if (typeof window.scrudCustomValidator === "function") {
            var isValid = window.scrudCustomValidator();

            if (!isValid) {
                scrudRemoveLoading();
            };

            return isValid;
        };

        return true;
    };

    return false;
};

function validateRequired(el) {
    var parent = el.parent();
    var value = el.val();
    var validator = parent.find(".required");

    if (window.isNullOrWhiteSpace(value) && !el.is("[readonly]")) {
        if (!validator.length) {
            parent.append("<div class='required error-message'></div>");
            validator = parent.find(".required");
        };

        validator.html(window.requiredLocalized);
        window.makeDirty(el);
        return;
    };

    window.removeDirty(el);
    validator.remove();
};

function validateType(el) {
    var parent = el.parent();
    var type = el.data("vtype");
    var required = el.is("[required]");
    var validator = parent.find(".vtype");
    var value = el.val();
    var invalid = false;

    if (!required & el.val() === "") {
        return;
    }

    var message = window.invalidNumberLocalized;

    switch (type) {
        case "date":
            break;
        case "int":
        case "int-strict2":
        case "dec":
        case "dec-strict2":

            if (containsIllegalCharacters(value)) {
                invalid = true;
            }
            else {
                if (type.substr(-7, 7) === "strict2") {
                    if (window.parseFloat2(value) < 0) {
                        invalid = true;
                    }
                }
            }
            break;
        case "int-strict":
        case "dec-strict":
            if (containsIllegalCharacters(value)) {
                invalid = true;
            }
            else {
                if (window.parseFloat2(value) <= 0) {
                    invalid = true;
                }
            }
            break;
    };

    if (invalid) {
        if (!validator.length) {
            parent.append("<div class='vtype error-message'></div>");
            validator = parent.find(".vtype");
        }

        // ReSharper disable once UsageOfPossiblyUnassignedValue
        validator.html(message);
        window.makeDirty(el);
        return;
    }

    window.removeDirty(el);
    validator.remove();
};

function containsIllegalCharacters(value) {
    if (value.split(window.decimalSeparator).length > 2) {
        return true;
    };

    value = value.split(window.thousandSeparator).join("");
    value = value.split(window.decimalSeparator).join("");

    var regex = new RegExp('^[0-9]+$');

    var match = value.replace(regex, "");

    if (!match.length) {
        return false;
    };

    return true;
};


function getFields() {
    var fields = [];

    $("[data-scrud]").each(function () {
        var el = $(this);
        var scrud = el.data("scrud");
        var columnName = $(this).attr("id");
        var value = $(this).val();

        if (scrud === "radio") {
            value = el.find("input[type=radio]:checked").val() === "yes";
        };

        if (!el.is("[readonly]")) {
            var field = new Object();
            field.Key = columnName;
            field.Value = value;

            fields.push(field);
        };
    });

    return fields;
};