<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AttachmentManager.ascx.cs" Inherits="MixERP.Net.Core.Modules.BackOffice.AttachmentManager" %>
<style>
    @media screen and (min-width: 768px) {
        .modal-wide .modal-dialog {
            width: 700px;
        }
    }
</style>
<h2>Manage Attachments</h2>

<mixerp:Attachment runat="server" ID="AttachmentUserControl" ShowSaveButton="true" />

<h3>View Attachments</h3>

<div class="row">
    <ul id="images" class="thumbnails list-unstyled">
    </ul>
</div>

<!-- Modal -->
<div class="modal modal-wide fade" id="opener" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                <h4 class="modal-title"></h4>
            </div>
            <div class="modal-body">
                <%-- ReSharper disable once Html.Obsolete --%>
                <center>
                    <img  alt="" />
                    <p class="vpad8">Hello brother</p>
                </center>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
    var images = $("#images");
    var saveButton = $("#SaveButton");

    var url = "";
    var data = "";

    $(document).ready(function () {
        initializeAttachments();
    });

    function initializeAttachments() {
        var book = getParameterByName("Book");
        var id = parseFloat2(getParameterByName("Id"));

        if (isNullOrWhiteSpace(book) || id <= 0) {
            return;
        };

        var ajaxGetAttachments = getAttachments(book, id);

        ajaxGetAttachments.success(function (msg) {
            images.html("");
            $.each(msg.d, function (index, item) {
                var id = item.Id;
                var comment = item.Comment;
                var filePath = item.FilePath;
                var originalFileName = item.OriginalFileName;
                var addedOn = new Date(parseInt(item.AddedOn.substr(6)));
                appendImage(id, comment, filePath, originalFileName, addedOn);
            });

            repaint();
        });

        ajaxGetAttachments.fail(function (xhr) {
            logAjaxErrorMessage(xhr);
        });
    };

    function appendImage(id, comment, filePath, originalFileName, addedOn) {
        images.append("<li class='col-md-3' id='li" + id + "'>" +
                        "<div class='thumbnail'>" +
                        "<center><h5>" + originalFileName + "</h5>" +
                            "<img src='/Modules/BackOffice/Handlers/ImageHandler.ashx?Path=" + filePath + "&H=300&W=250'  />" +
                        "<p class='vpad16'>" + comment + "<br />" + addedOn.toLocaleDateString() + ' ' + addedOn.toLocaleTimeString() + "</p>" +
                        "<div class='vpad8'>" +
                        "<a class='btn btn-sm btn-success' data-toggle='modal' data-target='#opener' onclick=\"initializeModal('" + filePath + "', '" + originalFileName + "', '" + comment + "');\">View</a>&nbsp;" +
                        "<a class='btn btn-sm btn-primary' target='_blank' href='" + filePath + "'>Download</a>&nbsp;" +
                        "<a class='btn btn-sm btn-danger' onclick=\"removeAttachment('" + id + "');\">Delete</a>&nbsp;" +
                        "</div>" +
                        "</center>" +
                        "</div></li>");
    };

    function initializeModal(filePath, originalFileName, comment) {
        $('#opener h4').html(originalFileName);
        $('#opener img').attr('src', '/Modules/BackOffice/Handlers/ImageHandler.ashx?&H=700&W=600&Path=' + filePath);
        $('#opener p').html(comment);
    };

    function removeAttachment(id) {
        if (confirm(areYouSureLocalized)) {
            var ajaxDeleteAttachment = deleteAttachment(id);

            ajaxDeleteAttachment.success(function () {
                $("#li" + id).remove();
                displaySucess();
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

        var book = getParameterByName("Book");
        var id = getParameterByName("Id");

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
</script>