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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MixERP.Net.Core.Modules.Sales.Services.Entry
{
    /// <summary>
    /// Summary description for Order
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    [System.Web.Script.Services.ScriptService]
    public class Order : WebService
    {
        [WebMethod(EnableSession = true)]
        public long Save(DateTime valueDate, int storeId, string partyCode, int priceTypeId, string referenceNumber, string data, string statementReference, string transactionIds, string attachmentsJSON)
        {
            System.Collections.ObjectModel.Collection<Common.Models.Transactions.StockMasterDetailModel> details = WebControls.StockTransactionFactory.Helpers.CollectionHelper.GetStockMasterDetailCollection(data, storeId);
            System.Collections.ObjectModel.Collection<int> tranIds = new System.Collections.ObjectModel.Collection<int>();

            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            System.Collections.ObjectModel.Collection<Common.Models.Core.AttachmentModel> attachments = js.Deserialize<System.Collections.ObjectModel.Collection<Common.Models.Core.AttachmentModel>>(attachmentsJSON);

            if (!string.IsNullOrWhiteSpace(transactionIds))
            {
                foreach (var transactionId in transactionIds.Split(','))
                {
                    tranIds.Add(Common.Conversion.TryCastInteger(transactionId));
                }
            }

            return Data.Helpers.Order.Add(valueDate, partyCode, priceTypeId, details, referenceNumber, statementReference, tranIds, attachments);
        }
    }
}