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
using MixERP.Net.BusinessLayer.Transactions;
using MixERP.Net.Common;
using MixERP.Net.Common.Models.Core;
using MixERP.Net.Common.Models.Transactions;
using System;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;
using System.Web.Services;
using MixERP.Net.WebControls.StockTransactionFactory.Helpers;

namespace MixERP.Net.FrontEnd.Services.Sales
{
    /// <summary>
    /// Summary description for SalesQuotation
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Quotation : WebService
    {

        [WebMethod(EnableSession = true)]
        public long Save(DateTime valueDate, int storeId, string partyCode, int priceTypeId, string referenceNumber, string data, string statementReference, string transactionIds, string attachmentsJSON)
        {
            Collection<StockMasterDetailModel> details = CollectionHelper.GetDetails(data, storeId);
            Collection<int> tranIds = new Collection<int>();

            JavaScriptSerializer js = new JavaScriptSerializer();
            Collection<Attachment> attachments = js.Deserialize<Collection<Attachment>>(attachmentsJSON);

            if (!string.IsNullOrWhiteSpace(transactionIds))
            {
                foreach (var transactionId in transactionIds.Split(','))
                {
                    tranIds.Add(Conversion.TryCastInteger(transactionId));
                }
            }


            return NonGlStockTransaction.Add("Sales.Quotation", valueDate, partyCode, priceTypeId, details, referenceNumber, statementReference, tranIds, attachments);
        }
    }
}
