/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MixERP.Net.BusinessLayer.Helpers;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory;

namespace MixERP.Net.FrontEnd.Sales.Setup
{
    public partial class AgentBonusSlabs : MixERP.Net.BusinessLayer.BasePageClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScrudForm scrud = new ScrudForm();

            scrud.KeyColumn = "bonus_slab_id";

            scrud.TableSchema = "core";
            scrud.Table = "bonus_slabs";
            scrud.ViewSchema = "core";
            scrud.View = "bonus_slab_view";

            scrud.DisplayFields = this.GetDisplayFields();
            scrud.DisplayViews = this.GetDisplayViews();

            scrud.Text = Resources.Titles.AgentBonusSlabs;

            ToolkitScriptManager1.NamingContainer.Controls.Add(scrud);
        }

        private string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.frequencies.frequency_id", ConfigurationHelper.GetDbParameter("FrequencyDisplayField"));
            return string.Join(",", displayFields);
        }

        private string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.frequencies.frequency_id", "core.frequencies");
            return string.Join(",", displayViews);
        }

    }
}