var toogleSelection = function (element) {
    var property = element.prop("checked");

    if (property) {
        element.prop("checked", false);
    } else {
        element.prop("checked", true);
    }
};