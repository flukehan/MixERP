using MixERP.Net.BusinessLayer;
using MixERP.Net.Core.Modules.Inventory.Resources;
using MixERP.Net.WebControls.ScrudFactory;
using System;

namespace MixERP.Net.Core.Modules.Inventory.Setup
{
    public partial class Brands : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "brand_id";

                scrud.TableSchema = "core";
                scrud.Table = "brands";
                scrud.ViewSchema = "core";
                scrud.View = "brands";

                scrud.Text = Titles.Brands;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }

            base.OnControlLoad(sender, e);
        }
    }
}