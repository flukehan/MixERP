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
    public partial class BonusSlabAssignment : MixERP.Net.BusinessLayer.BasePageClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "agent_bonus_setup_id";

                scrud.TableSchema = "core";
                scrud.Table = "agent_bonus_setups";
                scrud.ViewSchema = "core";
                scrud.View = "agent_bonus_setup_view";

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();

                scrud.Text = Resources.Titles.AgentBonusSlabAssignment;
                ScriptManager1.NamingContainer.Controls.Add(scrud);
            }
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.bonus_slabs.bonus_slab_id", ConfigurationHelper.GetDbParameter("BonusSlabDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.core.agents.agent_id", ConfigurationHelper.GetDbParameter("AgentDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.bonus_slabs.bonus_slab_id", "core.bonus_slab_view");
            ScrudHelper.AddDisplayView(displayViews, "core.agents.agent_id", "core.agent_view");
            return string.Join(",", displayViews);
        }

    }
}