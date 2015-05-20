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

using MixERP.Net.WebControls.ScrudFactory.Helpers;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        private void FormGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e == null)
            {
                return;
            }

            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    using (var radio = new HtmlInputRadioButton())
                    {
                        radio.ClientIDMode = ClientIDMode.Static;
                        radio.Name = "SelectRadio";
                        radio.ID = "SelectRadio";
                        radio.ClientIDMode = ClientIDMode.Predictable;
                        radio.Value = e.Row.Cells[1].Text;
                        //radio.Attributes.Add("onclick", "scrudSelectRadioById(this.id);");
                        e.Row.Cells[0].Controls.Add(radio);
                    }
                    break;

                case DataControlRowType.Header:
                    for (var i = 1; i < e.Row.Cells.Count; i++)
                    {
                        var cellText = e.Row.Cells[i].Text;

                        cellText = ScrudLocalizationHelper.GetResourceString(this.GetResourceClassName(), cellText);

                        e.Row.Cells[i].Text = cellText;
                    }
                    break;
            }
        }
    }
}