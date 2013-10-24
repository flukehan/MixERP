/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MixERP.Net.BusinessLayer.Helpers;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory;


namespace MixERP.Net.FrontEnd.Finance.Setup
{
    public partial class COA : MixERP.Net.BusinessLayer.BasePageClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScrudForm scrud = new ScrudForm();

            scrud.TableSchema = "core";
            scrud.Table = "accounts";
            scrud.KeyColumn = "account_id";
            scrud.ViewSchema = "core";
            scrud.View = "account_view";

            scrud.Width = 1500;
            scrud.Exclude = "sys_type";

            scrud.DisplayFields = GetDisplayFields();
            scrud.DisplayViews = GetDisplayViews();

            scrud.Text = Resources.Titles.ChartOfAccounts;

            this.ToolkitScriptManager1.NamingContainer.Controls.Add(scrud);
        }

        private static string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.account_masters.account_master_id", ConfigurationHelper.GetDbParameter("AccountMasterDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.accounts.account_id", ConfigurationHelper.GetDbParameter("AccountDisplayField"));
            return string.Join(",", displayFields);
        }

        private static string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.account_masters.account_master_id", "core.account_masters");
            ScrudHelper.AddDisplayView(displayViews, "core.accounts.account_id", "core.account_view");
            return string.Join(",", displayViews);
        }
    }
}