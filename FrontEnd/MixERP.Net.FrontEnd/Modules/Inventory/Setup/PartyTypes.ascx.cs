
using MixERP.Net.Core.Modules.Inventory.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.ScrudFactory;
using System;

namespace MixERP.Net.Core.Modules.Inventory.Setup
{
    public partial class PartyTypes : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "party_type_id";

                scrud.TableSchema = "core";
                scrud.Table = "party_types";
                scrud.ViewSchema = "core";
                scrud.View = "party_types";

                scrud.Text = Titles.PartyTypes;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }

            base.OnControlLoad(sender, e);
        }
    }
}