using MixERP.Net.Common.Helpers;
using MixERP.Net.DBFactory;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.TransactionGovernor.Data.Autoverification
{
    public static class Autoverify
    {
        public static bool Pass(long transactionMasterId)
        {
            const string sql = "SELECT transactions.auto_verify(@TransactionMasterId::bigint);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);
                return DbOperations.ExecuteNonQuery(command);
            }
        }
    }
}