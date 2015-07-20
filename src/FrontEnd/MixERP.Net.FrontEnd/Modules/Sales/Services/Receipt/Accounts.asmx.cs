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

using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.Entities.Core;
using MixERP.Net.Entities.Office;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace MixERP.Net.Core.Modules.Sales.Services.Receipt
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class Accounts : WebService
    {
        [WebMethod]
        public Collection<ListItem> GetBankAccounts()
        {
            int officeId = AppUsers.GetCurrent().View.OfficeId.ToInt();

            Collection<ListItem> values = new Collection<ListItem>();

            foreach (BankAccount bankAccount in Data.Helpers.Accounts.GetBankAccounts(AppUsers.GetCurrentUserDB(), officeId))
            {
                values.Add(new ListItem(bankAccount.BankName + " (" + bankAccount.BankAccountNumber + ")", bankAccount.AccountId.ToString(CultureInfo.InvariantCulture)));
            }

            return values;
        }

        [WebMethod]
        public Collection<ListItem> GetPaymentCards(long merchantAccountId)
        {
            Collection<ListItem> values = new Collection<ListItem>();

            bool isMerchantAccount = Data.Helpers.Accounts.IsMerchantAccount(AppUsers.GetCurrentUserDB(), merchantAccountId);

            if (!isMerchantAccount)
            {
                return values;
            }

            foreach (PaymentCard card in Data.Helpers.Accounts.GetPaymentCards(AppUsers.GetCurrentUserDB()))
            {
                values.Add(new ListItem(card.PaymentCardName, card.PaymentCardId.ToString(CultureInfo.InvariantCulture)));
            }

            return values;
        }

        [WebMethod]
        public MerchantFeeSetup GetMerchantFeeSetup(long merchantAccountId, int paymentCardId)
        {
            return Data.Helpers.Accounts.GetMerchantFeeSetup(AppUsers.GetCurrentUserDB(), merchantAccountId, paymentCardId);
        }


        [WebMethod]
        public Collection<ListItem> GetCashRepositories()
        {
            int officeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
            Collection<ListItem> values = new Collection<ListItem>();

            foreach (CashRepository cashRepository in Data.Helpers.Accounts.GetCashRepositories(AppUsers.GetCurrentUserDB(), officeId))
            {
                values.Add(new ListItem(cashRepository.CashRepositoryName, cashRepository.CashRepositoryId.ToString(CultureInfo.InvariantCulture)));
            }

            return values;
        }

        [WebMethod]
        public Collection<ListItem> GetCostCenters()
        {
            Collection<ListItem> values = new Collection<ListItem>();


            foreach (CostCenter costCenter in Data.Helpers.Accounts.GetCostCenters(AppUsers.GetCurrentUserDB()))
            {
                values.Add(new ListItem(costCenter.CostCenterName, costCenter.CostCenterId.ToString(CultureInfo.InvariantCulture)));
            }

            return values;
        }

        [WebMethod]
        public Collection<ListItem> GetFlags()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            foreach (FlagType flagType in Data.Helpers.Accounts.GetFlagTypes(AppUsers.GetCurrentUserDB()))
            {
                values.Add(new ListItem(flagType.FlagTypeName, flagType.FlagTypeId.ToString(CultureInfo.InvariantCulture)));
            }

            return values;
        }
    }
}