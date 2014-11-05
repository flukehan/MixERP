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

using MixERP.Net.Common.Models.Core;
using MixERP.Net.Common.Models.Transactions;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.Core.Modules.Sales.Data.Helpers
{
    public static class ParameterHelper
    {
        public static IEnumerable<NpgsqlParameter> AddStockMasterDetailParameter(Collection<StockMasterDetailModel> details)
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
                    collection.Add(new NpgsqlParameter("@TaxRate" + i, details[i].TaxRate));
                    collection.Add(new NpgsqlParameter("@Tax" + i, details[i].Tax));
                }
            }

            return collection;
        }

        public static IEnumerable<NpgsqlParameter> AddAttachmentParameter(Collection<AttachmentModel> attachments)
        {
            Collection<NpgsqlParameter> collection = new Collection<NpgsqlParameter>();

            if (attachments != null)
            {
                for (int i = 0; i < attachments.Count; i++)
                {
                    collection.Add(new NpgsqlParameter("@Comment" + i, attachments[i].Comment));
                    collection.Add(new NpgsqlParameter("@FilePath" + i, attachments[i].FilePath));
                    collection.Add(new NpgsqlParameter("@OriginalFileName" + i, attachments[i].OriginalFileName));
                }
            }

            return collection;
        }

        public static string CreateStockMasterDetailParameter(Collection<StockMasterDetailModel> details)
        {
            if (details == null)
            {
                return "NULL::stock_detail_type";
            }

            Collection<string> detailCollection = new Collection<string>();
            for (int i = 0; i < details.Count; i++)
            {
                detailCollection.Add(string.Format("ROW(@StoreId{0}, @ItemCode{0}, @Quantity{0}, @UnitName{0},@Price{0}, @Discount{0}, @TaxRate{0}, @Tax{0})::stock_detail_type", i.ToString(CultureInfo.InvariantCulture)));
            }

            return string.Join(",", detailCollection);
        }

        public static string CreateAttachmentModelParameter(Collection<AttachmentModel> attachments)
        {
            if (attachments == null)
            {
                return "NULL::core.attachment_type";
            }

            Collection<string> attachmentCollection = new Collection<string>();

            for (int i = 0; i < attachments.Count; i++)
            {
                attachmentCollection.Add(string.Format("ROW(@Comment{0}, @FilePath{0}, @OriginalFileName{0})::core.attachment_type", i.ToString(CultureInfo.InvariantCulture)));
            }

            return string.Join(",", attachmentCollection);
        }
    }
}