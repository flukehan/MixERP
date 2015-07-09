var images = $("#images");
var saveButton = $("#SaveButton");


var url = "";
var data = "";

$(document).ready(function () {
    initializeAttachments();
});

function initializeAttachments() {
    var book = getQueryStringByName("Book");
    var id = parseFloat2(getQueryStringByName("Id"));

    if (isNullOrWhiteSpace(book) || id <= 0) {
        return;
    };

    var ajaxGetAttachments = getAttachments(book, id);

    ajaxGetAttachments.success(function (msg) {
        LoadImages(msg.d);
    });

    ajaxGetAttachments.fail(function (xhr) {
        logAjaxErrorMessage(xhr);
    });
};

function LoadImages(model) {
    images.html("");
    images.addClass("loading");

    var connectedItems = "";

    for (var i = 0; i < model.length; i++) {
        mod = (i + 1) % 3;

        if (i === 0) {
            initializeModal(model[i].FilePath, "", "", true); //Initialize modal to avoid flickering animation
        };

        if (mod === 1) {
            connectedItems += '<div class="ui three doubling cards">';
        };

        connectedItems += LoadImageGroup(model[i]);

        if (mod === 0 || i === model.length - 1) {
            connectedItems += "</div>";
            images.append(connectedItems);

            connectedItems = "";
        };

        images.removeClass("loading");
    };

    repaint();
};

function LoadImageGroup(file) {
    var imagePath = "/Modules/BackOffice/Handlers/ImageHandler.ashx?Path=" + file.FilePath + "&H=300&W=250";

    var html = '<div class="card">' +
        '<div class="ui top attached blue segment">' + file.OriginalFileName + '</div>' +
        '<div class="image">' + '<img src="' + imagePath + '" />' + '</div>' +
        '<div class="content">' +
        '<div class="description">' + file.Comment + '<br />' + parseSerializedDate(file.AddedOn) + '<br />' + '<br />' + '</div>' +
        '</div>' +
        '<div class="ui bottom attached button">' +
        "<a class='ui positive button' data-toggle='modal' data-target='#opener' onclick=\"initializeModal('" + file.FilePath + "', '" + file.OriginalFileName + "', '" + file.Comment + "');\">" + Resources.Titles.View() + "</a>&nbsp;" +
        "<a class='ui blue button' target='_blank' href='" + file.FilePath + "'>" + Resources.Titles.Download() + "</a>&nbsp;" +
        "<a class='ui negative button' onclick=\"removeAttachment('" + file.AttachmentId + "');\">" + Resources.Titles.Delete() + "</a>&nbsp;" +
        '</div>' +
        '</div>';

    return html;
};

function initializeModal(filePath, originalFileName, comment, initializer) {
    $('#opener .header').html("<i class='photo icon'></i>" + originalFileName);
    $('#opener img').attr('src', '/Modules/BackOffice/Handlers/ImageHandler.ashx?&H=500&W=900&Path=' + filePath);
    $('#opener p').html(comment);

    if (initializer) {
        repaint();
    };

    if (!initializer) {
        $("#opener").modal('setting', 'transition', 'vertical flip').modal("show");
    }
};

function removeAttachment(id) {
    if (confirm(Resources.Questions.AreYouSure())) {
        var ajaxDeleteAttachment = deleteAttachment(id);

        ajaxDeleteAttachment.success(function () {
            $("#li" + id).remove();
            displaySucess();
            initializeAttachments();
        });

        ajaxDeleteAttachment.fail(function (xhr) {
            logAjaxErrorMessage(xhr);
        });
    };
};

function getAttachments(book, id) {
    url = "/Modules/BackOffice/Services/Attachments.asmx/GetAttachments";
    data = appendParameter("", "book", book);
    data = appendParameter(data, "id", id);

    data = getData(data);

    return getAjax(url, data);
};

function deleteAttachment(id) {
    url = "/Modules/BackOffice/Services/Attachments.asmx/DeleteAttachment";
    data = appendParameter("", "id", id);

    data = getData(data);

    return getAjax(url, data);
};

saveButton.click(function () {
    var uploads = uploadedFilesHidden.val();
    if (isNullOrWhiteSpace(uploads)) {
        $.notify("Nothing to save.");
        return;
    };

    var book = getQueryStringByName("Book");
    var id = getQueryStringByName("Id");

    var ajaxSave = save(book, id, uploads);

    ajaxSave.success(function (msg) {
        if (msg.d) {
            resetAttachmentForm();
            initializeAttachments();
            displaySucess();
        };
    });

    ajaxSave.fail(function (xhr) {
        logAjaxErrorMessage(xhr);
    });
});

function save(book, id, attachmentsJSON) {
    url = "/Modules/BackOffice/Services/Attachments.asmx/Save";
    data = appendParameter("", "book", book);
    data = appendParameter(data, "id", id);
    data = appendParameter(data, "attachmentsJSON", attachmentsJSON);
    data = getData(data);

    return getAjax(url, data);
};