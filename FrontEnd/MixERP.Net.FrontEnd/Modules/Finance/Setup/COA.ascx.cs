

using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Finance.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.ScrudFactory;
using System;
using System.Collections.Generic;

namespace MixERP.Net.Core.Modules.Finance.Setup
{
    public partial class COA : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.TableSchema = "core";
                scrud.Table = "accounts";
                scrud.KeyColumn = "account_id";
                scrud.ViewSchema = "core";
                scrud.View = "account_view";

                scrud.Width = 1500;
                scrud.Exclude = "sys_type";

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();
                scrud.Text = Titles.ChartOfAccounts;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }

            base.OnControlLoad(sender, e);
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.account_masters.account_master_id", ConfigurationHelper.GetDbParameter("AccountMasterDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.accounts.account_id", ConfigurationHelper.GetDbParameter("AccountDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.currencies.currency_code", ConfigurationHelper.GetDbParameter("CurrencyDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.account_masters.account_master_id", "core.account_masters");
            ScrudHelper.AddDisplayView(displayViews, "core.accounts.account_id", "core.account_view");
            ScrudHelper.AddDisplayView(displayViews, "core.currencies.currency_code", "core.currencies");
            return string.Join(",", displayViews);
        }
    }
}