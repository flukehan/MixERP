using MixERP.Net.BusinessLayer;
using MixERP.Net.WebControls.ScrudFactory;
using System;

namespace MixERP.Net.Core.Modules.BackOffice
{
    public partial class Roles : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "role_id";

                scrud.TableSchema = "office";
                scrud.Table = "roles";
                scrud.ViewSchema = "office";
                scrud.View = "roles";

                scrud.Text = Resources.Titles.Roles;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }

            base.OnControlLoad(sender, e);
        }
    }
}