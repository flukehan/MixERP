/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

/*global allowedExtensions, appendParameter, Resources, getAjax, getData */

var undoButton = $("#UndoButton");
var uploadedFilesHidden = $("#UploadedFilesHidden");
var uploadButton = $("#UploadButton");
var warningLabel = $("#WarningLabel");

$(window).load(function () {
    $('.browse').on('click', function () {
        var counter = this.id.replace("BrowseButton", "");
        var targetControlSelector = "#FileUpload" + counter;

        $(targetControlSelector).click();
        return false;
    });
});

$(function () {
    $("input:file").change(function () {
        var counter = this.id.replace("FileUpload", "");
        var filePathSelector = "#FilePath" + counter;
        var fileName = $(this).val();

        if (fileName) {
            if (validateFileName(fileName)) {
                $(filePathSelector).html(fileName);
                $(filePathSelector).removeClass("big error");
                return;
            };

            $(filePathSelector).html(Resources.Messages.InvalidFile() + " " + fileName);
            $(filePathSelector).addClass("big error");
            $(this).val("");
        };
    });
});

function validateFileName(fileName) {
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
        if (confirm(Resources.Questions.AreYouSure())) {
            $.post(undoUploadServiceUrl, {
                uploadedFilesJson: uploadedFilesHidden.val()
            }, function (data) {
                var result = $(data).find("boolean").text();

                if (result) {
                    resetAttachmentForm();
                    $.notify(Resources.Messages.UploadFilesDeleted(), "success");
                };
            });
        };
    };
});


function resetAttachmentForm() {
    var paragraphs = $(".path");
    var progressBars = $("progress");
    var comments = $(".comment");

    progressBars.val(0);
    paragraphs.html("");
    comments.val("");

    $("#FileUploads table *").enable();
    uploadedFilesHidden.val("");
};

function Upload(comment, filePath, originalFileName) {
    this.Comment = comment;
    this.FilePath = filePath;
    this.OriginalFileName = originalFileName;
};


var hasFile = function () {
    var files = [];
    var filePath;

    $(".upload").each(function () {
        filePath = $(this).val();
        if (filePath) {
            files.push(filePath);
        };
    });

    return files.length > 0;
};

uploadButton.on("click", function () {
    if (hasFile()) {
        if (validateDuplicates()) {
            if (confirm(Resources.Questions.AreYouSure())) {
                var uploads = [];

                $(".upload").each(function () {
                    var fileUploadButtonId = $(this).attr("id");
                    var counter = fileUploadButtonId.replace("FileUpload", "");
                    var browseButtonSelector = "#BrowseButton" + counter;
                    var commentTextBoxSelector = "#CommentTextBox" + counter;
                    var progressBarSelector = "#Progress" + counter;
                    var originalFileName = $(this).val().replace(/^.*[\\\/]/, "");


                    var comment = $(commentTextBoxSelector).val();

                    $(this).upload(uploadHandlerUrl, function (success) {
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

                $("#FileUploads table *").disable();
            }
        }
    }
});


var validateDuplicates = function () {
    var files = [];
    var filePath;

    $(".upload").each(function () {
        filePath = $(this).val();
        if (filePath) {
            files.push(filePath);
        };
    });


    var sortedFiles = files.sort();
    var duplicates = [];

    for (var i = 0; i < files.length - 1; i++) {
        if (sortedFiles[i + 1] === sortedFiles[i]) {
            duplicates.push(sortedFiles[i]);
        };
    };

    if (isNullOrWhiteSpace(duplicates)) { //No duplicate found.
        warningLabel.html("");
        return true;
    } else {
        warningLabel.html(Resources.Warnings.DuplicateFiles() + " : " + duplicates.join());
        return false;
    }
};