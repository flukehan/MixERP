
using MixERP.Net.Core.Modules.Finance.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.ScrudFactory;
using System;

namespace MixERP.Net.Core.Modules.Finance.Setup
{
    public partial class Currencies : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "currency_code";
                scrud.TableSchema = "core";
                scrud.Table = "currencies";
                scrud.ViewSchema = "core";
                scrud.View = "currencies";

                scrud.Text = Titles.Currencies;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }

            base.OnControlLoad(sender, e);
        }
    }
}