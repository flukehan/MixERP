using MixERP.Net.BusinessLayer;
using MixERP.Net.BusinessLayer.Helpers;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory;
using System;
using System.Collections.Generic;

namespace MixERP.Net.Core.Modules.Sales.Setup
{
    public partial class Agents : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            LoadControl();
            base.OnControlLoad(sender, e);
        }

        protected void LoadControl()
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "agent_id";
                scrud.TableSchema = "core";
                scrud.Table = "agents";
                scrud.ViewSchema = "core";
                scrud.View = "agent_view";

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();
                scrud.SelectedValues = GetSelectedValues();

                scrud.Text = LocalizationHelper.GetResourceString("Titles", "AgentSetup");

                this.Controls.Add(scrud);
            }
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
            //The default selected value of agent account
            //should be implemented via GL Mapping.
            ScrudHelper.AddSelectedValue(selectedValues, "core.accounts.account_id", "'20100 (Accounts Payable)'");
            return string.Join(",", selectedValues);
        }
    }
}