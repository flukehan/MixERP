var scrudInitialize = function () {
    //Registering grid row click event to automatically select the radio.
    $('#' + formGridViewId + ' tr').click(function () {
        //Grid row was clicked. Now, searching the radio button.
        var radio = $(this).find('td input:radio');

        //The radio button was found.
        scrudSelectRadioById(radio.attr("id"));
    });

    scrudSaveAndClose();
    scrudLayout();
    scrudOnServerError();

    var queryString = scrudGetQueryStringByName("edit");

    if (queryString) {
        $("#" + gridPanelId).hide();
        $("#" + formPanelId).show();
    };


    var formPanel = $("#" + formPanelId);
    var required = formPanel.find("[required]");
    var vType = formPanel.find("[data-vtype]");


    required.blur(function () {
        validateRequired($(this));
    });

    vType.blur(function () {
        validateType($(this));
    });

    formPanel.on("reset", function () {
        $(".error-message").remove();
    });

};