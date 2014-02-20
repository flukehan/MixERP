using System;
using MixERP.Net.BusinessLayer;
using MixERP.Net.WebControls.ScrudFactory;
using Resources;

namespace MixERP.Net.FrontEnd.Setup
{
    public partial class FiscalYear : MixERPWebpage
    {
        protected void Page_Load(object sender, EventArgs e)
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
        }
    }
}