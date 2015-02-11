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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using MixERP.Net.Common.Base;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Core.Modules.Finance.Data.Helpers;
using MixERP.Net.Core.Modules.Finance.Resources;
using MixERP.Net.Entities.Core;
using MixERP.Net.Entities.Office;

namespace MixERP.Net.Core.Modules.Finance.Services
{
    /// <summary>
    ///     Summary description for AccountData
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
                return AccountHelper.AccountNumberExists(accountNumber);
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
                if (CurrentSession.IsAdmin())
                {
                    return GetValues(AccountHelper.GetAccounts());
                }


                return GetValues(AccountHelper.GetNonConfidentialAccounts());
            }

            if (CurrentSession.IsAdmin())
            {
                return GetValues(AccountHelper.GetChildAccounts());
            }

            return GetValues(AccountHelper.GetNonConfidentialChildAccounts());
        }

        [WebMethod(EnableSession = true)]
        public Collection<ListItem> GetCashRepositories()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            int officeId = CurrentSession.GetOfficeId();

            foreach (CashRepository cashRepository in CashRepositories.GetCashRepositories(officeId))
            {
                values.Add(new ListItem(cashRepository.CashRepositoryName, cashRepository.CashRepositoryId.ToString(CultureInfo.InvariantCulture)));
            }
            return values;
        }

        [WebMethod(EnableSession = true)]
        public Collection<ListItem> GetCashRepositoriesByAccountNumber(string accountNumber)
        {
            Collection<ListItem> values = new Collection<ListItem>();

            if (AccountHelper.IsCashAccount(accountNumber))
            {
                int officeId = CurrentSession.GetOfficeId();
                foreach (CashRepository cashRepository in CashRepositories.GetCashRepositories(officeId))
                {
                    values.Add(new ListItem(cashRepository.CashRepositoryName, cashRepository.CashRepositoryCode));
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


            foreach (CostCenter costCenter in CostCenters.GetCostCenters())
            {
                values.Add(new ListItem(costCenter.CostCenterName, costCenter.CostCenterId.ToString(CultureInfo.InvariantCulture)));
            }

            return values;
        }

        [WebMethod]
        public Collection<ListItem> GetCurrencies()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            foreach (Currency currency in Currencies.GetCurrencies())
            {
                values.Add(new ListItem(currency.CurrencyCode, currency.CurrencyCode));
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

            string currencyCode = Currencies.GetCurrencyCode(accountNumber);

            values.Add(new ListItem(currencyCode, currencyCode));

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
                throw new MixERPException(Warnings.NegativeValueSupplied);
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
                return AccountHelper.IsCashAccount(accountNumber);
            }

            return false;
        }

        [WebMethod(EnableSession = true)]
        public Collection<ListItem> ListAccounts()
        {
            if (CurrentSession.IsAdmin())
            {
                return GetValues(AccountHelper.GetAccounts());
            }

            return GetValues(AccountHelper.GetNonConfidentialAccounts());
        }

        private static Collection<ListItem> GetValues(IEnumerable<Account> accounts)
        {
            Collection<ListItem> values = new Collection<ListItem>();

            foreach (Account account in accounts)
            {
                values.Add(new ListItem(account.AccountName, account.AccountNumber));
            }

            return values;
        }
    }
}