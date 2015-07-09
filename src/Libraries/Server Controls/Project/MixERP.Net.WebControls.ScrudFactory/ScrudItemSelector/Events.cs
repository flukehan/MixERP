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

using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory.Data;
using MixERP.Net.WebControls.ScrudFactory.Helpers;
using System;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudItemSelector
    {
        private void FilterSelectDataBound(object sender, EventArgs e)
        {
            using (DropDownList dropDownList = sender as DropDownList)
            {
                if (dropDownList == null)
                {
                    return;
                }

                foreach (ListItem item in dropDownList.Items)
                {
                    item.Text = ScrudLocalizationHelper.GetResourceString(this.GetResourceClassName(), item.Text);
                }
            }
        }

        private string GetResourceClassName()
        {
            if (string.IsNullOrWhiteSpace(this.ResourceClassName))
            {
                return DbConfig.GetScrudParameter(this.Catalog, "ResourceClassName");
            }

            return this.ResourceClassName;
        }

        private string GetSchema()
        {
            return Conversion.TryCastString(this.Page.Request["Schema"]);
        }

        private string GetView()
        {
            return Conversion.TryCastString(this.Page.Request["View"]);
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.GetSchema()))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(this.GetView()))
            {
                return;
            }

            using (
                var table = FormHelper.GetTable(this.Catalog, this.GetSchema(), this.GetView(),
                    this.filterSelect.SelectedItem.Value, this.filterInputText.Text, 10, "1"))
            {
                this.searchGridView.DataSource = table;
                this.searchGridView.DataBind();
            }
        }

        private void SearchGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e == null)
            {
                return;
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 1; i < e.Row.Cells.Count; i++)
                {
                    string cellText = e.Row.Cells[i].Text;

                    if (!string.IsNullOrWhiteSpace(cellText))
                    {
                        cellText =
                            ScrudLocalizationHelper.GetResourceString(this.GetResourceClassName(), cellText);
                        e.Row.Cells[i].Text = cellText;
                    }
                }
            }
        }
    }
}