var scrudShowCompact = function () {
    var href = window.location.href;
    href = window.removeURLParameter(href, "show");
    href = window.removeURLParameter(href, "edit");
    href = window.removeURLParameter(href, "page");
    window.location = href;
};

var scrudShowAll = function () {
    var href = window.updateQueryString('show', 'all');
    href = window.removeURLParameter(href, "edit");
    href = window.removeURLParameter(href, "page");
    window.location = href;
};

var scrudEdit = function () {
    if (scrudConfirmAction()) {
        window.location = window.updateQueryString('edit', scrudGetSelectedRadioValue());
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
};

function closeWindow() {
    if (window.opener && window.opener.document) {
        window.close();
    } else {
        parent.jQuery.colorbox.close();
    };
};
