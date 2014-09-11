using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Sales.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.ScrudFactory;
using System;
using System.Collections.Generic;

namespace MixERP.Net.Core.Modules.Sales.Setup
{
    public partial class BonusSlabAssignment : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
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
                scrud.Text = Titles.AgentBonusSlabAssignment;
                this.ScrudPlaceholder.Controls.Add(scrud);
            }

            base.OnControlLoad(sender, e);
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