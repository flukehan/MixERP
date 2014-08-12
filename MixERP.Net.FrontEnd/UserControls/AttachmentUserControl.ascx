<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AttachmentUserControl.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.AttachmentUserControl" %>

<%-- ReSharper disable once CssBrowserCompatibility --%>
<style type="text/css">
    .sub-title {
        color: #a53df6;
        cursor: pointer;
        font-weight: bold;
    }

    .hidden {
        display: none;
    }

    #fileUploads p {
        width: 100%;
    }

    progress {
        width: 100px;
        margin-right: 4px;
    }

    #fileUploads tr td {
        padding: 0px !important;
    }

    #fileUploads table tr td:first-child {
        width: 100px;
    }

    #fileUploads table tr td:nth-child(2) {
        width: 300px;
    }

    #fileUploads span {
        padding: 4px;
    }

    #fileUploads input[type=text] {
        width: 240px;
    }

    #fileUploads input[type=button] {
        margin-left: 2px;
    }

    #fileUploads p {
        padding: 4px;
    }

    #fileUploads thead tr td {
        background-color: #9900CC;
    }

    progress {
        -webkit-appearance: progress-bar;
        -webkit-box-sizing: border-box;
        -moz-box-sizing: border-box;
        box-sizing: border-box;
        display: inline-block;
        height: 1em;
        width: 10em;
        vertical-align: -0.2em;
        appearance: none;
        -moz-appearance: none;
        -webkit-appearance: none;
        border: solid 1px #808080;
        width: 200px;
        height: 10px;
        -webkit-writing-mode: horizontal-tb;
    }

    .button {
        padding: 4px 8px 4px 8px;
    }
    
    .nodpad {
        padding: 0px;
    }
</style>

<div>
    <asp:HiddenField ID="UploadedFiles" runat="server" />

    <div id="fileUploads">
        <table class="grid nopad form-table grid3" style="width: 100%;">
            <tbody>
                <tr>
                    <th><span>Comment</span>
                    </th>
                    <th><span>Upload</span>
                    </th>
                    <th><span>File Path</span>
                    </th>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="commentTextBox1" />
                    </td>
                    <td>
                        <input type="file" id="fileUpload1" class="hidden upload" name="file" />
                        <input type="button" id="browseButton1" class="browse button" value="Browse" />
                        <progress id="progress1" value="0" max="100"></progress>
                    </td>
                    <td>
                        <p id="filePath1" class="path"></p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="commentTextBox2" />
                    </td>
                    <td>
                        <input type="file" id="fileUpload2" class="hidden upload" name="file" />
                        <input type="button" id="browseButton2" class="browse button" value="Browse" />
                        <progress id="progress2" value="0" max="100"></progress>
                    </td>
                    <td>
                        <p id="filePath2" class="path"></p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="commentTextBox3" />
                    </td>
                    <td>
                        <input type="file" id="fileUpload3" class="hidden upload" name="file" />
                        <input type="button" id="browseButton3" class="browse button" value="Browse" />
                        <progress id="progress3" value="0" max="100"></progress>
                    </td>
                    <td>
                        <p id="filePath3" class="path"></p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="commentTextBox4" />
                    </td>
                    <td>
                        <input type="file" id="fileUpload4" class="hidden upload" name="file" />
                        <input type="button" id="browseButton4" class="browse button" value="Browse" />
                        <progress id="progress4" value="0" max="100"></progress>
                    </td>
                    <td>
                        <p id="filePath4" class="path"></p>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>

    <input type="button" id="uploadButton" value="Upload" class="button" />
    <input type="button" id="undoButton" value="Undo" class="button" />
    <p>
        <asp:Label ID="WarningLabel" runat="server" CssClass="error" />
    </p>


</div>

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
    };

</script>

<script type="text/javascript">

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

    var uploadedFiles = $("#UploadedFiles");
    var areYouSureLocalized = "<%= Resources.Questions.AreYouSure %>";

    $("#undoButton").on("click", function () {
        $(".browse").prop('disabled', false);
        var uploadedFiles = $("#UploadedFiles");
        var paragraphs = $(".path");
        var progressBars = $("progress");
        var uploadedFilesDeletedLocalized = "<%= Resources.Labels.UploadedFilesDeleted %>";

        if (uploadedFiles.val() != "") {
            if (confirm(areYouSureLocalized)) {
                $.ajax({
                    type: "POST",
                    url: "<%=this.ResolveUrl("~/Services/UploadHelper.asmx/UndoUpload") %>",
                    data: "{'uploadedFiles': '" + uploadedFiles.val() + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function () {
                        $.notify(uploadedFilesDeletedLocalized, "success");
                    },
                    error: function (e) {
                        $.notify(e.error, "error");
                    }
                });

                uploadedFiles.val("");
                progressBars.val(0);
                paragraphs.html("");
                $("#fileUploads table *").enable();

            }
        }
    });


    $("#uploadButton").on("click", function () {
        if (haveFile()) {
            if (validateDuplicates()) {

                if (confirm(areYouSureLocalized)) {

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
        } else {
            warningLabel.html(duplicateFileLocalized + " : " + duplicates.join());
            return false;
        }
    };
</script>
