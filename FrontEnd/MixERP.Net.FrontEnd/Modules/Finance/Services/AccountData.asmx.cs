using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Finance.Data.Helpers;
using System;
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
        public bool AccountCodeExists(string accountCode)
        {
            if (!string.IsNullOrWhiteSpace(accountCode))
            {
                return Accounts.AccountCodeExists(accountCode);
            }

            return false;
        }

        [WebMethod]
        public bool CashRepositoryCodeExists(string cashRepositoryCode)
        {
            return CashRepositories.CashRepositoryCodeExists(cashRepositoryCode);
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
                throw new InvalidOperationException("Negetive value supplied.");
            }

            decimal balance = CashRepositories.GetBalance(cashRepositoryCode, currencyCode);

            if (balance > credit)
            {
                return true;
            }

            return false;
        }

        [WebMethod]
        public Collection<ListItem> GetCashRepositories()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            using (DataTable table = CashRepositories.GetCashRepositoryDataTable())
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
        public Collection<ListItem> GetCurrenciesByAccountCode(string accountCode)
        {
            Collection<ListItem> values = new Collection<ListItem>();

            if (string.IsNullOrWhiteSpace(accountCode))
            {
                return values;
            }

            using (DataTable table = Currencies.GetCurrencyDataTable(accountCode))
            {
                foreach (DataRow dr in table.Rows)
                {
                    values.Add(new ListItem(dr["currency_code"].ToString(), dr["currency_code"].ToString()));
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
        public bool IsCashAccount(string accountCode)
        {
            if (!string.IsNullOrWhiteSpace(accountCode))
            {
                return Accounts.IsCashAccount(accountCode);
            }

            return false;
        }

        [WebMethod]
        public Collection<ListItem> GetCashRepositoriesByAccountCode(string accountCode)
        {
            Collection<ListItem> values = new Collection<ListItem>();

            if (Accounts.IsCashAccount(accountCode))
            {
                using (DataTable table = CashRepositories.GetCashRepositoryDataTable())
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
    }
}