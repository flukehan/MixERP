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
using MixERP.Net.Entities;
using MixERP.Net.Entities.Core;
using MixERP.Net.Entities.Office;

namespace MixERP.Net.Core.Modules.Sales.Data.Helpers
{
    public static class Accounts
    {
        public static IEnumerable<BankAccount> GetBankAccounts()
        {
            return Factory.Get<BankAccount>("SELECT * FROM core.bank_accounts ORDER BY account_id;");
        }

        public static IEnumerable<CashRepository> GetCashRepositories()
        {
            return Factory.Get<CashRepository>("SELECT * FROM office.cash_repositories ORDER BY cash_repository_id;");
        }

        public static IEnumerable<CostCenter> GetCostCenters()
        {
            return Factory.Get<CostCenter>("SELECT * FROM office.cost_center ORDER BY cost_center_id;");
        }

        public static IEnumerable<FlagType> GetFlagTypes()
        {
            return Factory.Get<FlagType>("SELECT * FROM core.flag_types ORDER BY flag_type_id;");
        }
    }
}