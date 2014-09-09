using MixERP.Net.BusinessLayer;
using MixERP.Net.Core.Modules.Finance.Resources;
using MixERP.Net.WebControls.ScrudFactory;
using System;

namespace MixERP.Net.Core.Modules.Finance.Setup
{
    public partial class AgeingSlabs : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "ageing_slab_id";

                scrud.TableSchema = "core";
                scrud.Table = "ageing_slabs";
                scrud.ViewSchema = "core";
                scrud.View = "ageing_slabs";

                scrud.Text = Titles.AgeingSlabs;
                this.ScrudPlaceholder.Controls.Add(scrud);
            }

            base.OnControlLoad(sender, e);
        }
    }
}