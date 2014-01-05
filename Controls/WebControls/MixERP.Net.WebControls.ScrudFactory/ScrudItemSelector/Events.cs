using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudItemSelector
    {

        protected void FilterDropDownList_DataBound(object sender, EventArgs e)
        {
            using (DropDownList FilterDropDownList = sender as DropDownList)
            {
                if (FilterDropDownList == null)
                {
                    return;
                }

                foreach (ListItem item in FilterDropDownList.Items)
                {
                    item.Text = MixERP.Net.Common.Helpers.LocalizationHelper.GetResourceString("ScrudResource", item.Text);
                }
            }
        }

        protected void SearchGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e == null)
            {
                return;
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    string cellText = e.Row.Cells[i].Text;
                    if (!string.IsNullOrWhiteSpace(cellText))
                    {
                        cellText = MixERP.Net.Common.Helpers.LocalizationHelper.GetResourceString("ScrudResource", cellText);
                        e.Row.Cells[i].Text = cellText;
                    }
                }
            }
        }

        protected void GoButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.GetSchema())) return;
            if (string.IsNullOrWhiteSpace(this.GetView())) return;

            using (System.Data.DataTable table = MixERP.Net.WebControls.ScrudFactory.Data.FormHelper.GetTable(this.GetSchema(), this.GetView(), filterDropDownList.SelectedItem.Value, filterTextBox.Text, 10))
            {
                searchGridView.DataSource = table;
                searchGridView.DataBind();
            }
        }

        private string GetSchema()
        {
            return MixERP.Net.Common.Conversion.TryCastString(this.Page.Request["Schema"]);
        }

        private string GetView()
        {
            return MixERP.Net.Common.Conversion.TryCastString(this.Page.Request["View"]);
        }


    }
}
