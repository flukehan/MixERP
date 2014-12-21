/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Finance.Resources;
using MixERP.Net.FrontEnd.Base;
using MixERP.Net.WebControls.ScrudFactory;
using System;
using System.Collections.Generic;
using System.Reflection;

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
                scrud.View = "account_scrud_view";

                scrud.Exclude = "sys_type";

                scrud.DisplayFields = GetDisplayFields();
                scrud.DisplayViews = GetDisplayViews();
                scrud.Text = Titles.ChartOfAccounts;
                scrud.ResourceAssembly = Assembly.GetAssembly(typeof(COA));

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
            ScrudHelper.AddDisplayField(displayFields, "core.cash_flow_headings.cash_flow_heading_id", ConfigurationHelper.GetDbParameter("CashFlowHeadingField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.account_masters.account_master_id", "core.account_master_selector_view");
            ScrudHelper.AddDisplayView(displayViews, "core.accounts.account_id", "core.account_selector_view");
            ScrudHelper.AddDisplayView(displayViews, "core.currencies.currency_code", "core.currency_selector_view");
            ScrudHelper.AddDisplayView(displayViews, "core.cash_flow_headings.cash_flow_heading_id", "core.cash_flow_headings");
            return string.Join(",", displayViews);
        }
    }
}