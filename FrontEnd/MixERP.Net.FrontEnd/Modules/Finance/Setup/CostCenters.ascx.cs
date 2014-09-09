using MixERP.Net.BusinessLayer;
using MixERP.Net.Core.Modules.Finance.Resources;
using MixERP.Net.WebControls.ScrudFactory;
using System;

namespace MixERP.Net.Core.Modules.Finance.Setup
{
    public partial class CostCenters : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
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

            base.OnControlLoad(sender, e);
        }
    }
}