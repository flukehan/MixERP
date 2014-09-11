

using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Finance.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.ScrudFactory;
using System;
using System.Collections.Generic;

namespace MixERP.Net.Core.Modules.Finance.Setup
{
    public partial class BankAccounts : MixERPUserControl
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            using (ScrudForm scrud = new ScrudForm())
            {
                scrud.KeyColumn = "account_id";

                scrud.TableSchema = "core";
                scrud.Table = "bank_accounts";
                scrud.ViewSchema = "core";
                scrud.View = "bank_accounts";

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();

                scrud.Text = Titles.BankAccounts;

                this.ScrudPlaceholder.Controls.Add(scrud);
            }

            base.OnControlLoad(sender, e);
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "office.users.user_id", ConfigurationHelper.GetDbParameter("UserDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.accounts.account_id", ConfigurationHelper.GetDbParameter("AccountDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "office.users.user_id", "office.user_view");
            ScrudHelper.AddDisplayView(displayViews, "core.accounts.account_id", "core.account_view");
            return string.Join(",", displayViews);
        }
    }
}