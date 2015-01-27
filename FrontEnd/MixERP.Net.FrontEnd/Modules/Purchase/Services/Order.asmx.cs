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
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace MixERP.Net.Core.Modules.Purchase.Services
{
    /// <summary>
    /// Summary description for PurchaseOrder
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the
    // following line.
    [System.Web.Script.Services.ScriptService]
    public class Order : WebService
    {
        [WebMethod(EnableSession = true)]
        public long Save(DateTime valueDate, string partyCode, string referenceNumber, string data, string statementReference, string attachmentsJSON)
        {
            Collection<StockMasterDetailModel> details = CollectionHelper.GetStockMasterDetailCollection(data, 0);

            JavaScriptSerializer js = new JavaScriptSerializer();
            Collection<PostgresqlAttachmentModel> attachments = js.Deserialize<Collection<PostgresqlAttachmentModel>>(attachmentsJSON);

            return Data.Transactions.Order.Add("Purchase.Order", valueDate, partyCode, 0, details, referenceNumber, statementReference, null, attachments);
        }
    }
}