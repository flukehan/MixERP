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