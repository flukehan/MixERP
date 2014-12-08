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

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AttachmentUserControl.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.AttachmentUserControl" %>

<style type="text/css">
    .comment {
    }

    .upload {
    }

    .browse {
    }

    #FileUploads .browse {
        width: 100%;
    }
</style>
<asp:HiddenField ID="UploadedFilesHidden" runat="server" />

<div id="FileUploads">
    <table class="ui table" style="width: 100%;">
        <thead>
            <tr>
                <th style="width: 340px;"><span>Comment</span>
                </th>
                <th style="width: 50px;" class="nopadding">
                    <span>Upload</span>
                </th>
                <th style="width: 240px;">
                    <span>Progress</span>
                </th>
                <th style="width: 340px;"><span>File Path</span>
                </th>
            </tr>
        </thead>
        <tbody class="ui form">
            <tr>
                <td>
                    <input type="text" id="CommentTextBox1" class="comment" />
                </td>
                <td>
                    <input type="file" id="FileUpload1" class="hidden upload" name="file" />
                    <input type="button" id="BrowseButton1" class="browse ui small blue button" value="Browse" />
                </td>
                <td>
                    <progress id="Progress1" value="0" max="100" style="width: 100%;"></progress>
                </td>
                <td>
                    <p id="FilePath1" class="path"></p>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="text" id="CommentTextBox2" class="comment" />
                </td>
                <td>
                    <input type="file" id="FileUpload2" class="hidden upload" name="file" />
                    <input type="button" id="BrowseButton2" class="browse ui small blue button" value="Browse" />
                </td>
                <td>
                    <progress id="Progress2" value="0" max="100" style="width: 100%;"></progress>
                </td>
                <td>
                    <p id="FilePath2" class="path"></p>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="text" id="CommentTextBox3" class="comment" />
                </td>
                <td>
                    <input type="file" id="FileUpload3" class="hidden upload" name="file" />
                    <input type="button" id="BrowseButton3" class="browse ui small blue button" value="Browse" />
                </td>
                <td>
                    <progress id="Progress3" value="0" max="100" style="width: 100%;"></progress>
                </td>
                <td>
                    <p id="FilePath3" class="path"></p>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="text" id="CommentTextBox4" class="comment" />
                </td>
                <td>
                    <input type="file" id="FileUpload4" class="hidden upload" name="file" />
                    <input type="button" id="BrowseButton4" class="browse ui small blue button" value="Browse" />
                </td>
                <td>
                    <progress id="Progress4" value="0" max="100" style="width: 100%;"></progress>
                </td>
                <td>
                    <p id="FilePath4" class="path"></p>
                </td>
            </tr>
        </tbody>
    </table>
</div>

<div class="vpad8">
    <input type="button" id="UploadButton" value="Upload" class="ui small blue button" />
    <input type="button" id="SaveButton" runat="server" value="Save" visible="False" class="ui small green button" />
    <input type="button" id="UndoButton" value="Undo" class="ui small red button" />
</div>
<p>
    <asp:Label ID="WarningLabel" runat="server" CssClass="big error" />
</p>

<script type="text/javascript">
    var allowedExtensions = "<%= this.GetAllowedExtensions() %>".split(",");
</script>

<script src="/Scripts/ajax-file-upload.js"></script>
<script src="/Scripts/UserControls/AttachmentUserControl.js"></script>