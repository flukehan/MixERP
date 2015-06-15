var makeDirty = function (obj) {
    obj.parent().addClass("error");
    obj.focus();
};

var removeDirty = function (obj) {
    obj.parent().removeClass("error");
};

var isNullOrWhiteSpace = function (obj) {
    if ($.isArray(obj)) {
        return isArrayNullOrWhiteSpace(obj) || obj.length === 0;
    } else {
        return (!obj || $.trim(obj) === "");
    }
};

var isArrayNullOrWhiteSpace = function (obj) {
    var checkArray = [];
    if (obj.length > 0) {
        $.each(obj, function (index) {
            var val = obj[index];
            if (!val) {
                checkArray.push(val);
            }
        });
    }
    return checkArray.length > 0;
};

function isDate(val) {
    var d = new Date(val);
    return !isNaN(d.valueOf());
};