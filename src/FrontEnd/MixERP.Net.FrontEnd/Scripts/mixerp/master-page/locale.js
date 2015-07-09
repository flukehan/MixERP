function urlExists(url) {
    var http = new XMLHttpRequest();
    http.open('HEAD', url, false);
    http.send();
    return http.status !== 404;
};


$(document).ready(function () {
    loadDatePickerLocale();
});

function loadDatePickerLocale() {
    window.datepickerLanguagePath = window.jqueryUIi18nPath + '/datepicker-' + window.culture + '.js';

    if (!urlExists(window.datepickerLanguagePath)) {
        window.datepickerLanguagePath = window.jqueryUIi18nPath + '/datepicker-' + window.language + '.js';
    };

    if (!urlExists(window.datepickerLanguagePath)) {
        window.datepickerLanguagePath = "";
    };

    if (window.datepickerLanguagePath) {
        addScriptReference(window.datepickerLanguagePath);
    };
};

function addScriptReference(path) {
    var script = document.createElement('script');
    script.type = 'text/javascript';
    script.async = true;
    script.src = path;

    var element = document.body.getElementsByTagName('script')[0];
    element.parentNode.insertBefore(script, element);
};