
using MixERP.Net.Core.Modules.POS.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.ScrudFactory;
using System;

namespace MixERP.Net.Core.Modules.POS.Setup
{
    public partial class StoreTypes : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "store_type_id";

                scrud.TableSchema = "office";
                scrud.Table = "store_types";
                scrud.ViewSchema = "office";
                scrud.View = "store_types";

                scrud.Text = Titles.StoreTypes;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }

            base.OnControlLoad(sender, e);
        }
    }
}