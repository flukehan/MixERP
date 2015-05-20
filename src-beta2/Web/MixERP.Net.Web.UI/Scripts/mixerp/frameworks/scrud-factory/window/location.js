function scrudGetQueryStringByName(name) {
    return window.getQueryStringByName(name);
};

function reloadPage() {
    var href = location.href;
    href = window.removeURLParameter(href, "edit");
    window.location = href;
};