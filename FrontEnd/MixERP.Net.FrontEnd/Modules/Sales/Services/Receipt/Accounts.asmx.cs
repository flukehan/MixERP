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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using MixERP.Net.Entities.Core;
using MixERP.Net.Entities.Office;


namespace MixERP.Net.Core.Modules.Sales.Services.Receipt
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class Accounts : WebService
    {
        [WebMethod]
        public Collection<ListItem> GetCashRepositories()
        {
            Collection<ListItem> values = new Collection<ListItem>();


            foreach (CashRepository cashRepository in Data.Helpers.Accounts.GetCashRepositories())
            {
                values.Add(new ListItem(cashRepository.CashRepositoryName, cashRepository.CashRepositoryId.ToString(CultureInfo.InvariantCulture)));
            }

            return values;
        }

        [WebMethod]
        public Collection<ListItem> GetFlags()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            foreach (FlagType flagType in Data.Helpers.Accounts.GetFlagTypes())
            {
                values.Add(new ListItem(flagType.FlagTypeName, flagType.FlagTypeId.ToString(CultureInfo.InvariantCulture)));
            }

            return values;
        }

        [WebMethod]
        public Collection<ListItem> GetCostCenters()
        {
            Collection<ListItem> values = new Collection<ListItem>();


            foreach (CostCenter costCenter in Data.Helpers.Accounts.GetCostCenters())
            {
                values.Add(new ListItem(costCenter.CostCenterName, costCenter.CostCenterId.ToString(CultureInfo.InvariantCulture)));
            }

            return values;
        }

        [WebMethod]
        public Collection<ListItem> GetBankAccounts()
        {
            Collection<ListItem> values = new Collection<ListItem>();

            foreach (BankAccount bankAccount in Data.Helpers.Accounts.GetBankAccounts())
            {
                values.Add(new ListItem(bankAccount.BankAccountNumber + " (" + bankAccount.BankAccountNumber + ")", bankAccount.AccountId.ToString(CultureInfo.InvariantCulture)));
            }

            return values;
        }
    }
}