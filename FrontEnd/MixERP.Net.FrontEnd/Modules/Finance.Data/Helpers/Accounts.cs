using MixERP.Net.DBFactory;
using Npgsql;
using System.Data;

namespace MixERP.Net.Core.Modules.Finance.Data.Helpers
{
    public static class Accounts
    {
        public static DataTable GetAccounts()
        {
            return FormHelper.GetTable("core", "accounts");
        }

        public static bool AccountCodeExists(string accountCode)
        {
            const string sql = "SELECT 1 FROM core.accounts WHERE account_code=@AccountCode;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@AccountCode", accountCode);

                return DbOperations.GetDataTable(command).Rows.Count.Equals(1);
            }
        }

        public static bool IsCashAccount(int accountId)
        {
            const string sql = "SELECT 1 FROM core.accounts WHERE is_cash=true AND account_id=@AccountId;";

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@AccountId", accountId);

                return DbOperations.GetDataTable(command).Rows.Count.Equals(1);
            }
        }

        public static bool IsCashAccount(string accountCode)
        {
            const string sql = "SELECT 1 FROM core.accounts WHERE is_cash=true AND account_code=@AccountCode;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@AccountCode", accountCode);

                return DbOperations.GetDataTable(command).Rows.Count.Equals(1);
            }
        }

        public static DataTable GetNonConfidentialAccounts()
        {
            return FormHelper.GetTable("core", "accounts", "confidential", "0");
        }

        public static DataTable GetChildAccounts()
        {
            return FormHelper.GetTable("core", "account_view", "has_child", "0");
        }

        public static DataTable GetNonConfidentialChildAccounts()
        {
            return FormHelper.GetTable("core", "account_view", "has_child, confidential", "0, 0");
        }
    }
}