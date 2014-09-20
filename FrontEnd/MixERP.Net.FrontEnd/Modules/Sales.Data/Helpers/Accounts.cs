using MixERP.Net.DBFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Core.Modules.Sales.Data.Helpers
{
    public static class Accounts
    {
        public static DataTable GetCashRepositories()
        {
            return FormHelper.GetTable("office", "cash_repositories", "cash_repository_id");
        }

        public static DataTable GetCostCenters()
        {
            return FormHelper.GetTable("office", "cost_centers", "cost_center_id");
        }

        public static DataTable GetBankAccounts()
        {
            return FormHelper.GetTable("core", "bank_accounts", "account_id");
        }

        public static DataTable GetFlags()
        {
            return FormHelper.GetTable("core", "flag_types", "flag_type_id");
        }
    }
}