using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Transactions;
using System;
using System.Collections.ObjectModel;

namespace MixERP.Net.Core.Modules.Sales.Data.Helpers
{
    public static class Transaction
    {
        public static decimal GetExchangeRate(int officeId, string sourceCurrencyCode, string destinationCurrencyCode)
        {
            return DatabaseLayer.Transactions.Transaction.GetExchangeRate(officeId, sourceCurrencyCode, destinationCurrencyCode);
        }
    }
}