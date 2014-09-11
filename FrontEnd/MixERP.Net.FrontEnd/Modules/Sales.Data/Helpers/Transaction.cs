using MixERP.Net.Common;
using MixERP.Net.Common.Helpers;
using MixERP.Net.Common.Models.Transactions;
using MixERP.Net.DBFactory;
using Npgsql;
using System;
using System.Collections.ObjectModel;

namespace MixERP.Net.Core.Modules.Sales.Data.Helpers
{
    public static class Transaction
    {
        public static decimal GetExchangeRate(int officeId, string sourceCurrencyCode, string destinationCurrencyCode)
        {
            const string sql = "SELECT transactions.get_exchange_rate(@OfficeId, @SourceCurrencyCode, @DestinationCurrencyCode);";

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddWithValue("@SourceCurrencyCode", sourceCurrencyCode);
                command.Parameters.AddWithValue("@DestinationCurrencyCode", destinationCurrencyCode);

                return Conversion.TryCastDecimal(DbOperations.GetScalarValue(command));
            }
        }
    }
}