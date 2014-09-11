
using MixERP.Net.Core.Modules.Inventory.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.ScrudFactory;
using System;

namespace MixERP.Net.Core.Modules.Inventory.Setup
{
    public partial class UOM : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "unit_id";

                scrud.TableSchema = "core";
                scrud.Table = "units";
                scrud.ViewSchema = "core";
                scrud.View = "units";

                scrud.Text = Titles.UnitsOfMeasure;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }

            base.OnControlLoad(sender, e);
        }
    }
}