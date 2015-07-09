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

using MixERP.Net.Entities.Models.Transactions;
using Npgsql;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace MixERP.Net.Core.Modules.Sales.Data
{
    public static class StockMasterDetailHelper
    {
        public static IEnumerable<NpgsqlParameter> AddStockMasterDetailParameter(Collection<StockDetail> details)
        {
            Collection<NpgsqlParameter> collection = new Collection<NpgsqlParameter>();

            if (details != null)
            {
                for (int i = 0; i < details.Count; i++)
                {
                    collection.Add(new NpgsqlParameter("@StoreId" + i, details[i].StoreId));
                    collection.Add(new NpgsqlParameter("@ItemCode" + i, details[i].ItemCode));
                    collection.Add(new NpgsqlParameter("@Quantity" + i, details[i].Quantity));
                    collection.Add(new NpgsqlParameter("@UnitName" + i, details[i].UnitName));
                    collection.Add(new NpgsqlParameter("@Price" + i, details[i].Price));
                    collection.Add(new NpgsqlParameter("@Discount" + i, details[i].Discount));
                    collection.Add(new NpgsqlParameter("@ShippingCharge" + i, details[i].ShippingCharge));
                    collection.Add(new NpgsqlParameter("@TaxForm" + i, details[i].TaxForm));
                    collection.Add(new NpgsqlParameter("@Tax" + i, details[i].Tax));
                }
            }

            return collection;
        }

        public static string CreateStockMasterDetailParameter(Collection<StockDetail> details)
        {
            if (details == null)
            {
                return "NULL::transactions.stock_detail_type";
            }

            Collection<string> detailCollection = new Collection<string>();
            for (int i = 0; i < details.Count; i++)
            {
                detailCollection.Add(string.Format(CultureInfo.InvariantCulture, "ROW(@StoreId{0}, @ItemCode{0}, @Quantity{0}, @UnitName{0},@Price{0}, @Discount{0}, @ShippingCharge{0}, @TaxForm{0}, @Tax{0})::transactions.stock_detail_type", i.ToString(CultureInfo.InvariantCulture)));
            }

            return string.Join(",", detailCollection);
        }
    }
}