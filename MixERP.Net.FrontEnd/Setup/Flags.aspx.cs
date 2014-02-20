using System;
using System.Diagnostics.CodeAnalysis;
using MixERP.Net.BusinessLayer;
using MixERP.Net.WebControls.ScrudFactory;
using Resources;

namespace MixERP.Net.FrontEnd.Setup
{
    [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flags")]
    public partial class Flags : MixERPWebpage
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

                scrud.Text = Titles.Flags;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }
        }
    }
}