using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MixERP.Net.WebControls.ScrudFactory;

namespace MixERP.Net.FrontEnd.Setup
{
    public partial class FiscalYear : MixERP.Net.BusinessLayer.MixERPWebPage
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

                scrud.Text = Resources.Titles.FiscalYear;

                ScriptManager1.NamingContainer.Controls.Add(scrud);
            }
        }
    }
}