<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AttachmentUserControl.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.AttachmentUserControl" %>

<style type="text/css">
    .comment {
    }

    .upload {
    }

    .browse {
    }

    #fileUploads .browse {
        width: 100%;
    }
</style>
<asp:HiddenField ID="UploadedFilesHidden" runat="server" />

<div>
    <div id="fileUploads">
        <table class="table-form-pad" style="width: 100%;">
            <tbody>
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
                <tr>
                    <td>
                        <input type="text" id="commentTextBox1" class="comment form-control input-sm" />
                    </td>
                    <td>
                        <input type="file" id="fileUpload1" class="hidden upload" name="file" />
                        <input type="button" id="browseButton1" class="browse btn btn-default btn-sm" value="Browse" />
                    </td>
                    <td>
                        <progress id="progress1" value="0" max="100" style="width: 100%;"></progress>
                    </td>
                    <td>
                        <p id="filePath1" class="path"></p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="commentTextBox2" class="comment form-control input-sm" />
                    </td>
                    <td>
                        <input type="file" id="fileUpload2" class="hidden upload" name="file" />
                        <input type="button" id="browseButton2" class="browse btn btn-default btn-sm" value="Browse" />
                    </td>
                    <td>
                        <progress id="progress2" value="0" max="100" style="width: 100%;"></progress>
                    </td>
                    <td>
                        <p id="filePath2" class="path"></p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="commentTextBox3" class="comment form-control input-sm" />
                    </td>
                    <td>
                        <input type="file" id="fileUpload3" class="hidden upload" name="file" />
                        <input type="button" id="browseButton3" class="browse btn btn-default btn-sm" value="Browse" />
                    </td>
                    <td>
                        <progress id="progress3" value="0" max="100" style="width: 100%;"></progress>
                    </td>
                    <td>
                        <p id="filePath3" class="path"></p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="commentTextBox4" class="comment form-control input-sm" />
                    </td>
                    <td>
                        <input type="file" id="fileUpload4" class="hidden upload" name="file" />
                        <input type="button" id="browseButton4" class="browse btn btn-default btn-sm" value="Browse" />
                    </td>
                    <td>
                        <progress id="progress4" value="0" max="100" style="width: 100%;"></progress>
                    </td>
                    <td>
                        <p id="filePath4" class="path"></p>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>

    <input type="button" id="UploadButton" value="Upload" class="btn btn-default btn-sm" />
    <input type="button" id="SaveButton" runat="server" value="Save" visible="False" class="btn btn-default btn-sm" />
    <input type="button" id="UndoButton" value="Undo" class="btn btn-default btn-sm" />
    <p>
        <asp:Label ID="WarningLabel" runat="server" CssClass="error" />
    </p>
</div>
<script type="text/javascript">
    var allowedExtensions = "<%= this.GetAllowedExtensions() %>".split(",");
</script>

<script src="/Scripts/ajax-file-upload.js"></script>
<script src="/Scripts/UserControls/AttachmentUserControl.js"></script>