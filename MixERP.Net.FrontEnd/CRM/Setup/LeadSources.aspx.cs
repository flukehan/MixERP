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

namespace MixERP.Net.FrontEnd.CRM.Setup
{
    public partial class LeadSources : MixERPWebpage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "lead_source_id";

                scrud.TableSchema = "crm";
                scrud.Table = "lead_sources";
                scrud.ViewSchema = "crm";
                scrud.View = "lead_sources";

                scrud.Text = Titles.LeadSources;
                this.ScrudPlaceholder.Controls.Add(scrud);
            }
        }
    }
}