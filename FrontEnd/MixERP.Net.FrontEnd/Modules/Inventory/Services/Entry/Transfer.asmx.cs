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
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace MixERP.Net.Core.Modules.Inventory.Services.Entry
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class Transfer : WebService
    {
        [WebMethod(EnableSession = true)]
        public long Save(DateTime valueDate, string referenceNumber, string statementReference, string data)
        {
            Collection<StockTransferModel> collection = this.GetModels(data);

            return 0;
        }

        private Collection<StockTransferModel> GetModels(string json)
        {
            Collection<StockTransferModel> models = new Collection<StockTransferModel>();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            dynamic result = jss.Deserialize<dynamic>(json);

            foreach (var item in result)
            {
                StockTransferModel model = new StockTransferModel();

                model.TransferType = Conversion.TryCastString(item[0]);
                model.StoreName = Conversion.TryCastString(item[1]);
                model.ItemCode = Conversion.TryCastString(item[2]);
                model.ItemName = Conversion.TryCastString(item[3]);
                model.UnitName = Conversion.TryCastString(item[4]);
                model.Quantity = Conversion.TryCastInteger(item[5]);

                models.Add(model);
            }

            return models;
        }
    }
}