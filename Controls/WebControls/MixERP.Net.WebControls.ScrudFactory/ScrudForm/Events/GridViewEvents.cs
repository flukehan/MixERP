/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MixERP.Net.Common.Helpers;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        protected void FormGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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
                        //radio.Attributes.Add("onclick", "selectNode(this.id);");
                        e.Row.Cells[0].Controls.Add(radio);
                    }
                    break;
                case DataControlRowType.Header:
                    for(var i = 1; i < e.Row.Cells.Count; i++)
                    {
                        var cellText = e.Row.Cells[i].Text;

                        cellText = LocalizationHelper.GetResourceString(this.GetResourceClassName(), cellText);
                        e.Row.Cells[i].Text = cellText;
                    }
                    break;
            }
        }
    }
}
