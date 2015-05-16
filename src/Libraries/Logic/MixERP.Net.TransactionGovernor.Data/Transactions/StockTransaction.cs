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

using MixERP.Net.Common;
using MixERP.Net.DbFactory;
using Npgsql;

namespace MixERP.Net.TransactionGovernor.Data.Transactions
{
    public static class StockTransaction
    {
        public static bool IsValidPartyByStockMasterId(string catalog, long stockMasterId, string partyCode)
        {
            const string sql = "SELECT transactions.is_valid_party_by_stock_master_id(@StockMasterId, core.get_party_id_by_party_code(@PartyCode));";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@StockMasterId", stockMasterId);
                command.Parameters.AddWithValue("@PartyCode", partyCode);

                return Conversion.TryCastBoolean(DbOperation.GetScalarValue(catalog, command));
            }
        }

        public static bool IsValidPartyByTransactionMasterId(string catalog, long transactionMasterId, string partyCode)
        {
            const string sql = "SELECT transactions.is_valid_party_by_transaction_master_id(@TransactionMasterId, core.get_party_id_by_party_code(@PartyCode));";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);
                command.Parameters.AddWithValue("@PartyCode", partyCode);

                return Conversion.TryCastBoolean(DbOperation.GetScalarValue(catalog, command));
            }
        }

        public static bool IsValidStockTransaction(string catalog, long transactionMasterId)
        {
            const string sql = "SELECT transactions.is_valid_stock_transaction_by_transaction_master_id(@TransactionMasterId);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@TransactionMasterId", transactionMasterId);

                return Conversion.TryCastBoolean(DbOperation.GetScalarValue(catalog, command));
            }
        }

        public static bool IsValidStockTransactionByStockMasterId(string catalog, long stockMasterId)
        {
            const string sql = "SELECT transactions.is_valid_stock_transaction_by_stock_master_id(@StockMasterId::bigint);";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@StockMasterId", stockMasterId);

                return Conversion.TryCastBoolean(DbOperation.GetScalarValue(catalog, command));
            }
        }
    }
}