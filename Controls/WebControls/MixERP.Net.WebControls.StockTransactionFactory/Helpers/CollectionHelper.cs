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
using MixERP.Net.Common.Models.Transactions;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;

namespace MixERP.Net.WebControls.StockTransactionFactory.Helpers
{
    public class CollectionHelper
    {
        public static Collection<StockMasterDetailModel> GetDetails(string json, int storeId)
        {
            Collection<StockMasterDetailModel> details = new Collection<StockMasterDetailModel>();
            var jss = new JavaScriptSerializer();

            dynamic result = jss.Deserialize<dynamic>(json);

            foreach (var item in result)
            {
                StockMasterDetailModel detail = new StockMasterDetailModel();
                detail.ItemCode = item[0];
                detail.Quantity = Conversion.TryCastInteger(item[2]);
                detail.UnitName = item[3];
                detail.Price = Conversion.TryCastDecimal(item[4]);
                detail.Discount = Conversion.TryCastDecimal(item[6]);
                detail.TaxRate = Conversion.TryCastDecimal(item[8]);
                detail.Tax = Conversion.TryCastDecimal(item[9]);
                detail.StoreId = storeId;

                details.Add(detail);
            }

            return details;
        }

    }
}
