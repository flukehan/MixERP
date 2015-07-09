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
using MixERP.Net.Entities.Core;
using MixERP.Net.Entities.Models.Transactions;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;

namespace MixERP.Net.WebControls.StockTransactionFactory.Helpers
{
    public static class CollectionHelper
    {
        public static Collection<Attachment> GetAttachmentCollection(string json)
        {
            Collection<Attachment> details = new Collection<Attachment>();
            var jss = new JavaScriptSerializer();

            dynamic result = jss.Deserialize<dynamic>(json);

            if (result != null)
            {
                foreach (dynamic item in result)
                {
                    Attachment detail = new Attachment();
                    detail.Comment = item["Comment"];
                    detail.FilePath = item["FilePath"];
                    detail.OriginalFileName = item["OriginalFileName"];

                    details.Add(detail);
                }
            }

            return details;
        }

        public static Collection<StockDetail> GetStockMasterDetailCollection(string json, int storeId)
        {
            Collection<StockDetail> details = new Collection<StockDetail>();
            var jss = new JavaScriptSerializer();

            dynamic result = jss.Deserialize<dynamic>(json);

            foreach (dynamic item in result)
            {
                StockDetail detail = new StockDetail();
                detail.ItemCode = item[0];
                detail.Quantity = Conversion.TryCastInteger(item[2]);
                detail.UnitName = item[3];
                detail.Price = Conversion.TryCastDecimal(item[4]);
                detail.Discount = Conversion.TryCastDecimal(item[6]);
                detail.ShippingCharge = Conversion.TryCastDecimal(item[7]);
                detail.TaxForm = item[9];
                detail.Tax = Conversion.TryCastDecimal(item[10]);
                detail.StoreId = storeId;

                details.Add(detail);
            }

            return details;
        }
    }
}