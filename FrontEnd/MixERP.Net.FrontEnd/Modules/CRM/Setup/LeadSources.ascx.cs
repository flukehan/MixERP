
using MixERP.Net.Core.Modules.CRM.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.ScrudFactory;
using System;

namespace MixERP.Net.Core.Modules.CRM.Setup
{
    public partial class LeadSources : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
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

            base.OnControlLoad(sender, e);
        }
    }
}