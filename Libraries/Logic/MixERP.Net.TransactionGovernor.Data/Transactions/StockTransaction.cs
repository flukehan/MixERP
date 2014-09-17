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

        public static bool IsValidStockTransactionByStockMasterId(long stockMasterId)
        {
            const string sql = "SELECT transactions.is_valid_stock_transaction_by_stock_master_id(@StockMasterId::bigint);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@StockMasterId", stockMasterId);

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

        public static bool IsValidPartyByStockMasterId(long stockMasterId, string partyCode)
        {
            const string sql = "SELECT transactions.is_valid_party_by_stock_master_id(@StockMasterId, core.get_party_id_by_party_code(@PartyCode));";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@StockMasterId", stockMasterId);
                command.Parameters.AddWithValue("@PartyCode", partyCode);

                return Conversion.TryCastBoolean(DbOperations.GetScalarValue(command));
            }
        }

        public static bool ValidateItemForReturn(long stockMasterId, int storeId, string itemCode, string unit, int quantity, decimal price)
        {
            const string sql = "SELECT transactions.validate_item_for_return(@StockMasterId, @StoreId, @ItemCode, @UnitName, @Quantity, @Price);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@StockMasterId", stockMasterId);
                command.Parameters.AddWithValue("@StoreId", storeId);
                command.Parameters.AddWithValue("@ItemCode", itemCode);
                command.Parameters.AddWithValue("@UnitName", unit);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@Price", price);

                return Conversion.TryCastBoolean(DbOperations.GetScalarValue(command));
            }
        }
    }
}