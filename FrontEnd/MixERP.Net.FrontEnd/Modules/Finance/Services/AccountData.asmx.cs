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

using MixERP.Net.Common.Base;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Finance.Data.Helpers;
using MixERP.Net.Core.Modules.Finance.Resources;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using SessionHelper = MixERP.Net.Common.Helpers.SessionHelper;

namespace MixERP.Net.Core.Modules.Finance.Services
{
    /// <summary>
    /// Summary description for AccountData
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the
    // following line. [System.Web.Script.Services.ScriptService]
    [ScriptService]
    public class AccountData : WebService
    {
        [WebMethod]
        public bool AccountNumberExists(string accountNumber)
        {
            if (!string.IsNullOrWhiteSpace(accountNumber))
            {
                return Accounts.AccountNumberExists(accountNumber);
            }

            return false;
        }

        [WebMethod]
        public bool CashRepositoryCodeExists(string cashRepositoryCode)
        {
            return CashRepositories.CashRepositoryCodeExists(cashRepositoryCode);
        }

        [WebMethod(EnableSession = true)]
        public Collection<ListItem> GetAccounts()
        {
            if (Switches.AllowParentAccountInGlTransaction())
            {
                if (SessionHelper.IsAdmin())
                {
                    using (DataTable table = Accounts.GetAccounts())
                    {
                        return GetValues(table);
                    }
                }

                using (DataTable table = Accounts.GetNonConfidentialAccounts())
                {
                    return GetValues(table);
                }
            }

            if (SessionHelper.IsAdmin())
            {
                using (DataTable table = Accounts.GetChildAccounts())
                {
                    return GetValues(table);
                }
            }

            using (DataTable table = Accounts.GetNonConfidentialChildAccounts())
            {
                return GetValues(table);
            }
        }

        [WebMethod(EnableSession = true)]
        public Collection<ListItem> GetCashRepositories()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            int officeId = SessionHelper.GetOfficeId();

            using (DataTable table = CashRepositories.GetCashRepositoryDataTable(officeId))
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

        [WebMethod(EnableSession = true)]
        public Collection<ListItem> GetCashRepositoriesByAccountNumber(string accountNumber)
        {
            Collection<ListItem> values = new Collection<ListItem>();

            if (Accounts.IsCashAccount(accountNumber))
            {
                int officeId = SessionHelper.GetOfficeId();
                using (DataTable table = CashRepositories.GetCashRepositoryDataTable(officeId))
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
        public decimal GetCashRepositoryBalance(int cashRepositoryId, string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
            {
                return CashRepositories.GetBalance(cashRepositoryId);
            }

            return CashRepositories.GetBalance(cashRepositoryId, currencyCode);
        }

        [WebMethod]
        public Collection<ListItem> GetCostCenters()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            using (DataTable table = CostCenters.GetCostCenterDataTable())
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
        public Collection<ListItem> GetCurrencies()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            using (DataTable table = Currencies.GetCurrencyDataTable())
            {
                foreach (DataRow dr in table.Rows)
                {
                    values.Add(new ListItem(dr["currency_code"].ToString(), dr["currency_code"].ToString()));
                }
            }
            return values;
        }

        [WebMethod]
        public Collection<ListItem> GetCurrenciesByAccountNumber(string accountNumber)
        {
            Collection<ListItem> values = new Collection<ListItem>();

            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                return values;
            }

            using (DataTable table = Currencies.GetCurrencyDataTable(accountNumber))
            {
                foreach (DataRow dr in table.Rows)
                {
                    values.Add(new ListItem(dr["currency_code"].ToString(), dr["currency_code"].ToString()));
                }
            }

            return values;
        }

        [WebMethod]
        public bool HasBalance(string cashRepositoryCode, string currencyCode, decimal credit)
        {
            if (credit.Equals(0))
            {
                return true;
            }

            if (credit < 0)
            {
                throw new MixERPException(Errors.NegativeValueSupplied);
            }

            decimal balance = CashRepositories.GetBalance(cashRepositoryCode, currencyCode);

            if (balance > credit)
            {
                return true;
            }

            return false;
        }

        [WebMethod]
        public bool IsCashAccount(string accountNumber)
        {
            if (!string.IsNullOrWhiteSpace(accountNumber))
            {
                return Accounts.IsCashAccount(accountNumber);
            }

            return false;
        }

        [WebMethod(EnableSession = true)]
        public Collection<ListItem> ListAccounts()
        {
            if (SessionHelper.IsAdmin())
            {
                using (DataTable table = Accounts.GetAccounts())
                {
                    return GetValues(table);
                }
            }

            using (DataTable table = Accounts.GetNonConfidentialAccounts())
            {
                return GetValues(table);
            }
        }

        private static Collection<ListItem> GetValues(DataTable table)
        {
            Collection<ListItem> values = new Collection<ListItem>();

            foreach (DataRow dr in table.Rows)
            {
                values.Add(new ListItem(dr["account_name"].ToString(), dr["account_number"].ToString()));
            }

            return values;
        }
    }
}