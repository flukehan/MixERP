/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Web.UI.WebControls;
using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using FormHelper = MixERP.Net.WebControls.ScrudFactory.Data.FormHelper;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudItemSelector
    {

        protected void FilterDropDownList_DataBound(object sender, EventArgs e)
        {
            using (var dropDownList = sender as DropDownList)
            {
                if (dropDownList == null)
                {
                    return;
                }

                foreach (ListItem item in dropDownList.Items)
                {
                    item.Text = LocalizationHelper.GetResourceString("ScrudResource", item.Text);
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
                for (var i = 0; i < e.Row.Cells.Count; i++)
                {
                    var cellText = e.Row.Cells[i].Text;
                    if (!string.IsNullOrWhiteSpace(cellText))
                    {
                        cellText = LocalizationHelper.GetResourceString("ScrudResource", cellText);
                        e.Row.Cells[i].Text = cellText;
                    }
                }
            }
        }

        protected void GoButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.GetSchema())) return;
            if (string.IsNullOrWhiteSpace(this.GetView())) return;

            using (var table = FormHelper.GetTable(this.GetSchema(), this.GetView(), this.filterDropDownList.SelectedItem.Value, this.filterTextBox.Text, 10))
            {
                this.searchGridView.DataSource = table;
                this.searchGridView.DataBind();
            }
        }

        private string GetSchema()
        {
            return Conversion.TryCastString(this.Page.Request["Schema"]);
        }

        private string GetView()
        {
            return Conversion.TryCastString(this.Page.Request["View"]);
        }


    }
}
