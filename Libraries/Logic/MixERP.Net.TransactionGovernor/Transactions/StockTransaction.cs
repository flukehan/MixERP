using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.TransactionGovernor.Transactions
{
    public static class StockTransaction
    {
        public static bool IsValidStockTransaction(long transactionMasterId)
        {
            if (transactionMasterId <= 0)
            {
                return false;
            }

            return Data.Transactions.StockTransaction.IsValidStockTransaction(transactionMasterId);
        }

        public static bool IsValidPartyByTransactionMasterId(long transactionMasterId, string partyCode)
        {
            if (transactionMasterId <= 0)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(partyCode))
            {
                return false;
            }

            return Data.Transactions.StockTransaction.IsValidPartyByTransactionMasterId(transactionMasterId, partyCode);
        }
    }
}