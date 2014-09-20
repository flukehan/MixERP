/*global allowedExtensions, appendParameter, areYouSureLocalized, duplicateFileLocalized, getAjax, getData, invalidFileLocalized, uploadedFilesDeletedLocalized */

var undoButton = $("#UndoButton");
var uploadedFilesHidden = $("#UploadedFilesHidden");
var uploadButton = $("#UploadButton");
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
        if (typeof this.disabled !== "undefined") this.disabled = true;
    });
};

$.fn.enable = function () {
    return this.each(function () {
        if (typeof this.disabled !== "undefined") this.disabled = false;
    });
};

undoButton.on("click", function () {
    $(".browse").prop('disabled', false);

    if (uploadedFilesHidden.val() !== "") {
        if (confirm(areYouSureLocalized)) {
            var url = "/Services/UploadHelper.asmx/UndoUpload";
            var data = appendParameter("", "uploadedFilesJson", uploadedFilesHidden.val());
            data = getData(data);

            var undoUploadAjax = getAjax(url, data);

            undoUploadAjax.success(function () {
                resetAttachmentForm();
                $.notify(uploadedFilesDeletedLocalized, "success");
            });

            undoUploadAjax.error(function (xhr) {
                $.notify(xhr.error, "error");
            });
        }
    }
});

function resetAttachmentForm() {
    var paragraphs = $(".path");
    var progressBars = $("progress");
    var comments = $(".comment");

    progressBars.val(0);
    paragraphs.html("");
    comments.val("");

    $("#fileUploads table *").enable();
    uploadedFilesHidden.val("");
};

function Upload(comment, filePath, originalFileName) {
    this.Comment = comment;
    this.FilePath = filePath;
    this.OriginalFileName = originalFileName;
}

uploadButton.on("click", function () {
    if (hasFile()) {
        if (validateDuplicates()) {
            if (confirm(areYouSureLocalized)) {
                var uploads = [];

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
                }).promise().done(function () {
                    if (typeof uploadButtonCallback == "function") {
                        uploadButtonCallback();
                    };
                });;

                $("#fileUploads table *").disable();
            }
        }
    }
});

var hasFile = function () {
    var files = [];
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
    var files = [];
    var filePath;

    $(".upload").each(function () {
        filePath = $(this).val();
        if (filePath) {
            files.push(filePath);
        }
    });

    var sortedFiles = files.sort();
    var duplicates = [];

    for (var i = 0; i < files.length - 1; i++) {
        if (sortedFiles[i + 1] === sortedFiles[i]) {
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