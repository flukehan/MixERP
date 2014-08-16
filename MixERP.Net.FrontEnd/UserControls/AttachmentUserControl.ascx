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
    <asp:HiddenField ID="UploadedFilesHidden" runat="server" />

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
                        <input type="text" id="commentTextBox1" class="comment"  />
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
                        <input type="text" id="commentTextBox2" class="comment"  />
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
                        <input type="text" id="commentTextBox3" class="comment"  />
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
                        <input type="text" id="commentTextBox4" class="comment" />
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
    var invalidFileLocalized = "<%= Resources.Warnings.InvalidFile %>";
    var allowedExtensions = "<%= this.GetAllowedExtensions() %>".split(",");
    var areYouSureLocalized = "<%= Resources.Questions.AreYouSure %>";
    var uploadedFilesDeletedLocalized = "<%= Resources.Labels.UploadedFilesDeleted %>";
    var duplicateFileLocalized = "<% = Resources.Errors.DuplicateFiles %>";
</script>

<script src="/Scripts/ajax-file-upload.js"></script>
<script src="/Scripts/UserControls/AttachmentUserControl.js"></script>
