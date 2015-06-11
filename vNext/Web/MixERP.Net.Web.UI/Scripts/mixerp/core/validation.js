var makeDirty = function (obj) {
    obj.parent().addClass("error");
    obj.focus();
};

var removeDirty = function (obj) {
    obj.parent().removeClass("error");
};

var isNullOrWhiteSpace = function (obj) {
    return (!obj || $.trim(obj) === "");
};

function isDate(val) {
    var d = new Date(val);
    return !isNaN(d.valueOf());
};