

using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Inventory.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.ScrudFactory;
using System;
using System.Collections.Generic;

namespace MixERP.Net.Core.Modules.Inventory.Setup
{
    public partial class ShippingAddresses : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "shipping_address_id";
                scrud.TableSchema = "core";
                scrud.Table = "shipping_addresses";
                scrud.ViewSchema = "core";
                scrud.View = "shipping_address_view";

                //Shipping address code will be automatically generated on the database.
                scrud.Exclude = "shipping_address_code";

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();

                scrud.Text = Titles.ShippingAddressMaintenance;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }

            base.OnControlLoad(sender, e);
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.parties.party_id", ConfigurationHelper.GetDbParameter("PartyDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.parties.party_id", "core.party_view");
            return string.Join(",", displayViews);
        }
    }
}