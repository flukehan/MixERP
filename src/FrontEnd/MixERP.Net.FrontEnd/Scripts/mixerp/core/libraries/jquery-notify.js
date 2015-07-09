function displayMessage(a, b) {
    $.notify(a, b);
};

function displaySucess() {
    $.notify(Resources.Labels.TaskCompletedSuccessfully(), "success");
};

var logError = function (a, b) {
    //Todo
    $.notify(a, b);
};

function logAjaxErrorMessage(xhr) {
    logError(getAjaxErrorMessage(xhr));
};