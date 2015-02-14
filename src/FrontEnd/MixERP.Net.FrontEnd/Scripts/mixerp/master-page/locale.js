function urlExists(url) {
    var http = new XMLHttpRequest();
    http.open('HEAD', url, false);
    http.send();
    return http.status !== 404;
};


$(document).ready(function() {
    window.datepickerLanguagePath = window.jqueryUIi18nPath + '/datepicker-' + window.culture + '.js';

    if (!urlExists(window.datepickerLanguagePath)) {
        window.datepickerLanguagePath = window.jqueryUIi18nPath + '/datepicker-' + window.language + '.js';
    };

    if (!urlExists(window.datepickerLanguagePath)) {
        window.datepickerLanguagePath = "";
    };

    if (window.datepickerLanguagePath) {
        var picker = document.createElement('script');
        picker.type = 'text/javascript';
        picker.async = true;
        picker.src = window.datepickerLanguagePath;

        var element = document.body.getElementsByTagName('script')[0];
        element.parentNode.insertBefore(picker, element);
    };
});