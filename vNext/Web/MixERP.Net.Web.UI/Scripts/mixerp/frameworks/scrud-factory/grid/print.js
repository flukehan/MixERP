var scrudPrintGridView = function () {
    var user = $("#" + userIdHiddenId).val();
    var office = $("#" + officeCodeHiddenId).val();
    var title = $("#" + titleLabelId).html();

    window.printGridView(window.reportTemplatePath, window.reportHeaderPath, title, formGridViewId, window.date, user, office, 'ScrudReport', 1, 0);
};