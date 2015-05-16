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

namespace MixERP.Net.TransactionGovernor.Transactions
{
    public static class StockTransaction
    {
        public static bool IsValidPartyByStockMasterId(string catalog, long stockMasterId, string partyCode)
        {
            if (stockMasterId <= 0 || string.IsNullOrWhiteSpace(partyCode))
            {
                return false;
            }

            return Data.Transactions.StockTransaction.IsValidPartyByStockMasterId(catalog, stockMasterId, partyCode);
        }

        public static bool IsValidPartyByTransactionMasterId(string catalog, long transactionMasterId, string partyCode)
        {
            if (transactionMasterId <= 0 || string.IsNullOrWhiteSpace(partyCode))
            {
                return false;
            }

            return Data.Transactions.StockTransaction.IsValidPartyByTransactionMasterId(catalog, transactionMasterId, partyCode);
        }

        public static bool IsValidStockTransactionByStockMasterId(string catalog, long stockMasterId)
        {
            if (stockMasterId <= 0)
            {
                return false;
            }

            return Data.Transactions.StockTransaction.IsValidStockTransactionByStockMasterId(catalog, stockMasterId);
        }

        public static bool IsValidStockTransactionByTransactionMasterId(string catalog, long transactionMasterId)
        {
            if (transactionMasterId <= 0)
            {
                return false;
            }

            return Data.Transactions.StockTransaction.IsValidStockTransaction(catalog, transactionMasterId);
        }

    }
}