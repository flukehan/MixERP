/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System.Collections.ObjectModel;
/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System.ComponentModel;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using MixERP.Net.BusinessLayer.Core;
using MixERP.Net.Common.Helpers;
using FormHelper = MixERP.Net.BusinessLayer.Helpers.FormHelper;
using SessionHelper = MixERP.Net.BusinessLayer.Helpers.SessionHelper;

namespace MixERP.Net.FrontEnd.Services
{
    /// <summary>
    /// Summary description for AccountData
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [ScriptService]
    public class AccountData : WebService
    {
        [WebMethod(EnableSession = true)]
        public Collection<ListItem> GetAccounts()
        {
            if (Switches.AllowParentAccountInGlTransaction())
            {
                if (SessionHelper.IsAdmin())
                {
                    using (DataTable table = FormHelper.GetTable("core", "accounts"))
                    {
                        return GetValues(table);
                    }
                }

                using (DataTable table = FormHelper.GetTable("core", "accounts", "confidential", "0"))
                {
                    return GetValues(table);
                }
            }

            if (SessionHelper.IsAdmin())
            {
                using (DataTable table = FormHelper.GetTable("core", "account_view", "has_child", "0"))
                {
                    return GetValues(table);
                }
            }

            using (DataTable table = FormHelper.GetTable("core", "account_view", "has_child, confidential", "0, 0"))
            {
                return GetValues(table);
            }
        }

        private static Collection<ListItem> GetValues(DataTable table)
        {
            Collection<ListItem> values = new Collection<ListItem>();

            foreach (DataRow dr in table.Rows)
            {
                values.Add(new ListItem(dr["account_name"].ToString(), dr["account_code"].ToString()));
            }

            return values;
        }


        [WebMethod]
        public Collection<ListItem> GetCashRepositories()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            using (DataTable table = FormHelper.GetTable("office", "cash_repositories"))
            {
                string displayField = ConfigurationHelper.GetDbParameter("CashRepositoryDisplayField");
                table.Columns.Add("cash_repository", typeof(string), displayField);

                foreach (DataRow dr in table.Rows)
                {
                    values.Add(new ListItem(dr["cash_repository"].ToString(), dr["cash_repository_id"].ToString()));
                }
            }
            return values;
        }

        [WebMethod]
        public Collection<ListItem> GetCostCenters()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            using (DataTable table = FormHelper.GetTable("office", "cost_centers"))
            {
                string displayField = ConfigurationHelper.GetDbParameter("CostCenterDisplayField");
                table.Columns.Add("cost_center", typeof(string), displayField);

                foreach (DataRow dr in table.Rows)
                {
                    values.Add(new ListItem(dr["cost_center"].ToString(), dr["cost_center_id"].ToString()));
                }
            }

            return values;
        }


        [WebMethod]
        public Collection<ListItem> GetCashRepositoriesByAccountCode(string accountCode)
        {
            Collection<ListItem> values = new Collection<ListItem>();

            if (Accounts.IsCashAccount(accountCode))
            {
                using (DataTable table = FormHelper.GetTable("office", "cost_centers"))
                {
                    string displayField = ConfigurationHelper.GetDbParameter("CashRepositoryDisplayField");
                    table.Columns.Add("cash_repository", typeof(string), displayField);

                    foreach (DataRow dr in table.Rows)
                    {
                        values.Add(new ListItem(dr["cash_repository"].ToString(), dr["cash_repository_code"].ToString()));
                    }
                }
            }

            return values;
        }

        [WebMethod]
        public decimal GetCashRepositoryBalance(int cashRepositoryId)
        {
            return BusinessLayer.Office.CashRepositories.GetBalance(cashRepositoryId);
        }

    }
}