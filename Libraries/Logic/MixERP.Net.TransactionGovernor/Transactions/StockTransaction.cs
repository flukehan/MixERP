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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.TransactionGovernor.Transactions
{
    public static class StockTransaction
    {
        public static bool IsValidStockTransactionByTransactionMasterId(long transactionMasterId)
        {
            if (transactionMasterId <= 0)
            {
                return false;
            }

            return Data.Transactions.StockTransaction.IsValidStockTransaction(transactionMasterId);
        }

        public static bool IsValidStockTransactionByStockMasterId(long stockMasterId)
        {
            if (stockMasterId <= 0)
            {
                return false;
            }

            return Data.Transactions.StockTransaction.IsValidStockTransactionByStockMasterId(stockMasterId);
        }

        public static bool IsValidPartyByTransactionMasterId(long transactionMasterId, string partyCode)
        {
            if (transactionMasterId <= 0 || string.IsNullOrWhiteSpace(partyCode))
            {
                return false;
            }

            return Data.Transactions.StockTransaction.IsValidPartyByTransactionMasterId(transactionMasterId, partyCode);
        }

        public static bool IsValidPartyByStockMasterId(long stockMasterId, string partyCode)
        {
            if (stockMasterId <= 0 || string.IsNullOrWhiteSpace(partyCode))
            {
                return false;
            }

            return Data.Transactions.StockTransaction.IsValidPartyByStockMasterId(stockMasterId, partyCode);
        }

        public static bool ValidateItemForReturn(long stockMasterId, int storeId, string itemCode, string unit, int quantity, decimal price)
        {
            if (stockMasterId <= 0 || storeId <= 0 || string.IsNullOrWhiteSpace(itemCode) || string.IsNullOrWhiteSpace(unit) || quantity <= 0 || price <= 0)
            {
                return false;
            }

            return Data.Transactions.StockTransaction.ValidateItemForReturn(stockMasterId, storeId, itemCode, unit, quantity, price);
        }
    }
}