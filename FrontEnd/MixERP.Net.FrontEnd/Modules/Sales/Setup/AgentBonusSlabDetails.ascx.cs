using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Sales.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.ScrudFactory;
using System;
using System.Collections.Generic;

namespace MixERP.Net.Core.Modules.Sales.Setup
{
    public partial class AgentBonusSlabDetails : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "bonus_slab_detail_id";
                scrud.TableSchema = "core";
                scrud.Table = "bonus_slab_details";
                scrud.ViewSchema = "core";
                scrud.View = "bonus_slab_detail_view";
                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();
                scrud.Text = Titles.BonusSlabDetails;
                this.ScrudPlaceholder.Controls.Add(scrud);
            }

            base.OnControlLoad(sender, e);
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.bonus_slabs.bonus_slab_id", ConfigurationHelper.GetDbParameter("BonusSlabDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.bonus_slabs.bonus_slab_id", "core.bonus_slabs");
            return string.Join(",", displayViews);
        }
    }
}