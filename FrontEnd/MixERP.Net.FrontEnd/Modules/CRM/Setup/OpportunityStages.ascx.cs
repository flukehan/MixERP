
using MixERP.Net.Core.Modules.CRM.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.ScrudFactory;
using System;

namespace MixERP.Net.Core.Modules.CRM.Setup
{
    public partial class OpportunityStages : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "opportunity_stage_id";

                scrud.TableSchema = "crm";
                scrud.Table = "opportunity_stages";
                scrud.ViewSchema = "crm";
                scrud.View = "opportunity_stages";

                scrud.Text = Titles.OpportunityStages;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }

            base.OnControlLoad(sender, e);
        }
    }
}