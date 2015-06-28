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

using System.Collections.Generic;
using MixERP.Net.Common;
using MixERP.Net.Core.Modules.Finance.Data.Models;
using MixERP.Net.DbFactory;
using Npgsql;
using Serilog;

namespace MixERP.Net.Core.Modules.Finance.Data.Core
{
    public static class ExchangeRates
    {
        public static long SaveExchangeRates(string catalog, int officeId, string baseCurrency, IEnumerable<ExchangeRate> exchangeRates)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.GetConnectionString(catalog)))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sql = "UPDATE core.exchange_rates SET status = false WHERE office_id=@OfficeId";
                        using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@OfficeId", officeId);
                            command.ExecuteNonQuery();
                        }

                        sql = "INSERT INTO core.exchange_rates(office_id, status) SELECT @OfficeId, true RETURNING exchange_rate_id;";

                        long exchangeRateId;
                        using (NpgsqlCommand er = new NpgsqlCommand(sql, connection))
                        {
                            er.Parameters.AddWithValue("@OfficeId", officeId);

                            exchangeRateId = Conversion.TryCastLong(er.ExecuteScalar());
                        }

                        foreach (ExchangeRate exchangeRate in exchangeRates)
                        {
                            sql =
                                "INSERT INTO core.exchange_rate_details(exchange_rate_id, local_currency_code, foreign_currency_code, unit, exchange_rate) " +
                                "SELECT @ExchangeRateId, @LocalCurrencyCode, @ForiegnCurrencyCode, 1, @ExchangeRate;";
                            using (NpgsqlCommand rate = new NpgsqlCommand(sql, connection))
                            {
                                rate.Parameters.AddWithValue("@ExchangeRateId", exchangeRateId);
                                rate.Parameters.AddWithValue("@LocalCurrencyCode", baseCurrency);
                                rate.Parameters.AddWithValue("@ForiegnCurrencyCode", exchangeRate.CurrencyCode);
                                rate.Parameters.AddWithValue("@ExchangeRate", exchangeRate.Rate);

                                rate.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        return exchangeRateId;
                    }
                    catch (NpgsqlException ex)
                    {
                        Log.Warning(@"Could not update exchange rate. {Exception}", ex);
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}