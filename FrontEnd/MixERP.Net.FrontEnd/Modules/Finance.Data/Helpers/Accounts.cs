using MixERP.Net.DBFactory;
using Npgsql;

namespace MixERP.Net.Core.Modules.Finance.Data.Helpers
{
    public static class Accounts
    {
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
    }
}