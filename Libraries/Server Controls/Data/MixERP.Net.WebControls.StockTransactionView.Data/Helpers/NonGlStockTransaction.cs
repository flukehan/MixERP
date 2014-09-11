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
using MixERP.Net.Common.Helpers;
using MixERP.Net.DBFactory;
using Npgsql;
using System;
using System.Collections.ObjectModel;
using System.Data;

namespace MixERP.Net.WebControls.StockTransactionView.Data.Helpers
{
    public static class NonGlStockTransaction
    {
        public static DataTable GetView(string book, DateTime dateFrom, DateTime dateTo, string office, string party, string priceType, string user, string referenceNumber, string statementReference)
        {
            return GetView(SessionHelper.GetUserId(), book, SessionHelper.GetOfficeId(), dateFrom, dateTo, office, party, priceType, user, referenceNumber, statementReference);
        }

        private static DataTable GetView(int userId, string book, int officeId, DateTime dateFrom, DateTime dateTo, string office, string party, string priceType, string user, string referenceNumber, string statementReference)
        {
            const string sql = "SELECT * FROM transactions.get_non_gl_product_view(@UserId, @Book, @OfficeId, @DateFrom, @DateTo, @Office, @Party, @PriceType, @User, @ReferenceNumber, @StatementReference);";

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@Book", book);
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddWithValue("@DateFrom", dateFrom);
                command.Parameters.AddWithValue("@DateTo", dateTo);
                command.Parameters.AddWithValue("@Office", office);
                command.Parameters.AddWithValue("@Party", party);
                command.Parameters.AddWithValue("@PriceType", priceType);
                command.Parameters.AddWithValue("@User", user);
                command.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);
                command.Parameters.AddWithValue("@StatementReference", statementReference);

                return DbOperations.GetDataTable(command);
            }
        }

        public static bool TransactionIdsBelongToSameParty(Collection<int> ids)
        {
            if (ids == null)
            {
                return false;
            }

            //Crate an array object to store the parameters.
            var parameters = new string[ids.Count];

            //Iterate through the ids and create a parameter array.
            for (int i = 0; i < ids.Count; i++)
            {
                parameters[i] = "@Id" + Conversion.TryCastString(i);
            }

            //Get the count of distinct number of parties associated with the transaction id.
            string sql = "SELECT COUNT(DISTINCT party_id) FROM transactions.non_gl_stock_master WHERE non_gl_stock_master_id IN(" + string.Join(",", parameters) + ");";

            //Create a PostgreSQL command object from the SQL string.
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                //Iterate through the IDs and add PostgreSQL parameters.
                for (int i = 0; i < ids.Count; i++)
                {
                    command.Parameters.AddWithValue("@Id" + Conversion.TryCastString(i), ids[i]);
                }

                //If the transactions associated with the supplied transaction ids belong to a single part,
                //the value should equal to 1.
                return Conversion.TryCastInteger(DbOperations.GetScalarValue(command)).Equals(1);
            }
        }

        public static bool AreSalesQuotationsAlreadyMerged(Collection<int> ids)
        {
            if (ids == null)
            {
                return false;
            }

            //Crate an array object to store the parameters.
            var parameters = new string[ids.Count];

            //Iterate through the ids and create a parameter array.
            for (int i = 0; i < ids.Count; i++)
            {
                parameters[i] = "@Id" + Conversion.TryCastString(i);
            }

            string sql = @"SELECT transactions.are_sales_quotations_already_merged(" + string.Join(",", parameters) + ");";

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                //Iterate through the IDs and add PostgreSQL parameters.
                for (int i = 0; i < ids.Count; i++)
                {
                    command.Parameters.AddWithValue("@Id" + Conversion.TryCastString(i), ids[i]);
                }

                return Conversion.TryCastBoolean(DbOperations.GetScalarValue(command));
            }
        }

        public static bool AreSalesOrdersAlreadyMerged(Collection<int> ids)
        {
            if (ids == null)
            {
                return false;
            }

            //Crate an array object to store the parameters.
            var parameters = new string[ids.Count];

            //Iterate through the ids and create a parameter array.
            for (int i = 0; i < ids.Count; i++)
            {
                parameters[i] = "@Id" + Conversion.TryCastString(i);
            }

            string sql = @"SELECT transactions.are_sales_orders_already_merged(" + string.Join(",", parameters) + ");";

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                //Iterate through the IDs and add PostgreSQL parameters.
                for (int i = 0; i < ids.Count; i++)
                {
                    command.Parameters.AddWithValue("@Id" + Conversion.TryCastString(i), ids[i]);
                }

                return Conversion.TryCastBoolean(DbOperations.GetScalarValue(command));
            }
        }
    }
}