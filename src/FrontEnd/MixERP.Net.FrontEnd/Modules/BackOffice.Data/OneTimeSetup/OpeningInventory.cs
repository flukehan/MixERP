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
using MixERP.Net.Entities.Transactions;
using MixERP.Net.i18n.Resources;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace MixERP.Net.Core.Modules.BackOffice.Data.OneTimeSetup
{
    public static class OpeningInventory
    {
        public static long Save(string catalog, int officeId, int userId, long loginId, DateTime valueDate, string referenceNumber, string statementReference, Collection<OpeningStockType> details)
        {
            if (details == null)
            {
                throw new InvalidOperationException(Warnings.NoTransactionToPost);
            }

            if (details.Count().Equals(0))
            {
                throw new InvalidOperationException(Warnings.NoTransactionToPost);
            }

            string detail = CreateOpeningStockParameter(details);
            string sql = string.Format(CultureInfo.InvariantCulture, "SELECT * FROM transactions.post_opening_inventory(@OfficeId::integer, @UserId::integer, @LoginId::bigint, @ValueDate::date, @ReferenceNumber::national character varying(24), @StatementReference::text, ARRAY[{0}])", detail);

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@LoginId", loginId);
                command.Parameters.AddWithValue("@ValueDate", valueDate);
                command.Parameters.AddWithValue("@ReferenceNumber", referenceNumber);
                command.Parameters.AddWithValue("@StatementReference", statementReference);


                command.Parameters.AddRange(AddOpeningStockParamter(details).ToArray());

                long tranId = Conversion.TryCastLong(DbOperation.GetScalarValue(catalog, command));
                return tranId;
            }
        }

        private static IEnumerable<NpgsqlParameter> AddOpeningStockParamter(Collection<OpeningStockType> details)
        {
            Collection<NpgsqlParameter> collection = new Collection<NpgsqlParameter>();

            if (details != null)
            {
                for (int i = 0; i < details.Count; i++)
                {
                    collection.Add(new NpgsqlParameter("@StoreName" + i, details[i].StoreName));
                    collection.Add(new NpgsqlParameter("@ItemCode" + i, details[i].ItemCode));
                    collection.Add(new NpgsqlParameter("@Quantity" + i, details[i].Quantity));
                    collection.Add(new NpgsqlParameter("@UnitName" + i, details[i].UnitName));
                    collection.Add(new NpgsqlParameter("@Amount" + i, details[i].Amount));
                }
            }

            return collection;
        }

        public static string CreateOpeningStockParameter(Collection<OpeningStockType> details)
        {
            if (details == null)
            {
                return "NULL::transactions.opening_stock_type";
            }

            Collection<string> detailCollection = new Collection<string>();
            for (int i = 0; i < details.Count; i++)
            {
                detailCollection.Add(string.Format(CultureInfo.InvariantCulture, "ROW(@StoreName{0}, @ItemCode{0}, @Quantity{0}, @UnitName{0},@Amount{0})::transactions.opening_stock_type", i.ToString(CultureInfo.InvariantCulture)));
            }

            return string.Join(",", detailCollection);
        }
    }
}