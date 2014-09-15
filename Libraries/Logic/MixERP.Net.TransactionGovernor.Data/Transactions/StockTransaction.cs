using MixERP.Net.Common;
using MixERP.Net.DBFactory;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.TransactionGovernor.Data.Transactions
{
    public static class StockTransaction
    {
        public static bool IsValidStockTransaction(long transactionMasterId)
        {
            const string sql = "SELECT transactions.is_valid_stock_transaction_by_transaction_master_id(@TransactionMasterId);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);

                return Conversion.TryCastBoolean(DbOperations.GetScalarValue(command));
            }
        }

        public static bool IsValidPartyByTransactionMasterId(long transactionMasterId, string partyCode)
        {
            const string sql = "SELECT transactions.is_valid_party_by_transaction_master_id(@TransactionMasterId, core.get_party_id_by_party_code(@PartyCode));";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);
                command.Parameters.AddWithValue("@PartyCode", partyCode);

                return Conversion.TryCastBoolean(DbOperations.GetScalarValue(command));
            }
        }
    }
}