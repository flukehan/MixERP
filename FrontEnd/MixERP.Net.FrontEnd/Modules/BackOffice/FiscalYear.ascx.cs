using MixERP.Net.BusinessLayer;
using MixERP.Net.Core.Modules.BackOffice.Resources;
using MixERP.Net.WebControls.ScrudFactory;
using System;

namespace MixERP.Net.Core.Modules.BackOffice
{
    public partial class FiscalYear : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "fiscal_year_code";

                scrud.TableSchema = "core";
                scrud.Table = "fiscal_year";
                scrud.ViewSchema = "core";
                scrud.View = "fiscal_year";

                scrud.Text = Titles.FiscalYear;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }

            base.OnControlLoad(sender, e);
        }
    }
}