using MixERP.Net.i18n.Resources;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.AttachmentFactory
{
    public sealed partial class Attachment
    {
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
                    row.CssClass = "ui form";
                    this.CreateCommentCell(row, i);
                    this.CreateUploadCell(row, i);
                    this.CreateProgressCell(row, i);
                    this.CreateFilePathCell(row, i);

                    table.Rows.Add(row);
                }
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
                    browseButton.Value = Titles.Browse;

                    cell.Controls.Add(browseButton);
                }

                row.Controls.Add(cell);
            }
        }
    }
}