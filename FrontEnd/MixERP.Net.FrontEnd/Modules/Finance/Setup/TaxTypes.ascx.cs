using MixERP.Net.BusinessLayer;
using MixERP.Net.Core.Modules.Finance.Resources;
using MixERP.Net.WebControls.ScrudFactory;
using System;

namespace MixERP.Net.Core.Modules.Finance.Setup
{
    public partial class TaxTypes : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "tax_type_id";

                scrud.TableSchema = "core";
                scrud.Table = "tax_types";
                scrud.ViewSchema = "core";
                scrud.View = "tax_types";

                scrud.Text = Titles.TaxTypes;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }

            base.OnControlLoad(sender, e);
        }
    }
}