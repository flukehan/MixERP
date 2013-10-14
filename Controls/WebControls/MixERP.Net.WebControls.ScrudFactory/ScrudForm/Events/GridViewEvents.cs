/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm : CompositeControl
    {
        protected void FormGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlInputRadioButton radio = new HtmlInputRadioButton();
                radio.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                radio.Name = "SelectRadio";
                radio.ID = "SelectRadio";
                radio.ClientIDMode = System.Web.UI.ClientIDMode.Predictable;
                radio.Value = e.Row.Cells[1].Text;
                radio.Attributes.Add("onclick", "selected(this.id);");
                e.Row.Cells[0].Controls.Add(radio);
            }
            else if(e.Row.RowType == DataControlRowType.Header)
            {
                for(int i = 0; i < e.Row.Cells.Count; i++)
                {
                    string cellText = e.Row.Cells[i].Text;

                    cellText = MixERP.Net.Common.Helpers.LocalizationHelper.GetResourceString("FormResource", cellText);
                    e.Row.Cells[i].Text = cellText;
                }
            }
        }
    }
}
