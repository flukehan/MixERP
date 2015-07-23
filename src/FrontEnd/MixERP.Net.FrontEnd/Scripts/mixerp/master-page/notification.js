$(document).ready(function() {
    if (update === "1") {
        addNotification(Resources.Titles.Update(), "document.location = \"/Modules/Update.aspx\";");
    };

    if (firstStepsPending.toUpperCase().substring(0, 1) === "T") {
        addNotification(Resources.Titles.FirstSteps(), "document.location = \"/Modules/FirstSteps.aspx\";");
    };

});