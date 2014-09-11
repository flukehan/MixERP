

using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Inventory.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.ScrudFactory;
using System;
using System.Collections.Generic;

namespace MixERP.Net.Core.Modules.Inventory.Setup
{
    public partial class Shippers : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "shipper_id";
                scrud.TableSchema = "core";
                scrud.Table = "shippers";
                scrud.ViewSchema = "core";
                scrud.View = "shippers";
                scrud.Width = 5000;

                //The following fields will be automatically generated on the database server.
                scrud.Exclude = "shipper_code, shipper_name";

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();
                scrud.SelectedValues = GetSelectedValues();

                scrud.Text = Titles.Shippers;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }

            base.OnControlLoad(sender, e);
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.accounts.account_id", ConfigurationHelper.GetDbParameter("AccountDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.accounts.account_id", "core.account_view");
            return string.Join(",", displayViews);
        }

        private static string GetSelectedValues()
        {
            List<string> selectedValues = new List<string>();

            //Todo:
            //The default selected value of shipping payable account
            //should be done via GL Mapping.
            ScrudHelper.AddSelectedValue(selectedValues, "core.accounts.account_id", "'20110 (Shipping Charge Payable)'");
            return string.Join(",", selectedValues);
        }
    }
}