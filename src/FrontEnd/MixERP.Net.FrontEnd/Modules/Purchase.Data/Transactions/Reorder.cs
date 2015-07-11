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
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using MixERP.Net.DbFactory;
using MixERP.Net.Entities.Transactions;
using Npgsql;
using PetaPoco;

namespace MixERP.Net.Core.Modules.Purchase.Data.Transactions
{
    public static class Reorder
    {
        public static IEnumerable<NpgsqlParameter> AddPurchaseReorderTypeParameter(Collection<Models.Reorder> details)
        {
            Collection<NpgsqlParameter> collection = new Collection<NpgsqlParameter>();

            if (details != null)
            {
                for (int i = 0; i < details.Count; i++)
                {
                    collection.Add(new NpgsqlParameter("@ItemId" + i, details[i].ItemId));
                    collection.Add(new NpgsqlParameter("@SupplierCode" + i, details[i].SupplierCode));
                    collection.Add(new NpgsqlParameter("@UnitId" + i, details[i].UnitId));
                    collection.Add(new NpgsqlParameter("@Price" + i, details[i].Price));
                    collection.Add(new NpgsqlParameter("@Tax" + i, details[i].Tax ?? string.Empty));
                    collection.Add(new NpgsqlParameter("@OrderQuantity" + i, details[i].OrderQuantity));
                }
            }

            return collection;
        }

        public static string CreatePurchaseReorderTypeParameter(Collection<Models.Reorder> details)
        {
            if (details == null)
            {
                return "NULL::stock_detail_type";
            }

            Collection<string> detailCollection = new Collection<string>();
            for (int i = 0; i < details.Count; i++)
            {
                detailCollection.Add(string.Format(CultureInfo.InvariantCulture,
                    "ROW(@ItemId{0}, @SupplierCode{0}, @UnitId{0}, @Price{0},@Tax{0}, @OrderQuantity{0})::transactions.purchase_reorder_type",
                    i.ToString(CultureInfo.InvariantCulture)));
            }

            return string.Join(",", detailCollection);
        }

        public static IEnumerable<DbGetReorderViewFunctionResult> GetReorderView(string catalog, int officeId)
        {
            const string sql = "SELECT * FROM transactions.get_reorder_view_function(@0::integer);";
            return Factory.Get<DbGetReorderViewFunctionResult>(catalog, sql, officeId);
        }

        public static bool Save(string catalog, long loginId, int userId, int officeId,
            Collection<Models.Reorder> details)
        {
            string sql = string.Format(CultureInfo.InvariantCulture,
                "SELECT * FROM transactions.post_purhcase_reorder(transactions.get_value_date(@OfficeId::integer)::date, @LoginId::bigint, @UserId::integer, @OfficeId::integer, ARRAY[{0}]);",
                CreatePurchaseReorderTypeParameter(details));

            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                command.Parameters.AddWithValue("@LoginId", loginId);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@OfficeId", officeId);
                command.Parameters.AddRange(AddPurchaseReorderTypeParameter(details).ToArray());

                return DbOperation.ExecuteNonQuery(catalog, command);
            }
        }
    }
}