using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MixERP.Net.WebControls.ScrudFactory;

namespace MixERP.Net.FrontEnd.Setup
{
    public partial class Roles : MixERP.Net.BusinessLayer.MixERPWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "role_id";

                scrud.TableSchema = "office";
                scrud.Table = "roles";
                scrud.ViewSchema = "office";
                scrud.View = "roles";

                scrud.Text = Resources.Titles.RoleMaintenance;

                ScriptManager1.NamingContainer.Controls.Add(scrud);
            }
        }
    }
}