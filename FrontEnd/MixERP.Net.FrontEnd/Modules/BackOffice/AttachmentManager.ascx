<%--
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
along with MixERP.  If not, see <http://www.gnu.org/licenses />.
--%>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AttachmentManager.ascx.cs" Inherits="MixERP.Net.Core.Modules.BackOffice.AttachmentManager" %>
<style>
    @media screen and (min-width: 768px) {
        .modal-wide .modal-dialog {
            width: 700px;
        }
    }
</style>

<div class="ui massive teal header">Upload Attachments</div>

<mixerp:Attachment runat="server" ID="AttachmentUserControl" ShowSaveButton="true" />

<div class="ui massive teal header">View Attachments</div>

<div id="images">
</div>

<!-- Modal -->
<div class="ui large modal" id="opener">
    <i class="close icon"></i>
    <div class="ui teal header">
    </div>
    <div class="content">

        <div class="ui segment">
            <img src="/" alt="" />
            <p class="vpad8"></p>
        </div>

        <div class="actions">
            <div class="ui teal button">
                Okay
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
            mod = (i + 1) % 5;

            if (i === 0) {
                initializeModal(model[i].FilePath, "", "", true);//Initialize modal to avoid flickering animation
            };

            if (mod === 1) {
                connectedItems += '<div class="ui five connected items">';
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

        var html = '<div class="item">' +
                        '<div class="image">' + '<img src="' + imagePath + '" />' + '</div>' +
                        '<div class="content">' +
                            '<div class="name">' + file.OriginalFileName + '</div>' +
                            '<div class="description">' + file.Comment + '<br />' + file.AddedOn + '<br />' + '<br />' + '</div>' +
                            "<a class='ui positive button' data-toggle='modal' data-target='#opener' onclick=\"initializeModal('" + file.FilePath + "', '" + file.OriginalFileName + "', '" + file.Comment + "');\">View</a>&nbsp;" +
                            "<a class='ui blue button' target='_blank' href='" + file.FilePath + "'>Download</a>&nbsp;" +
                            "<a class='ui negative button' onclick=\"removeAttachment('" + file.Id + "');\">Delete</a>&nbsp;" +
                        '</div>' +
                    '</div>';

        return html;
    };

    function initializeModal(filePath, originalFileName, comment, initializer) {
        $('#opener .header').html("<i class='photo icon'></i>" + originalFileName);
        $('#opener img').attr('src', '/Modules/BackOffice/Handlers/ImageHandler.ashx?&H=538&W=956&Path=' + filePath);
        $('#opener p').html(comment);

        if (initializer) {
            repaint();
        };

        if (!initializer) {
            $("#opener").modal('setting', 'transition', 'vertical flip').modal("show");
        }
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
