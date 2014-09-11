
using MixERP.Net.Core.Modules.BackOffice.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.ScrudFactory;
using System;

namespace MixERP.Net.Core.Modules.BackOffice
{
    public partial class Departments : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "department_id";

                scrud.TableSchema = "office";
                scrud.Table = "departments";
                scrud.ViewSchema = "office";
                scrud.View = "departments";

                scrud.Text = Titles.Departments;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }

            base.OnControlLoad(sender, e);
        }
    }
}