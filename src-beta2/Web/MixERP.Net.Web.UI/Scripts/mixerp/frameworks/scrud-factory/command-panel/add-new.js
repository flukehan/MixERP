var scrudAddNew = function () {
    if (window.customFormUrl) {
        window.location = window.customFormUrl;
    }

    $('#' + formGridViewId + 'tr').find('td input:radio').prop('checked', false);
    resetForm();

    displayForm();

    scrudRepaint();

    if (typeof window.scrudAddNewCallBack === "function") {
        window.scrudAddNewCallBack();
    };
};