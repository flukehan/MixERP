using MixERP.Net.DBFactory;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.TransactionGovernor.Data.Verification
{
    public static class Withdrawal
    {
        public static bool WithdrawTransaction(long transactionMasterId, int userId, string reason, short status)
        {
            const string sql = "UPDATE transactions.transaction_master SET verification_status_id=@Status, verified_by_user_id=@UserId, verification_reason=@Reason WHERE transactions.transaction_master.transaction_master_id=@TransactionMasterId;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@Reason", reason);
                command.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);

                return DbOperations.ExecuteNonQuery(command);
            }
        }
    }
}