/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using MixERP.Net.BusinessLayer;
using MixERP.Net.WebControls.ScrudFactory;
using Resources;

namespace MixERP.Net.FrontEnd.Finance.Setup
{
    public partial class CostCenters : MixERPWebpage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "cost_center_id";
                scrud.TableSchema = "office";
                scrud.Table = "cost_centers";
                scrud.ViewSchema = "office";
                scrud.View = "cost_center_view";

                scrud.Text = Titles.CostCenters;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }
        }
    }
}