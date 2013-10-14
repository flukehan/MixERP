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
        private void ClearSelectedValue()
        {
            foreach(GridViewRow row in formGridView.Rows)
            {
                HtmlInputRadioButton r = (HtmlInputRadioButton)row.Controls[0].Controls[0];
                if(r.Checked)
                {
                    r.Checked = false;
                }
            }
        }

        private string GetSelectedValue()
        {
            foreach(GridViewRow row in formGridView.Rows)
            {
                HtmlInputRadioButton r = (HtmlInputRadioButton)row.Controls[0].Controls[0];
                if(r.Checked)
                {
                    return r.Value;
                }
            }

            return string.Empty;
        }
    }
}
