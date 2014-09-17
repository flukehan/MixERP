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