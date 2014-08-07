<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AttachmentUserControl.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.AttachmentUserControl" %>

<style type="text/css">
</style>

<div>
    <asp:HiddenField ID="UploadedFiles" runat="server" />

    <div id="fileUploads">
        <table class="grid nopad">
            <tr>
                <td><span>Comment</span>
                </td>
                <td><span>Browser</span>
                </td>
                <td><span>File Path</span>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="text" id="commentTextBox1" />
                </td>
                <td>
                    <input type="file" id="fileUpload1" class="hidden upload" name="file" />
                    <input type="button" id="browseButton1" class="browse" value="Browse" />
                    <progress id="progress1" value="0" max="100"></progress>
                </td>
                <td>
                    <p id="filePath1"></p>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="text" id="commentTextBox2" />
                </td>
                <td>
                    <input type="file" id="fileUpload2" class="hidden upload" name="file" />
                    <input type="button" id="browseButton2" class="browse" value="Browse" />
                    <progress id="progress2" value="0" max="100"></progress>
                </td>
                <td>
                    <p id="filePath2"></p>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="text" id="commentTextBox3" />
                </td>
                <td>
                    <input type="file" id="fileUpload3" class="hidden upload" name="file" />
                    <input type="button" id="browseButton3" class="browse" value="Browse" />
                    <progress id="progress3" value="0" max="100"></progress>
                </td>
                <td>
                    <p id="filePath3"></p>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="text" id="commentTextBox4" />
                </td>
                <td>
                    <input type="file" id="fileUpload4" class="hidden upload" name="file" />
                    <input type="button" id="browseButton4" class="browse" value="Browse" />
                    <progress id="progress4" value="0" max="100"></progress>
                </td>
                <td>
                    <p id="filePath4"></p>
                </td>
            </tr>
        </table>
    </div>

    <input type="button" id="uploadButton" value="Upload" />
    <input type="button" id="UndoButton" runat="server" value="Undo" onserverclick="UndoButton_ServerClick" />
    <p>
        <asp:Label ID="WarningLabel" runat="server" CssClass="error" />
    </p>


</div>

<script type="text/javascript">
    $(window).load(function () {
        $('.browse').on('click', function () { // use .live() for older versions of jQuery
            var counter = this.id.replace("browseButton", "");
            var targetControlSelector = "#fileUpload" + counter;
            var filePathSelector = "#filePath" + counter;

            $(targetControlSelector).click();
            return false;
        });
    });

    $(function () {
        $("input:file").change(function () {
            var invalidFileLocalized = "<%= Resources.Warnings.InvalidFile %>";
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
        var allowedExtensions = "<%= this.GetAllowedExtensions() %>".split(",");
        var ext = fileName.split('.').pop();

        var index = $.inArray(ext, allowedExtensions);
        return index > -1;
    }

</script>
<script type="text/javascript">
    // Ajax File upload with jQuery and XHR2
    // Sean Clark http://square-bracket.com
    // xhr2 file upload
    // data is optional
    $.fn.upload = function (remote, data, successFn, progressFn) {
        // if we dont have post data, move it along
        if (typeof data != "object") {
            progressFn = successFn;
            successFn = data;
        }
        return this.each(function () {
            if ($(this)[0].files[0]) {
                var formData = new FormData();
                formData.append($(this).attr("name"), $(this)[0].files[0]);

                // if we have post data too
                if (typeof data == "object") {
                    for (var i in data) {
                        formData.append(i, data[i]);
                    }
                }

                // do the ajax request
                $.ajax({
                    url: remote,
                    type: 'POST',
                    xhr: function () {
                        myXhr = $.ajaxSettings.xhr();
                        if (myXhr.upload && progressFn) {
                            myXhr.upload.addEventListener('progress', function (prog) {
                                var value = ~~((prog.loaded / prog.total) * 100);

                                // if we passed a progress function
                                if (progressFn && typeof progressFn == "function") {
                                    progressFn(prog, value);

                                    // if we passed a progress element
                                } else if (progressFn) {
                                    $(progressFn).val(value);
                                }
                            }, false);
                        }
                        return myXhr;
                    },
                    data: formData,
                    dataType: "json",
                    cache: false,
                    contentType: false,
                    processData: false,
                    complete: function (res) {
                        var json;
                        try {
                            json = JSON.parse(res.responseText);
                        } catch (e) {
                            json = res.responseText;
                        }
                        if (successFn) successFn(json);
                    }
                });
            }
        });
    };



</script>

<script type="text/javascript">
    var uploadedFiles = $("#UploadedFiles");

    $("#uploadButton").on("click", function () {
        if (haveFile()) {
            if (validateDuplicates()) {
                $(".upload").each(function () {
                    var fileUploadButtonId = $(this).attr("id");
                    var counter = fileUploadButtonId.replace("fileUpload", "");
                    var browseButtonSelector = "#browseButton" + counter;
                    var commentTextBoxSelector = "#commentTextBox" + counter;
                    var progressBarSelector = "#progress" + counter;

                    var comment = $(commentTextBoxSelector).val();

                    $(this).upload("/Handlers/FileUpload.ashx", function (success) {
                        if (uploadedFiles.val() == '') {
                            uploadedFiles.val(comment + "|" + success);
                        }
                        else {
                            uploadedFiles.val(uploadedFiles.val() + ',' + comment + "|" + success);
                        }

                        $(browseButtonSelector).prop("disabled", true);
                        $("#" + fileUploadButtonId).val(""); //Clear file upload control.
                    }, function (progress, value) {
                        $(progressBarSelector).val(value);
                    });
                });
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
    }

    var validateDuplicates = function () {
        var files = new Array();
        var filePath;

        $(".upload").each(function () {
            filePath = $(this).val();
            if (filePath) {
                files.push(filePath);
            }
        });

        var duplicateFileLocalized = "<% = Resources.Errors.DuplicateFiles %>";
        var warningLabel = $("#WarningLabel");

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
        }
        else {
            warningLabel.html(duplicateFileLocalized + " : " + duplicates.join());
            return false;
        }
    }
</script>
