using MixERP.Net.Entities;
using MixERP.Net.Entities.Models.Transactions;
using Npgsql;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

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

namespace MixERP.Net.Core.Modules.Inventory.Data.Helpers
{
    public static class ParameterHelper
    {
        public static IEnumerable<NpgsqlParameter> AddStockTransferModelParameter(Collection<StockAdjustmentDetail> details)
        {
            Collection<NpgsqlParameter> collection = new Collection<NpgsqlParameter>();

            if (details != null)
            {
                for (int i = 0; i < details.Count; i++)
                {
                    string type = "Cr";
                    if (details[i].TransferTypeEnum.Equals(TransactionTypeEnum.Debit))
                    {
                        type = "Dr";
                    }

                    collection.Add(new NpgsqlParameter("@TranType" + i, type));
                    collection.Add(new NpgsqlParameter("@StoreName" + i, details[i].StoreName));
                    collection.Add(new NpgsqlParameter("@ItemCode" + i, details[i].ItemCode));
                    collection.Add(new NpgsqlParameter("@UnitName" + i, details[i].UnitName));
                    collection.Add(new NpgsqlParameter("@Quantity" + i, details[i].Quantity));
                }
            }

            return collection;
        }

        public static string CreateStockTransferModelParameter(Collection<StockAdjustmentDetail> details)
        {
            if (details == null)
            {
                return "NULL::transactions.stock_adjustment_type";
            }

            Collection<string> detailCollection = new Collection<string>();

            for (int i = 0; i < details.Count; i++)
            {
                detailCollection.Add(string.Format(CultureInfo.InvariantCulture, "ROW(@TranType{0}, @StoreName{0}, @ItemCode{0}, @UnitName{0},@Quantity{0})::transactions.stock_adjustment_type", i.ToString(CultureInfo.InvariantCulture)));
            }

            return string.Join(",", detailCollection);
        }
    }
}