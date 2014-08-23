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
using System.Collections.ObjectModel;
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