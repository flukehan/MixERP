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

using System;
using System.Configuration;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.Common;

namespace MixERP.Net.FrontEnd.UserControls
{
    public partial class AttachmentUserControl : UserControl
    {
        #region Properties
        public bool ShowSaveButton { get; set; }
        public int Count { get; set; }

        #endregion
        #region IDisposable

        private HtmlInputButton saveButton;
        private HtmlGenericControl warningLabel;
        #endregion


        protected void Page_Init(object sender, EventArgs e)
        {
            this.CreateUploadPanel(this.Placeholder1);
            this.CreateButtons(this.Placeholder1);
            this.CreateWarningLabel(this.Placeholder1);
            this.CreateHiddenFields(this.Placeholder1);
            this.RegisterJavascript(this.Placeholder1);
        }

        #region Upload Panel

        private void CreateUploadPanel(Control container)
        {
            using (HtmlGenericControl fileUploads = new HtmlGenericControl("div"))
            {
                fileUploads.ID = "FileUploads";

                using (Table table = new Table())
                {
                    table.CssClass = "ui form table";
                    table.Style.Add("width", "100%");

                    this.CreateHeaderRow(table);
                    this.CreateRow(table);

                    fileUploads.Controls.Add(table);
                }

                container.Controls.Add(fileUploads);
            }
        }


        #region Header Row
        private void CreateHeaderRow(Table table)
        {
            using (TableHeaderRow header = new TableHeaderRow())
            {
                header.TableSection = TableRowSection.TableHeader;

                this.AddHeaderCell(header, "340px", "Comment");
                this.AddHeaderCell(header, "50px", "Upload", "nopadding");
                this.AddHeaderCell(header, "240px", "Progress");
                this.AddHeaderCell(header, "340px", "File Path");

                table.Rows.Add(header);
            }
        }

        private void AddHeaderCell(TableHeaderRow row, string width, string text, string cssClass = "")
        {
            using (TableHeaderCell cell = new TableHeaderCell())
            {
                cell.Style.Add("width", width);

                if (!string.IsNullOrWhiteSpace(cssClass))
                {
                    cell.CssClass = cssClass;
                }

                using (HtmlGenericControl span = new HtmlGenericControl("span"))
                {
                    span.InnerText = text;
                    cell.Controls.Add(span);
                }

                row.Cells.Add(cell);
            }
        }


        #endregion

        #region Row
        private void CreateRow(Table table)
        {
            if (this.Count.Equals(0))
            {
                this.Count = 5;
            }

            for (int i = 0; i < this.Count; i++)
            {
                using (TableRow row = new TableRow())
                {


                    this.CreateCommentCell(row, i);
                    this.CreateUploadCell(row, i);
                    this.CreateProgressCell(row, i);
                    this.CreateFilePathCell(row, i);

                    table.Rows.Add(row);
                }
            }
        }

        private void CreateCommentCell(TableRow row, int index)
        {
            using (TableCell cell = new TableCell())
            {
                using (HtmlInputText commentTextBox = new HtmlInputText())
                {
                    commentTextBox.ID = "CommentTextBox" + index;
                    commentTextBox.Attributes.Add("class", "comment");

                    cell.Controls.Add(commentTextBox);
                }

                row.Controls.Add(cell);
            }
        }

        private void CreateUploadCell(TableRow row, int index)
        {
            using (TableCell cell = new TableCell())
            {
                using (HtmlInputFile file = new HtmlInputFile())
                {
                    file.ID = "FileUpload" + index;
                    file.Attributes.Add("class", "hidden upload");
                    file.Name = "file";
                    cell.Controls.Add(file);
                }

                using (HtmlInputButton browseButton = new HtmlInputButton())
                {
                    browseButton.ID = "BrowseButton" + index;
                    browseButton.Attributes.Add("class", "browse ui small blue button");
                    browseButton.Value = "Browse";

                    cell.Controls.Add(browseButton);
                }

                row.Controls.Add(cell);
            }
        }

        private void CreateProgressCell(TableRow row, int index)
        {
            using (TableCell cell = new TableCell())
            {
                using (HtmlGenericControl progress = new HtmlGenericControl("progress"))
                {
                    progress.ID = "Progress" + index;
                    progress.Attributes.Add("value", "0");
                    progress.Attributes.Add("max", "100");
                    progress.Attributes.Add("style", "width: 100%;");

                    cell.Controls.Add(progress);
                }
                row.Controls.Add(cell);
            }
        }

        private void CreateFilePathCell(TableRow row, int index)
        {
            using (TableCell cell = new TableCell())
            {
                using (HtmlGenericControl path = new HtmlGenericControl("p"))
                {
                    path.ID = "FilePath" + index;
                    path.Attributes.Add("class", "path");
                    cell.Controls.Add(path);
                }
                row.Controls.Add(cell);
            }
        }

        #endregion


        #endregion

        #region Buttons
        private void CreateButtons(Control container)
        {
            using (HtmlGenericControl buttonContainer = new HtmlGenericControl("div"))
            {
                buttonContainer.Attributes.Add("class", "vpad8");
                this.CreateUploadButton(buttonContainer);
                this.CreatSaveButton(buttonContainer);
                this.CreateUndoButton(buttonContainer);

                container.Controls.Add(buttonContainer);
            }
        }

        private void CreateUploadButton(HtmlGenericControl container)
        {
            using (HtmlInputButton uploadButton = new HtmlInputButton())
            {
                uploadButton.ID = "UploadButton";
                uploadButton.Value = "Upload";
                uploadButton.Attributes.Add("class", "ui small blue button");

                container.Controls.Add(uploadButton);
            }
        }

        private void CreatSaveButton(HtmlGenericControl container)
        {
            this.saveButton = new HtmlInputButton();
            this.saveButton.ID = "SaveButton";
            this.saveButton.Attributes.Add("class", "ui small green button");
            this.saveButton.Value = "Save";
            this.saveButton.Visible = this.ShowSaveButton;

            container.Controls.Add(this.saveButton);
        }

        private void CreateUndoButton(HtmlGenericControl container)
        {
            using (HtmlInputButton undoButton = new HtmlInputButton())
            {
                undoButton.ID = "UndoButton";
                undoButton.Value = "Undo";
                undoButton.Attributes.Add("class", "ui small red button");

                container.Controls.Add(undoButton);
            }
        }

        #endregion

        private void CreateWarningLabel(Control container)
        {
            using (HtmlGenericControl p = new HtmlGenericControl("p"))
            {
                using (HtmlGenericControl span = new HtmlGenericControl("span"))
                {
                    span.ID = "WarningLabel";
                    span.Attributes.Add("class", "big error");

                    p.Controls.Add(span);
                }

                container.Controls.Add(p);
            }
        }

        #region Hidden Fields
        private void CreateHiddenFields(Control container)
        {
            using (HiddenField uploadedFilesHidden = new HiddenField())
            {
                uploadedFilesHidden.ID = "UploadedFilesHidden";

                container.Controls.Add(uploadedFilesHidden);
            }
        }
        #endregion

        private void RegisterJavascript(Control container)
        {
            string javascript = "var allowedExtensions='{0}'.split(',');";
            javascript = string.Format(CultureInfo.InvariantCulture, javascript, this.GetAllowedExtensions());
            PageUtility.RegisterJavascript("AttachmentUserControl_Vars", javascript, this.Page, true);
        }

        public string GetAllowedExtensions()
        {
            return ConfigurationManager.AppSettings["AllowedExtensions"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.CheckPermission();
        }

        private void CheckPermission()
        {
            var folder = ConfigurationManager.AppSettings["AttachmentsDirectory"];
            var permission = new FileIOPermission(FileIOPermissionAccess.Write, this.Server.MapPath(folder));
            var permissionSet = new PermissionSet(PermissionState.None);
            permissionSet.AddPermission(permission);

            if (!permissionSet.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet))
            {
                this.warningLabel.InnerText = String.Format(CultureInfo.CurrentUICulture, "The directory \"{0}\" is write protected", folder);
            }
        }
    }
}