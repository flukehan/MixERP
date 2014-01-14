using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MixERP.Net.WebControls.ScrudFactory;

namespace MixERP.Net.FrontEnd.Setup
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flags")]
    public partial class Flags : MixERP.Net.BusinessLayer.MixERPWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "flag_type_id";

                scrud.TableSchema = "core";
                scrud.Table = "flag_types";
                scrud.ViewSchema = "core";
                scrud.View = "flag_types";

                scrud.Text = Resources.Titles.Flags;

                ScriptManager1.NamingContainer.Controls.Add(scrud);
            }
        }
    }
}