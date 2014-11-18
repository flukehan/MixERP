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

using MixERP.Net.DBFactory;
using System.Data;

namespace MixERP.Net.Core.Modules.Sales.Data.Helpers
{
    public static class Accounts
    {
        public static DataTable GetBankAccounts()
        {
            return FormHelper.GetTable("core", "bank_accounts", "account_id");
        }

        public static DataTable GetCashRepositories()
        {
            return FormHelper.GetTable("office", "cash_repositories", "cash_repository_id");
        }

        public static DataTable GetCostCenters()
        {
            return FormHelper.GetTable("office", "cost_centers", "cost_center_id");
        }

        public static DataTable GetFlags()
        {
            return FormHelper.GetTable("core", "flag_types", "flag_type_id");
        }
    }
}