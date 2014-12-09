using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.TransactionViewFactory.Resources;
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

            GridViewHelper.AddDataBoundControl(grid, "transaction_master_id", Titles.Id);
            GridViewHelper.AddDataBoundControl(grid, "transaction_code", Titles.TranCode);
            GridViewHelper.AddDataBoundControl(grid, "book", Titles.Book);
            GridViewHelper.AddDataBoundControl(grid, "value_date", Titles.ValueDate, "{0:d}");
            GridViewHelper.AddDataBoundControl(grid, "reference_number", Titles.ReferenceNumber);
            GridViewHelper.AddDataBoundControl(grid, "statement_reference", Titles.StatementReference);

            GridViewHelper.AddDataBoundControl(grid, "posted_by", Titles.PostedBy);
            GridViewHelper.AddDataBoundControl(grid, "office", Titles.Office);

            GridViewHelper.AddDataBoundControl(grid, "status", Titles.Status);
            GridViewHelper.AddDataBoundControl(grid, "verified_by", Titles.VerifiedBy);
            GridViewHelper.AddDataBoundControl(grid, "verified_on", Titles.VerifiedOn, "{0:g}");
            GridViewHelper.AddDataBoundControl(grid, "reason", Titles.Reason);
            GridViewHelper.AddDataBoundControl(grid, "transaction_ts", Titles.TransactionTimestamp, "{0:g}");
            GridViewHelper.AddDataBoundControl(grid, "flag_bg", "flag_bg");
            GridViewHelper.AddDataBoundControl(grid, "flag_fg", "flag_fg");
        }

        private static void AddTemplateFields(GridView grid)
        {
            TemplateField actionTemplateField = new TemplateField();
            actionTemplateField.HeaderText = Titles.Actions;
            actionTemplateField.ItemTemplate = new JournalViewActionTemplate();

            grid.Columns.Add(actionTemplateField);

            TemplateField checkBoxTemplateField = new TemplateField();
            checkBoxTemplateField.HeaderText = Titles.Select;
            checkBoxTemplateField.ItemTemplate = new JournalViewSelectTemplate();
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

        internal class JournalViewSelectTemplate : ITemplate
        {
            public void InstantiateIn(Control container)
            {
                using (HtmlGenericControl toggleCheckBox = new HtmlGenericControl("div"))
                {
                    toggleCheckBox.Attributes.Add("class", "ui toggle checkbox");

                    using (HtmlInputCheckBox checkBox = new HtmlInputCheckBox())
                    {
                        toggleCheckBox.Controls.Add(checkBox);
                    }

                    using (HtmlGenericControl label = new HtmlGenericControl("label"))
                    {
                        toggleCheckBox.Controls.Add(label);
                    }

                    container.Controls.Add(toggleCheckBox);
                }
            }
        }
    }
}