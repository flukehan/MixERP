var undoButton = $("#undoButton");
var uploadedFilesHidden = $("#UploadedFilesHidden");
var uploadButton = $("#uploadButton");
var warningLabel = $("#WarningLabel");

$(window).load(function () {
    $('.browse').on('click', function () { // use .live() for older versions of jQuery
        var counter = this.id.replace("browseButton", "");
        var targetControlSelector = "#fileUpload" + counter;

        $(targetControlSelector).click();
        return false;
    });
});

$(function () {
    $("input:file").change(function () {
        var counter = this.id.replace("fileUpload", "");
        var filePathSelector = "#filePath" + counter;
        var fileName = $(this).val();

        if (fileName) {
            if (validate(fileName)) {
                $(filePathSelector).html(fileName);
                $(filePathSelector).removeClass("error-message");
                return;
            }

            $(filePathSelector).html(invalidFileLocalized + " " + fileName);
            $(filePathSelector).addClass("error-message");
            $(this).val("");
        }
    });
});

var validate = function (fileName) {
    var ext = fileName.split('.').pop();

    var index = $.inArray(ext, allowedExtensions);
    return index > -1;
};

$.fn.disable = function () {
    return this.each(function () {
        if (typeof this.disabled != "undefined") this.disabled = true;
    });
};

$.fn.enable = function () {
    return this.each(function () {
        if (typeof this.disabled != "undefined") this.disabled = false;
    });
};


undoButton.on("click", function () {
    $(".browse").prop('disabled', false);
    var paragraphs = $(".path");
    var progressBars = $("progress");
    var comments = $(".comment");

    if (uploadedFilesHidden.val() != "") {
        if (confirm(areYouSureLocalized)) {
            $.ajax({
                type: "POST",
                url: "/Services/UploadHelper.asmx/UndoUpload",
                data: "{'uploadedFilesJson': '" + uploadedFilesHidden.val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () {
                    progressBars.val(0);
                    paragraphs.html("");
                    comments.val("");
                    $("#fileUploads table *").enable();
                    uploadedFilesHidden.val("");
                    $.notify(uploadedFilesDeletedLocalized, "success");
                },
                error: function (e) {
                    $.notify(e.error, "error");
                }
            });
        }
    }
});

function Upload(comment, filePath, originalFileName) {
    this.Comment = comment;
    this.FilePath = filePath;
    this.OriginalFileName = originalFileName;
}

uploadButton.on("click", function () {
    if (haveFile()) {
        if (validateDuplicates()) {

            if (confirm(areYouSureLocalized)) {
                var uploads = new Array();

                $(".upload").each(function () {
                    var fileUploadButtonId = $(this).attr("id");
                    var counter = fileUploadButtonId.replace("fileUpload", "");
                    var browseButtonSelector = "#browseButton" + counter;
                    var commentTextBoxSelector = "#commentTextBox" + counter;
                    var progressBarSelector = "#progress" + counter;
                    var originalFileName = $(this).val();

                    var comment = $(commentTextBoxSelector).val();

                    $(this).upload("/Handlers/FileUpload.ashx", function (success) {
                        $(browseButtonSelector).prop("disabled", true);
                        $("#" + fileUploadButtonId).val(""); //Clear file upload control.
                        var upload = new Upload(comment, success, originalFileName);
                        uploads.push(upload);
                        uploadedFilesHidden.val(JSON.stringify(uploads));
                    }, function (progress, value) {
                        $(progressBarSelector).val(value);
                    });
                });

                $("#fileUploads table *").disable();
            }
        }
    }
});

var haveFile = function () {
    var files = new Array();
    var filePath;

    $(".upload").each(function () {
        filePath = $(this).val();
        if (filePath) {
            files.push(filePath);
        }
    });

    return files.length > 0;
};

var validateDuplicates = function () {
    var files = new Array();
    var filePath;

    $(".upload").each(function () {
        filePath = $(this).val();
        if (filePath) {
            files.push(filePath);
        }
    });

    var sortedFiles = files.sort();
    var duplicates = new Array;

    for (var i = 0; i < files.length - 1; i++) {
        if (sortedFiles[i + 1] == sortedFiles[i]) {
            duplicates.push(sortedFiles[i]);
        }
    }

    if (duplicates == "") { //No duplicate found.
        warningLabel.html("");
        return true;
    } else {
        warningLabel.html(duplicateFileLocalized + " : " + duplicates.join());
        return false;
    }
};