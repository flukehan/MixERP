using MixERP.Net.Common.Helpers;
using MixERP.Net.i18n.Resources;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.TransactionViewFactory.Helpers
{
    internal static class GridViewColumnHelper
    {
        internal static void AddColumns(GridView grid)
        {
            grid.Columns.Clear();

            AddTemplateFields(grid);

            GridViewHelper.AddDataBoundControl(grid, "TransactionMasterId", Titles.Id);
            GridViewHelper.AddDataBoundControl(grid, "TransactionCode", Titles.TranCode);
            GridViewHelper.AddDataBoundControl(grid, "Book", Titles.Book);
            GridViewHelper.AddDataBoundControl(grid, "ValueDate", Titles.ValueDate, "{0:d}");
            GridViewHelper.AddDataBoundControl(grid, "ReferenceNumber", Titles.ReferenceNumber);
            GridViewHelper.AddDataBoundControl(grid, "StatementReference", Titles.StatementReference);

            GridViewHelper.AddDataBoundControl(grid, "PostedBy", Titles.PostedBy);
            GridViewHelper.AddDataBoundControl(grid, "Office", Titles.Office);

            GridViewHelper.AddDataBoundControl(grid, "Status", Titles.Status);
            GridViewHelper.AddDataBoundControl(grid, "VerifiedBy", Titles.VerifiedBy);
            GridViewHelper.AddDataBoundControl(grid, "VerifiedOn", Titles.VerifiedOn, "{0:g}");
            GridViewHelper.AddDataBoundControl(grid, "Reason", Titles.Reason);
            GridViewHelper.AddDataBoundControl(grid, "TransactionTs", Titles.TransactionTimestamp, "{0:g}");
            GridViewHelper.AddDataBoundControl(grid, "FlagBg", "FlagBg");
            GridViewHelper.AddDataBoundControl(grid, "FlagFg", "FlagFg");
        }

        private static void AddTemplateFields(GridView grid)
        {
            TemplateField actionTemplateField = new TemplateField();
            actionTemplateField.HeaderText = Titles.Actions;
            actionTemplateField.ItemTemplate = new JournalViewActionTemplate();

            grid.Columns.Add(actionTemplateField);

            TemplateField checkBoxTemplateField = new TemplateField();
            checkBoxTemplateField.HeaderText = Titles.Select;
            checkBoxTemplateField.ItemTemplate = new GridViewHelper.GridViewSelectTemplate();
            grid.Columns.Add(checkBoxTemplateField);
        }

        internal class JournalViewActionTemplate : ITemplate
        {
            public void InstantiateIn(Control container)
            {
                using (HtmlGenericControl checkListIcon = new HtmlGenericControl("i"))
                {
                    checkListIcon.Attributes.Add("class", "icon list layout");
                    checkListIcon.Attributes.Add("onclick", "showCheckList(this);");
                    container.Controls.Add(checkListIcon);
                }
                using (HtmlGenericControl previewIcon = new HtmlGenericControl("i"))
                {
                    previewIcon.Attributes.Add("class", "icon print");
                    previewIcon.Attributes.Add("onclick", "showPreview(this);");
                    container.Controls.Add(previewIcon);
                }
                using (HtmlGenericControl detailIcon = new HtmlGenericControl("i"))
                {
                    detailIcon.Attributes.Add("class", "icon grid layout");
                    detailIcon.Attributes.Add("onclick", "showStockDetail(this);");
                    container.Controls.Add(detailIcon);
                }

                using (HtmlGenericControl goTopIcon = new HtmlGenericControl("i"))
                {
                    goTopIcon.Attributes.Add("class", "icon chevron circle up");
                    goTopIcon.Attributes.Add("onclick", "window.scroll(0);");
                    container.Controls.Add(goTopIcon);
                }
            }
        }
    }
}