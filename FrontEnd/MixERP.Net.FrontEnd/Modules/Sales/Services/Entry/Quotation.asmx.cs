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
using MixERP.Net.WebControls.StockTransactionFactory.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace MixERP.Net.Core.Modules.Sales.Services.Entry
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class Quotation : WebService
    {
        [WebMethod(EnableSession = true)]
        public long Save(DateTime valueDate, int storeId, string partyCode, int priceTypeId, string referenceNumber, string data, string statementReference, string transactionIds, string attachmentsJSON)
        {
            Collection<StockMasterDetailModel> details = CollectionHelper.GetStockMasterDetailCollection(data, storeId);
            Collection<int> tranIds = new Collection<int>();

            JavaScriptSerializer js = new JavaScriptSerializer();
            Collection<AttachmentModel> attachments = js.Deserialize<Collection<AttachmentModel>>(attachmentsJSON);

            if (!string.IsNullOrWhiteSpace(transactionIds))
            {
                foreach (var transactionId in transactionIds.Split(','))
                {
                    tranIds.Add(Common.Conversion.TryCastInteger(transactionId));
                }
            }

            return Data.Helpers.Quotation.Add(valueDate, partyCode, priceTypeId, details, referenceNumber, statementReference, tranIds, attachments);
        }
    }
}