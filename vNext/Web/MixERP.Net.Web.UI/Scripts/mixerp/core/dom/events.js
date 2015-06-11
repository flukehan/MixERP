var triggerChange = function (controlId) {
    var element = document.getElementById(controlId);

    if ('createEvent' in document) {
        var evt = document.createEvent("HTMLEvents");
        evt.initEvent("change", false, true);
        element.dispatchEvent(evt);
    } else {
        if ("fireEvent" in element)
            element.fireEvent("onchange");
    }
};

var triggerClick = function (controlId) {
    var element = document.getElementById(controlId);

    if ('createEvent' in document) {
        var evt = document.createEvent("HTMLEvents");
        evt.initEvent("click", false, true);
        element.dispatchEvent(evt);
    } else {
        if ("fireEvent" in element)
            element.fireEvent("onclick");
    }
};
