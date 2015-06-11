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