var cancel = function () {
    var confirmed = confirm(window.scrudAreYouSureLocalized);

    if (confirmed) {
        var queryString = scrudGetQueryStringByName("edit");

        if (queryString) {
            window.location = removeURLParameter(window.location.href, "edit");
            return;
        };


        resetForm();
        $("#" + formPanelId).hide();
        $("#" + gridPanelId).show();

    };
};